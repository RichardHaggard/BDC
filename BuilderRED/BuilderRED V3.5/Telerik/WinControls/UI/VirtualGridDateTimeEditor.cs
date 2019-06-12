// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridDateTimeEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [RadToolboxItem(false)]
  public class VirtualGridDateTimeEditor : BaseVirtualGridEditor
  {
    private int selectedItemIndex = -1;
    private MaskDateTimeProvider dateTimeMaskHandler;
    private bool isRightmostMaskItemSelected;
    private bool isLeftmostMaskItemSelected;
    private object initialValue;

    public override object Value
    {
      get
      {
        RadDateTimeEditorElement editorElement = (RadDateTimeEditorElement) this.EditorElement;
        if (editorElement != null && editorElement.TextBoxElement.MaskType == MaskType.FreeFormDateTime)
        {
          string text = editorElement.TextBoxElement.Text;
          if (string.IsNullOrEmpty(text))
            return (object) null;
          editorElement.TextBoxElement.Validate(text);
          return editorElement.TextBoxElement.Provider.Value;
        }
        if (editorElement.Value.Equals((object) this.NullValue))
          return (object) null;
        return (object) editorElement.Value;
      }
      set
      {
        RadDateTimePickerElement editorElement = (RadDateTimePickerElement) this.EditorElement;
        if (value == null)
          editorElement.Value = new DateTime?();
        else
          editorElement.Value = new DateTime?((DateTime) value);
      }
    }

    public DateTime NullValue
    {
      get
      {
        return ((RadDateTimePickerElement) this.EditorElement).NullDate;
      }
      set
      {
        ((RadDateTimePickerElement) this.EditorElement).NullDate = value;
      }
    }

    public DateTime MinValue
    {
      get
      {
        return ((RadDateTimePickerElement) this.EditorElement).MinDate;
      }
      set
      {
        ((RadDateTimePickerElement) this.EditorElement).MinDate = value;
      }
    }

    public DateTime MaxValue
    {
      get
      {
        return ((RadDateTimePickerElement) this.EditorElement).MaxDate;
      }
      set
      {
        ((RadDateTimePickerElement) this.EditorElement).MaxDate = value;
      }
    }

    public string CustomFormat
    {
      get
      {
        return ((RadDateTimePickerElement) this.EditorElement).CustomFormat;
      }
      set
      {
        ((RadDateTimePickerElement) this.EditorElement).CustomFormat = value;
      }
    }

    public override System.Type DataType
    {
      get
      {
        return typeof (DateTime);
      }
    }

    public override bool IsModified
    {
      get
      {
        if (this.IsCurrentDateValid())
          return base.IsModified;
        return false;
      }
    }

    public override void BeginEdit()
    {
      this.Value = this.initialValue;
      base.BeginEdit();
      RadDateTimePickerElement editorElement = (RadDateTimePickerElement) this.EditorElement;
      RadTextBoxElement textBoxElement1 = (RadTextBoxElement) editorElement.TextBoxElement;
      RadDateTimePickerCalendar currentBehavior = editorElement.CurrentBehavior as RadDateTimePickerCalendar;
      editorElement.TextBoxElement.TextBoxItem.SelectAll();
      editorElement.TextBoxElement.TextBoxItem.HostedControl.Focus();
      if (currentBehavior != null)
      {
        currentBehavior.DropDownMinSize = new Size(editorElement.Size.Width, 200);
        RadMaskedEditBoxElement textBoxElement2 = editorElement.TextBoxElement;
        MaskDateTimeProvider provider = (MaskDateTimeProvider) textBoxElement2.Provider;
        if (editorElement.TextBoxElement.MaskType != MaskType.FreeFormDateTime)
          provider.SelectFirstEditableItem();
        this.selectedItemIndex = provider.SelectedItemIndex;
        currentBehavior.PopupControl.Opened += new EventHandler(this.PopupControl_Opened);
        currentBehavior.PopupControl.Closed += new RadPopupClosedEventHandler(this.PopupControl_Closed);
        currentBehavior.Calendar.CalendarElement.CalendarStatusElement.ClearButton.Click += new EventHandler(this.ClearButton_Click);
        currentBehavior.Calendar.CalendarElement.CalendarStatusElement.TodayButton.Click += new EventHandler(this.TodayButton_Click);
        DateTime? nullable = editorElement.Value;
        DateTime nullDate = editorElement.NullDate;
        if ((!nullable.HasValue ? 0 : (nullable.GetValueOrDefault() == nullDate ? 1 : 0)) != 0)
          textBoxElement2.Text = string.Empty;
      }
      editorElement.NotifyParentOnMouseInput = false;
      editorElement.ValueChanging += new ValueChangingEventHandler(this.RadDateTimeEditor_ValueChanging);
      editorElement.ValueChanged += new EventHandler(this.RadDateTimeEditor_ValueChanged);
      if (!RadTextBoxEditor.IsDarkTheme(this.OwnerElement))
      {
        int num = (int) editorElement.SetDefaultValueOverride(VisualElement.BackColorProperty, (object) Color.White);
      }
      if (textBoxElement1 != null)
      {
        textBoxElement1.KeyDown += new KeyEventHandler(this.TextBoxElement_KeyDown);
        textBoxElement1.KeyUp += new KeyEventHandler(this.textBoxElement_KeyUp);
      }
      RadControl control = editorElement.ElementTree.Control as RadControl;
      if ((control == null || !(control.ThemeName == "TelerikMetroTouch")) && !(ThemeResolutionService.ApplicationThemeName == "TelerikMetroTouch"))
        return;
      editorElement.CalendarSize = new Size(300, 300);
    }

    public override bool EndEdit()
    {
      base.EndEdit();
      RadDateTimePickerElement editorElement = (RadDateTimePickerElement) this.EditorElement;
      RadTextBoxElement textBoxElement = (RadTextBoxElement) editorElement.TextBoxElement;
      editorElement.ValueChanging -= new ValueChangingEventHandler(this.RadDateTimeEditor_ValueChanging);
      editorElement.ValueChanged -= new EventHandler(this.RadDateTimeEditor_ValueChanged);
      RadDateTimePickerCalendar currentBehavior = editorElement.GetCurrentBehavior() as RadDateTimePickerCalendar;
      if (currentBehavior != null)
      {
        currentBehavior.PopupControl.Opened -= new EventHandler(this.PopupControl_Opened);
        currentBehavior.PopupControl.Closed -= new RadPopupClosedEventHandler(this.PopupControl_Closed);
        currentBehavior.Calendar.CalendarElement.CalendarStatusElement.ClearButton.Click -= new EventHandler(this.ClearButton_Click);
        currentBehavior.Calendar.CalendarElement.CalendarStatusElement.TodayButton.Click -= new EventHandler(this.TodayButton_Click);
      }
      if (textBoxElement != null)
      {
        textBoxElement.KeyDown -= new KeyEventHandler(this.TextBoxElement_KeyDown);
        textBoxElement.KeyUp -= new KeyEventHandler(this.textBoxElement_KeyUp);
      }
      (editorElement.GetCurrentBehavior() as RadDateTimePickerCalendar)?.PopupControl.Hide();
      return true;
    }

    protected override RadElement CreateEditorElement()
    {
      RadDateTimeEditorElement timeEditorElement = new RadDateTimeEditorElement();
      RadDateTimePickerCalendar currentBehavior = timeEditorElement.GetCurrentBehavior() as RadDateTimePickerCalendar;
      if (currentBehavior != null)
      {
        currentBehavior.Calendar.ShowFooter = true;
        timeEditorElement.CalendarSize = new Size(200, 200);
      }
      return (RadElement) timeEditorElement;
    }

    public override void Initialize(object owner, object value)
    {
      this.OwnerElement = owner as RadElement;
      this.originalValue = value;
      this.initialValue = value;
      this.Value = value;
    }

    protected virtual bool IsCurrentDateValid()
    {
      RadDateTimePickerElement editorElement = (RadDateTimePickerElement) this.EditorElement;
      DateTime? nullable1 = editorElement.Value;
      DateTime minValue = this.MinValue;
      if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() < minValue ? 1 : 0) : 0) == 0)
      {
        DateTime? nullable2 = editorElement.Value;
        DateTime maxValue = this.MaxValue;
        if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() > maxValue ? 1 : 0) : 0) == 0)
          return true;
      }
      return false;
    }

    public override void OnKeyDown(KeyEventArgs e)
    {
      RadDateTimePickerElement editorElement = this.EditorElement as RadDateTimePickerElement;
      if (editorElement == null || !editorElement.IsInValidState(true))
        return;
      if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Escape)
        base.OnKeyDown(e);
      else if (e.KeyCode == Keys.Right)
        this.isRightmostMaskItemSelected = this.IsRightmostMaskItemSelected();
      else if (e.KeyCode == Keys.Left)
        this.isLeftmostMaskItemSelected = this.IsLeftmostMaskItemSelected();
      else
        editorElement.NullDate = DateTime.MinValue;
    }

    protected virtual void OnKeyUp(KeyEventArgs e)
    {
      RadDateTimePickerElement editorElement = this.EditorElement as RadDateTimePickerElement;
      if (editorElement == null || !editorElement.IsInValidState(true))
        return;
      bool flag = string.IsNullOrEmpty(editorElement.TextBoxElement.TextBoxItem.HostedControl.Text);
      switch (e.KeyCode)
      {
        case Keys.Left:
          if (!this.isLeftmostMaskItemSelected && !flag)
            break;
          base.OnKeyDown(e);
          this.isLeftmostMaskItemSelected = false;
          break;
        case Keys.Right:
          if (!this.isRightmostMaskItemSelected && !flag)
            break;
          base.OnKeyDown(e);
          this.isRightmostMaskItemSelected = false;
          break;
      }
    }

    private bool IsLeftmostMaskItemSelected()
    {
      if (!this.EnsureDateTimeMaskHandler() || this.dateTimeMaskHandler.SelectedItemIndex != this.selectedItemIndex)
        return false;
      this.selectedItemIndex = -1;
      return true;
    }

    private bool IsRightmostMaskItemSelected()
    {
      if (!this.EnsureDateTimeMaskHandler() || this.dateTimeMaskHandler.SelectedItemIndex != this.dateTimeMaskHandler.List.Count - 1)
        return false;
      this.selectedItemIndex = -1;
      return true;
    }

    private bool EnsureDateTimeMaskHandler()
    {
      RadDateTimePickerElement editorElement = this.EditorElement as RadDateTimePickerElement;
      if (editorElement == null || !(editorElement.CurrentBehavior is RadDateTimePickerCalendar))
        return false;
      RadMaskedEditBoxElement textBoxElement = editorElement.TextBoxElement;
      if (textBoxElement == null || textBoxElement.TextBoxItem.SelectionLength == textBoxElement.Text.Length)
        return false;
      this.dateTimeMaskHandler = textBoxElement.Provider as MaskDateTimeProvider;
      return this.dateTimeMaskHandler != null;
    }

    private void PopupControl_Opened(object sender, EventArgs e)
    {
      RadDateTimePickerCalendar currentBehavior = (this.EditorElement as RadDateTimePickerElement).GetCurrentBehavior() as RadDateTimePickerCalendar;
      if (currentBehavior == null)
        return;
      currentBehavior.Calendar.SelectedDates.Clear();
      currentBehavior.Calendar.InvalidateCalendar();
    }

    private void PopupControl_Closed(object sender, RadPopupClosedEventArgs args)
    {
      (this.EditorElement as RadDateTimePickerElement).TextBoxElement.TextBoxItem.HostedControl.Focus();
    }

    private void TextBoxElement_KeyDown(object sender, KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }

    private void textBoxElement_KeyUp(object sender, KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    private void RadDateTimeEditor_ValueChanged(object sender, EventArgs e)
    {
      this.OnValueChanged();
    }

    private void RadDateTimeEditor_ValueChanging(object sender, ValueChangingEventArgs e)
    {
      if (object.Equals(e.NewValue, e.OldValue))
        return;
      this.OnValueChanging(e);
    }

    private void ClearButton_Click(object sender, EventArgs e)
    {
      (this.EditorElement as RadDateTimeEditorElement).SetToNullValue();
    }

    private void TodayButton_Click(object sender, EventArgs e)
    {
      (this.EditorElement as RadDateTimeEditorElement).Value = new DateTime?(DateTime.Now);
    }
  }
}
