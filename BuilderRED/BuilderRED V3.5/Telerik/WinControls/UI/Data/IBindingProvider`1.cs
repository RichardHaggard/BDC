// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Data.IBindingProvider`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.UI.Data
{
  public interface IBindingProvider<T> where T : IDataBoundItem
  {
    IEnumerable<T> GetItems(Predicate<T> filterFunction);

    void Insert(T itemToInsert);

    void Update(T itemToUpdate, string propertyName);

    void Delete(T itemToDelete);

    event EventHandler<ListChangedEventArgs<T>> ItemsChanged;

    event PositionChangedEventHandler PositionChanged;

    int Position { get; set; }

    IPropertyMappingInfo PropertyMappings { get; set; }
  }
}
