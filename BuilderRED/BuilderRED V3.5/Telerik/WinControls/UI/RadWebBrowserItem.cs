// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadWebBrowserItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class RadWebBrowserItem : RadHostItem
  {
    private static readonly object DocumentCompletedEventKey = new object();
    private static readonly object FileDownloadEventKey = new object();
    private static readonly object NavigatedEventKey = new object();
    private static readonly object NavigatingEventKey = new object();
    private static readonly object NewWindowEventKey = new object();
    private static readonly object PreviewKeyDownEventKey = new object();
    private static readonly object ProgressChangedEventKey = new object();
    private static readonly object SystemColorsChangedEventKey = new object();

    public RadWebBrowserItem()
      : base((Control) new RadWebBrowserBase())
    {
      WebBrowser webBrowserControl = this.WebBrowserControl;
      webBrowserControl.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.WebBrowserControl_DocumentCompleted);
      webBrowserControl.FileDownload += new EventHandler(this.WebBrowserControl_FileDownload);
      webBrowserControl.Navigating += new WebBrowserNavigatingEventHandler(this.WebBrowserControl_Navigating);
      webBrowserControl.Navigated += new WebBrowserNavigatedEventHandler(this.WebBrowserControl_Navigated);
      webBrowserControl.NewWindow += new CancelEventHandler(this.WebBrowserControl_NewWindow);
      webBrowserControl.PreviewKeyDown += new PreviewKeyDownEventHandler(this.WebBrowserControl_PreviewKeyDown);
      webBrowserControl.ProgressChanged += new WebBrowserProgressChangedEventHandler(this.WebBrowserControl_ProgressChanged);
      webBrowserControl.SystemColorsChanged += new EventHandler(this.WebBrowserControl_SystemColorsChanged);
      webBrowserControl.CausesValidationChanged += new EventHandler(this.WebBrowserControl_CausesValidationChanged);
      webBrowserControl.CanGoBackChanged += new EventHandler(this.WebBrowserControl_CanGoBackChanged);
      webBrowserControl.CanGoForwardChanged += new EventHandler(this.WebBrowserControl_CanGoForwardChanged);
      webBrowserControl.ContextMenuChanged += new EventHandler(this.WebBrowserControl_ContextMenuChanged);
      webBrowserControl.ContextMenuStripChanged += new EventHandler(this.WebBrowserControl_ContextMenuStripChanged);
      webBrowserControl.DockChanged += new EventHandler(this.WebBrowserControl_DockChanged);
      webBrowserControl.DocumentTitleChanged += new EventHandler(this.WebBrowserControl_DocumentTitleChanged);
      webBrowserControl.EncryptionLevelChanged += new EventHandler(this.WebBrowserControl_EncryptionLevelChanged);
      webBrowserControl.StatusTextChanged += new EventHandler(this.WebBrowserControl_StatusTextChanged);
    }

    public WebBrowser WebBrowserControl
    {
      get
      {
        return (WebBrowser) this.HostedControl;
      }
    }

    public Uri Url
    {
      get
      {
        return this.WebBrowserControl.Url;
      }
      set
      {
        if (!(this.WebBrowserControl.Url != value))
          return;
        this.WebBrowserControl.Url = value;
      }
    }

    public string DocumentText
    {
      get
      {
        return this.WebBrowserControl.DocumentText;
      }
      set
      {
        this.WebBrowserControl.DocumentText = value;
      }
    }

    public string DocumentTitle
    {
      get
      {
        return this.WebBrowserControl.DocumentTitle;
      }
    }

    private void WebBrowserControl_CausesValidationChanged(object sender, EventArgs e)
    {
      this.OnNotifyPropertyChanged("CausesValidation");
    }

    private void WebBrowserControl_CanGoBackChanged(object sender, EventArgs e)
    {
      this.OnNotifyPropertyChanged("CanGoBack");
    }

    private void WebBrowserControl_CanGoForwardChanged(object sender, EventArgs e)
    {
      this.OnNotifyPropertyChanged("CanGoForward");
    }

    private void WebBrowserControl_ContextMenuChanged(object sender, EventArgs e)
    {
      this.OnNotifyPropertyChanged("ContextMenu");
    }

    private void WebBrowserControl_ContextMenuStripChanged(object sender, EventArgs e)
    {
      this.OnNotifyPropertyChanged("ContextMenuStrip");
    }

    private void WebBrowserControl_DockChanged(object sender, EventArgs e)
    {
      this.OnNotifyPropertyChanged("Dock");
    }

    private void WebBrowserControl_DocumentTitleChanged(object sender, EventArgs e)
    {
      this.OnNotifyPropertyChanged("DocumentTitle");
    }

    private void WebBrowserControl_EncryptionLevelChanged(object sender, EventArgs e)
    {
      this.OnNotifyPropertyChanged("EncryptionLevel");
    }

    private void WebBrowserControl_StatusTextChanged(object sender, EventArgs e)
    {
      this.OnNotifyPropertyChanged("StatusText");
    }

    private void WebBrowserControl_DocumentCompleted(
      object sender,
      WebBrowserDocumentCompletedEventArgs e)
    {
      this.OnDocumentCompleted(e);
    }

    private void WebBrowserControl_FileDownload(object sender, EventArgs e)
    {
      this.OnFileDownload(e);
    }

    private void WebBrowserControl_Navigating(object sender, WebBrowserNavigatingEventArgs e)
    {
      this.OnNavigating(e);
    }

    private void WebBrowserControl_Navigated(object sender, WebBrowserNavigatedEventArgs e)
    {
      this.OnNavigated(e);
    }

    private void WebBrowserControl_NewWindow(object sender, CancelEventArgs e)
    {
      this.OnNewWindow(e);
    }

    private void WebBrowserControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
      this.OnPreviewKeyDown(e);
    }

    private void WebBrowserControl_ProgressChanged(
      object sender,
      WebBrowserProgressChangedEventArgs e)
    {
      this.OnProgressChanged(e);
    }

    private void WebBrowserControl_SystemColorsChanged(object sender, EventArgs e)
    {
      this.OnSystemColorsChanged(e);
    }

    private void WebBrowserControl_Validated(object sender, EventArgs e)
    {
      this.OnValidated(e);
    }

    private void WebBrowserControl_Validating(object sender, CancelEventArgs e)
    {
      this.OnValidating(e);
    }

    public event WebBrowserDocumentCompletedEventHandler DocumentCompleted
    {
      add
      {
        this.Events.AddHandler(RadWebBrowserItem.DocumentCompletedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadWebBrowserItem.DocumentCompletedEventKey, (Delegate) value);
      }
    }

    public event EventHandler FileDownload
    {
      add
      {
        this.Events.AddHandler(RadWebBrowserItem.FileDownloadEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadWebBrowserItem.FileDownloadEventKey, (Delegate) value);
      }
    }

    public event WebBrowserNavigatedEventHandler Navigated
    {
      add
      {
        this.Events.AddHandler(RadWebBrowserItem.NavigatedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadWebBrowserItem.NavigatedEventKey, (Delegate) value);
      }
    }

    public event WebBrowserNavigatingEventHandler Navigating
    {
      add
      {
        this.Events.AddHandler(RadWebBrowserItem.NavigatingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadWebBrowserItem.NavigatingEventKey, (Delegate) value);
      }
    }

    public event CancelEventHandler NewWindow
    {
      add
      {
        this.Events.AddHandler(RadWebBrowserItem.NewWindowEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadWebBrowserItem.NewWindowEventKey, (Delegate) value);
      }
    }

    public event PreviewKeyDownEventHandler PreviewKeyDown
    {
      add
      {
        this.Events.AddHandler(RadWebBrowserItem.PreviewKeyDownEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadWebBrowserItem.PreviewKeyDownEventKey, (Delegate) value);
      }
    }

    public event WebBrowserProgressChangedEventHandler ProgressChanged
    {
      add
      {
        this.Events.AddHandler(RadWebBrowserItem.ProgressChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadWebBrowserItem.ProgressChangedEventKey, (Delegate) value);
      }
    }

    public event EventHandler SystemColorsChanged
    {
      add
      {
        this.Events.AddHandler(RadWebBrowserItem.SystemColorsChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadWebBrowserItem.SystemColorsChangedEventKey, (Delegate) value);
      }
    }

    protected virtual void OnDocumentCompleted(WebBrowserDocumentCompletedEventArgs e)
    {
      WebBrowserDocumentCompletedEventHandler completedEventHandler = (WebBrowserDocumentCompletedEventHandler) this.Events[RadWebBrowserItem.DocumentCompletedEventKey];
      if (completedEventHandler == null)
        return;
      completedEventHandler((object) this, e);
    }

    protected virtual void OnFileDownload(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadWebBrowserItem.FileDownloadEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected virtual void OnNavigated(WebBrowserNavigatedEventArgs e)
    {
      WebBrowserNavigatedEventHandler navigatedEventHandler = (WebBrowserNavigatedEventHandler) this.Events[RadWebBrowserItem.NavigatedEventKey];
      if (navigatedEventHandler == null)
        return;
      navigatedEventHandler((object) this, e);
    }

    protected virtual void OnNavigating(WebBrowserNavigatingEventArgs e)
    {
      WebBrowserNavigatingEventHandler navigatingEventHandler = (WebBrowserNavigatingEventHandler) this.Events[RadWebBrowserItem.NavigatingEventKey];
      if (navigatingEventHandler == null)
        return;
      navigatingEventHandler((object) this, e);
    }

    protected virtual void OnNewWindow(CancelEventArgs e)
    {
      CancelEventHandler cancelEventHandler = (CancelEventHandler) this.Events[RadWebBrowserItem.NewWindowEventKey];
      if (cancelEventHandler == null)
        return;
      cancelEventHandler((object) this, e);
    }

    protected virtual void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
    {
      PreviewKeyDownEventHandler downEventHandler = (PreviewKeyDownEventHandler) this.Events[RadWebBrowserItem.PreviewKeyDownEventKey];
      if (downEventHandler == null)
        return;
      downEventHandler((object) this, e);
    }

    protected virtual void OnProgressChanged(WebBrowserProgressChangedEventArgs e)
    {
      WebBrowserProgressChangedEventHandler changedEventHandler = (WebBrowserProgressChangedEventHandler) this.Events[RadWebBrowserItem.ProgressChangedEventKey];
      if (changedEventHandler == null)
        return;
      changedEventHandler((object) this, e);
    }

    protected virtual void OnSystemColorsChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadWebBrowserItem.SystemColorsChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }
  }
}
