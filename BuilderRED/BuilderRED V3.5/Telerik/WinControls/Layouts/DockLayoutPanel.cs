// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.DockLayoutPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.Layouts
{
  public class DockLayoutPanel : LayoutPanel
  {
    public static RadProperty DockProperty = RadProperty.RegisterAttached("Dock", typeof (Dock), typeof (DockLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Dock.Left, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout, new PropertyChangedCallback(DockLayoutPanel.OnDockChanged)), new ValidateValueCallback(DockLayoutPanel.IsValidDock));
    public static RadProperty LastChildFillProperty = RadProperty.Register(nameof (LastChildFill), typeof (bool), typeof (DockLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));

    protected override void InitializeFields()
    {
      base.InitializeFields();
    }

    protected override SizeF ArrangeOverride(SizeF arrangeSize)
    {
      RadElementCollection children = this.Children;
      int count = children.Count;
      int num1 = count - (this.LastChildFill ? 1 : 0);
      float x = 0.0f;
      float y = 0.0f;
      float num2 = 0.0f;
      float num3 = 0.0f;
      for (int index = 0; index < count; ++index)
      {
        RadElement element = children[index];
        if (element != null)
        {
          SizeF desiredSize = element.DesiredSize;
          RectangleF finalRect = new RectangleF(x, y, Math.Max(0.0f, arrangeSize.Width - (x + num2)), Math.Max(0.0f, arrangeSize.Height - (y + num3)));
          if (index < num1)
          {
            switch (DockLayoutPanel.GetDock(element))
            {
              case Dock.Left:
                x += desiredSize.Width;
                finalRect.Width = desiredSize.Width;
                break;
              case Dock.Top:
                y += desiredSize.Height;
                finalRect.Height = desiredSize.Height;
                break;
              case Dock.Right:
                num2 += desiredSize.Width;
                finalRect.X = Math.Max(0.0f, arrangeSize.Width - num2);
                finalRect.Width = desiredSize.Width;
                break;
              case Dock.Bottom:
                num3 += desiredSize.Height;
                finalRect.Y = Math.Max(0.0f, arrangeSize.Height - num3);
                finalRect.Height = desiredSize.Height;
                break;
            }
          }
          element.Arrange(finalRect);
        }
      }
      return arrangeSize;
    }

    public static Dock GetDock(RadElement element)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      return (Dock) element.GetValue(DockLayoutPanel.DockProperty);
    }

    protected override SizeF MeasureOverride(SizeF constraint)
    {
      RadElementCollection children = this.Children;
      float val1_1 = 0.0f;
      float val1_2 = 0.0f;
      float val2_1 = 0.0f;
      float val2_2 = 0.0f;
      int index = 0;
      for (int count = children.Count; index < count; ++index)
      {
        RadElement element = children[index];
        if (element != null)
        {
          SizeF availableSize = new SizeF(Math.Max(0.0f, constraint.Width - val2_1), Math.Max(0.0f, constraint.Height - val2_2));
          element.Measure(availableSize);
          SizeF desiredSize = element.DesiredSize;
          switch (DockLayoutPanel.GetDock(element))
          {
            case Dock.Left:
            case Dock.Right:
              val1_2 = Math.Max(val1_2, val2_2 + desiredSize.Height);
              val2_1 += desiredSize.Width;
              continue;
            case Dock.Top:
            case Dock.Bottom:
              val1_1 = Math.Max(val1_1, val2_1 + desiredSize.Width);
              val2_2 += desiredSize.Height;
              continue;
            default:
              continue;
          }
        }
      }
      return new SizeF(Math.Max(val1_1, val2_1), Math.Max(val1_2, val2_2));
    }

    private static bool IsValidDock(object value, RadObject obj)
    {
      Dock dock = (Dock) value;
      switch (dock)
      {
        case Dock.Left:
        case Dock.Top:
        case Dock.Right:
          return true;
        default:
          return dock == Dock.Bottom;
      }
    }

    private static void OnDockChanged(RadObject obj, RadPropertyChangedEventArgs e)
    {
      RadElement radElement = obj as RadElement;
      if (radElement == null)
        return;
      (radElement.Parent as DockLayoutPanel)?.InvalidateMeasure();
    }

    public static void SetDock(RadElement element, Dock dock)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      int num = (int) element.SetValue(DockLayoutPanel.DockProperty, (object) dock);
    }

    public bool LastChildFill
    {
      get
      {
        return (bool) this.GetValue(DockLayoutPanel.LastChildFillProperty);
      }
      set
      {
        int num = (int) this.SetValue(DockLayoutPanel.LastChildFillProperty, (object) value);
      }
    }
  }
}
