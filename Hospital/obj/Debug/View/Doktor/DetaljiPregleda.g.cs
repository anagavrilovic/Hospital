﻿#pragma checksum "..\..\..\..\View\Doktor\DetaljiPregleda.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "EAA91D166B988902B29FCF2E10C0AF73FFD6F81DE0A4462BE1DD1DE8BCF2DE38"
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
    /// DetaljiPregleda
    /// </summary>
    public partial class DetaljiPregleda : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\..\View\Doktor\DetaljiPregleda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame Main;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\View\Doktor\DetaljiPregleda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label DoktorLabel;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\View\Doktor\DetaljiPregleda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label tip;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\View\Doktor\DetaljiPregleda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button zapocniPregled;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\View\Doktor\DetaljiPregleda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Datum;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\View\Doktor\DetaljiPregleda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label trajanje;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\View\Doktor\DetaljiPregleda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label novac;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\View\Doktor\DetaljiPregleda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label salalab;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\View\Doktor\DetaljiPregleda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button karton;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\View\Doktor\DetaljiPregleda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cancel;
        
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
            System.Uri resourceLocater = new System.Uri("/Hospital;component/view/doktor/detaljipregleda.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Doktor\DetaljiPregleda.xaml"
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
            this.Main = ((System.Windows.Controls.Frame)(target));
            return;
            case 2:
            this.DoktorLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.tip = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.zapocniPregled = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\..\View\Doktor\DetaljiPregleda.xaml"
            this.zapocniPregled.Click += new System.Windows.RoutedEventHandler(this.zapocniTermin);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Datum = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.trajanje = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.novac = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.salalab = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.karton = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\..\..\View\Doktor\DetaljiPregleda.xaml"
            this.karton.Click += new System.Windows.RoutedEventHandler(this.pregledKartona);
            
            #line default
            #line hidden
            return;
            case 10:
            this.cancel = ((System.Windows.Controls.Button)(target));
            
            #line 76 "..\..\..\..\View\Doktor\DetaljiPregleda.xaml"
            this.cancel.Click += new System.Windows.RoutedEventHandler(this.cancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

