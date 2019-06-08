// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FormControlBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [DesignTimeVisible(false)]
  [ToolboxItem(false)]
  public abstract class FormControlBehavior : Component
  {
    private bool shouldHandleCreateChildItems = true;
    private bool isDisposed;
    protected IComponentTreeHandler targetHandler;
    private WndProcInvoker baseWndProcCallback;
    private WndProcInvoker defWndProcCallback;

    public FormControlBehavior()
    {
    }

    public FormControlBehavior(IComponentTreeHandler targetTreeHandler)
    {
      this.targetHandler = targetTreeHandler;
      if (this.targetHandler == null)
        return;
      if (!(this.targetHandler.RootElement.ElementTree.Control is RadFormControlBase))
        throw new ArgumentException("The implementation of IComponentTreeHandler is not of the correct type.");
      this.OnFormAssociated();
    }

    public FormControlBehavior(IComponentTreeHandler targetTreeHandler, bool handleCreateChildItems)
    {
      this.shouldHandleCreateChildItems = handleCreateChildItems;
      this.targetHandler = targetTreeHandler;
      if (this.targetHandler == null)
        return;
      if (!(this.targetHandler.RootElement.ElementTree.Control is RadFormControlBase))
        throw new ArgumentException("The implementation of IComponentTreeHandler is not of the correct type.");
      this.OnFormAssociated();
    }

    public abstract RadElement FormElement { get; }

    internal bool HandlesCreateChildItems
    {
      get
      {
        return this.shouldHandleCreateChildItems;
      }
    }

    public abstract Padding BorderWidth { get; }

    public abstract int CaptionHeight { get; }

    public abstract Padding ClientMargin { get; }

    public virtual void FormHandleCreated()
    {
    }

    protected virtual void OnFormAssociated()
    {
    }

    internal void SetBaseWndProcCallback(WndProcInvoker callback)
    {
      this.baseWndProcCallback = callback;
    }

    internal void SetDefWndProcCallback(WndProcInvoker callback)
    {
      this.defWndProcCallback = callback;
    }

    protected void CallBaseWndProc(ref Message m)
    {
      if (this.baseWndProcCallback == null)
        return;
      this.baseWndProcCallback(ref m);
    }

    protected void CallDefWndProc(ref Message m)
    {
      if (this.defWndProcCallback == null)
        return;
      this.defWndProcCallback(ref m);
    }

    public abstract bool HandleWndProc(ref Message m);

    public abstract void InvalidateElement(RadElement element, Rectangle bounds);

    public virtual bool OnAssociatedFormPaint(PaintEventArgs args)
    {
      return false;
    }

    public virtual bool OnAssociatedFormPaintBackground(PaintEventArgs args)
    {
      return false;
    }

    public virtual CreateParams CreateParams(CreateParams parameters)
    {
      return parameters;
    }

    public virtual void CreateChildItems(RadElement parent)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (this.isDisposed)
        return;
      if (disposing)
      {
        if (this.defWndProcCallback != null)
          this.defWndProcCallback = (WndProcInvoker) null;
        if (this.baseWndProcCallback != null)
          this.baseWndProcCallback = (WndProcInvoker) null;
      }
      this.isDisposed = true;
    }
  }
}
