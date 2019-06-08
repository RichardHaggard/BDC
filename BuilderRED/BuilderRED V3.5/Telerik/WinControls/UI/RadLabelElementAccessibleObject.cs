// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadLabelElementAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.CodedUI;

namespace Telerik.WinControls.UI
{
  [ComVisible(true)]
  public class RadLabelElementAccessibleObject : RadControlAccessibleObject
  {
    private RadLabelElement owner;

    public RadLabelElementAccessibleObject(RadLabelElement owner, string name)
      : base(owner.ElementTree.Control, name)
    {
      this.owner = owner;
    }

    public override object OwnerElement
    {
      get
      {
        return (object) this.owner;
      }
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
        return base.Description + RadLabelElementAccessibleObject.StripHtmlLikeFormatting(this.owner.Text);
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
        return RadLabelElementAccessibleObject.StripHtmlLikeFormatting(base.Description);
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

    public override Rectangle Bounds
    {
      get
      {
        return new Rectangle(this.owner.ElementTree.Control.PointToScreen(this.owner.ControlBoundingRectangle.Location), this.owner.Size);
      }
    }
  }
}
