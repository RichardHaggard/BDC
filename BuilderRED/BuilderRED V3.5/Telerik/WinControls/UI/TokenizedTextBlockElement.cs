// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TokenizedTextBlockElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class TokenizedTextBlockElement : StackLayoutElement, ITextBlock
  {
    private bool allowRemove = true;
    private int index;
    private int offset;
    private LightVisualElement contentElement;
    private RadButtonElement removeButton;
    private readonly RadTokenizedTextItem item;

    static TokenizedTextBlockElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new EditorElementStateManager(), typeof (TokenizedTextBlockElement));
    }

    public TokenizedTextBlockElement()
      : this(string.Empty)
    {
    }

    public TokenizedTextBlockElement(string text)
    {
      this.item = this.CreateTokenizedTextItem(text, (object) null);
      this.contentElement.Text = text;
    }

    protected virtual RadTokenizedTextItem CreateTokenizedTextItem(
      string text,
      object value)
    {
      return new RadTokenizedTextItem(text, value);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.index = 0;
      this.offset = 0;
      this.Orientation = Orientation.Horizontal;
      this.NotifyParentOnMouseInput = true;
      int num1 = (int) this.SetDefaultValueOverride(RadElement.ShapeProperty, (object) new RoundRectShape());
      int num2 = (int) this.SetDefaultValueOverride(LightVisualElement.BorderBoxStyleProperty, (object) BorderBoxStyle.SingleBorder);
      int num3 = (int) this.SetDefaultValueOverride(LightVisualElement.DrawBorderProperty, (object) true);
      int num4 = (int) this.SetDefaultValueOverride(RadElement.MarginProperty, (object) new Padding(1, 0, 1, 0));
      int num5 = (int) this.SetDefaultValueOverride(StackLayoutElement.ElementSpacingProperty, (object) 2);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.contentElement = this.CreateContentElement();
      this.contentElement.TextChanged += new EventHandler(this.OnContentElementTextChanged);
      this.contentElement.ShouldHandleMouseInput = false;
      this.contentElement.NotifyParentOnMouseInput = true;
      this.contentElement.ThemeRole = "TokenizedContentElement";
      this.Children.Add((RadElement) this.contentElement);
      this.removeButton = this.CreateRemoveButton();
      this.removeButton.CanFocus = false;
      this.removeButton.ShouldHandleMouseInput = true;
      this.removeButton.NotifyParentOnMouseInput = false;
      this.removeButton.ThemeRole = "TokenizedRemoveButton";
      int num1 = (int) this.removeButton.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) true);
      int num2 = (int) this.removeButton.SetDefaultValueOverride(RadElement.StretchHorizontallyProperty, (object) false);
      int num3 = (int) this.removeButton.SetDefaultValueOverride(RadElement.MinSizeProperty, (object) new Size(10, 0));
      this.removeButton.Click += new EventHandler(this.OnRemoveButtonClick);
      this.Children.Add((RadElement) this.removeButton);
    }

    protected virtual LightVisualElement CreateContentElement()
    {
      return new LightVisualElement();
    }

    protected virtual RadButtonElement CreateRemoveButton()
    {
      return new RadButtonElement();
    }

    public RadTokenizedTextItem Item
    {
      get
      {
        return this.item;
      }
    }

    public LightVisualElement ContentElement
    {
      get
      {
        return this.contentElement;
      }
    }

    public RadButtonElement RemoveButton
    {
      get
      {
        return this.removeButton;
      }
    }

    public bool AllowRemove
    {
      get
      {
        return this.allowRemove;
      }
      set
      {
        if (this.allowRemove == value)
          return;
        this.allowRemove = value;
        this.removeButton.Visibility = this.allowRemove ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
    }

    string ITextBlock.Text
    {
      get
      {
        return this.contentElement.Text;
      }
      set
      {
        this.contentElement.Text = value;
      }
    }

    int ITextBlock.Index
    {
      get
      {
        return this.index;
      }
      set
      {
        if (this.index == value)
          return;
        this.index = value;
      }
    }

    int ITextBlock.Offset
    {
      get
      {
        return this.offset;
      }
      set
      {
        if (this.offset == value)
          return;
        this.offset = value;
      }
    }

    int ITextBlock.Length
    {
      get
      {
        return this.contentElement.Text.Length + 1;
      }
    }

    private void OnContentElementTextChanged(object sender, EventArgs e)
    {
      this.item.Text = this.contentElement.Text;
    }

    private void OnRemoveButtonClick(object sender, EventArgs e)
    {
      if (!this.allowRemove)
        return;
      this.OnRemoveButtonClick();
    }

    protected virtual void OnRemoveButtonClick()
    {
      TextBoxViewElement parent = this.Parent as TextBoxViewElement;
      if (parent == null)
        return;
      parent.BeginEditUpdate();
      parent.Children.Remove((RadElement) this);
      parent.UpdateLayout();
      parent.EndEditUpdate(true, string.Empty, this.offset, TextBoxChangeAction.TextEdit);
    }

    public virtual RectangleF GetRectangleFromCharacterIndex(int index, bool trailEdge)
    {
      ITextBlock textBlock = (ITextBlock) this;
      RectangleF boundingRectangle = (RectangleF) this.ControlBoundingRectangle;
      if (index == textBlock.Length)
      {
        boundingRectangle.X = boundingRectangle.Right;
        boundingRectangle.Width = 0.0f;
      }
      return boundingRectangle;
    }

    public virtual int GetCharacterIndexFromX(float x)
    {
      int num1 = 0;
      Rectangle boundingRectangle = this.ControlBoundingRectangle;
      int num2 = boundingRectangle.X + boundingRectangle.Width / 2;
      if ((double) x > (double) num2)
        num1 = ((ITextBlock) this).Length;
      return num1;
    }

    [SpecialName]
    SizeF ITextBlock.get_DesiredSize()
    {
      return this.DesiredSize;
    }

    void ITextBlock.Measure(SizeF _param1)
    {
      this.Measure(_param1);
    }

    void ITextBlock.Arrange(RectangleF _param1)
    {
      this.Arrange(_param1);
    }
  }
}
