// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadPageViewButtonElement : RadPageViewElementBase
  {
    public static RadProperty IsSelectedProperty = RadProperty.Register(nameof (IsSelected), typeof (bool), typeof (RadPageViewButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool IsSelected
    {
      get
      {
        return (bool) this.GetValue(RadPageViewButtonElement.IsSelectedProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewButtonElement.IsSelectedProperty, (object) value);
      }
    }

    static RadPageViewButtonElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadPageViewButtonElementStateManager(), typeof (RadPageViewButtonElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ImageAlignment = ContentAlignment.MiddleCenter;
    }

    protected override void DoDoubleClick(EventArgs e)
    {
      this.DoClick(e);
    }
  }
}
