// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadOffice2007ScreenTipElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadOffice2007ScreenTipElement : RadScreenTipElement
  {
    public static RadProperty CaptionVisibleProperty = RadProperty.Register(nameof (CaptionVisible), typeof (bool), typeof (RadLabelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsLayout));
    public static RadProperty FooterVisibleProperty = RadProperty.Register(nameof (FooterVisible), typeof (bool), typeof (RadLabelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsLayout));
    private StackLayoutPanel screenTipPanel;
    private RadLabelElement captionLabel;
    private RadLineItem footerLine;
    private RadLabelElement mainText;
    private RadLabelElement footerText;
    private FillPrimitive fillPrimitive;
    private BorderPrimitive borderPrimitive;

    protected override void CreateChildElements()
    {
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.Class = "ScreenTipFill";
      this.Children.Add((RadElement) this.fillPrimitive);
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "ScreenTipBorder";
      this.Children.Add((RadElement) this.borderPrimitive);
      this.screenTipPanel = new StackLayoutPanel();
      this.screenTipPanel.Orientation = Orientation.Vertical;
      this.captionLabel = new RadLabelElement();
      this.captionLabel.Text = "ScreenTipCaptionText";
      this.captionLabel.Class = "ScreenTipCaptionText";
      this.captionLabel.TextWrap = true;
      this.screenTipPanel.Children.Add((RadElement) this.captionLabel);
      this.mainText = new RadLabelElement();
      this.mainText.Text = "ScreenTip Text";
      this.mainText.Class = "ScreenTipText";
      this.mainText.TextWrap = true;
      this.screenTipPanel.Children.Add((RadElement) this.mainText);
      this.footerLine = new RadLineItem();
      this.footerLine.MaxSize = new Size(0, 4);
      this.footerLine.Alignment = ContentAlignment.BottomCenter;
      this.footerLine.Class = "ScreenTipFooterLine";
      this.screenTipPanel.Children.Add((RadElement) this.footerLine);
      this.footerText = new RadLabelElement();
      this.footerText.Text = "ScreenTip Footer";
      this.footerText.Class = "ScreenTipFooterText";
      this.footerText.TextWrap = true;
      this.screenTipPanel.Children.Add((RadElement) this.footerText);
      this.Children.Add((RadElement) this.screenTipPanel);
      this.footerLine.Visibility = ElementVisibility.Collapsed;
      this.footerText.Visibility = ElementVisibility.Collapsed;
      this.Items.Add((RadItem) this.captionLabel);
      this.Items.Add((RadItem) this.mainText);
      this.Items.Add((RadItem) this.footerLine);
      this.Items.Add((RadItem) this.footerText);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CaptionVisible
    {
      get
      {
        return (bool) this.GetValue(RadOffice2007ScreenTipElement.CaptionVisibleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadOffice2007ScreenTipElement.CaptionVisibleProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool FooterVisible
    {
      get
      {
        return (bool) this.GetValue(RadOffice2007ScreenTipElement.FooterVisibleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadOffice2007ScreenTipElement.FooterVisibleProperty, (object) value);
      }
    }

    [Description("Represents the element that displays the caption")]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadLabelElement CaptionLabel
    {
      get
      {
        return this.captionLabel;
      }
    }

    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Represents the element that displays the footer line")]
    public RadLineItem FooterLine
    {
      get
      {
        return this.footerLine;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    [Description("Represents the element that displays the Text")]
    public RadLabelElement MainTextLabel
    {
      get
      {
        return this.mainText;
      }
    }

    [Description("Represents the element that displays the Footer")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    public RadLabelElement FooterTextLabel
    {
      get
      {
        return this.footerText;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public FillPrimitive ScreenTipFill
    {
      get
      {
        return this.fillPrimitive;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public BorderPrimitive ScreenTipBorder
    {
      get
      {
        return this.borderPrimitive;
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == RadOffice2007ScreenTipElement.CaptionVisibleProperty)
        this.CaptionLabel.Visibility = (bool) e.NewValue ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      else if (e.Property == RadOffice2007ScreenTipElement.FooterVisibleProperty)
      {
        bool newValue = (bool) e.NewValue;
        this.FooterLine.Visibility = newValue ? ElementVisibility.Visible : ElementVisibility.Collapsed;
        this.footerText.Visibility = newValue ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
      base.OnPropertyChanged(e);
    }
  }
}
