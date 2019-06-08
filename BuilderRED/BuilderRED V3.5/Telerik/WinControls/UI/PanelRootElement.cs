// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PanelRootElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class PanelRootElement : RootRadElement
  {
    protected override ValueUpdateResult SetValueCore(
      RadPropertyValue propVal,
      object propModifier,
      object newValue,
      ValueSource source)
    {
      if (propVal.Property == RadElement.ShapeProperty && newValue != null && !(newValue is RoundRectShape))
        return ValueUpdateResult.Canceled;
      return base.SetValueCore(propVal, propModifier, newValue, source);
    }

    protected override void OnBoundsChanged(RadPropertyChangedEventArgs e)
    {
      base.OnBoundsChanged(e);
      if (this.LayoutManager.IsUpdating)
        return;
      RadScrollablePanel control = this.ElementTree.Control as RadScrollablePanel;
      if (control == null || control.AutoSize || e.NewValueSource != ValueSource.Animation)
        return;
      control.Size = this.Size;
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RootRadElement);
      }
    }
  }
}
