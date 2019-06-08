// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadElementPropertyMetadata
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public class RadElementPropertyMetadata : RadPropertyMetadata
  {
    private ElementPropertyOptions _options;

    protected override RadPropertyMetadata CreateInstance()
    {
      return (RadPropertyMetadata) new RadElementPropertyMetadata();
    }

    public RadElementPropertyMetadata()
    {
    }

    public RadElementPropertyMetadata(object defaultValue)
      : base(defaultValue)
    {
    }

    public RadElementPropertyMetadata(object defaultValue, ElementPropertyOptions options)
      : base(defaultValue)
    {
      this._options = options;
    }

    public RadElementPropertyMetadata(
      object defaultValue,
      ElementPropertyOptions options,
      PropertyChangedCallback propertyChangedCallback)
      : base(defaultValue, propertyChangedCallback)
    {
      this._options = options;
    }

    public ElementPropertyOptions PropertyOptions
    {
      get
      {
        return this._options;
      }
    }

    public bool Cancelable
    {
      get
      {
        return (this._options & ElementPropertyOptions.Cancelable) == ElementPropertyOptions.Cancelable;
      }
      set
      {
        if (value)
          this._options |= ElementPropertyOptions.Cancelable;
        else
          this._options &= ~ElementPropertyOptions.Cancelable;
      }
    }

    public bool AffectsDisplay
    {
      get
      {
        return (this._options & ElementPropertyOptions.AffectsDisplay) == ElementPropertyOptions.AffectsDisplay;
      }
      set
      {
        if (value)
          this._options |= ElementPropertyOptions.AffectsDisplay;
        else
          this._options &= ~ElementPropertyOptions.AffectsDisplay;
      }
    }

    public bool AffectsLayout
    {
      get
      {
        return (this._options & ElementPropertyOptions.AffectsLayout) == ElementPropertyOptions.AffectsLayout;
      }
      set
      {
        if (value)
          this._options |= ElementPropertyOptions.AffectsLayout;
        else
          this._options &= ~ElementPropertyOptions.AffectsLayout;
      }
    }

    public bool AffectsMeasure
    {
      get
      {
        return (this._options & ElementPropertyOptions.AffectsMeasure) == ElementPropertyOptions.AffectsMeasure;
      }
      set
      {
        if (value)
          this._options |= ElementPropertyOptions.AffectsMeasure;
        else
          this._options &= ~ElementPropertyOptions.AffectsMeasure;
      }
    }

    public bool AffectsArrange
    {
      get
      {
        return (this._options & ElementPropertyOptions.AffectsArrange) == ElementPropertyOptions.AffectsArrange;
      }
      set
      {
        if (value)
          this._options |= ElementPropertyOptions.AffectsArrange;
        else
          this._options &= ~ElementPropertyOptions.AffectsArrange;
      }
    }

    public bool AffectsParentMeasure
    {
      get
      {
        return (this._options & ElementPropertyOptions.AffectsParentMeasure) == ElementPropertyOptions.AffectsParentMeasure;
      }
      set
      {
        if (value)
          this._options |= ElementPropertyOptions.AffectsParentMeasure;
        else
          this._options &= ~ElementPropertyOptions.AffectsParentMeasure;
      }
    }

    public bool AffectsParentArrange
    {
      get
      {
        return (this._options & ElementPropertyOptions.AffectsParentArrange) == ElementPropertyOptions.AffectsParentArrange;
      }
      set
      {
        if (value)
          this._options |= ElementPropertyOptions.AffectsParentArrange;
        else
          this._options &= ~ElementPropertyOptions.AffectsParentArrange;
      }
    }

    public bool InvalidatesLayout
    {
      get
      {
        return (this._options & ElementPropertyOptions.InvalidatesLayout) == ElementPropertyOptions.InvalidatesLayout;
      }
      set
      {
        if (value)
          this._options |= ElementPropertyOptions.InvalidatesLayout;
        else
          this._options &= ~ElementPropertyOptions.InvalidatesLayout;
      }
    }

    public bool CanInheritValue
    {
      get
      {
        return (this._options & ElementPropertyOptions.CanInheritValue) == ElementPropertyOptions.CanInheritValue;
      }
      set
      {
        if (value)
          this._options |= ElementPropertyOptions.CanInheritValue;
        else
          this._options &= ~ElementPropertyOptions.CanInheritValue;
      }
    }

    protected override void OnApply(RadProperty dp, Type targetType)
    {
      this.IsInherited = this.CanInheritValue;
      base.OnApply(dp, targetType);
    }

    protected override void Merge(RadPropertyMetadata baseMetadata, RadProperty dp)
    {
      base.Merge(baseMetadata, dp);
    }
  }
}
