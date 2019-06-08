// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridPrintSettings
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridPrintSettings
  {
    private Font groupsFont;
    private Font headersFont;
    private Font dataCellsFont;
    private Font summariesFont;
    private int hierarchyIndent;
    private bool printGrouping;
    private bool printSummaries;
    private bool printHierarchy;
    private bool printHiddenRows;
    private bool printHiddenColumns;
    private bool printAlternatingRowColor;
    private bool printHeaderOnEachPage;
    private bool printAllPages;
    private Color dataCellsBackColor;
    private Color headersBackColor;
    private Color groupsBackColor;
    private Color summariesBackColor;
    private Color generalBorderColor;
    private Padding cellPadding;
    private Color alternatingRowColor;
    private PrintFitWidthMode printFitWidthMode;
    private ChildViewPrintMode childViewPrintMode;

    public GridPrintSettings()
    {
      this.hierarchyIndent = 20;
      this.PrintGrouping = true;
      this.PrintSummaries = true;
      this.PrintHierarchy = false;
      this.PrintHiddenRows = false;
      this.PrintHiddenColumns = false;
      this.PrintHeaderOnEachPage = true;
      this.printAlternatingRowColor = false;
      this.dataCellsBackColor = Color.White;
      this.headersBackColor = Color.FromArgb(183, 183, 183);
      this.groupsBackColor = Color.FromArgb(149, 149, 149);
      this.alternatingRowColor = Color.FromArgb(225, 225, 225);
      this.summariesBackColor = Color.Gray;
      this.generalBorderColor = Color.Black;
      this.cellPadding = new Padding(2);
      this.FitWidthMode = PrintFitWidthMode.FitPageWidth;
      this.childViewPrintMode = ChildViewPrintMode.PrintCurrentlyActiveView;
    }

    public Font CellFont
    {
      get
      {
        return this.dataCellsFont;
      }
      set
      {
        if (value == null)
          return;
        this.dataCellsFont = value;
      }
    }

    public Font GroupRowFont
    {
      get
      {
        return this.groupsFont;
      }
      set
      {
        if (value == null)
          return;
        this.groupsFont = value;
      }
    }

    public Font HeaderCellFont
    {
      get
      {
        return this.headersFont;
      }
      set
      {
        if (value == null)
          return;
        this.headersFont = value;
      }
    }

    public Font SummaryCellFont
    {
      get
      {
        return this.summariesFont;
      }
      set
      {
        if (value == null)
          return;
        this.summariesFont = value;
      }
    }

    public int HierarchyIndent
    {
      get
      {
        return this.hierarchyIndent;
      }
      set
      {
        this.hierarchyIndent = value;
      }
    }

    public bool PrintGrouping
    {
      get
      {
        return this.printGrouping;
      }
      set
      {
        this.printGrouping = value;
      }
    }

    public bool PrintSummaries
    {
      get
      {
        return this.printSummaries;
      }
      set
      {
        this.printSummaries = value;
      }
    }

    public bool PrintHierarchy
    {
      get
      {
        return this.printHierarchy;
      }
      set
      {
        this.printHierarchy = value;
      }
    }

    public bool PrintHiddenRows
    {
      get
      {
        return this.printHiddenRows;
      }
      set
      {
        this.printHiddenRows = value;
      }
    }

    public bool PrintHiddenColumns
    {
      get
      {
        return this.printHiddenColumns;
      }
      set
      {
        this.printHiddenColumns = value;
      }
    }

    public bool PrintAlternatingRowColor
    {
      get
      {
        return this.printAlternatingRowColor;
      }
      set
      {
        this.printAlternatingRowColor = value;
      }
    }

    public bool PrintHeaderOnEachPage
    {
      get
      {
        return this.printHeaderOnEachPage;
      }
      set
      {
        this.printHeaderOnEachPage = value;
      }
    }

    public bool PrintAllPages
    {
      get
      {
        return this.printAllPages;
      }
      set
      {
        this.printAllPages = value;
      }
    }

    public Color CellBackColor
    {
      get
      {
        return this.dataCellsBackColor;
      }
      set
      {
        this.dataCellsBackColor = value;
      }
    }

    public Color HeaderCellBackColor
    {
      get
      {
        return this.headersBackColor;
      }
      set
      {
        this.headersBackColor = value;
      }
    }

    public Color GroupRowBackColor
    {
      get
      {
        return this.groupsBackColor;
      }
      set
      {
        this.groupsBackColor = value;
      }
    }

    public Color SummaryCellBackColor
    {
      get
      {
        return this.summariesBackColor;
      }
      set
      {
        this.summariesBackColor = value;
      }
    }

    public Color BorderColor
    {
      get
      {
        return this.generalBorderColor;
      }
      set
      {
        this.generalBorderColor = value;
      }
    }

    public Padding CellPadding
    {
      get
      {
        return this.cellPadding;
      }
      set
      {
        this.cellPadding = value;
      }
    }

    public Color AlternatingRowColor
    {
      get
      {
        return this.alternatingRowColor;
      }
      set
      {
        this.alternatingRowColor = value;
      }
    }

    public PrintFitWidthMode FitWidthMode
    {
      get
      {
        return this.printFitWidthMode;
      }
      set
      {
        this.printFitWidthMode = value;
      }
    }

    public ChildViewPrintMode ChildViewPrintMode
    {
      get
      {
        return this.childViewPrintMode;
      }
      set
      {
        this.childViewPrintMode = value;
      }
    }
  }
}
