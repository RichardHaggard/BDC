// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Code.Accessiblity.RadGridSpinEditorElementAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI.Code.Accessiblity
{
  public sealed class RadGridSpinEditorElementAccessibleObject : Control.ControlAccessibleObject
  {
    private string name;
    private Point location;
    private Size size;
    private RadSpinEditorElement editor;
    private RadGridView owner;
    private CellAccessibleObject parent;

    public RadGridSpinEditorElementAccessibleObject(
      RadGridView owner,
      RadSpinEditorElement editor,
      CellAccessibleObject parent,
      Size size,
      Point location,
      string name)
      : base((Control) owner)
    {
      this.location = location;
      this.size = size;
      this.name = name;
      this.owner = owner;
      this.parent = parent;
      this.editor = editor;
    }

    public override string Name
    {
      get
      {
        return this.name;
      }
    }

    public override string Description
    {
      get
      {
        return "Telerik.WinControls.UI.GridSpinEditorElement ;";
      }
    }

    public override AccessibleRole Role
    {
      get
      {
        return AccessibleRole.Text;
      }
    }

    public override Rectangle Bounds
    {
      get
      {
        return new Rectangle(this.owner.PointToScreen(this.location), this.size);
      }
    }

    public override AccessibleObject Parent
    {
      get
      {
        return (AccessibleObject) this.parent;
      }
    }

    public override AccessibleStates State
    {
      get
      {
        return AccessibleStates.Selectable;
      }
    }
  }
}
