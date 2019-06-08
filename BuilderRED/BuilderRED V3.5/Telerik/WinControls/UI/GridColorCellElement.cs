// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridColorCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridColorCellElement : GridDataCellElement
  {
    private ColorEditorColorBox colorBox;

    public GridColorCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.colorBox = new ColorEditorColorBox();
      int num = (int) this.colorBox.SetDefaultValueOverride(RadElement.MarginProperty, (object) new Padding(2));
      this.colorBox.BorderGradientStyle = GradientStyles.Solid;
      this.Children.Add((RadElement) this.colorBox);
    }

    public override bool IsCompatible(GridViewColumn data, object context)
    {
      return data is GridViewColorColumn;
    }

    public ColorEditorColorBox ColorBox
    {
      get
      {
        return this.colorBox;
      }
    }

    protected override void SetContentCore(object value)
    {
      base.SetContentCore(value);
      this.colorBox.BackColor = (Color) RadDataConverter.Instance.Format(value, typeof (Color), (IDataConversionInfoProvider) this);
    }

    public override void AddEditor(IInputEditor editor)
    {
      this.colorBox.Visibility = ElementVisibility.Collapsed;
      base.AddEditor(editor);
    }

    public override void RemoveEditor(IInputEditor editor)
    {
      base.RemoveEditor(editor);
      this.colorBox.Visibility = ElementVisibility.Visible;
    }

    protected override SizeF CalculateDesiredSize(
      SizeF availableSize,
      SizeF desiredSize,
      SizeF elementsDesiredSize)
    {
      desiredSize.Width += elementsDesiredSize.Width;
      if ((double) elementsDesiredSize.Height > (double) desiredSize.Height)
        desiredSize.Height = elementsDesiredSize.Height;
      desiredSize.Width = Math.Min(desiredSize.Width, availableSize.Width);
      desiredSize.Height = Math.Min(desiredSize.Height, availableSize.Height);
      return desiredSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      if (this.Editor != null)
      {
        this.ArrangeEditorElement(finalSize, clientRectangle);
        return finalSize;
      }
      this.colorBox.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y, this.colorBox.DesiredSize.Width, clientRectangle.Height));
      this.Layout.Arrange(new RectangleF(this.colorBox.DesiredSize.Width, clientRectangle.Y, clientRectangle.Width - this.colorBox.DesiredSize.Width, clientRectangle.Height));
      return finalSize;
    }
  }
}
