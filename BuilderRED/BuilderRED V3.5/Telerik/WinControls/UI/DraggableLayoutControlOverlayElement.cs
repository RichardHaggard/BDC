// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DraggableLayoutControlOverlayElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class DraggableLayoutControlOverlayElement : LightVisualElement
  {
    private RadItemOwnerCollection items;

    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.DrawBorder = false;
      this.ShouldHandleMouseInput = false;
      this.NotifyParentOnMouseInput = true;
      this.items = new RadItemOwnerCollection();
      this.items.Owner = (RadElement) this;
      this.AllowDrop = true;
    }

    protected override bool ProcessDragOver(Point mousePosition, ISupportDrag dragObject)
    {
      if (this.Parent is RootRadElement && this.Items.Count == 0)
        return true;
      return base.ProcessDragOver(mousePosition, dragObject);
    }
  }
}
