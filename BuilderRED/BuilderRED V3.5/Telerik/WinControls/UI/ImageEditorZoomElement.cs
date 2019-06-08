// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImageEditorZoomElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class ImageEditorZoomElement : LightVisualElement
  {
    public static RadProperty DropDownWidthProperty = RadProperty.Register(nameof (DropDownWidth), typeof (int), typeof (ImageEditorZoomElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 75));
    public static RadProperty TrackBarWidthProperty = RadProperty.Register(nameof (TrackBarWidth), typeof (int), typeof (ImageEditorZoomElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 150));
    private RadTrackBarElement trackBar;
    private RadDropDownListElement dropDownList;
    private RadImageEditorElement imageEditorElement;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Margin = new Padding(5, 0, 5, 0);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.trackBar = this.CreateTrackBar();
      this.dropDownList = this.CreateDropDownList();
      this.Children.Add((RadElement) this.trackBar);
      this.Children.Add((RadElement) this.dropDownList);
    }

    protected virtual RadTrackBarElement CreateTrackBar()
    {
      RadTrackBarElement radTrackBarElement = new RadTrackBarElement();
      radTrackBarElement.Margin = new Padding(5, 0, 5, 0);
      radTrackBarElement.TickStyle = TickStyles.Both;
      radTrackBarElement.TickFormatting += (TickFormattingEventHandler) ((sender, e) =>
      {
        e.TickElement.Line1.GradientStyle = GradientStyles.Solid;
        e.TickElement.Line2.GradientStyle = GradientStyles.Solid;
        e.TickElement.Line1.BackColor = Color.Transparent;
        e.TickElement.Line2.BackColor = Color.Transparent;
      });
      radTrackBarElement.Minimum = 1f;
      radTrackBarElement.Maximum = 50f;
      radTrackBarElement.Value = 10f;
      return radTrackBarElement;
    }

    protected virtual RadDropDownListElement CreateDropDownList()
    {
      return new RadDropDownListElement() { Items = { new RadListDataItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("ZoomFactorAuto")), new RadListDataItem("10 %", (object) 10f), new RadListDataItem("25 %", (object) 25f), new RadListDataItem("50 %", (object) 50f), new RadListDataItem("100 %", (object) 100f), new RadListDataItem("150 %", (object) 150f), new RadListDataItem("200 %", (object) 200f), new RadListDataItem("500 %", (object) 500f) }, SelectedValue = (object) 100f };
    }

    public ImageEditorZoomElement(RadImageEditorElement imageEditorElement)
    {
      this.imageEditorElement = imageEditorElement;
      this.WireEvents();
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.TrackBar.BackColor = Color.Transparent;
      this.TrackBar.GradientStyle = GradientStyles.Solid;
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.UnwireEvents();
    }

    public RadTrackBarElement TrackBar
    {
      get
      {
        return this.trackBar;
      }
    }

    public RadDropDownListElement DropDownList
    {
      get
      {
        return this.dropDownList;
      }
    }

    public int TrackBarWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(ImageEditorZoomElement.TrackBarWidthProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(ImageEditorZoomElement.TrackBarWidthProperty, (object) value);
      }
    }

    public int DropDownWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(ImageEditorZoomElement.DropDownWidthProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(ImageEditorZoomElement.DropDownWidthProperty, (object) value);
      }
    }

    protected virtual void WireEvents()
    {
      this.dropDownList.TextChanged += new EventHandler(this.DropDownList_TextChanged);
      this.trackBar.ValueChanged += new EventHandler(this.TrackBar_ValueChanged);
      this.trackBar.Scroll += new ScrollEventHandler(this.TrackBar_Scroll);
    }

    protected virtual void UnwireEvents()
    {
      this.dropDownList.TextChanged -= new EventHandler(this.DropDownList_TextChanged);
      this.trackBar.ValueChanged -= new EventHandler(this.TrackBar_ValueChanged);
      this.trackBar.Scroll -= new ScrollEventHandler(this.TrackBar_Scroll);
    }

    private void TrackBar_ValueChanged(object sender, EventArgs e)
    {
      this.SetZoomFactor();
    }

    private void TrackBar_Scroll(object sender, ScrollEventArgs e)
    {
      this.SetZoomFactor();
    }

    protected virtual void SetZoomFactor()
    {
      this.UnwireEvents();
      float num = this.trackBar.Value / 10f;
      this.imageEditorElement.ZoomFactor = new SizeF(num, num);
      this.dropDownList.Text = string.Format("{0} %", (object) (float) ((double) this.trackBar.Value * 10.0));
      this.WireEvents();
    }

    private void DropDownList_TextChanged(object sender, EventArgs e)
    {
      if (this.imageEditorElement.CurrentBitmap == null)
        return;
      this.UnwireEvents();
      float result = 0.0f;
      if (this.dropDownList.SelectedIndex == 0)
      {
        result = Math.Min((float) (this.imageEditorElement.Size.Width - this.imageEditorElement.CommandsElementWidth) / (float) this.imageEditorElement.CurrentBitmap.Width, (float) (this.imageEditorElement.Size.Height - this.imageEditorElement.ZoomElementHeight) / (float) this.imageEditorElement.CurrentBitmap.Height);
        this.imageEditorElement.ZoomFactor = new SizeF(result, result);
      }
      else if (float.TryParse(this.dropDownList.Text.Replace('%', ' ').Trim(), out result))
      {
        result /= 100f;
        if ((double) result * 10.0 >= (double) this.trackBar.Minimum && (double) result * 10.0 <= (double) this.trackBar.Maximum)
          this.imageEditorElement.ZoomFactor = new SizeF(result, result);
      }
      if ((double) result * 10.0 >= (double) this.trackBar.Minimum && (double) result * 10.0 <= (double) this.trackBar.Maximum)
        this.trackBar.Value = result * 10f;
      this.WireEvents();
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      RectangleF finalRect1 = new RectangleF(clientRectangle.Right - (float) this.DropDownWidth, (float) (((double) clientRectangle.Height - (double) this.DropDownList.DesiredSize.Height) / 2.0), (float) this.DropDownWidth, this.dropDownList.DesiredSize.Height);
      RectangleF finalRect2 = new RectangleF(clientRectangle.Right - (float) this.TrackBarWidth - (float) this.DropDownWidth, (float) (((double) clientRectangle.Height - (double) this.TrackBar.DesiredSize.Height) / 2.0), (float) this.TrackBarWidth, this.trackBar.DesiredSize.Height);
      this.dropDownList.Arrange(finalRect1);
      this.trackBar.Arrange(finalRect2);
      return finalSize;
    }
  }
}
