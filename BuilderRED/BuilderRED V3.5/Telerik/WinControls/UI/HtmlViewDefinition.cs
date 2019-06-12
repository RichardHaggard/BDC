// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.HtmlViewDefinition
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace Telerik.WinControls.UI
{
  public class HtmlViewDefinition : TableViewDefinition
  {
    private RowTemplate rowTemplate = new RowTemplate();

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(null)]
    [Description("Gets or sets a the row template that specifies how to visualize the rows in this template.")]
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public RowTemplate RowTemplate
    {
      get
      {
        return this.rowTemplate;
      }
    }

    public override IRowView CreateViewUIElement(GridViewInfo viewInfo)
    {
      foreach (GridViewColumn column in (Collection<GridViewDataColumn>) viewInfo.ViewTemplate.Columns)
      {
        column.SuspendPropertyNotifications();
        column.PinPosition = PinnedColumnPosition.None;
        column.ResumePropertyNotifications();
      }
      GridTableElement gridTableElement = new GridTableElement();
      gridTableElement.ViewElement.RowLayout = this.CreateRowLayout();
      return (IRowView) gridTableElement;
    }

    public void ReadXml(string fileName)
    {
      this.rowTemplate.ReadXml(fileName);
    }

    public void ReadXml(Stream stream)
    {
      this.rowTemplate.ReadXml(stream);
    }

    public override IGridRowLayout CreateRowLayout()
    {
      return (IGridRowLayout) new HtmlViewRowLayout(this);
    }
  }
}
