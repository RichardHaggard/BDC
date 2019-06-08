// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IInputElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public interface IInputElement
  {
    event KeyEventHandler KeyDown;

    event KeyEventHandler KeyUp;

    event KeyPressEventHandler KeyPress;

    event EventHandler MouseEnter;

    event EventHandler MouseLeave;

    event MouseEventHandler MouseMove;

    event MouseEventHandler MouseUp;

    event MouseEventHandler MouseDown;

    event MouseEventHandler MouseWheel;

    bool CaptureMouse();

    bool Focus();

    void ReleaseMouseCapture();

    bool Focusable { get; set; }

    bool IsEnabled { get; }

    bool IsFocused { get; }

    bool IsMouseCaptured { get; }

    bool IsMouseOver { get; }
  }
}
