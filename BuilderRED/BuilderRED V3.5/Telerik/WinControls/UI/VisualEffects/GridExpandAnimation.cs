// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VisualEffects.GridExpandAnimation
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI.VisualEffects
{
  public abstract class GridExpandAnimation
  {
    private GridTableElement tableElement;
    private bool isAnimating;

    public event EventHandler UpdateViewNeeded;

    protected GridExpandAnimation(GridTableElement tableElement)
    {
      this.tableElement = tableElement;
    }

    protected GridTableElement TableElement
    {
      get
      {
        return this.tableElement;
      }
    }

    public bool IsAnimating
    {
      get
      {
        return this.isAnimating;
      }
      protected set
      {
        this.isAnimating = value;
      }
    }

    protected virtual void OnUpdateViewNeeded(EventArgs e)
    {
      EventHandler updateViewNeeded = this.UpdateViewNeeded;
      if (updateViewNeeded == null)
        return;
      updateViewNeeded((object) this, e);
    }

    public abstract void Expand(GridViewRowInfo info, float maxOffset, int rowIndex);

    public abstract void Collapse(GridViewRowInfo rowInfo, float maxOffset, int rowIndex);
  }
}
