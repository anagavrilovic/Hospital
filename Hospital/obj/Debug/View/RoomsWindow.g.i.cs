﻿#pragma checksum "..\..\..\View\RoomsWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1C16FFB2B94BF3D3C19FFE68CBE41F6D0C4478D822E0CA2D3F0BCB21BA0EBFC5"
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
    /// RoomsWindow
    /// </summary>
    public partial class RoomsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\View\RoomsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGridRooms;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\View\RoomsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button add;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\View\RoomsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button edit;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\View\RoomsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button delete;
        
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
            System.Uri resourceLocater = new System.Uri("/Hospital;component/view/roomswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\RoomsWindow.xaml"
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
            this.dataGridRooms = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.add = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\View\RoomsWindow.xaml"
            this.add.Click += new System.Windows.RoutedEventHandler(this.addRoom);
            
            #line default
            #line hidden
            return;
            case 3:
            this.edit = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\View\RoomsWindow.xaml"
            this.edit.Click += new System.Windows.RoutedEventHandler(this.editRoom);
            
            #line default
            #line hidden
            return;
            case 4:
            this.delete = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\View\RoomsWindow.xaml"
            this.delete.Click += new System.Windows.RoutedEventHandler(this.deleteRoom);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

