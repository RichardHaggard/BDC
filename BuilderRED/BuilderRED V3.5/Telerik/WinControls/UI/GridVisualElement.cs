// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridVisualElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GridVisualElement : LightVisualElement
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.UseMnemonic = false;
      this.DisableHTMLRendering = true;
      this.StretchHorizontally = false;
      this.StretchVertically = false;
      this.CaptureOnMouseDown = true;
    }

    public RadGridView GridControl
    {
      get
      {
        if (this.ElementTree == null)
          return (RadGridView) null;
        return this.ElementTree.Control as RadGridView;
      }
    }

    protected virtual void ArrangeElement(
      RadElement element,
      SizeF finalSize,
      RectangleF clientRect)
    {
      if (element.FitToSizeMode == RadFitToSizeMode.FitToParentBounds)
        element.Arrange(new RectangleF((PointF) Point.Empty, finalSize));
      else if (element.FitToSizeMode == RadFitToSizeMode.FitToParentPadding)
      {
        element.Arrange(new RectangleF((float) this.BorderThickness.Left, (float) this.BorderThickness.Top, finalSize.Width - (float) this.BorderThickness.Horizontal, finalSize.Height - (float) this.BorderThickness.Vertical));
      }
      else
      {
        RectangleF finalRect = new RectangleF(clientRect.Left, clientRect.Top, Math.Min(clientRect.Width, element.DesiredSize.Width), Math.Min(clientRect.Height, element.DesiredSize.Height));
        if (element.StretchHorizontally || (double) finalRect.Width == 0.0 && element.Visibility != ElementVisibility.Collapsed)
          finalRect.Width = clientRect.Width;
        if (element.StretchVertically || (double) finalRect.Height == 0.0 && element.Visibility != ElementVisibility.Collapsed)
          finalRect.Height = clientRect.Height;
        element.Arrange(finalRect);
      }
    }

    internal static T GetElementAtPoint<T>(RadElementTree componentTree, Point point) where T : RadElement
    {
      if (componentTree != null)
      {
        for (RadElement radElement = componentTree.GetElementAtPoint(point); radElement != null; radElement = radElement.Parent)
        {
          T obj = radElement as T;
          if ((object) obj != null)
            return obj;
        }
      }
      return default (T);
    }
  }
}
