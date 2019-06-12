// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridSpinEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class PropertyGridSpinEditor : BaseSpinEditor
  {
    public override object Value
    {
      get
      {
        return this.GetEditorValue();
      }
      set
      {
        this.SetEditorValue(value);
      }
    }

    private object GetEditorValue()
    {
      RadSpinElement editorElement = this.EditorElement as RadSpinElement;
      editorElement.Validate();
      if (string.IsNullOrEmpty(editorElement.Text))
        return (object) null;
      return (object) editorElement.Value;
    }

    private void SetEditorValue(object value)
    {
      RadSpinElement editorElement = this.EditorElement as RadSpinElement;
      if (value == null)
      {
        editorElement.Text = string.Empty;
      }
      else
      {
        Decimal num = Convert.ToDecimal(value);
        if (num < editorElement.MinValue)
          num = editorElement.MinValue;
        else if (num > editorElement.MaxValue)
          num = editorElement.MaxValue;
        editorElement.Value = num;
      }
    }

    public override bool IsModified
    {
      get
      {
        if (this.originalValue == null || this.originalValue == DBNull.Value)
        {
          if (this.Value != null)
            return this.Value != DBNull.Value;
          return false;
        }
        if (this.Value != null && this.Value != DBNull.Value)
          return !Convert.ToDecimal(this.originalValue).Equals(Convert.ToDecimal(this.Value));
        if (this.originalValue != null)
          return this.originalValue != DBNull.Value;
        return false;
      }
    }

    public override void Initialize(object owner, object value)
    {
      PropertyGridItem data = (owner as PropertyGridItemElement).Data as PropertyGridItem;
      System.Type propertyType = data.PropertyType;
      if ((object) propertyType == (object) typeof (Decimal) || (object) propertyType == (object) typeof (double) || ((object) propertyType == (object) typeof (float) || (object) propertyType == (object) typeof (Decimal?)) || ((object) propertyType == (object) typeof (double?) || (object) propertyType == (object) typeof (float?)))
        ((RadSpinElement) this.EditorElement).DecimalPlaces = 2;
      else
        ((RadSpinElement) this.EditorElement).DecimalPlaces = 0;
      if ((object) Nullable.GetUnderlyingType(propertyType) != null)
        ((RadSpinElement) this.EditorElement).EnableNullValueInput = true;
      else
        ((RadSpinElement) this.EditorElement).EnableNullValueInput = false;
      base.Initialize(owner, value);
      this.SetMinAndMaxValue(data);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      BaseSpinEditorElement editorElement = this.EditorElement as BaseSpinEditorElement;
      PropertyGridItemElement ownerElement = this.OwnerElement as PropertyGridItemElement;
      if (ownerElement == null)
        return;
      switch (e.KeyCode)
      {
        case Keys.Return:
          if (e.Modifiers == Keys.Control)
            break;
          editorElement.Validate();
          ownerElement.PropertyTableElement.EndEdit();
          break;
        case Keys.Escape:
          ownerElement.Data.PropertyGridTableElement.CancelEdit();
          break;
        case Keys.Delete:
          if (this.selectionLength != editorElement.TextBoxItem.TextLength)
            break;
          editorElement.Text = (string) null;
          break;
      }
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      BaseSpinEditorElement editorElement = this.EditorElement as BaseSpinEditorElement;
      if (!(this.OwnerElement is PropertyGridItemElement))
        return;
      switch (e.KeyCode)
      {
        case Keys.Left:
          if ((!this.RightToLeft || this.selectionStart != editorElement.Text.Length) && (this.RightToLeft || this.selectionStart != 0 || this.selectionLength != 0))
            break;
          editorElement.Validate();
          break;
        case Keys.Right:
          if ((!this.RightToLeft || this.selectionStart != 0) && (this.RightToLeft || this.selectionStart != editorElement.Text.Length))
            break;
          editorElement.Validate();
          break;
      }
    }

    public override void OnLostFocus()
    {
      base.OnLostFocus();
      PropertyGridItemElement ownerElement = this.OwnerElement as PropertyGridItemElement;
      if (ownerElement == null || !ownerElement.IsInValidState(true) || (ownerElement.ElementTree.Control.Focused || ownerElement.ElementTree.Control.ContainsFocus))
        return;
      ownerElement.PropertyTableElement.EndEdit();
    }

    protected virtual void SetMinAndMaxValue(PropertyGridItem item)
    {
      RadRangeAttribute attribute = item.Attributes[typeof (RadRangeAttribute)] as RadRangeAttribute;
      if (attribute != null)
      {
        this.MinValue = (Decimal) attribute.MinValue;
        this.MaxValue = (Decimal) attribute.MaxValue;
      }
      else
      {
        switch (System.Type.GetTypeCode(item.PropertyType))
        {
          case TypeCode.SByte:
            this.MinValue = new Decimal((int) sbyte.MinValue);
            this.MaxValue = new Decimal((int) sbyte.MaxValue);
            break;
          case TypeCode.Byte:
            this.MinValue = new Decimal(0);
            this.MaxValue = new Decimal((int) byte.MaxValue);
            break;
          case TypeCode.Int16:
            this.MinValue = new Decimal((int) short.MinValue);
            this.MaxValue = new Decimal((int) short.MaxValue);
            break;
          case TypeCode.UInt16:
            this.MinValue = new Decimal(0);
            this.MaxValue = new Decimal((int) ushort.MaxValue);
            break;
          case TypeCode.Int32:
            this.MinValue = new Decimal(int.MinValue);
            this.MaxValue = new Decimal(int.MaxValue);
            break;
          case TypeCode.UInt32:
            this.MinValue = new Decimal(0);
            this.MaxValue = new Decimal((long) uint.MaxValue);
            break;
          case TypeCode.Int64:
            this.MinValue = new Decimal(long.MinValue);
            this.MaxValue = new Decimal(long.MaxValue);
            break;
          case TypeCode.UInt64:
            this.MinValue = new Decimal(0);
            this.MaxValue = new Decimal(-1, -1, 0, false, (byte) 0);
            break;
          case TypeCode.Single:
            this.MinValue = new Decimal(-1, -1, -1, true, (byte) 0);
            this.MaxValue = new Decimal(-1, -1, -1, false, (byte) 0);
            break;
          case TypeCode.Double:
            this.MinValue = new Decimal(-1, -1, -1, true, (byte) 0);
            this.MaxValue = new Decimal(-1, -1, -1, false, (byte) 0);
            break;
          case TypeCode.Decimal:
            this.MinValue = new Decimal(-1, -1, -1, true, (byte) 0);
            this.MaxValue = new Decimal(-1, -1, -1, false, (byte) 0);
            break;
        }
      }
    }

    public override void OnValueChanging(ValueChangingEventArgs e)
    {
      base.OnValueChanging(e);
      (this.OwnerElement as PropertyGridItemElement)?.PropertyTableElement.OnValueChanging((object) this, e);
    }

    public override void OnValueChanged()
    {
      base.OnValueChanged();
      (this.OwnerElement as PropertyGridItemElement)?.PropertyTableElement.OnValueChanged((object) this, EventArgs.Empty);
    }
  }
}
