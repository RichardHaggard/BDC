// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatListOverlay
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ChatListOverlay : BaseChatItemOverlay
  {
    private RadListView listView;

    public ChatListOverlay(string title)
      : base(title)
    {
      this.listView.SelectedIndexChanged += new EventHandler(this.ListViewElement_SelectedIndexChanged);
    }

    public RadListView ListView
    {
      get
      {
        return this.listView;
      }
    }

    private void ListViewElement_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.listView.SelectedItem == null)
        return;
      this.CurrentValue = this.listView.SelectedItem.Value;
    }

    protected override RadElement CreateMainElement()
    {
      this.listView = new RadListView();
      this.listView.AllowEdit = false;
      return (RadElement) new RadHostItem((Control) this.listView);
    }

    protected override void DisposeManagedResources()
    {
      this.listView.SelectedIndexChanged -= new EventHandler(this.ListViewElement_SelectedIndexChanged);
      base.DisposeManagedResources();
    }
  }
}
