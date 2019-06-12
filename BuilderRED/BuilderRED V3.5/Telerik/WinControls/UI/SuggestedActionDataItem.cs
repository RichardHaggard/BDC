// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SuggestedActionDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class SuggestedActionDataItem
  {
    private string text;
    private Image image;
    private object userData;

    public SuggestedActionDataItem(string text)
      : this(text, (Image) null, (object) null)
    {
    }

    public SuggestedActionDataItem(string text, Image image)
      : this(text, image, (object) null)
    {
    }

    public SuggestedActionDataItem(string text, Image image, object userData)
    {
      this.text = text;
      this.image = image;
      this.userData = userData;
    }

    public object UserData
    {
      get
      {
        return this.userData;
      }
      set
      {
        this.userData = value;
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
  }
}
