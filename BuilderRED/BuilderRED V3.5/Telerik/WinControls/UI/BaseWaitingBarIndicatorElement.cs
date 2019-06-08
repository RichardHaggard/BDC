// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseWaitingBarIndicatorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  public abstract class BaseWaitingBarIndicatorElement : LightVisualElement
  {
    public static RadProperty ElementCountProperty = RadProperty.Register(nameof (ElementCount), typeof (int), typeof (BaseWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 5, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ElementColorProperty = RadProperty.Register(nameof (ElementColor), typeof (Color), typeof (BaseWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(14, 58, 131), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ElementColor2Property = RadProperty.Register(nameof (ElementColor2), typeof (Color), typeof (BaseWaitingBarIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Transparent, ElementPropertyOptions.AffectsDisplay));

    [RadRange(0, 2147483647)]
    [RadPropertyDefaultValue("ElementCount", typeof (BaseWaitingBarIndicatorElement))]
    [Category("Appearance")]
    public virtual int ElementCount
    {
      get
      {
        return (int) this.GetValue(BaseWaitingBarIndicatorElement.ElementCountProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseWaitingBarIndicatorElement.ElementCountProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [RadPropertyDefaultValue("ElementColor", typeof (BaseWaitingBarIndicatorElement))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public virtual Color ElementColor
    {
      get
      {
        return (Color) this.GetValue(BaseWaitingBarIndicatorElement.ElementColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseWaitingBarIndicatorElement.ElementColorProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ElementColor2", typeof (BaseWaitingBarIndicatorElement))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public virtual Color ElementColor2
    {
      get
      {
        return (Color) this.GetValue(BaseWaitingBarIndicatorElement.ElementColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(BaseWaitingBarIndicatorElement.ElementColor2Property, (object) value);
      }
    }

    public abstract void Animate(int step);

    public abstract void ResetAnimation();

    public virtual void UpdateWaitingDirection(ProgressOrientation waitingDirection)
    {
      this.ResetAnimation();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RadWaitingBarElement ancestor = this.FindAncestor<RadWaitingBarElement>();
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (ancestor != null)
      {
        sizeF.Height = !this.StretchVertically || float.IsInfinity(availableSize.Height) ? (float) ancestor.WaitingIndicatorSize.Height : availableSize.Height;
        sizeF.Width = !this.StretchHorizontally || float.IsInfinity(availableSize.Width) ? (float) ancestor.WaitingIndicatorSize.Width : availableSize.Width;
      }
      return sizeF;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != BaseWaitingBarIndicatorElement.ElementCountProperty)
        return;
      if ((int) e.NewValue < 0)
        throw new ArgumentException("ElementCount cannot be negative.");
      this.ResetAnimation();
    }
  }
}
