// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Keyboard.ChordKeysUI
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Telerik.WinControls.Keyboard
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class ChordKeysUI : UserControl
  {
    private bool updateTextValue = true;
    private List<string> keys = new List<string>();
    private static Keys[] validKeys = Telerik.WinControls.Keyboard.ChordConverter.ValidKeys;
    private bool autorepeatKey;
    private object originalValue;
    private Chord currentValue;
    private bool updateCurrentValue;
    private ChordsEditor editor;
    private IWindowsFormsEditorService edSvc;
    private TypeConverter chordConverter;
    private IDictionary keyNames;
    private IContainer components;
    private System.Windows.Forms.TextBox chordBox;
    private Button btnReset;
    private Button btnAssign;
    private System.Windows.Forms.Label lblModifiers;
    private CheckBox chkCtrl;
    private CheckBox chkShift;
    private CheckBox chkAlt;
    private System.Windows.Forms.Label lblKeys;

    public ChordKeysUI()
      : this((ChordsEditor) null)
    {
    }

    public ChordKeysUI(ChordsEditor editor)
    {
      this.editor = editor;
      this.chordConverter = (TypeConverter) null;
      this.currentValue = (Chord) null;
      this.End();
      this.InitializeComponent();
      this.keyNames = (IDictionary) new Hashtable(34);
      this.AddKeyMap("Enter", Keys.Return);
      this.AddKeyMap("Ctrl", Keys.Control);
      this.AddKeyMap("PgDn", Keys.Next);
      this.AddKeyMap("Ins", Keys.Insert);
      this.AddKeyMap("Del", Keys.Delete);
      this.AddKeyMap("PgUp", Keys.Prior);
      this.AddKeyMap("0", Keys.D0);
      this.AddKeyMap("1", Keys.D1);
      this.AddKeyMap("2", Keys.D2);
      this.AddKeyMap("3", Keys.D3);
      this.AddKeyMap("4", Keys.D4);
      this.AddKeyMap("5", Keys.D5);
      this.AddKeyMap("6", Keys.D6);
      this.AddKeyMap("7", Keys.D7);
      this.AddKeyMap("8", Keys.D8);
      this.AddKeyMap("9", Keys.D9);
    }

    public void Start(IWindowsFormsEditorService edSvc, object value)
    {
      this.edSvc = edSvc;
      this.currentValue = new Chord();
      this.updateCurrentValue = false;
      if (value is Chord)
      {
        this.chkAlt.Checked = this.currentValue.ChordModifier.AltModifier = (value as Chord).ChordModifier.AltModifier;
        this.chkCtrl.Checked = this.currentValue.ChordModifier.ControlModifier = (value as Chord).ChordModifier.ControlModifier;
        this.chkShift.Checked = this.currentValue.ChordModifier.ShiftModifier = (value as Chord).ChordModifier.ShiftModifier;
      }
      this.originalValue = value;
      this.updateCurrentValue = true;
      if (!(value is Chord))
        return;
      this.chordBox.Text = (value as Chord).Keys;
    }

    public void End()
    {
      this.edSvc = (IWindowsFormsEditorService) null;
      this.Reset();
      this.originalValue = (object) null;
      this.currentValue = (Chord) null;
    }

    public void Reset()
    {
      this.updateCurrentValue = false;
      if (this.chkCtrl != null)
        this.chkCtrl.Checked = false;
      if (this.chkShift != null)
        this.chkShift.Checked = false;
      if (this.chkAlt != null)
        this.chkAlt.Checked = false;
      if (this.chordBox != null)
        this.chordBox.ResetText();
      this.updateCurrentValue = true;
      this.keys.Clear();
    }

    private void ModifierChanged(object sender, EventArgs e)
    {
      this.UpdateCurrentValue();
    }

    private void ResetClick(object sender, EventArgs e)
    {
      this.Reset();
      if (this.currentValue != null)
        this.currentValue.Clear();
      this.UpdateCurrentValue();
    }

    private void AssignClick(object sender, EventArgs e)
    {
      this.UpdateCurrentValue();
      if (this.EditorService == null)
        return;
      this.EditorService.CloseDropDown();
    }

    private void ChordBoxTextChanged(object sender, EventArgs e)
    {
      this.UpdateCurrentValue();
    }

    private void UpdateCurrentValue()
    {
      if (!this.updateCurrentValue || this.currentValue == null)
        return;
      this.currentValue.ChordModifier.AltModifier = this.chkAlt.Checked;
      this.currentValue.ChordModifier.ControlModifier = this.chkCtrl.Checked;
      this.currentValue.ChordModifier.ShiftModifier = this.chkShift.Checked;
      if (!this.updateTextValue)
        return;
      this.currentValue.Keys = this.chordBox.Text;
      this.updateTextValue = false;
      this.chordBox.Clear();
      if (!string.IsNullOrEmpty(this.currentValue.Keys))
        this.chordBox.AppendText(this.currentValue.Keys);
      this.updateTextValue = true;
    }

    private static bool IsValidKey(Keys keyCode)
    {
      for (int index = 0; index < ChordKeysUI.validKeys.Length; ++index)
      {
        if (ChordKeysUI.validKeys[index] == keyCode)
          return true;
      }
      return false;
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.chkCtrl.Focus();
    }

    protected override bool ProcessDialogKey(Keys keyData)
    {
      Keys keys1 = keyData & Keys.KeyCode;
      Keys keys2 = keyData & Keys.Modifiers;
      switch (keys1)
      {
        case Keys.Tab:
          if (keys2 == Keys.Shift && this.chordBox.Focused)
          {
            this.btnReset.Focus();
            return true;
          }
          break;
        case Keys.Escape:
          if (!this.chordBox.Focused || (keys2 & (Keys.Control | Keys.Alt)) != Keys.None)
          {
            this.currentValue = this.originalValue as Chord;
            break;
          }
          break;
        default:
          if (!this.chordBox.Focused || (keys2 & (Keys.Control | Keys.Alt)) != Keys.None)
            return base.ProcessDialogKey(keyData);
          break;
      }
      return base.ProcessDialogKey(keyData);
    }

    private void ChordBoxKeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void ChordBoxKeyDown(object sender, KeyEventArgs e)
    {
      Keys keys1 = e.KeyCode & Keys.KeyCode;
      if (e.Modifiers != Keys.None)
      {
        if ((keys1 | Keys.Menu) != Keys.Menu && (keys1 | Keys.ControlKey) != Keys.ControlKey && (keys1 | Keys.ShiftKey) != Keys.ShiftKey)
          return;
        if (e.Alt)
          this.chkAlt.Checked = true;
        if (e.Control)
          this.chkCtrl.Checked = true;
        if (e.Shift)
          this.chkShift.Checked = true;
        this.updateCurrentValue = false;
        this.UpdateCurrentValue();
        this.updateCurrentValue = true;
      }
      else
      {
        string str = string.Empty;
        foreach (string key in (IEnumerable) this.keyNames.Keys)
        {
          Keys keyName = (Keys) this.keyNames[(object) key];
          if ((keys1 & keyName) == keyName)
          {
            str = key;
            break;
          }
        }
        if (string.IsNullOrEmpty(str))
          str = keys1.ToString();
        if (this.autorepeatKey)
        {
          int index = this.keys.Count == 0 ? 0 : this.keys.Count - 1;
          Keys keys2 = Keys.None;
          try
          {
            object obj = Enum.Parse(typeof (Keys), this.keys[index]);
            if (obj != null)
              keys2 = (Keys) obj;
          }
          catch (ArgumentException ex)
          {
          }
          if (keys2 != keys1)
            this.autorepeatKey = false;
        }
        if (!this.autorepeatKey)
        {
          this.chordBox.Select(this.chordBox.Text.Length, this.chordBox.Text.Length);
          this.keys.Add(str);
          this.autorepeatKey = true;
        }
        else
          e.SuppressKeyPress = true;
      }
    }

    private void ChordBoxKeyUp(object sender, KeyEventArgs e)
    {
      this.autorepeatKey = false;
    }

    private void AddKeyMap(string key, Keys value)
    {
      this.keyNames[(object) key] = (object) value;
    }

    public IWindowsFormsEditorService EditorService
    {
      get
      {
        return this.edSvc;
      }
    }

    private TypeConverter ChordConverter
    {
      get
      {
        if (this.chordConverter == null)
          this.chordConverter = TypeDescriptor.GetConverter(typeof (Chord));
        return this.chordConverter;
      }
    }

    public object Value
    {
      get
      {
        if (this.chordBox.Text.Length > 0)
          return (object) this.currentValue;
        return (object) null;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Button AssignButton
    {
      get
      {
        return this.btnAssign;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public System.Windows.Forms.TextBox ChordBox
    {
      get
      {
        return this.chordBox;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.chordBox = new System.Windows.Forms.TextBox();
      this.btnReset = new Button();
      this.btnAssign = new Button();
      this.lblModifiers = new System.Windows.Forms.Label();
      this.chkCtrl = new CheckBox();
      this.chkShift = new CheckBox();
      this.chkAlt = new CheckBox();
      this.lblKeys = new System.Windows.Forms.Label();
      this.SuspendLayout();
      this.chordBox.Location = new Point(28, 70);
      this.chordBox.Name = "chordBox";
      this.chordBox.Size = new Size(178, 20);
      this.chordBox.TabIndex = 0;
      this.chordBox.KeyUp += new KeyEventHandler(this.ChordBoxKeyUp);
      this.chordBox.KeyPress += new KeyPressEventHandler(this.ChordBoxKeyPress);
      this.chordBox.TextChanged += new EventHandler(this.ChordBoxTextChanged);
      this.chordBox.KeyDown += new KeyEventHandler(this.ChordBoxKeyDown);
      this.btnReset.Location = new Point(131, 105);
      this.btnReset.Name = "btnReset";
      this.btnReset.Size = new Size(75, 23);
      this.btnReset.TabIndex = 1;
      this.btnReset.Text = "Reset";
      this.btnReset.UseVisualStyleBackColor = true;
      this.btnReset.Click += new EventHandler(this.ResetClick);
      this.btnAssign.Location = new Point(28, 105);
      this.btnAssign.Name = "btnAssign";
      this.btnAssign.Size = new Size(75, 23);
      this.btnAssign.TabIndex = 2;
      this.btnAssign.Text = "Assign";
      this.btnAssign.UseVisualStyleBackColor = true;
      this.btnAssign.Click += new EventHandler(this.AssignClick);
      this.lblModifiers.AutoSize = true;
      this.lblModifiers.Location = new Point(17, 4);
      this.lblModifiers.Name = "lblModifiers";
      this.lblModifiers.Size = new Size(52, 13);
      this.lblModifiers.TabIndex = 3;
      this.lblModifiers.Text = "Modifiers:";
      this.chkCtrl.AutoSize = true;
      this.chkCtrl.Location = new Point(28, 20);
      this.chkCtrl.Name = "chkCtrl";
      this.chkCtrl.Size = new Size(41, 17);
      this.chkCtrl.TabIndex = 4;
      this.chkCtrl.Text = "&Ctrl";
      this.chkCtrl.UseVisualStyleBackColor = true;
      this.chkCtrl.CheckedChanged += new EventHandler(this.ModifierChanged);
      this.chkShift.AutoSize = true;
      this.chkShift.Location = new Point(95, 20);
      this.chkShift.Name = "chkShift";
      this.chkShift.Size = new Size(47, 17);
      this.chkShift.TabIndex = 5;
      this.chkShift.Text = "&Shift";
      this.chkShift.UseVisualStyleBackColor = true;
      this.chkShift.CheckedChanged += new EventHandler(this.ModifierChanged);
      this.chkAlt.AutoSize = true;
      this.chkAlt.Location = new Point(168, 20);
      this.chkAlt.Name = "chkAlt";
      this.chkAlt.Size = new Size(38, 17);
      this.chkAlt.TabIndex = 6;
      this.chkAlt.Text = "&Alt";
      this.chkAlt.UseVisualStyleBackColor = true;
      this.chkAlt.CheckedChanged += new EventHandler(this.ModifierChanged);
      this.lblKeys.AutoSize = true;
      this.lblKeys.Location = new Point(17, 54);
      this.lblKeys.Name = "lblKeys";
      this.lblKeys.Size = new Size(33, 13);
      this.lblKeys.TabIndex = 7;
      this.lblKeys.Text = "Keys:";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.Controls.Add((Control) this.lblKeys);
      this.Controls.Add((Control) this.chkAlt);
      this.Controls.Add((Control) this.chkShift);
      this.Controls.Add((Control) this.chkCtrl);
      this.Controls.Add((Control) this.lblModifiers);
      this.Controls.Add((Control) this.btnAssign);
      this.Controls.Add((Control) this.btnReset);
      this.Controls.Add((Control) this.chordBox);
      this.Name = nameof (ChordKeysUI);
      this.Padding = new Padding(4);
      this.Size = new Size(229, 150);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
