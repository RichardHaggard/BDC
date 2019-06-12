// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridItemElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class PropertyGridItemElement : PropertyGridItemElementBase
  {
    public static RadProperty IsChildItemProperty = RadProperty.Register(nameof (IsChildItem), typeof (bool), typeof (PropertyGridItemElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsModifiedProperty = RadProperty.Register(nameof (IsModified), typeof (bool), typeof (PropertyGridItemElementBase), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsInEditModeProperty = RadProperty.Register(nameof (IsInEditMode), typeof (bool), typeof (PropertyGridItemElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsReadOnlyProperty = RadProperty.Register(nameof (IsReadOnly), typeof (bool), typeof (PropertyGridItemElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HasChildrenProperty = RadProperty.Register("HasChildren", typeof (bool), typeof (PropertyGridItemElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private const int resizePointerOffset = 3;
    private StackLayoutElement stack;
    private PropertyGridItem item;
    private PropertyGridRowHeaderElement headerElement;
    private PropertyGridIndentElement indentElement;
    private PropertyGridExpanderElement expanderElement;
    private PropertyGridTextElement textElement;
    private PropertyGridValueElement valueElement;
    private RadItem editorElement;
    private IInputEditor editor;
    private bool isResizing;
    private Point downLocation;
    private int downWidth;

    static PropertyGridItemElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new PropertyGridItemElementStateManager(), typeof (PropertyGridItemElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.DrawBorder = false;
      this.DrawFill = false;
    }

    protected override void CreateChildElements()
    {
      this.stack = new StackLayoutElement();
      this.stack.FitInAvailableSize = true;
      this.stack.StretchHorizontally = true;
      this.stack.StretchVertically = true;
      this.stack.NotifyParentOnMouseInput = true;
      this.stack.ShouldHandleMouseInput = false;
      this.stack.FitToSizeMode = RadFitToSizeMode.FitToParentBounds;
      this.headerElement = this.CreatePropertyGridRowHeaderElement();
      this.indentElement = this.CreatePropertyGridIndentElement();
      this.expanderElement = this.CreatePropertyGridExpanderElement();
      this.textElement = this.CreatePropertyGridTextElement();
      this.valueElement = this.CreatePropertyGridValueElement();
      this.stack.Children.Add((RadElement) this.headerElement);
      this.stack.Children.Add((RadElement) this.indentElement);
      this.stack.Children.Add((RadElement) this.expanderElement);
      this.stack.Children.Add((RadElement) this.textElement);
      this.stack.Children.Add((RadElement) this.valueElement);
      this.Children.Add((RadElement) this.stack);
    }

    protected virtual PropertyGridRowHeaderElement CreatePropertyGridRowHeaderElement()
    {
      return new PropertyGridRowHeaderElement();
    }

    protected virtual PropertyGridIndentElement CreatePropertyGridIndentElement()
    {
      return new PropertyGridIndentElement();
    }

    protected virtual PropertyGridExpanderElement CreatePropertyGridExpanderElement()
    {
      return new PropertyGridExpanderElement();
    }

    protected virtual PropertyGridTextElement CreatePropertyGridTextElement()
    {
      return new PropertyGridTextElement();
    }

    protected virtual PropertyGridValueElement CreatePropertyGridValueElement()
    {
      return new PropertyGridValueElement();
    }

    public bool IsChildItem
    {
      get
      {
        return (bool) this.GetValue(PropertyGridItemElement.IsChildItemProperty);
      }
      set
      {
        int num = (int) this.SetValue(PropertyGridItemElement.IsChildItemProperty, (object) value);
      }
    }

    public bool IsModified
    {
      get
      {
        return (bool) this.GetValue(PropertyGridItemElement.IsModifiedProperty);
      }
      set
      {
        int num = (int) this.SetValue(PropertyGridItemElement.IsModifiedProperty, (object) value);
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return (bool) this.GetValue(PropertyGridItemElement.IsReadOnlyProperty);
      }
      set
      {
        int num = (int) this.SetValue(PropertyGridItemElement.IsReadOnlyProperty, (object) value);
      }
    }

    public PropertyGridRowHeaderElement HeaderElement
    {
      get
      {
        return this.headerElement;
      }
    }

    public PropertyGridIndentElement IndentElement
    {
      get
      {
        return this.indentElement;
      }
    }

    public PropertyGridExpanderElement ExpanderElement
    {
      get
      {
        return this.expanderElement;
      }
    }

    public virtual PropertyGridContentElement TextElement
    {
      get
      {
        return (PropertyGridContentElement) this.textElement;
      }
    }

    public PropertyGridValueElement ValueElement
    {
      get
      {
        return this.valueElement;
      }
    }

    public virtual bool IsInResizeLocation(Point location)
    {
      if (this.RightToLeft)
      {
        if (location.X >= this.ControlBoundingRectangle.X + this.PropertyTableElement.ValueColumnWidth - 3)
          return location.X <= this.ControlBoundingRectangle.X + this.PropertyTableElement.ValueColumnWidth + 3;
        return false;
      }
      if (location.X >= this.ControlBoundingRectangle.Width - this.PropertyTableElement.ValueColumnWidth - 3)
        return location.X <= this.ControlBoundingRectangle.Width - this.PropertyTableElement.ValueColumnWidth + 3;
      return false;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.IsInResizeLocation(e.Location))
      {
        if (this.PropertyTableElement.IsEditing)
          this.PropertyTableElement.EndEdit();
        this.Capture = true;
        this.isResizing = true;
        this.downLocation = e.Location;
        this.downWidth = this.PropertyTableElement.ValueColumnWidth;
      }
      base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.isResizing)
      {
        int num = e.Location.X - this.downLocation.X;
        if (this.RightToLeft)
          num *= -1;
        this.PropertyTableElement.ValueColumnWidth = this.downWidth - num;
      }
      else
      {
        if (this.IsInResizeLocation(e.Location))
          this.ElementTree.Control.Cursor = Cursors.VSplit;
        else
          this.ElementTree.Control.Cursor = Cursors.Default;
        base.OnMouseMove(e);
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.isResizing)
      {
        this.isResizing = false;
        this.Capture = false;
      }
      base.OnMouseUp(e);
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
      PropertyGridItem propertyGridItem = data as PropertyGridItem;
      if (propertyGridItem == null)
        return;
      this.item = propertyGridItem;
      this.item.PropertyChanged += new PropertyChangedEventHandler(((PropertyGridItemElementBase) this).item_PropertyChanged);
      this.Synchronize();
    }

    public override void Detach()
    {
      this.item.PropertyChanged -= new PropertyChangedEventHandler(((PropertyGridItemElementBase) this).item_PropertyChanged);
      this.item = (PropertyGridItem) null;
    }

    public override void Synchronize()
    {
      PropertyGridItem data = this.Data as PropertyGridItem;
      if (data == null)
        return;
      this.IsSelected = data.Selected;
      this.IsExpanded = data.Expanded;
      this.IsChildItem = data.Level > 0;
      this.ToolTipText = data.ToolTipText;
      this.Enabled = data.Enabled;
      this.IsReadOnly = data.ReadOnly;
      int num1 = (int) this.SetValue(PropertyGridItemElement.HasChildrenProperty, (object) (data.GridItems.Count > 0));
      int num2 = (int) this.SetValue(PropertyGridItemElement.IsReadOnlyProperty, (object) data.ReadOnly);
      bool isModified = data.IsModified;
      this.IsModified = isModified;
      int num3 = (int) this.textElement.PropertyValueButton.SetValue(PropertyValueButtonElement.IsModifiedProperty, (object) isModified);
      int num4 = (int) this.valueElement.SetValue(PropertyGridValueElement.IsModifiedProperty, (object) isModified);
      string errorMessage = data.ErrorMessage;
      this.textElement.ErrorIndicator.ToolTipText = data.ErrorMessage;
      bool flag = !string.IsNullOrEmpty(errorMessage);
      if (flag)
        this.textElement.ErrorIndicator.Visibility = ElementVisibility.Visible;
      else
        this.textElement.ErrorIndicator.Visibility = ElementVisibility.Collapsed;
      int num5 = (int) this.valueElement.SetValue(PropertyGridValueElement.ContainsErrorProperty, (object) flag);
      this.headerElement.Synchronize();
      this.expanderElement.Synchronize();
      this.indentElement.Synchronize();
      this.valueElement.Synchronize();
      this.textElement.Synchronize();
      if (this.IsSelected)
      {
        this.ZIndex = 100;
      }
      else
      {
        int num6 = (int) this.ResetValue(RadElement.ZIndexProperty, ValueResetFlags.Local);
      }
      this.PropertyTableElement.OnItemFormatting(new PropertyGridItemFormattingEventArgs((PropertyGridItemElementBase) this));
    }

    public override bool IsCompatible(PropertyGridItemBase data, object context)
    {
      PropertyGridItem propertyGridItem = data as PropertyGridItem;
      if (propertyGridItem != null && (object) propertyGridItem.PropertyType != (object) typeof (bool) && ((object) propertyGridItem.PropertyType != (object) typeof (bool?) && (object) propertyGridItem.PropertyType != (object) typeof (Telerik.WinControls.Enumerations.ToggleState)))
        return (object) propertyGridItem.PropertyType != (object) typeof (Color);
      return false;
    }

    public bool IsInEditMode
    {
      get
      {
        return this.editor != null;
      }
    }

    public IInputEditor Editor
    {
      get
      {
        return this.editor;
      }
    }

    public virtual void AddEditor(IInputEditor editor)
    {
      if (editor == null || this.editor == editor)
        return;
      this.editor = editor;
      this.editorElement = this.GetEditorElement((IValueEditor) editor);
      if (this.editorElement == null || this.valueElement.Children.Contains((RadElement) this.editorElement))
        return;
      this.valueElement.Children.Add((RadElement) this.editorElement);
      int num = (int) this.SetValue(PropertyGridItemElement.IsInEditModeProperty, (object) true);
      this.Synchronize();
      this.UpdateLayout();
    }

    public virtual void RemoveEditor(IInputEditor editor)
    {
      if (editor == null || this.editor != editor)
        return;
      this.editorElement = this.GetEditorElement((IValueEditor) editor);
      if (this.editorElement != null && this.valueElement.Children.Contains((RadElement) this.editorElement))
      {
        this.valueElement.Children.Remove((RadElement) this.editorElement);
        this.editorElement = (RadItem) null;
      }
      this.editor = (IInputEditor) null;
      int num = (int) this.SetValue(PropertyGridItemElement.IsInEditModeProperty, (object) false);
      this.Synchronize();
    }

    protected RadItem GetEditorElement(IValueEditor editor)
    {
      BaseInputEditor editor1 = this.editor as BaseInputEditor;
      if (editor1 != null)
        return editor1.EditorElement as RadItem;
      return editor as RadItem;
    }
  }
}
