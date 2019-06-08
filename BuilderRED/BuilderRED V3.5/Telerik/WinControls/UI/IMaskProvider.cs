// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IMaskProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Globalization;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public interface IMaskProvider
  {
    void KeyDown(object sender, KeyEventArgs e);

    void KeyPress(object sender, KeyPressEventArgs e);

    bool Validate(string value);

    bool Click();

    RadTextBoxItem TextBoxItem { get; }

    string ToString(bool includePromt, bool includeLiterals);

    IMaskProvider Clone();

    CultureInfo Culture { get; }

    string Mask { get; }

    bool IncludePrompt { get; set; }

    char PromptChar { get; set; }

    object Value { get; set; }

    bool Delete();
  }
}
