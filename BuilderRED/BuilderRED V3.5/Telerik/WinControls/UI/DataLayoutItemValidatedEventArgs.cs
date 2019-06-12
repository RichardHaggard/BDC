// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataLayoutItemValidatedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class DataLayoutItemValidatedEventArgs : EventArgs
  {
    private DataLayoutControlItem item;
    private ErrorProvider errorProvider;
    private RadRangeAttribute rangeAttribute;

    public DataLayoutItemValidatedEventArgs(
      DataLayoutControlItem item,
      ErrorProvider errorProvider,
      RadRangeAttribute rangeAttribute)
    {
      this.item = item;
      this.errorProvider = errorProvider;
      this.rangeAttribute = rangeAttribute;
    }

    public DataLayoutControlItem Item
    {
      get
      {
        return this.item;
      }
    }

    public ErrorProvider ErrorProvider
    {
      get
      {
        return this.errorProvider;
      }
    }

    public RadRangeAttribute RangeAttribute
    {
      get
      {
        return this.rangeAttribute;
      }
    }
  }
}
