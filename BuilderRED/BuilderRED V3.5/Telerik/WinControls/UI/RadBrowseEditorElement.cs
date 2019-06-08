// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadBrowseEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class RadBrowseEditorElement : RadTextBoxElement
  {
    private bool readOnly = true;
    private BrowseEditorButton browseButton;
    private CommonDialog dialog;
    private string value;
    private bool valueChanging;
    private BrowseEditorDialogType browseEditorDialogType;

    public RadBrowseEditorElement()
    {
      this.TextBoxItem.RouteMessages = false;
      this.ButtonsStack.Children.Add((RadElement) this.browseButton);
      this.WireEvents();
      bool flag = this.ReadOnly;
      this.ReadOnly = false;
      this.TextBoxItem.HostedControl.Text = RadBrowseEditorElement.NoneText;
      this.ReadOnly = flag;
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.UnwireEvents();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.value = (string) null;
      this.browseEditorDialogType = BrowseEditorDialogType.OpenFileDialog;
      this.ShouldHandleMouseInput = true;
      this.NotifyParentOnMouseInput = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.dialog = (CommonDialog) this.CreateOpenFileDialog();
      this.browseButton = this.CreateDialogButtonElement();
    }

    protected virtual void WireEvents()
    {
      this.browseButton.Click += new EventHandler(this.ButtonElement_Click);
      this.TextBoxItem.KeyDown += new KeyEventHandler(this.TextBoxItem_KeyDown);
      this.TextBoxItem.Validated += new EventHandler(this.TextBoxItem_Validated);
      this.TextBoxItem.HostedControl.GotFocus += new EventHandler(this.HostedControl_GotFocus);
    }

    protected virtual void UnwireEvents()
    {
      this.browseButton.Click -= new EventHandler(this.ButtonElement_Click);
      this.TextBoxItem.KeyDown -= new KeyEventHandler(this.TextBoxItem_KeyDown);
      this.TextBoxItem.Validated -= new EventHandler(this.TextBoxItem_Validated);
      this.TextBoxItem.HostedControl.GotFocus -= new EventHandler(this.HostedControl_GotFocus);
    }

    [DefaultValue(null)]
    [Description("Gets the value of the editor.")]
    public virtual string Value
    {
      get
      {
        if (this.value == null && this.Text != RadBrowseEditorElement.NoneText || this.value != null && this.Text != this.value.ToString())
          this.SetEditorValue(this.Text);
        return this.value;
      }
      set
      {
        bool flag = this.readOnly;
        this.readOnly = false;
        this.SetEditorValue(value);
        this.readOnly = flag;
      }
    }

    [Description("Gets or sets the type of dialog to be opened when the browse button is pressed.")]
    [DefaultValue(BrowseEditorDialogType.OpenFileDialog)]
    public virtual BrowseEditorDialogType DialogType
    {
      get
      {
        return this.browseEditorDialogType;
      }
      set
      {
        if (this.browseEditorDialogType == value)
          return;
        this.browseEditorDialogType = value;
        this.ChangeDialogType(value);
      }
    }

    [Description("Gets the RadButtonElement that opens the OpenFileDialog.")]
    public BrowseEditorButton BrowseButton
    {
      get
      {
        return this.browseButton;
      }
    }

    [Description("Gets the dialog that will open upon pressing the browse button.")]
    public CommonDialog Dialog
    {
      get
      {
        return this.dialog;
      }
    }

    [Description("Determines if users can input text directly into the text field.")]
    [DefaultValue(true)]
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
        this.TextBoxItem.ReadOnly = value;
        this.OnReadOnlyChanged(new EventArgs());
      }
    }

    private static string NoneText
    {
      get
      {
        return LocalizationProvider<RadBrowseEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("RadBrowseEditorElementNone");
      }
    }

    [Description("Fires after the dialog window is closed.")]
    [Category("Action")]
    public event DialogClosedEventHandler DialogClosed;

    [Description("Fires right before the value is changed.")]
    [Category("Action")]
    public event ValueChangingEventHandler ValueChanging;

    [Description("Fires after the editor value is changed.")]
    [Category("Action")]
    public event EventHandler ValueChanged;

    protected virtual void OnValueChanged(EventArgs e)
    {
      EventHandler valueChanged = this.ValueChanged;
      if (valueChanged == null)
        return;
      valueChanged((object) this, e);
    }

    protected virtual void OnValueChanging(ValueChangingEventArgs e)
    {
      ValueChangingEventHandler valueChanging = this.ValueChanging;
      if (valueChanging == null)
        return;
      valueChanging((object) this, e);
    }

    protected virtual void OnDialogClosed(DialogClosedEventArgs e)
    {
      DialogClosedEventHandler dialogClosed = this.DialogClosed;
      if (dialogClosed == null)
        return;
      dialogClosed((object) this, e);
    }

    protected virtual void OnBrowseButtonClick(EventArgs e)
    {
      this.InitializeDialog();
      DialogResult dialogResult = this.Dialog.ShowDialog();
      if (dialogResult == DialogResult.OK)
      {
        bool flag = this.ReadOnly;
        this.ReadOnly = false;
        this.SaveValueFromDialog();
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
      e.Cancel = this.ReadOnly;
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
      if (this.BrowseButton.Shape == null)
        return;
      this.BrowseButton.Shape.IsRightToLeft = newValue;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      if (e.KeyData != Keys.Return)
        return;
      this.Value = this.Text;
    }

    private void TextBoxItem_Validated(object sender, EventArgs e)
    {
      this.Value = this.Text;
    }

    private void ButtonElement_Click(object sender, EventArgs e)
    {
      this.OnBrowseButtonClick(e);
    }

    private void TextBoxItem_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData != Keys.Return || this.ReadOnly)
        return;
      this.Value = this.Text;
    }

    private void HostedControl_GotFocus(object sender, EventArgs e)
    {
      if (this.ReadOnly || !(this.TextBoxItem.HostedControl.Text == RadBrowseEditorElement.NoneText))
        return;
      this.TextBoxItem.HostedControl.Text = "";
    }

    protected virtual void SetEditorValue(string newValue)
    {
      if (this.ReadOnly)
        return;
      this.valueChanging = true;
      ValueChangingEventArgs e = new ValueChangingEventArgs((object) newValue, (object) this.value);
      this.OnValueChanging(e);
      if (e.Cancel)
      {
        this.Text = this.value;
        this.valueChanging = false;
      }
      else
      {
        if (string.IsNullOrEmpty(newValue) || newValue == RadBrowseEditorElement.NoneText)
        {
          this.value = (string) null;
          this.Text = RadBrowseEditorElement.NoneText;
        }
        else
        {
          if (newValue == this.value)
          {
            this.valueChanging = false;
            return;
          }
          this.Text = newValue;
          this.value = newValue;
        }
        this.valueChanging = false;
        this.OnValueChanged(new EventArgs());
      }
    }

    protected virtual void ChangeDialogType(BrowseEditorDialogType dialogType)
    {
      switch (dialogType)
      {
        case BrowseEditorDialogType.OpenFileDialog:
          this.dialog = (CommonDialog) this.CreateOpenFileDialog();
          break;
        case BrowseEditorDialogType.FolderBrowseDialog:
          this.dialog = (CommonDialog) this.CreateFolderBrowserDialog();
          break;
        case BrowseEditorDialogType.SaveFileDialog:
          this.dialog = (CommonDialog) this.CreateSaveFileDialog();
          break;
        case BrowseEditorDialogType.FontDialog:
          this.dialog = (CommonDialog) this.CreateFontDialog();
          break;
      }
    }

    protected virtual void InitializeDialog()
    {
      if (this.DialogType == BrowseEditorDialogType.OpenFileDialog)
      {
        OpenFileDialog dialog = this.Dialog as OpenFileDialog;
        if (dialog == null)
          return;
        string file = "";
        string dir = "";
        this.GetFileAndPathNames(out file, out dir);
        if (Directory.Exists(dir))
          dialog.InitialDirectory = dir;
        dialog.FileName = file;
      }
      else if (this.DialogType == BrowseEditorDialogType.FolderBrowseDialog)
      {
        FolderBrowserDialog dialog = this.Dialog as FolderBrowserDialog;
        if (dialog == null)
          return;
        dialog.SelectedPath = this.Value;
      }
      else if (this.DialogType == BrowseEditorDialogType.SaveFileDialog)
      {
        SaveFileDialog dialog = this.Dialog as SaveFileDialog;
        if (dialog == null)
          return;
        string file = "";
        string dir = "";
        this.GetFileAndPathNames(out file, out dir);
        if (Directory.Exists(dir))
          dialog.InitialDirectory = dir;
        dialog.FileName = Path.GetFileName(this.Value);
      }
      else
      {
        if (this.DialogType != BrowseEditorDialogType.FontDialog)
          return;
        FontDialog dialog = this.Dialog as FontDialog;
        if (dialog == null)
          return;
        FontConverter fontConverter = new FontConverter();
        if (string.IsNullOrEmpty(this.Value))
          return;
        object obj = fontConverter.ConvertFromString(this.Value);
        dialog.Font = (Font) obj;
      }
    }

    protected virtual void SaveValueFromDialog()
    {
      if (this.DialogType == BrowseEditorDialogType.OpenFileDialog)
      {
        OpenFileDialog dialog = this.Dialog as OpenFileDialog;
        if (dialog == null)
          return;
        this.Value = dialog.FileName;
      }
      else if (this.DialogType == BrowseEditorDialogType.FolderBrowseDialog)
      {
        FolderBrowserDialog dialog = this.Dialog as FolderBrowserDialog;
        if (dialog == null)
          return;
        this.Value = dialog.SelectedPath;
      }
      else if (this.DialogType == BrowseEditorDialogType.SaveFileDialog)
      {
        SaveFileDialog dialog = this.Dialog as SaveFileDialog;
        if (dialog == null)
          return;
        this.Value = dialog.FileName;
      }
      else
      {
        if (this.DialogType != BrowseEditorDialogType.FontDialog)
          return;
        FontDialog dialog = this.Dialog as FontDialog;
        if (dialog == null)
          return;
        this.Value = new FontConverter().ConvertToString((object) dialog.Font);
      }
    }

    protected virtual OpenFileDialog CreateOpenFileDialog()
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.CheckFileExists = true;
      openFileDialog.CheckPathExists = true;
      openFileDialog.RestoreDirectory = true;
      return openFileDialog;
    }

    protected virtual FolderBrowserDialog CreateFolderBrowserDialog()
    {
      return new FolderBrowserDialog() { ShowNewFolderButton = true };
    }

    protected virtual SaveFileDialog CreateSaveFileDialog()
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.OverwritePrompt = true;
      saveFileDialog.RestoreDirectory = true;
      saveFileDialog.CreatePrompt = true;
      return saveFileDialog;
    }

    protected virtual FontDialog CreateFontDialog()
    {
      return new FontDialog() { ShowApply = false };
    }

    protected virtual BrowseEditorButton CreateDialogButtonElement()
    {
      return new BrowseEditorButton();
    }

    private void GetFileAndPathNames(out string file, out string dir)
    {
      try
      {
        file = Path.GetFileName(this.Value);
        dir = Path.GetDirectoryName(this.Value);
      }
      catch
      {
        file = "";
        dir = "";
      }
      if (string.IsNullOrEmpty(dir) || dir.EndsWith("\\"))
        return;
      dir += "\\";
    }
  }
}
