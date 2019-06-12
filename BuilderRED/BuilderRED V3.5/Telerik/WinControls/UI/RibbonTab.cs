// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RibbonTab
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public class RibbonTab : RadPageViewStripItem
  {
    public static RadProperty RightShadowInnerColor1Property = RadProperty.Register(nameof (RightShadowInnerColor1), typeof (Color), typeof (RibbonTab), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(10, Color.Black), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty RightShadowInnerColor2Property = RadProperty.Register(nameof (RightShadowInnerColor2), typeof (Color), typeof (RibbonTab), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(20, Color.Black), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty RightShadowOuterColor1Property = RadProperty.Register(nameof (RightShadowOuterColor1), typeof (Color), typeof (RibbonTab), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(0, Color.Black), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty RightShadowOuterColor2Property = RadProperty.Register(nameof (RightShadowOuterColor2), typeof (Color), typeof (RibbonTab), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(20, Color.Black), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LeftShadowInnerColor1Property = RadProperty.Register(nameof (LeftShadowInnerColor1), typeof (Color), typeof (RibbonTab), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(10, Color.Black), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LeftShadowInnerColor2Property = RadProperty.Register(nameof (LeftShadowInnerColor2), typeof (Color), typeof (RibbonTab), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(20, Color.Black), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LeftShadowOuterColor1Property = RadProperty.Register(nameof (LeftShadowOuterColor1), typeof (Color), typeof (RibbonTab), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(0, Color.Black), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LeftShadowOuterColor2Property = RadProperty.Register(nameof (LeftShadowOuterColor2), typeof (Color), typeof (RibbonTab), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb(20, Color.Black), ElementPropertyOptions.AffectsDisplay));
    private const int MinHeight = 28;
    private RadItemOwnerCollection items;
    private RadRibbonBarCommandTabCollection parentCollection;
    private ContextualTabGroup contextualTabGroup;
    internal RadPageViewItem obsoleteTab;
    private ExpandableStackLayout tabContentLayout;
    private RadTabStripContentPanel contentPanel;
    private RibbonTabStripElement owner;

    public RibbonTab()
    {
      this.Class = nameof (RibbonTab);
    }

    public RibbonTab(string text)
      : this()
    {
      this.Text = text;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.items = new RadItemOwnerCollection();
      this.items.ItemTypes = new Type[1]
      {
        typeof (RadRibbonBarGroup)
      };
    }

    [Category("Appearance")]
    [Description("Gets or sets first right inner color of the RibbonTab's shadow.")]
    public Color RightShadowInnerColor1
    {
      get
      {
        return (Color) this.GetValue(RibbonTab.RightShadowInnerColor1Property);
      }
      set
      {
        int num = (int) this.SetValue(RibbonTab.RightShadowInnerColor1Property, (object) value);
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets second right inner color of the RibbonTab's shadow.")]
    public Color RightShadowInnerColor2
    {
      get
      {
        return (Color) this.GetValue(RibbonTab.RightShadowInnerColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(RibbonTab.RightShadowInnerColor2Property, (object) value);
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets first right outer color of the RibbonTab's shadow.")]
    public Color RightShadowOuterColor1
    {
      get
      {
        return (Color) this.GetValue(RibbonTab.RightShadowOuterColor1Property);
      }
      set
      {
        int num = (int) this.SetValue(RibbonTab.RightShadowOuterColor1Property, (object) value);
      }
    }

    [Description("Gets or sets second right outer color of the RibbonTab's shadow.")]
    [Category("Appearance")]
    public Color RightShadowOuterColor2
    {
      get
      {
        return (Color) this.GetValue(RibbonTab.RightShadowOuterColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(RibbonTab.RightShadowOuterColor2Property, (object) value);
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the first left inner color of the RibbonTab's shadow.")]
    public Color LeftShadowInnerColor1
    {
      get
      {
        return (Color) this.GetValue(RibbonTab.LeftShadowInnerColor1Property);
      }
      set
      {
        int num = (int) this.SetValue(RibbonTab.LeftShadowInnerColor1Property, (object) value);
      }
    }

    [Description("Gets or sets the second left inner color of the RibbonTab's shadow.")]
    [Category("Appearance")]
    public Color LeftShadowInnerColor2
    {
      get
      {
        return (Color) this.GetValue(RibbonTab.LeftShadowInnerColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(RibbonTab.LeftShadowInnerColor2Property, (object) value);
      }
    }

    [Description("Gets or sets the first left outer color of the RibbonTab's shadow.")]
    [Category("Appearance")]
    public Color LeftShadowOuterColor1
    {
      get
      {
        return (Color) this.GetValue(RibbonTab.LeftShadowOuterColor1Property);
      }
      set
      {
        int num = (int) this.SetValue(RibbonTab.LeftShadowOuterColor1Property, (object) value);
      }
    }

    [Description("Gets or sets the second left outer color of the RibbonTab's shadow.")]
    [Category("Appearance")]
    public Color LeftShadowOuterColor2
    {
      get
      {
        return (Color) this.GetValue(RibbonTab.LeftShadowOuterColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(RibbonTab.LeftShadowOuterColor2Property, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadPageViewItem Tab
    {
      get
      {
        return (RadPageViewItem) this;
      }
      set
      {
        if (value == null)
          return;
        this.obsoleteTab = value;
        this.Visibility = value.Visibility;
        this.Text = value.Text;
        this.AssureTabAdded();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadTabStripContentPanel ContentPanel
    {
      get
      {
        if (this.contentPanel == null)
          this.contentPanel = new RadTabStripContentPanel();
        return this.contentPanel;
      }
    }

    [Browsable(false)]
    public ExpandableStackLayout ContentLayout
    {
      get
      {
        return this.tabContentLayout;
      }
    }

    [Browsable(false)]
    [DefaultValue(null)]
    [Editor(typeof (ContextualTabGroupsEditor), typeof (UITypeEditor))]
    public ContextualTabGroup ContextualTabGroup
    {
      get
      {
        return this.contextualTabGroup;
      }
      set
      {
        this.contextualTabGroup = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal RadRibbonBarCommandTabCollection ParentCollection
    {
      get
      {
        return this.parentCollection;
      }
      set
      {
        this.parentCollection = value;
        this.SetItems();
      }
    }

    [RadNewItem("Add New Group...", true, false, false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor("Telerik.WinControls.UI.Design.RadItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public virtual RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    public override bool IsSelected
    {
      get
      {
        return base.IsSelected;
      }
      set
      {
        base.IsSelected = value;
      }
    }

    protected internal virtual bool CanBeAddedToContextualGroup
    {
      get
      {
        return true;
      }
    }

    private void AssureTabAdded()
    {
      if (this.ParentCollection == null || this.ParentCollection.Owner == null)
        return;
      RadPageViewElement owner = (RadPageViewElement) this.ParentCollection.Owner;
      int index = this.ParentCollection.IndexOf((RadItem) this);
      if (index >= 0)
        owner.RemoveItem((RadPageViewItem) this);
      owner.InsertItem(index, (RadPageViewItem) this);
    }

    private void SetItems()
    {
      if (this.parentCollection == null || this.parentCollection.Owner == null)
      {
        if (this.tabContentLayout == null || this.tabContentLayout.Parent == null)
          return;
        this.tabContentLayout.Parent.Children.Remove((RadElement) this.tabContentLayout);
      }
      else
      {
        RadElement contentArea = (RadElement) ((RadRibbonBar) this.parentCollection.Owner.ElementTree.Control).RibbonBarElement.TabStripElement.ContentArea;
        if (this.tabContentLayout == null)
        {
          this.tabContentLayout = new ExpandableStackLayout();
          this.Items.Owner = (RadElement) this.tabContentLayout;
        }
        this.tabContentLayout.CollapseElementsOnResize = true;
        this.tabContentLayout.UseParentSizeAsAvailableSize = true;
        this.tabContentLayout.IsInStripMode = true;
        this.tabContentLayout.Visibility = ElementVisibility.Collapsed;
        if (contentArea.Children.Contains((RadElement) this.tabContentLayout))
          return;
        contentArea.Children.Add((RadElement) this.tabContentLayout);
      }
    }

    protected override void OnPropertyChanging(RadPropertyChangingEventArgs e)
    {
      base.OnPropertyChanging(e);
      if (e.Property == RadElement.VisibilityProperty && (ElementVisibility) e.NewValue == ElementVisibility.Collapsed)
      {
        int num = this.Owner.Items.IndexOf((RadPageViewItem) this);
        bool flag = false;
        for (int index = num + 1; index < this.Owner.Items.Count; ++index)
        {
          RadPageViewItem radPageViewItem = this.Owner.Items[index];
          if (radPageViewItem.Visibility == ElementVisibility.Visible)
          {
            this.Owner.SelectedItem = radPageViewItem;
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          for (int index = num - 1; index > -1; --index)
          {
            RadPageViewItem radPageViewItem = this.Owner.Items[index];
            if (radPageViewItem.Visibility == ElementVisibility.Visible)
            {
              this.Owner.SelectedItem = radPageViewItem;
              flag = true;
              break;
            }
          }
        }
        e.Cancel = !flag;
        if (!e.Cancel)
          this.ChangeTabVisibleCore();
        if (this.owner == null)
          return;
        this.owner.InvalidateMeasure();
        this.owner.UpdateLayout();
      }
      else
      {
        if (e.Property != RadElement.VisibilityProperty || (ElementVisibility) e.NewValue != ElementVisibility.Visible)
          return;
        this.ChangeTabVisibleCore();
      }
    }

    private void ChangeTabVisibleCore()
    {
      RadRibbonBarElement parent = this.Owner.Parent as RadRibbonBarElement;
      if (parent == null)
        return;
      parent.SuspendPropertyNotifications();
      parent.ElementTree.RootElement.SuspendLayout();
      bool expanded = parent.Expanded;
      parent.Expanded = true;
      CommandTabEventArgs args = new CommandTabEventArgs(this);
      parent.CallOnCommandTabCollapsed(args);
      parent.VisibleChangedCore();
      parent.Expanded = expanded;
      parent.ElementTree.RootElement.ResumeLayout(false);
      parent.ResumePropertyNotifications();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property.Name == "IsSelected" && (bool) e.NewValue && this.Owner != null)
        this.Owner.SetSelectedItem((RadPageViewItem) this);
      if (e.Property != RadElement.BoundsProperty || this.contextualTabGroup == null || (this.contextualTabGroup.Parent == null || this.contextualTabGroup.Parent.Parent == null))
        return;
      this.contextualTabGroup.Parent.Parent.InvalidateMeasure(true);
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      if (this.owner == null)
        this.owner = this.Owner as RibbonTabStripElement;
      if (this.owner.PaintTabShadows)
      {
        this.PaintRightTabShadow(graphics);
        this.PaintLeftTabShadow(graphics);
      }
      base.PaintElement(graphics, angle, scale);
    }

    private void PaintLeftTabShadow(IGraphics graphics)
    {
      int num = 3;
      int width = 1;
      GraphicsPath path1 = new GraphicsPath();
      Rectangle rectangle1 = new Rectangle(num - width, num + 1, width, this.Bounds.Height - num);
      path1.AddLine(num - width, num + 1, num - width, this.Bounds.Height - num);
      graphics.DrawLinearGradientPath(path1, (RectangleF) rectangle1, new Color[2]
      {
        this.LeftShadowInnerColor1,
        this.LeftShadowInnerColor2
      }, PenAlignment.Left, (float) width, 90f);
      GraphicsPath path2 = new GraphicsPath();
      Rectangle rectangle2 = new Rectangle(num - 2 * width, num + 1, width, this.Bounds.Height - num);
      path2.AddLine(num - 2 * width, num + 1, num - 2 * width, this.Bounds.Height - num);
      graphics.DrawLinearGradientPath(path2, (RectangleF) rectangle2, new Color[2]
      {
        this.LeftShadowOuterColor1,
        this.LeftShadowOuterColor2
      }, PenAlignment.Left, (float) width, 90f);
    }

    private void PaintRightTabShadow(IGraphics graphics)
    {
      int num = 3;
      int width = 1;
      GraphicsPath path1 = new GraphicsPath();
      Rectangle rectangle1 = new Rectangle(this.Bounds.Width - num, num + 1, width, this.Bounds.Height - num);
      path1.AddLine(this.Bounds.Width - num, num + 1, this.Bounds.Width - num, this.Bounds.Height - num);
      graphics.DrawLinearGradientPath(path1, (RectangleF) rectangle1, new Color[2]
      {
        this.RightShadowInnerColor1,
        this.RightShadowInnerColor2
      }, PenAlignment.Right, (float) width, 90f);
      GraphicsPath path2 = new GraphicsPath();
      Rectangle rectangle2 = new Rectangle(this.Bounds.Width - num + width, num + 1, 1, this.Bounds.Height - num);
      path2.AddLine(this.Bounds.Width - num + width, num + 1, this.Bounds.Width - num + width, this.Bounds.Height - num + 1);
      graphics.DrawLinearGradientPath(path2, (RectangleF) rectangle2, new Color[2]
      {
        this.RightShadowOuterColor1,
        this.RightShadowOuterColor2
      }, PenAlignment.Right, (float) width, 90f);
    }
  }
}
