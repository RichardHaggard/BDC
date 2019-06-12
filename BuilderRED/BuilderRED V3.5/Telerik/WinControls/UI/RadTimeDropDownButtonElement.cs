// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTimeDropDownButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadTimeDropDownButtonElement : RadButtonElement
  {
    static RadTimeDropDownButtonElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadTimeDropDownButtonElementStateManagerFactory(RadTimePickerElement.IsDropDownShownProperty), typeof (RadTimeDropDownButtonElement));
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(this.GetClientRectangle(availableSize).Size);
      sizeF.Width = Math.Min(sizeF.Width, availableSize.Width);
      sizeF.Height = Math.Min(sizeF.Height, availableSize.Height);
      return sizeF;
    }
  }
}
