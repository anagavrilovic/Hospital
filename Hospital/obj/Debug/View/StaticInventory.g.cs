﻿#pragma checksum "..\..\..\View\StaticInventory.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A6FF9B93DB90A76F6DD31BD91D6EC1EC6A42D2885B85C2EAD40DDF0E8F2679A5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Hospital.View;
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


namespace Hospital.View {
    
    
    /// <summary>
    /// StaticInventory
    /// </summary>
    public partial class StaticInventory : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 42 "..\..\..\View\StaticInventory.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox search;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\View\StaticInventory.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGridInventory;
        
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
            System.Uri resourceLocater = new System.Uri("/Hospital;component/view/staticinventory.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\StaticInventory.xaml"
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
            
            #line 31 "..\..\..\View\StaticInventory.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.back);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 38 "..\..\..\View\StaticInventory.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.edit);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 39 "..\..\..\View\StaticInventory.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.delete);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 40 "..\..\..\View\StaticInventory.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.transfer);
            
            #line default
            #line hidden
            return;
            case 5:
            this.search = ((System.Windows.Controls.TextBox)(target));
            
            #line 42 "..\..\..\View\StaticInventory.xaml"
            this.search.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.searchInventory);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 44 "..\..\..\View\StaticInventory.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.btnSearchMouseDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.dataGridInventory = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 8:
            
            #line 65 "..\..\..\View\StaticInventory.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.add);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
