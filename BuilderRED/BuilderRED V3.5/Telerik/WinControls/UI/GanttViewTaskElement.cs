// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTaskElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GanttViewTaskElement : GanttGraphicalViewBaseTaskElement
  {
    private TaskProgressIndicatorElement progressIndicatorElement;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawFill = true;
      this.BackColor = Color.LightBlue;
      this.GradientStyle = GradientStyles.Solid;
      this.CaptureOnMouseDown = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.progressIndicatorElement = this.CreateProgressIndicatorElement();
      this.Children.Add((RadElement) this.progressIndicatorElement);
    }

    protected virtual TaskProgressIndicatorElement CreateProgressIndicatorElement()
    {
      return new TaskProgressIndicatorElement();
    }

    public TaskProgressIndicatorElement ProgressIndicatorElement
    {
      get
      {
        return this.progressIndicatorElement;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      this.Layout.Measure(clientRectangle.Size);
      this.ProgressIndicatorElement.Measure(new SizeF((float) (int) ((Decimal) clientRectangle.Width / new Decimal(100) * ((GanttViewBaseItemElement) this.Parent).Data.Progress), clientRectangle.Height));
      return availableSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      this.Layout.Arrange(clientRectangle);
      int num = (int) ((Decimal) clientRectangle.Width / new Decimal(100) * ((GanttViewBaseItemElement) this.Parent).Data.Progress);
      this.ProgressIndicatorElement.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Bottom - this.ProgressIndicatorElement.DesiredSize.Height, (float) num, this.ProgressIndicatorElement.DesiredSize.Height));
      return finalSize;
    }
  }
}
