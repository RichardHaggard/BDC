// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.CollectionChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public class CollectionChangedEventArgs
  {
    private object target;
    private ItemsChangeOperation operation;
    private int index;

    public object Target
    {
      get
      {
        return this.target;
      }
      set
      {
        this.target = value;
      }
    }

    public int Index
    {
      get
      {
        return this.index;
      }
      set
      {
        this.index = value;
      }
    }

    public ItemsChangeOperation Operation
    {
      get
      {
        return this.operation;
      }
      set
      {
        this.operation = value;
      }
    }

    public CollectionChangedEventArgs(object target, int index, ItemsChangeOperation operation)
    {
      this.target = target;
      this.index = index;
      this.operation = operation;
    }
  }
}
