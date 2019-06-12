// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadColorBoxElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class RadColorBoxElement : RadTextBoxElement
  {
    private ColorEditorColorBox colorBox;
    private ColorPickerButtonElement colorPickerButton;
    private Color value;
    private Color oldValue;
    private TypeConverter converter;
    private RadColorDialog colorDialog;
    private bool valueChanging;
    private bool readOnly;
    private StackLayoutElement stack;

    public RadColorBoxElement()
    {
      RadTextBoxItem textBoxItem = this.TextBoxItem;
      this.Children.Remove((RadElement) textBoxItem);
      this.stack.Children.Add((RadElement) this.colorBox);
      this.stack.Children.Add((RadElement) textBoxItem);
      this.stack.Children.Add((RadElement) this.colorPickerButton);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.converter = TypeDescriptor.GetConverter(typeof (Color));
      this.stack = new StackLayoutElement();
      this.stack.StretchHorizontally = true;
      this.stack.StretchVertically = true;
      this.stack.Class = "ColorBoxLayout";
      this.stack.FitInAvailableSize = true;
      this.stack.ShouldHandleMouseInput = false;
      this.Children.Add((RadElement) this.stack);
      this.colorBox = this.CreateColorBoxElement();
      this.colorDialog = this.CreateColorDialog();
      this.colorPickerButton = this.CreateColorPickerButtonElement();
      this.WireEvents();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.value = Color.Empty;
      this.ShouldHandleMouseInput = true;
      this.NotifyParentOnMouseInput = true;
      this.valueChanging = false;
      this.readOnly = false;
      this.StretchVertically = true;
    }

    protected virtual ColorPickerButtonElement CreateColorPickerButtonElement()
    {
      return new ColorPickerButtonElement();
    }

    protected virtual ColorEditorColorBox CreateColorBoxElement()
    {
      return new ColorEditorColorBox();
    }

    protected virtual RadColorDialog CreateColorDialog()
    {
      return new RadColorDialog();
    }

    protected override void DisposeManagedResources()
    {
      this.UnwireEvents();
      this.colorDialog.Dispose();
      base.DisposeManagedResources();
    }

    protected virtual void WireEvents()
    {
      this.colorPickerButton.Click += new EventHandler(this.colorPickerButton_Click);
    }

    protected virtual void UnwireEvents()
    {
      this.colorPickerButton.Click -= new EventHandler(this.colorPickerButton_Click);
    }

    public virtual Color Value
    {
      get
      {
        return this.GetColorValue();
      }
      set
      {
        this.SetColorValue(value);
      }
    }

    public ColorEditorColorBox ColorBox
    {
      get
      {
        return this.colorBox;
      }
    }

    public RadColorDialog ColorDialog
    {
      get
      {
        return this.colorDialog;
      }
      set
      {
        if (value == this.colorDialog)
          return;
        this.colorDialog = value;
      }
    }

    public ColorPickerButtonElement ColorPickerButton
    {
      get
      {
        return this.colorPickerButton;
      }
    }

    public bool ReadOnly
    {
      get
      {
        return this.readOnly;
      }
      set
      {
        if (this.readOnly == value)
          return;
        this.readOnly = value;
        this.OnReadOnlyChanged(new EventArgs());
      }
    }

    [Category("Action")]
    [Description("Occurs when the editor is changing the value during the editing proccess.")]
    public event ValueChangingEventHandler ValueChanging;

    [Description("Occurs after the editor has changed the value during the editing process.")]
    [Category("Action")]
    public event EventHandler ValueChanged;

    [Category("Action")]
    [Description("Occurs when the dialog window is closed.")]
    public event DialogClosedEventHandler DialogClosed;

    public virtual bool Validate()
    {
      try
      {
        this.converter.ConvertFromString((ITypeDescriptorContext) null, Thread.CurrentThread.CurrentCulture, this.Text);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public virtual void SetColorValue(Color newValue)
    {
      if (this.ReadOnly)
        return;
      this.valueChanging = true;
      ValueChangingEventArgs e = new ValueChangingEventArgs((object) newValue, (object) this.value);
      this.OnValueChanging(e);
      if (e.Cancel)
      {
        this.Text = this.converter.ConvertToString((ITypeDescriptorContext) null, Thread.CurrentThread.CurrentCulture, (object) this.value);
        this.valueChanging = false;
      }
      else
      {
        this.oldValue = this.value;
        this.value = newValue;
        this.colorBox.BackColor = newValue;
        if (newValue.IsNamedColor)
          this.Text = LocalizationProvider<ColorDialogLocalizationProvider>.CurrentProvider.GetLocalizedString(newValue.Name);
        else
          this.Text = this.converter.ConvertToString((ITypeDescriptorContext) null, Thread.CurrentThread.CurrentCulture, (object) newValue);
        this.valueChanging = false;
        this.OnValueChanged(new EventArgs());
      }
    }

    public virtual Color GetColorValue()
    {
      try
      {
        this.value = (Color) this.converter.ConvertFromString((ITypeDescriptorContext) null, Thread.CurrentThread.CurrentCulture, this.Text);
      }
      catch (Exception ex)
      {
      }
      return this.value;
    }

    protected virtual void OnDialogClosed(DialogClosedEventArgs e)
    {
      DialogClosedEventHandler dialogClosed = this.DialogClosed;
      if (dialogClosed == null)
        return;
      dialogClosed((object) this, e);
    }

    protected virtual void OnValueChanged(EventArgs e)
    {
      EventHandler valueChanged = this.ValueChanged;
      if (valueChanged != null)
        valueChanged((object) this, e);
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "ValueChanged", (object) this.Text);
    }

    protected virtual void OnValueChanging(ValueChangingEventArgs e)
    {
      ValueChangingEventHandler valueChanging = this.ValueChanging;
      if (valueChanging == null)
        return;
      valueChanging((object) this, e);
    }

    protected virtual void OnDialogButtonClick(EventArgs e)
    {
      this.colorDialog.SelectedColor = this.value;
      RadForm colorDialogForm = this.colorDialog.ColorDialogForm as RadForm;
      RadControl control = this.ElementTree.Control as RadControl;
      if (colorDialogForm != null && control != null)
      {
        colorDialogForm.ThemeName = control.ThemeName;
        colorDialogForm.RightToLeft = control.RightToLeft;
      }
      colorDialogForm.EnableAnalytics = this.EnableAnalytics;
      DialogResult dialogResult = this.colorDialog.ShowDialog();
      if (dialogResult == DialogResult.OK)
      {
        string text = this.converter.ConvertToString((ITypeDescriptorContext) null, Thread.CurrentThread.CurrentCulture, (object) this.colorDialog.SelectedColor);
        bool flag = this.ReadOnly;
        this.ReadOnly = false;
        this.Value = (Color) this.converter.ConvertFromString((ITypeDescriptorContext) null, Thread.CurrentThread.CurrentCulture, text);
        this.ReadOnly = flag;
      }
      this.OnDialogClosed(new DialogClosedEventArgs(dialogResult));
    }

    protected override void OnTextChanged(EventArgs e)
    {
      if (this.valueChanging)
        return;
      base.OnTextChanged(e);
    }

    protected override void OnTextChanging(TextChangingEventArgs e)
    {
      e.Cancel = this.readOnly;
      if (this.valueChanging)
        return;
      base.OnTextChanging(e);
    }

    protected override void OnPropertyChanging(RadPropertyChangingEventArgs args)
    {
      if (this.valueChanging || args.Property == RadItem.TextProperty)
        return;
      base.OnPropertyChanging(args);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (!this.valueChanging && e.Property != RadItem.TextProperty)
        base.OnPropertyChanged(e);
      if (e.Property != RadElement.RightToLeftProperty)
        return;
      bool newValue = (bool) e.NewValue;
      if (this.Shape != null)
        this.Shape.IsRightToLeft = newValue;
      if (this.ColorPickerButton.Shape == null)
        return;
      this.ColorPickerButton.Shape.IsRightToLeft = newValue;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (e.KeyData == Keys.Return)
      {
        try
        {
          this.Value = (Color) this.converter.ConvertFromString((ITypeDescriptorContext) null, Thread.CurrentThread.CurrentCulture, this.Text);
        }
        catch (Exception ex)
        {
          this.Focus();
          this.TextBoxItem.SelectAll();
          return;
        }
      }
      else if (e.KeyData == Keys.Escape)
        this.Value = this.oldValue;
      base.OnKeyDown(e);
    }

    private void colorPickerButton_Click(object sender, EventArgs e)
    {
      this.OnDialogButtonClick(e);
    }
  }
}
