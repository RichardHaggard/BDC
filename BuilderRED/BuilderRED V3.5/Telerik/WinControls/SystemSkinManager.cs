// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.SystemSkinManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Telerik.WinControls
{
  public class SystemSkinManager
  {
    public static readonly VisualStyleElement EmptyElement = VisualStyleElement.CreateElement("", 0, 0);
    public static readonly VisualStyleElement DefaultElement = VisualStyleElement.Button.PushButton.Default;
    private const string Explorer = "explorer";
    private VisualStyleElement currentElement;
    private IntPtr currentHTheme;
    private Dictionary<string, IntPtr> themeHandles;
    private Control vistaExplorerThemeOwner;
    private bool useSystemSkin;
    private VisualStyleRenderer renderer;
    private static SystemSkinManager instance;

    public SystemSkinManager()
    {
      this.themeHandles = new Dictionary<string, IntPtr>();
      Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
      SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(this.OnUserPreferenceChanged);
    }

    public System.Drawing.Color GetThemeColor(ColorProperty color)
    {
      if (this.currentElement == null || this.currentHTheme == IntPtr.Zero)
        return System.Drawing.Color.Empty;
      int pColor = 0;
      UXTheme.GetThemeColor(this.currentHTheme, this.currentElement.Part, this.currentElement.State, (int) color, ref pColor);
      return ColorTranslator.FromWin32(pColor);
    }

    public Size GetPartPreferredSize(Graphics g, Rectangle bounds, ThemeSizeType type)
    {
      if (this.currentElement == null || this.currentHTheme == IntPtr.Zero)
        return Size.Empty;
      RadHdcWrapper radHdcWrapper = new RadHdcWrapper(g, false);
      IntPtr hdc = radHdcWrapper.GetHdc();
      NativeMethods.SIZE psz = new NativeMethods.SIZE();
      UXTheme.GetThemePartSize(this.currentHTheme, hdc, this.currentElement.Part, this.currentElement.State, IntPtr.Zero, type, psz);
      radHdcWrapper.Dispose();
      return new Size(psz.cx, psz.cy);
    }

    public bool SetCurrentElement(VisualStyleElement element)
    {
      this.currentHTheme = IntPtr.Zero;
      this.currentElement = (VisualStyleElement) null;
      if (element == null)
        return false;
      IntPtr htheme = this.GetHTheme(element.ClassName);
      if (htheme == IntPtr.Zero || !UXTheme.IsThemePartDefined(htheme, element.Part, 0))
        return false;
      this.currentHTheme = htheme;
      this.currentElement = element;
      return true;
    }

    public void PaintCurrentElement(Graphics g, Rectangle bounds)
    {
      if (this.currentHTheme == IntPtr.Zero)
        return;
      RadHdcWrapper radHdcWrapper = new RadHdcWrapper(g, true);
      IntPtr hdc = radHdcWrapper.GetHdc();
      NativeMethods.RECT rect = new NativeMethods.RECT()
      {
        left = bounds.Left,
        top = bounds.Top
      };
      rect.bottom = rect.top + bounds.Height;
      rect.right = rect.left + bounds.Width;
      UXTheme.DrawThemeBackground(this.currentHTheme, hdc, this.currentElement.Part, this.currentElement.State, ref rect, IntPtr.Zero);
      radHdcWrapper.Dispose();
    }

    public void NotifyChange()
    {
      foreach (Control openForm in (ReadOnlyCollectionBase) Application.OpenForms)
        openForm.Invalidate(true);
    }

    private void CloseAllHandles()
    {
      try
      {
        foreach (KeyValuePair<string, IntPtr> themeHandle in this.themeHandles)
          UXTheme.CloseThemeData(themeHandle.Value);
        this.themeHandles.Clear();
        if (this.vistaExplorerThemeOwner == null)
          return;
        this.vistaExplorerThemeOwner.Dispose();
        this.vistaExplorerThemeOwner = (Control) null;
      }
      catch
      {
      }
    }

    private IntPtr GetHTheme(string windowClass)
    {
      IntPtr num;
      if (!this.themeHandles.TryGetValue(windowClass, out num))
      {
        if (this.vistaExplorerThemeOwner == null)
        {
          this.vistaExplorerThemeOwner = new Control();
          this.vistaExplorerThemeOwner.CreateControl();
          UXTheme.SetWindowTheme(this.vistaExplorerThemeOwner.Handle, "explorer", (string) null);
        }
        IntPtr hwnd = IntPtr.Zero;
        string lower = windowClass.ToLower();
        if (lower == VisualStyleElement.TreeView.Item.Normal.ClassName.ToLower() || lower == VisualStyleElement.ListView.Item.Normal.ClassName.ToLower())
          hwnd = this.vistaExplorerThemeOwner.Handle;
        num = UXTheme.OpenThemeData(hwnd, windowClass);
        this.themeHandles.Add(windowClass, num);
      }
      return num;
    }

    private void OnApplicationExit(object sender, EventArgs e)
    {
      this.CloseAllHandles();
    }

    private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
      if (e.Category != UserPreferenceCategory.VisualStyle)
        return;
      this.CloseAllHandles();
    }

    public VisualStyleElement CurrentElement
    {
      get
      {
        return this.currentElement;
      }
    }

    public static bool IsVistaOrLater
    {
      get
      {
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
          return Environment.OSVersion.Version.Major >= 6;
        return false;
      }
    }

    public bool UseSystemSkin
    {
      get
      {
        return this.useSystemSkin;
      }
      set
      {
        if (this.useSystemSkin == value)
          return;
        this.useSystemSkin = value;
        this.NotifyChange();
      }
    }

    public static SystemSkinManager Instance
    {
      get
      {
        if (SystemSkinManager.instance == null)
          SystemSkinManager.instance = new SystemSkinManager();
        return SystemSkinManager.instance;
      }
    }

    public VisualStyleRenderer Renderer
    {
      get
      {
        if (this.renderer == null)
          this.renderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal);
        return this.renderer;
      }
    }
  }
}
