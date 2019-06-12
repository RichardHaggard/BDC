// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ToolbarActionDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class ToolbarActionDataItem
  {
    private Image image;
    private object userData;

    public ToolbarActionDataItem(Image image)
    {
      this.image = image;
    }

    public ToolbarActionDataItem(Image image, object userData)
      : this(image)
    {
      this.userData = userData;
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
  }
}
