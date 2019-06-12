// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TimeTableStackLayoutElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class TimeTableStackLayoutElement : StackLayoutElement
  {
    public static RadProperty ClockBeforeTablesProperty = RadProperty.Register(nameof (ClockBeforeTables), typeof (bool), typeof (TimeTableStackLayoutElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay | ElementPropertyOptions.AffectsTheme));

    static TimeTableStackLayoutElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new TimeTableStackLayoutElementVerticalStateManager(TimeTableStackLayoutElement.ClockBeforeTablesProperty), typeof (TimeTableStackLayoutElement));
    }

    public bool ClockBeforeTables
    {
      get
      {
        return (bool) this.GetValue(TimeTableStackLayoutElement.ClockBeforeTablesProperty);
      }
      set
      {
        int num = (int) this.SetValue(TimeTableStackLayoutElement.ClockBeforeTablesProperty, (object) value);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      Padding clientOffset = this.GetClientOffset(true);
      SizeF sizeF = new SizeF((float) clientOffset.Horizontal, (float) clientOffset.Vertical);
      availableSize.Width -= (float) clientOffset.Horizontal;
      availableSize.Height -= (float) clientOffset.Vertical;
      lock (this.Children)
      {
        this.Layout.Measure(availableSize);
        int elementSpacing = this.ElementSpacing;
        SizeF streachebleSpace = this.GetStreachebleSpace(availableSize);
        foreach (RadElement child in this.Children)
        {
          if (this.Orientation == Orientation.Vertical)
          {
            if (child.StretchVertically)
              child.Measure(streachebleSpace);
            sizeF.Height += child.DesiredSize.Height;
            sizeF.Width = Math.Max(sizeF.Width, child.DesiredSize.Width);
            if (this.FitInAvailableSize)
              availableSize.Height -= child.DesiredSize.Height + (float) elementSpacing;
          }
          else
          {
            if (child.StretchHorizontally)
              child.Measure(streachebleSpace);
            sizeF.Width += child.DesiredSize.Width;
            sizeF.Height = Math.Max(sizeF.Height, child.DesiredSize.Height);
            if (this.FitInAvailableSize)
              availableSize.Width -= child.DesiredSize.Width + (float) elementSpacing;
          }
        }
        if (this.Children.Count > 1)
        {
          if (this.Orientation == Orientation.Vertical)
            sizeF.Height += (float) (elementSpacing * (this.Children.Count - 1));
          else
            sizeF.Width += (float) (elementSpacing * (this.Children.Count - 1));
        }
        sizeF.Width += (float) clientOffset.Horizontal;
        sizeF.Height += (float) clientOffset.Vertical;
      }
      return sizeF;
    }

    private SizeF GetStreachebleSpace(SizeF availableSize)
    {
      int num = 0;
      SizeF sizeF = SizeF.Empty;
      SizeF streachebleSpace = this.GetNonStreachebleSpace(availableSize);
      if (this.Orientation == Orientation.Horizontal)
      {
        foreach (RadElement child in this.Children)
        {
          if (child.StretchHorizontally && child.Visibility != ElementVisibility.Collapsed)
            ++num;
        }
        if (num == 0)
          return availableSize;
        availableSize.Width -= streachebleSpace.Width;
        sizeF = new SizeF(availableSize.Width / (float) num, availableSize.Height);
      }
      else
      {
        foreach (RadElement child in this.Children)
        {
          if (child.StretchVertically && child.Visibility != ElementVisibility.Collapsed)
            ++num;
        }
        if (num == 0)
          return availableSize;
        availableSize.Height -= streachebleSpace.Height;
        sizeF = new SizeF(availableSize.Width, availableSize.Height / (float) num);
      }
      return sizeF;
    }

    private SizeF GetNonStreachebleSpace(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      foreach (RadElement child in this.Children)
      {
        if (this.Orientation == Orientation.Horizontal)
        {
          if (!child.StretchHorizontally && child.Visibility != ElementVisibility.Collapsed)
          {
            child.Measure(availableSize);
            empty.Width += child.DesiredSize.Width;
            empty.Height = availableSize.Height;
          }
        }
        else if (!child.StretchVertically && child.Visibility != ElementVisibility.Collapsed)
        {
          child.Measure(availableSize);
          empty.Height += child.DesiredSize.Height;
          empty.Width = availableSize.Width;
        }
      }
      return empty;
    }
  }
}
