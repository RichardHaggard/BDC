// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ComboBoxEditorLayoutPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class ComboBoxEditorLayoutPanel : LayoutPanel
  {
    private RadElement content;
    private RadElement arrowButton;

    public RadElement Content
    {
      get
      {
        return this.content;
      }
      set
      {
        if (object.ReferenceEquals((object) value, (object) this.content))
          return;
        if (this.content != null && this.Children.Contains(this.content))
          this.Children.Remove(this.content);
        if (!this.Children.Contains(value))
          this.Children.Add(value);
        this.content = value;
      }
    }

    public RadElement ArrowButton
    {
      get
      {
        return this.arrowButton;
      }
      set
      {
        if (object.ReferenceEquals((object) value, (object) this.arrowButton))
          return;
        if (this.arrowButton != null)
          this.Children.Remove(this.arrowButton);
        this.Children.Add(value);
        this.arrowButton = value;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      for (int index = 0; index < this.Children.Count; ++index)
      {
        RadElement child = this.Children[index];
        child.Measure(availableSize);
        empty.Width += child.DesiredSize.Width;
        empty.Height = Math.Max(empty.Height, child.DesiredSize.Height);
      }
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = this.arrowButton != null ? this.arrowButton.DesiredSize : SizeF.Empty;
      for (int index = 0; index < this.Children.Count; ++index)
      {
        RadElement child = this.Children[index];
        if (this.arrowButton != null && object.ReferenceEquals((object) child, (object) this.arrowButton))
        {
          RectangleF finalRect = new RectangleF(this.RightToLeft ? 0.0f : finalSize.Width - sizeF.Width, 0.0f, sizeF.Width, finalSize.Height);
          child.Arrange(finalRect);
        }
        else if (this.content != null && object.ReferenceEquals((object) child, (object) this.content))
        {
          RectangleF finalRect = new RectangleF(this.RightToLeft ? sizeF.Width : 0.0f, 0.0f, finalSize.Width - sizeF.Width, finalSize.Height);
          child.Arrange(finalRect);
        }
        else
          child.Arrange(new RectangleF(PointF.Empty, finalSize));
      }
      return finalSize;
    }
  }
}
