// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.EnvDTEInterop
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Reflection;

namespace Telerik.Licensing
{
  internal class EnvDTEInterop
  {
    private readonly object _dte;
    private string _solutionName;
    private object _globals;
    private object _solution;

    public EnvDTEInterop(object dte)
    {
      this._dte = dte;
      this.InitializeSolutionName();
      this.InitializeGlobals();
    }

    public virtual void SetVariablePerists(object variable, bool persists)
    {
      this._globals.GetType().InvokeMember("VariablePersists", BindingFlags.SetProperty, (Binder) null, this._globals, new object[2]
      {
        variable,
        (object) persists
      });
    }

    public virtual void SetVariable(object key, object variable)
    {
      this._globals.GetType().InvokeMember("VariableValue", BindingFlags.SetProperty, (Binder) null, this._globals, new object[2]
      {
        key,
        variable
      });
    }

    public bool GetViableExists(object variable)
    {
      return (bool) this._globals.GetType().InvokeMember("VariableExists", BindingFlags.GetProperty, (Binder) null, this._globals, new object[1]{ variable });
    }

    public virtual object GetVariable(object key)
    {
      return this._globals.GetType().InvokeMember("VariableValue", BindingFlags.GetProperty, (Binder) null, this._globals, new object[1]{ key });
    }

    public virtual string GetName()
    {
      return this._solutionName;
    }

    private void InitializeGlobals()
    {
      this._globals = this._solution.GetType().InvokeMember("Globals", BindingFlags.GetProperty, (Binder) null, this._solution, (object[]) null);
    }

    private void InitializeSolutionName()
    {
      this._solution = this._dte.GetType().InvokeMember("Solution", BindingFlags.GetProperty, (Binder) null, this._dte, (object[]) null);
      this._solutionName = (string) this._solution.GetType().InvokeMember("FullName", BindingFlags.GetProperty, (Binder) null, this._solution, (object[]) null);
    }
  }
}
