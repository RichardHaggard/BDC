// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadMessageBoxForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Paint;
using Telerik.WinControls.UI;

namespace Telerik.WinControls
{
  public class RadMessageBoxForm : RadForm
  {
    private Size buttonSize = new Size(75, 23);
    private bool enableBeep = true;
    private int detailsSectionHeight = 100;
    private const int BORDER_OFFSET = 5;
    private const int TOUCH_BORDER_OFFSET = 6;
    private const int BUTTON_MARGIN = 3;
    private const int TOUCH_BUTTON_MARGIN = 8;
    private int buttonCount;
    private MessageBoxButtons buttonsConfiguration;
    private RadButton radButton1;
    private RadButton radButton2;
    private RadButton radButton3;
    private MessageBoxDetailsButton radButtonDetails;
    private PictureBox pictureBox1;
    private RadLabel radLabel1;
    private RadTextBox radTextBoxDetials;
    private bool accObjectCreated;

    public RadMessageBoxForm()
    {
      this.InitializeComponents();
      this.buttonCount = 0;
      this.ThemeNameChanged += new ThemeNameChangedEventHandler(this.RadMessageBoxForm_ThemeNameChanged);
      this.KeyDown += new KeyEventHandler(this.RadMessageBoxForm_KeyDown);
      this.KeyPreview = true;
    }

    private void InitializeComponents()
    {
      this.pictureBox1 = new PictureBox();
      this.radButton1 = new RadButton();
      this.radButton2 = new RadButton();
      this.radButton3 = new RadButton();
      this.radButtonDetails = new MessageBoxDetailsButton();
      this.radLabel1 = new RadLabel();
      this.radTextBoxDetials = new RadTextBox();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.radButton1.BeginInit();
      this.radButton2.BeginInit();
      this.radButton3.BeginInit();
      this.radButtonDetails.BeginInit();
      this.radLabel1.BeginInit();
      this.radTextBoxDetials.BeginInit();
      this.BeginInit();
      this.pictureBox1.BackColor = Color.Transparent;
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(48, 48);
      this.pictureBox1.TabIndex = 5;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
      this.radButton1.Name = "radButton1";
      this.radButton1.Size = this.buttonSize;
      this.radButton1.TabIndex = 0;
      this.radButton1.Click += new EventHandler(this.radButton1_Click);
      this.radButton2.Name = "radButton2";
      this.radButton2.Size = this.buttonSize;
      this.radButton2.TabIndex = 1;
      this.radButton2.Click += new EventHandler(this.radButton2_Click);
      this.radButton3.Name = "radButton3";
      this.radButton3.Size = this.buttonSize;
      this.radButton3.TabIndex = 2;
      this.radButton3.Click += new EventHandler(this.radButton3_Click);
      this.radButtonDetails.Name = "radButtonDetails";
      this.radButtonDetails.Size = this.buttonSize;
      this.radButtonDetails.TabIndex = 2;
      this.radButtonDetails.Click += new EventHandler(this.radButtonDetails_Click);
      this.radButtonDetails.Visible = false;
      this.radLabel1.AutoSize = true;
      this.radLabel1.LabelElement.StretchHorizontally = true;
      this.radLabel1.LabelElement.StretchVertically = true;
      this.radLabel1.Name = "radLabel1";
      this.radLabel1.MaximumSize = new Size(650, 0);
      this.radLabel1.TextWrap = true;
      this.radLabel1.TabIndex = 4;
      this.radTextBoxDetials.AutoSize = false;
      this.radTextBoxDetials.Name = "radTextBoxDetials";
      this.radTextBoxDetials.ReadOnly = true;
      this.radTextBoxDetials.Multiline = true;
      this.radTextBoxDetials.WordWrap = true;
      this.radTextBoxDetials.TabIndex = 5;
      this.radTextBoxDetials.Visible = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.radLabel1);
      this.Controls.Add((Control) this.radButton1);
      this.Controls.Add((Control) this.radButton2);
      this.Controls.Add((Control) this.radButton3);
      this.Controls.Add((Control) this.radButtonDetails);
      this.Controls.Add((Control) this.radTextBoxDetials);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "RadMessageBox";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "MessageBox";
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.radButton1.EndInit();
      this.radButton2.EndInit();
      this.radButton3.EndInit();
      this.radButtonDetails.EndInit();
      this.radLabel1.EndInit();
      this.radTextBoxDetials.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      this.accObjectCreated = true;
      return (AccessibleObject) new RadMessageBoxForm.RadMessageBoxAccessibleObject(this);
    }

