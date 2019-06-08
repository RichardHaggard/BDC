// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewMenuDisplayingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class RadPageViewMenuDisplayingEventArgs : CancelEventArgs
  {
    private Rectangle alignRect;
    private List<RadMenuItemBase> items;
    private HorizontalPopupAlignment hAlign;
    private VerticalPopupAlignment vAlign;

    public RadPageViewMenuDisplayingEventArgs(List<RadMenuItemBase> items, Rectangle alignRect)
      : this(items, alignRect, HorizontalPopupAlignment.LeftToLeft, VerticalPopupAlignment.TopToBottom)
    {
    }

    public RadPageViewMenuDisplayingEventArgs(
      List<RadMenuItemBase> items,
      Rectangle alignRect,
      HorizontalPopupAlignment hAlign,
      VerticalPopupAlignment vAlign)
    {
      this.items = items;
      this.hAlign = hAlign;
      this.vAlign = vAlign;
      this.alignRect = alignRect;
    }

    public Rectangle AlignRect
    {
      get
      {
        return this.alignRect;
      }
      set
      {
        this.alignRect = value;
      }
    }

    public List<RadMenuItemBase> Items
    {
      get
      {
        return this.items;
      }
    }

    public HorizontalPopupAlignment HAlign
    {
      get
      {
        return this.hAlign;
      }
      set
      {
        this.hAlign = value;
      }
    }

    public VerticalPopupAlignment VAlign
    {
      get
      {
        return this.vAlign;
      }
      set
      {
        this.vAlign = value;
      }
    }
  }
}
