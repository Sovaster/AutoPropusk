﻿#pragma checksum "..\..\Cars.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A57C455D83BE354F24001F501A34AA244F26150513912B22DF9EF26E36FBF79E"
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
    /// Cars
    /// </summary>
    public partial class Cars : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 52 "..\..\Cars.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BackBTN;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\Cars.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbNomer;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\Cars.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbGod;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\Cars.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CbMark;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\Cars.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CbClient;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\Cars.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button InsertMarkBTN;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\Cars.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteMarkBTN;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\Cars.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EditMarkBTN;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\Cars.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid GridCar;
        
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
            System.Uri resourceLocater = new System.Uri("/AutoPropusk;component/cars.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Cars.xaml"
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
            
            #line 16 "..\..\Cars.xaml"
            ((AutoPropusk.Cars)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BackBTN = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\Cars.xaml"
            this.BackBTN.Click += new System.Windows.RoutedEventHandler(this.BackBTN_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TbNomer = ((System.Windows.Controls.TextBox)(target));
            
            #line 63 "..\..\Cars.xaml"
            this.TbNomer.AddHandler(System.Windows.Input.CommandManager.PreviewCanExecuteEvent, new System.Windows.Input.CanExecuteRoutedEventHandler(this.HandleCanExecute));
            
            #line default
            #line hidden
            
            #line 64 "..\..\Cars.xaml"
            this.TbNomer.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TbNomer_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TbGod = ((System.Windows.Controls.TextBox)(target));
            
            #line 73 "..\..\Cars.xaml"
            this.TbGod.AddHandler(System.Windows.Input.CommandManager.PreviewCanExecuteEvent, new System.Windows.Input.CanExecuteRoutedEventHandler(this.HandleCanExecute));
            
            #line default
            #line hidden
            
            #line 74 "..\..\Cars.xaml"
            this.TbGod.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TbGod_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CbMark = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.CbClient = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.InsertMarkBTN = ((System.Windows.Controls.Button)(target));
            
            #line 96 "..\..\Cars.xaml"
            this.InsertMarkBTN.Click += new System.Windows.RoutedEventHandler(this.InsertMarkBTN_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.DeleteMarkBTN = ((System.Windows.Controls.Button)(target));
            
            #line 98 "..\..\Cars.xaml"
            this.DeleteMarkBTN.Click += new System.Windows.RoutedEventHandler(this.DeleteMarkBTN_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.EditMarkBTN = ((System.Windows.Controls.Button)(target));
            
            #line 100 "..\..\Cars.xaml"
            this.EditMarkBTN.Click += new System.Windows.RoutedEventHandler(this.EditMarkBTN_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.GridCar = ((System.Windows.Controls.DataGrid)(target));
            
            #line 103 "..\..\Cars.xaml"
            this.GridCar.SelectedCellsChanged += new System.Windows.Controls.SelectedCellsChangedEventHandler(this.GridCar_SelectedCellsChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
