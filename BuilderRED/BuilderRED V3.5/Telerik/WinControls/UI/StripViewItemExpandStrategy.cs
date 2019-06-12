// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StripViewItemExpandStrategy
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  internal class StripViewItemExpandStrategy
  {
    private StripViewLayoutInfo layoutInfo;
    private StripViewItemExpandStrategy.ExpandInfo expandInfo;

    public StripViewItemExpandStrategy(StripViewLayoutInfo info)
    {
      this.layoutInfo = info;
      this.expandInfo = new StripViewItemExpandStrategy.ExpandInfo();
    }

    public void Execute()
    {
      this.UpdateExpandInfo();
      if ((double) this.expandInfo.layoutLength >= (double) this.layoutInfo.availableLength)
        return;
      int num1 = (int) ((double) this.layoutInfo.availableLength - (double) this.expandInfo.layoutLength);
      if (num1 < this.layoutInfo.itemCount)
        return;
      int num2 = num1 % this.layoutInfo.itemCount;
      int num3 = (num1 - num2) / this.layoutInfo.itemCount;
      foreach (PageViewItemSizeInfo viewItemSizeInfo in this.layoutInfo.items)
        viewItemSizeInfo.ChangeLayoutLength((int) viewItemSizeInfo.layoutLength + num3);
    }

    private void UpdateExpandInfo()
    {
      foreach (PageViewItemSizeInfo viewItemSizeInfo in this.layoutInfo.items)
        this.expandInfo.layoutLength += viewItemSizeInfo.layoutLength + (float) viewItemSizeInfo.marginLength;
      this.expandInfo.layoutLength += this.layoutInfo.paddingLength;
      this.expandInfo.layoutLength += (float) ((this.layoutInfo.itemCount - 1) * this.layoutInfo.itemSpacing);
    }

    private class ExpandInfo
    {
      public float layoutLength;
    }
  }
}
