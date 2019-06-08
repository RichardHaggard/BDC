// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ContextualTabGroup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [RadToolboxItem(false)]
  [Designer("Telerik.WinControls.UI.Design.ContextualTabGroupDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class ContextualTabGroup : RadItem
  {
    public static RadProperty BaseColorProperty = RadProperty.Register(nameof (BaseColor), typeof (Color), typeof (ContextualTabGroup), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Empty, ElementPropertyOptions.AffectsDisplay));
    private RadItemCollection tabItems;
    private FillPrimitive fillPrimitive;
    private BorderPrimitive borderPrimitive;
    private TextPrimitive captionText;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.tabItems = new RadItemCollection();
      this.tabItems.ItemTypes = new System.Type[1]
      {
        typeof (RibbonTab)
      };
      this.tabItems.ItemsChanged += new ItemChangedDelegate(this.commandTabs_ItemsChanged);
    }

    protected override void CreateChildElements()
    {
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.borderPrimitive.Class = "ContextualTabBorder";
      this.borderPrimitive.BoxStyle = BorderBoxStyle.SingleBorder;
      this.borderPrimitive.GradientStyle = GradientStyles.Linear;
      this.borderPrimitive.GradientAngle = 90f;
      this.borderPrimitive.ForeColor = Color.Transparent;
      this.borderPrimitive.ForeColor2 = Color.FromArgb(196, 194, 206);
      this.borderPrimitive.ForeColor3 = Color.FromArgb(196, 194, 206);
      this.borderPrimitive.ForeColor4 = Color.Transparent;
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.fillPrimitive.Class = "ContextualTabFill";
      this.fillPrimitive.GradientStyle = GradientStyles.Glass;
      this.fillPrimitive.BackColor = Color.FromArgb(10, this.BaseColor);
      this.fillPrimitive.BackColor2 = Color.FromArgb(90, this.BaseColor);
      this.fillPrimitive.BackColor3 = Color.FromArgb(130, this.BaseColor);
      this.fillPrimitive.BackColor4 = Color.FromArgb(180, this.BaseColor);
      this.fillPrimitive.GradientPercentage = 1f / 1000f;
      this.fillPrimitive.GradientPercentage2 = 0.92f;
      this.fillPrimitive.NumberOfColors = 4;
      this.captionText = new TextPrimitive();
      int num = (int) this.captionText.BindProperty(TextPrimitive.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      this.captionText.Alignment = ContentAlignment.MiddleLeft;
      this.captionText.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.captionText.Class = "ContextualTabCaption";
      this.captionText.Margin = new Padding(0, 5, 0, 0);
      this.captionText.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
      this.Children.Add((RadElement) this.fillPrimitive);
      this.Children.Add((RadElement) this.borderPrimitive);
      this.Children.Add((RadElement) this.captionText);
      this.ShouldPaint = true;
    }

    [Editor("Telerik.WinControls.UI.Design.ContextualTabGroupTabsEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    public RadItemCollection TabItems
    {
      get
      {
        return this.tabItems;
      }
      set
      {
        this.tabItems = value;
      }
    }

    [RadPropertyDefaultValue("BaseColor", typeof (ContextualTabGroup))]
    [Category("Appearance")]
    public Color BaseColor
    {
      get
      {
        return (Color) this.GetValue(ContextualTabGroup.BaseColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(ContextualTabGroup.BaseColorProperty, (object) value);
      }
    }

    private void commandTabs_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      if (operation == ItemsChangeOperation.Inserted && target != null)
      {
        target.RadPropertyChanged += new RadPropertyChangedEventHandler(this.tabStrip_RadPropertyChanged);
        if (!this.IsInValidState(true))
          return;
        RadRibbonBar control = this.ElementTree.Control as RadRibbonBar;
        if (control == null)
          return;
        control.RibbonBarElement.InvalidateMeasure(true);
        control.RibbonBarElement.UpdateLayout();
        control.RibbonBarElement.RibbonCaption.CaptionLayout.InvalidateMeasure();
        control.RibbonBarElement.RibbonCaption.CaptionLayout.InvalidateArrange();
        control.RibbonBarElement.RibbonCaption.CaptionLayout.UpdateLayout();
      }
      else
      {
        switch (operation)
        {
          case ItemsChangeOperation.Removed:
            target.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.tabStrip_RadPropertyChanged);
            if (!this.IsInValidState(true))
              break;
            RadRibbonBar control1 = this.ElementTree.Control as RadRibbonBar;
            if (control1 == null)
              break;
            control1.RibbonBarElement.InvalidateMeasure(true);
            control1.RibbonBarElement.UpdateLayout();
            control1.RibbonBarElement.RibbonCaption.CaptionLayout.InvalidateMeasure();
            control1.RibbonBarElement.RibbonCaption.CaptionLayout.InvalidateArrange();
            control1.RibbonBarElement.RibbonCaption.CaptionLayout.UpdateLayout();
            break;
          case ItemsChangeOperation.Clearing:
            using (RadItemCollection.RadItemEnumerator enumerator = this.tabItems.GetEnumerator())
            {
              while (enumerator.MoveNext())
                enumerator.Current.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.tabStrip_RadPropertyChanged);
              break;
            }
        }
      }
    }

    private void tabStrip_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (!(e.Metadata is RadElementPropertyMetadata))
        return;
      RadElementPropertyMetadata metadata = e.Metadata as RadElementPropertyMetadata;
      if (!metadata.AffectsLayout && !metadata.InvalidatesLayout)
        return;
      this.InvalidateMeasure();
      this.InvalidateArrange();
    }

    protected override void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      if (sender == this && args.RoutedEvent == RadElement.MouseClickedEvent && this.tabItems.Count > 0)
      {
        RadPageViewElement owner = (this.tabItems[0] as RadPageViewItem).Owner;
        if (owner == null)
          throw new NullReferenceException(string.Format("{0} has no parent TabStrip", (object) this.tabItems[0].ToString()));
        if (owner.SelectedItem != this.tabItems[0])
          owner.SetSelectedItem((RadPageViewItem) this.tabItems[0]);
      }
      base.OnBubbleEvent(sender, args);
    }

    protected override void OnPropertyChanging(RadPropertyChangingEventArgs args)
    {
      base.OnPropertyChanging(args);
      if (args.Property != RadElement.VisibilityProperty || this.tabItems.Count <= 0)
        return;
      RadPageViewElement owner = (this.tabItems[0] as RadPageViewItem).Owner;
      if (owner == null)
        throw new NullReferenceException(string.Format("{0} has no parent TabStrip", (object) this.tabItems[0].ToString()));
      for (int index = 0; index < this.tabItems.Count; ++index)
        (this.tabItems[index] as RadPageViewItem).Visibility = (ElementVisibility) args.NewValue;
      if (this.TabItems.Contains((RadItem) (owner.SelectedItem as RibbonTab)))
      {
        foreach (RibbonTab ribbonTab in (IEnumerable<RadPageViewItem>) owner.Items)
        {
          if (!this.TabItems.Contains((RadItem) ribbonTab))
          {
            owner.SelectedItem = (RadPageViewItem) ribbonTab;
            break;
          }
        }
      }
      owner.InvalidateMeasure();
      owner.InvalidateArrange();
      owner.UpdateLayout();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != ContextualTabGroup.BaseColorProperty)
        return;
      this.fillPrimitive.BackColor = Color.FromArgb(10, this.BaseColor);
      this.fillPrimitive.BackColor2 = Color.FromArgb(90, this.BaseColor);
      this.fillPrimitive.BackColor3 = Color.FromArgb(130, this.BaseColor);
      this.fillPrimitive.BackColor4 = Color.FromArgb(180, this.BaseColor);
    }

    protected override SizeF MeasureOverride(SizeF proposedSize)
    {
      SizeF sizeF = base.MeasureOverride(proposedSize);
      if ((double) proposedSize.Width != double.PositiveInfinity)
        sizeF.Width = proposedSize.Width;
      return sizeF;
    }
  }
}
