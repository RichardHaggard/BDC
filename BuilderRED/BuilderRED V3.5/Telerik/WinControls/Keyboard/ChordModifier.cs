// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Keyboard.ChordModifier
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.Keyboard
{
  [TypeConverter(typeof (ChordModifierConverter))]
  public class ChordModifier : IComparable, INotifyPropertyChanged
  {
    private bool shiftModifier;
    private bool altModifier;
    private bool controlModifier;

    public ChordModifier(Keys pressedKey)
    {
      ChordModifier.GetModifiers(this, pressedKey);
    }

    public ChordModifier(bool altModifier, bool controlModifier, bool shiftModifier)
    {
      this.altModifier = altModifier;
      this.controlModifier = controlModifier;
      this.shiftModifier = shiftModifier;
    }

    public ChordModifier(ChordModifier chordModifier)
    {
      this.altModifier = chordModifier.altModifier;
      this.controlModifier = chordModifier.controlModifier;
      this.shiftModifier = chordModifier.shiftModifier;
    }

    public ChordModifier()
    {
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsEmpty
    {
      get
      {
        if (!this.shiftModifier && !this.controlModifier)
          return !this.altModifier;
        return false;
      }
    }

    public bool ShiftModifier
    {
      get
      {
        return this.shiftModifier;
      }
      set
      {
        this.shiftModifier = value;
        this.OnNotifyPropertyChanged(nameof (ShiftModifier));
      }
    }

    public bool ControlModifier
    {
      get
      {
        return this.controlModifier;
      }
      set
      {
        this.controlModifier = value;
        this.OnNotifyPropertyChanged(nameof (ControlModifier));
      }
    }

    public bool AltModifier
    {
      get
      {
        return this.altModifier;
      }
      set
      {
        this.altModifier = value;
        this.OnNotifyPropertyChanged(nameof (AltModifier));
      }
    }

    public static ChordModifier GetModifiers(
      ChordModifier tempModifier,
      Keys pressedKey)
    {
      if ((pressedKey & Keys.Shift) == Keys.Shift)
        tempModifier.ShiftModifier = true;
      if ((pressedKey & Keys.Control) == Keys.Control)
        tempModifier.ControlModifier = true;
      if ((pressedKey & Keys.Alt) == Keys.Alt)
        tempModifier.AltModifier = true;
      return tempModifier;
    }

    public static ChordModifier GetModifiers(Keys pressedKey)
    {
      return ChordModifier.GetModifiers(new ChordModifier(), pressedKey);
    }

    public void Clear()
    {
      this.shiftModifier = false;
      this.altModifier = false;
      this.controlModifier = false;
    }

    public int CompareTo(object obj)
    {
      ChordModifier chordModifier = obj as ChordModifier;
      if (chordModifier == null && obj is Keys)
        chordModifier = ChordModifier.GetModifiers(new ChordModifier(), (Keys) obj);
      if (chordModifier == null)
        return 1;
      if (this.AltModifier == chordModifier.AltModifier)
      {
        if (this.ControlModifier == chordModifier.ControlModifier)
        {
          if (this.ShiftModifier == chordModifier.ShiftModifier)
            return 0;
          return this.ShiftModifier ? 1 : -1;
        }
        return this.ControlModifier ? 1 : -1;
      }
      return this.AltModifier ? 1 : -1;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnNotifyPropertyChanged(string propertyName)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, e);
    }
  }
}
