// Decompiled with JetBrains decompiler
// Type: BuilderRED.mdRightFormHandler
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using ERDC.CERL.SMS.BRED.Modules.Assessments.Views;
using ERDC.CERL.SMS.Libraries.DataContract.Common;
using Infragistics.Win.UltraWinTree;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BuilderRED
{
  [StandardModule]
  internal sealed class mdRightFormHandler
  {
    private static Panel pCurrentPanel;

    internal static Panel GetCurrentFrame()
    {
      return mdRightFormHandler.pCurrentPanel;
    }

    internal static void SetCurrentFrame(Panel NewPanel)
    {
      if (mdRightFormHandler.pCurrentPanel != null && Operators.CompareString(mdRightFormHandler.pCurrentPanel.Name, NewPanel.Name, false) == 0)
        return;
      if (mdRightFormHandler.pCurrentPanel != null)
      {
        mdRightFormHandler.SizeFrame(ref NewPanel);
        mdRightFormHandler.pCurrentPanel.Visible = false;
      }
      else
        mdRightFormHandler.SizeFrame(ref NewPanel);
      if (Operators.CompareString(NewPanel.Name, "pnlStartup", false) == 0)
      {
        string str1 = "Startup";
        ref string local1 = ref str1;
        string str2 = "";
        ref string local2 = ref str2;
        mdRightFormHandler.ButtonHandler(ref local1, ref local2);
      }
      mdRightFormHandler.pCurrentPanel = NewPanel;
      mdRightFormHandler.pCurrentPanel.Visible = true;
      mdRightFormHandler.pCurrentPanel.BringToFront();
    }

    internal static void SetCurrentItem(string strKey, string strObjectName, string strID)
    {
      try
      {
        if (mdUtility.fMainForm.Mode == frmMain.ProgramMode.pmFunctionality)
        {
          mdRightFormHandler.SetCurrentFrame(mdUtility.fMainForm.pnlFuncAssessment);
          OrgType orgType = OrgType.Facility;
          string Left = strObjectName;
          if (Operators.CompareString(Left, "FuncArea", false) != 0)
          {
            if (Operators.CompareString(Left, "Building", false) == 0)
            {
              string str1 = "Building";
              ref string local1 = ref str1;
              string str2 = "";
              ref string local2 = ref str2;
              mdRightFormHandler.ButtonHandler(ref local1, ref local2);
            }
          }
          else
          {
            string str1 = "FuncArea";
            ref string local1 = ref str1;
            string str2 = "";
            ref string local2 = ref str2;
            mdRightFormHandler.ButtonHandler(ref local1, ref local2);
            orgType = OrgType.FunctionalArea;
          }
          MainPageView mainPageView1 = mdUtility.fMainForm.MainPageView1;
          mdRightFormHandler.BREDFunctionalityOrgNodeHandler functionalityOrgNodeHandler = new mdRightFormHandler.BREDFunctionalityOrgNodeHandler(mdUtility.fMainForm.tvFunctionality.GetNodeByKey(strKey));
          functionalityOrgNodeHandler.set_ID(Guid.Parse(strKey));
          functionalityOrgNodeHandler.Type = orgType;
          mainPageView1.SetCurrentTarget((OrgNode) functionalityOrgNodeHandler);
        }
        else
        {
          mdUtility.fMainForm.Cursor = Cursors.WaitCursor;
          string Left = strObjectName;
          if (Operators.CompareString(Left, "Building", false) != 0)
          {
            if (Operators.CompareString(Left, "Location", false) != 0)
            {
              if (Operators.CompareString(Left, "System", false) != 0)
              {
                if (Operators.CompareString(Left, "Component", false) != 0)
                {
                  if (Operators.CompareString(Left, "Section", false) != 0)
                  {
                    if (Operators.CompareString(Left, "Inspection", false) == 0)
                    {
                      mdRightFormHandler.SetCurrentFrame(mdUtility.fMainForm.pnlInspectionInfo);
                      string str1 = "Inspection";
                      ref string local1 = ref str1;
                      string str2 = "";
                      ref string local2 = ref str2;
                      mdRightFormHandler.ButtonHandler(ref local1, ref local2);
                      if (mdUtility.fMainForm.m_bInspLoaded)
                      {
                        if (Operators.CompareString(Strings.Left(strKey, 1), "L", false) == 0)
                          Inspection.LoadInspectionDates(mdUtility.fMainForm.tvInspection.GetNodeByKey(strKey).Parent.Key);
                        else
                          Inspection.LoadInspectionDates(strID);
                      }
                    }
                  }
                  else
                  {
                    mdRightFormHandler.SetCurrentFrame(mdUtility.fMainForm.pnlSectionInfo);
                    string strForm = "Section";
                    mdRightFormHandler.ButtonHandler(ref strForm, ref strID);
                    Section.LoadSection(strID, true);
                  }
                }
                else
                {
                  mdRightFormHandler.SetCurrentFrame(mdUtility.fMainForm.pnlComponentInfo);
                  string strForm = "Component";
                  mdRightFormHandler.ButtonHandler(ref strForm, ref strID);
                }
              }
              else
              {
                mdRightFormHandler.SetCurrentFrame(mdUtility.fMainForm.pnlSystemInfo);
                string strForm = "System";
                mdRightFormHandler.ButtonHandler(ref strForm, ref strID);
              }
            }
            else
            {
              mdRightFormHandler.SetCurrentFrame(mdUtility.fMainForm.pnlLocationInfo);
              string strForm = "Location";
              mdRightFormHandler.ButtonHandler(ref strForm, ref strID);
            }
          }
          else
          {
            if (mdUtility.fMainForm.Mode == frmMain.ProgramMode.pmInspection)
              mdRightFormHandler.SetCurrentFrame(mdUtility.fMainForm.pnlBuildingInsp);
            else if (mdUtility.fMainForm.Mode == frmMain.ProgramMode.pmInventory)
            {
              mdRightFormHandler.SetCurrentFrame(mdUtility.fMainForm.pnlBuildingInfo);
              Building.LoadBuilding(strID);
            }
            else
            {
              mdRightFormHandler.SetCurrentFrame(mdUtility.fMainForm.pnlFunctionality);
              Building.LoadBuilding(strID);
            }
            string strForm = "Building";
            mdRightFormHandler.ButtonHandler(ref strForm, ref strID);
          }
        }
      }
      finally
      {
        mdUtility.fMainForm.Cursor = Cursors.Default;
      }
    }

    internal static void SizeFrame(ref Panel frmFrame)
    {
    }

    internal static void ImageButtonHandler(ref string strForm, ref string strID = "")
    {
      mdUtility.fMainForm.tsbNew1.Enabled = true;
    }

    internal static void ButtonHandler(ref string strForm, ref string strID = "")
    {
      mdUtility.fMainForm.tsddbToggle.Enabled = true;
      mdUtility.fMainForm.tsbNew1.Enabled = true;
      mdUtility.fMainForm.tsbNew1.Visible = true;
      mdUtility.fMainForm.tsbNew2.Enabled = true;
      mdUtility.fMainForm.tsbNew2.Visible = true;
      mdUtility.fMainForm.tsbDelete.Enabled = true;
      mdUtility.fMainForm.tsbDelete.Visible = true;
      mdUtility.fMainForm.tss2.Visible = true;
      mdUtility.fMainForm.tsbEdit.Visible = false;
      mdUtility.fMainForm.tsbEdit.Text = "Edit";
      mdUtility.fMainForm.tsbComment.Enabled = true;
      mdUtility.fMainForm.tss3.Visible = false;
      string str = strForm;
      // ISSUE: reference to a compiler-generated method
      switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(str))
      {
        case 718440320:
          if (Operators.CompareString(str, "Component", false) == 0)
          {
            if (mdUtility.fMainForm.Mode == frmMain.ProgramMode.pmInventory)
            {
              mdUtility.fMainForm.tsbNew1.Visible = true;
              mdUtility.fMainForm.tsbNew1.Enabled = true;
              mdUtility.fMainForm.tsbNew1.Text = "Add Component";
              mdUtility.fMainForm.tsbNew1.Image = (Image) BuilderRED.My.Resources.Resources.NewComponent;
              mdUtility.fMainForm.tsbNew2.Visible = true;
              mdUtility.fMainForm.tsbNew2.Enabled = true;
              mdUtility.fMainForm.tsbNew2.Text = "Add Section";
              mdUtility.fMainForm.tsbNew2.Image = (Image) BuilderRED.My.Resources.Resources.NewSection;
              mdUtility.fMainForm.tsbDelete.Visible = true;
              mdUtility.fMainForm.tsbDelete.Enabled = true;
              mdUtility.fMainForm.tsbDelete.Text = "Delete Component";
              mdUtility.fMainForm.tsbImages.Visible = true;
            }
            else
            {
              mdUtility.fMainForm.miToolsInspectSections.Enabled = false;
              mdUtility.fMainForm.tsbImages.Visible = false;
              if (Operators.CompareString(mdUtility.fMainForm.CurrentLocation, "", false) == 0)
              {
                mdUtility.fMainForm.tsbNew1.Visible = false;
                mdUtility.fMainForm.tsbNew2.Visible = false;
                mdUtility.fMainForm.tsbDelete.Visible = false;
              }
              else
              {
                mdUtility.fMainForm.tsbNew1.Visible = true;
                mdUtility.fMainForm.tsbNew1.Enabled = true;
                mdUtility.fMainForm.tsbNew1.Text = "Inspect Component";
                mdUtility.fMainForm.tsbNew1.Image = (Image) BuilderRED.My.Resources.Resources.NewComponent;
                mdUtility.fMainForm.tsbNew2.Visible = true;
                mdUtility.fMainForm.tsbNew2.Enabled = true;
                mdUtility.fMainForm.tsbNew2.Text = "Inspect Section";
                mdUtility.fMainForm.tsbNew2.Image = (Image) BuilderRED.My.Resources.Resources.NewSection;
                mdUtility.fMainForm.tsbDelete.Visible = false;
              }
            }
            mdUtility.fMainForm.tss2.Visible = false;
            mdUtility.fMainForm.tsbEdit.Visible = false;
            mdUtility.fMainForm.tsbSave.Visible = false;
            mdUtility.fMainForm.tsbCancel.Visible = false;
            mdUtility.fMainForm.tss3.Visible = false;
            mdUtility.fMainForm.tsbComment.Visible = false;
            break;
          }
          break;
        case 763853676:
          if (Operators.CompareString(str, "Section", false) == 0)
          {
            mdUtility.fMainForm.miToolsInspectSections.Enabled = false;
            mdUtility.fMainForm.tsbNew1.Visible = true;
            mdUtility.fMainForm.tsbNew1.Enabled = true;
            mdUtility.fMainForm.tsbNew1.Text = "Add Section";
            mdUtility.fMainForm.tsbNew1.Image = (Image) BuilderRED.My.Resources.Resources.NewSection;
            mdUtility.fMainForm.tsbNew2.Visible = false;
            mdUtility.fMainForm.tsbNew2.Enabled = true;
            mdUtility.fMainForm.tsbDelete.Visible = true;
            mdUtility.fMainForm.tsbDelete.Enabled = true;
            mdUtility.fMainForm.tsbDelete.Text = "Delete Section";
            mdUtility.fMainForm.tss2.Visible = true;
            mdUtility.fMainForm.tsbEdit.Visible = false;
            mdUtility.fMainForm.tsbEdit.Enabled = false;
            mdUtility.fMainForm.tsbCancel.Visible = true;
            mdUtility.fMainForm.tsbCancel.Enabled = false;
            mdUtility.fMainForm.tsbSave.Visible = true;
            mdUtility.fMainForm.tsbSave.Enabled = false;
            mdUtility.fMainForm.tsbComment.Visible = true;
            mdUtility.fMainForm.tss3.Visible = true;
            if (mdUtility.fMainForm.Mode == frmMain.ProgramMode.pmInventory)
            {
              mdUtility.fMainForm.tsbImages.Visible = true;
              break;
            }
            break;
          }
          break;
        case 1539345862:
          if (Operators.CompareString(str, "Location", false) == 0)
          {
            if (Operators.CompareString(mdUtility.fMainForm.tvInspection.ActiveNode.Tag.ToString(), "Non-sampling", false) == 0)
            {
              mdUtility.fMainForm.miToolsInspectSections.Enabled = false;
              mdUtility.fMainForm.tsbNew1.Visible = false;
              mdUtility.fMainForm.tsbNew2.Visible = false;
              mdUtility.fMainForm.tsbDelete.Visible = false;
              mdUtility.fMainForm.tss2.Visible = false;
              mdUtility.fMainForm.tsbEdit.Visible = false;
              mdUtility.fMainForm.tsbImages.Visible = false;
            }
            else if (Operators.CompareString(mdUtility.fMainForm.tvInspection.ActiveNode.Tag.ToString(), "Sample Locations", false) == 0)
            {
              mdUtility.fMainForm.miToolsInspectSections.Enabled = false;
              mdUtility.fMainForm.tsbNew1.Visible = true;
              mdUtility.fMainForm.tsbNew1.Text = "Add Location";
              mdUtility.fMainForm.tsbNew1.Image = (Image) BuilderRED.My.Resources.Resources.NewSampleLocation;
              mdUtility.fMainForm.tsbNew2.Visible = false;
              mdUtility.fMainForm.tsbDelete.Visible = false;
              mdUtility.fMainForm.tss2.Visible = false;
              mdUtility.fMainForm.tsbEdit.Visible = false;
            }
            else
            {
              mdUtility.fMainForm.miToolsInspectSections.Enabled = true;
              mdUtility.fMainForm.tsbNew1.Visible = true;
              mdUtility.fMainForm.tsbNew1.Text = "Add Location";
              mdUtility.fMainForm.tsbNew1.Image = (Image) BuilderRED.My.Resources.Resources.NewSampleLocation;
              mdUtility.fMainForm.tsbNew2.Visible = true;
              mdUtility.fMainForm.tsbNew2.Text = "Inspect Component";
              mdUtility.fMainForm.tsbNew2.Image = (Image) BuilderRED.My.Resources.Resources.NewComponent;
              mdUtility.fMainForm.tsbDelete.Visible = true;
              mdUtility.fMainForm.tsbDelete.Text = "Delete Location";
              mdUtility.fMainForm.tsbEdit.Visible = true;
              mdUtility.fMainForm.tsbEdit.Enabled = true;
              mdUtility.fMainForm.tsbEdit.Text = "Edit Location";
              mdUtility.fMainForm.tsbImages.Visible = false;
            }
            mdUtility.fMainForm.tss2.Visible = false;
            mdUtility.fMainForm.tsbCancel.Visible = false;
            mdUtility.fMainForm.tsbSave.Visible = false;
            mdUtility.fMainForm.tss3.Visible = false;
            mdUtility.fMainForm.tsbComment.Visible = false;
            break;
          }
          break;
        case 2324152213:
          if (Operators.CompareString(str, "Building", false) == 0)
          {
            if (mdUtility.fMainForm.Mode == frmMain.ProgramMode.pmInventory)
            {
              mdUtility.fMainForm.tsbNew1.Visible = true;
              mdUtility.fMainForm.tsbNew1.Text = "Add Building";
              mdUtility.fMainForm.tsbNew1.Image = (Image) BuilderRED.My.Resources.Resources.NewBuilding;
              mdUtility.fMainForm.tsbNew2.Visible = true;
              mdUtility.fMainForm.tsbNew2.Enabled = true;
              mdUtility.fMainForm.tsbNew2.Text = "Add System";
              mdUtility.fMainForm.tsbNew2.Image = (Image) BuilderRED.My.Resources.Resources.NewSystem;
              mdUtility.fMainForm.tsbDelete.Visible = true;
              mdUtility.fMainForm.tsbDelete.Enabled = true;
              mdUtility.fMainForm.tsbDelete.Text = "Delete Building";
              mdUtility.fMainForm.tss2.Visible = true;
              mdUtility.fMainForm.tsbEdit.Visible = false;
              mdUtility.fMainForm.tsbEdit.Enabled = false;
              mdUtility.fMainForm.tsbSave.Visible = true;
              mdUtility.fMainForm.tsbSave.Enabled = false;
              mdUtility.fMainForm.tsbCancel.Visible = true;
              mdUtility.fMainForm.tsbCancel.Enabled = false;
              mdUtility.fMainForm.tss3.Visible = true;
              mdUtility.fMainForm.tsbComment.Visible = true;
              mdUtility.fMainForm.tsbImages.Visible = true;
              mdUtility.fMainForm.tsbWorkItems.Visible = false;
              break;
            }
            if (mdUtility.fMainForm.Mode == frmMain.ProgramMode.pmInspection)
            {
              mdUtility.fMainForm.miToolsInspectSections.Enabled = false;
              mdUtility.fMainForm.tsbNew1.Visible = false;
              mdUtility.fMainForm.tsbNew2.Visible = false;
              mdUtility.fMainForm.tsbDelete.Visible = false;
              mdUtility.fMainForm.tss2.Visible = false;
              mdUtility.fMainForm.tsbEdit.Visible = false;
              mdUtility.fMainForm.tsbCancel.Visible = false;
              mdUtility.fMainForm.tss3.Visible = false;
              mdUtility.fMainForm.tsbComment.Visible = false;
              mdUtility.fMainForm.tsbImages.Visible = false;
              mdUtility.fMainForm.tsbWorkItems.Visible = true;
              break;
            }
            mdUtility.fMainForm.tsbNew1.Visible = true;
            mdUtility.fMainForm.tsbNew1.Text = "Add Functional Area";
            mdUtility.fMainForm.tsbNew1.Image = (Image) BuilderRED.My.Resources.Resources.NewFunctionalArea;
            mdUtility.fMainForm.tsbNew1.Enabled = true;
            mdUtility.fMainForm.tsbNew2.Visible = false;
            mdUtility.fMainForm.tsbDelete.Visible = true;
            mdUtility.fMainForm.tsbDelete.Enabled = false;
            mdUtility.fMainForm.tsbDelete.Text = "Delete Area";
            mdUtility.fMainForm.tss1.Enabled = true;
            mdUtility.fMainForm.tss1.Visible = true;
            mdUtility.fMainForm.tss2.Visible = false;
            mdUtility.fMainForm.tsbEdit.Visible = false;
            mdUtility.fMainForm.tsbSave.Visible = false;
            mdUtility.fMainForm.tsbSave.Enabled = false;
            mdUtility.fMainForm.tsbCancel.Visible = false;
            mdUtility.fMainForm.tss3.Visible = false;
            mdUtility.fMainForm.tsbComment.Visible = false;
            mdUtility.fMainForm.tsbWorkItems.Visible = false;
            mdUtility.fMainForm.tsbImages.Visible = false;
            mdUtility.fMainForm.tsbTally.Visible = false;
            break;
          }
          break;
        case 2402387132:
          if (Operators.CompareString(str, "System", false) == 0)
          {
            if (mdUtility.fMainForm.Mode == frmMain.ProgramMode.pmInventory)
            {
              mdUtility.fMainForm.tsbNew1.Visible = true;
              mdUtility.fMainForm.tsbNew1.Enabled = true;
              mdUtility.fMainForm.tsbNew1.Text = "Add System";
              mdUtility.fMainForm.tsbNew1.Image = (Image) BuilderRED.My.Resources.Resources.NewSystem;
              mdUtility.fMainForm.tsbNew2.Visible = true;
              mdUtility.fMainForm.tsbNew2.Text = "Add Component";
              mdUtility.fMainForm.tsbNew2.Image = (Image) BuilderRED.My.Resources.Resources.NewComponent;
              mdUtility.fMainForm.tsbDelete.Visible = true;
              mdUtility.fMainForm.tsbDelete.Text = "Delete System";
              mdUtility.fMainForm.tsbImages.Visible = true;
            }
            else
            {
              mdUtility.fMainForm.miToolsInspectSections.Enabled = false;
              mdUtility.fMainForm.tsbNew1.Visible = false;
              mdUtility.fMainForm.tsbNew2.Visible = false;
              mdUtility.fMainForm.tsbDelete.Visible = false;
              mdUtility.fMainForm.tss2.Visible = false;
              mdUtility.fMainForm.tsbImages.Visible = false;
            }
            mdUtility.fMainForm.tss2.Visible = false;
            mdUtility.fMainForm.tsbEdit.Visible = false;
            mdUtility.fMainForm.tsbSave.Visible = false;
            mdUtility.fMainForm.tsbCancel.Visible = false;
            mdUtility.fMainForm.tss3.Visible = false;
            mdUtility.fMainForm.tsbComment.Visible = false;
            break;
          }
          break;
        case 2633453491:
          if (Operators.CompareString(str, "Inspection", false) == 0)
          {
            mdUtility.fMainForm.miToolsInspectSections.Enabled = false;
            mdUtility.fMainForm.tsbNew1.Visible = false;
            mdUtility.fMainForm.tsbNew2.Visible = false;
            mdUtility.fMainForm.tsbDelete.Visible = false;
            mdUtility.fMainForm.tss2.Visible = true;
            mdUtility.fMainForm.tsbEdit.Visible = false;
            mdUtility.fMainForm.tsbCancel.Visible = true;
            mdUtility.fMainForm.tsbImages.Visible = true;
            if (!mdUtility.fMainForm.m_bUserChangedLocation)
              mdUtility.fMainForm.tsbCancel.Enabled = false;
            mdUtility.fMainForm.tsbSave.Visible = true;
            if (!mdUtility.fMainForm.m_bUserChangedLocation)
              mdUtility.fMainForm.tsbSave.Enabled = false;
            mdUtility.fMainForm.tss3.Visible = true;
            mdUtility.fMainForm.tsbComment.Visible = true;
            mdUtility.fMainForm.tsbComment.Enabled = true;
            break;
          }
          break;
        case 2878247498:
          if (Operators.CompareString(str, "Startup", false) == 0)
          {
            mdUtility.fMainForm.tsddbToggle.Enabled = false;
            mdUtility.fMainForm.tsbNew1.Enabled = false;
            mdUtility.fMainForm.tsbNew2.Enabled = false;
            mdUtility.fMainForm.tsbDelete.Enabled = false;
            mdUtility.fMainForm.tsbEdit.Enabled = false;
            mdUtility.fMainForm.tsbSave.Enabled = false;
            mdUtility.fMainForm.tsbCancel.Enabled = false;
            mdUtility.fMainForm.tsbComment.Visible = false;
            mdUtility.fMainForm.tsbImages.Visible = false;
            mdUtility.fMainForm.tsbWorkItems.Visible = false;
            break;
          }
          break;
        case 3708279898:
          if (Operators.CompareString(str, "FuncArea", false) == 0)
          {
            mdUtility.fMainForm.miToolsInspectSections.Enabled = false;
            mdUtility.fMainForm.tsbNew2.Visible = false;
            mdUtility.fMainForm.tsbNew1.Text = "Add Functional Area";
            mdUtility.fMainForm.tsbNew1.Enabled = false;
            mdUtility.fMainForm.tsbNew1.Image = (Image) BuilderRED.My.Resources.Resources.NewFunctionalArea;
            mdUtility.fMainForm.tss1.Visible = true;
            mdUtility.fMainForm.tsbSave.Visible = false;
            mdUtility.fMainForm.tsbWorkItems.Visible = false;
            mdUtility.fMainForm.tsbCancel.Visible = false;
            mdUtility.fMainForm.tsbImages.Visible = false;
            mdUtility.fMainForm.tsbDelete.Enabled = true;
            mdUtility.fMainForm.tsbDelete.Text = "Delete Functional Area";
            mdUtility.fMainForm.tsbTally.Visible = false;
            mdUtility.fMainForm.tsbComment.Visible = false;
            break;
          }
          break;
      }
    }

    public class BREDFunctionalityOrgNodeHandler : OrgNode
    {
      private UltraTreeNode _TargetNode;

      public BREDFunctionalityOrgNodeHandler(UltraTreeNode TargetNode)
      {
        this._TargetNode = TargetNode;
      }

      public override string Name
      {
        get
        {
          return this._TargetNode.Text;
        }
        set
        {
          this._TargetNode.Text = value;
        }
      }
    }
  }
}
