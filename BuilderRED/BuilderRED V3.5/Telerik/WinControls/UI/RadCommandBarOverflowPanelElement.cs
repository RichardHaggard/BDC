// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCommandBarOverflowPanelElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class RadCommandBarOverflowPanelElement : LightVisualElement
  {
    private LayoutPanel layout;

    [Browsable(false)]
    [Description("Represent Layout that holds elements over the menu")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LayoutPanel Layout
    {
      get
      {
        return this.layout;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.layout = this.CreateLayout();
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.Children.Add((RadElement) this.layout);
    }

    public virtual int GetChildMaxWidth()
    {
      int val1 = 0;
      foreach (RadElement child in this.Layout.Children)
      {
        RadCommandBarBaseItem commandBarBaseItem = child as RadCommandBarBaseItem;
        if (commandBarBaseItem != null && commandBarBaseItem.VisibleInStrip)
          val1 = Math.Max(val1, (int) Math.Floor((double) commandBarBaseItem.DesiredSize.Width));
      }
      return val1;
    }

    public virtual int GetChildrenTotalWidth()
    {
      int num = 0;
      foreach (RadElement child in this.Layout.Children)
      {
        RadCommandBarBaseItem commandBarBaseItem = child as RadCommandBarBaseItem;
        if (commandBarBaseItem != null && commandBarBaseItem.VisibleInStrip)
          num += (int) Math.Floor((double) commandBarBaseItem.DesiredSize.Width);
      }
      return num;
    }

    protected virtual LayoutPanel CreateLayout()
    {
      return (LayoutPanel) new RadCommandBarOverflowPanel();
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (CommandBarStripElement);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      return base.MeasureOverride(availableSize);
    }

    protected override void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      if (this.ElementTree == null || this.ElementTree.Control == null || !(this.ElementTree.Control.FindForm() is CommandBarFloatingForm))
        return;
      CommandBarStripElement stripElement = ((CommandBarFloatingForm) this.ElementTree.Control.FindForm()).StripElement;
      if (stripElement == null)
        return;
      if (args.RoutedEvent == RadCommandBarBaseItem.ClickEvent)
        stripElement.OnItemClicked((object) sender, args.OriginalEventArgs);
      if (args.RoutedEvent == RadCommandBarBaseItem.VisibleInStripChangedEvent)
        stripElement.OnItemVisibleInStripChanged((object) sender, args.OriginalEventArgs);
      if (args.RoutedEvent == RadCommandBarBaseItem.VisibleInStripChangingEvent)
        stripElement.OnItemVisibleInStripChanging((object) sender, args.OriginalEventArgs as CancelEventArgs);
      base.OnBubbleEvent(sender, args);
    }
  }
}
