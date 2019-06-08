// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.DependentList
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  internal class DependentList : FrugalObjectList<Dependent>
  {
    private static int _skipper;

    public void Add(RadObject d, RadProperty dp)
    {
      if (++DependentList._skipper % (1 + this.Count / 4) == 0)
        this.CleanUpDeadWeakReferences(true);
      this.Add(new Dependent(d, dp));
    }

    private void CleanUpDeadWeakReferences(bool doAll)
    {
      if (this.Count == 0)
        return;
      Dependent[] array = this.ToArray();
      for (int index = array.Length - 1; index >= 0; --index)
      {
        if (!array[index].IsValid())
          this.RemoveAt(index);
        else if (!doAll)
          break;
      }
    }

    public void InvalidateDependents(RadObject source, RadProperty sourceDP)
    {
      Dependent[] array = this.ToArray();
      int num = 0;
      while (num < array.Length)
        ++num;
    }

    public void Remove(RadObject d, RadProperty dp)
    {
      this.Remove(new Dependent(d, dp));
    }

    public bool IsEmpty
    {
      get
      {
        if (this.Count == 0)
          return true;
        this.CleanUpDeadWeakReferences(false);
        return 0 == this.Count;
      }
    }
  }
}
