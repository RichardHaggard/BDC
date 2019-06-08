// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutControlItemBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public abstract class LayoutControlItemBase : LightVisualElement
  {
    internal bool isPreview;
    private bool isHidden;
    private bool allowDelete;
    private bool drawingFakeBorder;

    static LayoutControlItemBase()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (LayoutControlItemBase));
    }

    public override bool DrawBorder
    {
      get
      {
        if (!base.DrawBorder)
          return this.drawingFakeBorder;
        return true;
      }
      set
      {
        base.DrawBorder = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsHidden
    {
      get
      {
        return this.isHidden;
      }
      internal set
      {
        if (this.isHidden == value)
          return;
        this.isHidden = value;
        this.OnNotifyPropertyChanged(nameof (IsHidden));
      }
    }

    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether this item can be deleted by the end-user from the Customize dialog.")]
    public bool AllowDelete
    {
      get
      {
        return this.allowDelete;
      }
      set
      {
        this.allowDelete = value;
      }
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      if (this.ElementTree == null)
        return;
      this.IsHidden = this.CheckIsHidden();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      base.AutoSize = false;
      this.BorderGradientStyle = GradientStyles.Solid;
    }

    public override bool AutoSize
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    protected override void SetBoundsCore(Rectangle bounds)
    {
      Size maxSize = this.MaxSize;
      Size minSize = this.MinSize;
      if (minSize.Width != 0)
      {
        bounds.Width = Math.Max(minSize.Width, bounds.Width);
        if (maxSize.Width != 0)
          maxSize.Width = Math.Max(minSize.Width, maxSize.Width);
      }
      if (minSize.Height != 0)
      {
        bounds.Height = Math.Max(minSize.Height, bounds.Height);
        if (maxSize.Height != 0)
          maxSize.Height = Math.Max(minSize.Height, maxSize.Height);
      }
      if (maxSize.Width != 0)
        bounds.Width = Math.Min(maxSize.Width, bounds.Width);
      if (maxSize.Height != 0)
        bounds.Height = Math.Min(maxSize.Height, bounds.Height);
      bounds.Width = Math.Max(0, bounds.Width);
      bounds.Height = Math.Max(0, bounds.Height);
      base.SetBoundsCore(bounds);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadElement.MinSizeProperty || e.Property == RadElement.MaxSizeProperty)
      {
        this.SetBounds(this.Bounds);
        this.FindAncestor<LayoutControlContainerElement>()?.LayoutTree.UpdateItemsBounds();
      }
      if (e.Property != RadElement.VisibilityProperty)
        return;
      this.IsHidden = this.CheckIsHidden();
    }

    protected bool CheckIsHidden()
    {
      if (this.Visibility != ElementVisibility.Visible)
        return true;
      for (RadElement parent = this.Parent; parent != null; parent = parent.Parent)
      {
        LayoutControlItemBase layoutControlItemBase = parent as LayoutControlItemBase;
        if (parent.Visibility != ElementVisibility.Visible || layoutControlItemBase != null && layoutControlItemBase.IsHidden)
          return true;
      }
      return false;
    }

    public ILayoutControlItemsHost GetParentItemsContainer()
    {
      for (RadElement parent = this.Parent; parent != null; parent = parent.Parent)
      {
        if (parent is ILayoutControlItemsHost)
          return (ILayoutControlItemsHost) parent;
      }
      return (ILayoutControlItemsHost) null;
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      RadLayoutControl control = this.ElementTree.Control as RadLayoutControl;
      if (this.DrawBorder || (control == null || !control.ShowItemBorders) && this.Site == null)
        return;
      this.drawingFakeBorder = true;
      this.PaintBorder(graphics, angle, scale);
      this.drawingFakeBorder = false;
    }

    protected override RectangleF GetBorderPaintRect(float angle, SizeF scale)
    {
      RectangleF borderPaintRect = base.GetBorderPaintRect(angle, scale);
      ++borderPaintRect.Width;
      ++borderPaintRect.Height;
      return borderPaintRect;
    }
  }
}
