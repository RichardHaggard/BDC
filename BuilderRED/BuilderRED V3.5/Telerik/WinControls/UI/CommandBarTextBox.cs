// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarTextBox
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
  public class CommandBarTextBox : RadCommandBarBaseItem
  {
    private RadTextBoxElement textBoxElement;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.StretchHorizontally = this.StretchVertically = false;
      this.textBoxElement = new RadTextBoxElement();
      this.textBoxElement.MinSize = new Size(60, 22);
      this.textBoxElement.Class = "RadCommandBarTextBoxElement";
      this.textBoxElement.Alignment = ContentAlignment.MiddleLeft;
      this.Children.Add((RadElement) this.textBoxElement);
    }

    [DefaultValue(true)]
    [Browsable(true)]
    [Description("Indicates whether the item should be drawn in the strip.")]
    [Category("Appearance")]
    public override bool VisibleInStrip
    {
      get
      {
        return base.VisibleInStrip;
      }
      set
      {
        base.VisibleInStrip = value;
        if (this.textBoxElement == null)
          return;
        int num = (int) this.textBoxElement.SetValue(RadElement.VisibilityProperty, (object) (ElementVisibility) (value ? 0 : 2));
      }
    }

    [Description("Represent the textbox inside into CommandBarTextBox")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadTextBoxElement TextBoxElement
    {
      get
      {
        return this.textBoxElement;
      }
      set
      {
        this.textBoxElement = value;
      }
    }

    [Localizable(true)]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the text associated with this item.")]
    [Bindable(true)]
    [SettingsBindable(true)]
    [DefaultValue("")]
    public override string Text
    {
      get
      {
        if (this.textBoxElement != null)
          return this.textBoxElement.TextBoxItem.Text;
        return base.Text;
      }
      set
      {
        if (this.textBoxElement != null)
          this.TextBoxElement.TextBoxItem.Text = value;
        base.Text = value;
      }
    }

    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    [Category("Behavior")]
    [DefaultValue('\0')]
    public char PasswordChar
    {
      get
      {
        return this.textBoxElement.PasswordChar;
      }
      set
      {
        this.textBoxElement.PasswordChar = value;
      }
    }

    [RadPropertyDefaultValue("NullText", typeof (RadTextBoxItem))]
    [Description("Gets or sets the prompt text that is displayed when the TextBox  contains no text")]
    public string NullText
    {
      get
      {
        return this.TextBoxElement.TextBoxItem.NullText;
      }
      set
      {
        this.TextBoxElement.TextBoxItem.NullText = value;
      }
    }

    [Description("Gets or sets the color of prompt text that is displayed when the TextBox  contains no text")]
    [RadPropertyDefaultValue("NullTextColor", typeof (RadTextBoxItem))]
    public Color NullTextColor
    {
      get
      {
        return this.TextBoxElement.TextBoxItem.NullTextColor;
      }
      set
      {
        this.TextBoxElement.TextBoxItem.NullTextColor = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the number of characters selected in the editable portion of the textbox.")]
    [Browsable(false)]
    public int SelectionLength
    {
      get
      {
        return this.TextBoxElement.TextBoxItem.TextBoxControl.SelectionLength;
      }
      set
      {
        this.TextBoxElement.TextBoxItem.TextBoxControl.SelectionLength = value;
      }
    }

    [Description("Gets or sets the starting index of text selected in the textbox.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int SelectionStart
    {
      get
      {
        return this.TextBoxElement.TextBoxItem.TextBoxControl.SelectionStart;
      }
      set
      {
        this.TextBoxElement.TextBoxItem.TextBoxControl.SelectionStart = value;
      }
    }

    public void AppendText(string text)
    {
      this.TextBoxElement.TextBoxItem.TextBoxControl.AppendText(text);
    }

    public void Clear()
    {
      this.TextBoxElement.TextBoxItem.TextBoxControl.Clear();
    }

    public void ClearUndo()
    {
      this.TextBoxElement.TextBoxItem.TextBoxControl.ClearUndo();
    }

    public void Copy()
    {
      this.TextBoxElement.TextBoxItem.TextBoxControl.Copy();
    }

    public void Cut()
    {
      this.TextBoxElement.TextBoxItem.TextBoxControl.Cut();
    }

    public void DeselectAll()
    {
      this.TextBoxElement.TextBoxItem.TextBoxControl.DeselectAll();
    }

    public char GetCharFromPosition(Point point)
    {
      return this.TextBoxElement.TextBoxItem.TextBoxControl.GetCharFromPosition(point);
    }

    public int GetCharIndexFromPosition(Point point)
    {
      return this.TextBoxElement.TextBoxItem.TextBoxControl.GetCharIndexFromPosition(point);
    }

    public int GetFirstCharIndexFromLine(int lineNumber)
    {
      return this.TextBoxElement.TextBoxItem.TextBoxControl.GetFirstCharIndexFromLine(lineNumber);
    }

    public int GetFirstCharIndexOfCurrentLine()
    {
      return this.TextBoxElement.TextBoxItem.TextBoxControl.GetFirstCharIndexOfCurrentLine();
    }

    public int GetLineFromCharIndex(int index)
    {
      return this.TextBoxElement.TextBoxItem.TextBoxControl.GetLineFromCharIndex(index);
    }

    public Point GetPositionFromCharIndex(int index)
    {
      return this.TextBoxElement.TextBoxItem.TextBoxControl.GetPositionFromCharIndex(index);
    }

    public void Paste()
    {
      this.TextBoxElement.TextBoxItem.TextBoxControl.Paste();
    }

    public void Paste(string text)
    {
      this.TextBoxElement.TextBoxItem.TextBoxControl.Paste(text);
    }

    public void ScrollToCaret()
    {
      this.TextBoxElement.TextBoxItem.TextBoxControl.ScrollToCaret();
    }

    public void Select(int start, int length)
    {
      this.TextBoxElement.TextBoxItem.TextBoxControl.Select(start, length);
    }

    public void SelectAll()
    {
      this.TextBoxElement.TextBoxItem.TextBoxControl.SelectAll();
    }

    public void Select()
    {
      this.TextBoxElement.TextBoxItem.HostedControl.Select();
    }

    [Description("Occurs when the RadItem has focus and the user pressees a key")]
    [Category("Key")]
    public new event KeyPressEventHandler KeyPress
    {
      add
      {
        this.textBoxElement.TextBoxItem.KeyPress += value;
      }
      remove
      {
        this.textBoxElement.TextBoxItem.KeyPress -= value;
      }
    }

    [Description("Occurs when the RadItem has focus and the user releases the pressed key up")]
    [Category("Key")]
    public new event KeyEventHandler KeyUp
    {
      add
      {
        this.textBoxElement.TextBoxItem.KeyUp += value;
      }
      remove
      {
        this.textBoxElement.TextBoxItem.KeyUp -= value;
      }
    }

    [Category("Key")]
    [Description("Occurs when the RadItem has focus and the user pressees a key down")]
    public new event KeyEventHandler KeyDown
    {
      add
      {
        this.textBoxElement.TextBoxItem.KeyDown += value;
      }
      remove
      {
        this.textBoxElement.TextBoxItem.KeyDown -= value;
      }
    }

    [Description("Occurs when the Text property value is about to be changed.")]
    [Category("Property Changed")]
    public new event TextChangingEventHandler TextChanging
    {
      add
      {
        this.textBoxElement.TextBoxItem.TextChanging += value;
      }
      remove
      {
        this.textBoxElement.TextBoxItem.TextChanging -= value;
      }
    }

    [Description("Occurs when the Text property value changes.")]
    [Category("Property Changed")]
    public new event EventHandler TextChanged
    {
      add
      {
        this.textBoxElement.TextBoxItem.TextChanged += value;
      }
      remove
      {
        this.textBoxElement.TextBoxItem.TextChanged -= value;
      }
    }

    [Description("Occurs when the element recieves focus.")]
    [Browsable(false)]
    [Category("Property Changed")]
    public event EventHandler GotFocus
    {
      add
      {
        this.textBoxElement.TextBoxItem.GotFocus += value;
      }
      remove
      {
        this.textBoxElement.TextBoxItem.GotFocus -= value;
      }
    }

    [Description("Occurs when the element loses focus.")]
    [Category("Property Changed")]
    [Browsable(false)]
    public event EventHandler LostFocus
    {
      add
      {
        this.textBoxElement.TextBoxItem.LostFocus += value;
      }
      remove
      {
        this.textBoxElement.TextBoxItem.LostFocus -= value;
      }
    }
  }
}
