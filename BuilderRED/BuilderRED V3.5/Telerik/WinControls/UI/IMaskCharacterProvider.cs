// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IMaskCharacterProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public interface IMaskCharacterProvider
  {
    string ToString(bool includePromt, bool includeLiterals);

    bool Set(string input, out int testPosition, out MaskedTextResultHint resultHint);

    bool RemoveAt(int startPosition, int endPosition);

    char PromptChar { get; set; }

    void KeyPress(object sender, KeyPressEventArgs e);

    void KeyDown(object sender, KeyEventArgs e);

    bool Delete();
  }
}
