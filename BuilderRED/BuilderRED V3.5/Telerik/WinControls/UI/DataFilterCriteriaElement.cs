// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterCriteriaElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class DataFilterCriteriaElement : BaseDataFilterNodeElement
  {
    public static int DefaultFieldWidth = 100;
    public static int DefaultOperatorWidth = 90;
    public static int DefaultValueWidth = 122;
    private DataFilterFieldEditorElement fieldElement;
    private DataFilterOperatorEditorElement operatorElement;
    private DataFilterValueEditorElement valueElement;
    private float cachedFieldElementWidth;
    private float cachedOperatorElementWidth;
    private float cachedValueElementWidth;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.ContentElement.Class = "DataFilterCriteriaContentElement";
      this.fieldElement = this.CreateFieldElement();
      this.fieldElement.Class = "FieldElement";
      this.fieldElement.EditorType = typeof (TreeViewDropDownListEditor);
      this.editorsStack.Children.Add((RadElement) this.fieldElement);
      this.operatorElement = this.CreateOperatorElement();
      this.operatorElement.Class = "OperatorElement";
      this.operatorElement.EditorType = typeof (TreeViewDropDownListEditor);
      this.editorsStack.Children.Add((RadElement) this.operatorElement);
      this.valueElement = this.CreateValueElement();
      this.valueElement.Class = "ValueElement";
      this.editorsStack.Children.Add((RadElement) this.valueElement);
    }

    protected virtual DataFilterFieldEditorElement CreateFieldElement()
    {
      return new DataFilterFieldEditorElement((BaseDataFilterNodeElement) this);
    }

    protected virtual DataFilterOperatorEditorElement CreateOperatorElement()
    {
      return new DataFilterOperatorEditorElement((BaseDataFilterNodeElement) this);
    }

    protected virtual DataFilterValueEditorElement CreateValueElement()
    {
      return new DataFilterValueEditorElement((BaseDataFilterNodeElement) this);
    }

    public DataFilterCriteriaNode CriteriaNode
    {
      get
      {
        return this.Data as DataFilterCriteriaNode;
      }
    }

    public DataFilterEditorElement FieldElement
    {
      get
      {
        return (DataFilterEditorElement) this.fieldElement;
      }
    }

    public DataFilterEditorElement OperatorElement
    {
      get
      {
        return (DataFilterEditorElement) this.operatorElement;
      }
    }

    public DataFilterEditorElement ValueElement
    {
      get
      {
        return (DataFilterEditorElement) this.valueElement;
      }
    }

    public override void AddEditor(IInputEditor editor)
    {
      if (editor == null || this.EditingElement == null)
        return;
      this.EditingElement.AddEditor(editor);
      this.PopulateEditorItems();
      this.Data.RootNode.TreeViewElement.ViewElement.UpdateItems();
      this.UpdateLayout();
    }

    public override void RemoveEditor(IInputEditor editor)
    {
      if (this.EditingElement == null || this.EditingElement.Editor == null)
        return;
      this.RemoveEditorItems();
      this.EditingElement.RemoveEditor();
      this.EditingElement = (DataFilterEditorElement) null;
      if (this.Data == null)
        return;
      this.Data.RootNode.TreeViewElement.ViewElement.UpdateItems();
      this.UpdateLayout();
      this.Synchronize();
    }

    public override void Synchronize()
    {
      if (!this.IsInValidState(true))
        return;
      DataFilterCriteriaNode criteriaNode = this.CriteriaNode;
      if (criteriaNode.Descriptor != null)
      {
        this.FieldElement.Synchronize(criteriaNode);
        this.OperatorElement.Synchronize(criteriaNode);
        this.ValueElement.Synchronize(criteriaNode);
        if (!DataFilterOperatorContext.IsEditableFilterOperator(criteriaNode.FilterOperator))
          this.ValueElement.Visibility = ElementVisibility.Hidden;
        else
          this.ValueElement.Visibility = ElementVisibility.Visible;
      }
      base.Synchronize();
    }

    public string GetValue()
    {
      return this.fieldElement.Text + " " + this.operatorElement.Text + " " + this.valueElement.Text;
    }

    public override object GetCurrentEditingElementValue()
    {
      if (this.EditingElement == this.fieldElement)
        return (object) this.CriteriaNode.PropertyName;
      if (this.EditingElement == this.operatorElement)
        return (object) this.CriteriaNode.FilterOperator;
      if (this.EditingElement == this.valueElement)
        return this.CriteriaNode.DescriptorValue;
      return (object) null;
    }

    public override void UpdateDescriptorValue(object value)
    {
      if (this.EditingElement == this.FieldElement)
      {
        value = value ?? (object) string.Empty;
        this.CriteriaNode.PropertyName = value.ToString();
      }
      else if (this.EditingElement == this.OperatorElement)
      {
        value = value ?? (object) string.Empty;
        this.CriteriaNode.FilterOperator = this.CriteriaNode.DescriptorItem.GetFilterOperatorByText(value.ToString());
      }
      else
      {
        if (this.EditingElement != this.ValueElement)
          return;
        this.CriteriaNode.DescriptorValue = value;
      }
    }

    public override bool IsCompatible(RadTreeNode data, object context)
    {
      if (!base.IsCompatible(data, context))
        return false;
      return data is DataFilterCriteriaNode;
    }

    private void PopulateEditorItems()
    {
      if (this.EditingElement == null)
        return;
      if (this.EditingElement == this.FieldElement)
        this.DataFilterElement.InitializeFieldEditor(this.FieldElement.Editor, this);
      else if (this.EditingElement == this.OperatorElement)
      {
        this.DataFilterElement.InitializeOperatorEditor(this.OperatorElement.Editor, this);
      }
      else
      {
        if (this.EditingElement != this.ValueElement)
          return;
        this.DataFilterElement.InitializeValueEditor(this.ValueElement.Editor, this);
      }
    }

    private void RemoveEditorItems()
    {
      if (this.CriteriaNode == null)
        return;
      this.CriteriaNode.EditorValue = (object) null;
    }

    protected override void OnCloseButtonClick(object sender, EventArgs e)
    {
      this.DataFilterElement.RemoveChildNode(this.Data);
    }

    protected internal override SizeF GetEditorSize(
      SizeF availableSize,
      DataFilterEditorElement editorElement)
    {
      SizeF sizeF = availableSize;
      bool isEditing = this.TreeViewElement.IsEditing;
      if (editorElement == this.fieldElement)
      {
        sizeF.Width = !isEditing || (double) this.cachedFieldElementWidth <= 0.0 ? Math.Max((float) TelerikDpiHelper.ScaleInt(DataFilterCriteriaElement.DefaultFieldWidth, this.DpiScaleFactor), availableSize.Width) : this.cachedFieldElementWidth;
        this.cachedFieldElementWidth = sizeF.Width;
      }
      else if (editorElement == this.operatorElement)
      {
        sizeF.Width = !isEditing || (double) this.cachedOperatorElementWidth <= 0.0 ? Math.Max((float) TelerikDpiHelper.ScaleInt(DataFilterCriteriaElement.DefaultOperatorWidth, this.DpiScaleFactor), availableSize.Width) : this.cachedOperatorElementWidth;
        this.cachedOperatorElementWidth = sizeF.Width;
      }
      else if (editorElement == this.valueElement)
      {
        sizeF.Width = !isEditing || (double) this.cachedValueElementWidth <= 0.0 ? Math.Max((float) TelerikDpiHelper.ScaleInt(DataFilterCriteriaElement.DefaultValueWidth, this.DpiScaleFactor), availableSize.Width) : this.cachedValueElementWidth;
        this.cachedValueElementWidth = sizeF.Width;
      }
      return sizeF;
    }
  }
}
