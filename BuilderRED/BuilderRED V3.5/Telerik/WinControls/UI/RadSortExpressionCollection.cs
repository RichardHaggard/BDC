// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadSortExpressionCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  [ListBindable(BindableSupport.No)]
  public class RadSortExpressionCollection : GridViewSortDescriptorCollection
  {
    public RadSortExpressionCollection(GridViewTemplate owner)
      : base(owner)
    {
    }

    public T GetDescriptor<T>(int index) where T : SortDescriptor
    {
      return this[index] as T;
    }

    public void Add(string expression)
    {
      this.Add((SortDescriptor) new GridSortField(expression));
    }

    public void Add(string fieldName, RadSortOrder sortOrder)
    {
      this.Add((SortDescriptor) new GridSortField(fieldName, sortOrder));
    }
  }
}
