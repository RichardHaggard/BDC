// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Keyboard.ChordConverter
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
  internal class ChordConverter : TypeConverter, IComparer
  {
    internal static string separator = "+";
    public static Keys[] ValidKeys = new Keys[94]{ Keys.A, Keys.B, Keys.C, Keys.D, Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.Delete, Keys.Down, Keys.E, Keys.End, Keys.F, Keys.F1, Keys.F10, Keys.F11, Keys.F12, Keys.F13, Keys.F14, Keys.F15, Keys.F16, Keys.F17, Keys.F18, Keys.F19, Keys.F2, Keys.F20, Keys.F21, Keys.F22, Keys.F23, Keys.F24, Keys.F3, Keys.F4, Keys.F5, Keys.F6, Keys.F7, Keys.F8, Keys.F9, Keys.G, Keys.H, Keys.I, Keys.Insert, Keys.J, Keys.K, Keys.L, Keys.Left, Keys.M, Keys.N, Keys.NumLock, Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad9, Keys.O, Keys.OemBackslash, Keys.OemClear, Keys.OemCloseBrackets, Keys.Oemcomma, Keys.OemMinus, Keys.OemOpenBrackets, Keys.OemPeriod, Keys.OemPipe, Keys.Oemplus, Keys.OemQuestion, Keys.OemQuotes, Keys.OemSemicolon, Keys.Oemtilde, Keys.P, Keys.Pause, Keys.Q, Keys.R, Keys.Right, Keys.S, Keys.Space, Keys.T, Keys.Tab, Keys.U, Keys.Up, Keys.V, Keys.W, Keys.X, Keys.Y, Keys.Z };
    private static List<string> displayOrder = new List<string>(34);
    public static IDictionary KeyMaps = (IDictionary) new Hashtable(34);
    private const Keys FirstAscii = Keys.A;
    private const Keys FirstDigit = Keys.D0;
    private const Keys FirstNumpadDigit = Keys.NumPad0;
    private const Keys LastAscii = Keys.Z;
    private const Keys LastDigit = Keys.D9;
    private const Keys LastNumpadDigit = Keys.NumPad9;

    static ChordConverter()
    {
      ChordConverter.AddKey("Enter", Keys.Return);
      ChordConverter.AddKey("Ctrl", Keys.ControlKey);
      ChordConverter.AddKey("PgDn", Keys.Next);
      ChordConverter.AddKey("Ins", Keys.Insert);
      ChordConverter.AddKey("Del", Keys.Delete);
      ChordConverter.AddKey("PgUp", Keys.Prior);
      ChordConverter.AddKey("Alt", Keys.Menu);
      ChordConverter.AddKey("0", Keys.D0);
      ChordConverter.AddKey("1", Keys.D1);
      ChordConverter.AddKey("2", Keys.D2);
      ChordConverter.AddKey("3", Keys.D3);
      ChordConverter.AddKey("4", Keys.D4);
      ChordConverter.AddKey("5", Keys.D5);
      ChordConverter.AddKey("6", Keys.D6);
      ChordConverter.AddKey("7", Keys.D7);
      ChordConverter.AddKey("8", Keys.D8);
      ChordConverter.AddKey("9", Keys.D9);
      ChordConverter.AddKey("+", Keys.Oemplus);
      ChordConverter.AddKey("Shift", Keys.ShiftKey);
    }

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
      Chord chord = new Chord();
      if (!string.Equals(a, string.Empty))
      {
        List<Keys> keysList = new List<Keys>();
        string[] strArray = a.Split(ChordConverter.separator.ToCharArray());
        for (int index = 0; index < strArray.Length; ++index)
        {
          strArray[index] = strArray[index].Trim();
          strArray[index] = strArray[index].ToUpper();
        }
        Keys keys1 = Keys.None;
        object keyMap = ChordConverter.KeyMaps[(object) ChordConverter.separator.ToCharArray()];
        if (keyMap != null)
          keys1 = (Keys) keyMap;
        for (int index1 = 0; index1 < strArray.Length; ++index1)
        {
          Keys keys2 = Keys.None;
          foreach (string key in (IEnumerable) ChordConverter.KeyMaps.Keys)
          {
            if (strArray[index1] == key.ToUpper())
            {
              keys2 = (Keys) ChordConverter.KeyMaps[(object) key];
              break;
            }
          }
          if (keys2 == Keys.None)
          {
            try
            {
              object obj = Enum.Parse(typeof (Keys), strArray[index1]);
              if (obj != null)
                keys2 = (Keys) obj;
            }
            catch (ArgumentException ex)
            {
            }
          }
          if (keys2 != Keys.None)
          {
            if (keys2 == Keys.Menu)
              chord.ChordModifier.AltModifier = true;
            else if (keys2 == Keys.ControlKey)
              chord.ChordModifier.ControlModifier = true;
            else if (keys2 == Keys.ShiftKey)
              chord.ChordModifier.ShiftModifier = true;
            else
              keysList.Add(keys2);
          }
          else if (string.IsNullOrEmpty(strArray[index1]))
          {
            bool flag = false;
            int index2 = index1;
            int num = 0;
            while (!flag)
            {
              if (string.IsNullOrEmpty(strArray[index2]))
                ++num;
              if (num == 2)
              {
                num = 0;
                if (keys1 != Keys.None)
                {
                  keysList.Add(keys1);
                }
                else
                {
                  try
                  {
                    object obj = Enum.Parse(typeof (Keys), ChordConverter.separator);
                    if (obj != null)
                      keysList.Add((Keys) obj);
                  }
                  catch
                  {
                  }
                }
              }
              if (string.IsNullOrEmpty(strArray[index2]) && index2 + 1 < strArray.Length)
                ++index2;
              else
                break;
            }
          }
          else
          {
            for (int index2 = 0; index2 < strArray[index1].Length; ++index2)
            {
              try
              {
                object obj = Enum.Parse(typeof (Keys), strArray[index1][index2].ToString().ToUpper());
                if (obj != null)
                  keysList.Add((Keys) obj);
              }
              catch (ArgumentException ex)
              {
                throw new FormatException("Invalid Key Name: " + (object) strArray[index1][index2]);
              }
            }
          }
        }
        if (!chord.ChordModifier.IsEmpty || keysList.Count > 0)
        {
          chord.KeysInternal = keysList;
          return (object) chord;
        }
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
      if (!(value is Chord) && !(value is List<Keys>))
        return base.ConvertTo(context, culture, value, destinationType);
      List<Keys> keysList = value as List<Keys> ?? (value as Chord).KeysInternal;
      string[] strArray = new string[keysList.Count];
      for (int index = 0; index < keysList.Count; ++index)
      {
        Keys keys = keysList[index];
        foreach (string key in (IEnumerable) ChordConverter.KeyMaps.Keys)
        {
          if (keys == (Keys) ChordConverter.KeyMaps[(object) key])
          {
            strArray[index] = key;
            break;
          }
        }
        if (string.IsNullOrEmpty(strArray[index]))
          strArray[index] = keys.ToString();
      }
      string str = string.Empty;
      if (value is Chord)
        str = (value as Chord).ModifierKeys;
      if (string.IsNullOrEmpty(str) && strArray.Length <= 0)
        return (object) null;
      string empty = string.Empty;
      if (!string.IsNullOrEmpty(str))
        empty += str;
      if (strArray.Length > 0)
      {
        if (!string.IsNullOrEmpty(str))
          empty += ChordConverter.separator;
        empty += string.Join(ChordConverter.separator, strArray);
      }
      return (object) empty;
    }

    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
      return false;
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public virtual bool ValidateChordCombination()
    {
      return true;
    }

    private static void AddKey(string key, Keys value)
    {
      ChordConverter.KeyMaps[(object) key] = (object) value;
      ChordConverter.displayOrder.Add(key);
    }

    public int Compare(object a, object b)
    {
      return string.Compare(this.ConvertToString(a), this.ConvertToString(b), false, CultureInfo.InvariantCulture);
    }
  }
}
