// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadPropertyMetadata
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public class RadPropertyMetadata
  {
    private int _flags;
    private object _defaultValue;
    private PropertyChangedCallback _propertyChangedCallback;
    private RadPropertyKey _readOnlyKey;
    private AttachedPropertyUsage _attachedPropertyUsage;
    private CoerceValueCallback _coerceValueCallback;

    public RadPropertyMetadata()
    {
    }

    public RadPropertyMetadata(object defaultValue)
    {
      this.DefaultValue = defaultValue;
    }

    public RadPropertyMetadata(PropertyChangedCallback propertyChangedCallback)
    {
      this.PropertyChangedCallback = propertyChangedCallback;
    }

    public RadPropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback)
      : this(defaultValue)
    {
      this.PropertyChangedCallback = propertyChangedCallback;
    }

    protected virtual RadPropertyMetadata CreateInstance()
    {
      return new RadPropertyMetadata();
    }

    public bool ReadOnly
    {
      get
      {
        return false;
      }
    }

    public bool DefaultValueWasSet()
    {
      return this.IsModified(1);
    }

    public CoerceValueCallback CoerceValueCallback
    {
      get
      {
        return this._coerceValueCallback;
      }
      set
      {
        if (this.Sealed)
          throw new InvalidOperationException(string.Format("TypeMetadataCannotChangeAfterUse"));
        this._coerceValueCallback = value;
        this.SetModified(8);
      }
    }

    public bool IsDefaultValueModified
    {
      get
      {
        return this.IsModified(1);
      }
    }

    public object DefaultValue
    {
      get
      {
        return this._defaultValue;
      }
      set
      {
        if (this.Sealed)
          throw new InvalidOperationException("TypeMetadataCannotChangeAfterUse");
        if (value == RadProperty.UnsetValue)
          throw new ArgumentException("DefaultValueMayNotBeUnset");
        this._defaultValue = value;
        this.SetModified(1);
      }
    }

    public bool IsInherited
    {
      get
      {
        return (32 & this._flags) != 0;
      }
      set
      {
        if (value)
          this._flags |= 32;
        else
          this._flags &= -33;
      }
    }

    public object GetDefaultValue(RadObject owner, RadProperty property)
    {
      if (this._defaultValue != DefaultValueOptions.CallCreateDefaultValue)
        return this._defaultValue;
      object obj = RadProperty.UnsetValue;
      if (obj == RadProperty.UnsetValue)
        obj = this.CreateDefaultValue(owner, property);
      return obj;
    }

    private void SetModified(int id)
    {
      this._flags |= id;
    }

    private bool IsModified(int id)
    {
      return (id & this._flags) != 0;
    }

    protected virtual object CreateDefaultValue(RadObject owner, RadProperty property)
    {
      throw new NotImplementedException("MissingCreateDefaultValue of property Metadata");
    }

    protected virtual void OnApply(RadProperty dp, Type targetType)
    {
    }

    public void Seal(RadProperty dp, Type targetType)
    {
      this.OnApply(dp, targetType);
      this.Sealed = true;
    }

    public bool Sealed
    {
      get
      {
        return this.ReadFlag(2);
      }
      set
      {
        this.WriteFlag(2, value);
      }
    }

    public bool ReadFlag(int id)
    {
      return (id & this._flags) != 0;
    }

    public void WriteFlag(int id, bool value)
    {
      if (value)
        this._flags |= id;
      else
        this._flags &= ~id;
    }

    protected bool IsSealed
    {
      get
      {
        return this.Sealed;
      }
    }

    public void SetAttachedPropertyUsage(AttachedPropertyUsage attachedPropertyUsage)
    {
      if (this.Sealed)
        throw new InvalidOperationException(string.Format("TypeMetadataCannotChangeAfterUse"));
      this._attachedPropertyUsage = attachedPropertyUsage;
      this.SetModified(16);
    }

    public PropertyChangedCallback PropertyChangedCallback
    {
      get
      {
        return this._propertyChangedCallback;
      }
      set
      {
        if (this.Sealed)
          throw new InvalidOperationException("Type metadata cannot change after it has been used");
        this._propertyChangedCallback = value;
        this.SetModified(4);
      }
    }

    public AttachedPropertyUsage AttachedPropertyUsage
    {
      get
      {
        return this._attachedPropertyUsage;
      }
    }

    public RadPropertyMetadata Copy(RadProperty dp)
    {
      RadPropertyMetadata instance = this.CreateInstance();
      instance.InvokeMerge(this, dp);
      return instance;
    }

    public void InvokeMerge(RadPropertyMetadata baseMetadata, RadProperty dp)
    {
      if (baseMetadata.ReadOnly)
        this._readOnlyKey = baseMetadata._readOnlyKey;
      this.Merge(baseMetadata, dp);
    }

    protected virtual void Merge(RadPropertyMetadata baseMetadata, RadProperty dp)
    {
      if (baseMetadata == null)
        throw new ArgumentNullException(nameof (baseMetadata));
      if (this.Sealed)
        throw new InvalidOperationException(string.Format("TypeMetadataCannotChangeAfterUse"));
      if (!this.IsModified(1))
        this._defaultValue = baseMetadata.DefaultValue;
      if (!this.IsModified(16))
        this._attachedPropertyUsage = baseMetadata.AttachedPropertyUsage;
      if (baseMetadata.PropertyChangedCallback != null)
      {
        Delegate[] invocationList = baseMetadata.PropertyChangedCallback.GetInvocationList();
        if (invocationList.Length > 0)
        {
          PropertyChangedCallback propertyChangedCallback = (PropertyChangedCallback) invocationList[0];
          for (int index = 1; index < invocationList.Length; ++index)
            propertyChangedCallback = (PropertyChangedCallback) Delegate.Combine((Delegate) propertyChangedCallback, invocationList[index]);
          this._propertyChangedCallback = propertyChangedCallback + this._propertyChangedCallback;
        }
      }
      if (this._coerceValueCallback != null)
        return;
      this._coerceValueCallback = baseMetadata.CoerceValueCallback;
    }
  }
}