    private int BorderOffset
    {
      get
      {
        return TelerikHelper.IsMaterialTheme(this.ThemeName) ? 6 : 5;
      }
    }

    private int ButtonMargin
    {
      get
      {
        return TelerikHelper.IsMaterialTheme(this.ThemeName) ? 8 : 3;
      }
    }

    public bool UseCompatibleTextRendering
    {
      set
      {
        this.UseCompatibleTextRendering = value;
        this.radLabel1.UseCompatibleTextRendering = value;
        this.radButton1.UseCompatibleTextRendering = value;
        this.radButton2.UseCompatibleTextRendering = value;
        this.radButton3.UseCompatibleTextRendering = value;
        this.radButtonDetails.UseCompatibleTextRendering = value;
        this.radTextBoxDetials.UseCompatibleTextRendering = value;
      }
    }

    public bool EnableBeep
    {
      get
      {
        return this.enableBeep;
      }
      set
      {
        this.enableBeep = value;
      }
    }

    public MessageBoxButtons ButtonsConfiguration
    {
      set
      {
        this.buttonsConfiguration = value;
        this.ConfigureButtons(value);
      }
    }

    public MessageBoxDefaultButton DefaultButton
    {
      set
      {
        this.SetButtonFocus(value);
      }
    }

    public override RightToLeft RightToLeft
    {
      set
      {
        base.RightToLeft = value;
        this.radLabel1.RightToLeft = value;
        this.radButton1.RightToLeft = value;
        this.radButton2.RightToLeft = value;
        this.radButton3.RightToLeft = value;
      }
    }

    public string MessageText
    {
      get
      {
        return this.radLabel1.Text;
      }
      set
      {
        this.SetLabelTextAndSize(value);
      }
    }

    public override string Text
    {
      set
      {
        base.Text = value;
      }
    }

    public string DetailsText
    {
      get
      {
        return this.radTextBoxDetials.Text;
      }
      set
      {
        this.SetDetailsText(value);
      }
    }

    public Bitmap MessageIcon
    {
      set
      {
        if (value != null)
        {
          Bitmap bitmap = value;
          if (bitmap.Size.Height > 32 || bitmap.Size.Width > 32)
            bitmap = new Bitmap((Image) bitmap, new Size(32, 32));
          this.pictureBox1.Image = (Image) bitmap;
          this.pictureBox1.Visible = true;
        }
        else
        {
          this.pictureBox1.Image = (Image) null;
          this.pictureBox1.Visible = false;
        }
      }
    }

    public Size ButtonSize
    {
      get
      {
        return TelerikDpiHelper.ScaleSize(this.buttonSize, this.RootElement.DpiScaleFactor);
      }
      set
      {
        if (!(this.buttonSize != value))
          return;
        this.buttonSize = value;
        this.radButton1.Size = value;
        this.radButton2.Size = value;
        this.radButton3.Size = value;
        this.radButtonDetails.Size = value;
      }
    }

