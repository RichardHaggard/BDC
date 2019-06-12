// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTextBoxEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class RadTextBoxEditorElement : RadTextBoxElement
  {
    static RadTextBoxEditorElement()
    {
      RadItem.TextProperty.OverrideMetadata(typeof (RadTextBoxEditorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.Cancelable));
    }

    public RadTextBoxEditorElement()
    {
      this.TextBoxItem.RouteMessages = false;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.MinSize = new Size(0, 20);
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadTextBoxElement);
      }
    }

    public bool IsCaretAtFirstLine
    {
      get
      {
        return (int) Telerik.WinControls.NativeMethods.SendMessage(this.TextBoxItem.HostedControl.Handle, 201, -1, 0) == this.TextBoxItem.GetLineFromCharIndex(0);
      }
    }

    public bool IsCaretAtLastLine
    {
      get
      {
        return (int) Telerik.WinControls.NativeMethods.SendMessage(this.TextBoxItem.HostedControl.Handle, 201, -1, 0) == this.TextBoxItem.GetLineFromCharIndex(this.Text.Length - 1);
      }
    }
  }
}
