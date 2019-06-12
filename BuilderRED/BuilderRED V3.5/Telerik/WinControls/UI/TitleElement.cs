// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TitleElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class TitleElement : CalendarVisualElement
  {
    protected internal TitleElement()
      : this(string.Empty)
    {
    }

    protected internal TitleElement(string text)
      : this((CalendarVisualElement) null, text)
    {
    }

    public TitleElement(CalendarVisualElement owner, string text)
      : base(owner, (RadCalendar) null, (CalendarView) null)
    {
      this.Text = text;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = "CalendarNavigationElement";
    }

    public override CalendarView View
    {
      get
      {
        return base.View;
      }
      set
      {
        base.View = value;
        if (value == null)
          return;
        this.Text = value.GetTitleContent();
        this.Invalidate();
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      return new SizeF(sizeF.Width, sizeF.Height + (float) this.Padding.Vertical);
    }
  }
}