    protected override void OnLoad(EventArgs e)
    {
      if (!this.IsLoaded)
        this.LoadElementTree();
      base.OnLoad(e);
      if (TelerikHelper.IsMaterialTheme(this.ThemeName))
      {
        this.Height += 45;
        this.ButtonSize = new Size(85, 36);
        int width = -15;
        if (this.RightToLeft == RightToLeft.Yes)
          width *= -1;
        this.radButtonDetails.Arrow.PositionOffset = (SizeF) new Size(width, 0);
      }
      this.SetSizeAndLocations();
      if (this.ThemeName == "TelerikMetroTouch" || ThemeResolutionService.ApplicationThemeName == "TelerikMetroTouch")
        this.Height += 30;
      if (!this.MinimizeBox)
        this.FormElement.TitleBar.MinimizeButton.Visibility = ElementVisibility.Collapsed;
      if (this.MaximizeBox)
        return;
      this.FormElement.TitleBar.MaximizeButton.Visibility = ElementVisibility.Collapsed;
    }

    protected override void OnShown(EventArgs e)
    {
      base.OnShown(e);
      Timer timer = new Timer();
      timer.Interval = 500;
      timer.Tick += new EventHandler(this.t_Tick);
      timer.Start();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      if (!this.radTextBoxDetials.Visible)
        return;
      this.HideDetails();
    }

