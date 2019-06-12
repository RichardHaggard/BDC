// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ValidationInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  internal class ValidationInfo
  {
    private RadLabel label;
    private RadLabel validationLabel;
    private ErrorProvider errorProvider;
    private RadRangeAttribute rangeAttribute;

    public ValidationInfo(
      RadLabel label,
      RadLabel validationLabel,
      ErrorProvider errorProvider,
      RadRangeAttribute rangeAttribute)
    {
      this.label = label;
      this.validationLabel = validationLabel;
      this.errorProvider = errorProvider;
      this.rangeAttribute = rangeAttribute;
    }

    public ValidationInfo()
    {
    }

    public RadLabel ValidationLabel
    {
      get
      {
        return this.validationLabel;
      }
      set
      {
        this.validationLabel = value;
      }
    }

    public RadLabel Label
    {
      get
      {
        return this.label;
      }
      set
      {
        this.label = value;
      }
    }

    public ErrorProvider ErrorProvider
    {
      get
      {
        return this.errorProvider;
      }
      set
      {
        this.errorProvider = value;
      }
    }

    public RadRangeAttribute RangeAttribute
    {
      get
      {
        return this.rangeAttribute;
      }
      set
      {
        this.rangeAttribute = value;
      }
    }
  }
}
