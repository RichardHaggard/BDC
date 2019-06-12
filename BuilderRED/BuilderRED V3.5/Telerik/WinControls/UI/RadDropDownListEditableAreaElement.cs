// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownListEditableAreaElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadDropDownListEditableAreaElement : LightVisualElement
  {
    private RadDropDownStyle dropDownStyle = RadDropDownStyle.DropDown;
    private string oldText = string.Empty;
    protected RadDropDownListElement owner;
    private RadDropDownTextBoxElement textBox;
    protected bool onTextBoxCaptureChanged;
    protected bool entering;

    static RadDropDownListEditableAreaElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new GroupBoxVisualElementStateManagerFactory(), typeof (RadDropDownListEditableAreaElement));
    }

    public RadDropDownListEditableAreaElement()
      : this((RadDropDownListElement) null)
    {
    }

    public RadDropDownListEditableAreaElement(RadDropDownListElement owner)
    {
      this.owner = owner;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Alignment = ContentAlignment.MiddleLeft;
      this.TextAlignment = ContentAlignment.MiddleLeft;
      this.BackColor = Color.White;
      this.DrawFill = true;
      this.DrawText = false;
      this.NumberOfColors = 1;
      this.StretchHorizontally = true;
      this.StretchVertically = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.textBox = new RadDropDownTextBoxElement();
      this.Children.Add((RadElement) this.textBox);
      int num = (int) this.BindProperty(RadItem.TextProperty, (RadObject) this.textBox, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      this.WireEvents();
    }

    protected override void DisposeManagedResources()
    {
      this.UnWireEvents();
      base.DisposeManagedResources();
    }

    protected virtual void WireEvents()
    {
      this.WireEditorKeysEvets();
      this.DoubleClick += new EventHandler(this.textBox_DoubleClick);
      this.textBox.Pasted += new EventHandler(this.textBox_Pasted);
      this.textBox.TextBoxItem.DoubleClick += new EventHandler(this.textBox_DoubleClick);
      this.textBox.TextChanged += new EventHandler(this.textBox_TextChanged);
      this.textBox.TextChanging += new TextChangingEventHandler(this.TextBox_TextChanging);
      this.textBox.TextBoxItem.TextBoxControl.LostFocus += new EventHandler(this.TextBoxItem_LostFocus);
      this.TextBox.TextBoxItem.TextBoxControl.MouseCaptureChanged += new EventHandler(this.TextBoxControl_MouseCaptureChanged);
      this.textBox.TextBoxItem.MouseWheel += new MouseEventHandler(this.textBox_MouseWheel);
    }

    protected virtual void UnWireEvents()
    {
      this.UnWireEditorKeysEvets();
      this.DoubleClick -= new EventHandler(this.textBox_DoubleClick);
      this.textBox.Pasted -= new EventHandler(this.textBox_Pasted);
      this.textBox.TextBoxItem.DoubleClick -= new EventHandler(this.textBox_DoubleClick);
      this.textBox.TextChanged -= new EventHandler(this.textBox_TextChanged);
      this.textBox.TextChanging -= new TextChangingEventHandler(this.TextBox_TextChanging);
      this.textBox.TextBoxItem.TextBoxControl.LostFocus -= new EventHandler(this.TextBoxItem_LostFocus);
      this.TextBox.TextBoxItem.TextBoxControl.MouseCaptureChanged -= new EventHandler(this.TextBoxControl_MouseCaptureChanged);
      this.textBox.TextBoxItem.MouseWheel -= new MouseEventHandler(this.textBox_MouseWheel);
    }

    internal bool Entering
    {
      get
      {
        return this.entering;
      }
      set
      {
        this.entering = value;
      }
    }

    internal string OldText
    {
      get
      {
        return this.oldText;
      }
      set
      {
        this.oldText = value;
      }
    }

    public virtual RadDropDownTextBoxElement TextBox
    {
      get
      {
        return this.textBox;
      }
      set
      {
        this.textBox = value;
      }
    }

    public virtual RadDropDownStyle DropDownStyle
    {
      get
      {
        return this.dropDownStyle;
      }
      set
      {
        if (this.dropDownStyle == value)
          return;
        this.UnWireEditorKeysEvets();
        this.dropDownStyle = value;
        this.WireEditorKeysEvets();
        this.SetDropDownStyle();
      }
    }

    [Description("Gets or sets the text that is selected in the editable portion of the ComboBox.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual string SelectedText
    {
      get
      {
        if (this.DropDownStyle != RadDropDownStyle.DropDownList)
          return this.textBox.TextBoxItem.SelectedText;
        return string.Empty;
      }
      set
      {
        if (this.DropDownStyle == RadDropDownStyle.DropDownList)
          return;
        this.textBox.TextBoxItem.SelectedText = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the number of characters selected in the editable portion of the combo box.")]
    public virtual int SelectionLength
    {
      get
      {
        return this.textBox.TextBoxItem.SelectionLength;
      }
      set
      {
        this.textBox.TextBoxItem.SelectionLength = value;
      }
    }

    [Description("Gets or sets the starting index of text selected in the combo box.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual int SelectionStart
    {
      get
      {
        return this.textBox.TextBoxItem.SelectionStart;
      }
      set
      {
        this.textBox.TextBoxItem.SelectionStart = value;
      }
    }

    [Category("Behavior")]
    [RadDefaultValue("NullText", typeof (RadTextBoxItem))]
    [RadDescription("NullText", typeof (RadTextBoxItem))]
    [Localizable(true)]
    public virtual string NullText
    {
      get
      {
        return this.textBox.TextBoxItem.NullText;
      }
      set
      {
        this.textBox.TextBoxItem.NullText = value;
      }
    }

    [Category("Behavior")]
    public virtual int MaxLength
    {
      get
      {
        return this.textBox.TextBoxItem.MaxLength;
      }
      set
      {
        this.textBox.TextBoxItem.MaxLength = value;
      }
    }

    private void textBox_Pasted(object sender, EventArgs e)
    {
      if (this.owner == null)
        return;
      this.owner.NotifyOwner(new PopupEditorNotificationData()
      {
        notificationContext = PopupEditorNotificationData.Context.Clipboard
      });
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.owner == null)
        return;
      base.OnMouseUp(e);
      if (this.dropDownStyle == RadDropDownStyle.DropDown)
        return;
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
      if (this.owner == null)
        return;
      this.owner.NotifyOwner(new PopupEditorNotificationData(e)
      {
        notificationContext = PopupEditorNotificationData.Context.KeyUp
      });
      if (e.Handled)
        return;
      this.owner.OnKeyUp(e);
      if (this.dropDownStyle != RadDropDownStyle.DropDown)
        return;
      this.OnKeyUp(e);
    }

    private void TextBoxControl_MouseCaptureChanged(object sender, EventArgs e)
    {
      if (this.onTextBoxCaptureChanged)
        return;
      this.onTextBoxCaptureChanged = true;
      if (!this.textBox.TextBoxItem.HostedControl.Capture || this.SelectAllText(this.Text))
        return;
      this.textBox.TextBoxItem.TextBoxControl.DeselectAll();
    }

    private void textBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (this.owner == null)
        return;
      this.owner.OnKeyPress(e);
      if (e.KeyChar == '\x001B')
      {
        this.HandleEsc();
        e.Handled = true;
      }
      else
        this.owner.NotifyOwner(new PopupEditorNotificationData(e));
    }

    private void TextBox_TextChanging(object sender, TextChangingEventArgs e)
    {
      this.oldText = e.OldValue;
    }

    private void textBox_TextChanged(object sender, EventArgs e)
    {
      if (this.owner == null)
        return;
      this.owner.NotifyOwner(new PopupEditorNotificationData()
      {
        notificationContext = PopupEditorNotificationData.Context.TextChanged
      });
    }

    private void TextBoxItem_LostFocus(object sender, EventArgs e)
    {
      if (this.owner == null)
        return;
      this.onTextBoxCaptureChanged = false;
      this.owner.NotifyOwner(new PopupEditorNotificationData()
      {
        notificationContext = PopupEditorNotificationData.Context.MouseEvent
      });
    }

    private void textBox_MouseWheel(object sender, MouseEventArgs e)
    {
      if (this.owner == null)
        return;
      this.owner.NotifyOwner(new PopupEditorNotificationData(e)
      {
        notificationContext = PopupEditorNotificationData.Context.MouseWheel
      });
    }

    private void textBox_DoubleClick(object sender, EventArgs e)
    {
      if (this.owner == null)
        return;
      this.owner.NotifyOwner(new PopupEditorNotificationData()
      {
        notificationContext = PopupEditorNotificationData.Context.TextBoxDoubleClick
      });
    }

    private void textBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (this.owner == null)
        return;
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

    public virtual void Select(int start, int length)
    {
      this.textBox.TextBoxItem.Select(start, length);
    }

    public virtual void SelectAll()
    {
      this.textBox.TextBoxItem.SelectAll();
    }

    public override bool Focus()
    {
      switch (this.dropDownStyle)
      {
        case RadDropDownStyle.DropDown:
          return this.textBox.TextBoxItem.TextBoxControl.Focus();
        case RadDropDownStyle.DropDownList:
          return base.Focus();
        default:
          return false;
      }
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      if (this.owner == null)
        return;
      base.OnMouseWheel(e);
      this.owner.OnMouseWheelCore(e);
    }

    protected virtual void SetDropDownStyle()
    {
      switch (this.dropDownStyle)
      {
        case RadDropDownStyle.DropDown:
          this.textBox.Visibility = ElementVisibility.Visible;
          this.DrawText = false;
          this.SetTabStop(false);
          break;
        case RadDropDownStyle.DropDownList:
          this.textBox.Visibility = ElementVisibility.Hidden;
          this.DrawText = true;
          this.SetTabStop(true);
          break;
      }
    }

    private void SetTabStop(bool tabStop)
    {
      if (this.ElementTree == null || this.ElementTree.Control == null)
        return;
      Control control = this.ElementTree.Control;
      if (!(control is RadDropDownList))
        return;
      control.TabStop = tabStop;
    }

    private void WireEditorKeysEvets()
    {
      switch (this.dropDownStyle)
      {
        case RadDropDownStyle.DropDown:
          this.textBox.KeyDown += new KeyEventHandler(this.textBox_KeyDown);
          this.textBox.KeyPress += new KeyPressEventHandler(this.textBox_KeyPress);
          this.textBox.KeyUp += new KeyEventHandler(this.textBox_KeyUp);
          this.textBox.MouseUp += new MouseEventHandler(this.textBox_MouseUp);
          break;
        case RadDropDownStyle.DropDownList:
          this.KeyDown += new KeyEventHandler(this.textBox_KeyDown);
          this.KeyPress += new KeyPressEventHandler(this.textBox_KeyPress);
          this.KeyUp += new KeyEventHandler(this.textBox_KeyUp);
          break;
      }
    }

    private void UnWireEditorKeysEvets()
    {
      switch (this.dropDownStyle)
      {
        case RadDropDownStyle.DropDown:
          this.textBox.KeyDown -= new KeyEventHandler(this.textBox_KeyDown);
          this.textBox.KeyPress -= new KeyPressEventHandler(this.textBox_KeyPress);
          this.textBox.KeyUp -= new KeyEventHandler(this.textBox_KeyUp);
          this.textBox.MouseUp -= new MouseEventHandler(this.textBox_MouseUp);
          break;
        case RadDropDownStyle.DropDownList:
          this.KeyDown -= new KeyEventHandler(this.textBox_KeyDown);
          this.KeyPress -= new KeyPressEventHandler(this.textBox_KeyPress);
          this.KeyUp -= new KeyEventHandler(this.textBox_KeyUp);
          break;
      }
    }

    private bool SelectAllText(string text)
    {
      if (!this.textBox.TextBoxItem.TextBoxControl.Focused)
        return false;
      this.SelectionStart = 0;
      this.SelectionLength = string.IsNullOrEmpty(text) ? 0 : text.Length;
      return true;
    }

    protected void HandleEsc()
    {
      if (this.owner == null)
        return;
      this.owner.NotifyOwner(new PopupEditorNotificationData()
      {
        notificationContext = PopupEditorNotificationData.Context.Esc
      });
    }

    protected void HandleKeyUpKeyDown(KeyEventArgs e)
    {
      if (this.owner == null)
        return;
      this.owner.NotifyOwner(new PopupEditorNotificationData(e)
      {
        notificationContext = PopupEditorNotificationData.Context.KeyUpKeyDownPress
      });
    }

    protected void HandleF4Down()
    {
      if (this.owner == null)
        return;
      this.owner.NotifyOwner(new PopupEditorNotificationData()
      {
        notificationContext = PopupEditorNotificationData.Context.F4Press
      });
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      if (this.Size.Width <= 0 || (this.Size.Height <= 0 || this.DropDownStyle == RadDropDownStyle.DropDown || (!this.owner.ContainsFocus || this.owner.IsPopupOpen)))
        return;
      RadControl componentTreeHandler = this.ElementTree.ComponentTreeHandler as RadControl;
      if (componentTreeHandler == null || !componentTreeHandler.AllowShowFocusCues)
        return;
      Color[] gradientColors1 = new Color[4]{ this.BorderColor, this.BorderColor2, this.BorderColor3, this.BorderColor4 };
      int num = 2;
      if (this.BorderBoxStyle == BorderBoxStyle.OuterInnerBorders)
        num = 3;
      Rectangle rectangle1 = new Rectangle(num - 1, num - 1, this.Size.Width - num - 1, this.Size.Height - num - 1);
      this.DrawRectangle(graphics, rectangle1, gradientColors1, 1f);
      Color[] gradientColors2 = new Color[4]{ this.BorderInnerColor, this.BorderInnerColor2, this.BorderInnerColor3, this.BorderInnerColor4 };
      Rectangle rectangle2 = Rectangle.Inflate(rectangle1, -1, -1);
      this.DrawRectangle(graphics, rectangle2, gradientColors2, 1f);
    }

    private void DrawRectangle(
      IGraphics graphics,
      Rectangle rectangle,
      Color[] gradientColors,
      float width)
    {
      if (this.BorderBoxStyle == BorderBoxStyle.FourBorders)
        graphics.DrawRectangle((RectangleF) rectangle, this.ForeColor, PenAlignment.Inset, width, DashStyle.Dot);
      else if (this.GradientStyle == GradientStyles.Solid)
      {
        graphics.DrawRectangle((RectangleF) rectangle, gradientColors[0], PenAlignment.Inset, width, DashStyle.Dot);
      }
      else
      {
        if (this.GradientStyle != GradientStyles.Linear)
          return;
        graphics.DrawLinearGradientRectangle((RectangleF) rectangle, gradientColors, PenAlignment.Center, width, this.GradientAngle, DashStyle.Dot);
      }
    }
  }
}
