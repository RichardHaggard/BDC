// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GroupFieldDragDropContext
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GroupFieldDragDropContext
  {
    private SortDescriptor sortDescription;
    private GroupDescriptor groupDescription;
    private GridViewTemplate template;

    public GroupFieldDragDropContext(
      GroupDescriptor groupDescription,
      SortDescriptor sortDescription,
      GridViewTemplate template)
    {
      this.groupDescription = groupDescription;
      this.sortDescription = sortDescription;
      this.template = template;
    }

    public GridViewTemplate ViewTemplate
    {
      get
      {
        return this.template;
      }
    }

    public GroupDescriptor GroupDescription
    {
      get
      {
        return this.groupDescription;
      }
    }

    public SortDescriptor SortDescription
    {
      get
      {
        return this.sortDescription;
      }
    }
  }
}
