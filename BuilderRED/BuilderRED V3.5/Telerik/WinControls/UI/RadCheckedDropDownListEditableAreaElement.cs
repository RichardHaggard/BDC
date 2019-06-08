// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCheckedDropDownListEditableAreaElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  public class RadCheckedDropDownListEditableAreaElement : RadDropDownListEditableAreaElement
  {
    private CheckedDropDownAutoCompleteBoxElement autoCompleteTextBox;

    public RadCheckedDropDownListEditableAreaElement(RadCheckedDropDownListElement owner)
      : base((RadDropDownListElement) owner)
    {
      this.autoCompleteTextBox.OwnerElement = owner;
    }

    public CheckedDropDownAutoCompleteBoxElement AutoCompleteTextBox
    {
      get
      {
        return this.autoCompleteTextBox;
      }
      set
      {
        this.autoCompleteTextBox = value;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.autoCompleteTextBox = new CheckedDropDownAutoCompleteBoxElement();
      this.autoCompleteTextBox.DrawBorder = false;
      this.autoCompleteTextBox.LostMouseCapture += new MouseEventHandler(this.TextBoxControl_MouseCaptureChanged);
      this.autoCompleteTextBox.MouseUp += new MouseEventHandler(this.textBox_MouseUp);
      this.autoCompleteTextBox.KeyPress += new KeyPressEventHandler(this.textBox_KeyPress);
      this.autoCompleteTextBox.KeyUp += new KeyEventHandler(this.textBox_KeyUp);
      this.autoCompleteTextBox.KeyDown += new KeyEventHandler(this.textBox_KeyDown);
      this.autoCompleteTextBox.TextChanged += new EventHandler(this.textBox_TextChanged);
      this.autoCompleteTextBox.MouseWheel += new MouseEventHandler(this.textBox_MouseWheel);
      this.autoCompleteTextBox.DoubleClick += new EventHandler(this.textBox_DoubleClick);
      this.DrawBorder = false;
      this.BorderBoxStyle = BorderBoxStyle.SingleBorder;
      this.BorderWidth = 0.0f;
      this.DrawText = false;
      this.Children.Remove((RadElement) base.TextBox);
      this.Children.Add((RadElement) this.autoCompleteTextBox);
    }

    public override RadDropDownTextBoxElement TextBox
    {
      get
      {
        return base.TextBox;
      }
      set
      {
        base.TextBox = value;
      }
    }

    public override RadDropDownStyle DropDownStyle
    {
      get
      {
        return RadDropDownStyle.DropDown;
      }
      set
      {
      }
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadDropDownListEditableAreaElement);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the text that is selected in the editable portion of the ComboBox.")]
    public override string SelectedText
    {
      get
      {
        return this.autoCompleteTextBox.SelectedText;
      }
      set
      {
        this.autoCompleteTextBox.SelectedText = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the number of characters selected in the editable portion of the combo box.")]
    [Browsable(false)]
    public override int SelectionLength
    {
      get
      {
        return this.autoCompleteTextBox.SelectionLength;
      }
      set
      {
        this.autoCompleteTextBox.SelectionLength = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the starting index of text selected in the combo box.")]
    public override int SelectionStart
    {
      get
      {
        return this.autoCompleteTextBox.SelectionStart;
      }
      set
      {
        this.autoCompleteTextBox.SelectionStart = value;
      }
    }

    [Category("Behavior")]
    [RadDefaultValue("NullText", typeof (RadTextBoxItem))]
    [RadDescription("NullText", typeof (RadTextBoxItem))]
    [Localizable(true)]
    public override string NullText
    {
      get
      {
        return this.autoCompleteTextBox.NullText;
      }
      set
      {
        this.autoCompleteTextBox.NullText = value;
      }
    }

    [Category("Behavior")]
    public override int MaxLength
    {
      get
      {
        return this.autoCompleteTextBox.MaxLength;
      }
      set
      {
        this.autoCompleteTextBox.MaxLength = value;
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.owner.NotifyOwner(new PopupEditorNotificationData(e)
      {
        notificationContext = PopupEditorNotificationData.Context.MouseUpOnEditorElement
      });
    }

    private void textBox_MouseUp(object sender, MouseEventArgs e)
    {
      if (this.DropDownStyle != RadDropDownStyle.DropDown || !this.Entering)
        return;
      this.SelectAll();
      this.Entering = false;
    }

    private void textBox_KeyUp(object sender, KeyEventArgs e)
    {
      this.owner.NotifyOwner(new PopupEditorNotificationData(e)
      {
        notificationContext = PopupEditorNotificationData.Context.KeyUp
      });
      if (e.Handled)
        return;
      this.owner.OnKeyUp(e);
      this.OnKeyUp(e);
    }

    private bool SelectAllText(string text)
    {
      if (!this.autoCompleteTextBox.IsFocused)
        return false;
      this.SelectionStart = 0;
      this.SelectionLength = string.IsNullOrEmpty(text) ? 0 : text.Length;
      return true;
    }

    private void TextBoxControl_MouseCaptureChanged(object sender, EventArgs e)
    {
      this.onTextBoxCaptureChanged = true;
      if (!this.autoCompleteTextBox.Capture || this.SelectAllText(this.Text))
        return;
      this.autoCompleteTextBox.DeselectAll();
    }

    private void textBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.owner.OnKeyPress(e);
      if (e.KeyChar == '\x001B')
      {
        this.HandleEsc();
        e.Handled = true;
      }
      else
        this.owner.NotifyOwner(new PopupEditorNotificationData(e));
    }

    private void textBox_TextChanged(object sender, EventArgs e)
    {
      this.owner.NotifyOwner(new PopupEditorNotificationData()
      {
        notificationContext = PopupEditorNotificationData.Context.TextChanged
      });
    }

    private void TextBoxItem_LostFocus(object sender, EventArgs e)
    {
      this.onTextBoxCaptureChanged = false;
      this.owner.NotifyOwner(new PopupEditorNotificationData()
      {
        notificationContext = PopupEditorNotificationData.Context.MouseEvent
      });
    }

    private void textBox_MouseWheel(object sender, MouseEventArgs e)
    {
      this.owner.NotifyOwner(new PopupEditorNotificationData(e)
      {
        notificationContext = PopupEditorNotificationData.Context.MouseWheel
      });
    }

    private void textBox_DoubleClick(object sender, EventArgs e)
    {
      this.owner.NotifyOwner(new PopupEditorNotificationData()
      {
        notificationContext = PopupEditorNotificationData.Context.TextBoxDoubleClick
      });
    }

    private void textBox_KeyDown(object sender, KeyEventArgs e)
    {
      this.owner.NotifyOwner(new PopupEditorNotificationData(e)
      {
        notificationContext = PopupEditorNotificationData.Context.KeyDown
      });
      if (e.Handled)
        return;
      this.owner.OnKeyDown(e);
      if (e.Handled)
        return;
      if (e.KeyCode == Keys.F4 || e.Alt && e.KeyCode == Keys.Down)
        this.HandleF4Down();
      else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
      {
        this.HandleKeyUpKeyDown(e);
      }
      else
      {
        if (e.KeyCode != Keys.Escape)
          return;
        this.HandleEsc();
        e.Handled = true;
      }
    }

    public override void Select(int start, int length)
    {
      this.autoCompleteTextBox.Select(start, length);
    }

    public override void SelectAll()
    {
    }

    public override bool Focus()
    {
      return this.autoCompleteTextBox.Focus();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      return base.MeasureOverride(availableSize);
    }
  }
}