    private void RadMessageBoxForm_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Control && e.KeyCode == Keys.C)
      {
        Clipboard.Clear();
        Clipboard.SetText(this.BuildTextForClipboard());
      }
      else
      {
        if (e.KeyCode != Keys.Escape || this.buttonsConfiguration != MessageBoxButtons.OK && this.buttonsConfiguration != MessageBoxButtons.OKCancel && this.buttonsConfiguration != MessageBoxButtons.YesNoCancel)
          return;
        this.DialogResult = DialogResult.Cancel;
        this.Close();
      }
    }

    private void RadMessageBoxForm_ThemeNameChanged(object source, ThemeNameChangedEventArgs args)
    {
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        if (this.Controls[index] is RadControl)
          (this.Controls[index] as RadControl).ThemeName = this.ThemeName;
      }
    }

    private void radButton1_Click(object sender, EventArgs e)
    {
      this.DialogResult = (DialogResult) this.radButton1.Tag;
      this.Close();
    }

    private void radButton2_Click(object sender, EventArgs e)
    {
      this.DialogResult = (DialogResult) this.radButton2.Tag;
      this.Close();
    }

    private void radButton3_Click(object sender, EventArgs e)
    {
      this.DialogResult = (DialogResult) this.radButton3.Tag;
      this.Close();
    }

    private void radButtonDetails_Click(object sender, EventArgs e)
    {
      if (this.radTextBoxDetials.Visible)
        this.HideDetails();
      else
        this.ShowDetails();
    }

    private void t_Tick(object sender, EventArgs e)
    {
      ((Timer) sender).Stop();
      ((Timer) sender).Tick -= new EventHandler(this.t_Tick);
      ((Component) sender).Dispose();
      if (!this.accObjectCreated || !this.Visible)
        return;
      this.Invoke((Delegate) (() =>
      {
        ((Control.ControlAccessibleObject) this.AccessibilityObject).NotifyClients(AccessibleEvents.Show);
        ((Control.ControlAccessibleObject) this.AccessibilityObject).NotifyClients(AccessibleEvents.SystemDialogStart);
      }));
      Application.DoEvents();
    }

    private void SetButtonFocus(MessageBoxDefaultButton defaultButton)
    {
      this.ActiveControl = (Control) this.radButton1;
      switch (defaultButton)
      {
        case MessageBoxDefaultButton.Button2:
          if (this.buttonCount <= 1)
            break;
          this.ActiveControl = (Control) this.radButton2;
          break;
        case MessageBoxDefaultButton.Button3:
          if (this.buttonCount <= 2)
            break;
          this.ActiveControl = (Control) this.radButton3;
          break;
      }
    }

    private void ConfigureButtons(MessageBoxButtons messageBoxButtons)
    {
      this.SetButtonsText(messageBoxButtons);
      switch (messageBoxButtons)
      {
        case MessageBoxButtons.OK:
          this.radButton1.Tag = (object) DialogResult.OK;
          this.radButton1.Visible = true;
          this.radButton2.Visible = false;
          this.radButton3.Visible = false;
          this.FormElement.TitleBar.CloseButton.Enabled = true;
          this.buttonCount = 1;
          break;
        case MessageBoxButtons.OKCancel:
          this.radButton1.Tag = (object) DialogResult.OK;
          this.radButton2.Tag = (object) DialogResult.Cancel;
          this.radButton1.Visible = true;
          this.radButton2.Visible = true;
          this.radButton3.Visible = false;
          this.FormElement.TitleBar.CloseButton.Enabled = true;
          this.buttonCount = 2;
          break;
        case MessageBoxButtons.AbortRetryIgnore:
          this.radButton1.Tag = (object) DialogResult.Abort;
          this.radButton2.Tag = (object) DialogResult.Retry;
          this.radButton3.Tag = (object) DialogResult.Ignore;
          this.radButton1.Visible = true;
          this.radButton2.Visible = true;
          this.radButton3.Visible = true;
          this.FormElement.TitleBar.CloseButton.Enabled = false;
          this.buttonCount = 3;
          break;
        case MessageBoxButtons.YesNoCancel:
          this.radButton1.Tag = (object) DialogResult.Yes;
          this.radButton2.Tag = (object) DialogResult.No;
          this.radButton3.Tag = (object) DialogResult.Cancel;
          this.radButton1.Visible = true;
          this.radButton2.Visible = true;
          this.radButton3.Visible = true;
          this.FormElement.TitleBar.CloseButton.Enabled = true;
          this.buttonCount = 3;
          break;
        case MessageBoxButtons.YesNo:
          this.radButton1.Tag = (object) DialogResult.Yes;
          this.radButton2.Tag = (object) DialogResult.No;
          this.radButton1.Visible = true;
          this.radButton2.Visible = true;
          this.radButton3.Visible = false;
          this.FormElement.TitleBar.CloseButton.Enabled = false;
          this.buttonCount = 2;
          break;
        case MessageBoxButtons.RetryCancel:
          this.radButton1.Tag = (object) DialogResult.Retry;
          this.radButton2.Tag = (object) DialogResult.Cancel;
          this.radButton1.Visible = true;
          this.radButton2.Visible = true;
          this.radButton3.Visible = false;
          this.FormElement.TitleBar.CloseButton.Enabled = true;
          this.buttonCount = 2;
          break;
      }
      if (string.IsNullOrEmpty(this.radTextBoxDetials.Text))
        return;
      this.radButtonDetails.Visible = true;
    }

    private void SetSizeAndLocations()
    {
      switch (this.buttonCount)
      {
        case 1:
          this.Size = new Size(2 * this.ButtonSize.Width, this.Size.Height);
          break;
        case 2:
          this.Size = new Size(3 * this.ButtonSize.Width, this.Size.Height);
          break;
        case 3:
          this.Size = new Size(4 * this.ButtonSize.Width, this.Size.Height);
          break;
      }
      if (!string.IsNullOrEmpty(this.radTextBoxDetials.Text))
        this.Size = new Size(this.Size.Width + this.ButtonSize.Width + 3 * this.ButtonMargin, this.Size.Height);
      Padding clientMargin = this.FormBehavior.ClientMargin;
      Size size = this.radLabel1.Size;
      size.Width += clientMargin.Horizontal + 2 * this.BorderOffset;
      size.Height += this.ButtonSize.Height + clientMargin.Vertical + 4 * this.BorderOffset;
      int widthFromTitleSize = this.CalculateWidthFromTitleSize();
      if (widthFromTitleSize > size.Width)
        size.Width = widthFromTitleSize;
      int width = this.Size.Width;
      if (this.pictureBox1.Image != null)
      {
        int borderOffset = this.BorderOffset;
        size.Width += this.pictureBox1.Width;
        if (this.radLabel1.Height < this.pictureBox1.Height)
        {
          size.Height += this.pictureBox1.Height - this.radLabel1.Height;
          borderOffset += (this.pictureBox1.Height - this.radLabel1.Height) / 2;
        }
        if (this.RightToLeft == RightToLeft.Yes)
        {
          if (width < size.Width)
            width = size.Width;
          this.pictureBox1.Location = new Point(width - this.BorderOffset - this.pictureBox1.Width, this.BorderOffset);
          this.radLabel1.Location = new Point(width - this.pictureBox1.Width - this.BorderOffset - this.radLabel1.Width - clientMargin.Right, borderOffset);
        }
        else
        {
          this.pictureBox1.Location = new Point(this.BorderOffset, this.BorderOffset);
          this.radLabel1.Location = new Point(this.BorderOffset + this.pictureBox1.Width, borderOffset);
        }
      }
      else if (this.RightToLeft == RightToLeft.Yes)
      {
        if (width < size.Width)
          width = size.Width;
        this.radLabel1.Location = new Point(width - this.BorderOffset - this.radLabel1.Width - clientMargin.Right, this.BorderOffset);
      }
      else
        this.radLabel1.Location = new Point(this.BorderOffset, this.BorderOffset);
      this.Size = new Size(Math.Max(this.Size.Width, size.Width), size.Height);
      if (string.IsNullOrEmpty(this.DetailsText))
        this.SetButtonsLocation();
      else
        this.SetButtonsLocationDetailsVisible();
    }

    private void SetButtonsLocation()
    {
      Size clientSize = this.ClientSize;
      int y = clientSize.Height - this.ButtonSize.Height - this.BorderOffset;
      Point point1;
      Point point2;
      Point point3;
      if (this.RightToLeft == RightToLeft.Yes)
      {
        point1 = new Point((clientSize.Width + (this.buttonCount - 2) * this.ButtonSize.Width) / 2, y);
        point2 = new Point(point1.X - this.ButtonSize.Width - this.ButtonMargin, y);
        point3 = new Point(point2.X - this.ButtonSize.Width - this.ButtonMargin, y);
      }
      else
      {
        point1 = new Point((clientSize.Width - this.buttonCount * this.ButtonSize.Width) / 2, y);
        point2 = new Point(point1.X + this.ButtonSize.Width + this.ButtonMargin, y);
        point3 = new Point(point2.X + this.ButtonSize.Width + this.ButtonMargin, y);
      }
      this.radButton1.Location = point1;
      this.radButton2.Location = point2;
      this.radButton3.Location = point3;
    }

    private void SetButtonsLocationDetailsVisible()
    {
      int y = this.ClientSize.Height - this.ButtonSize.Height - this.BorderOffset;
      Point point1;
      Point point2;
      Point point3;
      Point point4;
      if (this.RightToLeft == RightToLeft.Yes)
      {
        point1 = new Point(this.ClientRectangle.Right - this.ButtonSize.Width - this.ButtonMargin, y);
        point2 = new Point(this.ButtonMargin, y);
        point3 = new Point(point2.X + this.ButtonSize.Width + this.ButtonMargin, y);
        point4 = new Point(point3.X + this.ButtonSize.Width + this.ButtonMargin, y);
      }
      else
      {
        point1 = new Point(this.ButtonMargin, y);
        point2 = new Point(this.ClientRectangle.Right - (this.ButtonSize.Width + this.ButtonMargin), y);
        point3 = new Point(point2.X - (this.ButtonSize.Width + this.ButtonMargin), y);
        point4 = new Point(point3.X - (this.ButtonSize.Width + this.ButtonMargin), y);
      }
      this.radButtonDetails.Location = point1;
      switch (this.buttonCount)
      {
        case 1:
          this.radButton1.Location = point2;
          break;
        case 2:
          this.radButton1.Location = point3;
          this.radButton2.Location = point2;
          break;
        case 3:
          this.radButton1.Location = point4;
          this.radButton2.Location = point3;
          this.radButton3.Location = point2;
          break;
      }
      this.radTextBoxDetials.Location = new Point(0, point1.Y + this.ButtonSize.Height + this.ButtonMargin);
      this.radTextBoxDetials.Size = new Size(this.Width - this.FormBehavior.ClientMargin.Horizontal, this.detailsSectionHeight + this.radTextBoxDetials.TextBoxElement.BorderThickness.Bottom + this.radTextBoxDetials.TextBoxElement.BorderThickness.Top);
    }

    private void SetLabelTextAndSize(string text)
    {
      this.radLabel1.Size = new Size(0, 0);
      this.radLabel1.Text = string.Empty;
      RadGdiGraphics radGdiGraphics = new RadGdiGraphics(this.CreateGraphics());
      SizeF sizeF = radGdiGraphics.MeasureString(text, new Font(this.radLabel1.Font.FontFamily, this.radLabel1.Font.Size * this.RootElement.DpiScaleFactor.Height, this.radLabel1.Font.Style), 650, StringFormat.GenericDefault);
      radGdiGraphics.Dispose();
      this.radLabel1.Size = sizeF.ToSize();
      this.radLabel1.Text = text;
      this.SetSizeAndLocations();
    }

    private int CalculateWidthFromTitleSize()
    {
      MeasurementGraphics measurementGraphics = MeasurementGraphics.CreateMeasurementGraphics();
      int num = (int) ((double) measurementGraphics.Graphics.MeasureString(this.Text, new Font(this.Font.FontFamily, this.Font.Size * this.RootElement.DpiScaleFactor.Height, this.Font.Style)).Width + 170.0);
      measurementGraphics.Dispose();
      return num;
    }

    private void SetButtonsText(MessageBoxButtons buttons)
    {
      switch (buttons)
      {
        case MessageBoxButtons.OK:
          this.radButton1.Text = LocalizationProvider<RadMessageLocalizationProvider>.CurrentProvider.GetLocalizedString("OK");
          this.radButton2.Text = string.Empty;
          this.radButton3.Text = string.Empty;
          break;
        case MessageBoxButtons.OKCancel:
          this.radButton1.Text = LocalizationProvider<RadMessageLocalizationProvider>.CurrentProvider.GetLocalizedString("OK");
          this.radButton2.Text = LocalizationProvider<RadMessageLocalizationProvider>.CurrentProvider.GetLocalizedString("Cancel");
          this.radButton3.Text = string.Empty;
          break;
        case MessageBoxButtons.AbortRetryIgnore:
          this.radButton1.Text = LocalizationProvider<RadMessageLocalizationProvider>.CurrentProvider.GetLocalizedString("Abort");
          this.radButton2.Text = LocalizationProvider<RadMessageLocalizationProvider>.CurrentProvider.GetLocalizedString("Retry");
          this.radButton3.Text = LocalizationProvider<RadMessageLocalizationProvider>.CurrentProvider.GetLocalizedString("Ignore");
          break;
        case MessageBoxButtons.YesNoCancel:
          this.radButton1.Text = LocalizationProvider<RadMessageLocalizationProvider>.CurrentProvider.GetLocalizedString("Yes");
          this.radButton2.Text = LocalizationProvider<RadMessageLocalizationProvider>.CurrentProvider.GetLocalizedString("No");
          this.radButton3.Text = LocalizationProvider<RadMessageLocalizationProvider>.CurrentProvider.GetLocalizedString("Cancel");
          break;
        case MessageBoxButtons.YesNo:
          this.radButton1.Text = LocalizationProvider<RadMessageLocalizationProvider>.CurrentProvider.GetLocalizedString("Yes");
          this.radButton2.Text = LocalizationProvider<RadMessageLocalizationProvider>.CurrentProvider.GetLocalizedString("No");
          this.radButton3.Text = string.Empty;
          break;
        case MessageBoxButtons.RetryCancel:
          this.radButton1.Text = LocalizationProvider<RadMessageLocalizationProvider>.CurrentProvider.GetLocalizedString("Retry");
          this.radButton2.Text = LocalizationProvider<RadMessageLocalizationProvider>.CurrentProvider.GetLocalizedString("Cancel");
          this.radButton3.Text = string.Empty;
          break;
      }
      this.radButtonDetails.Text = LocalizationProvider<RadMessageLocalizationProvider>.CurrentProvider.GetLocalizedString("Details");
    }

    private void SetDetailsText(string value)
    {
      this.radTextBoxDetials.Text = value;
      if (!string.IsNullOrEmpty(value))
        this.radButtonDetails.Visible = true;
      else
        this.radButtonDetails.Visible = false;
    }

    private string BuildTextForClipboard()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("---------------------------");
      stringBuilder.AppendLine(this.Text);
      stringBuilder.AppendLine("---------------------------");
      stringBuilder.AppendLine(this.radLabel1.Text);
      stringBuilder.AppendLine("---------------------------");
      stringBuilder.Append(this.radButton1.Text);
      if (this.radButton2.Visible)
      {
        stringBuilder.Append("  ");
        stringBuilder.Append(this.radButton2.Text);
      }
      if (this.radButton3.Visible)
      {
        stringBuilder.Append("  ");
        stringBuilder.Append(this.radButton3.Text);
      }
      stringBuilder.AppendLine();
      stringBuilder.AppendLine("---------------------------");
      if (this.radButtonDetails.Arrow.Direction == ArrowDirection.Up)
      {
        stringBuilder.AppendLine(this.radButtonDetails.Text);
        stringBuilder.AppendLine("---------------------------");
        stringBuilder.AppendLine(this.DetailsText);
        stringBuilder.AppendLine("---------------------------");
      }
      return stringBuilder.ToString();
    }

    public void ShowDetails()
    {
      this.radTextBoxDetials.TextBoxElement.Fill.BackColor = this.FormElement.BackColor;
      this.radTextBoxDetials.TextBoxElement.BackColor = this.FormElement.BackColor;
      this.radTextBoxDetials.TextBoxElement.TextBoxItem.HostedControl.BackColor = this.FormElement.BackColor;
      this.radTextBoxDetials.Visible = true;
      StringFormatFlags stringFormatFlags = StringFormatFlags.FitBlackBox;
      StringFormat genericTypographic = StringFormat.GenericTypographic;
      genericTypographic.FormatFlags = stringFormatFlags;
      if ((int) RadGdiGraphics.MeasurementGraphics.MeasureString(this.radTextBoxDetials.Text, this.radTextBoxDetials.Font, this.radTextBoxDetials.Width, genericTypographic).Height > this.detailsSectionHeight + (this.radTextBoxDetials.TextBoxElement.BorderThickness.Vertical + this.radTextBoxDetials.TextBoxElement.Padding.Vertical))
        this.radTextBoxDetials.TextBoxItem.ScrollBars = ScrollBars.Vertical;
      else
        this.radTextBoxDetials.TextBoxItem.ScrollBars = ScrollBars.None;
      this.Size = new Size(this.Size.Width, this.Size.Height + this.detailsSectionHeight);
      this.radButtonDetails.Arrow.Direction = ArrowDirection.Up;
    }

    public void HideDetails()
    {
      this.radTextBoxDetials.Visible = false;
      this.Size = new Size(this.Size.Width, this.Size.Height - this.detailsSectionHeight);
      this.radButtonDetails.Arrow.Direction = ArrowDirection.Down;
    }

    private class RadMessageBoxAccessibleObject : Control.ControlAccessibleObject
    {
      private RadMessageBoxForm owner;

      public RadMessageBoxAccessibleObject(RadMessageBoxForm owner)
        : base((Control) owner)
      {
        this.owner = owner;
        this.owner.FormClosed += new FormClosedEventHandler(this.owner_FormClosed);
      }

      private void owner_FormClosed(object sender, FormClosedEventArgs e)
      {
        this.NotifyClients(AccessibleEvents.SystemDialogEnd);
      }

      public override AccessibleRole Role
      {
        get
        {
          return AccessibleRole.Dialog;
        }
      }

      public override string Description
      {
        get
        {
          return this.owner.radLabel1.Text;
        }
      }

      public override string Name
      {
        get
        {
          return this.owner.Text;
        }
        set
        {
          base.Name = value;
        }
      }
    }
  }
}
