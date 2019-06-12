// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadImageEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [DefaultProperty("CurrentBitmap")]
  [ToolboxItem(true)]
  [Docking(DockingBehavior.Ask)]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [TelerikToolboxCategory("Editors")]
  [Description("A control that can be used to perform image editing operations.")]
  public class RadImageEditor : RadControl
  {
    private RadImageEditorElement imageEditorElement;

    public RadImageEditor()
    {
      this.AllowDrop = true;
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.imageEditorElement = this.CreateImageEditorElement();
      parent.Children.Add((RadElement) this.imageEditorElement);
    }

    protected virtual RadImageEditorElement CreateImageEditorElement()
    {
      return new RadImageEditorElement();
    }

    public RadImageEditorElement ImageEditorElement
    {
      get
      {
        return this.imageEditorElement;
      }
    }

    [DefaultValue(null)]
    public Bitmap CurrentBitmap
    {
      get
      {
        return this.ImageEditorElement.CurrentBitmap;
      }
      set
      {
        this.ImageEditorElement.CurrentBitmap = value;
      }
    }

    [DefaultValue(null)]
    public string BitmapPath
    {
      get
      {
        return this.ImageEditorElement.BitmapPath;
      }
      set
      {
        this.ImageEditorElement.BitmapPath = value;
      }
    }

    [DefaultValue(typeof (SizeF), "1, 1")]
    public SizeF ZoomFactor
    {
      get
      {
        return this.ImageEditorElement.ZoomFactor;
      }
      set
      {
        this.ImageEditorElement.ZoomFactor = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return new Size(400, 300);
      }
    }

    public virtual void LoadAsync()
    {
      this.ImageEditorElement.LoadAsync();
    }

    public virtual void LoadAsync(string url)
    {
      this.ImageEditorElement.LoadAsync(url);
    }

    public virtual void CancelAsync()
    {
      this.ImageEditorElement.CancelAsync();
    }

    public virtual void OpenImage(Bitmap bitmap)
    {
      this.ImageEditorElement.OpenImage(bitmap);
    }

    public virtual void OpenImage(string fileName)
    {
      this.ImageEditorElement.OpenImage(fileName);
    }

    public virtual void OpenImage()
    {
      this.ImageEditorElement.OpenImage();
    }

    public virtual void SaveImage()
    {
      this.ImageEditorElement.SaveImage();
    }

    public virtual void SaveImage(string path)
    {
      this.ImageEditorElement.SaveImage(path);
    }

    public virtual void SaveImage(string path, ImageFormat imageFormat)
    {
      this.ImageEditorElement.SaveImage(path, imageFormat);
    }

    public virtual void SaveImage(Stream stream)
    {
      this.ImageEditorElement.SaveImage(stream);
    }

    public virtual void SaveImage(Stream stream, ImageFormat imageFormat)
    {
      this.ImageEditorElement.SaveImage(stream, imageFormat);
    }

    public virtual void SaveImageAs()
    {
      this.ImageEditorElement.SaveImageAs();
    }

    public event AsyncCompletedEventHandler ImageLoaded
    {
      add
      {
        this.ImageEditorElement.ImageLoaded += value;
      }
      remove
      {
        this.ImageEditorElement.ImageLoaded -= value;
      }
    }

    public event ProgressChangedEventHandler ImageLoadAsyncProgressChanged
    {
      add
      {
        this.ImageEditorElement.ImageLoadAsyncProgressChanged += value;
      }
      remove
      {
        this.ImageEditorElement.ImageLoadAsyncProgressChanged -= value;
      }
    }

    public event ErrorEventHandler OperationError
    {
      add
      {
        this.ImageEditorElement.OperationError += value;
      }
      remove
      {
        this.ImageEditorElement.OperationError -= value;
      }
    }

    protected override void OnThemeChanged()
    {
      base.OnThemeChanged();
      this.ImageEditorElement.CommandsElement.SetCommandIcons();
    }

    protected override void OnDragDrop(DragEventArgs e)
    {
      base.OnDragDrop(e);
      string[] data = (string[]) e.Data.GetData(DataFormats.FileDrop);
      if (data.Length <= 0)
        return;
      this.OpenImage(data[0]);
    }

    protected override void OnDragEnter(DragEventArgs e)
    {
      base.OnDragEnter(e);
      if (!e.Data.GetDataPresent(DataFormats.FileDrop))
        return;
      e.Effect = DragDropEffects.Copy;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.ImageEditorElement.ProcessMouseDown(e))
        return;
      base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.ImageEditorElement.ProcessMouseMove(e))
        return;
      base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.ImageEditorElement.ProcessMouseUp(e))
        return;
      base.OnMouseUp(e);
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
      if (this.ImageEditorElement.ProcessMouseClick(e))
        return;
      base.OnMouseClick(e);
    }

    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
      if (this.ImageEditorElement.ProcessDoubleClick(e))
        return;
      base.OnMouseDoubleClick(e);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      if (this.ImageEditorElement.ProcessMouseEnter(e))
        return;
      base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      if (this.ImageEditorElement.ProcessMouseLeave(e))
        return;
      base.OnMouseLeave(e);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      if (this.ImageEditorElement.ProcessMouseWheel(e))
        return;
      base.OnMouseWheel(e);
    }

    protected override void OnMouseHover(EventArgs e)
    {
      if (this.ImageEditorElement.ProcessMouseHover(e))
        return;
      base.OnMouseHover(e);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (this.ImageEditorElement.ProcessKeyDown(e))
        return;
      base.OnKeyDown(e);
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      if (this.ImageEditorElement.ProcessKeyPress(e))
        return;
      base.OnKeyPress(e);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      if (this.ImageEditorElement.ProcessKeyUp(e))
        return;
      base.OnKeyUp(e);
    }

    public virtual void ShowResizeDialog()
    {
      this.ImageEditorElement.ShowResizeDialog();
    }

    public virtual void ShowCanvasResizeDialog()
    {
      this.ImageEditorElement.ShowCanvasResizeDialog();
    }

    public virtual void ShowRoundCornersDialog()
    {
      this.ImageEditorElement.ShowRoundCornersDialog();
    }

    public virtual void ShowDrawTextDialog()
    {
      this.ImageEditorElement.ShowDrawTextDialog();
    }

    public virtual void ShowSaturationDialog()
    {
      this.ImageEditorElement.ShowSaturationDialog();
    }

    public virtual void ShowContrastDialog()
    {
      this.ImageEditorElement.ShowContrastDialog();
    }

    public virtual void ShowSharpenDialog()
    {
      this.ImageEditorElement.ShowSharpenDialog();
    }

    public virtual void ShowBlurDialog()
    {
      this.ImageEditorElement.ShowBlurDialog();
    }

    public virtual void ShowHueDialog()
    {
      this.ImageEditorElement.ShowHueDialog();
    }

    public virtual void ShowDrawShapeDialog()
    {
      this.ImageEditorElement.ShowDrawShapeDialog();
    }

    public virtual void ShowDrawDialog()
    {
      this.ImageEditorElement.ShowDrawDialog();
    }
  }
}
