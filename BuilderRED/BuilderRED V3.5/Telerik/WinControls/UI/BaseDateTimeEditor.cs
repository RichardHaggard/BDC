// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseDateTimeEditor
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
  public class BaseDateTimeEditor : BaseInputEditor
  {
    private int selectedItemIndex = -1;
    private MaskDateTimeProvider dateTimeMaskHandler;
    private bool isRightmostMaskItemSelected;
    private bool isLeftmostMaskItemSelected;
    private bool popupOpened;

    public override object Value
    {
      get
      {
        RadDateTimePickerElement editorElement = (RadDateTimePickerElement) this.EditorElement;
        if (editorElement.Value.Equals((object) this.NullValue))
          return (object) null;
        return (object) editorElement.Value;
      }
      set
      {
        RadDateTimePickerElement editorElement = (RadDateTimePickerElement) this.EditorElement;
        if (value == null)
        {
          editorElement.SetToNullValue();
        }
        else
        {
          try
          {
            editorElement.Value = new DateTime?(Convert.ToDateTime(value));
          }
          catch
          {
            editorElement.SetToNullValue();
          }
        }
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
      base.BeginEdit();
      RadDateTimePickerElement editorElement = (RadDateTimePickerElement) this.EditorElement;
      RadTextBoxElement textBoxElement1 = (RadTextBoxElement) editorElement.TextBoxElement;
      RadDateTimePickerCalendar currentBehavior = editorElement.CurrentBehavior as RadDateTimePickerCalendar;
      editorElement.TextBoxElement.TextBoxItem.HostedControl.Focus();
      editorElement.TextBoxElement.TextBoxItem.SelectionStart = 0;
      editorElement.TextBoxElement.TextBoxItem.SelectionLength = 1;
      if (currentBehavior != null)
      {
        currentBehavior.DropDownMinSize = new Size(editorElement.Size.Width, 200);
        RadMaskedEditBoxElement textBoxElement2 = editorElement.TextBoxElement;
        MaskDateTimeProvider provider = (MaskDateTimeProvider) textBoxElement2.Provider;
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
      if (!BaseTextBoxEditor.IsDarkTheme(this.OwnerElement))
      {
        int num = (int) editorElement.SetDefaultValueOverride(VisualElement.BackColorProperty, (object) Color.White);
      }
      editorElement.ValueChanging += new ValueChangingEventHandler(this.RadDateTimeEditor_ValueChanging);
      editorElement.ValueChanged += new EventHandler(this.RadDateTimeEditor_ValueChanged);
      if (textBoxElement1 == null)
        return;
      textBoxElement1.KeyDown += new KeyEventHandler(this.TextBoxElement_KeyDown);
      textBoxElement1.TextBoxItem.HostedControl.LostFocus += new EventHandler(this.HostedControl_LostFocus);
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
        textBoxElement.TextBoxItem.HostedControl.LostFocus -= new EventHandler(this.HostedControl_LostFocus);
      }
      (editorElement.GetCurrentBehavior() as RadDateTimePickerCalendar)?.PopupControl.Hide();
      return true;
    }

    protected override RadElement CreateEditorElement()
    {
      BaseDateTimeEditorElement timeEditorElement = new BaseDateTimeEditorElement();
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
      try
      {
        this.Value = (object) Convert.ToDateTime(value);
      }
      catch
      {
      }
    }

    protected virtual void OnKeyDown(KeyEventArgs e)
    {
      RadDateTimePickerElement editorElement = this.EditorElement as RadDateTimePickerElement;
      if (editorElement == null || !editorElement.IsInValidState(true) || this.OwnerElement == null)
        return;
      if (e.KeyCode == Keys.Right)
        this.isRightmostMaskItemSelected = this.IsRightmostMaskItemSelected();
      else if (e.KeyCode == Keys.Left)
        this.isLeftmostMaskItemSelected = this.IsLeftmostMaskItemSelected();
      else
        editorElement.NullDate = DateTime.MinValue;
    }

    protected virtual void OnLostFocus()
    {
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
      if (currentBehavior != null)
      {
        currentBehavior.Calendar.SelectedDates.Clear();
        currentBehavior.Calendar.InvalidateCalendar();
      }
      this.popupOpened = true;
    }

    private void PopupControl_Closed(object sender, RadPopupClosedEventArgs args)
    {
      (this.EditorElement as RadDateTimePickerElement).TextBoxElement.TextBoxItem.HostedControl.Focus();
      this.popupOpened = false;
    }

    private void TextBoxElement_KeyDown(object sender, KeyEventArgs e)
    {
      this.OnKeyDown(e);
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
      (this.EditorElement as BaseDateTimeEditorElement).SetToNullValue();
    }

    private void TodayButton_Click(object sender, EventArgs e)
    {
      (this.EditorElement as BaseDateTimeEditorElement).Value = new DateTime?(DateTime.Now);
    }

    private void HostedControl_LostFocus(object sender, EventArgs e)
    {
      if (this.popupOpened)
        return;
      this.OnLostFocus();
    }
  }
}
