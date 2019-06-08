// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCommandBarStripPanelLayoutInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class RadCommandBarStripPanelLayoutInfo
  {
    public RectangleF ControlBoundingRectangle;
    public PointF DesiredLocation;
    public RectangleF ArrangeRectangle;
    public CommandBarStripElement commandBarStripElement;
    public float DesiredSpaceToEnd;
    public float IntersectionSpaceToEnd;
    public float MinSpaceToEnd;
    public SizeF ExpectedDesiredSize;

    public RadCommandBarStripPanelLayoutInfo(CommandBarStripElement commandBarStripElement)
    {
      this.commandBarStripElement = commandBarStripElement;
      this.ControlBoundingRectangle = (RectangleF) commandBarStripElement.ControlBoundingRectangle;
      this.DesiredLocation = commandBarStripElement.DesiredLocation;
      this.ArrangeRectangle = RectangleF.Empty;
      this.DesiredSpaceToEnd = 0.0f;
      this.IntersectionSpaceToEnd = 0.0f;
      this.MinSpaceToEnd = 0.0f;
    }
  }
}
