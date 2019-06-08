// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadTimePickerPopup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Telerik.WinControls
{
  public class RadTimePickerPopup : RadEditorPopupControlBase
  {
    private RadTimePickerContentElement contentElement;

    public RadTimePickerPopup(RadTimePickerElement owner)
      : base((RadItem) owner)
    {
      this.TabStop = false;
      this.contentElement = new RadTimePickerContentElement((IPickerContentElementOwner) owner);
      this.SizingGripDockLayout.Children.Add((RadElement) this.contentElement);
      this.SizingGrip.ShouldAspectRootElement = false;
      this.SizingGrip.ZIndex = 100;
    }

    public RadTimePickerContentElement ContentElement
    {
      get
      {
        return this.contentElement;
      }
    }

    protected override void SetBoundsCore(
      int x,
      int y,
      int width,
      int height,
      BoundsSpecified specified)
    {
      base.SetBoundsCore(x, y, width, height, specified);
    }

    protected override void ShowPopupCore(Size size, Point location)
    {
      SizeF sizeF1 = this.OwnerElement != null ? this.OwnerElement.DpiScaleFactor : this.dpiScaleFactor;
      bool isEmpty = this.LastShowDpiScaleFactor.IsEmpty;
      if (this.LastShowDpiScaleFactor != sizeF1)
      {
        if (isEmpty)
          this.LastShowDpiScaleFactor = new SizeF(1f, 1f);
        SizeF sizeF2 = new SizeF(this.OwnerElement.DpiScaleFactor.Width / this.LastShowDpiScaleFactor.Width, this.OwnerElement.DpiScaleFactor.Height / this.LastShowDpiScaleFactor.Height);
        this.LastShowDpiScaleFactor = sizeF1;
        this.Scale(sizeF2);
        size = TelerikDpiHelper.ScaleSize(size, sizeF2);
      }
      this.SetBoundsCore(location.X, location.Y, size.Width, size.Height, BoundsSpecified.All);
      NativeMethods.ShowWindow(this.Handle, 4);
      if (DWMAPI.IsCompositionEnabled && this.EnableAeroEffects)
        this.UpdateAeroEffectState();
      ControlHelper.BringToFront(this.Handle, false);
    }
  }
}
