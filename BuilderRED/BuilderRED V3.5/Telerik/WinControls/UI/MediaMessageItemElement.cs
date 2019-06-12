// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MediaMessageItemElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class MediaMessageItemElement : BaseChatItemElement
  {
    protected override LightVisualElement CreateMainMessageElement()
    {
      LightVisualElement mainMessageElement = base.CreateMainMessageElement();
      mainMessageElement.SmoothingMode = SmoothingMode.HighQuality;
      mainMessageElement.ImageLayout = ImageLayout.Stretch;
      mainMessageElement.Shape = (ElementShape) new RoundRectShape(15);
      mainMessageElement.NotifyParentOnMouseInput = true;
      return mainMessageElement;
    }

    public override bool IsCompatible(BaseChatDataItem data, object context)
    {
      return (object) data.GetType() == (object) typeof (MediaMessageDataItem);
    }

    public override void Synchronize()
    {
      this.MainMessageElement.Image = ((MediaMessageDataItem) this.Data).MediaMessage.DisplayImage;
      base.Synchronize();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF1 = base.MeasureOverride(availableSize);
      if (this.Data == null)
        return sizeF1;
      this.MainMessageElement.Measure((SizeF) TelerikDpiHelper.ScaleSize(((MediaMessageDataItem) this.Data).MediaMessage.Size, this.DpiScaleFactor));
      SizeF sizeF2 = new SizeF(this.AvatarPictureElement.DesiredSize.Width + this.MainMessageElement.DesiredSize.Width + (float) this.Margin.Horizontal, Math.Max(this.AvatarPictureElement.DesiredSize.Height, this.MainMessageElement.DesiredSize.Height) + this.NameLabelElement.DesiredSize.Height + (float) this.Margin.Vertical);
      this.Data.ActualSize = sizeF2;
      return sizeF2;
    }
  }
}
