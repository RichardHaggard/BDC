// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Docking.SplitContainerLayoutStrategy
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Telerik.WinControls.UI.Docking
{
  public class SplitContainerLayoutStrategy
  {
    private SplitContainerLayoutInfo layoutInfo;
    private System.Type rootContainerType;

    public SplitContainerLayoutStrategy()
    {
      this.layoutInfo = new SplitContainerLayoutInfo();
    }

    public virtual void PerformLayout(RadSplitContainer container)
    {
      this.layoutInfo.Reset();
      this.FindFillPanels(container);
      this.UpdateLayoutInfo(container);
      if (this.layoutInfo.layoutTargets.Count == 0)
        return;
      if (this.layoutInfo.layoutTargets.Count == 1)
      {
        this.layoutInfo.layoutTargets[0].Bounds = this.layoutInfo.contentRect;
      }
      else
      {
        this.Measure();
        this.Layout(container);
      }
    }

    public virtual void ApplySplitterCorrection(SplitPanel left, SplitPanel right, int dragLength)
    {
      this.ApplySplitterCorrection(left, dragLength, true);
      this.ApplySplitterCorrection(right, dragLength, false);
      foreach (SplitPanel layoutTarget in this.layoutInfo.layoutTargets)
      {
        if (layoutTarget != left && layoutTarget != right && layoutTarget != this.layoutInfo.fillPanel)
        {
          SplitPanelSizeInfo sizeInfo = layoutTarget.SizeInfo;
          if (sizeInfo.SizeMode == SplitPanelSizeMode.Auto)
          {
            if (this.layoutInfo.fillPanel != null)
            {
              Size absoluteSize = layoutTarget.SizeInfo.AbsoluteSize;
              switch (this.layoutInfo.orientation)
              {
                case Orientation.Horizontal:
                  absoluteSize.Height = layoutTarget.Height;
                  break;
                case Orientation.Vertical:
                  absoluteSize.Width = layoutTarget.Width;
                  break;
              }
              sizeInfo.AbsoluteSize = absoluteSize;
            }
            else
            {
              SizeF autoSizeScale = sizeInfo.AutoSizeScale;
              switch (this.layoutInfo.Orientation)
              {
                case Orientation.Horizontal:
                  float num1 = (float) layoutTarget.Height / (float) this.layoutInfo.autoSizeLength;
                  autoSizeScale.Height = num1 - this.layoutInfo.autoSizeCountFactor;
                  break;
                case Orientation.Vertical:
                  float num2 = (float) layoutTarget.Width / (float) this.layoutInfo.autoSizeLength;
                  autoSizeScale.Width = num2 - this.layoutInfo.autoSizeCountFactor;
                  break;
              }
              sizeInfo.AutoSizeScale = autoSizeScale;
            }
          }
        }
      }
    }

    protected virtual void UpdateLayoutInfo(RadSplitContainer container)
    {
      this.layoutInfo.contentRect = container.ContentRectangle;
      this.layoutInfo.orientation = container.Orientation;
      this.layoutInfo.availableLength = this.GetLength(this.layoutInfo.contentRect.Size);
      this.layoutInfo.autoSizeLength = this.layoutInfo.availableLength;
      this.layoutInfo.splitterLength = container.SplitterWidth;
      int count = container.SplitPanels.Count;
      for (int index = 0; index < count; ++index)
      {
        SplitPanel splitPanel = container.SplitPanels[index];
        if (!splitPanel.Collapsed)
        {
          this.layoutInfo.layoutTargets.Add(splitPanel);
          SplitPanelSizeInfo sizeInfo = splitPanel.SizeInfo;
          sizeInfo.minLength = this.GetLength(this.GetMinimumSize(splitPanel));
          this.layoutInfo.totalMinLength += sizeInfo.minLength;
          switch (sizeInfo.SizeMode)
          {
            case SplitPanelSizeMode.Auto:
            case SplitPanelSizeMode.Relative:
              this.layoutInfo.autoSizeTargets.Add(splitPanel);
              continue;
            case SplitPanelSizeMode.Absolute:
              int length = this.GetLength(sizeInfo.AbsoluteSize);
              if (length > 0)
              {
                this.layoutInfo.autoSizeLength -= length;
                this.layoutInfo.absoluteSizeTargets.Add(splitPanel);
                continue;
              }
              this.layoutInfo.autoSizeTargets.Add(splitPanel);
              continue;
            default:
              continue;
          }
        }
      }
      if (this.layoutInfo.layoutTargets.Count > 0)
        this.layoutInfo.totalSplitterLength = (this.layoutInfo.layoutTargets.Count - 1) * this.layoutInfo.splitterLength;
      if (this.layoutInfo.autoSizeTargets.Count <= 0)
        return;
      this.layoutInfo.autoSizeCountFactor = 1f / (float) this.layoutInfo.autoSizeTargets.Count;
      this.layoutInfo.autoSizeLength -= (this.layoutInfo.autoSizeTargets.Count - 1) * this.layoutInfo.splitterLength;
    }

    protected virtual void Measure()
    {
      if (this.layoutInfo.fillPanel != null)
        this.MeasureWithFillPanel();
      else
        this.MeasureDefault();
      this.ClampMeasuredLength();
    }

    protected virtual void Layout(RadSplitContainer container)
    {
      int layoutOffset = this.GetLayoutOffset();
      int availableLength = this.layoutInfo.availableLength;
      int count = this.layoutInfo.LayoutTargets.Count;
      for (int panelIndex = 0; panelIndex < count; ++panelIndex)
      {
        SplitPanel layoutTarget = this.layoutInfo.LayoutTargets[panelIndex];
        int num = layoutTarget.SizeInfo.MeasuredLength;
        if (panelIndex == count - 1)
          num = availableLength;
        Rectangle rectangle;
        Rectangle bounds;
        if (this.layoutInfo.Orientation == Orientation.Vertical)
        {
          rectangle = new Rectangle(layoutOffset, this.layoutInfo.contentRect.Top, num, this.layoutInfo.contentRect.Height);
          bounds = new Rectangle(rectangle.Left - this.layoutInfo.splitterLength, rectangle.Top, this.layoutInfo.splitterLength, rectangle.Height);
        }
        else
        {
          rectangle = new Rectangle(this.layoutInfo.contentRect.Left, layoutOffset, this.layoutInfo.contentRect.Width, num);
          bounds = new Rectangle(rectangle.Left, rectangle.Top - this.layoutInfo.splitterLength, rectangle.Width, this.layoutInfo.splitterLength);
        }
        layoutTarget.Bounds = rectangle;
        layoutOffset += num + this.layoutInfo.splitterLength;
        availableLength -= num + this.layoutInfo.splitterLength;
        if (panelIndex > 0)
          container.UpdateSplitter(this.layoutInfo, panelIndex, bounds);
      }
    }

    protected virtual int GetLength(Size size)
    {
      if (this.layoutInfo.orientation == Orientation.Horizontal)
        return size.Height;
      return size.Width;
    }

    protected virtual float GetLengthF(SizeF size)
    {
      if (this.layoutInfo.orientation == Orientation.Horizontal)
        return size.Height;
      return size.Width;
    }

    protected virtual int GetAvailableLength(List<SplitPanel> panels, int index, int remaining)
    {
      int count = panels.Count;
      int num = 0;
      for (int index1 = index + 1; index1 < count; ++index1)
      {
        SplitPanel panel = panels[index1];
        num += panel.SizeInfo.minLength;
      }
      return remaining - num;
    }

    protected internal virtual Size GetMinimumSize(SplitPanel panel)
    {
      Size minimumSize1 = panel.SizeInfo.MinimumSize;
      RadSplitContainer radSplitContainer = panel as RadSplitContainer;
      if (radSplitContainer == null)
        return minimumSize1;
      int count = radSplitContainer.SplitPanels.Count;
      if (count <= 1)
        return minimumSize1;
      int val1_1 = 0;
      int val1_2 = 0;
      for (int index = 0; index < count; ++index)
      {
        SplitPanel splitPanel = radSplitContainer.SplitPanels[index];
        if (!splitPanel.Collapsed)
        {
          Size minimumSize2 = this.GetMinimumSize(splitPanel);
          switch (radSplitContainer.Orientation)
          {
            case Orientation.Horizontal:
              val1_1 = Math.Max(val1_1, minimumSize2.Width);
              val1_2 += minimumSize2.Height;
              if (index < count - 1)
              {
                val1_2 += radSplitContainer.SplitterWidth;
                continue;
              }
              continue;
            case Orientation.Vertical:
              val1_1 += minimumSize2.Width;
              if (index < count - 1)
                val1_1 += radSplitContainer.SplitterWidth;
              val1_2 = Math.Max(val1_2, minimumSize2.Height);
              continue;
            default:
              continue;
          }
        }
      }
      return new Size(Math.Max(val1_1, minimumSize1.Width), Math.Max(val1_2, minimumSize1.Height));
    }

    private void MeasureWithFillPanel()
    {
      int availableLength = this.layoutInfo.availableLength;
      int num1 = 0;
      int count = this.layoutInfo.layoutTargets.Count;
      for (int index = 0; index < count; ++index)
      {
        SplitPanel layoutTarget = this.layoutInfo.layoutTargets[index];
        if (layoutTarget != this.layoutInfo.fillPanel)
          num1 += this.GetLength(layoutTarget.SizeInfo.AbsoluteSize);
      }
      SplitPanelSizeInfo sizeInfo = this.layoutInfo.fillPanel.SizeInfo;
      int num2 = this.layoutInfo.availableLength - this.layoutInfo.totalSplitterLength;
      int num3 = 0;
      int minLength = sizeInfo.minLength;
      if (num1 + minLength > num2)
        num3 = num1 + minLength - num2;
      int num4 = num3;
      for (int index = 0; index < this.layoutInfo.layoutTargets.Count; ++index)
      {
        SplitPanel layoutTarget = this.layoutInfo.layoutTargets[index];
        if (layoutTarget != this.layoutInfo.fillPanel)
        {
          int length = this.GetLength(TelerikDpiHelper.ScaleSize(layoutTarget.SizeInfo.AbsoluteSize, new SizeF(1f / layoutTarget.SplitPanelElement.DpiScaleFactor.Width, 1f / layoutTarget.SplitPanelElement.DpiScaleFactor.Height)));
          if (num4 > 0 && layoutTarget.SizeInfo.SizeMode != SplitPanelSizeMode.Absolute)
          {
            int num5 = Math.Max(1, (int) ((double) ((float) length / (float) num1) * (double) num3));
            num4 -= num5;
            length -= num5;
          }
          layoutTarget.SizeInfo.MeasuredLength = length;
          availableLength -= layoutTarget.SizeInfo.MeasuredLength + this.layoutInfo.splitterLength;
        }
      }
      this.layoutInfo.fillPanel.SizeInfo.MeasuredLength = availableLength;
    }

    private void MeasureDefault()
    {
      int autoSizeLength = this.layoutInfo.autoSizeLength;
      for (int index = 0; index < this.layoutInfo.absoluteSizeTargets.Count; ++index)
      {
        SplitPanel absoluteSizeTarget = this.layoutInfo.absoluteSizeTargets[index];
        switch (this.layoutInfo.orientation)
        {
          case Orientation.Horizontal:
            absoluteSizeTarget.SizeInfo.MeasuredLength = absoluteSizeTarget.SizeInfo.AbsoluteSize.Height;
            break;
          case Orientation.Vertical:
            absoluteSizeTarget.SizeInfo.MeasuredLength = absoluteSizeTarget.SizeInfo.AbsoluteSize.Width;
            break;
        }
        autoSizeLength -= this.layoutInfo.splitterLength;
      }
      List<SplitPanel> splitPanelList = new List<SplitPanel>((IEnumerable<SplitPanel>) this.layoutInfo.autoSizeTargets);
      for (int index = 0; index < splitPanelList.Count; ++index)
      {
        SplitPanel splitPanel = splitPanelList[index];
        if (splitPanel.SizeInfo.SizeMode == SplitPanelSizeMode.Relative)
        {
          float lengthF = this.GetLengthF(splitPanel.SizeInfo.RelativeRatio);
          if ((double) lengthF != 0.0)
          {
            splitPanel.SizeInfo.MeasuredLength = (int) Math.Round((double) lengthF * (double) this.layoutInfo.autoSizeLength);
            splitPanelList.RemoveAt(index--);
            autoSizeLength -= splitPanel.SizeInfo.MeasuredLength;
          }
        }
      }
      int count = splitPanelList.Count;
      for (int index = 0; index < count; ++index)
      {
        SplitPanel splitPanel = splitPanelList[index];
        if (index == count - 1)
        {
          splitPanel.SizeInfo.MeasuredLength = autoSizeLength;
          break;
        }
        float num = this.GetLengthF(splitPanel.SizeInfo.AutoSizeScale) + this.layoutInfo.autoSizeCountFactor;
        splitPanel.SizeInfo.MeasuredLength = (int) Math.Round((double) num * (double) this.layoutInfo.autoSizeLength);
        autoSizeLength -= splitPanel.SizeInfo.MeasuredLength;
      }
    }

    private void ClampMeasuredLength()
    {
      int count = this.layoutInfo.layoutTargets.Count;
      if (count == 1)
        return;
      int num1 = this.layoutInfo.availableLength - this.layoutInfo.totalSplitterLength;
      for (int index = 0; index < count; ++index)
      {
        SplitPanel layoutTarget = this.layoutInfo.layoutTargets[index];
        int measuredLength = layoutTarget.SizeInfo.MeasuredLength;
        int minLength = layoutTarget.SizeInfo.minLength;
        int length = this.GetLength(layoutTarget.SizeInfo.MaximumSize);
        int val2 = measuredLength;
        if (length > 0)
          val2 = Math.Min(length, val2);
        int num2 = Math.Max(minLength, val2);
        if (num2 != measuredLength)
          this.layoutInfo.autoSizeTargets.Remove(layoutTarget);
        int num3 = num2;
        layoutTarget.SizeInfo.MeasuredLength = num3;
        this.layoutInfo.totalMeasuredLength += num3;
        num1 -= num3;
      }
      this.FitMeasuredAndLayoutLength();
    }

    private void FitMeasuredAndLayoutLength()
    {
      int num = this.layoutInfo.availableLength - this.layoutInfo.totalSplitterLength;
      if (this.layoutInfo.totalMinLength >= num)
      {
        for (int index = 0; index < this.layoutInfo.layoutTargets.Count; ++index)
        {
          SplitPanel layoutTarget = this.layoutInfo.layoutTargets[index];
          if (layoutTarget.SizeInfo.SizeMode != SplitPanelSizeMode.Absolute)
            layoutTarget.SizeInfo.MeasuredLength = layoutTarget.SizeInfo.minLength;
        }
      }
      else
      {
        int correction = num - this.layoutInfo.totalMeasuredLength;
        if (correction == 0 || this.layoutInfo.autoSizeTargets.Count <= 0)
          return;
        this.PerformMeasureCorrection(correction);
      }
    }

    private void PerformMeasureCorrection(int correction)
    {
      correction = Math.Sign(correction) <= 0 ? Math.Min(-1, correction) : Math.Max(1, correction);
      float measuredLength = 0.0f;
      for (int index = 0; index < this.layoutInfo.autoSizeTargets.Count; ++index)
      {
        SplitPanel autoSizeTarget = this.layoutInfo.autoSizeTargets[index];
        measuredLength += (float) autoSizeTarget.SizeInfo.MeasuredLength;
      }
      this.ApplyLengthCorrection(this.layoutInfo.autoSizeTargets, correction, measuredLength);
    }

    private void ApplyLengthCorrection(
      List<SplitPanel> panels,
      int correction,
      float measuredLength)
    {
      int num1 = Math.Sign(correction);
      int val2 = correction;
      int index = 0;
      while (panels.Count > 0 && (num1 >= 0 || val2 < 0) && (num1 <= 0 || val2 > 0))
      {
        if (index > panels.Count - 1)
          index = 0;
        SplitPanelSizeInfo sizeInfo = panels[index].SizeInfo;
        int val1 = (int) ((double) ((float) (sizeInfo.MeasuredLength - this.GetLength(sizeInfo.SplitterCorrection)) / measuredLength) * (double) correction + 0.5 * (double) num1);
        int num2 = num1 >= 0 ? Math.Max(1, Math.Min(val1, val2)) : Math.Min(-1, Math.Max(val1, val2));
        int num3 = sizeInfo.MeasuredLength + num2;
        int minLength = sizeInfo.minLength;
        if (num3 < minLength)
        {
          num3 = minLength;
          panels.RemoveAt(index--);
        }
        val2 += sizeInfo.MeasuredLength - num3;
        sizeInfo.MeasuredLength = num3;
        ++index;
      }
    }

    private void ApplySplitterCorrection(SplitPanel panel, int dragLength, bool left)
    {
      SizeF autoSizeScale = panel.SizeInfo.AutoSizeScale;
      Size absoluteSize = panel.SizeInfo.AbsoluteSize;
      Size splitterCorrection = panel.SizeInfo.SplitterCorrection;
      SizeF relativeRatio = panel.SizeInfo.RelativeRatio;
      if (left)
        dragLength *= -1;
      if (this.layoutInfo.Orientation == Orientation.Vertical)
      {
        splitterCorrection.Width += dragLength;
        int num = panel.Width + dragLength;
        relativeRatio.Width = (float) num / (float) this.layoutInfo.autoSizeLength;
        autoSizeScale.Width = relativeRatio.Width - this.layoutInfo.autoSizeCountFactor;
        absoluteSize.Width = num;
      }
      else
      {
        splitterCorrection.Height += dragLength;
        int num = panel.Height + dragLength;
        relativeRatio.Height = (float) num / (float) this.layoutInfo.autoSizeLength;
        autoSizeScale.Height = (float) num / (float) this.layoutInfo.autoSizeLength - this.layoutInfo.autoSizeCountFactor;
        absoluteSize.Height = num;
      }
      panel.SizeInfo.SplitterCorrection = splitterCorrection;
      if (this.layoutInfo.fillPanel != null)
      {
        panel.SizeInfo.AbsoluteSize = absoluteSize;
      }
      else
      {
        switch (panel.SizeInfo.SizeMode)
        {
          case SplitPanelSizeMode.Auto:
            panel.SizeInfo.AutoSizeScale = autoSizeScale;
            break;
          case SplitPanelSizeMode.Absolute:
            panel.SizeInfo.AbsoluteSize = absoluteSize;
            break;
          case SplitPanelSizeMode.Relative:
            panel.SizeInfo.RelativeRatio = relativeRatio;
            break;
        }
      }
      if (!(panel is RadSplitContainer))
        return;
      this.PropagateSplitterChangeOnChildren((RadSplitContainer) panel);
    }

    private void PropagateSplitterChangeOnChildren(RadSplitContainer container)
    {
    }

    private int GetLayoutOffset()
    {
      return this.layoutInfo.orientation != Orientation.Horizontal ? this.layoutInfo.contentRect.Left : this.layoutInfo.contentRect.Top;
    }

    private void FindFillPanels(RadSplitContainer container)
    {
      foreach (SplitPanel splitPanel in (IEnumerable<SplitPanel>) container.SplitPanels)
      {
        if (!splitPanel.Collapsed)
        {
          if (splitPanel.SizeInfo.SizeMode == SplitPanelSizeMode.Fill)
          {
            this.layoutInfo.fillPanel = splitPanel;
            break;
          }
          RadSplitContainer radSplitContainer = splitPanel as RadSplitContainer;
          if (radSplitContainer != null && this.ContainsFillPanel((Control) radSplitContainer))
            this.layoutInfo.fillPanel = (SplitPanel) radSplitContainer;
          if (this.layoutInfo.fillPanel != null)
            break;
        }
      }
    }

    private bool ContainsFillPanel(Control parent)
    {
      foreach (Control control in (ArrangedElementCollection) parent.Controls)
      {
        SplitPanel splitPanel = control as SplitPanel;
        if (splitPanel != null && (splitPanel.SizeInfo.SizeMode == SplitPanelSizeMode.Fill || ((object) this.rootContainerType == null || !this.rootContainerType.IsInstanceOfType((object) splitPanel)) && this.ContainsFillPanel((Control) splitPanel)))
          return true;
      }
      return false;
    }

    protected SplitContainerLayoutInfo LayoutInfo
    {
      get
      {
        return this.layoutInfo;
      }
    }

    public System.Type RootContainerType
    {
      get
      {
        return this.rootContainerType;
      }
      set
      {
        this.rootContainerType = value;
      }
    }
  }
}
