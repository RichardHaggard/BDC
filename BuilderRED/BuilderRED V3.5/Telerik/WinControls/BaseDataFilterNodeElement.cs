// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.BaseDataFilterNodeElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.UI;

namespace Telerik.WinControls
{
  public abstract class BaseDataFilterNodeElement : TreeNodeElement
  {
    protected StackLayoutPanel editorsStack;
    private LightVisualElement dragElement;
    private LightVisualButtonElement closeButton;

    public BaseDataFilterNodeElement()
    {
      this.ContentElement.DrawFill = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.ContentElement.DrawText = false;
      this.ContentElement.DrawBorder = false;
      this.ContentElement.DrawFill = true;
      this.ContentElement.GradientStyle = GradientStyles.Solid;
      this.ContentElement.StretchHorizontally = true;
      this.ContentElement.Padding = new Padding(0, 3, 0, 3);
      this.editorsStack = this.CreateStackPanel();
      this.ContentElement.Children.Add((RadElement) this.editorsStack);
      this.dragElement = this.CreateDragElement();
      this.dragElement.Class = "DragElement";
      this.dragElement.Padding = new Padding(4, 0, 4, 0);
      this.editorsStack.Children.Add((RadElement) this.dragElement);
      this.closeButton = this.CreateCloseButton();
      this.closeButton.Class = "CloseButton";
      int num = (int) this.CloseButton.BindProperty(VisualElement.BackColorProperty, (RadObject) this.ContentElement, VisualElement.BackColorProperty, PropertyBindingOptions.OneWay);
      this.closeButton.Click += new EventHandler(this.CloseButton_Click);
      this.Children.Add((RadElement) this.closeButton);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Padding = new Padding(0, 1, 0, 1);
    }

    protected virtual StackLayoutPanel CreateStackPanel()
    {
      return new StackLayoutPanel();
    }

    protected virtual LightVisualElement CreateDragElement()
    {
      return new LightVisualElement();
    }

    protected virtual LightVisualButtonElement CreateCloseButton()
    {
      LightVisualButtonElement visualButtonElement = new LightVisualButtonElement();
      visualButtonElement.DrawBorder = false;
      visualButtonElement.DrawFill = true;
      visualButtonElement.GradientStyle = GradientStyles.Solid;
      visualButtonElement.StretchHorizontally = false;
      visualButtonElement.Padding = new Padding(4, 0, 4, 0);
      return visualButtonElement;
    }

    public DataFilterEditorElement EditingElement { get; set; }

    public LightVisualElement DragElement
    {
      get
      {
        return this.dragElement;
      }
    }

    public LightVisualButtonElement CloseButton
    {
      get
      {
        return this.closeButton;
      }
    }

    public RadDataFilterElement DataFilterElement
    {
      get
      {
        return this.TreeViewElement as RadDataFilterElement;
      }
    }

    public override void Synchronize()
    {
      RadTreeViewElement treeViewElement = this.TreeViewElement;
      this.DragElement.Visibility = treeViewElement.AllowDragDrop ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      this.CloseButton.Visibility = treeViewElement.AllowRemove ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      base.Synchronize();
    }

    public void SetControlCursor(Cursor cursor)
    {
      if (this.ElementTree == null || this.ElementTree.Control == null)
        return;
      this.ElementTree.Control.Cursor = cursor;
    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
      this.OnCloseButtonClick(sender, e);
    }

    protected abstract void OnCloseButtonClick(object sender, EventArgs e);

    public abstract void UpdateDescriptorValue(object value);

    public abstract object GetCurrentEditingElementValue();

    protected internal abstract SizeF GetEditorSize(
      SizeF availableSize,
      DataFilterEditorElement editorElement);
  }
}
