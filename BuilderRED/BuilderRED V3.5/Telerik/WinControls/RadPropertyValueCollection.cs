// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadPropertyValueCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls
{
  public class RadPropertyValueCollection
  {
    private Dictionary<int, RadPropertyValue> entries;
    private RadObject owner;
    private MapPropertyDelegate propertyMapper;

    internal RadPropertyValueCollection(RadObject owner)
    {
      this.owner = owner;
      this.entries = new Dictionary<int, RadPropertyValue>();
    }

    public RadPropertyValue GetEntry(RadProperty prop, bool createNew)
    {
      if (prop == null)
        throw new ArgumentNullException("RadProperty");
      if (this.propertyMapper != null)
      {
        RadProperty radProperty = this.propertyMapper(prop);
        if (radProperty != null)
          prop = radProperty;
      }
      RadPropertyValue radPropertyValue;
      if (!this.entries.TryGetValue(prop.GlobalIndex, out radPropertyValue) && createNew)
      {
        radPropertyValue = new RadPropertyValue(this.owner, prop);
        this.entries[prop.GlobalIndex] = radPropertyValue;
      }
      return radPropertyValue;
    }

    internal void Clear()
    {
      this.entries = (Dictionary<int, RadPropertyValue>) null;
    }

    internal MapPropertyDelegate PropertyMapper
    {
      get
      {
        return this.propertyMapper;
      }
      set
      {
        this.propertyMapper = value;
      }
    }

    internal void Reset()
    {
      foreach (RadPropertyValue radPropertyValue in this.entries.Values)
        radPropertyValue.Dispose();
      this.entries.Clear();
      this.propertyMapper = (MapPropertyDelegate) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void ResetLocalProperties()
    {
      if (this.entries.Count == 0)
        return;
      List<RadPropertyValue> radPropertyValueList = new List<RadPropertyValue>((IEnumerable<RadPropertyValue>) this.entries.Values);
      int count = radPropertyValueList.Count;
      for (int index = 0; index < count; ++index)
      {
        RadPropertyValue propVal = radPropertyValueList[index];
        if (propVal.ValueSource == ValueSource.Local)
        {
          int num = (int) this.owner.ResetValueCore(propVal, ValueResetFlags.Local);
        }
      }
    }

    internal void ResetStyleProperties()
    {
      if (this.entries.Count == 0)
        return;
      List<RadPropertyValue> radPropertyValueList = new List<RadPropertyValue>((IEnumerable<RadPropertyValue>) this.entries.Values);
      int count = radPropertyValueList.Count;
      for (int index = 0; index < count; ++index)
      {
        RadPropertyValue propVal = radPropertyValueList[index];
        if (propVal.ValueSource == ValueSource.Style)
        {
          int num = (int) this.owner.ResetValueCore(propVal, ValueResetFlags.Style);
        }
      }
    }

    internal void ResetInheritableProperties()
    {
      if (this.entries.Count == 0)
        return;
      List<RadPropertyValue> radPropertyValueList = new List<RadPropertyValue>((IEnumerable<RadPropertyValue>) this.entries.Values);
      int count = radPropertyValueList.Count;
      for (int index = 0; index < count; ++index)
      {
        RadPropertyValue propVal = radPropertyValueList[index];
        if (propVal.Metadata.IsInherited && propVal.InvalidateInheritedValue())
        {
          int num = (int) this.owner.UpdateValueCore(propVal);
        }
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SetLocalValuesAsDefault()
    {
      foreach (RadPropertyValue radPropertyValue in this.entries.Values)
      {
        if (radPropertyValue.ValueSource == ValueSource.Local)
        {
          radPropertyValue.BeginUpdate(true, true);
          object localValue = radPropertyValue.LocalValue;
          radPropertyValue.SetLocalValue(RadProperty.UnsetValue);
          radPropertyValue.SetDefaultValueOverride(localValue);
          radPropertyValue.EndUpdate(true, true);
        }
      }
    }

    public Dictionary<int, RadPropertyValue> Entries
    {
      get
      {
        return this.entries;
      }
    }
  }
}
