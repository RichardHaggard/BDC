// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class RadPageViewObject : DisposableObject
  {
    private RadPageView owner;

    public RadPageView Owner
    {
      get
      {
        return this.owner;
      }
      protected set
      {
        this.owner = value;
      }
    }
  }
}
