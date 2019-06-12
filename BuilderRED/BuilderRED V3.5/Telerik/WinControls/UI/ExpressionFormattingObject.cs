// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ExpressionFormattingObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class ExpressionFormattingObject : BaseFormattingObject
  {
    private string expression;

    public ExpressionFormattingObject()
      : this("NewCondition", string.Empty, false)
    {
    }

    public ExpressionFormattingObject(string name, string expression, bool applyToRow)
      : base(name, applyToRow)
    {
      this.expression = expression;
    }

    [DefaultValue("")]
    public string Expression
    {
      get
      {
        return this.expression;
      }
      set
      {
        if (!(this.expression != value))
          return;
        this.expression = value;
        this.OnPropertyChanged(nameof (Expression));
      }
    }

    public override bool Evaluate(GridViewRowInfo row, GridViewColumn column)
    {
      if (!this.Enabled || string.IsNullOrEmpty(this.expression))
        return false;
      object obj = row.ViewTemplate.ListSource.CollectionView.Evaluate(this.expression, (IEnumerable<GridViewRowInfo>) new GridViewRowInfo[1]
      {
        row
      });
      if (!(obj is bool))
        return false;
      return (bool) obj;
    }

    public override object Clone()
    {
      return (object) ReflectionHelper.Clone<ExpressionFormattingObject>(this);
    }

    public override void Copy(BaseFormattingObject source)
    {
      ExpressionFormattingObject source1 = source as ExpressionFormattingObject;
      if (source1 != null)
      {
        ReflectionHelper.CopyFields<ExpressionFormattingObject>(this, source1);
        this.OnPropertyChanged("CellBackColor");
      }
      else
        base.Copy(source);
    }
  }
}
