// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadApplicationMenuDropDownElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class RadApplicationMenuDropDownElement : RadElement
  {
    private int rightColumnWidth = 300;
    private RadApplicationMenuContentElement content;
    private RadApplicationMenuContentElement topContent;
    private RadApplicationMenuDropDownMenuElement topRightContent;
    private RadApplicationMenuContentElement bottomContent;
    private RadApplicationMenuDropDownMenuElement menuElement;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = "ApplicationMenuDropDownElement";
    }

    [DefaultValue(300)]
    public int RightColumnWidth
    {
      get
      {
        return this.rightColumnWidth;
      }
      set
      {
        if (this.rightColumnWidth == value)
          return;
        this.rightColumnWidth = value;
        this.topRightContent.MinSize = new Size(value, 0);
        this.topRightContent.MaxSize = new Size(value, 0);
        this.topRightContent.Layout.MinSize = new Size(this.rightColumnWidth, 0);
        this.topRightContent.Layout.MaxSize = new Size(this.rightColumnWidth, 0);
      }
    }

    public RadApplicationMenuContentElement ContentElement
    {
      get
      {
        return this.content;
      }
    }

    public RadApplicationMenuContentElement TopContentElement
    {
      get
      {
        return this.topContent;
      }
    }

    public RadDropDownMenuElement TopRightContentElement
    {
      get
      {
        return (RadDropDownMenuElement) this.topRightContent;
      }
    }

    public RadDropDownMenuElement MenuElement
    {
      get
      {
        return (RadDropDownMenuElement) this.menuElement;
      }
    }

    public RadApplicationMenuContentElement BottomContentElement
    {
      get
      {
        return this.bottomContent;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      this.content.Measure(availableSize);
      this.topContent.Measure(availableSize);
      this.bottomContent.Measure(availableSize);
      this.topRightContent.Measure(availableSize);
      this.menuElement.Measure(availableSize);
      return sizeF;
    }

    protected override void CreateChildElements()
    {
      this.content = new RadApplicationMenuContentElement();
      this.content.Layout.Orientation = Orientation.Vertical;
      this.content.Class = "AppMenuContentElement";
      this.content.Fill.Class = "AppMenuFill";
      this.content.Border.Class = "AppMenuBorder";
      this.Children.Add((RadElement) this.content);
      this.topContent = new RadApplicationMenuContentElement();
      this.topContent.Layout.Orientation = Orientation.Horizontal;
      this.topContent.Class = "AppMenuTopContentElement";
      this.topContent.Fill.Class = "AppMenuTopFill";
      this.topContent.Border.Class = "AppMenuTopBorder";
      this.content.Layout.Children.Add((RadElement) this.topContent);
      this.menuElement = new RadApplicationMenuDropDownMenuElement();
      this.topContent.Layout.Children.Add((RadElement) this.menuElement);
      this.menuElement.Layout.Class = "AppMenuLeftLayout";
      this.topRightContent = new RadApplicationMenuDropDownMenuElement();
      this.topRightContent.Class = "AppMenuTopRightContentElement";
      this.topRightContent.Fill.Class = "AppMenuRightColumnFill";
      this.topRightContent.Border.Class = "AppMenuRightColumnBorder";
      this.topRightContent.MinSize = new Size(this.rightColumnWidth, 0);
      this.topRightContent.Layout.MinSize = new Size(this.rightColumnWidth, 0);
      this.topRightContent.Layout.Class = "AppMenuRightLayout";
      this.topRightContent.Layout.LeftColumnMinWidth = 0;
      this.topRightContent.LeftColumnElement.Visibility = ElementVisibility.Hidden;
      this.topContent.Layout.Children.Add((RadElement) this.topRightContent);
      this.bottomContent = new RadApplicationMenuContentElement();
      this.bottomContent.Layout = (StackLayoutPanel) new RadApplicationMenuBottomStripLayout();
      this.bottomContent.Class = "AppMenuBottomContentElement";
      this.bottomContent.Fill.Class = "AppMenuBottomFill";
      this.bottomContent.Border.Class = "AppMenuBottomBorder";
      this.bottomContent.Layout.Orientation = Orientation.Horizontal;
      this.bottomContent.Layout.StretchHorizontally = false;
      this.bottomContent.Layout.Alignment = ContentAlignment.MiddleRight;
      this.bottomContent.Layout.Class = "AppMenuBottomLayout";
      this.content.Layout.Children.Add((RadElement) this.bottomContent);
      int num1 = (int) this.menuElement.SetValue(RadDropDownMenuElement.DropDownPositionProperty, (object) DropDownPosition.LeftContent);
      int num2 = (int) this.topRightContent.SetValue(RadDropDownMenuElement.DropDownPositionProperty, (object) DropDownPosition.RightContent);
    }
  }
}
