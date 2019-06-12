// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IGridBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public interface IGridBehavior
  {
    RadGridViewElement GridViewElement { get; }

    RadGridView GridControl { get; }

    void Initialize(RadGridViewElement gridRootElement);

    bool OnClick(EventArgs e);

    bool OnDoubleClick(EventArgs e);

    bool ProcessKey(KeyEventArgs keys);

    bool ProcessKeyDown(KeyEventArgs keys);

    bool ProcessKeyUp(KeyEventArgs keys);

    bool ProcessKeyPress(KeyPressEventArgs keys);

    bool OnMouseDown(MouseEventArgs e);

    bool OnMouseUp(MouseEventArgs e);

    bool OnMouseDoubleClick(MouseEventArgs e);

    bool OnMouseEnter(EventArgs e);

    bool OnMouseLeave(EventArgs e);

    bool OnMouseMove(MouseEventArgs e);

    bool OnMouseWheel(MouseEventArgs e);

    bool OnContextMenu(MouseEventArgs e);
  }
}
