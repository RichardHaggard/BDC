// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GroupBoxFooter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class GroupBoxFooter : GroupBoxVisualElement
  {
    private TextPrimitive textPrimitive;
    private ImagePrimitive imagePrimitive;
    private ImageAndTextLayoutPanel imageAndTextLayout;

    public TextPrimitive TextPrimitive
    {
      get
      {
        return this.textPrimitive;
      }
      set
      {
        this.textPrimitive = value;
      }
    }

    public ImagePrimitive ImagePrimitive
    {
      get
      {
        return this.imagePrimitive;
      }
      set
      {
        this.imagePrimitive = value;
      }
    }

    public ImageAndTextLayoutPanel ImageAndTextLayout
    {
      get
      {
        return this.imageAndTextLayout;
      }
      set
      {
        this.imageAndTextLayout = value;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.textPrimitive = new TextPrimitive();
      this.imagePrimitive = new ImagePrimitive();
      this.imageAndTextLayout = new ImageAndTextLayoutPanel();
      int num1 = (int) this.textPrimitive.SetValue(ImageAndTextLayoutPanel.IsTextPrimitiveProperty, (object) true);
      int num2 = (int) this.imagePrimitive.SetValue(ImageAndTextLayoutPanel.IsImagePrimitiveProperty, (object) true);
      this.imageAndTextLayout.Children.Add((RadElement) this.imagePrimitive);
      this.imageAndTextLayout.Children.Add((RadElement) this.textPrimitive);
      this.Children.Add((RadElement) this.imageAndTextLayout);
      this.Class = nameof (GroupBoxFooter);
      this.Visibility = ElementVisibility.Collapsed;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      this.imageAndTextLayout.Measure(availableSize);
      empty.Width = Math.Max(this.imageAndTextLayout.DesiredSize.Width + (float) this.BorderThickness.Vertical, empty.Width);
      empty.Height = Math.Max(this.imageAndTextLayout.DesiredSize.Height + (float) this.BorderThickness.Horizontal, empty.Height);
      return empty;
    }

    public override string ToString()
    {
      return nameof (GroupBoxFooter);
    }
  }
}
