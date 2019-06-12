// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridPrintStyle
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class GridPrintStyle
  {
    private RadGridView grid;
    private GridPrintSettings printSettings;
    private PrintGridTraverser traverser;
    private BaseGridPrintRenderer printRenderer;

    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Font CellFont
    {
      get
      {
        return this.printSettings.CellFont;
      }
      set
      {
        this.printSettings.CellFont = value;
      }
    }

    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Font GroupRowFont
    {
      get
      {
        return this.printSettings.GroupRowFont;
      }
      set
      {
        this.printSettings.GroupRowFont = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(null)]
    public Font HeaderCellFont
    {
      get
      {
        return this.printSettings.HeaderCellFont;
      }
      set
      {
        this.printSettings.HeaderCellFont = value;
      }
    }

    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Font SummaryCellFont
    {
      get
      {
        return this.printSettings.SummaryCellFont;
      }
      set
      {
        this.printSettings.SummaryCellFont = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(20)]
    public int HierarchyIndent
    {
      get
      {
        return this.printSettings.HierarchyIndent;
      }
      set
      {
        this.printSettings.HierarchyIndent = value;
      }
    }

    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool PrintGrouping
    {
      get
      {
        return this.printSettings.PrintGrouping;
      }
      set
      {
        this.printSettings.PrintGrouping = value;
      }
    }

    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool PrintSummaries
    {
      get
      {
        return this.printSettings.PrintSummaries;
      }
      set
      {
        this.printSettings.PrintSummaries = value;
      }
    }

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool PrintHiddenRows
    {
      get
      {
        return this.printSettings.PrintHiddenRows;
      }
      set
      {
        this.printSettings.PrintHiddenRows = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    public bool PrintHierarchy
    {
      get
      {
        return this.printSettings.PrintHierarchy;
      }
      set
      {
        this.printSettings.PrintHierarchy = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    public bool PrintHiddenColumns
    {
      get
      {
        return this.printSettings.PrintHiddenColumns;
      }
      set
      {
        this.printSettings.PrintHiddenColumns = value;
      }
    }

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool PrintAlternatingRowColor
    {
      get
      {
        return this.printSettings.PrintAlternatingRowColor;
      }
      set
      {
        this.printSettings.PrintAlternatingRowColor = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    public bool PrintHeaderOnEachPage
    {
      get
      {
        return this.printSettings.PrintHeaderOnEachPage;
      }
      set
      {
        this.printSettings.PrintHeaderOnEachPage = value;
      }
    }

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool PrintAllPages
    {
      get
      {
        return this.printSettings.PrintAllPages;
      }
      set
      {
        this.printSettings.PrintAllPages = value;
      }
    }

    [DefaultValue(typeof (Color), "White")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color CellBackColor
    {
      get
      {
        return this.printSettings.CellBackColor;
      }
      set
      {
        this.printSettings.CellBackColor = value;
      }
    }

    [DefaultValue(typeof (Color), "0xB7B7B7")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color HeaderCellBackColor
    {
      get
      {
        return this.printSettings.HeaderCellBackColor;
      }
      set
      {
        this.printSettings.HeaderCellBackColor = value;
      }
    }

    [DefaultValue(typeof (Color), "0x959595")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GroupRowBackColor
    {
      get
      {
        return this.printSettings.GroupRowBackColor;
      }
      set
      {
        this.printSettings.GroupRowBackColor = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(typeof (Color), "Gray")]
    public Color SummaryCellBackColor
    {
      get
      {
        return this.printSettings.SummaryCellBackColor;
      }
      set
      {
        this.printSettings.SummaryCellBackColor = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(typeof (Color), "0xE1E1E1")]
    public Color AlternatingRowColor
    {
      get
      {
        return this.printSettings.AlternatingRowColor;
      }
      set
      {
        this.printSettings.AlternatingRowColor = value;
      }
    }

    [DefaultValue(typeof (Color), "Black")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color BorderColor
    {
      get
      {
        return this.printSettings.BorderColor;
      }
      set
      {
        this.printSettings.BorderColor = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(typeof (Padding), "2, 2, 2, 2")]
    public Padding CellPadding
    {
      get
      {
        return this.printSettings.CellPadding;
      }
      set
      {
        this.printSettings.CellPadding = value;
      }
    }

    [DefaultValue(typeof (PrintFitWidthMode), "FitPageWidth")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public PrintFitWidthMode FitWidthMode
    {
      get
      {
        return this.printSettings.FitWidthMode;
      }
      set
      {
        this.printSettings.FitWidthMode = value;
      }
    }

    [DefaultValue(typeof (ChildViewPrintMode), "PrintCurrentlyActiveView")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ChildViewPrintMode ChildViewPrintMode
    {
      get
      {
        return this.printSettings.ChildViewPrintMode;
      }
      set
      {
        this.printSettings.ChildViewPrintMode = value;
      }
    }

    [DefaultValue(null)]
    public RadGridView GridView
    {
      get
      {
        return this.grid;
      }
      internal set
      {
        this.grid = value;
        this.Initialize();
        this.Reset();
      }
    }

    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public PrintGridTraverser PrintTraverser
    {
      get
      {
        return this.traverser;
      }
    }

    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public BaseGridPrintRenderer PrintRenderer
    {
      get
      {
        return this.printRenderer;
      }
      set
      {
        this.UnwirePrintRendererEvents();
        this.printRenderer = value;
        this.WirePrintRendererEvents();
      }
    }

    public GridPrintStyle()
    {
      this.printSettings = new GridPrintSettings();
    }

    public GridPrintStyle(RadGridView grid)
    {
      this.grid = grid;
      this.printSettings = new GridPrintSettings();
      this.Initialize();
    }

    public virtual void Initialize()
    {
      this.PrintRenderer = this.InitializePrintRenderer(this.GridView);
      this.traverser = new PrintGridTraverser(this.GridView.MasterView);
    }

    protected virtual BaseGridPrintRenderer InitializePrintRenderer(
      RadGridView grid)
    {
      this.UnwirePrintRendererEvents();
      BaseGridPrintRenderer gridPrintRenderer = (BaseGridPrintRenderer) null;
      if ((object) grid.ViewDefinition.GetType() == (object) typeof (ColumnGroupsViewDefinition))
      {
        if (!(gridPrintRenderer is ColumnGroupsViewDefinitionPrintRenderer))
          gridPrintRenderer = (BaseGridPrintRenderer) new ColumnGroupsViewDefinitionPrintRenderer(grid);
      }
      else if ((object) grid.ViewDefinition.GetType() == (object) typeof (HtmlViewDefinition))
      {
        if (!(gridPrintRenderer is HtmlViewDefinitionPrintRenderer))
          gridPrintRenderer = (BaseGridPrintRenderer) new HtmlViewDefinitionPrintRenderer(grid);
      }
      else if (!(gridPrintRenderer is TableViewDefinitionPrintRenderer))
        gridPrintRenderer = (BaseGridPrintRenderer) new TableViewDefinitionPrintRenderer(grid);
      gridPrintRenderer.ChildViewPrinting += new ChildViewPrintingEventHandler(this.renderer_ChildViewPrinting);
      gridPrintRenderer.PrintCellFormatting += new PrintCellFormattingEventHandler(this.renderer_PrintCellFormatting);
      gridPrintRenderer.PrintCellPaint += new PrintCellPaintEventHandler(this.renderer_PrintCellPaint);
      return gridPrintRenderer;
    }

    public virtual void DrawPage(Rectangle drawArea, Graphics graphics, int pageNumber)
    {
      if (this.PrintRenderer == null || (object) this.GridView.ViewDefinition.GetType() != (object) this.PrintRenderer.ViewDefinitionType)
        this.PrintRenderer = this.InitializePrintRenderer(this.GridView);
      this.PrintRenderer.DrawPage(this.traverser, drawArea, graphics, this.printSettings, pageNumber);
    }

    public virtual int GetNumberOfPages(Rectangle drawArea)
    {
      TableViewRowLayoutBase rowLayout = (this.grid.ViewDefinition as TableViewDefinition).CreateRowLayout() as TableViewRowLayoutBase;
      rowLayout.Context = GridLayoutContext.Printer;
      rowLayout.Initialize(this.grid.TableElement);
      if (this.GridView.PrintStyle.FitWidthMode == PrintFitWidthMode.FitPageWidth)
        rowLayout.MeasureRow(new SizeF((float) drawArea.Width, (float) drawArea.Height));
      else
        rowLayout.MeasureRow((SizeF) this.GridView.Size);
      int height = drawArea.Height;
      if (this.PrintHeaderOnEachPage && !this.PrintHierarchy)
        height -= rowLayout.GetRowHeight((GridViewRowInfo) this.GridView.MasterView.TableHeaderRow);
      int num1 = 0;
      int num2 = 1;
      this.traverser.Reset();
      this.traverser.ProcessHiddenRows = this.PrintHiddenRows;
      this.traverser.ProcessHierarchy = this.PrintHierarchy;
      while (this.traverser.MoveNext())
      {
        if (this.traverser.Current is GridViewDataRowInfo || this.traverser.Current is GridViewTableHeaderRowInfo && (this.PrintHierarchy || !this.PrintHeaderOnEachPage) || (this.traverser.Current is GridViewGroupRowInfo && this.PrintGrouping || this.traverser.Current is GridViewSummaryRowInfo && this.PrintSummaries))
        {
          GridViewHierarchyRowInfo current = this.traverser.Current as GridViewHierarchyRowInfo;
          if (current != null && current.Views.Count > 0)
          {
            switch (this.printSettings.ChildViewPrintMode)
            {
              case ChildViewPrintMode.PrintFirstView:
                current.ActiveView = current.Views[0];
                break;
              case ChildViewPrintMode.SelectViewToPrint:
                ChildViewPrintingEventArgs e = new ChildViewPrintingEventArgs(current.Views.IndexOf(current.ActiveView), current);
                this.OnChildViewPrinting((object) this, e);
                current.ActiveView = current.Views[e.ActiveViewIndex];
                break;
            }
          }
          int num3 = !this.GridView.AutoSizeRows ? rowLayout.GetRowHeight(this.traverser.Current) + this.grid.TableElement.RowSpacing : this.PrintRenderer.GetDataRowHeight(this.traverser.Current, rowLayout);
          if (num1 + num3 < height)
          {
            num1 += num3;
          }
          else
          {
            ++num2;
            num1 = num3;
          }
        }
      }
      this.traverser.Reset();
      TableViewDefinitionPrintRenderer printRenderer = this.PrintRenderer as TableViewDefinitionPrintRenderer;
      if (printRenderer != null && printRenderer.PrintPages.Count > 0 && !this.PrintHierarchy)
        return num2 * printRenderer.PrintPages.Count;
      return num2;
    }

    public virtual void Reset()
    {
      this.PrintTraverser.Reset();
      this.PrintRenderer.Reset();
    }

    protected virtual void WirePrintRendererEvents()
    {
      if (this.printRenderer == null)
        return;
      this.printRenderer.ChildViewPrinting += new ChildViewPrintingEventHandler(this.renderer_ChildViewPrinting);
      this.printRenderer.PrintCellFormatting += new PrintCellFormattingEventHandler(this.renderer_PrintCellFormatting);
      this.printRenderer.PrintCellPaint += new PrintCellPaintEventHandler(this.renderer_PrintCellPaint);
    }

    protected virtual void UnwirePrintRendererEvents()
    {
      if (this.printRenderer == null)
        return;
      this.printRenderer.PrintCellPaint -= new PrintCellPaintEventHandler(this.renderer_PrintCellPaint);
      this.printRenderer.PrintCellFormatting -= new PrintCellFormattingEventHandler(this.renderer_PrintCellFormatting);
      this.printRenderer.ChildViewPrinting -= new ChildViewPrintingEventHandler(this.renderer_ChildViewPrinting);
    }

    [Description("Fires when the content of a print cell is painted, allows custom painting.")]
    public event PrintCellPaintEventHandler PrintCellPaint;

    [Browsable(true)]
    [Category("Action")]
    protected virtual void OnPrintCellPaint(object sender, PrintCellPaintEventArgs e)
    {
      this.GridView.OnPrintCellPaint(sender, e);
      if (this.PrintCellPaint == null)
        return;
      this.PrintCellPaint(sender, e);
    }

    [Description("Fires when the content of a print cell needs to be formatted for print.")]
    public event PrintCellFormattingEventHandler PrintCellFormatting;

    [Browsable(true)]
    [Category("Action")]
    protected virtual void OnPrintCellFormatting(object sender, PrintCellFormattingEventArgs e)
    {
      this.GridView.OnPrintCellFormatting(sender, e);
      if (this.PrintCellFormatting == null)
        return;
      this.PrintCellFormatting(sender, e);
    }

    [Description("Fires for hierarchy rows with more than one child views.")]
    public event ChildViewPrintingEventHandler ChildViewPrinting;

    protected virtual void OnChildViewPrinting(object sender, ChildViewPrintingEventArgs e)
    {
      this.GridView.OnChildViewPrinting(sender, e);
      if (this.ChildViewPrinting == null)
        return;
      this.ChildViewPrinting((object) this, e);
    }

    private void renderer_PrintCellPaint(object sender, PrintCellPaintEventArgs e)
    {
      this.OnPrintCellPaint(sender, e);
    }

    private void renderer_PrintCellFormatting(object sender, PrintCellFormattingEventArgs e)
    {
      this.OnPrintCellFormatting(sender, e);
    }

    private void renderer_ChildViewPrinting(object sender, ChildViewPrintingEventArgs e)
    {
      this.OnChildViewPrinting(sender, e);
    }
  }
}
