// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatCardAction
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class ChatCardAction
  {
    private string text;
    private Image image;
    private object value;

    public ChatCardAction(string text)
      : this(text, (Image) null, (object) text)
    {
    }

    public ChatCardAction(string text, Image image, object value)
    {
      this.text = text;
      this.image = image;
      this.value = value;
    }

    public string Text
    {
      get
      {
        return this.text;
      }
      set
      {
        this.text = value;
      }
    }

    public Image Image
    {
      get
      {
        return this.image;
      }
      set
      {
        this.image = value;
      }
    }

    public object Value
    {
      get
      {
        return this.value;
      }
      set
      {
        this.value = value;
      }
    }
  }
}
