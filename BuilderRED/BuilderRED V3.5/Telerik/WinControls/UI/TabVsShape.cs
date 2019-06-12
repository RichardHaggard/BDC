// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TabVsShape
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Telerik.WinControls.UI
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class TabVsShape : ElementShape
  {
    private const char Separator = ';';
    private bool rightToLeft;
    private bool closeFigure;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    public bool RightToLeft
    {
      get
      {
        return this.rightToLeft;
      }
      set
      {
        this.rightToLeft = value;
      }
    }

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CloseFigure
    {
      get
      {
        return this.closeFigure;
      }
      set
      {
        this.closeFigure = value;
      }
    }

    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      int num = bounds.Width < bounds.Height * 2 ? bounds.Width / 2 : bounds.Height;
      if (this.rightToLeft)
      {
        graphicsPath.AddLine(new Point(bounds.X, bounds.Y + bounds.Height), new Point(bounds.X, bounds.Y + 2));
        graphicsPath.AddLine(new Point(bounds.X + 2, bounds.Y), new Point(bounds.X + bounds.Width - num - 2, bounds.Y));
        graphicsPath.AddLine(new Point(bounds.X + bounds.Width - num + 2, bounds.Y + 2), new Point(bounds.X + bounds.Width, bounds.Y + bounds.Height));
      }
      else
      {
        graphicsPath.AddLine(new Point(bounds.X, bounds.Y + bounds.Height), new Point(bounds.X + num - 2, bounds.Y + 2));
        graphicsPath.AddLine(new Point(bounds.X + num + 2, bounds.Y), new Point(bounds.X + bounds.Width - 2, bounds.Y));
        graphicsPath.AddLine(new Point(bounds.X + bounds.Width, bounds.Y + 2), new Point(bounds.X + bounds.Width, bounds.Y + bounds.Height));
      }
      if (this.closeFigure)
        graphicsPath.CloseAllFigures();
      this.MirrorPath(graphicsPath, (RectangleF) bounds);
      return graphicsPath;
    }

    public override string SerializeProperties()
    {
      return this.rightToLeft.ToString() + (object) ';' + this.closeFigure.ToString();
    }

    public override void DeserializeProperties(string propertiesString)
    {
      if (string.IsNullOrEmpty(propertiesString))
        return;
      string[] strArray = propertiesString.Split(';');
      if (strArray.Length > 0)
        this.rightToLeft = bool.Parse(strArray[0]);
      if (strArray.Length <= 1)
        return;
      this.closeFigure = bool.Parse(strArray[1]);
    }
  }
}
