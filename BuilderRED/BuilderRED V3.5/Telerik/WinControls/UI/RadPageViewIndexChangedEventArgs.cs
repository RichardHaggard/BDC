// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewIndexChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class RadPageViewIndexChangedEventArgs : RadPageViewEventArgs
  {
    private int oldIndex;
    private int newIndex;

    public RadPageViewIndexChangedEventArgs(RadPageViewPage page, int oldIndex, int newIndex)
      : base(page)
    {
      this.oldIndex = oldIndex;
      this.newIndex = newIndex;
    }

    public int OldIndex
    {
      get
      {
        return this.oldIndex;
      }
    }

    public int NewIndex
    {
      get
      {
        return this.newIndex;
      }
    }
  }
}
