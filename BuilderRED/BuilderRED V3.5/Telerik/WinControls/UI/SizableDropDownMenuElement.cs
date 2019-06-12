// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SizableDropDownMenuElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class SizableDropDownMenuElement : RadDropDownMenuElement
  {
    private RadItemOwnerCollection items;

    public SizableDropDownMenuElement()
    {
      this.items = new RadItemOwnerCollection((RadElement) ((SizableDropDownMenuLayout) this.Layout).Stack);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.SetBitState(137438953472L, false);
    }

    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadDropDownMenuElement);
      }
    }

    protected override RadDropDownMenuLayout CreateMenuLayout()
    {
      return (RadDropDownMenuLayout) new SizableDropDownMenuLayout();
    }
  }
}
