// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMaskedEditBoxEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class RadMaskedEditBoxEditorElement : RadMaskedEditBoxElement
  {
    static RadMaskedEditBoxEditorElement()
    {
      RadItem.TextProperty.OverrideMetadata(typeof (RadMaskedEditBoxEditorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.Cancelable));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.MinSize = new Size(0, 20);
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadMaskedEditBoxElement);
      }
    }

    public override MaskType MaskType
    {
      get
      {
        return base.MaskType;
      }
      set
      {
        if (value == this.MaskType)
          return;
        this.SuspendPropertyNotifications();
        base.MaskType = value;
        this.Value = (object) null;
        this.ResumePropertyNotifications();
        this.OnNotifyPropertyChanged(nameof (MaskType));
      }
    }
  }
}
