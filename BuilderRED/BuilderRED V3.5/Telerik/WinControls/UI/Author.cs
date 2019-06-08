// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Author
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class Author
  {
    private object data;
    private Image avatar;
    private string name;

    public Author(Image avatar, string name)
      : this(avatar, name, (object) null)
    {
    }

    public Author(Image avatar, string name, object data)
    {
      this.data = data;
      this.avatar = avatar;
      this.name = name;
    }

    public object Data
    {
      get
      {
        return this.data;
      }
      set
      {
        this.data = value;
      }
    }

    public Image Avatar
    {
      get
      {
        return this.avatar;
      }
      set
      {
        this.avatar = value;
      }
    }

    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }
  }
}
