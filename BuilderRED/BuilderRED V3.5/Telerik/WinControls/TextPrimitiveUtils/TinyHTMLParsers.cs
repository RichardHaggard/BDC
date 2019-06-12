// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.TextPrimitiveUtils.TinyHTMLParsers
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.TextPrimitiveUtils
{
  public class TinyHTMLParsers
  {
    private static Color linkColor = Color.Navy;
    private static Color linkedClickedColor = Color.Red;
    private static TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof (Color));
    private static IFormatProvider formatProvider = (IFormatProvider) NumberFormatInfo.InvariantInfo;
    private static FontStyle oldFontStyle;
    private static Color oldFontColor;

    public static Color LinkColor
    {
      get
      {
        return TinyHTMLParsers.linkColor;
      }
      set
      {
        TinyHTMLParsers.linkColor = value;
      }
    }

    public static Color LinkClickedColor
    {
      get
      {
        return TinyHTMLParsers.linkedClickedColor;
      }
      set
      {
        TinyHTMLParsers.linkedClickedColor = value;
      }
    }

    public static bool IsHTMLMode(string text)
    {
      return !string.IsNullOrEmpty(text) && text.Length >= 7 && text[0] == '<' && ((text[1] == 'H' || text[1] == 'h') && (text[2] == 'T' || text[2] == 't')) && ((text[3] == 'M' || text[3] == 'm') && (text[4] == 'L' || text[4] == 'l') && text[5] == '>');
    }

    public static FormattedTextBlock Parse(TextParams textParams)
    {
      return TinyHTMLParsers.Parse(textParams.text, textParams.foreColor, textParams.font.Name, textParams.font.Size, textParams.font.Style, textParams.alignment);
    }

    public static FormattedTextBlock Parse(
      string text,
      Color baseForeColor,
      string baseFont,
      float fontSize,
      ContentAlignment aligment)
    {
      return TinyHTMLParsers.Parse(text, baseForeColor, baseFont, fontSize, FontStyle.Regular, aligment);
    }

    public static FormattedTextBlock Parse(
      string text,
      Color baseForeColor,
      string baseFont,
      float fontSize,
      FontStyle fontStyle,
      ContentAlignment aligment)
    {
      if (string.IsNullOrEmpty(text))
        return new FormattedTextBlock();
      new Stack<FormattedText.HTMLLikeListType>().Push(FormattedText.HTMLLikeListType.None);
      text = text.Replace("\\", "[CDATA[\\]]");
      text = text.Replace("\\<", "&lt;");
      text = text.Replace("\\>", "&gt;");
      text = text.Replace("\r\n", "<br>");
      text = text.Replace("\n", "<br>");
      FormattedTextBlock formattedTextBlock = new FormattedTextBlock();
      FormattedText prevItem = new FormattedText();
      prevItem.FontColor = baseForeColor;
      prevItem.FontName = baseFont;
      prevItem.FontSize = fontSize;
      prevItem.ContentAlignment = aligment;
      prevItem.FontStyle = fontStyle;
      StringTokenizer tokenizer = new StringTokenizer(text, "<");
      int num = tokenizer.Count();
      bool hasOpenTag = text.IndexOf("<") > -1;
      TextLine textLine = new TextLine();
      formattedTextBlock.Lines.Add(textLine);
      bool shouldProduceNewLine = false;
      TinyHTMLParsers.TinyHTMLParsersData parserData = new TinyHTMLParsers.TinyHTMLParsersData();
      for (int index = 0; index < num; ++index)
      {
        FormattedText prototypeFormattedText = TinyHTMLParsers.ProcessToken(ref prevItem, tokenizer, hasOpenTag, parserData, ref shouldProduceNewLine);
        if (!string.IsNullOrEmpty(prototypeFormattedText.HtmlTag) && prototypeFormattedText.HtmlTag.Length >= 1 && shouldProduceNewLine)
        {
          textLine = new TextLine();
          formattedTextBlock.Lines.Add(textLine);
          if (prototypeFormattedText.HtmlTag.Length >= 2)
            prototypeFormattedText.HtmlTag = prototypeFormattedText.HtmlTag.TrimEnd(' ').Trim('/');
          shouldProduceNewLine = !string.IsNullOrEmpty(prototypeFormattedText.text) && prototypeFormattedText.text.Trim().Length == 0;
        }
        textLine.List.Add(prototypeFormattedText);
        prevItem = new FormattedText(prototypeFormattedText);
      }
      return formattedTextBlock;
    }

    public static bool ApplyHTMLSettingsFromTag(
      ref FormattedText currentFormattedText,
      FormattedText prevText,
      string htmlTag,
      TinyHTMLParsers.TinyHTMLParsersData parserData,
      ref bool shouldProduceNewLine,
      ref string text)
    {
      if (string.IsNullOrEmpty(htmlTag))
        return true;
      htmlTag = htmlTag.Trim('<', '>');
      bool flag1 = htmlTag.StartsWith("/");
      currentFormattedText.IsClosingTag = flag1;
      if (flag1)
        htmlTag = htmlTag.TrimStart('/');
      string lower = htmlTag.ToLower();
      bool flag2 = true;
      if (lower == "i" || lower == "em")
        currentFormattedText.FontStyle = TinyHTMLParsers.ProcessFontStyle(currentFormattedText.FontStyle, FontStyle.Italic, flag1);
      else if (lower == "b" || lower == "strong")
        currentFormattedText.FontStyle = TinyHTMLParsers.ProcessFontStyle(currentFormattedText.FontStyle, FontStyle.Bold, flag1);
      else if (lower == "u")
        currentFormattedText.FontStyle = TinyHTMLParsers.ProcessFontStyle(currentFormattedText.FontStyle, FontStyle.Underline, flag1);
      else if (lower.StartsWith("color") && htmlTag.Length > 6)
        currentFormattedText.FontColor = TinyHTMLParsers.ParseColor(htmlTag.Substring(6), new Color?(currentFormattedText.FontColor));
      else if (lower.StartsWith("size=") || lower.StartsWith("size ="))
        currentFormattedText.FontSize = TinyHTMLParsers.ParseSize(htmlTag, prevText.FontSize);
      else if ((lower.StartsWith("font=") || lower.StartsWith("font =")) && htmlTag.Length > 5)
        currentFormattedText.FontName = TinyHTMLParsers.ParseFont(htmlTag.Substring(5));
      else if (lower == "strike")
        currentFormattedText.FontStyle = TinyHTMLParsers.ProcessFontStyle(currentFormattedText.FontStyle, FontStyle.Strikeout, flag1);
      else if (lower.StartsWith("bgcolor"))
        currentFormattedText.BgColor = TinyHTMLParsers.ProcessBgColor(lower, currentFormattedText.BgColor, flag1);
      else if (lower == "ul")
        TinyHTMLParsers.ProcessListEntry(flag1, FormattedText.HTMLLikeListType.List, parserData.lastListType, parserData.lastListNumberCount, currentFormattedText);
      else if (lower == "ol")
        TinyHTMLParsers.ProcessListEntry(flag1, FormattedText.HTMLLikeListType.OrderedList, parserData.lastListType, parserData.lastListNumberCount, currentFormattedText);
      else if (lower == "li")
      {
        int numberCount = 0;
        shouldProduceNewLine = !shouldProduceNewLine && !flag1;
        if (parserData.lastListNumberCount.Count > 0)
          numberCount = parserData.lastListNumberCount.Pop();
        if (parserData.lastListType.Count > 0)
          TinyHTMLParsers.ProcessSingleListEntry(flag1, ref numberCount, currentFormattedText, parserData);
        parserData.lastListNumberCount.Push(numberCount);
      }
      else if (!(lower == "html"))
      {
        if (lower == "br" || lower == "br /" || lower == "br/")
          shouldProduceNewLine = !flag1;
        else if (lower == "p")
        {
          if (flag1)
            shouldProduceNewLine = false;
          else if (!prevText.IsClosingTag && !prevText.StartNewLine)
            shouldProduceNewLine = !shouldProduceNewLine;
        }
        else if (lower.StartsWith("img "))
          TinyHTMLParsers.SetImage(TinyHTMLParsers.ParseImageAttribute(htmlTag, lower, "src"), TinyHTMLParsers.ParseAttribute(htmlTag, lower, "width"), TinyHTMLParsers.ParseAttribute(htmlTag, lower, "height"), currentFormattedText);
        else if (lower.StartsWith("a"))
          TinyHTMLParsers.ProcessLink(currentFormattedText, htmlTag, lower, flag1, ref text);
        else if (lower.StartsWith("span"))
          TinyHTMLParsers.ProcessSpan(ref currentFormattedText, lower, htmlTag, flag1, parserData);
        else
          flag2 = false;
      }
      return flag2;
    }

    private static void ProcessSpan(
      ref FormattedText currentFormattedText,
      string lowerHtmlTag,
      string htmlTag,
      bool isClosingTag,
      TinyHTMLParsers.TinyHTMLParsersData parserData)
    {
      if (isClosingTag)
      {
        if (parserData.lastFormattedText.Count <= 0)
          return;
        currentFormattedText = parserData.lastFormattedText.Pop();
        currentFormattedText.ShouldDisplayBullet = false;
      }
      else
      {
        parserData.lastFormattedText.Push(new FormattedText(currentFormattedText));
        string attribute = TinyHTMLParsers.ParseAttribute(htmlTag, lowerHtmlTag, "style");
        string tag1 = TinyHTMLParsers.ProcessStyle(attribute, "color");
        if (!string.IsNullOrEmpty(tag1))
          currentFormattedText.FontColor = TinyHTMLParsers.ParseColor(tag1, new Color?(currentFormattedText.FontColor));
        string tag2 = TinyHTMLParsers.ProcessStyle(attribute, "background-color");
        if (!string.IsNullOrEmpty(tag2))
          currentFormattedText.BgColor = new Color?(TinyHTMLParsers.ParseColor(tag2, currentFormattedText.BgColor));
        string htmlTag1 = TinyHTMLParsers.ProcessStyle(attribute, "font-family");
        if (!string.IsNullOrEmpty(htmlTag1))
          currentFormattedText.FontName = TinyHTMLParsers.ParseFont(htmlTag1);
        string tag3 = TinyHTMLParsers.ProcessStyle(attribute, "font-size");
        if (string.IsNullOrEmpty(tag3))
          return;
        currentFormattedText.FontSize = TinyHTMLParsers.ParseNumber(tag3, currentFormattedText.FontSize);
      }
    }

    private static string ProcessStyle(string style, string attribute)
    {
      string str1 = style;
      char[] chArray1 = new char[1]{ ';' };
      foreach (string str2 in str1.Split(chArray1))
      {
        char[] chArray2 = new char[1]{ ':' };
        string[] strArray = str2.Split(chArray2);
        if (strArray.Length == 2 && attribute == strArray[0].Trim())
          return strArray[1].Trim();
      }
      return string.Empty;
    }

    private static void ProcessSingleListEntry(
      bool isCloseTag,
      ref int numberCount,
      FormattedText currentFormattedText,
      TinyHTMLParsers.TinyHTMLParsersData parserData)
    {
      FormattedText.HTMLLikeListType htmlLikeListType = parserData.lastListType.Peek();
      currentFormattedText.ShouldDisplayBullet = !isCloseTag;
      if (htmlLikeListType != FormattedText.HTMLLikeListType.OrderedList)
        return;
      if (!isCloseTag)
        ++numberCount;
      currentFormattedText.Number = numberCount;
    }

    private static void ProcessListEntry(
      bool isCloseTag,
      FormattedText.HTMLLikeListType listType,
      Stack<FormattedText.HTMLLikeListType> lastListType,
      Stack<int> lastListNumber,
      FormattedText currentFormattedText)
    {
      if (!isCloseTag)
      {
        currentFormattedText.ListType = listType;
        lastListType.Push(listType);
        ++currentFormattedText.Offset;
        currentFormattedText.BulletFontName = currentFormattedText.FontName;
        currentFormattedText.BulletFontStyle = currentFormattedText.FontStyle;
        currentFormattedText.BulletFontSize = currentFormattedText.FontSize;
        lastListNumber.Push(0);
      }
      else
      {
        if (lastListType.Count > 1)
        {
          int num = (int) lastListType.Pop();
          currentFormattedText.ListType = lastListType.Peek();
        }
        else
          currentFormattedText.ListType = FormattedText.HTMLLikeListType.None;
        --currentFormattedText.Offset;
        if (lastListNumber.Count <= 0)
          return;
        lastListNumber.Pop();
      }
    }

    private static void SetImage(
      string imageName,
      string width,
      string height,
      FormattedText currentFormattedText)
    {
      int result1 = 0;
      int result2 = 0;
      if (!string.IsNullOrEmpty(width))
        int.TryParse(width, out result1);
      if (!string.IsNullOrEmpty(height))
        int.TryParse(height, out result2);
      if (imageName.StartsWith("res:") || imageName.StartsWith("resource:"))
      {
        imageName = imageName.Replace("res:", "").Replace("resource:", "");
        TinyHTMLParsers.SetImageFromRes(imageName.Replace("res:", ""), currentFormattedText, result1, result2);
      }
      else
        TinyHTMLParsers.SetImageFromDisk(imageName, currentFormattedText, result1, result2);
    }

    private static void SetImageFromRes(
      string imageName,
      FormattedText currentFormattedText,
      int width,
      int height)
    {
      if (currentFormattedText.Image != null)
        return;
      if (width > 0 && height > 0)
        currentFormattedText.Image = (Image) new Bitmap(TinyHTMLParsers.GetImageFromResource(imageName), width, height);
      else
        currentFormattedText.Image = TinyHTMLParsers.GetImageFromResource(imageName);
    }

    private static Image GetImageFromResource(string resourceName)
    {
      foreach (RadTypeResolver.LoadedAssembly loadedAssembly in RadTypeResolver.Instance.LoadedAssemblies)
      {
        using (Stream manifestResourceStream = loadedAssembly.assembly.GetManifestResourceStream(resourceName))
        {
          if (manifestResourceStream != null)
            return (Image) new Bitmap(Image.FromStream(manifestResourceStream));
        }
      }
      return (Image) null;
    }

    private static void SetImageFromDisk(
      string imageName,
      FormattedText currentFormattedText,
      int width,
      int height)
    {
      if (currentFormattedText.Image != null)
        return;
      if (imageName.StartsWith("~"))
        imageName = imageName.Replace("~", Application.StartupPath);
      if (File.Exists(imageName))
      {
        if (width > 0 && height > 0)
          currentFormattedText.Image = (Image) new Bitmap(Image.FromFile(imageName), width, height);
        else
          currentFormattedText.Image = Image.FromFile(imageName);
      }
      else
      {
        if (width <= 0 || height <= 0)
          return;
        currentFormattedText.Image = (Image) new Bitmap(width, height);
      }
    }

    private static FormattedText ProcessToken(
      ref FormattedText prevItem,
      StringTokenizer tokenizer,
      bool hasOpenTag,
      TinyHTMLParsers.TinyHTMLParsersData parserData,
      ref bool shouldProduceNewLine)
    {
      string source = tokenizer.NextToken();
      StringTokenizer stringTokenizer = new StringTokenizer(source, ">");
      bool flag = source.IndexOf(">") > -1;
      string htmlTag = stringTokenizer.NextToken();
      string text = stringTokenizer.NextToken();
      FormattedText currentFormattedText = new FormattedText(prevItem);
      if (!hasOpenTag || !flag)
      {
        currentFormattedText.text = htmlTag;
        currentFormattedText.HtmlTag = string.Empty;
      }
      else
      {
        currentFormattedText.text = !TinyHTMLParsers.ApplyHTMLSettingsFromTag(ref currentFormattedText, prevItem, htmlTag, parserData, ref shouldProduceNewLine, ref text) ? htmlTag + text : text;
        currentFormattedText.HtmlTag = htmlTag;
      }
      if (!string.IsNullOrEmpty(currentFormattedText.text))
        currentFormattedText.text = currentFormattedText.text.Replace("&lt;", "<").Replace("&gt;", ">").Replace("[CDATA[\\]]", "\\");
      currentFormattedText.StartNewLine = shouldProduceNewLine;
      return currentFormattedText;
    }

    private static void ProcessLink(
      FormattedText textBlock,
      string htmlTag,
      string lowerHtmlTag,
      bool isCloseTag,
      ref string text)
    {
      if (isCloseTag)
      {
        textBlock.Link = string.Empty;
        textBlock.FontColor = TinyHTMLParsers.oldFontColor;
        textBlock.FontStyle = TinyHTMLParsers.oldFontStyle;
      }
      else
      {
        int num1 = lowerHtmlTag.IndexOf(" href");
        if (num1 == -1)
        {
          textBlock.Link = string.Empty;
          textBlock.FontColor = TinyHTMLParsers.oldFontColor;
          textBlock.FontStyle = TinyHTMLParsers.oldFontStyle;
        }
        else
        {
          int num2 = lowerHtmlTag.IndexOf('=', num1 + " href".Length);
          if (num2 == -1)
          {
            textBlock.Link = string.Empty;
            textBlock.FontColor = TinyHTMLParsers.oldFontColor;
            textBlock.FontStyle = TinyHTMLParsers.oldFontStyle;
          }
          else
          {
            string str = htmlTag.Substring(num2 + 1).Trim();
            textBlock.Link = str;
            TinyHTMLParsers.oldFontColor = textBlock.FontColor;
            TinyHTMLParsers.oldFontStyle = textBlock.FontStyle;
            textBlock.FontStyle |= FontStyle.Underline;
            textBlock.FontColor = TinyHTMLParsers.LinkColor;
            while (text != null && text.Contains(" "))
              text = text.Replace(' ', '_');
          }
        }
      }
    }

    private static Color? ProcessBgColor(string tag, Color? oldColor, bool isCloseTag)
    {
      if (isCloseTag)
        return new Color?();
      if (tag.Length <= 8)
        return new Color?();
      tag = tag.Substring(8);
      return new Color?(TinyHTMLParsers.ParseColor(tag, oldColor));
    }

    private static FontStyle ProcessFontStyle(
      FontStyle currentStyle,
      FontStyle newStyle,
      bool removeNewStyle)
    {
      if (removeNewStyle)
        return currentStyle & ~newStyle;
      return currentStyle | newStyle;
    }

    private static Color ParseColor(string tag, Color? oldColor)
    {
      Color color = Control.DefaultForeColor;
      if (oldColor.HasValue)
        color = oldColor.Value;
      try
      {
        color = (Color) TinyHTMLParsers.typeConverter.ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, tag);
      }
      catch (Exception ex)
      {
      }
      return color;
    }

    private static float ParseSize(string htmlTag, float startValue)
    {
      int startIndex = 5;
      if (htmlTag.Length > 5 && (htmlTag[5] == '+' || htmlTag[5] == '-'))
        startIndex = 6;
      float result;
      if (!float.TryParse(htmlTag.Substring(startIndex), NumberStyles.Any, TinyHTMLParsers.formatProvider, out result) || (double) result <= 0.0)
        return startValue;
      if (startIndex != 6)
        return result;
      return startValue + (htmlTag.Length <= 5 || htmlTag[5] != '-' ? result : -result);
    }

    private static string ParseFont(string htmlTag)
    {
      return htmlTag.Trim('"').Trim('\'');
    }

    private static string ParseImageAttribute(string tag, string lowerTag, string attribute)
    {
      tag = tag.Replace("[CDATA[\\]]", "\\");
      int num1 = lowerTag.IndexOf(attribute);
      if (num1 == -1)
        return string.Empty;
      int num2 = tag.IndexOf("=", num1 + 1);
      if (num2 == -1 || num2 + 1 >= tag.Length)
        return string.Empty;
      char[] anyOf = new char[2]{ '"', '\'' };
      string str = tag.Substring(num2 + 1);
      int length = str.LastIndexOfAny(anyOf);
      if (length < 0)
      {
        length = str.IndexOf(' ');
        if (length < 0)
          length = str.Length;
      }
      return str.Substring(0, length).Trim(anyOf);
    }

    private static string ParseAttribute(string tag, string lowerTag, string attribute)
    {
      int num1 = lowerTag.IndexOf(attribute);
      if (num1 == -1)
        return string.Empty;
      int startIndex = tag.IndexOf("=", num1 + 1);
      if (startIndex == -1)
        return string.Empty;
      int num2 = tag.IndexOfAny("\"'".ToCharArray(), startIndex);
      int num3 = tag.IndexOfAny("\"'".ToCharArray(), num2 + 1);
      if (num2 == -1)
      {
        num2 = tag.IndexOfAny(" =".ToCharArray(), startIndex);
        num3 = tag.IndexOfAny(" >".ToCharArray(), num2 + 1);
        if (num3 == -1)
          num3 = tag.Length - 1;
      }
      return tag.Substring(num2 + 1, num3 - num2).Trim(' ', '/', '"');
    }

    private static float ParseNumber(string tag, float oldValue)
    {
      string[] strArray = new string[9]{ "xx-small", "x-small", "smaller", "small", "medium", "large", "larger", "x-large", "xx-large" };
      float num = (float) ((double) oldValue / 1.5 / 1.5 / 1.5);
      if (tag.EndsWith("pt") && tag.Length > 2)
        tag = tag.Substring(0, tag.Length - 2);
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (tag == strArray[index])
          return num;
        num *= 1.5f;
      }
      float result = 0.0f;
      if (!float.TryParse(tag, NumberStyles.Any, (IFormatProvider) NumberFormatInfo.InvariantInfo, out result) || (double) result <= 0.0)
        return oldValue;
      return result;
    }

    public class TinyHTMLParsersData
    {
      public Stack<int> lastListNumberCount = new Stack<int>();
      public Stack<FormattedText.HTMLLikeListType> lastListType = new Stack<FormattedText.HTMLLikeListType>();
      public Stack<FormattedText> lastFormattedText = new Stack<FormattedText>();
    }
  }
}
