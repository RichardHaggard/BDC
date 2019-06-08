// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadShimControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  [ToolboxItem(false)]
  public class RadShimControl : Control
  {
    private double opacity = 1.0;

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = base.CreateParams;
        createParams.Style &= -79691777;
        createParams.ExStyle &= -262145;
        createParams.Style |= int.MinValue;
        createParams.Style |= 67108864;
        createParams.Style |= 134217728;
        createParams.ExStyle |= 524288;
        createParams.ExStyle |= 32;
        return createParams;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override void OnHandleCreated(EventArgs e)
    {
      base.OnHandleCreated(e);
      this.UpdateLayered();
    }

    private byte OpacityAsByte
    {
      get
      {
        return (byte) (this.Opacity * (double) byte.MaxValue);
      }
    }

    [Category("CatWindowStyle")]
    [DefaultValue(1.0)]
    [TypeConverter(typeof (OpacityConverter))]
    [Description("FormOpacityDescr")]
    public double Opacity
    {
      get
      {
        return this.opacity;
      }
      set
      {
        if (value > 1.0)
          value = 1.0;
        else if (value < 0.0)
          value = 0.0;
        this.opacity = value;
        this.UpdateLayered();
      }
    }

    private void UpdateLayered()
    {
      if (!this.IsHandleCreated || !OSFeature.Feature.IsPresent(OSFeature.LayeredWindows))
        return;
      NativeMethods.SetLayeredWindowAttributes(new HandleRef((object) this, this.Handle), 0, this.OpacityAsByte, 2);
    }

    public bool Redraw
    {
      set
      {
        if (!this.Visible)
          return;
        if (value)
          NativeMethods.SendMessage(new HandleRef((object) null, this.Handle), 11, (IntPtr) 1, (IntPtr) 0);
        else
          NativeMethods.SendMessage(new HandleRef((object) null, this.Handle), 11, (IntPtr) 0, (IntPtr) 0);
      }
    }
  }
}
