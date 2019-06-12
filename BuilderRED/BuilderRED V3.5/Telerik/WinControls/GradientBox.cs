// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.GradientBox
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Runtime.InteropServices;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  internal class GradientBox : RadControl
  {
    private FillPrimitive fill;
    private BorderPrimitive border;

    public GradientBox()
    {
      this.Behavior.BitmapRepository.DisableBitmapCache = true;
    }

    public FillPrimitive Fill
    {
      get
      {
        return this.fill;
      }
    }

    public BorderPrimitive Border
    {
      get
      {
        return this.border;
      }
    }

    public GradientStyles FillStyle
    {
      get
      {
        return this.fill.GradientStyle;
      }
      set
      {
        this.fill.GradientStyle = value;
      }
    }

    public float FillAngle
    {
      get
      {
        return this.fill.GradientAngle;
      }
      set
      {
        this.fill.GradientAngle = value;
      }
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.fill = new FillPrimitive();
      this.fill.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.border = new BorderPrimitive();
      this.border.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.RootElement.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.RootElement.Children.Add((RadElement) this.fill);
      this.RootElement.Children.Add((RadElement) this.border);
    }
  }
}
