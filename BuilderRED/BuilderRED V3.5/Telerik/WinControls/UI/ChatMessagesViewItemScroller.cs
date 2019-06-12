// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatMessagesViewItemScroller
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class ChatMessagesViewItemScroller : ItemScroller<BaseChatDataItem>
  {
    public override void UpdateScrollStep()
    {
      bool flag = this.Scrollbar.Value == this.Scrollbar.Maximum - this.Scrollbar.LargeChange + 1;
      base.UpdateScrollStep();
      if (!flag)
        return;
      this.Scrollbar.Value = this.Scrollbar.Maximum - this.Scrollbar.LargeChange + 1;
    }

    public override void UpdateScrollRange()
    {
      bool flag = this.Scrollbar.Value == this.Scrollbar.Maximum - this.Scrollbar.LargeChange + 1;
      base.UpdateScrollRange();
      if (!flag)
        return;
      this.Scrollbar.Value = this.Scrollbar.Maximum - this.Scrollbar.LargeChange + 1;
    }
  }
}
