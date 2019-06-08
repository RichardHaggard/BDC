// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadPropertyChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public class RadPropertyChangedEventArgs : EventArgs
  {
    public static readonly RadPropertyChangedEventArgs Empty = new RadPropertyChangedEventArgs();
    private RadProperty _property;
    private RadPropertyMetadata _metadata;
    private object _oldValue;
    private object _newValue;
    private bool _isASubPropertyChange;
    private bool _isOldValueDeferred;
    private bool _isNewValueDeferred;
    private ValueSource _oldValueSource;
    private ValueSource _newValueSource;

    private RadPropertyChangedEventArgs()
    {
    }

    public RadPropertyChangedEventArgs(RadProperty property, object oldValue, object newValue)
    {
      this._property = property;
      this._oldValue = oldValue;
      this._newValue = newValue;
    }

    public RadPropertyChangedEventArgs(
      RadProperty property,
      RadPropertyMetadata metadata,
      object oldValue,
      object newValue)
      : this(property, oldValue, newValue)
    {
      this._metadata = metadata;
    }

    internal RadPropertyChangedEventArgs(
      RadProperty property,
      RadPropertyMetadata metadata,
      object value)
      : this(property, metadata, value, value)
    {
      this._isASubPropertyChange = true;
    }

    internal RadPropertyChangedEventArgs(
      RadProperty property,
      RadPropertyMetadata metadata,
      object oldValue,
      object newValue,
      bool isOldValueDeferred,
      bool isNewValueDeferred,
      ValueSource oldValueSource,
      ValueSource newValueSource)
      : this(property, metadata, oldValue, newValue)
    {
      this._isOldValueDeferred = isOldValueDeferred;
      this._isNewValueDeferred = isNewValueDeferred;
      this._oldValueSource = oldValueSource;
      this._newValueSource = newValueSource;
    }

    public RadProperty Property
    {
      get
      {
        return this._property;
      }
    }

    public object OldValue
    {
      get
      {
        if (this._isOldValueDeferred)
        {
          this._oldValue = ((DeferredReference) this._oldValue).GetValue();
          this._isOldValueDeferred = false;
        }
        return this._oldValue;
      }
    }

    public object NewValue
    {
      get
      {
        if (this._isNewValueDeferred)
        {
          this._newValue = ((DeferredReference) this._newValue).GetValue();
          this._isNewValueDeferred = false;
        }
        return this._newValue;
      }
    }

    internal bool IsASubPropertyChange
    {
      get
      {
        return this._isASubPropertyChange;
      }
    }

    public RadPropertyMetadata Metadata
    {
      get
      {
        return this._metadata;
      }
    }

    public ValueSource OldValueSource
    {
      get
      {
        return this._oldValueSource;
      }
    }

    public ValueSource NewValueSource
    {
      get
      {
        return this._newValueSource;
      }
    }

    public override bool Equals(object obj)
    {
      return this.Equals((RadPropertyChangedEventArgs) obj);
    }

    public bool Equals(RadPropertyChangedEventArgs args)
    {
      if (this._property == args._property && this._metadata == args._metadata && (this._oldValue == args._oldValue && this._newValue == args._newValue) && (this._isASubPropertyChange == args._isASubPropertyChange && this._isOldValueDeferred == args._isOldValueDeferred && (this._isNewValueDeferred == args._isNewValueDeferred && this._oldValueSource == args._oldValueSource)))
        return this._newValueSource == args._newValueSource;
      return false;
    }

    public static bool operator ==(
      RadPropertyChangedEventArgs left,
      RadPropertyChangedEventArgs right)
    {
      return object.Equals((object) left, (object) right);
    }

    public static bool operator !=(
      RadPropertyChangedEventArgs left,
      RadPropertyChangedEventArgs right)
    {
      return !object.Equals((object) left, (object) right);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
