// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Data.EnumBinder
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Telerik.WinControls.UI.Data
{
  [ToolboxItem(false)]
  [DesignTimeVisible(false)]
  [ComVisible(false)]
  public class EnumBinder : Component, ICollection<EnumDescriptor>, IEnumerable<EnumDescriptor>, IEnumerable
  {
    private List<EnumDescriptor> enums = new List<EnumDescriptor>();
    private Type source;
    private object target;

    public EnumBinder()
    {
    }

    public EnumBinder(Type source)
    {
      this.Source = source;
    }

    [DefaultValue(null)]
    public Type Source
    {
      get
      {
        return this.source;
      }
      set
      {
        if ((object) this.source == (object) value)
          return;
        this.source = value;
        if (!this.source.IsEnum)
          return;
        Array values = Enum.GetValues(this.source);
        for (int index = 0; index < values.Length; ++index)
          this.enums.Add(new EnumDescriptor(this.source, values.GetValue(index)));
      }
    }

    [DefaultValue(null)]
    public object Target
    {
      get
      {
        return this.target;
      }
      set
      {
        if (this.target == value)
          return;
        this.Bind(value);
      }
    }

    public static implicit operator EnumBinder(Type source)
    {
      return new EnumBinder(source);
    }

    public bool Bind(object target)
    {
      if (target == null)
        throw new ArgumentNullException();
      PropertyInfo property1 = target.GetType().GetProperty("DataSource", BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);
      if ((object) property1 == null)
        return false;
      PropertyInfo property2 = target.GetType().GetProperty("DisplayMember", BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);
      if ((object) property2 != null)
        property2.SetValue(target, (object) "Description", (object[]) null);
      PropertyInfo property3 = target.GetType().GetProperty("ValueMember", BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);
      if ((object) property3 != null)
        property3.SetValue(target, (object) "Value", (object[]) null);
      property1.SetValue(target, (object) this, (object[]) null);
      this.target = target;
      return true;
    }

    void ICollection<EnumDescriptor>.Add(EnumDescriptor item)
    {
    }

    void ICollection<EnumDescriptor>.Clear()
    {
    }

    public bool Contains(EnumDescriptor item)
    {
      return this.enums.Contains(item);
    }

    public void CopyTo(EnumDescriptor[] array, int arrayIndex)
    {
      this.enums.CopyTo(array, arrayIndex);
    }

    [Browsable(false)]
    public int Count
    {
      get
      {
        return this.enums.Count;
      }
    }

    [Browsable(false)]
    public bool IsReadOnly
    {
      get
      {
        return true;
      }
    }

    bool ICollection<EnumDescriptor>.Remove(EnumDescriptor item)
    {
      return false;
    }

    public IEnumerator<EnumDescriptor> GetEnumerator()
    {
      return (IEnumerator<EnumDescriptor>) this.enums.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }
  }
}
