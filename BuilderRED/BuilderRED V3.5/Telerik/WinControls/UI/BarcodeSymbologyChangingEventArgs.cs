// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BarcodeSymbologyChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.UI.Barcode.Symbology;

namespace Telerik.WinControls.UI
{
  public class BarcodeSymbologyChangingEventArgs : CancelEventArgs
  {
    private ISymbology oldValue;
    private ISymbology newValue;

    public BarcodeSymbologyChangingEventArgs(ISymbology oldValue, ISymbology newValue)
    {
      this.oldValue = oldValue;
      this.newValue = newValue;
    }

    public ISymbology OldValue
    {
      get
      {
        return this.oldValue;
      }
      set
      {
        this.oldValue = value;
      }
    }

    public ISymbology NewValue
    {
      get
      {
        return this.newValue;
      }
      set
      {
        this.newValue = value;
      }
    }
  }
}
