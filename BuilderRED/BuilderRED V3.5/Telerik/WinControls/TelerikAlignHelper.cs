// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.TelerikAlignHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls
{
  public class TelerikAlignHelper
  {
    private static readonly TextImageRelation[] imageAlignToRelation = new TextImageRelation[11]
    {
      TextImageRelation.ImageBeforeText | TextImageRelation.ImageAboveText,
      TextImageRelation.ImageAboveText,
      TextImageRelation.TextBeforeImage | TextImageRelation.ImageAboveText,
      TextImageRelation.Overlay,
      TextImageRelation.ImageBeforeText,
      TextImageRelation.Overlay,
      TextImageRelation.TextBeforeImage,
      TextImageRelation.Overlay,
      TextImageRelation.ImageBeforeText | TextImageRelation.TextAboveImage,
      TextImageRelation.TextAboveImage,
      TextImageRelation.TextBeforeImage | TextImageRelation.TextAboveImage
    };

    public static TextFormatFlags TranslateAlignmentForGDI(ContentAlignment align)
    {
      ContentAlignment contentAlignment1 = ContentAlignment.BottomLeft | ContentAlignment.BottomCenter | ContentAlignment.BottomRight;
      ContentAlignment contentAlignment2 = ContentAlignment.MiddleLeft | ContentAlignment.MiddleCenter | ContentAlignment.MiddleRight;
      if ((align & contentAlignment1) != (ContentAlignment) 0)
        return TextFormatFlags.Bottom;
      return (align & contentAlignment2) != (ContentAlignment) 0 ? TextFormatFlags.VerticalCenter : TextFormatFlags.Default;
    }

    public static TextFormatFlags TranslateLineAlignmentForGDI(ContentAlignment align)
    {
      ContentAlignment contentAlignment1 = ContentAlignment.TopRight | ContentAlignment.MiddleRight | ContentAlignment.BottomRight;
      ContentAlignment contentAlignment2 = ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter;
      if ((align & contentAlignment1) != (ContentAlignment) 0)
        return TextFormatFlags.Right;
      return (align & contentAlignment2) != (ContentAlignment) 0 ? TextFormatFlags.HorizontalCenter : TextFormatFlags.Default;
    }

    public static StringAlignment TranslateAlignment(ContentAlignment align)
    {
      ContentAlignment contentAlignment1 = ContentAlignment.TopRight | ContentAlignment.MiddleRight | ContentAlignment.BottomRight;
      ContentAlignment contentAlignment2 = ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter;
      if ((align & contentAlignment1) != (ContentAlignment) 0)
        return StringAlignment.Far;
      return (align & contentAlignment2) != (ContentAlignment) 0 ? StringAlignment.Center : StringAlignment.Near;
    }

    public static StringAlignment TranslateLineAlignment(ContentAlignment align)
    {
      ContentAlignment contentAlignment1;
      ContentAlignment contentAlignment2 = contentAlignment1 = ContentAlignment.BottomLeft | ContentAlignment.BottomCenter | ContentAlignment.BottomRight;
      ContentAlignment contentAlignment3 = ContentAlignment.MiddleLeft | ContentAlignment.MiddleCenter | ContentAlignment.MiddleRight;
      if ((align & contentAlignment2) != (ContentAlignment) 0)
        return StringAlignment.Far;
      return (align & contentAlignment3) != (ContentAlignment) 0 ? StringAlignment.Center : StringAlignment.Near;
    }

    public static ContentAlignment RtlTranslateContent(ContentAlignment align)
    {
      ContentAlignment[][] contentAlignmentArray = new ContentAlignment[3][]
      {
        new ContentAlignment[2]
        {
          ContentAlignment.TopLeft,
          ContentAlignment.TopRight
        },
        new ContentAlignment[2]
        {
          ContentAlignment.MiddleLeft,
          ContentAlignment.MiddleRight
        },
        new ContentAlignment[2]
        {
          ContentAlignment.BottomLeft,
          ContentAlignment.BottomRight
        }
      };
      for (int index = 0; index < 3; ++index)
      {
        if (contentAlignmentArray[index][0] == align)
          return contentAlignmentArray[index][1];
        if (contentAlignmentArray[index][1] == align)
          return contentAlignmentArray[index][0];
      }
      return align;
    }

    public static TextImageRelation RtlTranslateRelation(TextImageRelation relation)
    {
      if (relation == TextImageRelation.ImageBeforeText)
        return TextImageRelation.TextBeforeImage;
      if (relation == TextImageRelation.TextBeforeImage)
        return TextImageRelation.ImageBeforeText;
      return relation;
    }

    public static TextImageRelation ImageAlignToRelation(ContentAlignment alignment)
    {
      return TelerikAlignHelper.imageAlignToRelation[LayoutUtils.ContentAlignmentToIndex(alignment)];
    }

    public static TextImageRelation TextAlignToRelation(ContentAlignment alignment)
    {
      return LayoutUtils.GetOppositeTextImageRelation(TelerikAlignHelper.ImageAlignToRelation(alignment));
    }
  }
}
