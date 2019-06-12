// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterColorBoxValueEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class DataFilterColorBoxValueEditorElement : DataFilterValueEditorElement
  {
    private ColorEditorColorBox colorBox;

    public DataFilterColorBoxValueEditorElement(BaseDataFilterNodeElement dataFilterNodeElement)
      : base(dataFilterNodeElement)
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

    public ColorEditorColorBox ColorBox
    {
      get
      {
        return this.colorBox;
      }
    }

    public override void Synchronize(DataFilterCriteriaNode criteriaNode)
    {
      base.Synchronize(criteriaNode);
      Color color = Color.Empty;
      if (criteriaNode.DescriptorValue != null)
        color = (Color) criteriaNode.DescriptorValue;
      this.colorBox.BackColor = color;
    }

    internal override void AddEditor(IInputEditor editor)
    {
      this.colorBox.Visibility = ElementVisibility.Collapsed;
      base.AddEditor(editor);
    }

    internal override void RemoveEditor()
    {
      base.RemoveEditor();
      this.colorBox.Visibility = ElementVisibility.Visible;
    }

    protected override SizeF CalculateDesiredSize(
      SizeF availableSize,
      SizeF desiredSize,
      SizeF elementsDesiredSize)
    {
      SizeF desiredSize1 = base.CalculateDesiredSize(availableSize, desiredSize, elementsDesiredSize);
      desiredSize1.Width += this.ColorBox.DesiredSize.Width;
      return desiredSize1;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      if (this.Editor != null)
      {
        base.ArrangeOverride(finalSize);
        return finalSize;
      }
      this.colorBox.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y, this.colorBox.DesiredSize.Width, clientRectangle.Height));
      this.Layout.Arrange(new RectangleF(this.colorBox.DesiredSize.Width, clientRectangle.Y, clientRectangle.Width - this.colorBox.DesiredSize.Width, clientRectangle.Height));
      return finalSize;
    }
  }
}
