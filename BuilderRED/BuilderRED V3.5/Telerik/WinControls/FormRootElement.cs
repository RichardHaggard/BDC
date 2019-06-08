// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.FormRootElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Telerik.WinControls
{
  public class FormRootElement : RootRadElement, IFormRootElement
  {
    private RadFormControlBase formControl;

    public FormRootElement(RadFormControlBase control)
    {
      this.formControl = control;
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      int num = (int) this.SetDefaultValueOverride(RootRadElement.ApplyShapeToControlProperty, (object) true);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == RadElement.BoundsProperty)
      {
        if (this.Shape == null || this.formControl == null || (!this.ApplyShapeToControl || !this.formControl.AllowTheming))
          return;
        Rectangle oldValue = (Rectangle) e.OldValue;
        Rectangle newValue = (Rectangle) e.NewValue;
        if (!(oldValue.Size != newValue.Size))
          return;
        this.CreateRegionFromShape(newValue.Size);
      }
      else if (e.Property == RadElement.ShapeProperty && this.ApplyShapeToControl)
      {
        if (!(e.NewValue is ElementShape) || this.ElementTree == null)
          return;
        this.CreateRegionFromShape(this.Size);
      }
      else if (e.Property == RootRadElement.ApplyShapeToControlProperty)
      {
        if ((bool) e.NewValue && this.Shape != null && this.formControl.AllowTheming)
          this.CreateRegionFromShape(this.Size);
        else
          this.ElementTree.Control.Region = (Region) null;
      }
      else
        base.OnPropertyChanged(e);
    }

    private void CreateRegionFromShape(Size regionSize)
    {
      Region regionY = (Region) null;
      if (this.formControl.WindowState != FormWindowState.Maximized || this.formControl.MaximumSize != Size.Empty)
      {
        using (GraphicsPath path = this.Shape.CreatePath(new Rectangle(Point.Empty, regionSize)))
          regionY = new Region(path);
      }
      else if (!TelerikHelper.IsWindows7OrLower || DWMAPI.IsCompositionEnabled)
      {
        int num1 = DWMAPI.IsCompositionEnabled ? SystemInformation.FixedFrameBorderSize.Height : SystemInformation.FrameBorderSize.Height;
        int num2 = num1 + num1;
        int num3 = TelerikHelper.IsWindows8OrHigher ? num2 + 1 : num2 + 2;
        Rectangle rect = new Rectangle(new Point(num3, num3), new Size(regionSize.Width - num3 * 2, regionSize.Height - num3 * 2));
        if (!this.formControl.IsMdiChild && !TelerikHelper.IsWindows7OrLower)
          rect = new Rectangle(new Point(num3 + 1, num3), new Size(regionSize.Width - num3 * 2 - 2, regionSize.Height - num3 * 2));
        using (GraphicsPath path = new GraphicsPath())
        {
          path.AddRectangle(rect);
          regionY = new Region(path);
        }
      }
      if (FormRootElement.AreEqualRegions(this.formControl.Region, regionY))
        return;
      this.formControl.Region = regionY;
    }

    private static bool AreEqualRegions(Region regionX, Region regionY)
    {
      if (regionX == null && regionY == null)
        return true;
      if (regionX == null || regionY == null)
        return false;
      byte[] data1 = regionX.GetRegionData().Data;
      byte[] data2 = regionY.GetRegionData().Data;
      int length = data1.Length;
      if (length != data2.Length)
        return false;
      for (int index = 0; index < length; ++index)
      {
        if ((int) data1[index] != (int) data2[index])
          return false;
      }
      return true;
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RootRadElement);
      }
    }
  }
}
