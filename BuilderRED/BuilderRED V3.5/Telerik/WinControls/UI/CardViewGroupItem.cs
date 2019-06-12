// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CardViewGroupItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class CardViewGroupItem : LayoutControlGroupItem, ICardViewBoundItem
  {
    private ListViewDetailColumn cardField;
    private string fieldName;

    [TypeConverter(typeof (CardViewFieldNameTypeConverter))]
    [Description("Gets or sets the name of field associated with this item.")]
    [Browsable(true)]
    [Category("CardView")]
    [DefaultValue(null)]
    public string FieldName
    {
      get
      {
        return this.fieldName;
      }
      set
      {
        this.fieldName = value;
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
        if (this.cardField == null || !(this.FieldName != this.cardField.Name))
          return;
        this.FieldName = this.cardField.Name;
      }
    }

    public virtual void Synchronize()
    {
      CardListViewVisualItem ancestor = this.FindAncestor<CardListViewVisualItem>();
      if (this.CardField == null || ancestor == null || ancestor.Data == null)
        return;
      this.Text = ancestor.Data[this.CardField].ToString();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.HeaderElement.ShouldHandleMouseInput = false;
      this.HeaderElement.HeaderLineElement.ShouldHandleMouseInput = false;
      this.HeaderElement.HeaderButtonElement.ShouldHandleMouseInput = true;
      this.HeaderElement.HeaderButtonElement.NotifyParentOnMouseInput = true;
      this.HeaderElement.HeaderTextElement.ShouldHandleMouseInput = false;
    }

    protected override LayoutControlContainerElement CreateContainerElement()
    {
      return (LayoutControlContainerElement) new CardViewContainerElement();
    }

    protected override void UpdateOnExpandCollapse()
    {
      bool flag = this.CheckIsHidden() || this.IsHidden;
      foreach (LayoutControlItemBase layoutControlItemBase in this.Items)
        layoutControlItemBase.IsHidden = flag || !this.IsExpanded;
      this.FindAncestor<LayoutControlContainerElement>()?.LayoutTree.UpdateItemsBounds();
      this.ContainerElement.Visibility = !this.IsExpanded || this.Visibility != ElementVisibility.Visible ? ElementVisibility.Collapsed : ElementVisibility.Visible;
      this.SetBounds(this.Bounds);
      if (this.ElementTree == null)
        return;
      CardListViewVisualItem ancestor = this.FindAncestor<CardListViewVisualItem>();
      if (ancestor == null || ancestor.Data.Owner.isSynchronizing)
        return;
      if (!this.isAttaching)
        ancestor.Data.Owner.ViewElement.ProcessSelection(ancestor.Data, Control.ModifierKeys, true);
      if (ancestor.Data != null)
        ancestor.Data.Owner.ViewElement.ViewElement.UpdateItems();
      if (this.isAttaching)
        return;
      ancestor.UpdateLayout();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public void SetCardField(ListViewDetailColumn column)
    {
      this.CardField = column;
    }
  }
}
