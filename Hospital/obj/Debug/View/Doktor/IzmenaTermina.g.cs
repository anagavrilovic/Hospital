﻿#pragma checksum "..\..\..\..\View\Doktor\IzmenaTermina.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A812A5EE07EFBBB30667F7B360E0F410CAD9A0FA2AB45AF0038992C30D8A9EC4"
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
    /// IzmenaTermina
    /// </summary>
    public partial class IzmenaTermina : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\..\View\Doktor\IzmenaTermina.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox pacijentBox;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\View\Doktor\IzmenaTermina.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox doctorBox;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\View\Doktor\IzmenaTermina.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox tipTermina;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\View\Doktor\IzmenaTermina.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBoxItem pregled;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\View\Doktor\IzmenaTermina.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBoxItem operacija;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\View\Doktor\IzmenaTermina.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker Datum;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\View\Doktor\IzmenaTermina.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox trajanjeTermina;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\View\Doktor\IzmenaTermina.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox sala;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\View\Doktor\IzmenaTermina.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button save;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\View\Doktor\IzmenaTermina.xaml"
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
            System.Uri resourceLocater = new System.Uri("/Hospital;component/view/doktor/izmenatermina.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Doktor\IzmenaTermina.xaml"
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
            this.pacijentBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.doctorBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 31 "..\..\..\..\View\Doktor\IzmenaTermina.xaml"
            this.doctorBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.doktorBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tipTermina = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.pregled = ((System.Windows.Controls.ComboBoxItem)(target));
            return;
            case 5:
            this.operacija = ((System.Windows.Controls.ComboBoxItem)(target));
            return;
            case 6:
            this.Datum = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 7:
            this.trajanjeTermina = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.sala = ((System.Windows.Controls.ComboBox)(target));
            
            #line 56 "..\..\..\..\View\Doktor\IzmenaTermina.xaml"
            this.sala.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.sala_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.save = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\..\View\Doktor\IzmenaTermina.xaml"
            this.save.Click += new System.Windows.RoutedEventHandler(this.save_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.cancel = ((System.Windows.Controls.Button)(target));
            
            #line 60 "..\..\..\..\View\Doktor\IzmenaTermina.xaml"
            this.cancel.Click += new System.Windows.RoutedEventHandler(this.cancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
