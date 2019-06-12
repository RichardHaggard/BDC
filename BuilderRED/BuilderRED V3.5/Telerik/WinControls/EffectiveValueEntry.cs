// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.EffectiveValueEntry
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  internal struct EffectiveValueEntry
  {
    private short _propertyIndex;
    private object _value;
    private EffectiveValueEntry.PrivateFlags _flags;
    internal bool IsTrackingDefaultValues;
    internal bool IsTrackingDesignerValues;

    internal void SetExpressionValue(object value, object baseValue)
    {
      this.ModifiedValue.ExpressionValue = value;
      this.IsExpression = true;
    }

    internal void SetAnimatedValue(object value, object baseValue)
    {
      this.ModifiedValue.AnimatedValue = value;
      this.IsAnimated = true;
    }

    internal void SetCoercedValue(object value, object baseValue)
    {
      this.ModifiedValue.CoercedValue = value;
      this.IsCoerced = true;
    }

    public int PropertyIndex
    {
      get
      {
        return (int) this._propertyIndex;
      }
      set
      {
        this._propertyIndex = (short) value;
      }
    }

    internal object Value
    {
      get
      {
        return this._value;
      }
      set
      {
        this._value = value;
      }
    }

    internal ValueSource ValueSource
    {
      get
      {
        return (ValueSource) (this._flags & EffectiveValueEntry.PrivateFlags.ValueSourceMask);
      }
      set
      {
        this._flags = this._flags & (EffectiveValueEntry.PrivateFlags.IsExpression | EffectiveValueEntry.PrivateFlags.IsAnimated | EffectiveValueEntry.PrivateFlags.IsCoerced | EffectiveValueEntry.PrivateFlags.IsDeferredReference) | (EffectiveValueEntry.PrivateFlags) value;
      }
    }

    internal bool IsDeferredReference
    {
      get
      {
        return this.ReadPrivateFlag(EffectiveValueEntry.PrivateFlags.IsDeferredReference);
      }
      set
      {
        this.WritePrivateFlag(EffectiveValueEntry.PrivateFlags.IsDeferredReference, value);
      }
    }

    internal bool IsExpression
    {
      get
      {
        return this.ReadPrivateFlag(EffectiveValueEntry.PrivateFlags.IsExpression);
      }
      set
      {
        this.WritePrivateFlag(EffectiveValueEntry.PrivateFlags.IsExpression, value);
      }
    }

    internal bool IsAnimated
    {
      get
      {
        return this.ReadPrivateFlag(EffectiveValueEntry.PrivateFlags.IsAnimated);
      }
      set
      {
        this.WritePrivateFlag(EffectiveValueEntry.PrivateFlags.IsAnimated, value);
      }
    }

    internal bool IsCoerced
    {
      get
      {
        return this.ReadPrivateFlag(EffectiveValueEntry.PrivateFlags.IsCoerced);
      }
      set
      {
        this.WritePrivateFlag(EffectiveValueEntry.PrivateFlags.IsCoerced, value);
      }
    }

    internal object LocalValue
    {
      get
      {
        if (this.ValueSource != ValueSource.Local)
          return RadProperty.UnsetValue;
        if (!this.HasModifiers)
          return this.Value;
        return ((ModifiedValue) this.Value).BaseValue;
      }
      set
      {
        if (!this.HasModifiers)
          this.Value = value;
        else
          ((ModifiedValue) this.Value).BaseValue = value;
      }
    }

    internal bool HasModifiers
    {
      get
      {
        return (this._flags & (EffectiveValueEntry.PrivateFlags.IsExpression | EffectiveValueEntry.PrivateFlags.IsAnimated | EffectiveValueEntry.PrivateFlags.IsCoerced)) != ~(EffectiveValueEntry.PrivateFlags.ValueSourceMask | EffectiveValueEntry.PrivateFlags.IsExpression | EffectiveValueEntry.PrivateFlags.IsAnimated | EffectiveValueEntry.PrivateFlags.IsCoerced | EffectiveValueEntry.PrivateFlags.IsDeferredReference);
      }
    }

    internal ModifiedValue ModifiedValue
    {
      get
      {
        if (this._value == null)
          return (ModifiedValue) (this._value = (object) new ModifiedValue());
        ModifiedValue modifiedValue = this._value as ModifiedValue;
        if (modifiedValue == null)
        {
          modifiedValue = new ModifiedValue();
          modifiedValue.BaseValue = this._value;
          this._value = (object) modifiedValue;
        }
        return modifiedValue;
      }
    }

    private void WritePrivateFlag(EffectiveValueEntry.PrivateFlags bit, bool value)
    {
      if (value)
        this._flags |= bit;
      else
        this._flags &= ~bit;
    }

    private bool ReadPrivateFlag(EffectiveValueEntry.PrivateFlags bit)
    {
      return (this._flags & bit) != ~(EffectiveValueEntry.PrivateFlags.ValueSourceMask | EffectiveValueEntry.PrivateFlags.IsExpression | EffectiveValueEntry.PrivateFlags.IsAnimated | EffectiveValueEntry.PrivateFlags.IsCoerced | EffectiveValueEntry.PrivateFlags.IsDeferredReference);
    }

    private enum PrivateFlags : byte
    {
      ValueSourceMask = 15, // 0x0F
      IsExpression = 16, // 0x10
      IsAnimated = 32, // 0x20
      IsCoerced = 64, // 0x40
      IsDeferredReference = 128, // 0x80
    }
  }
}
