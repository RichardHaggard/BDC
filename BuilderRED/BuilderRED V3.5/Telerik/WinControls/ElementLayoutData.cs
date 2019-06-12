// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ElementLayoutData
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public class ElementLayoutData
  {
    private RadElement element;
    private PerformLayoutType performLayoutType;
    private bool performed;

    public RadElement Element
    {
      get
      {
        return this.element;
      }
      set
      {
        this.element = value;
      }
    }

    public PerformLayoutType PerformLayoutType
    {
      get
      {
        return this.performLayoutType;
      }
      set
      {
        this.performLayoutType = value;
      }
    }

    public bool Performed
    {
      get
      {
        return this.performed;
      }
      set
      {
        this.performed = value;
      }
    }

    public ElementLayoutData(RadElement element, PerformLayoutType performLayoutType)
    {
      this.element = element;
      this.performLayoutType = performLayoutType;
    }
  }
}
