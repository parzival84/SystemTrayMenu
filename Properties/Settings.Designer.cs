﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SystemTrayMenu.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.6.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(CustomSettingsProvider))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string CurrentCultureInfoName {
            get {
                return ((string)(this["CurrentCultureInfoName"]));
            }
            set {
                this["CurrentCultureInfoName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(CustomSettingsProvider))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string PathDirectory {
            get {
                return ((string)(this["PathDirectory"]));
            }
            set {
                this["PathDirectory"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(CustomSettingsProvider))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool IsAutostartActivated {
            get {
                return ((bool)(this["IsAutostartActivated"]));
            }
            set {
                this["IsAutostartActivated"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(CustomSettingsProvider))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Ctrl+Alt+Apps")]
        public string HotKey {
            get {
                return ((string)(this["HotKey"]));
            }
            set {
                this["HotKey"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(CustomSettingsProvider))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool IsUpgraded {
            get {
                return ((bool)(this["IsUpgraded"]));
            }
            set {
                this["IsUpgraded"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(CustomSettingsProvider))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool OpenItemWithOneClick {
            get {
                return ((bool)(this["OpenItemWithOneClick"]));
            }
            set {
                this["OpenItemWithOneClick"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(CustomSettingsProvider))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool IsDarkModeAlwaysOn {
            get {
                return ((bool)(this["IsDarkModeAlwaysOn"]));
            }
            set {
                this["IsDarkModeAlwaysOn"] = value;
            }
        }
    }
}
