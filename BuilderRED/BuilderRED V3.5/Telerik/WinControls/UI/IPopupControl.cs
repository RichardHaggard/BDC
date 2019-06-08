// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IPopupControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public interface IPopupControl
  {
    void ShowPopup(Rectangle alignmentRect);

    void ClosePopup(RadPopupCloseReason reason);

    void ClosePopup(PopupCloseInfo closeInfo);

    bool CanClosePopup(RadPopupCloseReason reason);

    List<IPopupControl> Children { get; }

    IPopupControl OwnerPopup { get; }

    Rectangle Bounds { get; }

    RadElement OwnerElement { get; }

    bool OnKeyDown(Keys keyData);

    bool OnMouseWheel(Control target, int delta);
  }
}
