// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Keyboard.Chord
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

namespace Telerik.WinControls.Keyboard
{
  [Editor(typeof (ChordsEditor), typeof (UITypeEditor))]
  [TypeConverter(typeof (Telerik.WinControls.Keyboard.ChordConverter))]
  public class Chord : IComparable
  {
    private List<System.Windows.Forms.Keys> keys;
    private ChordModifier chordModifier;
    private TypeConverter chordModifierConverter;
    private TypeConverter chordConverter;

    public Chord()
      : this(new List<System.Windows.Forms.Keys>(), new ChordModifier())
    {
    }

    public Chord(List<System.Windows.Forms.Keys> keys)
      : this(keys, new ChordModifier())
    {
    }

    public Chord(List<System.Windows.Forms.Keys> keys, ChordModifier chordModifier)
    {
      this.keys = keys;
      this.chordModifier = chordModifier;
      this.ProccessModifiers();
    }

    public Chord(string keys)
    {
      this.Keys = keys;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public List<System.Windows.Forms.Keys> KeysInternal
    {
      get
      {
        if (this.keys == null)
          this.keys = new List<System.Windows.Forms.Keys>();
        return this.keys;
      }
      set
      {
        this.keys = value;
        this.ProccessModifiers();
      }
    }

    public string Keys
    {
      get
      {
        if (this.ChordConverter.CanConvertTo(typeof (string)))
          return this.ChordConverter.ConvertToString((object) this);
        return string.Empty;
      }
      set
      {
        if (string.IsNullOrEmpty(value))
        {
          this.Clear();
        }
        else
        {
          if (!this.ChordConverter.CanConvertFrom(typeof (string)))
            return;
          Chord chord = (Chord) this.ChordConverter.ConvertFromString(value);
          if (chord == null)
            return;
          this.KeysInternal = chord.KeysInternal;
          this.ChordModifier = chord.ChordModifier;
        }
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ModifierKeys
    {
      get
      {
        if (this.ChordModifierConverter.CanConvertTo(typeof (string)))
          return this.ChordModifierConverter.ConvertToString((object) this.chordModifier);
        return string.Empty;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ChordKeys
    {
      get
      {
        if (this.ChordConverter.CanConvertTo(typeof (string)))
          return this.ChordConverter.ConvertToString((object) new Chord(this.KeysInternal));
        return string.Empty;
      }
    }

    public ChordModifier ChordModifier
    {
      get
      {
        return this.chordModifier;
      }
      set
      {
        this.chordModifier = value;
      }
    }

    private TypeConverter ChordModifierConverter
    {
      get
      {
        if (this.chordModifierConverter == null)
          this.chordModifierConverter = TypeDescriptor.GetConverter(typeof (ChordModifier));
        return this.chordModifierConverter;
      }
    }

    private TypeConverter ChordConverter
    {
      get
      {
        if (this.chordConverter == null)
          this.chordConverter = TypeDescriptor.GetConverter((object) this);
        return this.chordConverter;
      }
    }

    public void Clear()
    {
      if (this.ChordModifier != null)
        this.ChordModifier.Clear();
      if (this.KeysInternal == null)
        return;
      this.KeysInternal.Clear();
    }

    public override string ToString()
    {
      if (!string.IsNullOrEmpty(this.Keys))
        return this.Keys;
      return base.ToString();
    }

    public void ProccessModifiers()
    {
      if (this.chordModifier == null)
        this.chordModifier = new ChordModifier();
      for (int index = 0; index < this.keys.Count; ++index)
      {
        bool flag = false;
        if ((this.keys[index] & System.Windows.Forms.Keys.Shift) == System.Windows.Forms.Keys.Shift)
        {
          this.chordModifier.ShiftModifier = true;
          flag = true;
        }
        if ((this.keys[index] & System.Windows.Forms.Keys.Control) == System.Windows.Forms.Keys.Control)
        {
          this.chordModifier.ControlModifier = true;
          flag = true;
        }
        if ((this.keys[index] & System.Windows.Forms.Keys.Alt) == System.Windows.Forms.Keys.Alt)
        {
          this.chordModifier.AltModifier = true;
          flag = true;
        }
        if (flag)
          this.keys.Remove(this.keys[index]);
      }
    }

    public void ReverseProccessModifiers()
    {
      if (this.chordModifier == null)
        return;
      if (this.chordModifier.ShiftModifier && !this.keys.Contains(System.Windows.Forms.Keys.ShiftKey))
        this.keys.Add(System.Windows.Forms.Keys.ShiftKey);
      if (this.chordModifier.ControlModifier && !this.keys.Contains(System.Windows.Forms.Keys.ControlKey))
        this.keys.Add(System.Windows.Forms.Keys.ControlKey);
      if (!this.chordModifier.AltModifier || this.keys.Contains(System.Windows.Forms.Keys.Alt))
        return;
      this.keys.Add(System.Windows.Forms.Keys.Alt);
    }

    public int CompareTo(object obj)
    {
      Chord chord = obj as Chord;
      if (chord == null)
        return 1;
      if (this.ChordModifier.CompareTo((object) chord.ChordModifier) != 0)
        return this.ChordModifier.CompareTo((object) chord.ChordModifier);
      if (this.KeysInternal.Count != chord.KeysInternal.Count)
        return this.KeysInternal.Count - chord.KeysInternal.Count;
      this.KeysInternal.Sort();
      chord.KeysInternal.Sort();
      for (int index = 0; index < this.KeysInternal.Count; ++index)
      {
        if (!this.KeysInternal[index].Equals((object) chord.KeysInternal[index]))
          return -1;
      }
      return 0;
    }
  }
}
