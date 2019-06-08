// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridUITypeEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Threading;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class PropertyGridUITypeEditorElement : RadTextBoxElement
  {
    private Type editedType;
    private TypeConverter converter;
    private UITypeEditor editor;
    private object currentValue;
    private RadButtonElement editorButton;
    private PropertyGridUITypeEditor editorService;

    public RadButtonElement Button
    {
      get
      {
        return this.editorButton;
      }
    }

    public virtual object Value
    {
      get
      {
        if (this.Validate())
          return this.GetValue();
        return this.currentValue;
      }
      set
      {
        PropertyGridItem propertyGridItem = (PropertyGridItem) null;
        PropertyGridItemElement ancestor = this.FindAncestor<PropertyGridItemElement>();
        if (ancestor != null)
          propertyGridItem = ancestor.Data as PropertyGridItem;
        if (value is string && this.Converter.CanConvertFrom((ITypeDescriptorContext) propertyGridItem, typeof (string)))
        {
          this.Text = value as string;
          this.currentValue = this.Converter.ConvertFromString((ITypeDescriptorContext) propertyGridItem, Thread.CurrentThread.CurrentCulture, value as string);
        }
        else
        {
          if (this.Converter.CanConvertTo((ITypeDescriptorContext) propertyGridItem, typeof (string)))
            this.Text = this.Converter.ConvertToString((ITypeDescriptorContext) propertyGridItem, Thread.CurrentThread.CurrentCulture, value);
          this.currentValue = value;
        }
      }
    }

    public UITypeEditor Editor
    {
      get
      {
        return this.editor;
      }
      set
      {
        this.editor = value;
      }
    }

    public TypeConverter Converter
    {
      get
      {
        return this.converter;
      }
      set
      {
        this.converter = value;
      }
    }

    public Type EditedType
    {
      get
      {
        return this.editedType;
      }
      set
      {
        this.editedType = value;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.editorButton = (RadButtonElement) new PropertyGridUITypeEditorButtonElement();
      int num = (int) this.editorButton.SetValue(DockLayoutPanel.DockProperty, (object) Dock.Right);
      this.editorButton.Click += new EventHandler(this.editorButton_Click);
      this.editorService = new PropertyGridUITypeEditor(this);
    }

    public PropertyGridUITypeEditorElement(Type editedType)
    {
      this.EditedType = editedType;
      DockLayoutPanel dockLayoutPanel = new DockLayoutPanel();
      dockLayoutPanel.LastChildFill = true;
      this.Children.Remove((RadElement) this.TextBoxItem);
      this.TextBoxItem.Alignment = ContentAlignment.MiddleLeft;
      dockLayoutPanel.Children.Add((RadElement) this.editorButton);
      dockLayoutPanel.Children.Add((RadElement) this.TextBoxItem);
      this.Children.Add((RadElement) dockLayoutPanel);
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.editorButton.Click -= new EventHandler(this.editorButton_Click);
    }

    protected virtual void DropEditor()
    {
      UITypeEditor editor = this.Editor;
      if (editor == null)
        return;
      try
      {
        PropertyGridItemElement ancestor1 = this.FindAncestor<PropertyGridItemElement>();
        if (ancestor1 == null)
          return;
        PropertyGridItem data = ancestor1.Data as PropertyGridItem;
        if (data == null)
          return;
        this.currentValue = !this.Validate() ? data.Value : this.Converter.ConvertFromString((ITypeDescriptorContext) data, Thread.CurrentThread.CurrentCulture, this.Text);
        object obj = editor.EditValue((ITypeDescriptorContext) data, (IServiceProvider) this.editorService, this.currentValue);
        bool expanded = data.Expanded;
        PropertyGridTableElement ancestor2 = this.FindAncestor<PropertyGridTableElement>();
        if (data.Expandable)
        {
          data.Collapse();
          ancestor1.UpdateLayout();
        }
        this.Value = obj;
        this.Text = this.Converter.ConvertToString((ITypeDescriptorContext) data, Thread.CurrentThread.CurrentCulture, obj);
        ancestor2?.EndEdit();
        data.Expanded = expanded;
      }
      catch (Exception ex)
      {
      }
    }

    protected virtual void OnEditorButtonClick()
    {
      this.DropEditor();
    }

    protected virtual bool ShouldCloseEditor(object oldValue, object newValue)
    {
      if (oldValue == null && newValue == null)
        return false;
      if (oldValue == null && newValue != null)
        return (object) this.editedType != (object) newValue.GetType();
      if (oldValue != null && newValue != null)
        return (object) oldValue.GetType() != (object) newValue.GetType();
      return true;
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadBrowseEditorElement);
      }
    }

    public void SelectTextBox()
    {
      this.TextBoxItem.SelectAll();
    }

    public virtual bool Validate()
    {
      try
      {
        this.GetValue();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    protected virtual object GetValue()
    {
      PropertyGridItem propertyGridItem = (PropertyGridItem) null;
      PropertyGridItemElement ancestor = this.FindAncestor<PropertyGridItemElement>();
      if (ancestor != null)
        propertyGridItem = ancestor.Data as PropertyGridItem;
      return this.Converter.ConvertFromString((ITypeDescriptorContext) propertyGridItem, Thread.CurrentThread.CurrentCulture, this.Text);
    }

    private void editorButton_Click(object sender, EventArgs e)
    {
      this.OnEditorButtonClick();
    }
  }
}
