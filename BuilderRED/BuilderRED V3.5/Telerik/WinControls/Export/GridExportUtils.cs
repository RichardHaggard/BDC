// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.GridExportUtils
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.Export
{
  public class GridExportUtils
  {
    public static Color ColorMixer(params Color[] colors)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      for (int index = 0; index < colors.Length; ++index)
      {
        num1 += (int) colors[index].R;
        num2 += (int) colors[index].G;
        num3 += (int) colors[index].B;
      }
      return Color.FromArgb(num1 / colors.Length, num2 / colors.Length, num3 / colors.Length);
    }

    public static Color ColorMixer(
      GradientStyles gradientSyle,
      int numberOfColors,
      float gradientPercent,
      float gradientPercent2,
      params Color[] colors)
    {
      if (numberOfColors < 1 || numberOfColors > 4)
        throw new ArgumentException("Invalid number of colors. Should be between 1 and 4");
      Color color;
      if (gradientSyle != GradientStyles.Solid)
      {
        switch (numberOfColors)
        {
          case 1:
            break;
          case 2:
            color = GridExportUtils.ColorMixer(colors[0], colors[1]);
            goto label_8;
          case 3:
            float num1 = 0.0f;
            float num2 = 0.0f;
            float num3 = 0.0f;
            float num4 = num1 + (float) ((double) colors[0].R * (double) gradientPercent / 2.0);
            float num5 = num2 + (float) ((double) colors[0].G * (double) gradientPercent / 2.0);
            float num6 = num3 + (float) ((double) colors[0].B * (double) gradientPercent / 2.0);
            float num7 = num4 + (float) ((int) colors[1].R / 2);
            float num8 = num5 + (float) ((int) colors[1].G / 2);
            float num9 = num6 + (float) ((int) colors[1].B / 2);
            color = Color.FromArgb((int) (num7 + (float) colors[2].R * (float) ((1.0 - (double) gradientPercent) / 2.0)), (int) (num8 + (float) colors[2].G * (float) ((1.0 - (double) gradientPercent) / 2.0)), (int) (num9 + (float) colors[2].B * (float) ((1.0 - (double) gradientPercent) / 2.0)));
            goto label_8;
          default:
            float num10 = 0.0f;
            float num11 = 0.0f;
            float num12 = 0.0f;
            float num13 = num10 + (float) ((double) colors[0].R * (double) gradientPercent / 2.0);
            float num14 = num11 + (float) ((double) colors[0].G * (double) gradientPercent / 2.0);
            float num15 = num12 + (float) ((double) colors[0].B * (double) gradientPercent / 2.0);
            float num16 = num13 + (float) colors[1].R * (float) ((double) gradientPercent / 2.0 + ((double) gradientPercent2 - (double) gradientPercent) / 2.0);
            float num17 = num14 + (float) colors[1].G * (float) ((double) gradientPercent / 2.0 + ((double) gradientPercent2 - (double) gradientPercent) / 2.0);
            float num18 = num15 + (float) colors[1].B * (float) ((double) gradientPercent / 2.0 + ((double) gradientPercent2 - (double) gradientPercent) / 2.0);
            float num19 = num16 + (float) colors[2].R * (float) (((double) gradientPercent2 - (double) gradientPercent) / 2.0 + (1.0 - (double) gradientPercent2) / 2.0);
            float num20 = num17 + (float) colors[2].G * (float) (((double) gradientPercent2 - (double) gradientPercent) / 2.0 + (1.0 - (double) gradientPercent2) / 2.0);
            float num21 = num18 + (float) colors[2].B * (float) (((double) gradientPercent2 - (double) gradientPercent) / 2.0 + (1.0 - (double) gradientPercent2) / 2.0);
            color = Color.FromArgb((int) (num19 + (float) colors[3].R * (float) ((1.0 - (double) gradientPercent2) / 2.0)), (int) (num20 + (float) colors[3].G * (float) ((1.0 - (double) gradientPercent2) / 2.0)), (int) (num21 + (float) colors[3].B * (float) ((1.0 - (double) gradientPercent2) / 2.0)));
            goto label_8;
        }
      }
      color = colors[0];
label_8:
      return color;
    }

    public static Color GetBackColor(LightVisualElement element)
    {
      RadElement radElement = (RadElement) element;
      Color color = Color.Empty;
      LightVisualElement lightVisualElement = (LightVisualElement) null;
      if (element.BackColor.A < (byte) 200 || !element.DrawFill)
      {
        while (radElement.Parent != null)
        {
          radElement = radElement.Parent;
          lightVisualElement = radElement as LightVisualElement;
          if (lightVisualElement != null && (lightVisualElement.BackColor.A > (byte) 200 && lightVisualElement.DrawFill))
            break;
        }
      }
      else
        lightVisualElement = radElement as LightVisualElement;
      if (lightVisualElement != null)
      {
        color = GridExportUtils.ColorMixer(lightVisualElement.GradientStyle, lightVisualElement.NumberOfColors, lightVisualElement.GradientPercentage, lightVisualElement.GradientPercentage2, lightVisualElement.BackColor, lightVisualElement.BackColor2, lightVisualElement.BackColor3, lightVisualElement.BackColor4);
      }
      else
      {
        RootRadElement rootRadElement = radElement as RootRadElement;
        if (rootRadElement != null)
          color = rootRadElement.BackColor;
      }
      return color;
    }

    internal static void ReleaseRowElement(
      RadGridView radGridView,
      RowElementProvider rowProvider,
      GridRowElement rowElement)
    {
      GridExportUtils.ReleaseRowElement(radGridView, rowProvider, rowElement, true);
    }

    internal static void ReleaseRowElement(
      RadGridView radGridView,
      RowElementProvider rowProvider,
      GridRowElement rowElement,
      bool cacheRow)
    {
      if (cacheRow)
        rowProvider.CacheElement((IVirtualizedElement<GridViewRowInfo>) rowElement);
      rowElement.Detach();
      radGridView.TableElement.Children.Remove((RadElement) rowElement);
      rowElement.ResumeLayout(false);
    }

    internal static void ReleaseCellElement(
      CellElementProvider cellProvider,
      GridRowElement rowElement,
      GridCellElement cell)
    {
      GridExportUtils.ReleaseCellElement(cellProvider, rowElement, cell, true);
    }

    internal static void ReleaseCellElement(
      CellElementProvider cellProvider,
      GridRowElement rowElement,
      GridCellElement cell,
      bool cache)
    {
      GridVirtualizedCellElement virtualizedCellElement = cell as GridVirtualizedCellElement;
      if (virtualizedCellElement != null)
      {
        if (cache)
          cellProvider.CacheElement((IVirtualizedElement<GridViewColumn>) virtualizedCellElement);
        virtualizedCellElement.Detach();
        rowElement.Children.Remove((RadElement) cell);
      }
      else
        cell.Dispose();
    }

    internal static double GetRowHeight(
      RadGridView radGridView,
      RowElementProvider rowProvider,
      CellElementProvider cellProvider,
      GridViewRowInfo gridViewRowInfo,
      bool exportVisualSettings)
    {
      double val1 = 0.0;
      if (radGridView.AutoSizeRows && exportVisualSettings)
      {
        GridRowElement element1 = rowProvider.GetElement(gridViewRowInfo, (object) null) as GridRowElement;
        element1.InitializeRowView(radGridView.TableElement);
        element1.Initialize(gridViewRowInfo);
        radGridView.TableElement.Children.Add((RadElement) element1);
        foreach (GridViewColumn column in (Collection<GridViewDataColumn>) element1.ViewTemplate.Columns)
        {
          if (!(column is GridViewRowHeaderColumn) && !(column is GridViewIndentColumn))
          {
            GridCellElement element2 = cellProvider.GetElement(column, (object) element1) as GridCellElement;
            element1.Children.Add((RadElement) element2);
            element2.Initialize(column, element1);
            val1 = Math.Max(val1, (double) GridExportUtils.GetCellDesiredSize(element2).Height);
            GridExportUtils.ReleaseCellElement(cellProvider, element1, element2);
          }
        }
        GridExportUtils.ReleaseRowElement(radGridView, rowProvider, element1);
      }
      return Math.Max(val1, (double) radGridView.TableElement.RowScroller.ElementProvider.GetElementSize(gridViewRowInfo).Height);
    }

    internal static SizeF GetCellDesiredSize(GridCellElement cell)
    {
      cell.SetContent();
      cell.UpdateInfo();
      (cell as GridHeaderCellElement)?.UpdateArrowState();
      cell.ResetLayout(true);
      cell.Measure(new SizeF((float) cell.ColumnInfo.Width, float.PositiveInfinity));
      return cell.DesiredSize;
    }

    public static SizeF ConvertMmToDipSize(SizeF value)
    {
      return new SizeF((float) GridExportUtils.MmToDip((double) value.Width), (float) GridExportUtils.MmToDip((double) value.Height));
    }

    public static SizeF ConvertDipToMmSize(SizeF value)
    {
      return new SizeF((float) GridExportUtils.DipToMm((double) value.Width), (float) GridExportUtils.DipToMm((double) value.Height));
    }

    public static Padding ConvertMmToDipPadding(Padding value)
    {
      return new Padding() { Left = (int) GridExportUtils.MmToDip((double) value.Left), Top = (int) GridExportUtils.MmToDip((double) value.Top), Right = (int) GridExportUtils.MmToDip((double) value.Right), Bottom = (int) GridExportUtils.MmToDip((double) value.Bottom) };
    }

    public static Padding ConvertDipToMmPadding(Padding value)
    {
      return new Padding() { Left = (int) GridExportUtils.DipToMm((double) value.Left), Top = (int) GridExportUtils.DipToMm((double) value.Top), Right = (int) GridExportUtils.DipToMm((double) value.Right), Bottom = (int) GridExportUtils.DipToMm((double) value.Bottom) };
    }

    public static double MmToDip(double value)
    {
      return value * 96.0 / 25.4;
    }

    public static double DipToMm(double value)
    {
      return value * 25.4 / 96.0;
    }

    public static ContentAlignment ConvertToRightToLeftAlignment(
      ContentAlignment alignment)
    {
      ContentAlignment contentAlignment;
      switch (alignment)
      {
        case ContentAlignment.TopLeft:
          contentAlignment = ContentAlignment.TopRight;
          break;
        case ContentAlignment.TopRight:
          contentAlignment = ContentAlignment.TopLeft;
          break;
        case ContentAlignment.MiddleLeft:
          contentAlignment = ContentAlignment.MiddleRight;
          break;
        case ContentAlignment.MiddleRight:
          contentAlignment = ContentAlignment.MiddleLeft;
          break;
        case ContentAlignment.BottomLeft:
          contentAlignment = ContentAlignment.BottomRight;
          break;
        case ContentAlignment.BottomRight:
          contentAlignment = ContentAlignment.BottomLeft;
          break;
        default:
          contentAlignment = alignment;
          break;
      }
      return contentAlignment;
    }

    public static byte[] ConvertImageToByteArray(Image image)
    {
      return (byte[]) new ImageConverter().ConvertTo((object) image, typeof (byte[]));
    }
  }
}
