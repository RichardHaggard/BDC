// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadLabelAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadLabelAccessibleObject : Control.ControlAccessibleObject
  {
    private RadLabel owner;

    public RadLabelAccessibleObject(RadLabel owner)
      : base((Control) owner)
    {
      this.owner = owner;
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.StaticText;
      }
    }

    public override AccessibleStates State
    {
      get
      {
        return AccessibleStates.ReadOnly;
      }
    }

    public override string Name
    {
      get
      {
        return base.Description + RadLabelAccessibleObject.StripHtmlLikeFormatting(this.owner.Text);
      }
      set
      {
        this.owner.Name = value;
      }
    }

    public override string Description
    {
      get
      {
        return RadLabelAccessibleObject.StripHtmlLikeFormatting(base.Description);
      }
    }

    public static string StripHtmlLikeFormatting(string baseText)
    {
      if (baseText == null)
        baseText = "";
      string str = "";
      bool flag = false;
      for (int index = 0; index < baseText.Length; ++index)
      {
        char ch = baseText[index];
        if (ch == '<')
          flag = true;
        if (!flag)
          str += (string) (object) ch;
        if (ch == '>')
          flag = false;
      }
      return str;
    }
  }
}
