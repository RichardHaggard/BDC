// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterGroupNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class DataFilterGroupNode : RadTreeNode
  {
    private CompositeFilterDescriptor descriptor;
    private DataFilterAddNode associatedAddNode;

    public CompositeFilterDescriptor CompositeDescriptor
    {
      get
      {
        return this.descriptor;
      }
      set
      {
        if (this.descriptor == value)
          return;
        if (value != null)
          value.PropertyChanged += new PropertyChangedEventHandler(this.Descriptor_PropertyChanged);
        else if (this.descriptor != null)
          this.descriptor.PropertyChanged -= new PropertyChangedEventHandler(this.Descriptor_PropertyChanged);
        this.descriptor = value;
      }
    }

    public virtual FilterLogicalOperator LogicalOperator
    {
      get
      {
        return this.CompositeDescriptor.LogicalOperator;
      }
      set
      {
        this.CompositeDescriptor.LogicalOperator = value;
      }
    }

    public DataFilterAddNode AssociatedAddNode
    {
      get
      {
        return this.associatedAddNode;
      }
      set
      {
        this.associatedAddNode = value;
      }
    }

    public virtual void AddChildDescriptor(FilterDescriptor descriptorToAdd)
    {
      if (descriptorToAdd == null || this.CompositeDescriptor == null)
        return;
      this.CompositeDescriptor.FilterDescriptors.Add(descriptorToAdd);
    }

    public virtual void RemoveChildDescriptor(FilterDescriptor descriptorToRemove)
    {
      if (descriptorToRemove == null || this.CompositeDescriptor == null)
        return;
      this.CompositeDescriptor.FilterDescriptors.Remove(descriptorToRemove);
    }

    public override void Remove()
    {
      if (this.parent != null)
        (this.parent as DataFilterGroupNode).RemoveChildDescriptor((FilterDescriptor) this.CompositeDescriptor);
      base.Remove();
    }

    public override string ToString()
    {
      string str = nameof (DataFilterGroupNode);
      if (this.CompositeDescriptor == null)
        return str;
      return string.Format("{0} : {1}", (object) str, (object) this.LogicalOperator);
    }

    private void Descriptor_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "LogicalOperator"))
        return;
      this.OnNotifyPropertyChanged("LogicalOperator");
    }
  }
}
