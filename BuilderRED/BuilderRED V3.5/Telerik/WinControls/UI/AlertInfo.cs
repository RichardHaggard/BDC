// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.AlertInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class AlertInfo
  {
    private RadItemCollection alertButtons;
    private string captionText;
    private string contentText;
    private Image contentImage;

    public AlertInfo(string contentText)
    {
      this.contentText = contentText;
    }

    public AlertInfo(string contentText, string captionText)
      : this(contentText)
    {
      this.captionText = captionText;
    }

    public AlertInfo(string contentText, string captionText, Image alertImage)
      : this(contentText, captionText)
    {
      this.contentImage = alertImage;
    }

    public AlertInfo(
      string contentText,
      string captionText,
      Image alertImage,
      RadItemCollection alertButtons)
      : this(contentText, captionText, alertImage)
    {
      this.alertButtons = alertButtons;
    }
  }
}
