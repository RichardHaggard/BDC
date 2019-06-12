// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ToolbarTextBoxElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class ToolbarTextBoxElement : RadTextBoxElement
  {
    private ToolbarTextBoxButton searchButton;
    private DockLayoutPanel dockPanel;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.searchButton = new ToolbarTextBoxButton();
      this.searchButton.DisplayStyle = DisplayStyle.Image;
    }

    public ToolbarTextBoxElement()
    {
      RadTextBoxItem textBoxItem = this.TextBoxItem;
      this.Children.Remove((RadElement) textBoxItem);
      this.SetSearchButonLocation();
      this.dockPanel = new DockLayoutPanel();
      this.dockPanel.LastChildFill = true;
      this.dockPanel.Children.Add((RadElement) this.searchButton);
      this.dockPanel.Children.Add((RadElement) textBoxItem);
      this.Children.Add((RadElement) this.dockPanel);
      this.RadPropertyChanged += new RadPropertyChangedEventHandler(this.ToolbarTextBoxElement_RadPropertyChanged);
    }

    private void ToolbarTextBoxElement_RadPropertyChanged(
      object sender,
      RadPropertyChangedEventArgs e)
    {
      if (e.Property != RadElement.RightToLeftProperty)
        return;
      this.SetSearchButonLocation();
    }

    protected virtual void SetSearchButonLocation()
    {
      if (this.RightToLeft)
      {
        int num1 = (int) this.searchButton.SetValue(DockLayoutPanel.DockProperty, (object) Dock.Left);
      }
      else
      {
        int num2 = (int) this.searchButton.SetValue(DockLayoutPanel.DockProperty, (object) Dock.Right);
      }
    }

    public ToolbarTextBoxButton SearchButton
    {
      get
      {
        return this.searchButton;
      }
    }

    protected override void OnTextChanged(EventArgs e)
    {
      base.OnTextChanged(e);
      this.searchButton.IsSearching = !string.IsNullOrEmpty(this.Text);
    }
  }
}
