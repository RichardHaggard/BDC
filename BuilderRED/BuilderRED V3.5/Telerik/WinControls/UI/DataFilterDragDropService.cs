// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterDragDropService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class DataFilterDragDropService : TreeViewDragDropService
  {
    public DataFilterDragDropService(RadDataFilterElement owner)
      : base((RadTreeViewElement) owner)
    {
    }

    protected override bool CanStart(object context)
    {
      if (base.CanStart(context))
      {
        BaseDataFilterNodeElement filterNodeElement = context as BaseDataFilterNodeElement;
        if (filterNodeElement == null || filterNodeElement.ElementTree == null || (filterNodeElement.ElementTree.Control == null || filterNodeElement.Data == filterNodeElement.DataFilterElement.RootNode))
          return false;
        Point client = filterNodeElement.ElementTree.Control.PointToClient(Control.MousePosition);
        RadElement elementAtPoint = filterNodeElement.ElementTree.GetElementAtPoint(client);
        if (elementAtPoint != null && filterNodeElement.DragElement == elementAtPoint)
          return true;
      }
      return false;
    }

    protected override bool CanDragOver(
      DropPosition dropPosition,
      TreeNodeElement targetNodeElement)
    {
      if ((this.Owner as RadDataFilterElement).RootNode == targetNodeElement.Data && dropPosition != DropPosition.AsChildNode)
        return false;
      return base.CanDragOver(dropPosition, targetNodeElement);
    }

    protected override bool CancelPreviewDragDrop(RadDropEventArgs e)
    {
      return e.Handled;
    }
  }
}
