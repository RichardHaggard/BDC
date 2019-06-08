// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.PropertyReader
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI;

namespace Telerik.WinControls
{
  public class PropertyReader
  {
    private static CultureInfo serializationCulture = CultureInfo.GetCultureInfo("en-US");
    private static Dictionary<string, PropertyReader.ConvertFromString> directConverters = new Dictionary<string, PropertyReader.ConvertFromString>(55);
    private static Dictionary<string, PropertyReader.TypeConverterInfo> typeDescriptorConverters = new Dictionary<string, PropertyReader.TypeConverterInfo>(200);

    static PropertyReader()
    {
      PropertyReader.directConverters.Add("PaintUsingParentShape", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("ShouldPaint", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("ApplyShapeToControl", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("DrawBorder", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("DrawFill", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("UseDefaultDisabledPaint", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("ShowHorizontalLine", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("DrawSignBorder", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("DrawSignFill", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("StretchVertically", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("Expanded", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("ClipDrawing", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("ShowLines", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("UseCompatibleTextRendering", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("FlipText", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("FullRowSelect", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("DrawArrow", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("AutoCorrectOrientation", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("ShowShadow", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("ShowSlideArea", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("UseDefaultThumbShape", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("UseParentShape", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("AutoSize", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("DrawBorderOnTop", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("MeasureTrailingSpaces", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("DrawVerticalStripes", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("DrawVerticalFills", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("DrawHorizontalStripes", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("DrawHorizontalFills", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("AlternatingHorizonatColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("AlternatingVerticalColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("IsVisible", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("EnableImageTransparency", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("DrawText", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("TextWrap", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("DrawPolarFills", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("DrawRadialFills", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("DrawPolarStripes", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("DrawRadialStripes", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("DrawPageShadow", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("DrawConnectionsBetweenBars", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("ExpandCollapseRing", new PropertyReader.ConvertFromString(PropertyReader.ConvertBool));
      PropertyReader.directConverters.Add("NumberOfColors", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("ZIndex", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("ItemSpacing", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("ElementSpacing", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("NodeSpacing", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("StepWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("SeparatorWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("SweepAngle", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("NumberOfDots", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("HorizontalLineWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("ItemHeight", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("SlideAreaWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("SplitterWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("LineWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("ThumbWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("AutoHidePopupOffset", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("TableHeaderHeight", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("SearchRowHeight", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("RowHeight", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("GroupHeaderHeight", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("CellSpacing", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("RowSpacing", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("FilterRowHeight", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("GroupIndent", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("HourLineStartPosition", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("HeaderHeight", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("RowHeaderColumnWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("SectionLineStartPosition", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("NavigatorsWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("ViewHeaderHeight", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("ColumnHeaderHeight", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("Thickness", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("Radius", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("InnerRadius", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("NewRowHeight", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("HeaderRowHeight", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("ElementCount", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("InitialStartElementAngle", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("ElementNumberOfColors", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("DotRadius", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("LastDotRadius", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("SlowSpeedRange", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("DelayBetweenAnimationCycles", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("DotSweepAngleLifeCycle", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("MaxSpeedSweepAngle", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("MinSweepAngle", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("OuterRingSweepAngle", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("OuterRingWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("InnerRingSweepAngle", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("InnerRingStartAngle", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("InnerRingWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("VerticalOrientationItemSpacing", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("HorizontalOrientationItemSpacing", new PropertyReader.ConvertFromString(PropertyReader.ConvertInt));
      PropertyReader.directConverters.Add("GradientAngle", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("GradientPercentage", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("GradientPercentage2", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("Width", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("BottomWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("TopWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("LeftWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("SliderAreaGradientAngle", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("BorderLeftWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("BorderRightWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("BorderTopWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("AngleTransform", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("BorderWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("ZoomFactor", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("SignWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("BorderGradientAngle", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("BorderBottomWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("RightWidth", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("HeightAspectRatio", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("RadiusAspectRatio", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("ShadowDistance", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("ConnectionWidthProperty", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("ElementGradientPercentage", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("ElementGradientPercentage2", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("SegmentDistance", new PropertyReader.ConvertFromString(PropertyReader.ConvertFloat));
      PropertyReader.directConverters.Add("OffsetFromCenter", new PropertyReader.ConvertFromString(PropertyReader.ConvertDouble));
      PropertyReader.directConverters.Add("Opacity", new PropertyReader.ConvertFromString(PropertyReader.ConvertDouble));
      PropertyReader.directConverters.Add("AccelerationSpeed", new PropertyReader.ConvertFromString(PropertyReader.ConvertDouble));
      PropertyReader.directConverters.Add("DistanceBetweenDots", new PropertyReader.ConvertFromString(PropertyReader.ConvertDouble));
      PropertyReader.directConverters.Add("LineThickness", new PropertyReader.ConvertFromString(PropertyReader.ConvertDouble));
      PropertyReader.directConverters.Add("Text", new PropertyReader.ConvertFromString(PropertyReader.ConvertString));
      PropertyReader.directConverters.Add("Image", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("SignImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("Icon", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("CollapseImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("ExpandImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("HoveredExpandImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("HoveredCollapseImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("ShowFewerButtonsImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("ShowMoreButtonsImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("ErrorRowHeaderImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("CurrentRowHeaderImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("EditRowHeaderImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("NewRowHeaderImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("SearchRowHeaderImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("HotImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("ArrowImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("ExceptionIcon", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("RecurrenceIcon", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("BackgroundImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("FirstPageButtonImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("LastPageButtonImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("NextPageButtonImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("PreviousPageButtonImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("ErrorIcon", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("MoveFirstItemButtonImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("MovePreviousItemButtonImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("MoveNextItemButtonImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("MoveLastItemButtonImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("AddNewButtonImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("DeleteButtonImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("WaitingImage", new PropertyReader.ConvertFromString(PropertyReader.ConvertImage));
      PropertyReader.directConverters.Add("Shape", new PropertyReader.ConvertFromString(PropertyReader.ConvertShape));
      PropertyReader.directConverters.Add("AppointmentShape", new PropertyReader.ConvertFromString(PropertyReader.ConvertShape));
      PropertyReader.directConverters.Add("AppointmentStatusShape", new PropertyReader.ConvertFromString(PropertyReader.ConvertShape));
      PropertyReader.directConverters.Add("AppointmentShadowShape", new PropertyReader.ConvertFromString(PropertyReader.ConvertShape));
      PropertyReader.directConverters.Add("BackColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BackColor2", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BackColor3", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BackColor4", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("ForeColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("ForeColor2", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("ForeColor3", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("ForeColor4", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("InnerColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("InnerColor2", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("InnerColor3", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("InnerColor4", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BottomColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BottomShadowColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("LeftColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("RightColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("TopColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("LeftShadowColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("RightShadowColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("TopShadowColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BorderColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BorderColor2", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BorderColor3", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BorderColor4", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BorderInnerColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("ShadowColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BorderColor1", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("SliderAreaGradientColor1", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("TickColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("SliderAreaGradientColor2", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BorderBottomColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BorderRightColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BorderBottomShadowColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BorderLeftColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BorderTopColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BorderLeftShadowColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BorderRightShadowColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BorderTopShadowColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("HorizontalLineColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BackColor6", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BackColor5", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("LineColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("RightShadowInnerColor1", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("RightShadowInnerColor2", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("RightShadowOuterColor1", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("RightShadowOuterColor2", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("LeftShadowOuterColor1", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("LeftShadowOuterColor2", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("TransparentColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BorderInnerColor2", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BorderInnerColor3", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("BorderInnerColor4", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("AlternatingRowColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("GroupingLinesColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("HourLineColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("HourLineShadowColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("SectionLineColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("CurrentTimePointerColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("AlternatingBackColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("AlternatingBackColor2", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("PageBackground", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("LinksColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("LeftShadowInnerColor1", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("LeftShadowInnerColor2", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("SearchHighlightColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("SelectionFillColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("SelectionStrokeColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("OverlayColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("ConnectionColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("ElementColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("ElementColor2", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("ElementColor3", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("ElementColor4", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("OuterRingBackgroundColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("InnerRingBackgroundColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("ViewportControlBackColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("ViewportControlBorderColor", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("NormalFill", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("SelectedFill", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("FullySelectedFill", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.directConverters.Add("HoverFill", new PropertyReader.ConvertFromString(PropertyReader.ConvertColor));
      PropertyReader.typeDescriptorConverters.Add("Font", new PropertyReader.TypeConverterInfo(typeof (Font)));
      PropertyReader.typeDescriptorConverters.Add("MinutesFont", new PropertyReader.TypeConverterInfo(typeof (Font)));
      PropertyReader.typeDescriptorConverters.Add("DefaultDateTimeTitleFont", new PropertyReader.TypeConverterInfo(typeof (Font)));
      PropertyReader.typeDescriptorConverters.Add("Padding", new PropertyReader.TypeConverterInfo(typeof (Padding)));
      PropertyReader.typeDescriptorConverters.Add("Margin", new PropertyReader.TypeConverterInfo(typeof (Padding)));
      PropertyReader.typeDescriptorConverters.Add("BorderPadding", new PropertyReader.TypeConverterInfo(typeof (Padding)));
      PropertyReader.typeDescriptorConverters.Add("FillPadding", new PropertyReader.TypeConverterInfo(typeof (Padding)));
      PropertyReader.typeDescriptorConverters.Add("BorderThickness", new PropertyReader.TypeConverterInfo(typeof (Padding)));
      PropertyReader.typeDescriptorConverters.Add("AppointmentMargin", new PropertyReader.TypeConverterInfo(typeof (Padding)));
      PropertyReader.typeDescriptorConverters.Add("CellPadding", new PropertyReader.TypeConverterInfo(typeof (Padding)));
      PropertyReader.typeDescriptorConverters.Add("MinSize", new PropertyReader.TypeConverterInfo(typeof (Size)));
      PropertyReader.typeDescriptorConverters.Add("MaxSize", new PropertyReader.TypeConverterInfo(typeof (Size)));
      PropertyReader.typeDescriptorConverters.Add("ButtonsSize", new PropertyReader.TypeConverterInfo(typeof (Size)));
      PropertyReader.typeDescriptorConverters.Add("ThumbSize", new PropertyReader.TypeConverterInfo(typeof (Size)));
      PropertyReader.typeDescriptorConverters.Add("ScaleSize", new PropertyReader.TypeConverterInfo(typeof (Size)));
      PropertyReader.typeDescriptorConverters.Add("PositionOffset", new PropertyReader.TypeConverterInfo(typeof (SizeF)));
      PropertyReader.typeDescriptorConverters.Add("LocationOnCenterGuide", new PropertyReader.TypeConverterInfo(typeof (Point)));
      PropertyReader.typeDescriptorConverters.Add("RowDragHint", new PropertyReader.TypeConverterInfo(typeof (RadImageShape)));
      PropertyReader.typeDescriptorConverters.Add("HorizontalImage", new PropertyReader.TypeConverterInfo(typeof (RadImageShape)));
      PropertyReader.typeDescriptorConverters.Add("VerticalImage", new PropertyReader.TypeConverterInfo(typeof (RadImageShape)));
      PropertyReader.typeDescriptorConverters.Add("TextAlignment", new PropertyReader.TypeConverterInfo(typeof (ContentAlignment)));
      PropertyReader.typeDescriptorConverters.Add("ImageAlignment", new PropertyReader.TypeConverterInfo(typeof (ContentAlignment)));
      PropertyReader.typeDescriptorConverters.Add("Alignment", new PropertyReader.TypeConverterInfo(typeof (ContentAlignment)));
      PropertyReader.typeDescriptorConverters.Add("SmoothingMode", new PropertyReader.TypeConverterInfo(typeof (SmoothingMode)));
      PropertyReader.typeDescriptorConverters.Add("ImageLayout", new PropertyReader.TypeConverterInfo(typeof (ImageLayout)));
      PropertyReader.typeDescriptorConverters.Add("BackgroundImageLayout", new PropertyReader.TypeConverterInfo(typeof (ImageLayout)));
      PropertyReader.typeDescriptorConverters.Add("TextImageRelation", new PropertyReader.TypeConverterInfo(typeof (TextImageRelation)));
      PropertyReader.typeDescriptorConverters.Add("BoxStyle", new PropertyReader.TypeConverterInfo(typeof (BorderBoxStyle)));
      PropertyReader.typeDescriptorConverters.Add("BorderBoxStyle", new PropertyReader.TypeConverterInfo(typeof (BorderBoxStyle)));
      PropertyReader.typeDescriptorConverters.Add("BorderDrawMode", new PropertyReader.TypeConverterInfo(typeof (BorderDrawModes)));
      PropertyReader.typeDescriptorConverters.Add("DisplayStyle", new PropertyReader.TypeConverterInfo(typeof (DisplayStyle)));
      PropertyReader.typeDescriptorConverters.Add("Visibility", new PropertyReader.TypeConverterInfo(typeof (ElementVisibility)));
      PropertyReader.typeDescriptorConverters.Add("CheckPrimitiveStyle", new PropertyReader.TypeConverterInfo(typeof (CheckPrimitiveStyleEnum)));
      PropertyReader.typeDescriptorConverters.Add("TickStyle", new PropertyReader.TypeConverterInfo(typeof (TickStyles)));
      PropertyReader.typeDescriptorConverters.Add("GradientStyle", new PropertyReader.TypeConverterInfo(typeof (GradientStyles)));
      PropertyReader.typeDescriptorConverters.Add("BorderGradientStyle", new PropertyReader.TypeConverterInfo(typeof (GradientStyles)));
      PropertyReader.typeDescriptorConverters.Add("AutoSizeMode", new PropertyReader.TypeConverterInfo(typeof (RadAutoSizeMode)));
      PropertyReader.typeDescriptorConverters.Add("FitToSizeMode", new PropertyReader.TypeConverterInfo(typeof (RadFitToSizeMode)));
      PropertyReader.typeDescriptorConverters.Add("BackgroundShape", new PropertyReader.TypeConverterInfo(typeof (RadImageShape)));
      PropertyReader.typeDescriptorConverters.Add("ColumnDragHint", new PropertyReader.TypeConverterInfo(typeof (RadImageShape)));
      PropertyReader.typeDescriptorConverters.Add("DragHint", new PropertyReader.TypeConverterInfo(typeof (RadImageShape)));
      PropertyReader.typeDescriptorConverters.Add("ItemDropHint", new PropertyReader.TypeConverterInfo(typeof (RadImageShape)));
      PropertyReader.typeDescriptorConverters.Add("ItemDragHint", new PropertyReader.TypeConverterInfo(typeof (RadImageShape)));
      PropertyReader.typeDescriptorConverters.Add("BorderDashStyle", new PropertyReader.TypeConverterInfo(typeof (DashStyle)));
      PropertyReader.typeDescriptorConverters.Add("ConnectionDashStyle", new PropertyReader.TypeConverterInfo(typeof (DashStyle)));
      PropertyReader.typeDescriptorConverters.Add("TimePointerStyle", new PropertyReader.TypeConverterInfo(typeof (RulerCurrentTimePointer)));
      PropertyReader.typeDescriptorConverters.Add("SignStyle", new PropertyReader.TypeConverterInfo(typeof (SignStyles)));
    }

    public static object Deserialize(string propertyName, string value)
    {
      return PropertyReader.Deserialize((string) null, propertyName, value);
    }

    public static object Deserialize(string fullName, string propertyName, string value)
    {
      if (PropertyReader.directConverters.ContainsKey(propertyName))
        return PropertyReader.directConverters[propertyName](value);
      if (PropertyReader.typeDescriptorConverters.ContainsKey(propertyName))
        return PropertyReader.typeDescriptorConverters[propertyName].Converter?.ConvertFromString((ITypeDescriptorContext) null, PropertyReader.serializationCulture, value);
      object obj = (object) null;
      try
      {
        RadProperty property = XmlPropertySetting.DeserializePropertySafe(!string.IsNullOrEmpty(fullName) ? fullName : propertyName);
        if (property != null)
          obj = XmlPropertySetting.DeserializeValue(property, value);
      }
      catch
      {
      }
      return obj;
    }

    private static object ConvertString(string value)
    {
      return (object) value;
    }

    private static object ConvertInt(string value)
    {
      return (object) int.Parse(value, (IFormatProvider) PropertyReader.serializationCulture);
    }

    private static object ConvertFloat(string value)
    {
      return (object) float.Parse(value, (IFormatProvider) PropertyReader.serializationCulture);
    }

    private static object ConvertDouble(string value)
    {
      return (object) double.Parse(value, (IFormatProvider) PropertyReader.serializationCulture);
    }

    private static object ConvertBool(string value)
    {
      return (object) bool.Parse(value);
    }

    private static object ConvertImage(string value)
    {
      return new ImageTypeConverter().ConvertFrom((object) value);
    }

    private static object ConvertShape(string value)
    {
      return new ElementShapeConverter().ConvertFrom((object) value);
    }

    private static object ConvertColor(string value)
    {
      if (value == null)
        return (object) Color.Empty;
      value = value.Trim();
      if (!string.IsNullOrEmpty(value) && value[0] == '#')
        return (object) ColorTranslator.FromHtml(value);
      return TypeDescriptor.GetConverter(typeof (Color)).ConvertFromString((ITypeDescriptorContext) null, PropertyReader.serializationCulture, value);
    }

    private static object EmptyConverter(string value)
    {
      return (object) null;
    }

    private delegate object ConvertFromString(string value);

    private class TypeConverterInfo
    {
      private System.Type type;
      private TypeConverter converter;

      public TypeConverterInfo(System.Type type)
      {
        this.type = type;
      }

      public System.Type Type
      {
        get
        {
          return this.type;
        }
      }

      public TypeConverter Converter
      {
        get
        {
          if (this.converter == null)
          {
            foreach (object customAttribute in this.type.GetCustomAttributes(true))
            {
              TypeConverterAttribute converterAttribute = customAttribute as TypeConverterAttribute;
              if (converterAttribute != null)
              {
                this.converter = (TypeConverter) Activator.CreateInstance(System.Type.GetType(converterAttribute.ConverterTypeName), false);
                break;
              }
            }
            if (this.converter == null)
              this.converter = TypeDescriptor.GetConverter(this.type);
          }
          return this.converter;
        }
      }
    }
  }
}
