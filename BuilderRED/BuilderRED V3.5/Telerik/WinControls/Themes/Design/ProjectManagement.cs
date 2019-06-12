// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Themes.Design.ProjectManagement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Themes.Design
{
  public class ProjectManagement
  {
    private DteServices _Services;

    private ProjectManagement(IServiceProvider provider)
    {
      this._Services = new DteServices(provider);
    }

    public static ProjectManagement GetProjectManagementInstance(
      IServiceProvider provider)
    {
      if (provider != null)
        return new ProjectManagement(provider);
      return (ProjectManagement) null;
    }

    internal static string GetProjectFolder(IServiceProvider provider)
    {
      return new DteServices(provider).GetActiveProjectFullPath();
    }

    public DteServices Services
    {
      get
      {
        return this._Services;
      }
    }
  }
}
