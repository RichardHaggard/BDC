// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseVirtualGridColumnLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public abstract class BaseVirtualGridColumnLayout
  {
    private VirtualGridTableElement tableElement;

    public VirtualGridTableElement TableElement
    {
      get
      {
        return this.tableElement;
      }
    }

    public void Initialize(VirtualGridTableElement tableElement)
    {
      this.tableElement = tableElement;
    }

    public abstract void CalculateColumnWidths(SizeF availableSize);

    public abstract void StartColumnResize(int column);

    public abstract bool ResizeColumn(int delta);

    public abstract void EndResizeColumn();

    public abstract void ResetCache();
  }
}
