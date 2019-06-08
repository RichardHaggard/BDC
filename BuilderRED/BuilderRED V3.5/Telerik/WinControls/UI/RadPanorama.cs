// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPanorama
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [Designer("Telerik.WinControls.UI.Design.RadTilePanelDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [ToolboxItem(true)]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [TelerikToolboxCategory("Containers")]
  public class RadPanorama : RadControl
  {
    private RadPanoramaElement tilePanelElement;

    [Browsable(false)]
    [Category("Layout")]
    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
      set
      {
        base.AutoSize = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the way that RadPanorama should handle mouse wheel input.")]
    [DefaultValue(PanoramaMouseWheelBehavior.Zoom)]
    [Browsable(true)]
    public PanoramaMouseWheelBehavior MouseWheelBehavior
    {
      get
      {
        return this.PanoramaElement.MouseWheelBehavior;
      }
      set
      {
        this.PanoramaElement.MouseWheelBehavior = value;
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets a value that indicates whether the newly added tiles should be automatically arranged.")]
    [Browsable(true)]
    [Category("Behavior")]
    public bool AutoArrangeNewTiles
    {
      get
      {
        return this.PanoramaElement.AutoArrangeNewTiles;
      }
      set
      {
        this.PanoramaElement.AutoArrangeNewTiles = value;
      }
    }

    [Category("Behavior")]
    [Description("Enables or Disables the build in zoom functionality.")]
    [Browsable(true)]
    [DefaultValue(true)]
    public bool EnableZooming
    {
      get
      {
        return this.PanoramaElement.EnableZooming;
      }
      set
      {
        this.PanoramaElement.EnableZooming = value;
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the minimum number of columns that the view can be reduced to.")]
    [DefaultValue(0)]
    public int MinimumColumns
    {
      get
      {
        return this.tilePanelElement.MinimumColumns;
      }
      set
      {
        this.tilePanelElement.MinimumColumns = value;
      }
    }

    [Description("Gets or sets a value indicating whether reordering of tiles via drag and drop is allowed.")]
    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(true)]
    public bool AllowDragDrop
    {
      get
      {
        return this.PanoramaElement.AllowDragDrop;
      }
      set
      {
        this.PanoramaElement.AllowDragDrop = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the groups or the items should be displayed.")]
    public bool ShowGroups
    {
      get
      {
        return this.tilePanelElement.ShowGroups;
      }
      set
      {
        this.tilePanelElement.ShowGroups = value;
      }
    }

    [Description("Gets or sets a value indicating whether the background image should be scrolled along with the tiles.")]
    [Browsable(true)]
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool ScrollingBackground
    {
      get
      {
        return this.PanoramaElement.ScrollingBackground;
      }
      set
      {
        this.PanoramaElement.ScrollingBackground = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the position on which the scrollbar should be aligned.")]
    [DefaultValue(HorizontalScrollAlignment.Top)]
    [Category("Appearance")]
    public HorizontalScrollAlignment ScrollBarAlignment
    {
      get
      {
        return this.PanoramaElement.ScrollBarAlignment;
      }
      set
      {
        this.PanoramaElement.ScrollBarAlignment = value;
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [Description("Gets or sets the thickness of the scrollbar.")]
    [DefaultValue(16)]
    public int ScrollBarThickness
    {
      get
      {
        return this.PanoramaElement.ScrollBarThickness;
      }
      set
      {
        this.PanoramaElement.ScrollBarThickness = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets the RadPanoramaElement that represents the main element of the control.")]
    public RadPanoramaElement PanoramaElement
    {
      get
      {
        return this.tilePanelElement;
      }
    }

    [Description("Gets or sets the image that is displayed in the background.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance")]
    [DefaultValue(null)]
    [Browsable(false)]
    public Image PanelImage
    {
      get
      {
        return this.tilePanelElement.PanelImage;
      }
      set
      {
        this.tilePanelElement.PanelImage = value;
      }
    }

    [Description("Gets or sets the size of the image that is displayed in the background.")]
    [Browsable(true)]
    [DefaultValue(typeof (Size), "0, 0")]
    [Category("Appearance")]
    public Size PanelImageSize
    {
      get
      {
        return this.tilePanelElement.PanelImageSize;
      }
      set
      {
        this.tilePanelElement.PanelImageSize = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets or sets the current number of columns.")]
    public int ColumnsCount
    {
      get
      {
        return this.tilePanelElement.ColumnsCount;
      }
      set
      {
        this.tilePanelElement.ColumnsCount = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(1)]
    [Description("Gets or sets the number of rows.")]
    [Browsable(true)]
    public int RowsCount
    {
      get
      {
        return this.tilePanelElement.RowsCount;
      }
      set
      {
        this.tilePanelElement.RowsCount = value;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the size of a single cell.")]
    [DefaultValue(typeof (Size), "100, 100")]
    [Browsable(true)]
    public Size CellSize
    {
      get
      {
        return this.tilePanelElement.CellSize;
      }
      set
      {
        this.tilePanelElement.CellSize = value;
      }
    }

    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    [Description("Gets a collection of RadTileElement objects that represent the tiles that are displayed.")]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.tilePanelElement.Items;
      }
    }

    [Category("Data")]
    [Description("Gets a collection of RadTileElement objects that represent the tiles that are displayed.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemOwnerCollection Groups
    {
      get
      {
        return this.tilePanelElement.Groups;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(240, 150));
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override Color ForeColor
    {
      get
      {
        return base.ForeColor;
      }
      set
      {
        base.ForeColor = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
      }
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.tilePanelElement = this.CreatePanoramaElement();
      this.tilePanelElement.PropertyChanged += new PropertyChangedEventHandler(this.tilePanelElement_PropertyChanged);
      parent.Children.Add((RadElement) this.tilePanelElement);
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      this.EnableGesture(GestureType.Pan);
      this.EnableGesture(GestureType.Zoom);
    }

    protected override void Dispose(bool disposing)
    {
      this.tilePanelElement.PropertyChanged -= new PropertyChangedEventHandler(this.tilePanelElement_PropertyChanged);
      base.Dispose(disposing);
    }

    public new void Select()
    {
      base.Select();
      this.PanoramaElement.Focus();
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.PanoramaElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, "");
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.PanoramaElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
    }

    protected virtual RadPanoramaElement CreatePanoramaElement()
    {
      return new RadPanoramaElement();
    }

    private void tilePanelElement_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.OnNotifyPropertyChanged(e.PropertyName);
    }
  }
}
