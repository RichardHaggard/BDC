// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewControlCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadPageViewControlCollection : Control.ControlCollection
  {
    private RadPageView owner;
    private byte suspendOwnerNotify;

    public RadPageViewControlCollection(RadPageView owner)
      : base((Control) owner)
    {
      this.owner = owner;
    }

    public override void Add(Control value)
    {
      if (value is HostedTextBoxBase)
      {
        base.Add(value);
      }
      else
      {
        RadPageViewPage page = value as RadPageViewPage;
        this.ValidatePage(page);
        if (this.suspendOwnerNotify == (byte) 0)
        {
          RadPageViewCancelEventArgs e = new RadPageViewCancelEventArgs(page);
          this.owner.OnPageAdding(e);
          if (e.Cancel)
            return;
        }
        base.Add(value);
        if (this.suspendOwnerNotify != (byte) 0)
          return;
        this.owner.OnPageAdded(new RadPageViewEventArgs(page));
      }
    }

    public override void Clear()
    {
      if (this.Count == 0)
        return;
      if (this.suspendOwnerNotify == (byte) 0)
      {
        CancelEventArgs e = new CancelEventArgs();
        this.owner.OnPagesClearing(e);
        if (e.Cancel)
          return;
      }
      base.Clear();
      if (this.suspendOwnerNotify != (byte) 0)
        return;
      this.owner.OnPagesCleared(EventArgs.Empty);
    }

    public override void Remove(Control value)
    {
      if (value is HostedTextBoxBase)
      {
        base.Remove(value);
      }
      else
      {
        RadPageViewPage page = value as RadPageViewPage;
        this.ValidatePage(page);
        if (this.suspendOwnerNotify == (byte) 0)
        {
          RadPageViewCancelEventArgs e = new RadPageViewCancelEventArgs(page);
          this.owner.OnPageRemoving(e);
          if (e.Cancel)
            return;
        }
        base.Remove(value);
        if (this.suspendOwnerNotify != (byte) 0)
          return;
        this.owner.OnPageRemoved(new RadPageViewEventArgs(page));
      }
    }

    public override void SetChildIndex(Control child, int newIndex)
    {
      if (!this.owner.AllowPageIndexChange)
        return;
      this.owner.DisablePageIndexChange();
      RadPageViewPage page = child as RadPageViewPage;
      this.ValidatePage(page);
      int num = this.IndexOf((Control) page);
      if (this.suspendOwnerNotify == (byte) 0)
      {
        RadPageViewIndexChangingEventArgs e = new RadPageViewIndexChangingEventArgs(page, newIndex, num);
        this.owner.OnPageIndexChanging(e);
        if (e.Cancel)
          return;
      }
      base.SetChildIndex(child, newIndex);
      if (this.suspendOwnerNotify != (byte) 0)
        return;
      this.owner.OnPageIndexChanged(new RadPageViewIndexChangedEventArgs(page, num, newIndex));
    }

    internal void SuspendOwnerNotify()
    {
      ++this.suspendOwnerNotify;
    }

    internal void ResumeOwnerNotify()
    {
      if (this.suspendOwnerNotify <= (byte) 0)
        return;
      --this.suspendOwnerNotify;
    }

    private void ValidatePage(RadPageViewPage page)
    {
      if (page == null)
        throw new ArgumentException("RadPageView accepts only RadPageViewPage instances as child controls");
    }
  }
}
