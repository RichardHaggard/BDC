// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridBehaviorImpl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public abstract class GridBehaviorImpl : IGridBehavior
  {
    private RadGridViewElement gridViewElement;

    public virtual RadGridViewElement GridViewElement
    {
      get
      {
        return this.gridViewElement;
      }
    }

    public virtual RadGridView GridControl
    {
      get
      {
        if (this.gridViewElement != null && this.gridViewElement.ElementTree != null)
          return this.gridViewElement.ElementTree.Control as RadGridView;
        return (RadGridView) null;
      }
    }

    public virtual void Initialize(RadGridViewElement gridRootElement)
    {
      this.gridViewElement = gridRootElement;
    }

    public abstract bool OnClick(EventArgs e);

    public abstract bool OnDoubleClick(EventArgs e);

    public abstract bool ProcessKey(KeyEventArgs keys);

    public abstract bool ProcessKeyDown(KeyEventArgs keys);

    public abstract bool ProcessKeyUp(KeyEventArgs keys);

    public abstract bool ProcessKeyPress(KeyPressEventArgs keys);

    public abstract bool OnMouseDown(MouseEventArgs e);

    public abstract bool OnMouseUp(MouseEventArgs e);

    public abstract bool OnMouseDoubleClick(MouseEventArgs e);

    public abstract bool OnMouseMove(MouseEventArgs e);

    public abstract bool OnMouseWheel(MouseEventArgs e);

    public abstract bool OnContextMenu(MouseEventArgs e);

    public abstract bool OnMouseEnter(EventArgs e);

    public abstract bool OnMouseLeave(EventArgs e);
  }
}
