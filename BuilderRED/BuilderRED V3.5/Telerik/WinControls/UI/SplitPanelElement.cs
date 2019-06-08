// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SplitPanelElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class SplitPanelElement : RadItem
  {
    private BorderPrimitive borderPrimitive;
    private FillPrimitive fillPrimitive;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldHandleMouseInput = false;
    }

    [Browsable(false)]
    public BorderPrimitive Border
    {
      get
      {
        return this.borderPrimitive;
      }
    }

    [Browsable(false)]
    public FillPrimitive Fill
    {
      get
      {
        return this.fillPrimitive;
      }
    }

    protected override void CreateChildElements()
    {
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "SplitContainerBorder";
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.Class = "SplitContainerFill";
      this.Children.Add((RadElement) this.fillPrimitive);
      this.Children.Add((RadElement) this.borderPrimitive);
    }
  }
}
