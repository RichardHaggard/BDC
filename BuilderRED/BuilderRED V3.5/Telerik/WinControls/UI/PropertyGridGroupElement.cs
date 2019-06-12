// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridGroupElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class PropertyGridGroupElement : PropertyGridItemElementBase
  {
    private PropertyGridGroupItem item;
    private StackLayoutElement stack;
    private PropertyGridGroupExpanderElement expanderElement;
    private PropertyGridGroupTextElement textElement;

    protected override void CreateChildElements()
    {
      this.textElement = new PropertyGridGroupTextElement();
      this.expanderElement = new PropertyGridGroupExpanderElement();
      this.stack = new StackLayoutElement();
      this.stack.FitInAvailableSize = true;
      this.stack.StretchHorizontally = true;
      this.stack.StretchVertically = true;
      this.stack.NotifyParentOnMouseInput = true;
      this.stack.ShouldHandleMouseInput = false;
      this.stack.FitToSizeMode = RadFitToSizeMode.FitToParentBounds;
      this.textElement.DrawBorder = true;
      this.textElement.DrawFill = true;
      this.textElement.TextAlignment = ContentAlignment.MiddleLeft;
      this.expanderElement.ExpanderItem.Class = "PropertyGridGroupExpanderItem";
      this.stack.Children.Add((RadElement) this.expanderElement);
      this.stack.Children.Add((RadElement) this.textElement);
      this.Children.Add((RadElement) this.stack);
    }

    public PropertyGridGroupExpanderElement ExpanderElement
    {
      get
      {
        return this.expanderElement;
      }
    }

    public PropertyGridGroupTextElement TextElement
    {
      get
      {
        return this.textElement;
      }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.ElementTree.Control.Cursor = Cursors.Default;
    }

    public override PropertyGridItemBase Data
    {
      get
      {
        return (PropertyGridItemBase) this.item;
      }
    }

    public override void Attach(PropertyGridItemBase data, object context)
    {
      PropertyGridGroupItem propertyGridGroupItem = data as PropertyGridGroupItem;
      if (propertyGridGroupItem == null)
        return;
      this.item = propertyGridGroupItem;
      this.textElement.Text = data.Label;
      this.item.PropertyChanged += new PropertyChangedEventHandler(((PropertyGridItemElementBase) this).item_PropertyChanged);
      this.Synchronize();
    }

    public override void Detach()
    {
      this.item.PropertyChanged -= new PropertyChangedEventHandler(((PropertyGridItemElementBase) this).item_PropertyChanged);
      this.item = (PropertyGridGroupItem) null;
    }

    public override void Synchronize()
    {
      this.IsSelected = this.Data.Selected;
      this.IsExpanded = this.Data.Expanded;
      this.ToolTipText = this.Data.ToolTipText;
      this.expanderElement.Synchronize();
      this.textElement.Synchronize();
      this.PropertyTableElement.OnItemFormatting(new PropertyGridItemFormattingEventArgs((PropertyGridItemElementBase) this));
    }

    public override bool IsCompatible(PropertyGridItemBase data, object context)
    {
      return data is PropertyGridGroupItem;
    }
  }
}
