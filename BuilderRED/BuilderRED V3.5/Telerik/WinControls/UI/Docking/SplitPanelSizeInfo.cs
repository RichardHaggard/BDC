// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Docking.SplitPanelSizeInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace Telerik.WinControls.UI.Docking
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  [Serializable]
  public class SplitPanelSizeInfo : RadDockObject
  {
    public static readonly Size DefaultAbsoluteSize = new Size(200, 200);
    public static readonly Size DefaultMinimumSize = new Size(0, 0);
    private SizeF dpiScale = new SizeF(1f, 1f);
    private SplitPanelSizeMode sizeMode;
    private Size absoluteSize;
    private Size minimumSize;
    private Size maximumSize;
    private Size splitterCorrection;
    private SizeF relativeRatio;
    private SizeF autoSizeScale;
    [NonSerialized]
    private int measuredLength;
    [NonSerialized]
    internal int minLength;

    public SplitPanelSizeInfo()
    {
      this.sizeMode = SplitPanelSizeMode.Auto;
      this.absoluteSize = SplitPanelSizeInfo.DefaultAbsoluteSize;
      this.minimumSize = SplitPanelSizeInfo.DefaultMinimumSize;
    }

    public SplitPanelSizeInfo(SplitPanelSizeInfo source)
    {
      this.Copy(source);
    }

    public void Copy(SplitPanelSizeInfo source)
    {
      this.relativeRatio = source.relativeRatio;
      this.absoluteSize = source.absoluteSize;
      this.splitterCorrection = source.splitterCorrection;
      this.autoSizeScale = source.autoSizeScale;
      this.minimumSize = source.minimumSize;
      if (source.sizeMode == SplitPanelSizeMode.Fill || this.sizeMode == SplitPanelSizeMode.Fill)
        return;
      this.sizeMode = source.sizeMode;
    }

    public void Reset()
    {
      this.relativeRatio = SizeF.Empty;
      this.absoluteSize = SplitPanelSizeInfo.DefaultAbsoluteSize;
      this.splitterCorrection = Size.Empty;
      this.autoSizeScale = SizeF.Empty;
      this.minimumSize = SplitPanelSizeInfo.DefaultMinimumSize;
    }

    public SplitPanelSizeInfo Clone()
    {
      return new SplitPanelSizeInfo(this);
    }

    protected override bool ShouldSerializeProperty(string propName)
    {
      switch (propName)
      {
        case "MaximumSize":
          return this.maximumSize != Size.Empty;
        case "MinimumSize":
          return this.minimumSize != SplitPanelSizeInfo.DefaultMinimumSize;
        case "AbsoluteSize":
          return this.absoluteSize != SplitPanelSizeInfo.DefaultAbsoluteSize;
        case "RelativeRatio":
          return this.relativeRatio != SizeF.Empty;
        case "AutoSizeScale":
          return this.autoSizeScale != SizeF.Empty;
        case "SplitterCorrection":
          return this.splitterCorrection != Size.Empty;
        default:
          return base.ShouldSerializeProperty(propName);
      }
    }

    [Description("Gets or sets the minimum size for the associated SplitPanel.")]
    public Size MinimumSize
    {
      get
      {
        return TelerikDpiHelper.ScaleSize(this.minimumSize, this.DpiScale);
      }
      set
      {
        if (this.minimumSize == value || !this.OnPropertyChanging(nameof (MinimumSize)))
          return;
        this.minimumSize = value;
        this.OnPropertyChanged(nameof (MinimumSize));
      }
    }

    private bool ShouldSerializeMinimumSize()
    {
      return this.ShouldSerializeProperty("MinimumSize");
    }

    [Description("Gets or sets the maximum size for the associated SplitPanel.")]
    public Size MaximumSize
    {
      get
      {
        return TelerikDpiHelper.ScaleSize(this.maximumSize, this.DpiScale);
      }
      set
      {
        if (this.maximumSize == value || !this.OnPropertyChanging(nameof (MaximumSize)))
          return;
        this.maximumSize = value;
        this.OnPropertyChanged(nameof (MaximumSize));
      }
    }

    private bool ShouldSerializeMaximumSize()
    {
      return this.ShouldSerializeProperty("MaximumSize");
    }

    [Description("Gets or sets the amount (in pixels) applied to the size of the panel by a splitter.")]
    public Size SplitterCorrection
    {
      get
      {
        return TelerikDpiHelper.ScaleSize(this.splitterCorrection, this.DpiScale);
      }
      set
      {
        if (this.splitterCorrection == value || !this.OnPropertyChanging(nameof (SplitterCorrection)))
          return;
        this.splitterCorrection = value;
        this.OnPropertyChanged(nameof (SplitterCorrection));
      }
    }

    private bool ShouldSerializeSplitterCorrection()
    {
      return this.ShouldSerializeProperty("SplitterCorrection");
    }

    [Description("Gets or sets the scale factor for relatively-sized panels.")]
    public SizeF RelativeRatio
    {
      get
      {
        return this.relativeRatio;
      }
      set
      {
        value = SplitPanelHelper.EnsureSizeBounds(value, SizeF.Empty, new SizeF(1f, 1f));
        if (this.relativeRatio == value || !this.OnPropertyChanging(nameof (RelativeRatio)))
          return;
        this.relativeRatio = value;
        this.OnPropertyChanged(nameof (RelativeRatio));
      }
    }

    private bool ShouldSerializeRelativeRatio()
    {
      return this.ShouldSerializeProperty("RelativeRatio");
    }

    [Description("Gets or sets the scale factor to be used by auto-sized panels. Usually this is internally updated by a splitter resize.")]
    [Browsable(false)]
    public SizeF AutoSizeScale
    {
      get
      {
        return this.autoSizeScale;
      }
      set
      {
        value = SplitPanelHelper.EnsureSizeBounds(value, new SizeF(-1f, -1f), new SizeF(1f, 1f));
        if (this.autoSizeScale == value || !this.OnPropertyChanging(nameof (AutoSizeScale)))
          return;
        this.autoSizeScale = value;
        this.OnPropertyChanged(nameof (AutoSizeScale));
      }
    }

    private bool ShouldSerializeAutoSizeScale()
    {
      return this.ShouldSerializeProperty("AutoSizeScale");
    }

    [Description("Gets or sets the size mode for the owning panel.")]
    [DefaultValue(SplitPanelSizeMode.Auto)]
    public SplitPanelSizeMode SizeMode
    {
      get
      {
        return this.sizeMode;
      }
      set
      {
        if (this.sizeMode == value || !this.OnPropertyChanging(nameof (SizeMode)))
          return;
        this.sizeMode = value;
        this.OnPropertyChanged(nameof (SizeMode));
      }
    }

    [Description("Gets or sets the size used when size mode is Absolute.")]
    public Size AbsoluteSize
    {
      get
      {
        return TelerikDpiHelper.ScaleSize(this.absoluteSize, this.DpiScale);
      }
      set
      {
        if (this.absoluteSize == value || !this.OnPropertyChanging(nameof (AbsoluteSize)))
          return;
        this.absoluteSize = value;
        this.OnPropertyChanged(nameof (AbsoluteSize));
      }
    }

    private bool ShouldSerializeAbsoluteSize()
    {
      return this.ShouldSerializeProperty("AbsoluteSize");
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [XmlIgnore]
    [Browsable(false)]
    public int MeasuredLength
    {
      get
      {
        return this.measuredLength;
      }
      set
      {
        this.measuredLength = value;
      }
    }

    [Browsable(false)]
    [XmlIgnore]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SizeF DpiScale
    {
      get
      {
        return this.dpiScale;
      }
      internal set
      {
        this.dpiScale = value;
      }
    }
  }
}
