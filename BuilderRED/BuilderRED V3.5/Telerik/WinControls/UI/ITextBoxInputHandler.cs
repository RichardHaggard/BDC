// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ITextBoxInputHandler
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public interface ITextBoxInputHandler
  {
    bool ProcessKeyDown(KeyEventArgs e);

    bool ProcessKeyUp(KeyEventArgs e);

    bool ProcessKeyPress(KeyPressEventArgs e);

    bool ProcessMouseDown(MouseEventArgs e);

    bool ProcessMouseUp(MouseEventArgs e);

    bool ProcessMouseMove(MouseEventArgs e);

    bool ProcessMouseWheel(MouseEventArgs e);

    bool ProcessDoubleClick(EventArgs e);

    void ProcessMouseLeave(EventArgs e);

    void PrcessMouseEnter(EventArgs e);
  }
}
