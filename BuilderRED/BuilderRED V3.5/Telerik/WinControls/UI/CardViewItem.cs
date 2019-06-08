// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CardViewItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class CardViewItem : LayoutControlItemBase, ICardViewBoundItem
  {
    private float textProportionalSize = 0.5f;
    private int textFixedSize = 100;
    private LayoutItemTextPosition textPosition = LayoutItemTextPosition.Left;
    private CardViewEditorItem editorItem;
    private ListViewDetailColumn cardField;
    private string fieldName;
    private int textMinSize;
    private int textMaxSize;
    private LayoutItemTextSizeMode textSizeMode;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.DrawFill = false;
      this.DrawBorder = false;
      this.DrawText = true;
      this.TextAlignment = ContentAlignment.MiddleLeft;
      this.MinSize = new Size(46, 26);
      this.Bounds = new Rectangle(0, 0, 100, 100);
      this.editorItem = this.CreateEditorItem();
      this.Children.Add((RadElement) this.editorItem);
    }

    protected virtual CardViewEditorItem CreateEditorItem()
    {
      return new CardViewEditorItem();
    }

    [Description("Gets or sets the position of the text of the item.")]
    [Browsable(true)]
    [Category("CardView")]
    [DefaultValue(LayoutItemTextPosition.Left)]
    public virtual LayoutItemTextPosition TextPosition
    {
      get
      {
        return this.textPosition;
      }
      set
      {
        if (this.textPosition == value)
          return;
        this.textPosition = value;
        this.OnNotifyPropertyChanged(nameof (TextPosition));
      }
    }

    [Description("Gets or sets the proportional size of the text part which will be used when TextSizeMode is set to proportional.")]
    [Browsable(true)]
    [Category("CardView")]
    [DefaultValue(0.5f)]
    public float TextProportionalSize
    {
      get
      {
        return this.textProportionalSize;
      }
      set
      {
        if ((double) this.textProportionalSize == (double) value)
          return;
        this.textProportionalSize = value;
        this.OnNotifyPropertyChanged(nameof (TextProportionalSize));
      }
    }

    [Description("Gets or sets the fixed size of the text part which will be used when TextSizeMode is set to fixed.")]
    [Browsable(true)]
    [Category("CardView")]
    [DefaultValue(100)]
    public int TextFixedSize
    {
      get
      {
        return this.textFixedSize;
      }
      set
      {
        if (this.textFixedSize == value)
          return;
        this.textFixedSize = value;
        this.OnNotifyPropertyChanged(nameof (TextFixedSize));
      }
    }

    [Category("CardView")]
    [DefaultValue(0)]
    [Description("Gets or sets the minimum size of the text part. ")]
    [Browsable(true)]
    public int TextMinSize
    {
      get
      {
        return this.textMinSize;
      }
      set
      {
        if (this.textMinSize == value)
          return;
        this.textMinSize = value;
        this.OnNotifyPropertyChanged(nameof (TextMinSize));
      }
    }

    [Browsable(true)]
    [Category("CardView")]
    [DefaultValue(0)]
    [Description("Gets or sets the maximum size of the text part. ")]
    public int TextMaxSize
    {
      get
      {
        if (this.textMaxSize != 0 && this.textMinSize != 0 && this.textMaxSize < this.textMinSize)
          return this.textMinSize;
        return this.textMaxSize;
      }
      set
      {
        if (this.textMaxSize == value)
          return;
        this.textMaxSize = value;
        this.OnNotifyPropertyChanged(nameof (TextMaxSize));
      }
    }

    [Browsable(true)]
    [DefaultValue(LayoutItemTextSizeMode.Proportional)]
    [Category("CardView")]
    [Description("Gets or sets the way in which the text part will be sized - proportionally or fixed-size.")]
    public LayoutItemTextSizeMode TextSizeMode
    {
      get
      {
        return this.textSizeMode;
      }
      set
      {
        if (this.textSizeMode == value)
          return;
        this.textSizeMode = value;
        this.OnNotifyPropertyChanged(nameof (TextSizeMode));
      }
    }

    [Description("Gets or sets the element associated with this item.")]
    public CardViewEditorItem EditorItem
    {
      get
      {
        return this.editorItem;
      }
    }

    [Description("Gets or sets the name of field associated with this item.")]
    [Browsable(true)]
    [Category("CardView")]
    [DefaultValue(null)]
    [TypeConverter(typeof (CardViewFieldNameTypeConverter))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string FieldName
    {
      get
      {
        return this.fieldName;
      }
      set
      {
        if (!(this.fieldName != value))
          return;
        this.fieldName = value;
        this.Text = this.fieldName;
      }
    }

    [Browsable(false)]
    [Description("Gets the field associated with this item.")]
    public ListViewDetailColumn CardField
    {
      get
      {
        return this.cardField;
      }
      protected internal set
      {
        this.cardField = value;
        if (this.cardField == null)
          return;
        this.FieldName = this.cardField.Name;
      }
    }

    public virtual void Synchronize()
    {
      CardListViewVisualItem ancestor = this.FindAncestor<CardListViewVisualItem>();
      if (this.CardField == null || ancestor == null || ancestor.Data == null)
        return;
      this.Text = this.CardField.HeaderText;
      object obj = ancestor.Data[this.CardField];
      if (obj is byte[])
      {
        Image image = (Image) new ImageConverter().ConvertFrom((object) (obj as byte[]));
        if (image == null)
          return;
        this.EditorItem.DrawText = false;
        this.editorItem.ShouldHandleMouseInput = false;
        this.EditorItem.Image = image;
      }
      else if (obj is Image)
      {
        Image image = obj as Image;
        this.EditorItem.DrawText = false;
        this.editorItem.ShouldHandleMouseInput = false;
        this.EditorItem.Image = image;
      }
      else
        this.EditorItem.Text = Convert.ToString(obj);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public void SetCardField(ListViewDetailColumn column)
    {
      this.CardField = column;
    }

    protected virtual RectangleF GetTextRectangle(RectangleF clientRect)
    {
      SizeF size = clientRect.Size;
      if (this.TextPosition == LayoutItemTextPosition.Left || this.TextPosition == LayoutItemTextPosition.Right)
      {
        size.Width = this.TextSizeMode == LayoutItemTextSizeMode.Proportional ? clientRect.Width * this.TextProportionalSize : (float) this.TextFixedSize;
        if (this.TextMinSize != 0)
          size.Width = Math.Max((float) this.TextMinSize, size.Width);
        if (this.TextMaxSize != 0)
          size.Width = Math.Min((float) this.TextMaxSize, size.Width);
      }
      if (this.TextPosition == LayoutItemTextPosition.Top || this.TextPosition == LayoutItemTextPosition.Bottom)
      {
        size.Height = this.TextSizeMode == LayoutItemTextSizeMode.Proportional ? clientRect.Height * this.TextProportionalSize : (float) this.TextFixedSize;
        if (this.TextMinSize != 0)
          size.Height = Math.Max((float) this.TextMinSize, size.Height);
        if (this.TextMaxSize != 0)
          size.Height = Math.Min((float) this.TextMaxSize, size.Height);
      }
      RectangleF rectangleF = RectangleF.Empty;
      if (this.TextPosition == LayoutItemTextPosition.Left || this.TextPosition == LayoutItemTextPosition.Top)
        rectangleF = new RectangleF(clientRect.Location, size);
      else if (this.TextPosition == LayoutItemTextPosition.Right)
        rectangleF = new RectangleF(new PointF(clientRect.Right - size.Width, clientRect.Y), size);
      else if (this.TextPosition == LayoutItemTextPosition.Bottom)
        rectangleF = new RectangleF(new PointF(clientRect.X, clientRect.Bottom - size.Height), size);
      return rectangleF;
    }

    protected virtual Rectangle GetEditorItemRectangle(RectangleF clientRect)
    {
      if (this.DrawText)
      {
        RectangleF textRectangle = this.GetTextRectangle(clientRect);
        if (this.TextPosition == LayoutItemTextPosition.Left)
          clientRect = new RectangleF(textRectangle.Right, clientRect.Y, clientRect.Width - textRectangle.Width, clientRect.Height);
        else if (this.TextPosition == LayoutItemTextPosition.Top)
          clientRect = new RectangleF(clientRect.X, textRectangle.Bottom, clientRect.Width, clientRect.Height - textRectangle.Height);
        else if (this.TextPosition == LayoutItemTextPosition.Right)
          clientRect = new RectangleF(clientRect.X, clientRect.Y, clientRect.Width - textRectangle.Width, clientRect.Height);
        else if (this.TextPosition == LayoutItemTextPosition.Bottom)
          clientRect = new RectangleF(clientRect.X, clientRect.Y, clientRect.Width, clientRect.Height - textRectangle.Height);
      }
      return this.GetAlignedEditorRectangle(new Rectangle((int) clientRect.X, (int) clientRect.Y, (int) clientRect.Width, (int) clientRect.Height));
    }

    protected internal virtual RectangleF GetEditorSize(SizeF availableSize)
    {
      return (RectangleF) this.GetEditorItemRectangle(this.GetClientRectangle(availableSize));
    }

    protected virtual Rectangle GetAlignedEditorRectangle(Rectangle editorArrangeRect)
    {
      return editorArrangeRect;
    }

    protected virtual void OnColumnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.Synchronize();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != LightVisualElement.DrawTextProperty || this.Parent == null)
        return;
      this.Parent.InvalidateMeasure();
      this.Parent.UpdateLayout();
      this.Parent.Invalidate();
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      if ((e.PropertyName == "TextPosition" || e.PropertyName == "TextProportionalSize" || (e.PropertyName == "TextFixedSize" || e.PropertyName == "TextMinSize") || (e.PropertyName == "TextMaxSize" || e.PropertyName == "TextSizeMode")) && (this.Parent != null && !this.Parent.IsLayoutSuspended))
      {
        this.Parent.InvalidateMeasure();
        this.Parent.UpdateLayout();
        this.Parent.Invalidate();
      }
      base.OnNotifyPropertyChanged(e);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      if (this.DrawText)
        this.Layout.Measure(this.GetTextRectangle(clientRectangle).Size);
      this.EditorItem.Measure((SizeF) this.GetEditorItemRectangle(clientRectangle).Size);
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.MeasureOverride(finalSize);
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      if (this.DrawText)
        this.Layout.Arrange(this.GetTextRectangle(clientRectangle));
      this.EditorItem.Arrange((RectangleF) this.GetEditorItemRectangle(clientRectangle));
      return sizeF;
    }
  }
}
