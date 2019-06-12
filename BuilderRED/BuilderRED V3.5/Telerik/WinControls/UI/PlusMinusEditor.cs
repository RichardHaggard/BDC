// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PlusMinusEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class PlusMinusEditor : UserControl
  {
    private Decimal maxValue = new Decimal(10);
    private Decimal step = new Decimal(1);
    private string formatString = "{0:F1}''";
    private string trimString = "''";
    private bool loop;
    private Decimal value;
    private Decimal minValue;
    private IContainer components;
    private RadRepeatButton minusButton;
    private RadRepeatButton plusButton;
    private RadTextBox radTextBox1;
    private RadPanel radPanel1;

    public event EventHandler ValueChanged;

    public RadRepeatButton MinusButton
    {
      get
      {
        return this.minusButton;
      }
    }

    public RadRepeatButton PlusButton
    {
      get
      {
        return this.plusButton;
      }
    }

    public RadTextBox TextBox
    {
      get
      {
        return this.radTextBox1;
      }
    }

    [DefaultValue(1)]
    public Decimal Value
    {
      get
      {
        return this.value;
      }
      set
      {
        Decimal num = Math.Max(this.minValue, Math.Min(this.maxValue, value));
        if (!(this.value != num))
          return;
        this.value = num;
        this.OnValueChanged();
      }
    }

    [DefaultValue("")]
    public string FormatString
    {
      get
      {
        return this.formatString;
      }
      set
      {
        if (!(this.formatString != value))
          return;
        this.formatString = value;
        this.radTextBox1.Text = string.Format(this.formatString, (object) this.Value);
      }
    }

    [DefaultValue("")]
    public string TrimString
    {
      get
      {
        return this.trimString;
      }
      set
      {
        this.trimString = value;
      }
    }

    [DefaultValue(0)]
    public Decimal MinValue
    {
      get
      {
        return this.minValue;
      }
      set
      {
        this.minValue = value;
      }
    }

    [DefaultValue(10)]
    public Decimal MaxValue
    {
      get
      {
        return this.maxValue;
      }
      set
      {
        this.maxValue = value;
      }
    }

    [DefaultValue(1)]
    public Decimal Step
    {
      get
      {
        return this.step;
      }
      set
      {
        this.step = value;
      }
    }

    [DefaultValue(false)]
    public bool Loop
    {
      get
      {
        return this.loop;
      }
      set
      {
        this.loop = value;
      }
    }

    public PlusMinusEditor()
    {
      this.InitializeComponent();
      this.minusButton.ButtonElement.Shape = (ElementShape) new RoundRectShape(2, true, true, false, false);
      this.minusButton.ButtonElement.Shape = (ElementShape) new RoundRectShape(2, true, true, false, false);
      this.plusButton.ButtonElement.Shape = (ElementShape) new RoundRectShape(2, false, false, true, true);
      this.plusButton.ButtonElement.Shape = (ElementShape) new RoundRectShape(2, false, false, true, true);
      this.radTextBox1.Text = string.Format(this.formatString, (object) this.Value);
      this.radPanel1.Padding = new Padding(0);
      this.radTextBox1.Validating += new CancelEventHandler(this.radTextBox1_Validating);
      this.radTextBox1.KeyDown += new KeyEventHandler(this.radTextBox1_KeyDown);
      this.radTextBox1.TextBoxItem.HostedControl.MouseWheel += new MouseEventHandler(this.radTextBox1_MouseWheel);
    }

    protected virtual void OnValueChanged()
    {
      this.radTextBox1.Text = string.Format(this.formatString, (object) this.Value);
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, EventArgs.Empty);
    }

    private void plusButton_Click(object sender, EventArgs e)
    {
      this.IncreaseValue();
    }

    private void minusButton_Click(object sender, EventArgs e)
    {
      this.DecreaseValue();
    }

    private void radTextBox1_MouseWheel(object sender, MouseEventArgs e)
    {
      if (e.Delta > 0)
        this.IncreaseValue();
      else
        this.DecreaseValue();
    }

    private void radTextBox1_Validating(object sender, CancelEventArgs e)
    {
      this.TryParseUserInput();
    }

    private void radTextBox1_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      this.TryParseUserInput();
    }

    public void IncreaseValue()
    {
      if (this.Loop && this.Value == this.MaxValue)
        this.Value = this.MinValue;
      else
        this.Value += this.Step;
    }

    public void DecreaseValue()
    {
      if (this.Loop && this.Value == this.MinValue)
        this.Value = this.MaxValue;
      else
        this.Value -= this.Step;
    }

    protected virtual void TryParseUserInput()
    {
      Decimal result = this.Value;
      string s = this.radTextBox1.Text.Trim();
      if (s.ToLower().EndsWith(this.TrimString.ToLower()) && !string.IsNullOrEmpty(this.TrimString))
        s = s.Remove(s.Length - this.TrimString.Length);
      if (Decimal.TryParse(s, out result))
        this.Value = result;
      this.radTextBox1.Text = string.Format(this.formatString, (object) this.Value);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.radTextBox1 = new RadTextBox();
      this.plusButton = new RadRepeatButton();
      this.minusButton = new RadRepeatButton();
      this.radPanel1 = new RadPanel();
      this.radTextBox1.BeginInit();
      this.plusButton.BeginInit();
      this.minusButton.BeginInit();
      this.radPanel1.BeginInit();
      this.radPanel1.SuspendLayout();
      this.SuspendLayout();
      this.radTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radTextBox1.AutoSize = false;
      this.radTextBox1.Location = new Point(26, 2);
      this.radTextBox1.Name = "radTextBox1";
      this.radTextBox1.RootElement.StretchVertically = true;
      this.radTextBox1.Size = new Size(62, 17);
      this.radTextBox1.TabIndex = 2;
      this.radTextBox1.TabStop = false;
      this.radTextBox1.Text = "text";
      this.radTextBox1.GetChildAt(0).StretchVertically = true;
      ((RadItem) this.radTextBox1.GetChildAt(0)).Text = "text";
      this.radTextBox1.GetChildAt(0).GetChildAt(0).Alignment = ContentAlignment.MiddleCenter;
      this.radTextBox1.GetChildAt(0).GetChildAt(2).Visibility = ElementVisibility.Collapsed;
      this.plusButton.Dock = DockStyle.Right;
      this.plusButton.Location = new Point(88, 1);
      this.plusButton.Name = "plusButton";
      this.plusButton.Size = new Size(25, 19);
      this.plusButton.TabIndex = 1;
      this.plusButton.Text = "+";
      this.plusButton.Click += new EventHandler(this.plusButton_Click);
      this.minusButton.Dock = DockStyle.Left;
      this.minusButton.Location = new Point(1, 1);
      this.minusButton.Name = "minusButton";
      this.minusButton.Size = new Size(25, 19);
      this.minusButton.TabIndex = 0;
      this.minusButton.Text = "-";
      this.minusButton.Click += new EventHandler(this.minusButton_Click);
      this.radPanel1.Controls.Add((Control) this.radTextBox1);
      this.radPanel1.Controls.Add((Control) this.plusButton);
      this.radPanel1.Controls.Add((Control) this.minusButton);
      this.radPanel1.Dock = DockStyle.Fill;
      this.radPanel1.Location = new Point(0, 0);
      this.radPanel1.Name = "radPanel1";
      this.radPanel1.Padding = new Padding(1);
      this.radPanel1.RootElement.Padding = new Padding(1);
      this.radPanel1.Size = new Size(114, 21);
      this.radPanel1.TabIndex = 3;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.Controls.Add((Control) this.radPanel1);
      this.Name = nameof (PlusMinusEditor);
      this.Size = new Size(114, 21);
      this.radTextBox1.EndInit();
      this.plusButton.EndInit();
      this.minusButton.EndInit();
      this.radPanel1.EndInit();
      this.radPanel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
