// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SystemSoundItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Media;

namespace Telerik.WinControls.UI
{
  public class SystemSoundItem
  {
    public SystemSound Sound;
    public string SoundName;

    public SystemSoundItem(SystemSound sound, string soundName)
    {
      this.Sound = sound;
      this.SoundName = soundName;
    }

    public override string ToString()
    {
      return this.SoundName;
    }
  }
}
