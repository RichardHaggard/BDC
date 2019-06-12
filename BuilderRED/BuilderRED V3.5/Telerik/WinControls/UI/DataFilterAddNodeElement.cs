// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterAddNodeElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class DataFilterAddNodeElement : TreeNodeElement
  {
    private RadDropDownButtonElement dropDownButton;
    private RadMenuItem addGroupItem;
    private RadMenuItem addCriteriaItem;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.Children.Remove((RadElement) this.ContentElement);
      this.dropDownButton = this.CreateDropDownButton();
      this.dropDownButton.Text = LocalizationProvider<DataFilterLocalizationProvider>.CurrentProvider.GetLocalizedString("AddNewButtonText");
      this.Children.Add((RadElement) this.dropDownButton);
      this.addGroupItem = this.CreateMenuItem();
      this.addGroupItem.Text = LocalizationProvider<DataFilterLocalizationProvider>.CurrentProvider.GetLocalizedString("AddNewButtonGroup");
      this.addGroupItem.Click += new EventHandler(this.AddGroupItem_Click);
      this.dropDownButton.Items.Add((RadItem) this.addGroupItem);
      this.addCriteriaItem = this.CreateMenuItem();
      this.addCriteriaItem.Text = LocalizationProvider<DataFilterLocalizationProvider>.CurrentProvider.GetLocalizedString("AddNewButtonExpression");
      this.addCriteriaItem.Click += new EventHandler(this.AddCriteriaItem_Click);
      this.dropDownButton.Items.Add((RadItem) this.addCriteriaItem);
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.DropDownButton.ActionButton.Padding = new Padding(0, 1, 0, 1);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Padding = new Padding(0, 2, 0, 2);
    }

    protected virtual RadDropDownButtonElement CreateDropDownButton()
    {
      RadDropDownButtonElement downButtonElement = new RadDropDownButtonElement();
      downButtonElement.StretchHorizontally = false;
      downButtonElement.StretchVertically = false;
      downButtonElement.MinSize = new Size(54, 0);
      return downButtonElement;
    }

    protected virtual RadMenuItem CreateMenuItem()
    {
      return new RadMenuItem();
    }

    public RadDropDownButtonElement DropDownButton
    {
      get
      {
        return this.dropDownButton;
      }
    }

    private void AddGroupItem_Click(object sender, EventArgs e)
    {
      this.AddNewNode((FilterDescriptor) new CompositeFilterDescriptor());
    }

    private void AddCriteriaItem_Click(object sender, EventArgs e)
    {
      this.AddNewNode(new FilterDescriptor("", ~FilterOperator.None, (object) null));
    }

    private void AddNewNode(FilterDescriptor descriptor)
    {
      DataFilterGroupNode groupNode = this.FindGroupNode();
      if (groupNode == null)
        return;
      RadDataFilterElement treeViewElement = this.TreeViewElement as RadDataFilterElement;
      if (!treeViewElement.ValidateAddNewNode())
        return;
      treeViewElement.AddChildNodes(descriptor, (RadTreeNode) groupNode);
    }

    private DataFilterGroupNode FindGroupNode()
    {
      return (this.Data as DataFilterAddNode)?.AssociatedGroupNode;
    }

    public override bool IsCompatible(RadTreeNode data, object context)
    {
      if (!base.IsCompatible(data, context))
        return false;
      return data is DataFilterAddNode;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      RectangleF boundingRectangle = (RectangleF) this.DropDownButton.BoundingRectangle;
      boundingRectangle.Y = clientRectangle.Y + (float) (((double) clientRectangle.Height - (double) this.DropDownButton.DesiredSize.Height) / 2.0);
      this.DropDownButton.Arrange(boundingRectangle);
      return sizeF;
    }
  }
}
