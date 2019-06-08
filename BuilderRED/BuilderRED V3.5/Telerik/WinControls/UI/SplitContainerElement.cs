// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SplitContainerElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using Telerik.WinControls.Design;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class SplitContainerElement : RadItem
  {
    public static RadProperty IsVerticalProperty = RadProperty.Register("IsVertical", typeof (bool), typeof (SplitContainerElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty SplitterWidthProperty = RadProperty.Register(nameof (SplitterWidth), typeof (int), typeof (SplitContainerElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 4, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty EnableCollapsingProperty = RadProperty.Register(nameof (EnableCollapsing), typeof (bool), typeof (SplitContainerElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty UseSplitterButtonsProperty = RadProperty.Register(nameof (UseSplitterButtons), typeof (bool), typeof (SplitContainerElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));

    static SplitContainerElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new SplitContainerElementStateManager(), typeof (SplitContainerElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldHandleMouseInput = false;
    }

    public override RadElement GetChildAt(int index)
    {
      RadSplitContainer control = this.ElementTree.Control as RadSplitContainer;
      if (control != null && this.Count != control.VisiblePanelCount)
        control.LayoutInternal();
      return base.GetChildAt(index);
    }

    internal SplitterElement this[int index]
    {
      get
      {
        return (SplitterElement) this.Children[index];
      }
    }

    internal int Count
    {
      get
      {
        return this.Children.Count;
      }
    }

    [RadPropertyDefaultValue("SplitterWidth", typeof (SplitContainerElement))]
    [Description("Gets or sets the width of each splitter within the container.")]
    public int SplitterWidth
    {
      get
      {
        return (int) this.GetValue(SplitContainerElement.SplitterWidthProperty);
      }
      set
      {
        value = Math.Max(0, value);
        int num = (int) this.SetValue(SplitContainerElement.SplitterWidthProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("EnableCollapsing", typeof (SplitContainerElement))]
    [Description("Determines whether the panels can be collapsed when clicking twice on splitter or click once on navigation button")]
    public bool EnableCollapsing
    {
      get
      {
        return (bool) this.GetValue(SplitContainerElement.EnableCollapsingProperty);
      }
      set
      {
        int num = (int) this.SetValue(SplitContainerElement.EnableCollapsingProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("UseSplitterButtons", typeof (SplitContainerElement))]
    [Description("Enable and Disable navigation buttons.")]
    public bool UseSplitterButtons
    {
      get
      {
        return (bool) this.GetValue(SplitContainerElement.UseSplitterButtonsProperty);
      }
      set
      {
        int num = (int) this.SetValue(SplitContainerElement.UseSplitterButtonsProperty, (object) value);
        RadSplitContainer control = this.ElementTree.Control as RadSplitContainer;
        if (control == null)
          return;
        foreach (SplitterElement splitter in control.Splitters)
          splitter.Layout.UseSplitterButtons = value;
        control.LayoutInternal();
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == SplitContainerElement.SplitterWidthProperty && this.ElementState == ElementState.Loaded)
        (this.ElementTree.Control as RadSplitContainer)?.ApplySplitterWidth((int) e.NewValue);
      if (e.Property != SplitContainerElement.IsVerticalProperty)
        return;
      for (int index = 0; index < this.Children.Count; ++index)
      {
        SplitterElement child = this.Children[index] as SplitterElement;
        if (child != null)
        {
          int num = (int) child.SetValue(SplitterElement.IsVerticalProperty, e.NewValue);
        }
      }
    }
  }
}
