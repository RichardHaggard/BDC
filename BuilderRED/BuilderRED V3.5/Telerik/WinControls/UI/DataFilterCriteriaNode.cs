// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterCriteriaNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class DataFilterCriteriaNode : RadTreeNode
  {
    private FilterDescriptor descriptor;
    private object editorValue;
    private DataFilterDescriptorItem descriptorItem;

    public DataFilterCriteriaNode()
    {
      this.AllowDrop = false;
    }

    public FilterDescriptor Descriptor
    {
      get
      {
        return this.descriptor;
      }
      set
      {
        this.descriptor = value;
        if (value == null)
          return;
        this.InitializeDescriptorDefaultValues();
      }
    }

    public DataFilterDescriptorItem DescriptorItem
    {
      get
      {
        return this.descriptorItem;
      }
      set
      {
        this.descriptorItem = value;
      }
    }

    public string PropertyName
    {
      get
      {
        return this.Descriptor.PropertyName;
      }
      set
      {
        if (this.Descriptor.PropertyName == value)
          return;
        System.Type valueType = this.ValueType;
        bool flag = this.DescriptorItem is DataFilterComboDescriptorItem;
        this.Descriptor.PropertyName = value;
        this.DescriptorItem = (this.TreeViewElement as RadDataFilterElement).GetDescriptorItemByName(value);
        if ((object) valueType != (object) this.ValueType)
        {
          if ((object) this.ValueType == (object) typeof (bool) || (object) this.ValueType == (object) typeof (Color) || ((object) valueType == (object) typeof (bool) || (object) valueType == (object) typeof (Color)))
            this.TreeViewElement.Update(RadTreeViewElement.UpdateActions.Reset);
          this.UpdateDescriptor();
        }
        else
        {
          if (!flag && !(this.DescriptorItem is DataFilterComboDescriptorItem))
            return;
          this.UpdateDescriptor();
        }
      }
    }

    public FilterOperator FilterOperator
    {
      get
      {
        return this.Descriptor.Operator;
      }
      set
      {
        bool flag = DataFilterOperatorContext.IsEditableFilterOperator(this.Descriptor.Operator);
        this.Descriptor.Operator = value;
        if (flag || this.Descriptor.Value != null && !(this.Descriptor.Value.ToString() == string.Empty))
          return;
        this.descriptor.Value = this.DescriptorItem.DefaultValue;
      }
    }

    public object DescriptorValue
    {
      get
      {
        return this.Descriptor.Value;
      }
      set
      {
        this.Descriptor.Value = value;
      }
    }

    public System.Type ValueType
    {
      get
      {
        return this.DescriptorItem.DescriptorType;
      }
    }

    public override object Value
    {
      get
      {
        return this.EditorValue;
      }
      set
      {
        base.Value = value;
      }
    }

    internal object EditorValue
    {
      get
      {
        return this.editorValue;
      }
      set
      {
        this.editorValue = value;
      }
    }

    public override void Remove()
    {
      if (this.parent != null)
        (this.parent as DataFilterGroupNode).RemoveChildDescriptor(this.Descriptor);
      base.Remove();
    }

    public override string ToString()
    {
      string str = nameof (DataFilterCriteriaNode);
      if (this.Descriptor == null)
        return str;
      return string.Format("{0} : {1} - {2} - {3}", (object) str, (object) this.PropertyName, (object) this.FilterOperator, this.DescriptorValue);
    }

    protected internal virtual string GetFormattedValue()
    {
      RadDataFilterElement treeViewElement = this.TreeViewElement as RadDataFilterElement;
      if (treeViewElement != null)
      {
        object comboboxValue = treeViewElement.GetComboboxValue(this);
        if (comboboxValue != null)
          return comboboxValue.ToString();
      }
      string s = this.Descriptor.Value == null ? string.Empty : this.DescriptorValue.ToString();
      if ((object) this.ValueType != (object) typeof (DateTime) && (object) this.ValueType != (object) typeof (DateTime?))
        return s;
      DateTime result = DateTime.Now;
      if (this.Descriptor.Value is DateTime)
        result = (DateTime) this.descriptor.Value;
      else if (s != string.Empty)
        DateTime.TryParse(s, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
      string format = string.Empty;
      if (treeViewElement != null)
      {
        switch (treeViewElement.DefaultDateEditorFormat)
        {
          case DateTimePickerFormat.Long:
            format = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.LongDatePattern;
            break;
          case DateTimePickerFormat.Time:
            format = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.LongTimePattern;
            break;
          case DateTimePickerFormat.Custom:
            format = treeViewElement.DefaultCustomDateEditorFormat;
            break;
          default:
            format = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern;
            break;
        }
      }
      return result.ToString(format);
    }

    private void InitializeDescriptorDefaultValues()
    {
      if (string.IsNullOrEmpty(this.Descriptor.PropertyName))
        return;
      this.DescriptorItem = (this.TreeViewElement as RadDataFilterElement).GetDescriptorItemByName(this.Descriptor.PropertyName);
      if (!Enum.IsDefined(typeof (FilterOperator), (object) this.descriptor.Operator))
        this.descriptor.Operator = this.DescriptorItem.DefaultFilterOperator;
      if (this.descriptor.Value == null || this.descriptor.Value.ToString() == string.Empty)
      {
        this.descriptor.Value = this.DescriptorItem.DefaultValue;
      }
      else
      {
        if (DataFilterOperatorContext.IsEditableFilterOperator(this.descriptor.Operator))
          return;
        this.descriptor.Value = (object) null;
      }
    }

    private void UpdateDescriptor()
    {
      this.Descriptor.Operator = this.DescriptorItem.DefaultFilterOperator;
      this.Descriptor.Value = this.DescriptorItem.DefaultValue;
    }
  }
}
