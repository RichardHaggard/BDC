// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownMenuLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class RadDropDownMenuLayout : StackLayoutPanel
  {
    public static RadProperty LeftColumnMinWidthProperty = RadProperty.Register(nameof (LeftColumnMinWidth), typeof (int), typeof (RadDropDownMenuLayout), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 21, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty RightColumnPaddingProperty = RadProperty.Register(nameof (RightColumnPadding), typeof (int), typeof (RadDropDownMenuLayout), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 25, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    private float leftColumnWidth;
    private float rightColumnWidth;
    private float leftColumnMaxPadding;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Orientation = Orientation.Vertical;
      this.EqualChildrenWidth = true;
    }

    [Description("Gets or sets the left column minimal width.")]
    [Category("Appearance")]
    [DefaultValue(false)]
    public virtual int LeftColumnMinWidth
    {
      get
      {
        return (int) this.GetValue(RadDropDownMenuLayout.LeftColumnMinWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadDropDownMenuLayout.LeftColumnMinWidthProperty, (object) value);
      }
    }

    [Description("Gets or sets the right column minimal width.")]
    [DefaultValue(false)]
    [Category("Appearance")]
    public virtual int RightColumnPadding
    {
      get
      {
        return (int) this.GetValue(RadDropDownMenuLayout.RightColumnPaddingProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadDropDownMenuLayout.RightColumnPaddingProperty, (object) value);
      }
    }

    public virtual float LeftColumnWidth
    {
      get
      {
        return this.leftColumnWidth;
      }
    }

    public virtual float RightColumnWidth
    {
      get
      {
        return this.rightColumnWidth;
      }
    }

    public virtual float LeftColumnMaxPadding
    {
      get
      {
        return this.leftColumnMaxPadding;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.ElementTree == null)
        return base.MeasureOverride(availableSize);
      RadDropDownMenu control = this.ElementTree.Control as RadDropDownMenu;
      RadApplicationMenuDropDown applicationMenuDropDown = (RadApplicationMenuDropDown) null;
      if (control != null && control.OwnerElement != null && control.OwnerElement.IsInValidState(true))
        applicationMenuDropDown = control.OwnerElement.ElementTree.Control as RadApplicationMenuDropDown;
      if (applicationMenuDropDown != null)
        availableSize.Width = (float) (applicationMenuDropDown.RightColumnWidth - 5);
      foreach (RadElement child in this.Children)
      {
        RadMenuItem radMenuItem = child as RadMenuItem;
        if (radMenuItem != null)
        {
          radMenuItem.Measure(availableSize);
          this.leftColumnWidth = Math.Max(this.leftColumnWidth, radMenuItem.LeftColumnElement.DesiredSize.Width);
          this.rightColumnWidth = Math.Max(this.rightColumnWidth, radMenuItem.RightColumnElement.DesiredSize.Width);
          this.leftColumnMaxPadding = Math.Max(this.leftColumnMaxPadding, (float) (radMenuItem.Padding.Left + radMenuItem.BorderThickness.Left + radMenuItem.Margin.Left));
        }
      }
      this.leftColumnWidth = Math.Max(this.leftColumnWidth, (float) this.LeftColumnMinWidth);
      SizeF sizeF = base.MeasureOverride(availableSize);
      sizeF.Width += (float) this.RightColumnPadding;
      sizeF.Width += (float) this.Padding.Horizontal;
      sizeF.Height += (float) this.Padding.Vertical;
      if (applicationMenuDropDown != null)
        sizeF.Width = (float) (applicationMenuDropDown.RightColumnWidth - 5);
      return sizeF;
    }

    protected override void OnChildrenChanged(
      RadElement child,
      ItemsChangeOperation changeOperation)
    {
      base.OnChildrenChanged(child, changeOperation);
      RadDropDownMenuElement ancestor = this.FindAncestor<RadDropDownMenuElement>();
      if (ancestor == null)
        return;
      if (changeOperation == ItemsChangeOperation.Inserted || changeOperation == ItemsChangeOperation.Set)
      {
        DropDownPosition dropDownPosition = (DropDownPosition) ancestor.GetValue(RadDropDownMenuElement.DropDownPositionProperty);
        foreach (RadObject radObject in child.ChildrenHierarchy)
        {
          int num = (int) radObject.SetValue(RadDropDownMenuElement.DropDownPositionProperty, (object) dropDownPosition);
        }
      }
      ancestor.InvalidateMeasure();
    }
  }
}
