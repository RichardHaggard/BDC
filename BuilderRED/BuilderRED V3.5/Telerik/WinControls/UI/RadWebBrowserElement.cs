// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadWebBrowserElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class RadWebBrowserElement : RadItem
  {
    private RadWebBrowserItem webBrowserItem;
    private FillPrimitive fillPrimitive;
    private BorderPrimitive borderPrimitive;

    protected override void CreateChildElements()
    {
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.Class = "TextBoxFill";
      this.fillPrimitive.BackColor = Color.White;
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "TextBoxBorder";
      this.webBrowserItem = new RadWebBrowserItem();
      this.Children.AddRange((RadElement) this.webBrowserItem, (RadElement) this.fillPrimitive, (RadElement) this.borderPrimitive);
      base.CreateChildElements();
    }

    public Uri Url
    {
      get
      {
        return this.WebBrowserItem.Url;
      }
    }

    public string DocumentText
    {
      get
      {
        return this.WebBrowserItem.DocumentText;
      }
      set
      {
        this.WebBrowserItem.DocumentText = value;
      }
    }

    public string DocumentTitle
    {
      get
      {
        return this.WebBrowserItem.DocumentTitle;
      }
    }

    public RadWebBrowserItem WebBrowserItem
    {
      get
      {
        return this.webBrowserItem;
      }
    }

    public FillPrimitive FillPrimitive
    {
      get
      {
        return this.fillPrimitive;
      }
    }

    public BorderPrimitive BorderPrimitive
    {
      get
      {
        return this.borderPrimitive;
      }
    }

    public bool ShowBorder
    {
      get
      {
        return this.BorderPrimitive.Visibility == ElementVisibility.Visible;
      }
      set
      {
        if (this.BorderPrimitive.Visibility == ElementVisibility.Visible == value)
          return;
        this.BorderPrimitive.Visibility = value ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
    }
  }
}
