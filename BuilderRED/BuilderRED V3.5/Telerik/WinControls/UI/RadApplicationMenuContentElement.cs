// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadApplicationMenuContentElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadApplicationMenuContentElement : RadElement
  {
    private FillPrimitive fill;
    private BorderPrimitive border;
    private StackLayoutPanel layout;

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

    public StackLayoutPanel Layout
    {
      get
      {
        return this.layout;
      }
      set
      {
        if (value == null || value == this.layout)
          return;
        if (this.layout != null)
          this.Children.Remove((RadElement) this.layout);
        this.layout = value;
        this.Children.Add((RadElement) this.layout);
      }
    }

    protected override void CreateChildElements()
    {
      this.fill = new FillPrimitive();
      this.Children.Add((RadElement) this.fill);
      this.border = new BorderPrimitive();
      this.Children.Add((RadElement) this.border);
      this.layout = new StackLayoutPanel();
      this.Children.Add((RadElement) this.layout);
    }
  }
}
