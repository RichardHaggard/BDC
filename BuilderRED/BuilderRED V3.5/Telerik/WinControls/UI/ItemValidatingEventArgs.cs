// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ItemValidatingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ItemValidatingEventArgs : CancelEventArgs
  {
    private RadLabel label;
    private RadLabel validationLabel;
    private ErrorProvider errorProvider;
    private RadRangeAttribute rangeAttribute;

    public ItemValidatingEventArgs(
      RadLabel label,
      RadLabel validation,
      ErrorProvider errorProvider,
      RadRangeAttribute rangeAttribute)
    {
      this.label = label;
      this.validationLabel = validation;
      this.errorProvider = errorProvider;
      this.rangeAttribute = rangeAttribute;
    }

    public RadLabel Label
    {
      get
      {
        return this.label;
      }
    }

    public RadLabel ValidationLabel
    {
      get
      {
        return this.validationLabel;
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
