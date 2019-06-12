// Decompiled with JetBrains decompiler
// Type: BuilderRED.AssessmentsImagePackage
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using ERDC.CERL.SMS.Libraries.Business.Assessments;
using ERDC.CERL.SMS.Libraries.Data.DataAccess;
using ERDC.CERL.SMS.Libraries.DataContract.Assessments;
using ERDC.CERL.SMS.Libraries.DataContract.Common;
using ERDC.CERL.SMS.Libraries.Service.Contracts.Assessments;
using ERDC.CERL.SMS.Libraries.Service.Managers.Assessments;
using SMS.Libraries.Utility.BredPackage.Classes.Containers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BuilderRED
{
  public class AssessmentsImagePackage : IAssessments, IImageDeletionProvider
  {
    private ZipBredPackage _ZP;
    private ERDC.CERL.SMS.Libraries.Service.Managers.Assessments.Assessments _AssessmentsClient;

    public ERDC.CERL.SMS.Libraries.Service.Managers.Assessments.Assessments AssessmentsClient
    {
      get
      {
        return this._AssessmentsClient;
      }
    }

    public AssessmentsImagePackage(
      IAssessmentDB assessDB,
      IPersistenceManager PM,
      string packageFileName,
      IUnitSystemPreferenceProvider UnitsSystemProvider)
    {
      this._ZP = new ZipBredPackage(packageFileName, false);
      this._AssessmentsClient = new ERDC.CERL.SMS.Libraries.Service.Managers.Assessments.Assessments(assessDB, PM, (IImageDeletionProvider) this, UnitsSystemProvider, false);
    }

    public ERDC.CERL.SMS.Libraries.DataContract.Common.GuidResult CreateAssessment(
      Guid SessionID,
      string name)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.CreateAssessment(SessionID, name);
    }

    public StringGuidResult CreateAssessmentInstance(
      Guid SessionID,
      Guid entityID,
      DateTime assessDate,
      CreateAssessmentInstanceOptions options)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.CreateAssessmentInstance(SessionID, entityID, assessDate, options);
    }

    public ERDC.CERL.SMS.Libraries.DataContract.Common.GuidResult CreateAssessmentSetDescription(
      Guid SessionID,
      string Name)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.CreateAssessmentSetDescription(SessionID, Name);
    }

    public Guid CreateFunctionalArea(
      Guid SessionID,
      string bldgID,
      string name,
      double area,
      int functionalUseTypeID,
      int useTypeID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.CreateFunctionalArea(SessionID, bldgID, name, area, functionalUseTypeID, useTypeID);
    }

    public ERDC.CERL.SMS.Libraries.DataContract.Common.GuidResult CreateValueCategory(
      Guid SessionID,
      string name)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.CreateValueCategory(SessionID, name);
    }

    public FunctionResultMessage DeleteAssessment(Guid SessionID, Guid ID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.DeleteAssessment(SessionID, ID);
    }

    public FunctionResultMessage DeleteAssessmentInstance(
      Guid SessionID,
      Guid id)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.DeleteAssessmentInstance(SessionID, id);
    }

    public FunctionResultMessage DeleteAssessmentSet(Guid SessionID, Guid ID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.DeleteAssessmentSet(SessionID, ID);
    }

    public FunctionResultMessage DeleteAssessmentSetDescription(
      Guid SessionID,
      Guid ID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.DeleteAssessmentSetDescription(SessionID, ID);
    }

    public FunctionResultMessage DeleteValueCategory(Guid SessionID, Guid ID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.DeleteValueCategory(SessionID, ID);
    }

    public ValueCategoryResult EditValueCategory(Guid SessionID, Guid id)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.EditValueCategory(SessionID, id);
    }

    public GetAssessmentResult GetAssessment(Guid SessionID, Guid ID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetAssessment(SessionID, ID);
    }

    public GetAssessmentDetailsResult GetAssessmentDetails(
      Guid SessionID,
      Guid id,
      OrgType type)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetAssessmentDetails(SessionID, id, type);
    }

    public StringGuidListResult GetAssessmentHistory(
      Guid SessionID,
      Guid objectid,
      Guid assessmentID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetAssessmentHistory(SessionID, objectid, assessmentID);
    }

    public GetAssessmentResult GetAssessmentInstance(Guid SessionID, Guid ID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetAssessmentInstance(SessionID, ID);
    }

    public StringGuidListResult GetAssessmentInstances(
      Guid SessionID,
      Guid objectid)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetAssessmentInstancesAsync(SessionID, objectid);
    }

    public StringGuidListResult GetAssessments(Guid SessionID, OrgType type)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetAssessments(SessionID, type);
    }

    public AssessmentOverviewCollectionResult GetAssessmentsByType(
      Guid SessionID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetAssessmentsByType(SessionID);
    }

    public AssessmentSetResult GetAssessmentSet(Guid SessionID, Guid ID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetAssessmentSet(SessionID, ID);
    }

    public AssessmentSetHistoryResult GetAssessmentSetHistory(
      Guid SessionID,
      Guid objectid,
      OrgType type)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetAssessmentSetHistory(SessionID, objectid, type);
    }

    public AssessmentOverviewCollectionResult GetAssessmentSetsByType(
      Guid SessionID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetAssessmentSetsByType(SessionID);
    }

    public IntResult GetBuildingUseType(Guid SessionID, Guid BuildingID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetBuildingUseType(SessionID, BuildingID);
    }

    public MissingComponentDisplayDataLabel GetCMC_Data(
      Guid SessionID,
      string id)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetCMC_Data(SessionID, id);
    }

    public ObservableCollection<StringPair> GetComponentsList(
      Guid SessionID,
      string id)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetComponentsList(SessionID, id);
    }

    public ObservableCollection<StringPair> GetCompTypeList(
      Guid SessionID,
      string id,
      string value)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetCompTypeList(SessionID, id, value);
    }

    public EditAssessmentResult GetEditAssessmentData(Guid SessionID, Guid ID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetEditAssessmentData(SessionID, ID);
    }

    public GetEditAssessmentSetDescriptionResult GetEditAssessmentSetDescriptionData(
      Guid SessionID,
      Guid ID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetEditAssessmentSetDescriptionData(SessionID, ID);
    }

    public FunctionalAreaResult GetFunctionalAreaData(Guid SessionID, Guid ID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetFunctionalAreaData(SessionID, ID);
    }

    public IntResult GetFunctionalAreaUseType(Guid SessionID, Guid FuntionalAreaID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetFunctionalAreaUseType(SessionID, FuntionalAreaID);
    }

    public ObservableCollection<StringIntPair> GetFunctionalUseTypes(
      Guid SessionID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetFunctionalUseTypes(SessionID);
    }

    public ObservableCollection<StringPair> GetMatCatList(
      Guid SessionID,
      string id)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetMatCatList(SessionID, id);
    }

    public MissingComponentsResult GetMissingComponenetsData(
      Guid SessionID,
      string BuildingID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetMissingComponenetsData(SessionID, BuildingID);
    }

    public ObservableCollection<MissingComponentDisplayDataLabel> GetMissingComponentsDisplayLabels(
      Guid SessionID,
      AssessmentQuestion question)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetMissingComponentsListDisplayLabels(SessionID, question);
    }

    public AssessmentSetResult GetNewAssessmentSet(
      Guid SessionID,
      Guid assessmentSetDescID,
      Guid objectID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetNewAssessmentSet(SessionID, assessmentSetDescID, objectID);
    }

    public ObservableCollection<BuildingPartDescription> getSectionLevelDetail(
      Guid SessionID,
      string buildingID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.getSectionLevelDetail(SessionID, buildingID);
    }

    public ObservableCollection<StringPair> GetSystemsList(
      Guid SessionID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetSystemsList(SessionID);
    }

    public StringGuidListResult GetTypeAssessmentSets(
      Guid SessionID,
      OrgType type)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetTypeAssessmentSets(SessionID, type);
    }

    public string GetUnitOfMeasure(Guid SessionID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetUnitOfMeasure(SessionID);
    }

    public StringIntListResult GetUseTypes(Guid SessionID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetUseTypes(SessionID);
    }

    public StringGuidListResult GetValueCategories(Guid SessionID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.GetValueCategories(SessionID);
    }

    public GuidListResult SaveAssessmentInstances(
      Guid SessionID,
      Guid entityID,
      DateTime assessDate,
      ObservableCollection<Guid> instanceIDs,
      ObservableCollection<CreateOrUpdateAssessmentInstanceOptions> options)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.SaveAssessmentInstances(SessionID, entityID, assessDate, instanceIDs, options);
    }

    public ERDC.CERL.SMS.Libraries.DataContract.Common.GuidResult SaveAssessmentSet(
      Guid SessionID,
      CreateAssessmentSetOptions data)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.SaveAssessmentSet(SessionID, data);
    }

    public SeismicLookupResult SeismicGetLookupData(Guid SessionID)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.SeismicGetLookupData(SessionID);
    }

    public FunctionResultMessage UpdateAssessment(
      Guid SessionID,
      Guid ID,
      EditAssessmentData data)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.UpdateAssessment(SessionID, ID, data);
    }

    public FunctionResultMessage UpdateAssessmentInstance(
      Guid SessionID,
      Guid ID,
      DateTime assessDate,
      CreateAssessmentInstanceOptions options)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.UpdateAssessmentInstance(SessionID, ID, assessDate, options);
    }

    public FunctionResultMessage UpdateAssessmentSetDescription(
      Guid SessionID,
      Guid ID,
      AssessmentSetDescriptionDetails details)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.UpdateAssessmentSetDescription(SessionID, ID, details);
    }

    public FunctionResultMessage UpdateFunctionalArea(
      Guid SessionID,
      Guid ID,
      string Name,
      double Area,
      int UseType,
      int FunctionType,
      ObservableCollection<Guid> Sections)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.UpdateFunctionalArea(SessionID, ID, Name, Area, UseType, FunctionType, Sections);
    }

    public FunctionResultMessage UpdateValueCategory(
      Guid SessionID,
      AssessmentValueCategory data)
    {
      lock ((object) this._AssessmentsClient)
        return this.AssessmentsClient.UpdateValueCategory(SessionID, data);
    }

    public ImageResult GetAssessmentImage(Guid SessionID, Guid ID)
    {
      Guid imageReponseImageId;
      lock ((object) this._AssessmentsClient)
        imageReponseImageId = this.AssessmentsClient.GetImageReponseImageID(ID);
      this._ZP.LoadManifest();
      byte[] numArray = this._ZP.RetrieveAttachmentData(imageReponseImageId);
      ImageResult imageResult1;
      if (numArray != null)
      {
        ImageResult imageResult2 = new ImageResult();
        imageResult2.Status = FunctionResultMessage.Success;
        imageResult2.ImageData = ((IEnumerable<byte>) numArray).ToArray<byte>();
        imageResult1 = imageResult2;
      }
      return imageResult1;
    }

    public ERDC.CERL.SMS.Libraries.DataContract.Common.GuidResult UploadAssessmentImage(
      Guid SessionID,
      Guid QuestionID,
      byte[] Data,
      string FileName)
    {
      Guid guid = Guid.NewGuid();
      this._ZP.LoadManifest();
      Guid imageID = this._ZP.AddAttachment(Data, QuestionID, guid, FileName);
      this._ZP.SaveManifest();
      lock ((object) this._AssessmentsClient)
        this.AssessmentsClient.CreateImageResponseForImageWithID(guid, imageID);
      ERDC.CERL.SMS.Libraries.DataContract.Common.GuidResult guidResult = new ERDC.CERL.SMS.Libraries.DataContract.Common.GuidResult();
      guidResult.set_ID(guid);
      return guidResult;
    }

    public void DeleteImageIfNoAssociatedResponses(
      IAssessmentDB context,
      Assessment_Response_Image img)
    {
      if (img.Responses.Count != 0)
        return;
      this._ZP.DeleteManifestEntry(img.ImageID);
      context.Assessment_Response_Images.Remove(img);
    }

    public FunctionResultMessage RecomputeAssessments(Guid SessionID)
    {
      throw new Exception("Recompute assessments not implemented in BRED.");
    }
  }
}
