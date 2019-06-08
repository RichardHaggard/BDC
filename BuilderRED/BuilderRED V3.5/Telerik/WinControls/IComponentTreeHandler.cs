// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.IComponentTreeHandler
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls
{
  public interface IComponentTreeHandler : ILayoutHandler
  {
    object GetAmbientPropertyValue(RadProperty property);

    void OnAmbientPropertyChanged(RadProperty property);

    void InitializeRootElement(RootRadElement rootElement);

    void LoadElementTree(Size size);

    void LoadElementTree();

    RootRadElement CreateRootElement();

    void CreateChildItems(RadElement parent);

    event ThemeNameChangedEventHandler ThemeNameChanged;

    event ToolTipTextNeededEventHandler ToolTipTextNeeded;

    void CallOnThemeNameChanged(ThemeNameChangedEventArgs e);

    void InvalidateIfNotSuspended();

    bool GetShowFocusCues();

    void OnDisplayPropertyChanged(RadPropertyChangedEventArgs e);

    bool IsDesignMode { get; }

    void CallOnMouseCaptureChanged(EventArgs e);

    void CallOnToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e);

    ComponentThemableElementTree ElementTree { get; }

    ComponentInputBehavior Behavior { get; }

    string ThemeClassName { get; set; }

    string ThemeName { get; set; }

    string Name { get; set; }

    RootRadElement RootElement { get; }

    void InvalidateElement(RadElement element);

    void InvalidateElement(RadElement element, Rectangle bounds);

    bool OnFocusRequested(RadElement element);

    bool OnCaptureChangeRequested(RadElement element, bool capture);

    void SuspendUpdate();

    void ResumeUpdate();

    ImageList SmallImageList { get; set; }

    ImageList ImageList { get; set; }

    Size SmallImageScalingSize { get; set; }

    Size ImageScalingSize { get; set; }

    bool Initializing { get; }

    void RegisterHostedControl(RadHostItem hostElement);

    void UnregisterHostedControl(RadHostItem hostElement, bool removeControl);

    bool ControlDefinesThemeForElement(RadElement element);

    void CallOnScreenTipNeeded(object sender, ScreenTipNeededEventArgs e);

    void ControlThemeChangedCallback();
  }
}
