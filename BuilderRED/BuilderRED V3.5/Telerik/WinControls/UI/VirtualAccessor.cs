// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualAccessor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  internal class VirtualAccessor : Accessor
  {
    public VirtualAccessor(GridViewColumn column)
      : base(column)
    {
    }

    public override object this[GridViewRowInfo row]
    {
      get
      {
        GridViewCellValueEventArgs args = new GridViewCellValueEventArgs(row, this.Column);
        this.Template.EventDispatcher.RaiseEvent<GridViewCellValueEventArgs>(EventDispatcher.CellValueNeeded, (object) this.Template, args);
        return args.Value;
      }
      set
      {
        this.Template.EventDispatcher.RaiseEvent<GridViewCellValueEventArgs>(EventDispatcher.CellValuePushed, (object) this.Template, new GridViewCellValueEventArgs(row, this.Column)
        {
          Value = value
        });
      }
    }
  }
}
