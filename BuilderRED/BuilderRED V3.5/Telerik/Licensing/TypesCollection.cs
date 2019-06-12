// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.TypesCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.ObjectModel;

namespace Telerik.Licensing
{
  internal class TypesCollection : Collection<string>
  {
    public event CollectionChangedEventHandler CollectionChanged;

    public void TryAdd(string item)
    {
      if (this.Contains(item))
        return;
      this.Add(item);
    }

    protected override void InsertItem(int index, string item)
    {
      base.InsertItem(index, item);
      this.RaiseCollectionChanged();
    }

    private void RaiseCollectionChanged()
    {
      if (this.CollectionChanged == null)
        return;
      this.CollectionChanged((object) this, new CollectionChangedEventArgs());
    }
  }
}
