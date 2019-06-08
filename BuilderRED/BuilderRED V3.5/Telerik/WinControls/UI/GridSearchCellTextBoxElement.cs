// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridSearchCellTextBoxElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class GridSearchCellTextBoxElement : RadTextBoxElement
  {
    private RadLabelElement searchInfoLabel;
    private DockLayoutPanel dockPanel;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchVertically = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.searchInfoLabel = this.CreateSearchInfoLabel();
    }

    protected virtual RadLabelElement CreateSearchInfoLabel()
    {
      return new RadLabelElement();
    }

    public GridSearchCellTextBoxElement()
    {
      RadTextBoxItem textBoxItem = this.TextBoxItem;
      this.Children.Remove((RadElement) textBoxItem);
      this.SetSearchButonLocation();
      this.dockPanel = new DockLayoutPanel();
      this.dockPanel.LastChildFill = true;
      this.dockPanel.Alignment = ContentAlignment.MiddleLeft;
      this.dockPanel.Children.Add((RadElement) this.searchInfoLabel);
      this.dockPanel.Children.Add((RadElement) textBoxItem);
      this.Children.Add((RadElement) this.dockPanel);
      this.RadPropertyChanged += new RadPropertyChangedEventHandler(this.GridSearchCellTextBoxElement_RadPropertyChanged);
    }

    private void GridSearchCellTextBoxElement_RadPropertyChanged(
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
        int num1 = (int) this.searchInfoLabel.SetValue(DockLayoutPanel.DockProperty, (object) Dock.Left);
      }
      else
      {
        int num2 = (int) this.searchInfoLabel.SetValue(DockLayoutPanel.DockProperty, (object) Dock.Right);
      }
    }

    public RadLabelElement SearchInfoLabel
    {
      get
      {
        return this.searchInfoLabel;
      }
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadTextBoxElement);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      this.dockPanel.Measure(new SizeF(availableSize.Width - this.ButtonsStack.DesiredSize.Width, availableSize.Height));
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      if (this.RightToLeft)
        clientRectangle.Location = PointF.Add(clientRectangle.Location, new SizeF(this.ButtonsStack.DesiredSize.Width, 0.0f));
      clientRectangle.Size = SizeF.Subtract(clientRectangle.Size, new SizeF(this.ButtonsStack.DesiredSize.Width, 0.0f));
      this.dockPanel.Arrange(clientRectangle);
      return sizeF;
    }
  }
}
