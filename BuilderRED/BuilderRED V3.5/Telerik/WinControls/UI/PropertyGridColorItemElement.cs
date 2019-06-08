// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridColorItemElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class PropertyGridColorItemElement : PropertyGridItemElement
  {
    private ColorEditorColorBox colorBox;
    private PropertyGridContentElement contentElement;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.colorBox = new ColorEditorColorBox();
      this.contentElement = new PropertyGridContentElement();
      this.contentElement.DrawBorder = false;
      this.contentElement.DrawFill = false;
      this.contentElement.DrawText = true;
      this.contentElement.StretchHorizontally = true;
      StackLayoutElement stackLayoutElement = new StackLayoutElement();
      stackLayoutElement.Orientation = Orientation.Horizontal;
      stackLayoutElement.FitInAvailableSize = true;
      stackLayoutElement.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      stackLayoutElement.Padding = new Padding(3);
      stackLayoutElement.Alignment = ContentAlignment.MiddleLeft;
      stackLayoutElement.Children.Add((RadElement) this.colorBox);
      stackLayoutElement.Children.Add((RadElement) this.contentElement);
      this.ValueElement.Children.Add((RadElement) stackLayoutElement);
      this.ValueElement.DrawText = false;
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.UnwireEvents();
    }

    internal ColorEditorColorBox ColorBox
    {
      get
      {
        return this.colorBox;
      }
    }

    public PropertyGridContentElement ContentElement
    {
      get
      {
        return this.contentElement;
      }
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (PropertyGridItemElement);
      }
    }

    protected virtual void WireEvents()
    {
      this.colorBox.MouseMove += new MouseEventHandler(this.element_MouseMove);
      this.contentElement.MouseMove += new MouseEventHandler(this.element_MouseMove);
    }

    protected virtual void UnwireEvents()
    {
      this.colorBox.MouseMove -= new MouseEventHandler(this.element_MouseMove);
      this.contentElement.MouseMove -= new MouseEventHandler(this.element_MouseMove);
    }

    private void element_MouseMove(object sender, MouseEventArgs e)
    {
      this.ElementTree.Control.Cursor = Cursors.Default;
    }

    public override bool IsCompatible(PropertyGridItemBase data, object context)
    {
      PropertyGridItem propertyGridItem = data as PropertyGridItem;
      if (propertyGridItem != null)
        return (object) propertyGridItem.PropertyType == (object) typeof (Color);
      return false;
    }

    public override void Synchronize()
    {
      base.Synchronize();
      PropertyGridItem data = (PropertyGridItem) this.Data;
      if (data.Value == null || data.Value.Equals((object) Color.Empty))
      {
        this.colorBox.BackColor = Color.Empty;
        this.contentElement.Text = " ";
      }
      else
      {
        this.colorBox.BackColor = (Color) data.Value;
        this.contentElement.Text = data.FormattedValue;
      }
    }
  }
}
