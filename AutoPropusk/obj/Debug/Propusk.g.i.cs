﻿#pragma checksum "..\..\Propusk.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1DFCD2095668EDD7ABC8BC12938550C43C6D5E15E59075DE6530D9DF9778CF2C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using AutoPropusk;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace AutoPropusk {
    
    
    /// <summary>
    /// Propusk
    /// </summary>
    public partial class Propusk : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 52 "..\..\Propusk.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BackBTN;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\Propusk.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button WordBTN;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\Propusk.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CbClient;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\Propusk.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button InsertMarkBTN;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\Propusk.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteMarkBTN;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\Propusk.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid GridPropusk;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AutoPropusk;component/propusk.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Propusk.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 16 "..\..\Propusk.xaml"
            ((AutoPropusk.Propusk)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BackBTN = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\Propusk.xaml"
            this.BackBTN.Click += new System.Windows.RoutedEventHandler(this.BackBTN_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.WordBTN = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\Propusk.xaml"
            this.WordBTN.Click += new System.Windows.RoutedEventHandler(this.WordBTN_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.CbClient = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.InsertMarkBTN = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\Propusk.xaml"
            this.InsertMarkBTN.Click += new System.Windows.RoutedEventHandler(this.InsertMarkBTN_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.DeleteMarkBTN = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\Propusk.xaml"
            this.DeleteMarkBTN.Click += new System.Windows.RoutedEventHandler(this.DeleteMarkBTN_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.GridPropusk = ((System.Windows.Controls.DataGrid)(target));
            
            #line 72 "..\..\Propusk.xaml"
            this.GridPropusk.SelectedCellsChanged += new System.Windows.Controls.SelectedCellsChangedEventHandler(this.GridPropusk_SelectedCellsChanged);
            
            #line default
            #line hidden
            
            #line 73 "..\..\Propusk.xaml"
            this.GridPropusk.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.GridPropusk_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
