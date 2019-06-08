// Decompiled with JetBrains decompiler
// Type: BuilderRED.BREDAssessmentServiceConnectionProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using ERDC.CERL.SMS.BRED.Modules.Assessments;
using ERDC.CERL.SMS.Libraries.Business.Assessments;
using ERDC.CERL.SMS.Libraries.Data.DataAccess;
using ERDC.CERL.SMS.Libraries.Portable.Assessments;
using ERDC.CERL.SMS.Libraries.Security.Builder;
using ERDC.CERL.SMS.Libraries.Service.Contracts.Assessments;
using ERDC.CERL.SMS.Libraries.Service.Managers.Assessments;
using JetEntityFrameworkProvider;
using System.Data.Common;

namespace BuilderRED
{
  public class BREDAssessmentServiceConnectionProvider : IAssessmentsServiceProvider
  {
    private AssessmentsImagePackage m_AssessmentsClient;

    public BREDAssessmentServiceConnectionProvider(string connectionString, string packageFileName)
    {
      this.m_AssessmentsClient = new AssessmentsImagePackage((IAssessmentDB) new BREDAssessmentDB((DbConnection) new JetConnection()
      {
        ConnectionString = connectionString
      }), PMFactory.Create(false, false, (IBuilderPrincipalProvider) null), packageFileName, (IUnitSystemPreferenceProvider) new mdUtility.BREDUnitOfMeasurePreferenceProvider());
    }

    public IAssessments AssessmentsClient
    {
      get
      {
        return (IAssessments) this.m_AssessmentsClient;
      }
    }
  }
}
