// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadShortcut
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class RadShortcut
  {
    public static string ControlText = nameof (Ctrl);
    public static string ShiftText = nameof (Shift);
    public static string AltText = nameof (Alt);
    public const char Delimiter = '+';
    private Keys modifiers;
    private List<Keys> keyMappings;

    public RadShortcut()
    {
      this.modifiers = Keys.None;
      this.keyMappings = new List<Keys>();
    }

    public RadShortcut(Keys modifiers, params Keys[] mappings)
      : this()
    {
      this.modifiers = modifiers;
      if (mappings == null)
        return;
      this.keyMappings.AddRange((IEnumerable<Keys>) mappings);
    }

    public Keys[] KeyMappings
    {
      get
      {
        return this.keyMappings.ToArray();
      }
    }

    public Keys Modifiers
    {
      get
      {
        return this.modifiers;
      }
    }

    public bool Ctrl
    {
      get
      {
        return (this.modifiers & Keys.Control) == Keys.Control;
      }
    }

    public bool Alt
    {
      get
      {
        return (this.modifiers & Keys.Alt) == Keys.Alt;
      }
    }

    public bool Shift
    {
      get
      {
        return (this.modifiers & Keys.Shift) == Keys.Shift;
      }
    }

    public bool IsShortcutCombination(Keys modifiers, params Keys[] keys)
    {
      if (this.keyMappings.Count == 0 || modifiers != this.modifiers || (keys == null || keys.Length != this.keyMappings.Count))
        return false;
      bool flag = true;
      for (int index = 0; index < keys.Length; ++index)
      {
        if (this.keyMappings[index] != keys[index])
        {
          flag = false;
          break;
        }
      }
      return flag;
    }

    public bool IsPartialShortcutCombination(Keys modifiers, params Keys[] keys)
    {
      if (this.keyMappings.Count == 0 || modifiers != this.modifiers || (keys == null || keys.Length >= this.keyMappings.Count))
        return false;
      bool flag = true;
      for (int index = 0; index < keys.Length; ++index)
      {
        if (this.keyMappings[index] != keys[index])
        {
          flag = false;
          break;
        }
      }
      return flag;
    }

    public bool IsMappingKey(Keys key)
    {
      return this.keyMappings.IndexOf(key) >= 0;
    }

    public string GetDisplayText()
    {
      if (this.keyMappings.Count == 0)
        return string.Empty;
      string empty = string.Empty;
      if (this.Ctrl)
        empty += RadShortcut.ControlText;
      if (this.Shift)
      {
        if (empty.Length > 0)
          empty += (string) (object) '+';
        empty += RadShortcut.ShiftText;
      }
      if (this.Alt)
      {
        if (empty.Length > 0)
          empty += (string) (object) '+';
        empty += RadShortcut.AltText;
      }
      string str1 = string.Empty;
      KeysConverter keysConverter = new KeysConverter();
      foreach (Keys keyMapping in this.keyMappings)
        str1 = str1 + keysConverter.ConvertToString((object) keyMapping) + (object) '+';
      string str2 = str1.Remove(str1.Length - 1, 1);
      if (string.IsNullOrEmpty(str2))
        return string.Empty;
      if (string.IsNullOrEmpty(empty))
        return str2;
      return empty + (object) '+' + str2;
    }

    public override string ToString()
    {
      return "RadShortcut - " + this.GetDisplayText();
    }
  }
}
