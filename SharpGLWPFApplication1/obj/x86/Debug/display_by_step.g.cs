﻿#pragma checksum "..\..\..\display_by_step.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6F40EF6103F9A6DE5B3CB7E390EE8C20"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using SharpGL.WPF;
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


namespace SharpGLWPFApplication1 {
    
    
    /// <summary>
    /// display_by_step
    /// </summary>
    public partial class display_by_step : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\display_by_step.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock myTextBlock;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\display_by_step.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SharpGL.WPF.OpenGLControl myGLCon;
        
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
            System.Uri resourceLocater = new System.Uri("/SharpGLWPFApplication1;component/display_by_step.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\display_by_step.xaml"
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
            this.myTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.myGLCon = ((SharpGL.WPF.OpenGLControl)(target));
            
            #line 18 "..\..\..\display_by_step.xaml"
            this.myGLCon.OpenGLDraw += new SharpGL.SceneGraph.OpenGLEventHandler(this.myGLCon_OpenGLDraw);
            
            #line default
            #line hidden
            
            #line 21 "..\..\..\display_by_step.xaml"
            this.myGLCon.OpenGLInitialized += new SharpGL.SceneGraph.OpenGLEventHandler(this.myGLCon_OpenGLInitialized);
            
            #line default
            #line hidden
            
            #line 21 "..\..\..\display_by_step.xaml"
            this.myGLCon.Resized += new SharpGL.SceneGraph.OpenGLEventHandler(this.myGLControl_Resized);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
