﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace SystemTrayMenu.Utilities
{
    // from https://www.codeproject.com/Articles/2532/Obtaining-and-managing-file-and-folder-icons-using
    // added ImageList_GetIcon, IconCache, AddIconOverlay

    /// <summary>
    /// Provides static methods to read system icons for both folders and files.
    /// </summary>
    /// <example>
    /// <code>IconReader.GetFileIcon("c:\\general.xls");</code>
    /// </example>
    public static class IconReader
    {
        private static readonly ConcurrentDictionary<string, Icon> dictIconCache = new ConcurrentDictionary<string, Icon>();

        public enum IconSize
        {
            Large = 0, //32x32 pixels
            Small = 1 //16x16 pixels
        }

        public enum FolderType
        {
            Open = 0,
            Closed = 1
        }

        public static Icon GetFileIconWithCache(string filePath, bool linkOverlay,
            IconSize size = IconSize.Small)
        {
            Icon icon = null;
            string extension = Path.GetExtension(filePath);
            bool IsExtensionWitSameIcon(string fileExtension)
            {
                bool isExtensionWitSameIcon = true;
                List<string> extensionsWithDiffIcons = new List<string>
                { ".exe", ".lnk", ".ico", ".url" };
                if (extensionsWithDiffIcons.Contains(fileExtension.ToLower()))
                {
                    isExtensionWitSameIcon = false;
                }
                return isExtensionWitSameIcon;
            }

            if (IsExtensionWitSameIcon(extension))
            {
                icon = dictIconCache.GetOrAdd(extension, GetIcon);
                Icon GetIcon(string keyExtension)
                {
                    return GetFileIcon(filePath, linkOverlay, size);
                }
            }
            else
            {
                icon = GetFileIcon(filePath, linkOverlay, size);
            }

            return icon;
        }

        private static Icon GetFileIcon(string filePath, bool linkOverlay,
            IconSize size = IconSize.Small)
        {
            Icon icon = null;
            NativeDllImport.NativeMethods.SHFILEINFO shfi = new NativeDllImport.NativeMethods.SHFILEINFO();
            uint flags = NativeDllImport.NativeMethods.ShgfiIcon | NativeDllImport.NativeMethods.ShgfiSYSICONINDEX;

            //MH: Removed, otherwise wrong icon
            // | Shell32.SHGFI_USEFILEATTRIBUTES ;

            if (true == linkOverlay)
            {
                flags += NativeDllImport.NativeMethods.ShgfiLINKOVERLAY;
            }

            /* Check the size specified for return. */
            if (IconSize.Small == size)
            {
                flags += NativeDllImport.NativeMethods.ShgfiSMALLICON;
            }
            else
            {
                flags += NativeDllImport.NativeMethods.ShgfiLARGEICON;
            }

            IntPtr hImageList = NativeDllImport.NativeMethods.Shell32SHGetFileInfo(filePath,
                NativeDllImport.NativeMethods.FileAttributeNormal,
                ref shfi,
                (uint)System.Runtime.InteropServices.Marshal.SizeOf(shfi),
                flags);
            if (hImageList != IntPtr.Zero) // got valid handle?
            {
                IntPtr hIcon;
                if (linkOverlay)
                {
                    hIcon = shfi.hIcon; // Get icon directly
                }
                else
                {
                    // Get icon from .ink without overlay
                    hIcon = NativeDllImport.NativeMethods.ImageList_GetIcon(hImageList, shfi.iIcon, NativeDllImport.NativeMethods.IldTransparent);
                }

                try
                {
                    // Copy (clone) the returned icon to a new object, thus allowing us to clean-up properly
                    icon = (Icon)Icon.FromHandle(hIcon).Clone();
                }
                catch (Exception ex)
                {
                    Log.Error($"filePath:'{filePath}'", ex);
                }

                // Cleanup
                if (!linkOverlay)
                {
                    NativeDllImport.NativeMethods.User32DestroyIcon(hIcon);
                }

                NativeDllImport.NativeMethods.User32DestroyIcon(shfi.hIcon);
            }

            return icon;
        }

        public static Icon GetFolderIcon(string directoryPath,
            FolderType folderType, bool linkOverlay,
            IconSize size = IconSize.Small)
        {
            Icon icon = null;

            // Need to add size check, although errors generated at present!
            //uint flags = Shell32.SHGFI_ICON | Shell32.SHGFI_USEFILEATTRIBUTES;

            //MH: Removed SHGFI_USEFILEATTRIBUTES, otherwise was wrong folder icon
            uint flags = NativeDllImport.NativeMethods.ShgfiIcon; // | Shell32.SHGFI_USEFILEATTRIBUTES;

            if (true == linkOverlay)
            {
                flags += NativeDllImport.NativeMethods.ShgfiLINKOVERLAY;
            }

            if (FolderType.Open == folderType)
            {
                flags += NativeDllImport.NativeMethods.ShgfiOPENICON;
            }

            if (IconSize.Small == size)
            {
                flags += NativeDllImport.NativeMethods.ShgfiSMALLICON;
            }
            else
            {
                flags += NativeDllImport.NativeMethods.ShgfiLARGEICON;
            }

            // Get the folder icon
            NativeDllImport.NativeMethods.SHFILEINFO shfi = new NativeDllImport.NativeMethods.SHFILEINFO();
            IntPtr Success = NativeDllImport.NativeMethods.Shell32SHGetFileInfo(directoryPath,
                NativeDllImport.NativeMethods.FileAttributeDirectory,
                ref shfi,
                (uint)Marshal.SizeOf(shfi),
                flags);
            if (Success != IntPtr.Zero) // got valid handle?
            {
                try
                {
                    Icon.FromHandle(shfi.hIcon); // Load the icon from an HICON handle

                    // Now clone the icon, so that it can be successfully stored in an ImageList
                    icon = (Icon)Icon.FromHandle(shfi.hIcon).Clone();
                }
                catch (Exception ex)
                {
                    Log.Error($"directoryPath:'{directoryPath}'", ex);
                }

                // Cleanup
                NativeDllImport.NativeMethods.User32DestroyIcon(shfi.hIcon);
            }

            return icon;
        }

        public static Icon AddIconOverlay(Icon originalIcon, Icon overlay)
        {
            Icon icon = null;
            if (originalIcon != null)
            {
                using (Bitmap target = new Bitmap(
                    originalIcon.Width, originalIcon.Height,
                    PixelFormat.Format32bppArgb))
                {
                    Graphics graphics = Graphics.FromImage(target);
                    graphics.DrawIcon(originalIcon, 0, 0);
                    graphics.DrawIcon(overlay, 0, 0);
                    target.MakeTransparent(target.GetPixel(1, 1));
                    icon = Icon.FromHandle(target.GetHicon());
                }
            }

            return icon;
        }
    }
}