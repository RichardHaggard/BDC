// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterGroupElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Data;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.StateManagers;

namespace Telerik.WinControls.UI
{
  public class DataFilterGroupElement : BaseDataFilterNodeElement
  {
    public static RadProperty LogicalOperatorProperty = RadProperty.Register(nameof (LogicalOperator), typeof (FilterLogicalOperator), typeof (DataFilterGroupElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) FilterLogicalOperator.And, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static int DefaultLogicalOperatorWidth = 20;
    private DataFilterLogicalOperatorEditorElement logicalOperatorElement;
    private LightVisualElement textElement;
    private float cachedLogicalOperatorElementWidth;

    static DataFilterGroupElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new DataFilterGroupElementStateManager(), typeof (DataFilterGroupElement));
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.ContentElement.Class = "DataFilterGroupContentElement";
      int num1 = (int) this.ContentElement.BindProperty(DataFilterGroupNodeContentElement.LogicalOperatorProperty, (RadObject) this, DataFilterGroupElement.LogicalOperatorProperty, PropertyBindingOptions.OneWay);
      this.logicalOperatorElement = this.CreateLogicalOperatorElement();
      this.logicalOperatorElement.Class = "LogicalOperator";
      this.logicalOperatorElement.EditorType = typeof (TreeViewDropDownListEditor);
      this.editorsStack.Children.Add((RadElement) this.logicalOperatorElement);
      this.textElement = this.CreateTextElement();
      this.textElement.Class = "TextElement";
      this.textElement.Text = LocalizationProvider<DataFilterLocalizationProvider>.CurrentProvider.GetLocalizedString("LogicalOperatorDescription");
      this.editorsStack.Children.Add((RadElement) this.textElement);
      int num2 = (int) this.CloseButton.BindProperty(VisualElement.BackColorProperty, (RadObject) this.ContentElement, VisualElement.BackColorProperty, PropertyBindingOptions.OneWay);
    }

    protected override TreeNodeContentElement CreateContentElement()
    {
      return (TreeNodeContentElement) new DataFilterGroupNodeContentElement();
    }

    protected virtual DataFilterLogicalOperatorEditorElement CreateLogicalOperatorElement()
    {
      return new DataFilterLogicalOperatorEditorElement((BaseDataFilterNodeElement) this);
    }

    protected virtual LightVisualElement CreateTextElement()
    {
      return new LightVisualElement();
    }

    public DataFilterGroupNode GroupNode
    {
      get
      {
        return this.Data as DataFilterGroupNode;
      }
    }

    public DataFilterEditorElement LogicalOperatorElement
    {
      get
      {
        return (DataFilterEditorElement) this.logicalOperatorElement;
      }
    }

    public LightVisualElement TextElement
    {
      get
      {
        return this.textElement;
      }
    }

