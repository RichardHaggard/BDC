// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterRootNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class DataFilterRootNode : DataFilterGroupNode
  {
    private FilterDescriptorCollection filters;

    public FilterDescriptorCollection Filters
    {
      get
      {
        return this.filters;
      }
      set
      {
        if (this.filters == value)
          return;
        if (value != null)
          value.PropertyChanged += new PropertyChangedEventHandler(this.Filters_PropertyChanged);
        else if (this.filters != null)
          this.filters.PropertyChanged -= new PropertyChangedEventHandler(this.Filters_PropertyChanged);
        this.filters = value;
      }
    }

    public override FilterLogicalOperator LogicalOperator
    {
      get
      {
        return this.Filters.LogicalOperator;
      }
      set
      {
        this.Filters.LogicalOperator = value;
      }
    }

    public override void AddChildDescriptor(FilterDescriptor descriptorToAdd)
    {
      if (descriptorToAdd == null || this.filters == null)
        return;
      this.filters.Add(descriptorToAdd);
    }

    public override void RemoveChildDescriptor(FilterDescriptor descriptorToRemove)
    {
      if (descriptorToRemove == null || this.filters == null)
        return;
      this.filters.Remove(descriptorToRemove);
    }

    public override string ToString()
    {
      string str = nameof (DataFilterRootNode);
      if (this.filters == null)
        return str;
      return string.Format("{0} : {1}", (object) str, (object) this.LogicalOperator);
    }

    private void Filters_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "LogicalOperator"))
        return;
      this.OnNotifyPropertyChanged("LogicalOperator");
    }
  }
}
