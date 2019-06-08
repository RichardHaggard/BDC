// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridTextElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class PropertyGridTextElement : PropertyGridContentElement
  {
    private StackLayoutElement buttonsLayout;
    private PropertyValueButtonElement propertyValueButton;
    private PropertyGridErrorIndicatorElement errorIndicator;

    public StackLayoutElement ButtonsLayout
    {
      get
      {
        return this.buttonsLayout;
      }
    }

    public PropertyValueButtonElement PropertyValueButton
    {
      get
      {
        return this.propertyValueButton;
      }
    }

    public PropertyGridErrorIndicatorElement ErrorIndicator
    {
      get
      {
        return this.errorIndicator;
      }
    }

    static PropertyGridTextElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new PropertyGridItemElementStateManager(), typeof (PropertyGridTextElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldHandleMouseInput = false;
      this.NotifyParentOnMouseInput = true;
      this.ClipDrawing = true;
      this.ClipText = true;
      this.AutoEllipsis = true;
      this.TextAlignment = ContentAlignment.MiddleLeft;
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.DrawBorder = false;
      this.DrawFill = false;
      this.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.ImageLayout = ImageLayout.None;
      this.ImageAlignment = ContentAlignment.MiddleLeft;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.buttonsLayout = new StackLayoutElement();
      this.buttonsLayout.Alignment = ContentAlignment.MiddleRight;
      this.buttonsLayout.FitInAvailableSize = true;
      this.errorIndicator = new PropertyGridErrorIndicatorElement();
      this.buttonsLayout.Children.Add((RadElement) this.errorIndicator);
      this.propertyValueButton = new PropertyValueButtonElement();
      this.propertyValueButton.Click += new EventHandler(this.propertyValueButton_Click);
      this.buttonsLayout.Children.Add((RadElement) this.propertyValueButton);
      this.Children.Add((RadElement) this.buttonsLayout);
    }

    public virtual void Synchronize()
    {
      this.Text = this.VisualItem.Data.Label;
      this.Image = this.VisualItem.Data.Image;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      return SizeF.Empty;
    }

    private void propertyValueButton_Click(object sender, EventArgs e)
    {
      RadContextMenu elementContextMenu = this.PropertyGridTableElement.GetElementContextMenu(this.VisualItem);
      if (elementContextMenu == null || this.PropertyGridTableElement.IsEditing && !this.PropertyGridTableElement.EndEdit())
        return;
      elementContextMenu.Show(this.ElementTree.Control, this.propertyValueButton.ControlBoundingRectangle.X, this.propertyValueButton.ControlBoundingRectangle.Bottom);
    }
  }
}
