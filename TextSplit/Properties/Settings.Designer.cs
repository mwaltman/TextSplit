﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TextSplit.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.ArrayList FileNames {
            get {
                return ((global::System.Collections.ArrayList)(this["FileNames"]));
            }
            set {
                this["FileNames"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ReadOnly {
            get {
                return ((bool)(this["ReadOnly"]));
            }
            set {
                this["ReadOnly"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DisableHK {
            get {
                return ((bool)(this["DisableHK"]));
            }
            set {
                this["DisableHK"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.ArrayList UserThemes {
            get {
                return ((global::System.Collections.ArrayList)(this["UserThemes"]));
            }
            set {
                this["UserThemes"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool Continuous {
            get {
                return ((bool)(this["Continuous"]));
            }
            set {
                this["Continuous"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool NavigateAll {
            get {
                return ((bool)(this["NavigateAll"]));
            }
            set {
                this["NavigateAll"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Collections.ArrayList Hotkeys {
            get {
                return ((global::System.Collections.ArrayList)(this["Hotkeys"]));
            }
            set {
                this["Hotkeys"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\r\n----------\r\n")]
        public string DefaultDelimiter {
            get {
                return ((string)(this["DefaultDelimiter"]));
            }
            set {
                this["DefaultDelimiter"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Slide $C$ / $T$\r\n")]
        public string DefaultInfoText {
            get {
                return ((string)(this["DefaultInfoText"]));
            }
            set {
                this["DefaultInfoText"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool NavigationWindowAlwaysOnTop {
            get {
                return ((bool)(this["NavigationWindowAlwaysOnTop"]));
            }
            set {
                this["NavigationWindowAlwaysOnTop"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int DisplayVerticalScrollBars {
            get {
                return ((int)(this["DisplayVerticalScrollBars"]));
            }
            set {
                this["DisplayVerticalScrollBars"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.DateTime TimeSinceLastCheck {
            get {
                return ((global::System.DateTime)(this["TimeSinceLastCheck"]));
            }
            set {
                this["TimeSinceLastCheck"] = value;
            }
        }
    }
}
