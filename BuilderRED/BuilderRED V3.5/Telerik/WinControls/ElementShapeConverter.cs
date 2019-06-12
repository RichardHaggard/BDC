// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ElementShapeConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Tests;
using Telerik.WinControls.UI;

namespace Telerik.WinControls
{
  public class ElementShapeConverter : ComponentConverter
  {
    private const string noneString = "(none)";
    private static bool shownError;

    public ElementShapeConverter()
      : base(typeof (ElementShape))
    {
    }

    public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
    {
      if ((object) sourceType == (object) typeof (string))
        return true;
      return base.CanConvertFrom(context, sourceType);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
    {
      if ((object) destinationType == (object) typeof (string))
        return true;
      return base.CanConvertTo(context, destinationType);
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (context != null && context.Container != null)
        return base.ConvertFrom(context, culture, value);
      if (!(value is string))
        return base.ConvertFrom(context, culture, value);
      if ("(none)".Equals(value))
        return (object) null;
      try
      {
        string[] strArray = (value as string).Split('|');
        if (strArray.Length == 0 || string.IsNullOrEmpty(strArray[0]) || ElementShapeConverter.shownError)
          return (object) null;
        ElementShape elementShape = (ElementShape) null;
        string str = strArray[0].Trim();
        System.Type type = Assembly.GetExecutingAssembly().GetType(str, false, false);
        if ((object) type == null)
        {
          switch (str)
          {
            case "Telerik.WinControls.Tests.DonutShape":
              type = typeof (DonutShape);
              break;
            case "Telerik.WinControls.Tests.QAShape":
              type = typeof (QAShape);
              break;
            case "Telerik.WinControls.EllipseShape":
              type = typeof (EllipseShape);
              break;
            case "Telerik.WinControls.RoundRectShape":
              type = typeof (RoundRectShape);
              break;
            case "Telerik.WinControls.ChamferedRectShape":
              type = typeof (ChamferedRectShape);
              break;
            case "Telerik.WinControls.UI.OfficeShape":
              type = typeof (OfficeShape);
              break;
            case "Telerik.WinControls.UI.TabIEShape":
              type = typeof (TabIEShape);
              break;
            case "Telerik.WinControls.UI.TabOffice12Shape":
              type = typeof (TabOffice12Shape);
              break;
            case "Telerik.WinControls.UI.TabVsShape":
              type = typeof (TabVsShape);
              break;
            case "Telerik.WinControls.UI.TrackBarDThumbShape":
              type = typeof (TrackBarDThumbShape);
              break;
            case "Telerik.WinControls.UI.TrackBarLThumbShape":
              type = typeof (TrackBarLThumbShape);
              break;
            case "Telerik.WinControls.UI.TrackBarRThumbShape":
              type = typeof (TrackBarRThumbShape);
              break;
            case "Telerik.WinControls.UI.TrackBarUThumbShape":
              type = typeof (TrackBarUThumbShape);
              break;
          }
        }
        if ((object) type == null)
          type = RadTypeResolver.Instance.GetTypeByName(str);
        if ((object) type == null)
          return (object) null;
        if (!typeof (ElementShape).IsAssignableFrom(type))
        {
          VSCacheError.ShowVSCacheError(typeof (ElementShape).Assembly, type.Assembly);
          ElementShapeConverter.shownError = true;
        }
        else
        {
          elementShape = (ElementShape) Activator.CreateInstance(type);
          if (strArray.Length > 1)
            elementShape.DeserializeProperties(strArray[1]);
        }
        return (object) elementShape;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Error deserializing custom shape: " + ex.ToString());
        return (object) null;
      }
    }

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      System.Type destinationType)
    {
      if (context != null && context.Container != null)
        return base.ConvertTo(context, culture, value, destinationType);
      if (value == null && (object) destinationType == (object) typeof (string))
        return (object) "(none)";
      if (value == null)
        return (object) null;
      if ((object) destinationType != (object) typeof (string) || !typeof (ElementShape).IsAssignableFrom(value.GetType()))
        return base.ConvertTo(context, culture, value, destinationType);
      string str = ((ElementShape) value).SerializeProperties();
      return (object) (value.GetType().FullName + (string.IsNullOrEmpty(str) ? "" : "|" + str));
    }
  }
}
