// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.AlertWindowTextAndSystemButtonsElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class AlertWindowTextAndSystemButtonsElement : LightVisualElement
  {
    private TextPrimitive text;
    private RadButtonElement closeButton;
    private RadToggleButtonElement pinButton;
    private RadDropDownButtonElement optionsButton;
    private StackLayoutPanel buttonsLayoutPanel;
    private DockLayoutPanel mainLayoutPanel;

    public DockLayoutPanel MainLayoutPanel
    {
      get
      {
        return this.mainLayoutPanel;
      }
    }

    public StackLayoutPanel ButtonsLayoutPanel
    {
      get
      {
        return this.buttonsLayoutPanel;
      }
    }

    public TextPrimitive TextElement
    {
      get
      {
        return this.text;
      }
    }

    public RadButtonElement CloseButton
    {
      get
      {
        return this.closeButton;
      }
    }

    public RadToggleButtonElement PinButton
    {
      get
      {
        return this.pinButton;
      }
    }

    public RadDropDownButtonElement OptionsButton
    {
      get
      {
        return this.optionsButton;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.text = new TextPrimitive();
      this.text.Class = "AlertWindowTextCaptionText";
      this.closeButton = new RadButtonElement();
      int num1 = (int) this.closeButton.SetDefaultValueOverride(RadButtonItem.DisplayStyleProperty, (object) DisplayStyle.Image);
      this.closeButton.ThemeRole = "AlertCloseButton";
      this.pinButton = new RadToggleButtonElement();
      int num2 = (int) this.pinButton.SetDefaultValueOverride(RadButtonItem.DisplayStyleProperty, (object) DisplayStyle.Image);
      this.pinButton.ThemeRole = "AlertWindowPinButton";
      this.optionsButton = new RadDropDownButtonElement();
      int num3 = (int) this.optionsButton.ArrowButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
      this.optionsButton.ThemeRole = "AlertWindowOptionsButton";
      this.buttonsLayoutPanel = new StackLayoutPanel();
      this.buttonsLayoutPanel.Class = "AlertWindowButtonsLayoutPanel";
      this.mainLayoutPanel = new DockLayoutPanel();
      this.mainLayoutPanel.Class = "AlertWindowMainLayoutPanel";
      this.mainLayoutPanel.LastChildFill = true;
      this.MinSize = new Size(0, 15);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.buttonsLayoutPanel.Children.Add((RadElement) this.optionsButton);
      this.buttonsLayoutPanel.Children.Add((RadElement) this.pinButton);
      this.buttonsLayoutPanel.Children.Add((RadElement) this.closeButton);
      this.mainLayoutPanel.Children.Add((RadElement) this.buttonsLayoutPanel);
      this.mainLayoutPanel.Children.Add((RadElement) this.text);
      DockLayoutPanel.SetDock((RadElement) this.text, this.RightToLeft ? Dock.Right : Dock.Left);
      DockLayoutPanel.SetDock((RadElement) this.buttonsLayoutPanel, this.RightToLeft ? Dock.Left : Dock.Right);
      this.Children.Add((RadElement) this.mainLayoutPanel);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.RightToLeftProperty)
        return;
      DockLayoutPanel.SetDock((RadElement) this.text, this.RightToLeft ? Dock.Right : Dock.Left);
      DockLayoutPanel.SetDock((RadElement) this.buttonsLayoutPanel, this.RightToLeft ? Dock.Left : Dock.Right);
    }
  }
}
