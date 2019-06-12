// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ITextBoxNavigator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public interface ITextBoxNavigator
  {
    TextPosition SelectionStart { get; set; }

    TextPosition SelectionEnd { get; set; }

    TextPosition CaretPosition { get; set; }

    int SelectionLength { get; }

    event SelectionChangingEventHandler SelectionChanging;

    event SelectionChangedEventHandler SelectionChanged;

    void SuspendNotifications();

    void ResumeNotifications();

    bool Navigate(KeyEventArgs keys);

    void SaveSelection();

    void RestoreSelection();

    bool ScrollToCaret();

    bool Select(TextPosition start, TextPosition end);

    TextPosition GetPositionFromPoint(PointF point);

    TextPosition GetPositionFromOffset(int offset);

    TextPosition GetPreviousPosition(TextPosition position);

    TextPosition GetNextPosition(TextPosition position);
  }
}
