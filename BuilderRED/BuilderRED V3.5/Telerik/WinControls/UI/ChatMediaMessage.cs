// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatMediaMessage
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class ChatMediaMessage : ChatMessage
  {
    private Image displayImage;
    private Size size;

    public ChatMediaMessage(Image displayImage, Size size, Author author, DateTime timeStamp)
      : this(displayImage, size, author, timeStamp, (object) null)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("The \"userData\" parameter has been moved to the last position. This is required to keep the API consistent across chat message constructors.")]
    public ChatMediaMessage(
      Image displayImage,
      Size size,
      object userData,
      Author author,
      DateTime timeStamp)
      : this(displayImage, size, author, timeStamp, userData)
    {
    }

    public ChatMediaMessage(
      Image displayImage,
      Size size,
      Author author,
      DateTime timeStamp,
      object userData)
      : base(author, timeStamp, userData)
    {
      this.displayImage = displayImage;
      this.size = size;
    }

    public Image DisplayImage
    {
      get
      {
        return this.displayImage;
      }
    }

    public Size Size
    {
      get
      {
        return this.size;
      }
    }
  }
}
