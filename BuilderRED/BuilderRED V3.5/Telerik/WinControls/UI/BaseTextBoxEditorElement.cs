// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseTextBoxEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class BaseTextBoxEditorElement : RadTextBoxElement
  {
    static BaseTextBoxEditorElement()
    {
      RadItem.TextProperty.OverrideMetadata(typeof (BaseTextBoxEditorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.Cancelable));
    }

    public BaseTextBoxEditorElement()
    {
      this.TextBoxItem.RouteMessages = false;
      this.DefaultSize = new Size(150, 20);
      this.Alignment = ContentAlignment.MiddleCenter;
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

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      availableSize.Width = Math.Min(availableSize.Width, (float) this.DefaultSize.Width);
      SizeF sizeF = base.MeasureOverride(availableSize);
      if ((double) sizeF.Width < (double) availableSize.Width && (double) sizeF.Width < (double) this.DefaultSize.Width)
        sizeF.Width = availableSize.Width;
      return sizeF;
    }
  }
}
