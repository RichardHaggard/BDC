// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Keyboard.ChordModifierConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Telerik.WinControls.Keyboard
{
  internal class ChordModifierConverter : TypeConverter
  {
    public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
    {
      if ((object) sourceType != (object) typeof (string))
        return base.CanConvertFrom(context, sourceType);
      return true;
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
    {
      if ((object) destinationType != (object) typeof (string))
        return base.CanConvertTo(context, destinationType);
      return true;
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (!(value is string))
        return base.ConvertFrom(context, culture, value);
      string a = ((string) value).Trim();
      if (!string.Equals(a, string.Empty))
      {
        char[] charArray = ChordConverter.separator.ToCharArray();
        ChordModifier chordModifier = new ChordModifier();
        string[] strArray = a.Split(charArray);
        for (int index = 0; index < strArray.Length; ++index)
        {
          strArray[index] = strArray[index].Trim();
          strArray[index] = strArray[index].ToUpper();
        }
        for (int index = 0; index < strArray.Length; ++index)
        {
          object keyMap = ChordConverter.KeyMaps[(object) strArray[index]];
          if (keyMap == null)
          {
            try
            {
              keyMap = Enum.Parse(typeof (Keys), strArray[index]);
            }
            catch (ArgumentException ex)
            {
            }
          }
          if (keyMap != null)
          {
            Keys keys = (Keys) keyMap;
            if (keys == Keys.Menu)
              chordModifier.AltModifier = true;
            if (keys == Keys.ControlKey)
              chordModifier.ControlModifier = true;
            if (keys == Keys.ShiftKey)
              chordModifier.ShiftModifier = true;
          }
        }
        if (!chordModifier.IsEmpty)
          return (object) chordModifier;
      }
      return (object) null;
    }

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      System.Type destinationType)
    {
      if ((object) destinationType == null)
        throw new ArgumentNullException("Destination Type is not defined.");
      if ((object) destinationType != (object) typeof (string))
        return base.ConvertTo(context, culture, value, destinationType);
      if (value == null)
        return (object) null;
      if (!(value is ChordModifier) && !(value is Keys))
        return base.ConvertTo(context, culture, value, destinationType);
      ChordModifier chordModifier = !(value is Keys) ? value as ChordModifier : ChordModifier.GetModifiers((Keys) value);
      if (chordModifier.IsEmpty)
        return (object) null;
      List<string> stringList = new List<string>(1);
      foreach (string key in (IEnumerable) ChordConverter.KeyMaps.Keys)
      {
        if (chordModifier.AltModifier && Keys.Menu == (Keys) ChordConverter.KeyMaps[(object) key] || chordModifier.ControlModifier && Keys.ControlKey == (Keys) ChordConverter.KeyMaps[(object) key] || chordModifier.ShiftModifier && Keys.ShiftKey == (Keys) ChordConverter.KeyMaps[(object) key])
          stringList.Add(key.ToString());
      }
      if (stringList.Count > 0)
        return (object) string.Join(ChordConverter.separator, stringList.ToArray());
      return (object) null;
    }

    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
      return false;
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }
  }
}