    public FilterLogicalOperator LogicalOperator
    {
      get
      {
        return (FilterLogicalOperator) this.GetValue(DataFilterGroupElement.LogicalOperatorProperty);
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
      this.Data.RootNode.TreeViewElement.ViewElement.UpdateItems();
      this.UpdateLayout();
      this.Synchronize();
    }

    private void RemoveEditorItems()
    {
      if (this.EditingElement == null || this.EditingElement.Editor == null)
        return;
      BaseDropDownListEditor editor = this.logicalOperatorElement.Editor as BaseDropDownListEditor;
      if (editor == null)
        return;
      RadDropDownListElement editorElement = editor.EditorElement as RadDropDownListElement;
      if (editorElement == null)
        return;
      editorElement.Visibility = ElementVisibility.Visible;
    }

    public override void Synchronize()
    {
      base.Synchronize();
      if (!this.IsInValidState(true))
        return;
      DataFilterGroupNode groupNode1 = this.GroupNode;
      DataFilterRootNode groupNode2 = this.GroupNode as DataFilterRootNode;
      if ((groupNode1 == null || groupNode1.CompositeDescriptor == null) && (groupNode2 == null || groupNode2.Filters == null))
        return;
      this.logicalOperatorElement.Text = this.GetOperatorText(groupNode1.LogicalOperator);
      int num = (int) this.SetValue(DataFilterGroupElement.LogicalOperatorProperty, (object) groupNode1.LogicalOperator);
    }

    protected virtual string GetOperatorText(FilterLogicalOperator logicalOperator)
    {
      string str = string.Empty;
      switch (logicalOperator)
      {
        case FilterLogicalOperator.And:
          str = LocalizationProvider<DataFilterLocalizationProvider>.CurrentProvider.GetLocalizedString("LogicalOperatorAnd");
          break;
        case FilterLogicalOperator.Or:
          str = LocalizationProvider<DataFilterLocalizationProvider>.CurrentProvider.GetLocalizedString("LogicalOperatorOr");
          break;
      }
      return str;
    }

    public override void UpdateDescriptorValue(object value)
    {
      if (this.EditingElement != this.logicalOperatorElement)
        return;
      value = value ?? (object) string.Empty;
      this.GroupNode.LogicalOperator = this.GetLogicalOperatorFormText(value.ToString());
    }

    private FilterLogicalOperator GetLogicalOperatorFormText(string text)
    {
      return !(text == LocalizationProvider<DataFilterLocalizationProvider>.CurrentProvider.GetLocalizedString("LogicalOperatorOr")) ? (!(text == LocalizationProvider<DataFilterLocalizationProvider>.CurrentProvider.GetLocalizedString("LogicalOperatorAnd")) ? FilterLogicalOperator.And : FilterLogicalOperator.And) : FilterLogicalOperator.Or;
    }

    public override object GetCurrentEditingElementValue()
    {
      return (object) this.GroupNode.LogicalOperator;
    }

    public override bool IsCompatible(RadTreeNode data, object context)
    {
      if (!base.IsCompatible(data, context))
        return false;
      return data is DataFilterGroupNode;
    }

    protected internal override SizeF GetEditorSize(
      SizeF availableSize,
      DataFilterEditorElement editorElement)
    {
      SizeF sizeF = availableSize;
      bool isEditing = this.TreeViewElement.IsEditing;
      if (editorElement == this.logicalOperatorElement)
      {
        sizeF.Width = !isEditing || (double) this.cachedLogicalOperatorElementWidth <= 0.0 ? Math.Max((float) TelerikDpiHelper.ScaleInt(DataFilterGroupElement.DefaultLogicalOperatorWidth, this.DpiScaleFactor), availableSize.Width) : this.cachedLogicalOperatorElementWidth;
        this.cachedLogicalOperatorElementWidth = sizeF.Width;
      }
      return sizeF;
    }

    private void PopulateEditorItems()
    {
      BaseDropDownListEditor editor = this.logicalOperatorElement.Editor as BaseDropDownListEditor;
      if (editor == null)
        return;
      RadDropDownListElement editorElement = editor.EditorElement as RadDropDownListElement;
      DataFilterGroupNode groupNode = this.GroupNode;
      if (editorElement == null || groupNode == null)
        return;
      editorElement.BeginUpdate();
      editorElement.Items.Clear();
      int num1 = 0;
      int num2 = -1;
      foreach (FilterLogicalOperator logicalOperator in Enum.GetValues(typeof (FilterLogicalOperator)))
      {
        RadListDataItem radListDataItem = new RadListDataItem(this.GetOperatorText(logicalOperator), (object) logicalOperator);
        editorElement.Items.Add(radListDataItem);
        if (object.Equals((object) logicalOperator, (object) groupNode.LogicalOperator))
          num2 = num1;
        ++num1;
      }
      editorElement.DropDownWidth = 110;
      editorElement.SelectedIndex = num2;
      editorElement.EndUpdate();
      editorElement.SelectedValue = (object) groupNode.LogicalOperator;
      editorElement.Visibility = ElementVisibility.Hidden;
    }

    protected internal virtual void OpenEditor()
    {
      BaseDropDownListEditor editor = this.logicalOperatorElement.Editor as BaseDropDownListEditor;
      if (editor == null)
        return;
      (editor.EditorElement as RadDropDownListElement)?.ShowPopup();
    }

    protected override void OnCloseButtonClick(object sender, EventArgs e)
    {
      this.DataFilterElement.ClearChildNodes(this.Data as DataFilterGroupNode);
    }

    protected override void OnNodePropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnNodePropertyChanged(e);
      if (!(e.PropertyName == "LogicalOperator"))
        return;
      int num = (int) this.SetValue(DataFilterGroupElement.LogicalOperatorProperty, (object) this.GroupNode.LogicalOperator);
      this.Synchronize();
    }
  }
}
