﻿#pragma checksum "..\..\..\..\View\Secretary\Kalendar.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "02EA937F25DE64B54FE345ED5855E925E40E33A79F23B619DE3A0C92F035CE67"
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
    /// Kalendar
    /// </summary>
    public partial class Kalendar : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 47 "..\..\..\..\View\Secretary\Kalendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox DoctorComboBox;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\View\Secretary\Kalendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Pacijent;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\View\Secretary\Kalendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Zakazi;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\View\Secretary\Kalendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Otkazi;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\..\View\Secretary\Kalendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker Datum;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\..\View\Secretary\Kalendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button TrenutnaNedelja;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\View\Secretary\Kalendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Previous;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\..\View\Secretary\Kalendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Next;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\..\View\Secretary\Kalendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid KalendarDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/Hospital;component/view/secretary/kalendar.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Secretary\Kalendar.xaml"
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
            this.DoctorComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 48 "..\..\..\..\View\Secretary\Kalendar.xaml"
            this.DoctorComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBoxDoctorEvent);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Pacijent = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\..\View\Secretary\Kalendar.xaml"
            this.Pacijent.Click += new System.Windows.RoutedEventHandler(this.OdaberiPacijentaClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Zakazi = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\..\View\Secretary\Kalendar.xaml"
            this.Zakazi.Click += new System.Windows.RoutedEventHandler(this.ZakaziClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Otkazi = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\..\View\Secretary\Kalendar.xaml"
            this.Otkazi.Click += new System.Windows.RoutedEventHandler(this.OtkaziClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Datum = ((System.Windows.Controls.DatePicker)(target));
            
            #line 64 "..\..\..\..\View\Secretary\Kalendar.xaml"
            this.Datum.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.DateChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TrenutnaNedelja = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\..\..\View\Secretary\Kalendar.xaml"
            this.TrenutnaNedelja.Click += new System.Windows.RoutedEventHandler(this.TrenutnaNedeljaClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Previous = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\..\..\View\Secretary\Kalendar.xaml"
            this.Previous.Click += new System.Windows.RoutedEventHandler(this.PreviousClick);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Next = ((System.Windows.Controls.Button)(target));
            
            #line 79 "..\..\..\..\View\Secretary\Kalendar.xaml"
            this.Next.Click += new System.Windows.RoutedEventHandler(this.NextClick);
            
            #line default
            #line hidden
            return;
            case 9:
            this.KalendarDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

