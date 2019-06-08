// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmMain
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using BuilderRED.My;
using ERDC.CERL.SMS.BRED.Modules.Assessments;
using ERDC.CERL.SMS.BRED.Modules.Assessments.Views;
using ERDC.CERL.SMS.Libraries.Data.DataAccess;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTree;
using Ionic.Zip;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SMS.Libraries.Utility.BredPackage.Classes.Containers;
using SMS.Libraries.Utility.BredPackage.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Forms.Layout;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace BuilderRED
{
  [DesignerGenerated]
  public class frmMain : Form
  {
    private IContainer components;
    public System.Windows.Forms.ToolTip ToolTip1;
    public MainMenu MainMenu1;
    internal MainPageView MainPageView1;
    private const int SPRINKLER_CMC_ID = 5061;
    internal int _ToggleView;
    private string _packageFileName;
    private InventoryClass? _InventoryClass;
    private ZipBredPackage _ZP;
    private Guid _siteID;
    private bool _isWorkItemTableExist;
    private Bitmap imgExpand;
    private Bitmap imgCollapse;
    private Guid _SelectedID;
    private bool m_bInspCanEdit;
    internal bool m_bInspLoaded;
    private bool m_bDistressWarn;
    internal bool m_bInspNeedToSave;
    internal bool m_bInspNew;
    internal bool m_bSampleNew;
    internal bool m_bBldgLoaded;
    private bool _bBldgNeedToSave;
    private bool _FuncAreaNeedToSave;
    internal bool m_bSectionLoaded;
    internal bool m_bSectionYearChanged;
    internal bool m_bSectionNeedToSave;
    private frmMain.ProgramMode m_pmProgramMode;
    internal bool m_bUserChangedLocation;
    internal bool m_bDDLoad;
    internal bool m_ReverseFuncArea;
    private bool m_bNonNumberEntered;
    private string m_strBldgID;
    private string m_DoesNotContain;
    private string m_strSysID;
    private string m_strLocation;
    private string m_strCompID;
    private string m_strSecID;
    private string m_strInspID;
    private string m_strSampID;
    private string m_strAreaID;
    private bool bUpdatingRow;
    private bool bInitializeRow;
    private bool m_bLoading;

    [DebuggerNonUserCode]
    protected override void Dispose(bool Disposing)
    {
      if (Disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(Disposing);
    }

    public virtual MenuItem miFileImport
    {
      get
      {
        return this._miFileImport;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.miFileImport_Click);
        MenuItem miFileImport1 = this._miFileImport;
        if (miFileImport1 != null)
          miFileImport1.Click -= eventHandler;
        this._miFileImport = value;
        MenuItem miFileImport2 = this._miFileImport;
        if (miFileImport2 == null)
          return;
        miFileImport2.Click += eventHandler;
      }
    }

    public virtual MenuItem miFileBar0 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual MenuItem miFileInspector
    {
      get
      {
        return this._miFileInspector;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.miFileInspector_Popup);
        EventHandler eventHandler2 = new EventHandler(this.miFileInspector_Click);
        MenuItem miFileInspector1 = this._miFileInspector;
        if (miFileInspector1 != null)
        {
          miFileInspector1.Popup -= eventHandler1;
          miFileInspector1.Click -= eventHandler2;
        }
        this._miFileInspector = value;
        MenuItem miFileInspector2 = this._miFileInspector;
        if (miFileInspector2 == null)
          return;
        miFileInspector2.Popup += eventHandler1;
        miFileInspector2.Click += eventHandler2;
      }
    }

    public virtual MenuItem miFileBar1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual MenuItem miFile { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual MenuItem miInventoryMode
    {
      get
      {
        return this._miInventoryMode;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.miViewToggleDisplay_Popup);
        EventHandler eventHandler2 = new EventHandler(this.miInventoryMode_Click);
        MenuItem miInventoryMode1 = this._miInventoryMode;
        if (miInventoryMode1 != null)
        {
          miInventoryMode1.Popup -= eventHandler1;
          miInventoryMode1.Click -= eventHandler2;
        }
        this._miInventoryMode = value;
        MenuItem miInventoryMode2 = this._miInventoryMode;
        if (miInventoryMode2 == null)
          return;
        miInventoryMode2.Popup += eventHandler1;
        miInventoryMode2.Click += eventHandler2;
      }
    }

    public virtual MenuItem miViewRefresh
    {
      get
      {
        return this._miViewRefresh;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.miViewRefresh_Click);
        MenuItem miViewRefresh1 = this._miViewRefresh;
        if (miViewRefresh1 != null)
          miViewRefresh1.Click -= eventHandler;
        this._miViewRefresh = value;
        MenuItem miViewRefresh2 = this._miViewRefresh;
        if (miViewRefresh2 == null)
          return;
        miViewRefresh2.Click += eventHandler;
      }
    }

    public virtual MenuItem miViewBar2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual MenuItem miUnits
    {
      get
      {
        return this._miUnits;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.miUnits_Popup);
        EventHandler eventHandler2 = new EventHandler(this.miUnits_Click);
        MenuItem miUnits1 = this._miUnits;
        if (miUnits1 != null)
        {
          miUnits1.Popup -= eventHandler1;
          miUnits1.Click -= eventHandler2;
        }
        this._miUnits = value;
        MenuItem miUnits2 = this._miUnits;
        if (miUnits2 == null)
          return;
        miUnits2.Popup += eventHandler1;
        miUnits2.Click += eventHandler2;
      }
    }

    public virtual MenuItem miView { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual MenuItem miToolsCopyInventory
    {
      get
      {
        return this._miToolsCopyInventory;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.miToolsCopyInventory_Popup);
        EventHandler eventHandler2 = new EventHandler(this.miToolsCopyInventory_Click);
        MenuItem toolsCopyInventory1 = this._miToolsCopyInventory;
        if (toolsCopyInventory1 != null)
        {
          toolsCopyInventory1.Popup -= eventHandler1;
          toolsCopyInventory1.Click -= eventHandler2;
        }
        this._miToolsCopyInventory = value;
        MenuItem toolsCopyInventory2 = this._miToolsCopyInventory;
        if (toolsCopyInventory2 == null)
          return;
        toolsCopyInventory2.Popup += eventHandler1;
        toolsCopyInventory2.Click += eventHandler2;
      }
    }

    public virtual MenuItem miToolsInspectSections
    {
      get
      {
        return this._miToolsInspectSections;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.miToolsInspectSections_Click);
        MenuItem toolsInspectSections1 = this._miToolsInspectSections;
        if (toolsInspectSections1 != null)
          toolsInspectSections1.Click -= eventHandler;
        this._miToolsInspectSections = value;
        MenuItem toolsInspectSections2 = this._miToolsInspectSections;
        if (toolsInspectSections2 == null)
          return;
        toolsInspectSections2.Click += eventHandler;
      }
    }

    public virtual MenuItem miTools { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual MenuItem miHelpContents
    {
      get
      {
        return this._miHelpContents;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.miHelpContents_Click);
        MenuItem miHelpContents1 = this._miHelpContents;
        if (miHelpContents1 != null)
          miHelpContents1.Click -= eventHandler;
        this._miHelpContents = value;
        MenuItem miHelpContents2 = this._miHelpContents;
        if (miHelpContents2 == null)
          return;
        miHelpContents2.Click += eventHandler;
      }
    }

    public virtual MenuItem miHelpBar0 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual MenuItem miHelpAbout
    {
      get
      {
        return this._miHelpAbout;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.miHelpAbout_Click);
        MenuItem miHelpAbout1 = this._miHelpAbout;
        if (miHelpAbout1 != null)
          miHelpAbout1.Click -= eventHandler;
        this._miHelpAbout = value;
        MenuItem miHelpAbout2 = this._miHelpAbout;
        if (miHelpAbout2 == null)
          return;
        miHelpAbout2.Click += eventHandler;
      }
    }

    public virtual MenuItem miHelp { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual HelpProvider HelpProvider { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblLocationInfo { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual CheckBox chkYearEstimated
    {
      get
      {
        return this._chkYearEstimated;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.chkYearEstimated_CheckStateChanged);
        CheckBox chkYearEstimated1 = this._chkYearEstimated;
        if (chkYearEstimated1 != null)
          chkYearEstimated1.CheckStateChanged -= eventHandler;
        this._chkYearEstimated = value;
        CheckBox chkYearEstimated2 = this._chkYearEstimated;
        if (chkYearEstimated2 == null)
          return;
        chkYearEstimated2.CheckStateChanged += eventHandler;
      }
    }

    public virtual System.Windows.Forms.TextBox dtPainted
    {
      get
      {
        return this._dtPainted;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Section_TextChanged);
        System.Windows.Forms.TextBox dtPainted1 = this._dtPainted;
        if (dtPainted1 != null)
          dtPainted1.TextChanged -= eventHandler;
        this._dtPainted = value;
        System.Windows.Forms.TextBox dtPainted2 = this._dtPainted;
        if (dtPainted2 == null)
          return;
        dtPainted2.TextChanged += eventHandler;
      }
    }

    public virtual CheckBox chkPainted
    {
      get
      {
        return this._chkPainted;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.chkPainted_CheckStateChanged);
        CheckBox chkPainted1 = this._chkPainted;
        if (chkPainted1 != null)
          chkPainted1.CheckStateChanged -= eventHandler;
        this._chkPainted = value;
        CheckBox chkPainted2 = this._chkPainted;
        if (chkPainted2 == null)
          return;
        chkPainted2.CheckStateChanged += eventHandler;
      }
    }

    public virtual System.Windows.Forms.TextBox txtSectionAmount
    {
      get
      {
        return this._txtSectionAmount;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        KeyEventHandler keyEventHandler = new KeyEventHandler(this.txtSectionAmount_KeyDown);
        KeyPressEventHandler pressEventHandler = new KeyPressEventHandler(this.txtSectionAmount_KeyPress);
        EventHandler eventHandler = new EventHandler(this.Section_TextChanged);
        System.Windows.Forms.TextBox txtSectionAmount1 = this._txtSectionAmount;
        if (txtSectionAmount1 != null)
        {
          txtSectionAmount1.KeyDown -= keyEventHandler;
          txtSectionAmount1.KeyPress -= pressEventHandler;
          txtSectionAmount1.TextChanged -= eventHandler;
        }
        this._txtSectionAmount = value;
        System.Windows.Forms.TextBox txtSectionAmount2 = this._txtSectionAmount;
        if (txtSectionAmount2 == null)
          return;
        txtSectionAmount2.KeyDown += keyEventHandler;
        txtSectionAmount2.KeyPress += pressEventHandler;
        txtSectionAmount2.TextChanged += eventHandler;
      }
    }

    public virtual System.Windows.Forms.TextBox txtSectionYearBuilt
    {
      get
      {
        return this._txtSectionYearBuilt;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.txtSectionYearBuilt_TextChanged);
        EventHandler eventHandler2 = new EventHandler(this.txtSectionYearBuilt_Leave);
        System.Windows.Forms.TextBox sectionYearBuilt1 = this._txtSectionYearBuilt;
        if (sectionYearBuilt1 != null)
        {
          sectionYearBuilt1.TextChanged -= eventHandler1;
          sectionYearBuilt1.Leave -= eventHandler2;
        }
        this._txtSectionYearBuilt = value;
        System.Windows.Forms.TextBox sectionYearBuilt2 = this._txtSectionYearBuilt;
        if (sectionYearBuilt2 == null)
          return;
        sectionYearBuilt2.TextChanged += eventHandler1;
        sectionYearBuilt2.Leave += eventHandler2;
      }
    }

    public virtual System.Windows.Forms.Label lblPaintType { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblDatePainted { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblPainted { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblSectionAmount { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblSectionYearBuilt { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblComponentType { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblMaterial { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblSectionName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.TextBox txtBldgArea
    {
      get
      {
        return this._txtBldgArea;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Building_TextChanged);
        System.Windows.Forms.TextBox txtBldgArea1 = this._txtBldgArea;
        if (txtBldgArea1 != null)
          txtBldgArea1.TextChanged -= eventHandler;
        this._txtBldgArea = value;
        System.Windows.Forms.TextBox txtBldgArea2 = this._txtBldgArea;
        if (txtBldgArea2 == null)
          return;
        txtBldgArea2.TextChanged += eventHandler;
      }
    }

    public virtual GroupBox frmBuildingPOC { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.TextBox txtPOCEmail
    {
      get
      {
        return this._txtPOCEmail;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Building_TextChanged);
        System.Windows.Forms.TextBox txtPocEmail1 = this._txtPOCEmail;
        if (txtPocEmail1 != null)
          txtPocEmail1.TextChanged -= eventHandler;
        this._txtPOCEmail = value;
        System.Windows.Forms.TextBox txtPocEmail2 = this._txtPOCEmail;
        if (txtPocEmail2 == null)
          return;
        txtPocEmail2.TextChanged += eventHandler;
      }
    }

    public virtual System.Windows.Forms.TextBox txtPOCPhone
    {
      get
      {
        return this._txtPOCPhone;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Building_TextChanged);
        System.Windows.Forms.TextBox txtPocPhone1 = this._txtPOCPhone;
        if (txtPocPhone1 != null)
          txtPocPhone1.TextChanged -= eventHandler;
        this._txtPOCPhone = value;
        System.Windows.Forms.TextBox txtPocPhone2 = this._txtPOCPhone;
        if (txtPocPhone2 == null)
          return;
        txtPocPhone2.TextChanged += eventHandler;
      }
    }

    public virtual System.Windows.Forms.TextBox txtPOC
    {
      get
      {
        return this._txtPOC;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Building_TextChanged);
        System.Windows.Forms.TextBox txtPoc1 = this._txtPOC;
        if (txtPoc1 != null)
          txtPoc1.TextChanged -= eventHandler;
        this._txtPOC = value;
        System.Windows.Forms.TextBox txtPoc2 = this._txtPOC;
        if (txtPoc2 == null)
          return;
        txtPoc2.TextChanged += eventHandler;
      }
    }

    public virtual System.Windows.Forms.Label lblPOCEmail { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblPOCPhone { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblPOC { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual GroupBox frmBuildingAddress { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.TextBox txtZipCode
    {
      get
      {
        return this._txtZipCode;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Building_TextChanged);
        System.Windows.Forms.TextBox txtZipCode1 = this._txtZipCode;
        if (txtZipCode1 != null)
          txtZipCode1.TextChanged -= eventHandler;
        this._txtZipCode = value;
        System.Windows.Forms.TextBox txtZipCode2 = this._txtZipCode;
        if (txtZipCode2 == null)
          return;
        txtZipCode2.TextChanged += eventHandler;
      }
    }

    public virtual System.Windows.Forms.TextBox txtState
    {
      get
      {
        return this._txtState;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Building_TextChanged);
        System.Windows.Forms.TextBox txtState1 = this._txtState;
        if (txtState1 != null)
          txtState1.TextChanged -= eventHandler;
        this._txtState = value;
        System.Windows.Forms.TextBox txtState2 = this._txtState;
        if (txtState2 == null)
          return;
        txtState2.TextChanged += eventHandler;
      }
    }

    public virtual System.Windows.Forms.TextBox txtCity
    {
      get
      {
        return this._txtCity;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Building_TextChanged);
        System.Windows.Forms.TextBox txtCity1 = this._txtCity;
        if (txtCity1 != null)
          txtCity1.TextChanged -= eventHandler;
        this._txtCity = value;
        System.Windows.Forms.TextBox txtCity2 = this._txtCity;
        if (txtCity2 == null)
          return;
        txtCity2.TextChanged += eventHandler;
      }
    }

    public virtual System.Windows.Forms.TextBox txtAddress
    {
      get
      {
        return this._txtAddress;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Building_TextChanged);
        System.Windows.Forms.TextBox txtAddress1 = this._txtAddress;
        if (txtAddress1 != null)
          txtAddress1.TextChanged -= eventHandler;
        this._txtAddress = value;
        System.Windows.Forms.TextBox txtAddress2 = this._txtAddress;
        if (txtAddress2 == null)
          return;
        txtAddress2.TextChanged += eventHandler;
      }
    }

    public virtual System.Windows.Forms.Label lblZipCode { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblState { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblCity { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblAddress { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.TextBox txtNoFloors
    {
      get
      {
        return this._txtNoFloors;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Building_TextChanged);
        System.Windows.Forms.TextBox txtNoFloors1 = this._txtNoFloors;
        if (txtNoFloors1 != null)
          txtNoFloors1.TextChanged -= eventHandler;
        this._txtNoFloors = value;
        System.Windows.Forms.TextBox txtNoFloors2 = this._txtNoFloors;
        if (txtNoFloors2 == null)
          return;
        txtNoFloors2.TextChanged += eventHandler;
      }
    }

    public virtual System.Windows.Forms.TextBox txtYearBuilt
    {
      get
      {
        return this._txtYearBuilt;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Building_TextChanged);
        System.Windows.Forms.TextBox txtYearBuilt1 = this._txtYearBuilt;
        if (txtYearBuilt1 != null)
          txtYearBuilt1.TextChanged -= eventHandler;
        this._txtYearBuilt = value;
        System.Windows.Forms.TextBox txtYearBuilt2 = this._txtYearBuilt;
        if (txtYearBuilt2 == null)
          return;
        txtYearBuilt2.TextChanged += eventHandler;
      }
    }

    public virtual System.Windows.Forms.TextBox txtBuildingName
    {
      get
      {
        return this._txtBuildingName;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Building_TextChanged);
        System.Windows.Forms.TextBox txtBuildingName1 = this._txtBuildingName;
        if (txtBuildingName1 != null)
          txtBuildingName1.TextChanged -= eventHandler;
        this._txtBuildingName = value;
        System.Windows.Forms.TextBox txtBuildingName2 = this._txtBuildingName;
        if (txtBuildingName2 == null)
          return;
        txtBuildingName2.TextChanged += eventHandler;
      }
    }

    public virtual System.Windows.Forms.TextBox txtBuildingNumber
    {
      get
      {
        return this._txtBuildingNumber;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Building_TextChanged);
        System.Windows.Forms.TextBox txtBuildingNumber1 = this._txtBuildingNumber;
        if (txtBuildingNumber1 != null)
          txtBuildingNumber1.TextChanged -= eventHandler;
        this._txtBuildingNumber = value;
        System.Windows.Forms.TextBox txtBuildingNumber2 = this._txtBuildingNumber;
        if (txtBuildingNumber2 == null)
          return;
        txtBuildingNumber2.TextChanged += eventHandler;
      }
    }

    public virtual System.Windows.Forms.Label lblBldgSF { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblBldgArea { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblNoFloors { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblYearBuilt { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblConstructionType { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblCategoryCode { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblBuildingID { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Panel frmLocation { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Button cmdSampComment
    {
      get
      {
        return this._cmdSampComment;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdSampComment_Click);
        Button cmdSampComment1 = this._cmdSampComment;
        if (cmdSampComment1 != null)
          cmdSampComment1.Click -= eventHandler;
        this._cmdSampComment = value;
        Button cmdSampComment2 = this._cmdSampComment;
        if (cmdSampComment2 == null)
          return;
        cmdSampComment2.Click += eventHandler;
      }
    }

    public virtual Button cmdEditSample
    {
      get
      {
        return this._cmdEditSample;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdEditSample_Click);
        Button cmdEditSample1 = this._cmdEditSample;
        if (cmdEditSample1 != null)
          cmdEditSample1.Click -= eventHandler;
        this._cmdEditSample = value;
        Button cmdEditSample2 = this._cmdEditSample;
        if (cmdEditSample2 == null)
          return;
        cmdEditSample2.Click += eventHandler;
      }
    }

    public virtual Button cmdDeleteSample
    {
      get
      {
        return this._cmdDeleteSample;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdDeleteSample_Click);
        Button cmdDeleteSample1 = this._cmdDeleteSample;
        if (cmdDeleteSample1 != null)
          cmdDeleteSample1.Click -= eventHandler;
        this._cmdDeleteSample = value;
        Button cmdDeleteSample2 = this._cmdDeleteSample;
        if (cmdDeleteSample2 == null)
          return;
        cmdDeleteSample2.Click += eventHandler;
      }
    }

    public virtual CheckBox chkSampNonRep
    {
      get
      {
        return this._chkSampNonRep;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.chkSampNonRep_CheckStateChanged);
        CheckBox chkSampNonRep1 = this._chkSampNonRep;
        if (chkSampNonRep1 != null)
          chkSampNonRep1.CheckStateChanged -= eventHandler;
        this._chkSampNonRep = value;
        CheckBox chkSampNonRep2 = this._chkSampNonRep;
        if (chkSampNonRep2 == null)
          return;
        chkSampNonRep2.CheckStateChanged += eventHandler;
      }
    }

    public virtual System.Windows.Forms.TextBox txtSQuantity
    {
      get
      {
        return this._txtSQuantity;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.txtSQuantity_Leave);
        EventHandler eventHandler2 = new EventHandler(this.txtSQuantity_TextChanged);
        CancelEventHandler cancelEventHandler = new CancelEventHandler(this.txtSQuantity_Validating);
        System.Windows.Forms.TextBox txtSquantity1 = this._txtSQuantity;
        if (txtSquantity1 != null)
        {
          txtSquantity1.Leave -= eventHandler1;
          txtSquantity1.TextChanged -= eventHandler2;
          txtSquantity1.Validating -= cancelEventHandler;
        }
        this._txtSQuantity = value;
        System.Windows.Forms.TextBox txtSquantity2 = this._txtSQuantity;
        if (txtSquantity2 == null)
          return;
        txtSquantity2.Leave += eventHandler1;
        txtSquantity2.TextChanged += eventHandler2;
        txtSquantity2.Validating += cancelEventHandler;
      }
    }

    public virtual Button cmdNewSample
    {
      get
      {
        return this._cmdNewSample;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdNewSample_Click_1);
        Button cmdNewSample1 = this._cmdNewSample;
        if (cmdNewSample1 != null)
          cmdNewSample1.Click -= eventHandler;
        this._cmdNewSample = value;
        Button cmdNewSample2 = this._cmdNewSample;
        if (cmdNewSample2 == null)
          return;
        cmdNewSample2.Click += eventHandler;
      }
    }

    public virtual System.Windows.Forms.Label lblSampQty { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblPCInsp { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblQty { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblLocation { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual GroupBox frmMethod { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual RadioButton optMethod_0
    {
      get
      {
        return this._optMethod_0;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optMethod_CheckedChanged);
        RadioButton optMethod0_1 = this._optMethod_0;
        if (optMethod0_1 != null)
          optMethod0_1.CheckedChanged -= eventHandler;
        this._optMethod_0 = value;
        RadioButton optMethod0_2 = this._optMethod_0;
        if (optMethod0_2 == null)
          return;
        optMethod0_2.CheckedChanged += eventHandler;
      }
    }

    public virtual RadioButton optMethod_1
    {
      get
      {
        return this._optMethod_1;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optMethod_CheckedChanged);
        RadioButton optMethod1_1 = this._optMethod_1;
        if (optMethod1_1 != null)
          optMethod1_1.CheckedChanged -= eventHandler;
        this._optMethod_1 = value;
        RadioButton optMethod1_2 = this._optMethod_1;
        if (optMethod1_2 == null)
          return;
        optMethod1_2.CheckedChanged += eventHandler;
      }
    }

    public virtual GroupBox frmRatingType { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual RadioButton optRatingType_1
    {
      get
      {
        return this._optRatingType_1;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.optRatingType_CheckedChanged);
        EventHandler eventHandler2 = new EventHandler(this.optRatingType_Enter);
        RadioButton optRatingType1_1 = this._optRatingType_1;
        if (optRatingType1_1 != null)
        {
          optRatingType1_1.CheckedChanged -= eventHandler1;
          optRatingType1_1.Enter -= eventHandler2;
        }
        this._optRatingType_1 = value;
        RadioButton optRatingType1_2 = this._optRatingType_1;
        if (optRatingType1_2 == null)
          return;
        optRatingType1_2.CheckedChanged += eventHandler1;
        optRatingType1_2.Enter += eventHandler2;
      }
    }

    public virtual RadioButton optRatingType_2
    {
      get
      {
        return this._optRatingType_2;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.optRatingType_CheckedChanged);
        EventHandler eventHandler2 = new EventHandler(this.optRatingType_Enter);
        RadioButton optRatingType2_1 = this._optRatingType_2;
        if (optRatingType2_1 != null)
        {
          optRatingType2_1.CheckedChanged -= eventHandler1;
          optRatingType2_1.Enter -= eventHandler2;
        }
        this._optRatingType_2 = value;
        RadioButton optRatingType2_2 = this._optRatingType_2;
        if (optRatingType2_2 == null)
          return;
        optRatingType2_2.CheckedChanged += eventHandler1;
        optRatingType2_2.Enter += eventHandler2;
      }
    }

    public virtual GroupBox frmDistressSurvey
    {
      get
      {
        return this._frmDistressSurvey;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.frmDistressSurvey_Enter);
        GroupBox frmDistressSurvey1 = this._frmDistressSurvey;
        if (frmDistressSurvey1 != null)
          frmDistressSurvey1.Enter -= eventHandler;
        this._frmDistressSurvey = value;
        GroupBox frmDistressSurvey2 = this._frmDistressSurvey;
        if (frmDistressSurvey2 == null)
          return;
        frmDistressSurvey2.Enter += eventHandler;
      }
    }

    public virtual CheckBox chkPaintDefFree
    {
      get
      {
        return this._chkPaintDefFree;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.chkPaintDefFree_CheckStateChanged);
        CheckBox chkPaintDefFree1 = this._chkPaintDefFree;
        if (chkPaintDefFree1 != null)
          chkPaintDefFree1.CheckStateChanged -= eventHandler;
        this._chkPaintDefFree = value;
        CheckBox chkPaintDefFree2 = this._chkPaintDefFree;
        if (chkPaintDefFree2 == null)
          return;
        chkPaintDefFree2.CheckStateChanged += eventHandler;
      }
    }

    public virtual CheckBox chkDefFree
    {
      get
      {
        return this._chkDefFree;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.chkDefFree_CheckStateChanged);
        CheckBox chkDefFree1 = this._chkDefFree;
        if (chkDefFree1 != null)
          chkDefFree1.CheckStateChanged -= eventHandler;
        this._chkDefFree = value;
        CheckBox chkDefFree2 = this._chkDefFree;
        if (chkDefFree2 == null)
          return;
        chkDefFree2.CheckStateChanged += eventHandler;
      }
    }

    public virtual System.Windows.Forms.Label lblSecQtyUM { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblSecQty { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label txtComponentType { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblCmponentType { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label txtMaterialCategory { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label txtComponent { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblMaterialCategory { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblComponent { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblOpeningMessage { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblComponentText { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblSystemText { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual System.Windows.Forms.ComboBox cboConstructionType
    {
      get
      {
        return this._cboConstructionType;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.Building_SelectedIndexChanged);
        EventHandler eventHandler2 = new EventHandler(this.BuildingReadonly_Enter);
        System.Windows.Forms.ComboBox constructionType1 = this._cboConstructionType;
        if (constructionType1 != null)
        {
          constructionType1.SelectedIndexChanged -= eventHandler1;
          constructionType1.Enter -= eventHandler2;
        }
        this._cboConstructionType = value;
        System.Windows.Forms.ComboBox constructionType2 = this._cboConstructionType;
        if (constructionType2 == null)
          return;
        constructionType2.SelectedIndexChanged += eventHandler1;
        constructionType2.Enter += eventHandler2;
      }
    }

    internal virtual System.Windows.Forms.ComboBox cboSectionMaterial
    {
      get
      {
        return this._cboSectionMaterial;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.cboSectionMaterial_SelectedIndexChanged);
        EventHandler eventHandler2 = new EventHandler(this.SectionReadonly_Enter);
        System.Windows.Forms.ComboBox cboSectionMaterial1 = this._cboSectionMaterial;
        if (cboSectionMaterial1 != null)
        {
          cboSectionMaterial1.SelectedIndexChanged -= eventHandler1;
          cboSectionMaterial1.Enter -= eventHandler2;
        }
        this._cboSectionMaterial = value;
        System.Windows.Forms.ComboBox cboSectionMaterial2 = this._cboSectionMaterial;
        if (cboSectionMaterial2 == null)
          return;
        cboSectionMaterial2.SelectedIndexChanged += eventHandler1;
        cboSectionMaterial2.Enter += eventHandler2;
      }
    }

    internal virtual System.Windows.Forms.ComboBox cboSectionComponentType
    {
      get
      {
        return this._cboSectionComponentType;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.cboSectionComponentType_SelectedIndexChanged);
        EventHandler eventHandler2 = new EventHandler(this.SectionReadonly_Enter);
        System.Windows.Forms.ComboBox sectionComponentType1 = this._cboSectionComponentType;
        if (sectionComponentType1 != null)
        {
          sectionComponentType1.SelectedIndexChanged -= eventHandler1;
          sectionComponentType1.Enter -= eventHandler2;
        }
        this._cboSectionComponentType = value;
        System.Windows.Forms.ComboBox sectionComponentType2 = this._cboSectionComponentType;
        if (sectionComponentType2 == null)
          return;
        sectionComponentType2.SelectedIndexChanged += eventHandler1;
        sectionComponentType2.Enter += eventHandler2;
      }
    }

    internal virtual System.Windows.Forms.ComboBox cboSectionPaintType { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual System.Windows.Forms.ComboBox cboSectionName
    {
      get
      {
        return this._cboSectionName;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.cboSectionName_SelectedIndexChanged);
        EventHandler eventHandler2 = new EventHandler(this.SectionChanged);
        EventHandler eventHandler3 = new EventHandler(this.SectionReadonly_Enter);
        System.Windows.Forms.ComboBox cboSectionName1 = this._cboSectionName;
        if (cboSectionName1 != null)
        {
          cboSectionName1.SelectedIndexChanged -= eventHandler1;
          cboSectionName1.TextChanged -= eventHandler2;
          cboSectionName1.Enter -= eventHandler3;
        }
        this._cboSectionName = value;
        System.Windows.Forms.ComboBox cboSectionName2 = this._cboSectionName;
        if (cboSectionName2 == null)
          return;
        cboSectionName2.SelectedIndexChanged += eventHandler1;
        cboSectionName2.TextChanged += eventHandler2;
        cboSectionName2.Enter += eventHandler3;
      }
    }

    internal virtual System.Windows.Forms.ComboBox cboCatCode
    {
      get
      {
        return this._cboCatCode;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.Building_SelectedIndexChanged);
        EventHandler eventHandler2 = new EventHandler(this.cboCatCodeSelectedIndexChanged);
        EventHandler eventHandler3 = new EventHandler(this.BuildingReadonly_Enter);
        System.Windows.Forms.ComboBox cboCatCode1 = this._cboCatCode;
        if (cboCatCode1 != null)
        {
          cboCatCode1.SelectedIndexChanged -= eventHandler1;
          cboCatCode1.SelectedIndexChanged -= eventHandler2;
          cboCatCode1.Enter -= eventHandler3;
        }
        this._cboCatCode = value;
        System.Windows.Forms.ComboBox cboCatCode2 = this._cboCatCode;
        if (cboCatCode2 == null)
          return;
        cboCatCode2.SelectedIndexChanged += eventHandler1;
        cboCatCode2.SelectedIndexChanged += eventHandler2;
        cboCatCode2.Enter += eventHandler3;
      }
    }

    internal virtual System.Windows.Forms.ComboBox cboLocation
    {
      get
      {
        return this._cboLocation;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cboLocation_SelectedIndexChanged);
        System.Windows.Forms.ComboBox cboLocation1 = this._cboLocation;
        if (cboLocation1 != null)
          cboLocation1.SelectedIndexChanged -= eventHandler;
        this._cboLocation = value;
        System.Windows.Forms.ComboBox cboLocation2 = this._cboLocation;
        if (cboLocation2 == null)
          return;
        cboLocation2.SelectedIndexChanged += eventHandler;
      }
    }

    internal virtual System.Windows.Forms.Timer Timer1
    {
      get
      {
        return this._Timer1;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Timer1_Tick);
        System.Windows.Forms.Timer timer1_1 = this._Timer1;
        if (timer1_1 != null)
          timer1_1.Tick -= eventHandler;
        this._Timer1 = value;
        System.Windows.Forms.Timer timer1_2 = this._Timer1;
        if (timer1_2 == null)
          return;
        timer1_2.Tick += eventHandler;
      }
    }

    public virtual System.Windows.Forms.Label lblNoInspection { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual System.Windows.Forms.Label lblPCInspValue { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual System.Windows.Forms.Label lblSecQtyValue
    {
      get
      {
        return this._lblSecQtyValue;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.lblSecQtyValue_TextChanged);
        System.Windows.Forms.Label lblSecQtyValue1 = this._lblSecQtyValue;
        if (lblSecQtyValue1 != null)
          lblSecQtyValue1.TextChanged -= eventHandler;
        this._lblSecQtyValue = value;
        System.Windows.Forms.Label lblSecQtyValue2 = this._lblSecQtyValue;
        if (lblSecQtyValue2 == null)
          return;
        lblSecQtyValue2.TextChanged += eventHandler;
      }
    }

    public virtual MenuItem miViewBar1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual CheckBox chkSampPainted
    {
      get
      {
        return this._chkSampPainted;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.chkSampPainted_CheckStateChanged);
        CheckBox chkSampPainted1 = this._chkSampPainted;
        if (chkSampPainted1 != null)
          chkSampPainted1.CheckStateChanged -= eventHandler;
        this._chkSampPainted = value;
        CheckBox chkSampPainted2 = this._chkSampPainted;
        if (chkSampPainted2 == null)
          return;
        chkSampPainted2.CheckStateChanged += eventHandler;
      }
    }

    internal virtual MenuItem miToolsSep1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual MenuItem miToolsOptions { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual MenuItem miOpenLastFile
    {
      get
      {
        return this._miOpenLastFile;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.miOpenLastFile_Click);
        MenuItem miOpenLastFile1 = this._miOpenLastFile;
        if (miOpenLastFile1 != null)
          miOpenLastFile1.Click -= eventHandler;
        this._miOpenLastFile = value;
        MenuItem miOpenLastFile2 = this._miOpenLastFile;
        if (miOpenLastFile2 == null)
          return;
        miOpenLastFile2.Click += eventHandler;
      }
    }

    internal virtual System.Windows.Forms.Label lblEnergyAuditRequired { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual CheckBox chkEnergyAuditRequired
    {
      get
      {
        return this._chkEnergyAuditRequired;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.SectionChanged);
        EventHandler eventHandler2 = new EventHandler(this.chkEnergyEneryAuditRequired_Click);
        CheckBox energyAuditRequired1 = this._chkEnergyAuditRequired;
        if (energyAuditRequired1 != null)
        {
          energyAuditRequired1.CheckedChanged -= eventHandler1;
          energyAuditRequired1.CheckedChanged -= eventHandler2;
        }
        this._chkEnergyAuditRequired = value;
        CheckBox energyAuditRequired2 = this._chkEnergyAuditRequired;
        if (energyAuditRequired2 == null)
          return;
        energyAuditRequired2.CheckedChanged += eventHandler1;
        energyAuditRequired2.CheckedChanged += eventHandler2;
      }
    }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmMain));
      this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.cmdIncrease = new Button();
      this.cmdDecrease = new Button();
      this.cmdCalc = new Button();
      this.cmdSampComment = new Button();
      this.cmdEditSample = new Button();
      this.cmdDeleteSample = new Button();
      this.cmdNewSample = new Button();
      this.cmdNewInspection = new Button();
      this.cmdCopyInspection = new Button();
      this.cmdDeleteInspection = new Button();
      this.optCompRating_3 = new RadioButton();
      this.optCompRating_1 = new RadioButton();
      this.optCompRating_2 = new RadioButton();
      this.optCompRating_4 = new RadioButton();
      this.optCompRating_7 = new RadioButton();
      this.optCompRating_8 = new RadioButton();
      this.optCompRating_6 = new RadioButton();
      this.optCompRating_5 = new RadioButton();
      this.optCompRating_9 = new RadioButton();
      this.optPaintRating_2 = new RadioButton();
      this.optPaintRating_5 = new RadioButton();
      this.optPaintRating_7 = new RadioButton();
      this.optPaintRating_4 = new RadioButton();
      this.optPaintRating_3 = new RadioButton();
      this.optPaintRating_8 = new RadioButton();
      this.optPaintRating_6 = new RadioButton();
      this.optPaintRating_1 = new RadioButton();
      this.optPaintRating_9 = new RadioButton();
      this.miViewBar1 = new MenuItem();
      this.MainMenu1 = new MainMenu(this.components);
      this.miFile = new MenuItem();
      this.miFileImport = new MenuItem();
      this.miFileBar0 = new MenuItem();
      this.miFileInspector = new MenuItem();
      this.miFileBar1 = new MenuItem();
      this.miFileClose = new MenuItem();
      this.miView = new MenuItem();
      this.miInventoryMode = new MenuItem();
      this.miInspectionMode = new MenuItem();
      this.miFunctionalityMode = new MenuItem();
      this.miViewRefresh = new MenuItem();
      this.miViewBar2 = new MenuItem();
      this.miUnits = new MenuItem();
      this.miTools = new MenuItem();
      this.miToolsCopyInventory = new MenuItem();
      this.miToolsInspectSections = new MenuItem();
      this.miToolsSep1 = new MenuItem();
      this.miToolsReports = new MenuItem();
      this.miToolsOptions = new MenuItem();
      this.miOpenLastFile = new MenuItem();
      this.miHelp = new MenuItem();
      this.miHelpContents = new MenuItem();
      this.miHelpBar0 = new MenuItem();
      this.miHelpAbout = new MenuItem();
      this.HelpProvider = new HelpProvider();
      this.tvInventory = new UltraTree();
      this.tvInspection = new UltraTree();
      this.txtSectionAmount = new System.Windows.Forms.TextBox();
      this.txtSectionYearBuilt = new System.Windows.Forms.TextBox();
      this.txtBldgArea = new System.Windows.Forms.TextBox();
      this.txtPOCEmail = new System.Windows.Forms.TextBox();
      this.txtPOCPhone = new System.Windows.Forms.TextBox();
      this.txtPOC = new System.Windows.Forms.TextBox();
      this.txtZipCode = new System.Windows.Forms.TextBox();
      this.txtState = new System.Windows.Forms.TextBox();
      this.txtCity = new System.Windows.Forms.TextBox();
      this.txtAddress = new System.Windows.Forms.TextBox();
      this.txtNoFloors = new System.Windows.Forms.TextBox();
      this.txtYearBuilt = new System.Windows.Forms.TextBox();
      this.txtBuildingName = new System.Windows.Forms.TextBox();
      this.frmLocation = new Panel();
      this.tlpLocation = new TableLayoutPanel();
      this.cboLocation = new System.Windows.Forms.ComboBox();
      this.chkSampPainted = new CheckBox();
      this.lblQty = new System.Windows.Forms.Label();
      this.lblPCInspValue = new System.Windows.Forms.Label();
      this.lblPCInsp = new System.Windows.Forms.Label();
      this.lblSampQty = new System.Windows.Forms.Label();
      this.lblLocation = new System.Windows.Forms.Label();
      this.chkSampNonRep = new CheckBox();
      this.txtSQuantity = new System.Windows.Forms.TextBox();
      this.flpLocation = new FlowLayoutPanel();
      this.txtComponent = new System.Windows.Forms.Label();
      this.frmDistressSurvey = new GroupBox();
      this.TableLayoutPanel1 = new TableLayoutPanel();
      this.ugSubcomponents = new UltraGrid();
      this.flpSubCompData = new FlowLayoutPanel();
      this.chkDefFree = new CheckBox();
      this.chkPaintDefFree = new CheckBox();
      this.frmDirectRating = new GroupBox();
      this.tlpRatings = new TableLayoutPanel();
      this.frmDirectComponent = new GroupBox();
      this.tlpDirectRating = new TableLayoutPanel();
      this.frmDirectPaint = new GroupBox();
      this.tlpPaintRating = new TableLayoutPanel();
      this.cboInspectionDates = new System.Windows.Forms.ComboBox();
      this.lblEnergyAuditRequired = new System.Windows.Forms.Label();
      this.chkEnergyAuditRequired = new CheckBox();
      this.cboSectionName = new System.Windows.Forms.ComboBox();
      this.cboSectionPaintType = new System.Windows.Forms.ComboBox();
      this.cboSectionComponentType = new System.Windows.Forms.ComboBox();
      this.cboSectionMaterial = new System.Windows.Forms.ComboBox();
      this.chkYearEstimated = new CheckBox();
      this.dtPainted = new System.Windows.Forms.TextBox();
      this.chkPainted = new CheckBox();
      this.lblPaintType = new System.Windows.Forms.Label();
      this.lblDatePainted = new System.Windows.Forms.Label();
      this.lblPainted = new System.Windows.Forms.Label();
      this.lblSectionAmount = new System.Windows.Forms.Label();
      this.lblSectionYearBuilt = new System.Windows.Forms.Label();
      this.lblComponentType = new System.Windows.Forms.Label();
      this.lblMaterial = new System.Windows.Forms.Label();
      this.lblSectionName = new System.Windows.Forms.Label();
      this.lblLocationInfo = new System.Windows.Forms.Label();
      this.cboCatCode = new System.Windows.Forms.ComboBox();
      this.cboConstructionType = new System.Windows.Forms.ComboBox();
      this.frmBuildingPOC = new GroupBox();
      this.tlpBuildingPOC = new TableLayoutPanel();
      this.lblPOC = new System.Windows.Forms.Label();
      this.lblPOCEmail = new System.Windows.Forms.Label();
      this.lblPOCPhone = new System.Windows.Forms.Label();
      this.frmBuildingAddress = new GroupBox();
      this.tlpBuildingAddress = new TableLayoutPanel();
      this.lblZipCode = new System.Windows.Forms.Label();
      this.lblState = new System.Windows.Forms.Label();
      this.lblCity = new System.Windows.Forms.Label();
      this.lblAddress = new System.Windows.Forms.Label();
      this.txtBuildingNumber = new System.Windows.Forms.TextBox();
      this.lblBldgSF = new System.Windows.Forms.Label();
      this.lblBldgArea = new System.Windows.Forms.Label();
      this.lblNoFloors = new System.Windows.Forms.Label();
      this.lblYearBuilt = new System.Windows.Forms.Label();
      this.lblConstructionType = new System.Windows.Forms.Label();
      this.lblCategoryCode = new System.Windows.Forms.Label();
      this.lblBuildingID = new System.Windows.Forms.Label();
      this.lblSecQtyValue = new System.Windows.Forms.Label();
      this.frmMethod = new GroupBox();
      this.flpInspMethod = new FlowLayoutPanel();
      this.optMethod_0 = new RadioButton();
      this.optMethod_1 = new RadioButton();
      this.frmRatingType = new GroupBox();
      this.flpInspType = new FlowLayoutPanel();
      this.optRatingType_1 = new RadioButton();
      this.optRatingType_2 = new RadioButton();
      this.optRatingType_3 = new RadioButton();
      this.lblSecQtyUM = new System.Windows.Forms.Label();
      this.lblSecQty = new System.Windows.Forms.Label();
      this.txtComponentType = new System.Windows.Forms.Label();
      this.lblCmponentType = new System.Windows.Forms.Label();
      this.txtMaterialCategory = new System.Windows.Forms.Label();
      this.lblMaterialCategory = new System.Windows.Forms.Label();
      this.lblComponent = new System.Windows.Forms.Label();
      this.lblNoInspection = new System.Windows.Forms.Label();
      this.lblOpeningMessage = new System.Windows.Forms.Label();
      this.lblComponentText = new System.Windows.Forms.Label();
      this.lblSystemText = new System.Windows.Forms.Label();
      this.Timer1 = new System.Windows.Forms.Timer(this.components);
      this.pnlLocationInfo = new Panel();
      this.pnlComponentInfo = new Panel();
      this.pnlSystemInfo = new Panel();
      this.pnlStartup = new Panel();
      this.pnlBuildingInsp = new Panel();
      this.lnkFunctionality = new LinkLabel();
      this.pnlBuildingInfo = new Panel();
      this.tlpBuildingInfo = new TableLayoutPanel();
      this.lblAlternateID = new System.Windows.Forms.Label();
      this.lblAlternateIDSource = new System.Windows.Forms.Label();
      this.txtAlternateID = new System.Windows.Forms.TextBox();
      this.txtAlternateIDSource = new System.Windows.Forms.TextBox();
      this.lblYearRenovated = new System.Windows.Forms.Label();
      this.txtYearRenovated = new System.Windows.Forms.TextBox();
      this.txtDoesNotContain = new System.Windows.Forms.Label();
      this.lblUnableToInspect = new System.Windows.Forms.Label();
      this.rcbInspIssue = new System.Windows.Forms.ComboBox();
      this.btnDoesNotContain = new Button();
      this.pnlSectionInfo = new Panel();
      this.tbSection = new RadPageView();
      this.tpSection = new RadPageViewPage();
      this.tlpSectionInfo = new TableLayoutPanel();
      this.FlowLayoutPanel1 = new FlowLayoutPanel();
      this.lblUOM = new System.Windows.Forms.Label();
      this.lblFunctionalArea = new System.Windows.Forms.Label();
      this.cboFunctionalArea = new System.Windows.Forms.ComboBox();
      this.cboSectionStatus = new System.Windows.Forms.ComboBox();
      this.tpDetails = new RadPageViewPage();
      this.rgDetails = new RadGridView();
      this.RadCommandBar1 = new RadCommandBar();
      this.CommandBarRowElement1 = new CommandBarRowElement();
      this.CommandBarStripElement1 = new CommandBarStripElement();
      this.cmdNewDetail = new CommandBarButton();
      this.pnlInspectionInfo = new Panel();
      this.tlpInspectionInfo = new TableLayoutPanel();
      this.lblInspectionDate = new System.Windows.Forms.Label();
      this.tlpSecQty = new TableLayoutPanel();
      this.flpInspectionDate = new FlowLayoutPanel();
      this.pnlAssessments = new Panel();
      this.pnlTaskList = new Panel();
      this.ShowTasksButton = new Button();
      this.alertFormsUpdate = new RadDesktopAlert(this.components);
      this.alertFormsHide = new RadMenuItem();
      this.pnlDetails = new Panel();
      this.pnlFuncAssessment = new Panel();
      this.pnlFuncArea = new Panel();
      this.lblFuncArea = new System.Windows.Forms.Label();
      this.pnlFunctionality = new Panel();
      this.pnlTrees = new Panel();
      this.tvFunctionality = new UltraTree();
      this.SplitContainerMain = new SplitContainer();
      this.ssStatusStrip = new StatusStrip();
      this.tsslStatus = new ToolStripStatusLabel();
      this.tsslInspector = new ToolStripStatusLabel();
      this.tsslCurrentDate = new ToolStripStatusLabel();
      this.tsslCurrentTime = new ToolStripStatusLabel();
      this.tsToolbar = new ToolStrip();
      this.tsddbToggle = new ToolStripDropDownButton();
      this.InventoryToolStripMenuItem = new ToolStripMenuItem();
      this.InspectionsToolStripMenuItem = new ToolStripMenuItem();
      this.FunctionalityToolStripMenuItem = new ToolStripMenuItem();
      this.tss1 = new ToolStripSeparator();
      this.tsbTally = new ToolStripButton();
      this.tsbNew1 = new ToolStripButton();
      this.tsbNew2 = new ToolStripButton();
      this.tsbDelete = new ToolStripButton();
      this.tss2 = new ToolStripSeparator();
      this.tsbEdit = new ToolStripButton();
      this.tsbSave = new ToolStripButton();
      this.tsbCancel = new ToolStripButton();
      this.tss3 = new ToolStripSeparator();
      this.tsbComment = new ToolStripButton();
      this.tsbImages = new ToolStripButton();
      this.tsbWorkItems = new ToolStripButton();
      this.lblSectionStatus = new System.Windows.Forms.Label();
      this.ElementHost1 = new ElementHost();
      this.MainPageView1 = new MainPageView();
      ((ISupportInitialize) this.tvInventory).BeginInit();
      ((ISupportInitialize) this.tvInspection).BeginInit();
      this.frmLocation.SuspendLayout();
      this.tlpLocation.SuspendLayout();
      this.flpLocation.SuspendLayout();
      this.frmDistressSurvey.SuspendLayout();
      this.TableLayoutPanel1.SuspendLayout();
      ((ISupportInitialize) this.ugSubcomponents).BeginInit();
      this.flpSubCompData.SuspendLayout();
      this.frmDirectRating.SuspendLayout();
      this.tlpRatings.SuspendLayout();
      this.frmDirectComponent.SuspendLayout();
      this.tlpDirectRating.SuspendLayout();
      this.frmDirectPaint.SuspendLayout();
      this.tlpPaintRating.SuspendLayout();
      this.frmBuildingPOC.SuspendLayout();
      this.tlpBuildingPOC.SuspendLayout();
      this.frmBuildingAddress.SuspendLayout();
      this.tlpBuildingAddress.SuspendLayout();
      this.frmMethod.SuspendLayout();
      this.flpInspMethod.SuspendLayout();
      this.frmRatingType.SuspendLayout();
      this.flpInspType.SuspendLayout();
      this.pnlLocationInfo.SuspendLayout();
      this.pnlComponentInfo.SuspendLayout();
      this.pnlSystemInfo.SuspendLayout();
      this.pnlStartup.SuspendLayout();
      this.pnlBuildingInsp.SuspendLayout();
      this.pnlBuildingInfo.SuspendLayout();
      this.tlpBuildingInfo.SuspendLayout();
      this.pnlSectionInfo.SuspendLayout();
      this.tbSection.BeginInit();
      this.tpSection.SuspendLayout();
      this.tlpSectionInfo.SuspendLayout();
      this.FlowLayoutPanel1.SuspendLayout();
      this.tpDetails.SuspendLayout();
      this.rgDetails.BeginInit();
      this.rgDetails.MasterTemplate.BeginInit();
      this.RadCommandBar1.BeginInit();
      this.pnlInspectionInfo.SuspendLayout();
      this.tlpInspectionInfo.SuspendLayout();
      this.tlpSecQty.SuspendLayout();
      this.flpInspectionDate.SuspendLayout();
      this.pnlAssessments.SuspendLayout();
      this.pnlTaskList.SuspendLayout();
      this.pnlDetails.SuspendLayout();
      this.pnlFuncAssessment.SuspendLayout();
      this.pnlFuncArea.SuspendLayout();
      this.pnlTrees.SuspendLayout();
      ((ISupportInitialize) this.tvFunctionality).BeginInit();
      this.SplitContainerMain.BeginInit();
      this.SplitContainerMain.Panel1.SuspendLayout();
      this.SplitContainerMain.Panel2.SuspendLayout();
      this.SplitContainerMain.SuspendLayout();
      this.ssStatusStrip.SuspendLayout();
      this.tsToolbar.SuspendLayout();
      this.SuspendLayout();
      this.cmdIncrease.BackgroundImage = (Image) BuilderRED.My.Resources.Resources.Symbol_Add_2;
      this.cmdIncrease.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdIncrease.Location = new Point(13, 0);
      this.cmdIncrease.Margin = new Padding(0);
      this.cmdIncrease.Name = "cmdIncrease";
      this.cmdIncrease.Size = new Size(23, 23);
      this.cmdIncrease.TabIndex = 14;
      this.ToolTip1.SetToolTip((Control) this.cmdIncrease, "Increment Quantity");
      this.cmdIncrease.UseVisualStyleBackColor = true;
      this.cmdDecrease.BackgroundImage = (Image) BuilderRED.My.Resources.Resources.Symbol_Restricted_2;
      this.cmdDecrease.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdDecrease.Location = new Point(36, 0);
      this.cmdDecrease.Margin = new Padding(0);
      this.cmdDecrease.Name = "cmdDecrease";
      this.cmdDecrease.Size = new Size(23, 23);
      this.cmdDecrease.TabIndex = 15;
      this.ToolTip1.SetToolTip((Control) this.cmdDecrease, "Decrease Quantity");
      this.cmdDecrease.UseVisualStyleBackColor = true;
      this.cmdCalc.BackgroundImage = (Image) BuilderRED.My.Resources.Resources.Calculator_Accounting;
      this.cmdCalc.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdCalc.Location = new Point(59, 0);
      this.cmdCalc.Margin = new Padding(0);
      this.cmdCalc.Name = "cmdCalc";
      this.cmdCalc.Size = new Size(23, 23);
      this.cmdCalc.TabIndex = 16;
      this.ToolTip1.SetToolTip((Control) this.cmdCalc, "Calculate Quantity");
      this.cmdCalc.UseVisualStyleBackColor = true;
      this.cmdSampComment.AutoSize = true;
      this.cmdSampComment.BackColor = SystemColors.Control;
      this.cmdSampComment.BackgroundImage = (Image) BuilderRED.My.Resources.Resources.Clipboard;
      this.cmdSampComment.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdSampComment.Cursor = Cursors.Default;
      this.cmdSampComment.ForeColor = SystemColors.ControlText;
      this.cmdSampComment.Location = new Point(66, 3);
      this.cmdSampComment.Margin = new Padding(0, 3, 0, 3);
      this.cmdSampComment.Name = "cmdSampComment";
      this.cmdSampComment.RightToLeft = RightToLeft.No;
      this.cmdSampComment.Size = new Size(22, 22);
      this.cmdSampComment.TabIndex = 22;
      this.cmdSampComment.TextAlign = ContentAlignment.BottomCenter;
      this.ToolTip1.SetToolTip((Control) this.cmdSampComment, "Sample Comments");
      this.cmdSampComment.UseVisualStyleBackColor = false;
      this.cmdEditSample.AutoSize = true;
      this.cmdEditSample.BackColor = SystemColors.Control;
      this.cmdEditSample.BackgroundImage = (Image) BuilderRED.My.Resources.Resources.edit;
      this.cmdEditSample.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdEditSample.Cursor = Cursors.Default;
      this.cmdEditSample.ForeColor = SystemColors.ControlText;
      this.cmdEditSample.Location = new Point(44, 3);
      this.cmdEditSample.Margin = new Padding(0, 3, 0, 3);
      this.cmdEditSample.Name = "cmdEditSample";
      this.cmdEditSample.RightToLeft = RightToLeft.No;
      this.cmdEditSample.Size = new Size(22, 22);
      this.cmdEditSample.TabIndex = 21;
      this.cmdEditSample.TextAlign = ContentAlignment.BottomCenter;
      this.ToolTip1.SetToolTip((Control) this.cmdEditSample, "Edit Sample Details");
      this.cmdEditSample.UseVisualStyleBackColor = false;
      this.cmdDeleteSample.AutoSize = true;
      this.cmdDeleteSample.BackColor = SystemColors.Control;
      this.cmdDeleteSample.BackgroundImage = (Image) BuilderRED.My.Resources.Resources.Delete;
      this.cmdDeleteSample.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdDeleteSample.Cursor = Cursors.Default;
      this.cmdDeleteSample.ForeColor = SystemColors.ControlText;
      this.cmdDeleteSample.Location = new Point(22, 3);
      this.cmdDeleteSample.Margin = new Padding(0, 3, 0, 3);
      this.cmdDeleteSample.Name = "cmdDeleteSample";
      this.cmdDeleteSample.RightToLeft = RightToLeft.No;
      this.cmdDeleteSample.Size = new Size(22, 22);
      this.cmdDeleteSample.TabIndex = 20;
      this.cmdDeleteSample.TextAlign = ContentAlignment.BottomCenter;
      this.ToolTip1.SetToolTip((Control) this.cmdDeleteSample, "Delete Sample");
      this.cmdDeleteSample.UseVisualStyleBackColor = false;
      this.cmdNewSample.AutoSize = true;
      this.cmdNewSample.BackColor = SystemColors.Control;
      this.cmdNewSample.BackgroundImage = (Image) BuilderRED.My.Resources.Resources._new;
      this.cmdNewSample.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdNewSample.Cursor = Cursors.Default;
      this.cmdNewSample.ForeColor = SystemColors.ControlText;
      this.cmdNewSample.Location = new Point(0, 3);
      this.cmdNewSample.Margin = new Padding(0, 3, 0, 3);
      this.cmdNewSample.Name = "cmdNewSample";
      this.cmdNewSample.RightToLeft = RightToLeft.No;
      this.cmdNewSample.Size = new Size(22, 22);
      this.cmdNewSample.TabIndex = 19;
      this.ToolTip1.SetToolTip((Control) this.cmdNewSample, "Add Sample");
      this.cmdNewSample.UseVisualStyleBackColor = false;
      this.cmdNewInspection.Anchor = AnchorStyles.None;
      this.cmdNewInspection.BackColor = SystemColors.Control;
      this.cmdNewInspection.BackgroundImage = (Image) BuilderRED.My.Resources.Resources._new;
      this.cmdNewInspection.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdNewInspection.Cursor = Cursors.Default;
      this.cmdNewInspection.ForeColor = SystemColors.ControlText;
      this.cmdNewInspection.Location = new Point(102, 1);
      this.cmdNewInspection.Margin = new Padding(0);
      this.cmdNewInspection.Name = "cmdNewInspection";
      this.cmdNewInspection.RightToLeft = RightToLeft.No;
      this.cmdNewInspection.Size = new Size(24, 24);
      this.cmdNewInspection.TabIndex = 12;
      this.cmdNewInspection.TextAlign = ContentAlignment.BottomCenter;
      this.ToolTip1.SetToolTip((Control) this.cmdNewInspection, "Add Inspection");
      this.cmdNewInspection.UseVisualStyleBackColor = false;
      this.cmdCopyInspection.Anchor = AnchorStyles.None;
      this.cmdCopyInspection.BackColor = SystemColors.Control;
      this.cmdCopyInspection.BackgroundImage = (Image) BuilderRED.My.Resources.Resources.Copy;
      this.cmdCopyInspection.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdCopyInspection.Cursor = Cursors.Default;
      this.cmdCopyInspection.ForeColor = SystemColors.ControlText;
      this.cmdCopyInspection.Location = new Point(126, 1);
      this.cmdCopyInspection.Margin = new Padding(0);
      this.cmdCopyInspection.Name = "cmdCopyInspection";
      this.cmdCopyInspection.RightToLeft = RightToLeft.No;
      this.cmdCopyInspection.Size = new Size(24, 24);
      this.cmdCopyInspection.TabIndex = 13;
      this.cmdCopyInspection.TextAlign = ContentAlignment.BottomCenter;
      this.ToolTip1.SetToolTip((Control) this.cmdCopyInspection, "Copy Inspection");
      this.cmdCopyInspection.UseVisualStyleBackColor = false;
      this.cmdDeleteInspection.Anchor = AnchorStyles.None;
      this.cmdDeleteInspection.BackColor = SystemColors.Control;
      this.cmdDeleteInspection.BackgroundImage = (Image) BuilderRED.My.Resources.Resources.Delete;
      this.cmdDeleteInspection.BackgroundImageLayout = ImageLayout.Stretch;
      this.cmdDeleteInspection.Cursor = Cursors.Default;
      this.cmdDeleteInspection.ForeColor = SystemColors.ControlText;
      this.cmdDeleteInspection.Location = new Point(150, 1);
      this.cmdDeleteInspection.Margin = new Padding(0);
      this.cmdDeleteInspection.Name = "cmdDeleteInspection";
      this.cmdDeleteInspection.RightToLeft = RightToLeft.No;
      this.cmdDeleteInspection.Size = new Size(24, 24);
      this.cmdDeleteInspection.TabIndex = 14;
      this.cmdDeleteInspection.TextAlign = ContentAlignment.BottomCenter;
      this.ToolTip1.SetToolTip((Control) this.cmdDeleteInspection, "Delete Inspection");
      this.cmdDeleteInspection.UseVisualStyleBackColor = false;
      this.optCompRating_3.BackColor = SystemColors.ControlDark;
      this.optCompRating_3.Cursor = Cursors.Default;
      this.optCompRating_3.Dock = DockStyle.Fill;
      this.optCompRating_3.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.optCompRating_3.ForeColor = Color.Lime;
      this.optCompRating_3.Location = new Point(1, 57);
      this.optCompRating_3.Margin = new Padding(1);
      this.optCompRating_3.Name = "optCompRating_3";
      this.optCompRating_3.Padding = new Padding(2, 2, 2, 2);
      this.optCompRating_3.RightToLeft = RightToLeft.No;
      this.optCompRating_3.Size = new Size(86, 26);
      this.optCompRating_3.TabIndex = 92;
      this.optCompRating_3.TabStop = true;
      this.optCompRating_3.Text = "Green -";
      this.ToolTip1.SetToolTip((Control) this.optCompRating_3, "Minor deterioration. Complete Operation largely met. Minor repair required.");
      this.optCompRating_3.UseVisualStyleBackColor = false;
      this.optCompRating_1.BackColor = SystemColors.ControlDark;
      this.optCompRating_1.Cursor = Cursors.Default;
      this.optCompRating_1.Dock = DockStyle.Fill;
      this.optCompRating_1.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.optCompRating_1.ForeColor = Color.Lime;
      this.optCompRating_1.Location = new Point(1, 1);
      this.optCompRating_1.Margin = new Padding(1);
      this.optCompRating_1.Name = "optCompRating_1";
      this.optCompRating_1.Padding = new Padding(2, 2, 2, 2);
      this.optCompRating_1.RightToLeft = RightToLeft.No;
      this.optCompRating_1.Size = new Size(86, 26);
      this.optCompRating_1.TabIndex = 94;
      this.optCompRating_1.TabStop = true;
      this.optCompRating_1.Text = "Green +";
      this.ToolTip1.SetToolTip((Control) this.optCompRating_1, "Free of observable or known distress.");
      this.optCompRating_1.UseVisualStyleBackColor = false;
      this.optCompRating_2.BackColor = SystemColors.ControlDark;
      this.optCompRating_2.Cursor = Cursors.Default;
      this.optCompRating_2.Dock = DockStyle.Fill;
      this.optCompRating_2.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.optCompRating_2.ForeColor = Color.Lime;
      this.optCompRating_2.Location = new Point(1, 29);
      this.optCompRating_2.Margin = new Padding(1);
      this.optCompRating_2.Name = "optCompRating_2";
      this.optCompRating_2.Padding = new Padding(2, 2, 2, 2);
      this.optCompRating_2.RightToLeft = RightToLeft.No;
      this.optCompRating_2.Size = new Size(86, 26);
      this.optCompRating_2.TabIndex = 93;
      this.optCompRating_2.TabStop = true;
      this.optCompRating_2.Text = "Green ";
      this.ToolTip1.SetToolTip((Control) this.optCompRating_2, "Slight deterioration, but Operation totally intact. Routine maintenance or minor repair could be accomplished.");
      this.optCompRating_2.UseVisualStyleBackColor = false;
      this.optCompRating_4.BackColor = SystemColors.ControlDark;
      this.optCompRating_4.Cursor = Cursors.Default;
      this.optCompRating_4.Dock = DockStyle.Fill;
      this.optCompRating_4.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.optCompRating_4.ForeColor = Color.Yellow;
      this.optCompRating_4.Location = new Point(89, 1);
      this.optCompRating_4.Margin = new Padding(1);
      this.optCompRating_4.Name = "optCompRating_4";
      this.optCompRating_4.Padding = new Padding(2, 2, 2, 2);
      this.optCompRating_4.RightToLeft = RightToLeft.No;
      this.optCompRating_4.Size = new Size(86, 26);
      this.optCompRating_4.TabIndex = 91;
      this.optCompRating_4.TabStop = true;
      this.optCompRating_4.Text = "Amber +";
      this.ToolTip1.SetToolTip((Control) this.optCompRating_4, "Moderate deterioration. Operation adequate, but somewhat impaired. Moderate level of repair required.");
      this.optCompRating_4.UseVisualStyleBackColor = false;
      this.optCompRating_7.BackColor = SystemColors.ControlDark;
      this.optCompRating_7.Cursor = Cursors.Default;
      this.optCompRating_7.Dock = DockStyle.Fill;
      this.optCompRating_7.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.optCompRating_7.ForeColor = Color.Red;
      this.optCompRating_7.Location = new Point(177, 1);
      this.optCompRating_7.Margin = new Padding(1);
      this.optCompRating_7.Name = "optCompRating_7";
      this.optCompRating_7.Padding = new Padding(2, 2, 2, 2);
      this.optCompRating_7.RightToLeft = RightToLeft.No;
      this.optCompRating_7.Size = new Size(86, 26);
      this.optCompRating_7.TabIndex = 88;
      this.optCompRating_7.TabStop = true;
      this.optCompRating_7.Text = "Red +";
      this.ToolTip1.SetToolTip((Control) this.optCompRating_7, "Significant deterioration resulting in major impact on Operation. Major repair or rehabilitation required.");
      this.optCompRating_7.UseVisualStyleBackColor = false;
      this.optCompRating_8.BackColor = SystemColors.ControlDark;
      this.optCompRating_8.Cursor = Cursors.Default;
      this.optCompRating_8.Dock = DockStyle.Fill;
      this.optCompRating_8.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.optCompRating_8.ForeColor = Color.Red;
      this.optCompRating_8.Location = new Point(177, 29);
      this.optCompRating_8.Margin = new Padding(1);
      this.optCompRating_8.Name = "optCompRating_8";
      this.optCompRating_8.Padding = new Padding(2, 2, 2, 2);
      this.optCompRating_8.RightToLeft = RightToLeft.No;
      this.optCompRating_8.Size = new Size(86, 26);
      this.optCompRating_8.TabIndex = 87;
      this.optCompRating_8.TabStop = true;
      this.optCompRating_8.Text = "Red";
      this.ToolTip1.SetToolTip((Control) this.optCompRating_8, "Significant deterioration resulting in little Operation remaining.  Major rehabilitation or replacement required.");
      this.optCompRating_8.UseVisualStyleBackColor = false;
      this.optCompRating_6.BackColor = SystemColors.ControlDark;
      this.optCompRating_6.Cursor = Cursors.Default;
      this.optCompRating_6.Dock = DockStyle.Fill;
      this.optCompRating_6.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.optCompRating_6.ForeColor = Color.Yellow;
      this.optCompRating_6.Location = new Point(89, 57);
      this.optCompRating_6.Margin = new Padding(1);
      this.optCompRating_6.Name = "optCompRating_6";
      this.optCompRating_6.Padding = new Padding(2, 2, 2, 2);
      this.optCompRating_6.RightToLeft = RightToLeft.No;
      this.optCompRating_6.Size = new Size(86, 26);
      this.optCompRating_6.TabIndex = 89;
      this.optCompRating_6.TabStop = true;
      this.optCompRating_6.Text = "Amber -";
      this.ToolTip1.SetToolTip((Control) this.optCompRating_6, "Moderate deterioration adversely affects other components. Operation definitely impaired. Moderate repair required.");
      this.optCompRating_6.UseVisualStyleBackColor = false;
      this.optCompRating_5.BackColor = SystemColors.ControlDark;
      this.optCompRating_5.Cursor = Cursors.Default;
      this.optCompRating_5.Dock = DockStyle.Fill;
      this.optCompRating_5.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.optCompRating_5.ForeColor = Color.Yellow;
      this.optCompRating_5.Location = new Point(89, 29);
      this.optCompRating_5.Margin = new Padding(1);
      this.optCompRating_5.Name = "optCompRating_5";
      this.optCompRating_5.Padding = new Padding(2, 2, 2, 2);
      this.optCompRating_5.RightToLeft = RightToLeft.No;
      this.optCompRating_5.Size = new Size(86, 26);
      this.optCompRating_5.TabIndex = 90;
      this.optCompRating_5.TabStop = true;
      this.optCompRating_5.Text = "Amber";
      this.ToolTip1.SetToolTip((Control) this.optCompRating_5, "Moderate deterioration. Operation definitely impaired. Improvements needed. Moderate level of repair required.");
      this.optCompRating_5.UseVisualStyleBackColor = false;
      this.optCompRating_9.BackColor = SystemColors.ControlDark;
      this.optCompRating_9.Cursor = Cursors.Default;
      this.optCompRating_9.Dock = DockStyle.Fill;
      this.optCompRating_9.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.optCompRating_9.ForeColor = Color.Red;
      this.optCompRating_9.Location = new Point(177, 57);
      this.optCompRating_9.Margin = new Padding(1);
      this.optCompRating_9.Name = "optCompRating_9";
      this.optCompRating_9.Padding = new Padding(2, 2, 2, 2);
      this.optCompRating_9.RightToLeft = RightToLeft.No;
      this.optCompRating_9.Size = new Size(86, 26);
      this.optCompRating_9.TabIndex = 86;
      this.optCompRating_9.TabStop = true;
      this.optCompRating_9.Text = "Red -";
      this.ToolTip1.SetToolTip((Control) this.optCompRating_9, "Total deterioration resulting in complete loss of Operation. Total replacement or renewal warranted.");
      this.optCompRating_9.UseVisualStyleBackColor = false;
      this.optPaintRating_2.BackColor = SystemColors.ControlDark;
      this.optPaintRating_2.Cursor = Cursors.Default;
      this.optPaintRating_2.Dock = DockStyle.Fill;
      this.optPaintRating_2.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.optPaintRating_2.ForeColor = Color.Lime;
      this.optPaintRating_2.Location = new Point(1, 29);
      this.optPaintRating_2.Margin = new Padding(1);
      this.optPaintRating_2.Name = "optPaintRating_2";
      this.optPaintRating_2.Padding = new Padding(2, 2, 2, 2);
      this.optPaintRating_2.RightToLeft = RightToLeft.No;
      this.optPaintRating_2.Size = new Size(80, 26);
      this.optPaintRating_2.TabIndex = 97;
      this.optPaintRating_2.TabStop = true;
      this.optPaintRating_2.Text = "Green";
      this.ToolTip1.SetToolTip((Control) this.optPaintRating_2, "Little visible deterioration.");
      this.optPaintRating_2.UseVisualStyleBackColor = false;
      this.optPaintRating_5.BackColor = SystemColors.ControlDark;
      this.optPaintRating_5.Cursor = Cursors.Default;
      this.optPaintRating_5.Dock = DockStyle.Fill;
      this.optPaintRating_5.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.optPaintRating_5.ForeColor = Color.Yellow;
      this.optPaintRating_5.Location = new Point(83, 29);
      this.optPaintRating_5.Margin = new Padding(1);
      this.optPaintRating_5.Name = "optPaintRating_5";
      this.optPaintRating_5.Padding = new Padding(2, 2, 2, 2);
      this.optPaintRating_5.RightToLeft = RightToLeft.No;
      this.optPaintRating_5.Size = new Size(80, 26);
      this.optPaintRating_5.TabIndex = 101;
      this.optPaintRating_5.TabStop = true;
      this.optPaintRating_5.Text = "Amber";
      this.ToolTip1.SetToolTip((Control) this.optPaintRating_5, "Moderate visible deterioration.");
      this.optPaintRating_5.UseVisualStyleBackColor = false;
      this.optPaintRating_7.BackColor = SystemColors.ControlDark;
      this.optPaintRating_7.Cursor = Cursors.Default;
      this.optPaintRating_7.Dock = DockStyle.Fill;
      this.optPaintRating_7.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.optPaintRating_7.ForeColor = Color.Red;
      this.optPaintRating_7.Location = new Point(165, 1);
      this.optPaintRating_7.Margin = new Padding(1);
      this.optPaintRating_7.Name = "optPaintRating_7";
      this.optPaintRating_7.Padding = new Padding(2, 2, 2, 2);
      this.optPaintRating_7.RightToLeft = RightToLeft.No;
      this.optPaintRating_7.Size = new Size(80, 26);
      this.optPaintRating_7.TabIndex = 103;
      this.optPaintRating_7.TabStop = true;
      this.optPaintRating_7.Text = "Red +";
      this.ToolTip1.SetToolTip((Control) this.optPaintRating_7, "Significant visible deterioration.");
      this.optPaintRating_7.UseVisualStyleBackColor = false;
      this.optPaintRating_4.BackColor = SystemColors.ControlDark;
      this.optPaintRating_4.Cursor = Cursors.Default;
      this.optPaintRating_4.Dock = DockStyle.Fill;
      this.optPaintRating_4.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.optPaintRating_4.ForeColor = Color.Yellow;
      this.optPaintRating_4.Location = new Point(83, 1);
      this.optPaintRating_4.Margin = new Padding(1);
      this.optPaintRating_4.Name = "optPaintRating_4";
      this.optPaintRating_4.Padding = new Padding(2, 2, 2, 2);
      this.optPaintRating_4.RightToLeft = RightToLeft.No;
      this.optPaintRating_4.Size = new Size(80, 26);
      this.optPaintRating_4.TabIndex = 99;
      this.optPaintRating_4.TabStop = true;
      this.optPaintRating_4.Text = "Amber +";
      this.ToolTip1.SetToolTip((Control) this.optPaintRating_4, "Minor visible deterioration.");
      this.optPaintRating_4.UseVisualStyleBackColor = false;
      this.optPaintRating_3.BackColor = SystemColors.ControlDark;
      this.optPaintRating_3.Cursor = Cursors.Default;
      this.optPaintRating_3.Dock = DockStyle.Fill;
      this.optPaintRating_3.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.optPaintRating_3.ForeColor = Color.Lime;
      this.optPaintRating_3.Location = new Point(1, 57);
      this.optPaintRating_3.Margin = new Padding(1);
      this.optPaintRating_3.Name = "optPaintRating_3";
      this.optPaintRating_3.Padding = new Padding(2, 2, 2, 2);
      this.optPaintRating_3.RightToLeft = RightToLeft.No;
      this.optPaintRating_3.Size = new Size(80, 26);
      this.optPaintRating_3.TabIndex = 98;
      this.optPaintRating_3.TabStop = true;
      this.optPaintRating_3.Text = "Green -";
      this.ToolTip1.SetToolTip((Control) this.optPaintRating_3, "Some visible deterioration.");
      this.optPaintRating_3.UseVisualStyleBackColor = false;
      this.optPaintRating_8.BackColor = SystemColors.ControlDark;
      this.optPaintRating_8.Cursor = Cursors.Default;
      this.optPaintRating_8.Dock = DockStyle.Fill;
      this.optPaintRating_8.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.optPaintRating_8.ForeColor = Color.Red;
      this.optPaintRating_8.Location = new Point(165, 29);
      this.optPaintRating_8.Margin = new Padding(1);
      this.optPaintRating_8.Name = "optPaintRating_8";
      this.optPaintRating_8.Padding = new Padding(2, 2, 2, 2);
      this.optPaintRating_8.RightToLeft = RightToLeft.No;
      this.optPaintRating_8.Size = new Size(80, 26);
      this.optPaintRating_8.TabIndex = 104;
      this.optPaintRating_8.TabStop = true;
      this.optPaintRating_8.Text = "Red";
      this.ToolTip1.SetToolTip((Control) this.optPaintRating_8, "Severe visible deterioration.");
      this.optPaintRating_8.UseVisualStyleBackColor = false;
      this.optPaintRating_6.BackColor = SystemColors.ControlDark;
      this.optPaintRating_6.Cursor = Cursors.Default;
      this.optPaintRating_6.Dock = DockStyle.Fill;
      this.optPaintRating_6.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.optPaintRating_6.ForeColor = Color.Yellow;
      this.optPaintRating_6.Location = new Point(83, 57);
      this.optPaintRating_6.Margin = new Padding(1);
      this.optPaintRating_6.Name = "optPaintRating_6";
      this.optPaintRating_6.Padding = new Padding(2, 2, 2, 2);
      this.optPaintRating_6.RightToLeft = RightToLeft.No;
      this.optPaintRating_6.Size = new Size(80, 26);
      this.optPaintRating_6.TabIndex = 100;
      this.optPaintRating_6.TabStop = true;
      this.optPaintRating_6.Text = "Amber -";
      this.ToolTip1.SetToolTip((Control) this.optPaintRating_6, "Major visible deterioration.");
      this.optPaintRating_6.UseVisualStyleBackColor = false;
      this.optPaintRating_1.BackColor = SystemColors.ControlDark;
      this.optPaintRating_1.Cursor = Cursors.Default;
      this.optPaintRating_1.Dock = DockStyle.Fill;
      this.optPaintRating_1.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.optPaintRating_1.ForeColor = Color.Lime;
      this.optPaintRating_1.Location = new Point(1, 1);
      this.optPaintRating_1.Margin = new Padding(1);
      this.optPaintRating_1.Name = "optPaintRating_1";
      this.optPaintRating_1.Padding = new Padding(2, 2, 2, 2);
      this.optPaintRating_1.RightToLeft = RightToLeft.No;
      this.optPaintRating_1.Size = new Size(80, 26);
      this.optPaintRating_1.TabIndex = 96;
      this.optPaintRating_1.TabStop = true;
      this.optPaintRating_1.Text = "Green +";
      this.ToolTip1.SetToolTip((Control) this.optPaintRating_1, "Perfect condition. No visible deterioration.");
      this.optPaintRating_1.UseVisualStyleBackColor = false;
      this.optPaintRating_9.BackColor = SystemColors.ControlDark;
      this.optPaintRating_9.Cursor = Cursors.Default;
      this.optPaintRating_9.Dock = DockStyle.Fill;
      this.optPaintRating_9.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.optPaintRating_9.ForeColor = Color.Red;
      this.optPaintRating_9.Location = new Point(165, 57);
      this.optPaintRating_9.Margin = new Padding(1);
      this.optPaintRating_9.Name = "optPaintRating_9";
      this.optPaintRating_9.Padding = new Padding(2, 2, 2, 2);
      this.optPaintRating_9.RightToLeft = RightToLeft.No;
      this.optPaintRating_9.Size = new Size(80, 26);
      this.optPaintRating_9.TabIndex = 102;
      this.optPaintRating_9.TabStop = true;
      this.optPaintRating_9.Text = "Red -";
      this.ToolTip1.SetToolTip((Control) this.optPaintRating_9, "Complete deterioration.");
      this.optPaintRating_9.UseVisualStyleBackColor = false;
      this.miViewBar1.Index = 3;
      this.miViewBar1.Text = "-";
      this.MainMenu1.MenuItems.AddRange(new MenuItem[4]
      {
        this.miFile,
        this.miView,
        this.miTools,
        this.miHelp
      });
      this.miFile.Index = 0;
      this.miFile.MenuItems.AddRange(new MenuItem[5]
      {
        this.miFileImport,
        this.miFileBar0,
        this.miFileInspector,
        this.miFileBar1,
        this.miFileClose
      });
      this.miFile.Text = "&File";
      this.miFileImport.Index = 0;
      this.miFileImport.Text = "&Open Inspection Database";
      this.miFileBar0.Index = 1;
      this.miFileBar0.Text = "-";
      this.miFileInspector.Enabled = false;
      this.miFileInspector.Index = 2;
      this.miFileInspector.Text = "&Change Current Inspector...";
      this.miFileBar1.Index = 3;
      this.miFileBar1.Text = "-";
      this.miFileClose.Index = 4;
      this.miFileClose.Text = "&Close";
      this.miView.Enabled = false;
      this.miView.Index = 1;
      this.miView.MenuItems.AddRange(new MenuItem[7]
      {
        this.miInventoryMode,
        this.miInspectionMode,
        this.miFunctionalityMode,
        this.miViewBar1,
        this.miViewRefresh,
        this.miViewBar2,
        this.miUnits
      });
      this.miView.Text = "&View";
      this.miInventoryMode.Checked = true;
      this.miInventoryMode.Index = 0;
      this.miInventoryMode.Text = "Inventory Mode";
      this.miInspectionMode.Index = 1;
      this.miInspectionMode.Text = "Inspection Mode";
      this.miFunctionalityMode.Index = 2;
      this.miFunctionalityMode.Text = "Functionality Mode";
      this.miViewRefresh.Index = 4;
      this.miViewRefresh.Text = "Refresh Trees";
      this.miViewBar2.Index = 5;
      this.miViewBar2.Text = "-";
      this.miUnits.Checked = true;
      this.miUnits.Index = 6;
      this.miUnits.Text = "Display measurements in English";
      this.miTools.Enabled = false;
      this.miTools.Index = 2;
      this.miTools.MenuItems.AddRange(new MenuItem[5]
      {
        this.miToolsCopyInventory,
        this.miToolsInspectSections,
        this.miToolsSep1,
        this.miToolsReports,
        this.miToolsOptions
      });
      this.miTools.Text = "&Tools";
      this.miToolsCopyInventory.Index = 0;
      this.miToolsCopyInventory.Text = "&Copy Sections";
      this.miToolsInspectSections.Enabled = false;
      this.miToolsInspectSections.Index = 1;
      this.miToolsInspectSections.Text = "&Inspect Sections";
      this.miToolsSep1.Index = 2;
      this.miToolsSep1.Text = "-";
      this.miToolsReports.Index = 3;
      this.miToolsReports.Text = "&Reports";
      this.miToolsOptions.Index = 4;
      this.miToolsOptions.MenuItems.AddRange(new MenuItem[1]
      {
        this.miOpenLastFile
      });
      this.miToolsOptions.Text = "Options";
      this.miOpenLastFile.Index = 0;
      this.miOpenLastFile.Text = "Open Last File on Startup";
      this.miHelp.Index = 3;
      this.miHelp.MenuItems.AddRange(new MenuItem[3]
      {
        this.miHelpContents,
        this.miHelpBar0,
        this.miHelpAbout
      });
      this.miHelp.Text = "&Help";
      this.miHelpContents.Index = 0;
      this.miHelpContents.Text = "&Contents";
      this.miHelpBar0.Index = 1;
      this.miHelpBar0.Text = "-";
      this.miHelpAbout.Index = 2;
      this.miHelpAbout.Text = "&About ";
      this.tvInventory.Dock = DockStyle.Fill;
      this.tvInventory.ExpansionIndicatorSize = new Size(15, 15);
      this.HelpProvider.SetHelpKeyword((Control) this.tvInventory, "");
      this.HelpProvider.SetHelpNavigator((Control) this.tvInventory, HelpNavigator.KeywordIndex);
      this.HelpProvider.SetHelpString((Control) this.tvInventory, "");
      this.tvInventory.HideSelection = false;
      this.tvInventory.Location = new Point(0, 0);
      this.tvInventory.Name = "tvInventory";
      this.HelpProvider.SetShowHelp((Control) this.tvInventory, true);
      this.tvInventory.Size = new Size(298, 498);
      this.tvInventory.TabIndex = 130;
      this.tvInventory.UseOsThemes = DefaultableBoolean.False;
      this.tvInspection.Dock = DockStyle.Fill;
      this.tvInspection.ExpansionIndicatorSize = new Size(15, 15);
      this.HelpProvider.SetHelpNavigator((Control) this.tvInspection, HelpNavigator.KeywordIndex);
      this.tvInspection.HideSelection = false;
      this.tvInspection.Location = new Point(0, 0);
      this.tvInspection.Name = "tvInspection";
      this.HelpProvider.SetShowHelp((Control) this.tvInspection, true);
      this.tvInspection.Size = new Size(298, 498);
      this.tvInspection.TabIndex = 131;
      this.tvInspection.UseOsThemes = DefaultableBoolean.False;
      this.txtSectionAmount.AcceptsReturn = true;
      this.txtSectionAmount.BackColor = SystemColors.Window;
      this.txtSectionAmount.Cursor = Cursors.IBeam;
      this.txtSectionAmount.Dock = DockStyle.Fill;
      this.txtSectionAmount.ForeColor = SystemColors.WindowText;
      this.HelpProvider.SetHelpNavigator((Control) this.txtSectionAmount, HelpNavigator.KeywordIndex);
      this.txtSectionAmount.Location = new Point(119, 84);
      this.txtSectionAmount.MaxLength = 0;
      this.txtSectionAmount.Name = "txtSectionAmount";
      this.txtSectionAmount.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this.txtSectionAmount, true);
      this.txtSectionAmount.Size = new Size(86, 19);
      this.txtSectionAmount.TabIndex = 49;
      this.txtSectionYearBuilt.AcceptsReturn = true;
      this.txtSectionYearBuilt.BackColor = SystemColors.Window;
      this.txtSectionYearBuilt.Cursor = Cursors.IBeam;
      this.txtSectionYearBuilt.Dock = DockStyle.Fill;
      this.txtSectionYearBuilt.ForeColor = SystemColors.WindowText;
      this.HelpProvider.SetHelpNavigator((Control) this.txtSectionYearBuilt, HelpNavigator.KeywordIndex);
      this.txtSectionYearBuilt.Location = new Point(119, 109);
      this.txtSectionYearBuilt.MaxLength = 0;
      this.txtSectionYearBuilt.Name = "txtSectionYearBuilt";
      this.txtSectionYearBuilt.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this.txtSectionYearBuilt, true);
      this.txtSectionYearBuilt.Size = new Size(86, 19);
      this.txtSectionYearBuilt.TabIndex = 51;
      this.txtBldgArea.AcceptsReturn = true;
      this.txtBldgArea.BackColor = SystemColors.Window;
      this.txtBldgArea.Cursor = Cursors.IBeam;
      this.txtBldgArea.Dock = DockStyle.Fill;
      this.txtBldgArea.ForeColor = SystemColors.WindowText;
      this.HelpProvider.SetHelpNavigator((Control) this.txtBldgArea, HelpNavigator.KeywordIndex);
      this.txtBldgArea.Location = new Point(95, 83);
      this.txtBldgArea.MaxLength = 0;
      this.txtBldgArea.Name = "txtBldgArea";
      this.txtBldgArea.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this.txtBldgArea, true);
      this.txtBldgArea.Size = new Size(100, 20);
      this.txtBldgArea.TabIndex = 16;
      this.txtPOCEmail.AcceptsReturn = true;
      this.txtPOCEmail.BackColor = SystemColors.Window;
      this.tlpBuildingPOC.SetColumnSpan((Control) this.txtPOCEmail, 2);
      this.txtPOCEmail.Cursor = Cursors.IBeam;
      this.txtPOCEmail.Dock = DockStyle.Fill;
      this.txtPOCEmail.ForeColor = SystemColors.WindowText;
      this.HelpProvider.SetHelpNavigator((Control) this.txtPOCEmail, HelpNavigator.KeywordIndex);
      this.txtPOCEmail.Location = new Point(50, 55);
      this.txtPOCEmail.MaxLength = 0;
      this.txtPOCEmail.Name = "txtPOCEmail";
      this.txtPOCEmail.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this.txtPOCEmail, true);
      this.txtPOCEmail.Size = new Size(660, 20);
      this.txtPOCEmail.TabIndex = 23;
      this.txtPOCPhone.AcceptsReturn = true;
      this.txtPOCPhone.BackColor = SystemColors.Window;
      this.txtPOCPhone.Cursor = Cursors.IBeam;
      this.txtPOCPhone.Dock = DockStyle.Fill;
      this.txtPOCPhone.ForeColor = SystemColors.WindowText;
      this.HelpProvider.SetHelpNavigator((Control) this.txtPOCPhone, HelpNavigator.KeywordIndex);
      this.txtPOCPhone.Location = new Point(50, 29);
      this.txtPOCPhone.MaxLength = 0;
      this.txtPOCPhone.Name = "txtPOCPhone";
      this.txtPOCPhone.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this.txtPOCPhone, true);
      this.txtPOCPhone.Size = new Size(89, 20);
      this.txtPOCPhone.TabIndex = 22;
      this.txtPOC.AcceptsReturn = true;
      this.txtPOC.BackColor = SystemColors.Window;
      this.tlpBuildingPOC.SetColumnSpan((Control) this.txtPOC, 2);
      this.txtPOC.Cursor = Cursors.IBeam;
      this.txtPOC.Dock = DockStyle.Fill;
      this.txtPOC.ForeColor = SystemColors.WindowText;
      this.HelpProvider.SetHelpNavigator((Control) this.txtPOC, HelpNavigator.KeywordIndex);
      this.txtPOC.Location = new Point(50, 3);
      this.txtPOC.MaxLength = 0;
      this.txtPOC.Name = "txtPOC";
      this.txtPOC.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this.txtPOC, true);
      this.txtPOC.Size = new Size(660, 20);
      this.txtPOC.TabIndex = 21;
      this.txtZipCode.AcceptsReturn = true;
      this.txtZipCode.BackColor = SystemColors.Window;
      this.txtZipCode.Cursor = Cursors.IBeam;
      this.txtZipCode.Dock = DockStyle.Fill;
      this.txtZipCode.ForeColor = SystemColors.WindowText;
      this.HelpProvider.SetHelpNavigator((Control) this.txtZipCode, HelpNavigator.KeywordIndex);
      this.txtZipCode.Location = new Point(187, 55);
      this.txtZipCode.MaxLength = 0;
      this.txtZipCode.Name = "txtZipCode";
      this.txtZipCode.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this.txtZipCode, true);
      this.txtZipCode.Size = new Size(344, 20);
      this.txtZipCode.TabIndex = 20;
      this.txtState.AcceptsReturn = true;
      this.txtState.BackColor = SystemColors.Window;
      this.txtState.Cursor = Cursors.IBeam;
      this.txtState.Dock = DockStyle.Fill;
      this.txtState.ForeColor = SystemColors.WindowText;
      this.HelpProvider.SetHelpNavigator((Control) this.txtState, HelpNavigator.KeywordIndex);
      this.txtState.Location = new Point(73, 55);
      this.txtState.MaxLength = 0;
      this.txtState.Name = "txtState";
      this.txtState.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this.txtState, true);
      this.txtState.Size = new Size(49, 20);
      this.txtState.TabIndex = 19;
      this.txtCity.AcceptsReturn = true;
      this.txtCity.BackColor = SystemColors.Window;
      this.tlpBuildingAddress.SetColumnSpan((Control) this.txtCity, 3);
      this.txtCity.Cursor = Cursors.IBeam;
      this.txtCity.Dock = DockStyle.Fill;
      this.txtCity.ForeColor = SystemColors.WindowText;
      this.HelpProvider.SetHelpNavigator((Control) this.txtCity, HelpNavigator.KeywordIndex);
      this.txtCity.Location = new Point(73, 29);
      this.txtCity.MaxLength = 0;
      this.txtCity.Name = "txtCity";
      this.txtCity.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this.txtCity, true);
      this.txtCity.Size = new Size(458, 20);
      this.txtCity.TabIndex = 18;
      this.txtAddress.AcceptsReturn = true;
      this.txtAddress.BackColor = SystemColors.Window;
      this.tlpBuildingAddress.SetColumnSpan((Control) this.txtAddress, 4);
      this.txtAddress.Cursor = Cursors.IBeam;
      this.txtAddress.Dock = DockStyle.Fill;
      this.txtAddress.ForeColor = SystemColors.WindowText;
      this.HelpProvider.SetHelpNavigator((Control) this.txtAddress, HelpNavigator.KeywordIndex);
      this.txtAddress.Location = new Point(73, 3);
      this.txtAddress.MaxLength = 0;
      this.txtAddress.Name = "txtAddress";
      this.txtAddress.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this.txtAddress, true);
      this.txtAddress.Size = new Size(637, 20);
      this.txtAddress.TabIndex = 17;
      this.txtNoFloors.AcceptsReturn = true;
      this.txtNoFloors.BackColor = SystemColors.Window;
      this.txtNoFloors.Cursor = Cursors.IBeam;
      this.txtNoFloors.Dock = DockStyle.Fill;
      this.HelpProvider.SetHelpNavigator((Control) this.txtNoFloors, HelpNavigator.KeywordIndex);
      this.txtNoFloors.Location = new Point(201, 135);
      this.txtNoFloors.MaxLength = 0;
      this.txtNoFloors.Name = "txtNoFloors";
      this.txtNoFloors.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this.txtNoFloors, true);
      this.txtNoFloors.Size = new Size(205, 20);
      this.txtNoFloors.TabIndex = 15;
      this.txtYearBuilt.AcceptsReturn = true;
      this.txtYearBuilt.BackColor = SystemColors.Window;
      this.txtYearBuilt.Cursor = Cursors.IBeam;
      this.txtYearBuilt.Dock = DockStyle.Fill;
      this.HelpProvider.SetHelpNavigator((Control) this.txtYearBuilt, HelpNavigator.KeywordIndex);
      this.txtYearBuilt.Location = new Point(95, 109);
      this.txtYearBuilt.MaxLength = 0;
      this.txtYearBuilt.Name = "txtYearBuilt";
      this.txtYearBuilt.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this.txtYearBuilt, true);
      this.txtYearBuilt.Size = new Size(100, 20);
      this.txtYearBuilt.TabIndex = 14;
      this.txtBuildingName.AcceptsReturn = true;
      this.txtBuildingName.BackColor = SystemColors.Window;
      this.tlpBuildingInfo.SetColumnSpan((Control) this.txtBuildingName, 3);
      this.txtBuildingName.Cursor = Cursors.IBeam;
      this.txtBuildingName.Dock = DockStyle.Fill;
      this.txtBuildingName.ForeColor = SystemColors.WindowText;
      this.HelpProvider.SetHelpNavigator((Control) this.txtBuildingName, HelpNavigator.KeywordIndex);
      this.txtBuildingName.Location = new Point(201, 3);
      this.txtBuildingName.MaxLength = 0;
      this.txtBuildingName.Name = "txtBuildingName";
      this.txtBuildingName.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this.txtBuildingName, true);
      this.txtBuildingName.Size = new Size(521, 20);
      this.txtBuildingName.TabIndex = 11;
      this.frmLocation.AutoSize = true;
      this.frmLocation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.frmLocation.BackColor = SystemColors.Control;
      this.tlpInspectionInfo.SetColumnSpan((Control) this.frmLocation, 7);
      this.frmLocation.Controls.Add((Control) this.tlpLocation);
      this.frmLocation.Cursor = Cursors.Default;
      this.frmLocation.Dock = DockStyle.Fill;
      this.frmLocation.ForeColor = SystemColors.ControlText;
      this.HelpProvider.SetHelpNavigator((Control) this.frmLocation, HelpNavigator.KeywordIndex);
      this.frmLocation.Location = new Point(3, 153);
      this.frmLocation.Name = "frmLocation";
      this.HelpProvider.SetShowHelp((Control) this.frmLocation, true);
      this.frmLocation.Size = new Size(719, 68);
      this.frmLocation.TabIndex = 109;
      this.tlpLocation.AutoSize = true;
      this.tlpLocation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.tlpLocation.ColumnCount = 8;
      this.tlpLocation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpLocation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpLocation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpLocation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 10f));
      this.tlpLocation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpLocation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 50f));
      this.tlpLocation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 50f));
      this.tlpLocation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpLocation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 10f));
      this.tlpLocation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 10f));
      this.tlpLocation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 10f));
      this.tlpLocation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Absolute, 10f));
      this.tlpLocation.Controls.Add((Control) this.cboLocation, 5, 0);
      this.tlpLocation.Controls.Add((Control) this.chkSampPainted, 6, 1);
      this.tlpLocation.Controls.Add((Control) this.lblQty, 0, 0);
      this.tlpLocation.Controls.Add((Control) this.lblPCInspValue, 1, 1);
      this.tlpLocation.Controls.Add((Control) this.lblPCInsp, 0, 1);
      this.tlpLocation.Controls.Add((Control) this.lblSampQty, 2, 0);
      this.tlpLocation.Controls.Add((Control) this.lblLocation, 4, 0);
      this.tlpLocation.Controls.Add((Control) this.chkSampNonRep, 5, 1);
      this.tlpLocation.Controls.Add((Control) this.txtSQuantity, 1, 0);
      this.tlpLocation.Controls.Add((Control) this.flpLocation, 6, 0);
      this.tlpLocation.Dock = DockStyle.Fill;
      this.tlpLocation.Location = new Point(0, 0);
      this.tlpLocation.Name = "tlpLocation";
      this.tlpLocation.RowCount = 2;
      this.tlpLocation.RowStyles.Add(new RowStyle());
      this.tlpLocation.RowStyles.Add(new RowStyle());
      this.tlpLocation.Size = new Size(719, 68);
      this.tlpLocation.TabIndex = 136;
      this.tlpLocation.SetColumnSpan((Control) this.cboLocation, 2);
      this.cboLocation.Dock = DockStyle.Fill;
      this.cboLocation.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboLocation.Location = new Point(248, 3);
      this.cboLocation.Name = "cboLocation";
      this.cboLocation.Size = new Size(380, 21);
      this.cboLocation.TabIndex = 18;
      this.chkSampPainted.AutoSize = true;
      this.chkSampPainted.BackColor = SystemColors.Control;
      this.chkSampPainted.CheckAlign = ContentAlignment.MiddleRight;
      this.chkSampPainted.Cursor = Cursors.Default;
      this.chkSampPainted.Dock = DockStyle.Fill;
      this.chkSampPainted.ForeColor = SystemColors.ControlText;
      this.chkSampPainted.Location = new Point(441, 31);
      this.chkSampPainted.Name = "chkSampPainted";
      this.chkSampPainted.Size = new Size(187, 34);
      this.chkSampPainted.TabIndex = 24;
      this.chkSampPainted.Text = "Painted/Coated:";
      this.chkSampPainted.TextAlign = ContentAlignment.MiddleRight;
      this.chkSampPainted.UseVisualStyleBackColor = false;
      this.lblQty.AutoSize = true;
      this.lblQty.BackColor = SystemColors.Control;
      this.lblQty.Cursor = Cursors.Default;
      this.lblQty.Dock = DockStyle.Fill;
      this.lblQty.ForeColor = SystemColors.ControlText;
      this.lblQty.Location = new Point(3, 0);
      this.lblQty.Name = "lblQty";
      this.lblQty.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this.lblQty, true);
      this.lblQty.Size = new Size(59, 28);
      this.lblQty.TabIndex = 111;
      this.lblQty.Text = "Samp. Qty:";
      this.lblQty.TextAlign = ContentAlignment.MiddleRight;
      this.lblPCInspValue.Dock = DockStyle.Fill;
      this.lblPCInspValue.Location = new Point(68, 28);
      this.lblPCInspValue.Name = "lblPCInspValue";
      this.lblPCInspValue.Size = new Size(80, 40);
      this.lblPCInspValue.TabIndex = 135;
      this.lblPCInspValue.TextAlign = ContentAlignment.MiddleLeft;
      this.lblPCInsp.AutoSize = true;
      this.lblPCInsp.BackColor = SystemColors.Control;
      this.lblPCInsp.Cursor = Cursors.Default;
      this.lblPCInsp.Dock = DockStyle.Fill;
      this.lblPCInsp.ForeColor = SystemColors.ControlText;
      this.lblPCInsp.Location = new Point(3, 28);
      this.lblPCInsp.Name = "lblPCInsp";
      this.lblPCInsp.RightToLeft = RightToLeft.No;
      this.lblPCInsp.Size = new Size(59, 40);
      this.lblPCInsp.TabIndex = 113;
      this.lblPCInsp.Text = "% Insp:";
      this.lblPCInsp.TextAlign = ContentAlignment.MiddleRight;
      this.lblSampQty.AutoSize = true;
      this.lblSampQty.BackColor = SystemColors.Control;
      this.lblSampQty.Cursor = Cursors.Default;
      this.lblSampQty.Dock = DockStyle.Fill;
      this.lblSampQty.ForeColor = SystemColors.ControlText;
      this.lblSampQty.Location = new Point(154, 0);
      this.lblSampQty.Name = "lblSampQty";
      this.lblSampQty.RightToLeft = RightToLeft.No;
      this.lblSampQty.Size = new Size(21, 28);
      this.lblSampQty.TabIndex = 115;
      this.lblSampQty.Text = "EA";
      this.lblSampQty.TextAlign = ContentAlignment.MiddleLeft;
      this.lblLocation.AutoSize = true;
      this.lblLocation.BackColor = SystemColors.Control;
      this.lblLocation.Cursor = Cursors.Default;
      this.lblLocation.Dock = DockStyle.Right;
      this.lblLocation.ForeColor = SystemColors.ControlText;
      this.lblLocation.Location = new Point(191, 0);
      this.lblLocation.Name = "lblLocation";
      this.lblLocation.RightToLeft = RightToLeft.No;
      this.lblLocation.Size = new Size(51, 28);
      this.lblLocation.TabIndex = 110;
      this.lblLocation.Text = "Location:";
      this.lblLocation.TextAlign = ContentAlignment.MiddleRight;
      this.chkSampNonRep.BackColor = SystemColors.Control;
      this.chkSampNonRep.CheckAlign = ContentAlignment.MiddleRight;
      this.chkSampNonRep.Cursor = Cursors.Default;
      this.chkSampNonRep.Dock = DockStyle.Fill;
      this.chkSampNonRep.ForeColor = SystemColors.ControlText;
      this.chkSampNonRep.Location = new Point(248, 31);
      this.chkSampNonRep.Name = "chkSampNonRep";
      this.chkSampNonRep.Size = new Size(187, 34);
      this.chkSampNonRep.TabIndex = 23;
      this.chkSampNonRep.Text = "Non-representative:";
      this.chkSampNonRep.TextAlign = ContentAlignment.MiddleRight;
      this.chkSampNonRep.UseVisualStyleBackColor = false;
      this.txtSQuantity.AcceptsReturn = true;
      this.txtSQuantity.BackColor = SystemColors.Window;
      this.txtSQuantity.Cursor = Cursors.IBeam;
      this.txtSQuantity.Dock = DockStyle.Fill;
      this.txtSQuantity.ForeColor = SystemColors.WindowText;
      this.HelpProvider.SetHelpNavigator((Control) this.txtSQuantity, HelpNavigator.KeywordIndex);
      this.txtSQuantity.Location = new Point(68, 3);
      this.txtSQuantity.MaxLength = 0;
      this.txtSQuantity.Name = "txtSQuantity";
      this.txtSQuantity.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this.txtSQuantity, true);
      this.txtSQuantity.Size = new Size(80, 20);
      this.txtSQuantity.TabIndex = 17;
      this.txtSQuantity.TextAlign = HorizontalAlignment.Right;
      this.flpLocation.AutoSize = true;
      this.flpLocation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.flpLocation.Controls.Add((Control) this.cmdNewSample);
      this.flpLocation.Controls.Add((Control) this.cmdDeleteSample);
      this.flpLocation.Controls.Add((Control) this.cmdEditSample);
      this.flpLocation.Controls.Add((Control) this.cmdSampComment);
      this.flpLocation.Dock = DockStyle.Fill;
      this.flpLocation.Location = new Point(631, 0);
      this.flpLocation.Margin = new Padding(0);
      this.flpLocation.Name = "flpLocation";
      this.flpLocation.Size = new Size(88, 28);
      this.flpLocation.TabIndex = 138;
      this.flpLocation.WrapContents = false;
      this.txtComponent.AutoSize = true;
      this.txtComponent.BackColor = SystemColors.Control;
      this.tlpInspectionInfo.SetColumnSpan((Control) this.txtComponent, 6);
      this.txtComponent.Cursor = Cursors.Default;
      this.txtComponent.Dock = DockStyle.Fill;
      this.txtComponent.ForeColor = SystemColors.ControlText;
      this.HelpProvider.SetHelpNavigator((Control) this.txtComponent, HelpNavigator.KeywordIndex);
      this.txtComponent.Location = new Point(95, 0);
      this.txtComponent.Name = "txtComponent";
      this.txtComponent.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this.txtComponent, true);
      this.txtComponent.Size = new Size(627, 13);
      this.txtComponent.TabIndex = 2;
      this.txtComponent.Text = "txtComponent";
      this.txtComponent.TextAlign = ContentAlignment.MiddleLeft;
      this.frmDistressSurvey.AutoSize = true;
      this.frmDistressSurvey.BackColor = SystemColors.Control;
      this.frmDistressSurvey.Controls.Add((Control) this.TableLayoutPanel1);
      this.frmDistressSurvey.Dock = DockStyle.Bottom;
      this.frmDistressSurvey.ForeColor = SystemColors.ControlText;
      this.HelpProvider.SetHelpNavigator((Control) this.frmDistressSurvey, HelpNavigator.KeywordIndex);
      this.frmDistressSurvey.Location = new Point(0, 75);
      this.frmDistressSurvey.Name = "frmDistressSurvey";
      this.frmDistressSurvey.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this.frmDistressSurvey, true);
      this.frmDistressSurvey.Size = new Size(719, 193);
      this.frmDistressSurvey.TabIndex = 82;
      this.frmDistressSurvey.TabStop = false;
      this.frmDistressSurvey.Visible = false;
      this.TableLayoutPanel1.AutoSize = true;
      this.TableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.TableLayoutPanel1.ColumnCount = 1;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.TableLayoutPanel1.Controls.Add((Control) this.ugSubcomponents, 0, 2);
      this.TableLayoutPanel1.Controls.Add((Control) this.flpSubCompData, 0, 1);
      this.TableLayoutPanel1.Dock = DockStyle.Fill;
      this.TableLayoutPanel1.Location = new Point(3, 16);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 3;
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle());
      this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.TableLayoutPanel1.Size = new Size(713, 174);
      this.TableLayoutPanel1.TabIndex = 121;
      this.ugSubcomponents.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
      this.ugSubcomponents.Dock = DockStyle.Bottom;
      this.HelpProvider.SetHelpNavigator((Control) this.ugSubcomponents, HelpNavigator.KeywordIndex);
      this.ugSubcomponents.Location = new Point(3, 32);
      this.ugSubcomponents.Name = "ugSubcomponents";
      this.HelpProvider.SetShowHelp((Control) this.ugSubcomponents, true);
      this.ugSubcomponents.Size = new Size(707, 139);
      this.ugSubcomponents.TabIndex = 119;
      this.ugSubcomponents.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChange;
      this.flpSubCompData.AutoScroll = true;
      this.flpSubCompData.AutoSize = true;
      this.flpSubCompData.Controls.Add((Control) this.chkDefFree);
      this.flpSubCompData.Controls.Add((Control) this.chkPaintDefFree);
      this.flpSubCompData.Location = new Point(3, 3);
      this.flpSubCompData.Name = "flpSubCompData";
      this.flpSubCompData.Size = new Size(272, 23);
      this.flpSubCompData.TabIndex = 120;
      this.chkDefFree.AutoSize = true;
      this.chkDefFree.BackColor = SystemColors.Control;
      this.chkDefFree.Cursor = Cursors.Default;
      this.chkDefFree.ForeColor = SystemColors.ControlText;
      this.chkDefFree.Location = new Point(3, 3);
      this.chkDefFree.Name = "chkDefFree";
      this.chkDefFree.RightToLeft = RightToLeft.No;
      this.chkDefFree.Size = new Size(82, 17);
      this.chkDefFree.TabIndex = 119;
      this.chkDefFree.Text = "Defect Free";
      this.chkDefFree.UseVisualStyleBackColor = false;
      this.chkPaintDefFree.AutoSize = true;
      this.chkPaintDefFree.BackColor = SystemColors.Control;
      this.chkPaintDefFree.Cursor = Cursors.Default;
      this.chkPaintDefFree.ForeColor = SystemColors.ControlText;
      this.chkPaintDefFree.Location = new Point(91, 3);
      this.chkPaintDefFree.Name = "chkPaintDefFree";
      this.chkPaintDefFree.RightToLeft = RightToLeft.No;
      this.chkPaintDefFree.Size = new Size(178, 17);
      this.chkPaintDefFree.TabIndex = 120;
      this.chkPaintDefFree.Text = "Paint/Coating Defect Free (D/F)";
      this.chkPaintDefFree.UseVisualStyleBackColor = false;
      this.frmDirectRating.AutoSize = true;
      this.frmDirectRating.BackColor = SystemColors.Control;
      this.frmDirectRating.Controls.Add((Control) this.tlpRatings);
      this.frmDirectRating.ForeColor = SystemColors.ControlText;
      this.HelpProvider.SetHelpNavigator((Control) this.frmDirectRating, HelpNavigator.KeywordIndex);
      this.frmDirectRating.Location = new Point(0, 28);
      this.frmDirectRating.Name = "frmDirectRating";
      this.frmDirectRating.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this.frmDirectRating, true);
      this.frmDirectRating.Size = new Size(679, 193);
      this.frmDirectRating.TabIndex = 68;
      this.frmDirectRating.TabStop = false;
      this.tlpRatings.AutoSize = true;
      this.tlpRatings.ColumnCount = 3;
      this.tlpRatings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpRatings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpRatings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 100f));
      this.tlpRatings.Controls.Add((Control) this.frmDirectComponent, 0, 0);
      this.tlpRatings.Controls.Add((Control) this.frmDirectPaint, 1, 0);
      this.tlpRatings.Dock = DockStyle.Fill;
      this.tlpRatings.Location = new Point(3, 16);
      this.tlpRatings.Margin = new Padding(2, 2, 2, 2);
      this.tlpRatings.Name = "tlpRatings";
      this.tlpRatings.RowCount = 2;
      this.tlpRatings.RowStyles.Add(new RowStyle());
      this.tlpRatings.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tlpRatings.Size = new Size(673, 174);
      this.tlpRatings.TabIndex = 96;
      this.frmDirectComponent.AutoSize = true;
      this.frmDirectComponent.BackColor = SystemColors.Control;
      this.frmDirectComponent.Controls.Add((Control) this.tlpDirectRating);
      this.frmDirectComponent.Dock = DockStyle.Fill;
      this.frmDirectComponent.ForeColor = SystemColors.ControlText;
      this.frmDirectComponent.Location = new Point(3, 3);
      this.frmDirectComponent.Name = "frmDirectComponent";
      this.frmDirectComponent.RightToLeft = RightToLeft.No;
      this.frmDirectComponent.Size = new Size(270, 103);
      this.frmDirectComponent.TabIndex = 85;
      this.frmDirectComponent.TabStop = false;
      this.frmDirectComponent.Text = "Direct Condition Rating Scale";
      this.tlpDirectRating.AutoSize = true;
      this.tlpDirectRating.ColumnCount = 3;
      this.tlpDirectRating.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 33.33333f));
      this.tlpDirectRating.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 33.33333f));
      this.tlpDirectRating.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 33.33333f));
      this.tlpDirectRating.Controls.Add((Control) this.optCompRating_9, 2, 2);
      this.tlpDirectRating.Controls.Add((Control) this.optCompRating_5, 1, 1);
      this.tlpDirectRating.Controls.Add((Control) this.optCompRating_6, 1, 2);
      this.tlpDirectRating.Controls.Add((Control) this.optCompRating_8, 2, 1);
      this.tlpDirectRating.Controls.Add((Control) this.optCompRating_7, 2, 0);
      this.tlpDirectRating.Controls.Add((Control) this.optCompRating_4, 1, 0);
      this.tlpDirectRating.Controls.Add((Control) this.optCompRating_2, 0, 1);
      this.tlpDirectRating.Controls.Add((Control) this.optCompRating_1, 0, 0);
      this.tlpDirectRating.Controls.Add((Control) this.optCompRating_3, 0, 2);
      this.tlpDirectRating.Dock = DockStyle.Fill;
      this.tlpDirectRating.Location = new Point(3, 16);
      this.tlpDirectRating.Name = "tlpDirectRating";
      this.tlpDirectRating.RowCount = 3;
      this.tlpDirectRating.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
      this.tlpDirectRating.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
      this.tlpDirectRating.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
      this.tlpDirectRating.Size = new Size(264, 84);
      this.tlpDirectRating.TabIndex = 123;
      this.frmDirectPaint.AutoSize = true;
      this.frmDirectPaint.BackColor = SystemColors.Control;
      this.frmDirectPaint.Controls.Add((Control) this.tlpPaintRating);
      this.frmDirectPaint.Dock = DockStyle.Fill;
      this.frmDirectPaint.ForeColor = SystemColors.ControlText;
      this.frmDirectPaint.Location = new Point(279, 3);
      this.frmDirectPaint.Name = "frmDirectPaint";
      this.frmDirectPaint.RightToLeft = RightToLeft.No;
      this.frmDirectPaint.Size = new Size(252, 103);
      this.frmDirectPaint.TabIndex = 95;
      this.frmDirectPaint.TabStop = false;
      this.frmDirectPaint.Text = "Paint/Coating Condition Rating Scale";
      this.tlpPaintRating.AutoSize = true;
      this.tlpPaintRating.ColumnCount = 3;
      this.tlpPaintRating.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 33.33333f));
      this.tlpPaintRating.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 33.33333f));
      this.tlpPaintRating.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 33.33333f));
      this.tlpPaintRating.Controls.Add((Control) this.optPaintRating_9, 2, 2);
      this.tlpPaintRating.Controls.Add((Control) this.optPaintRating_1, 0, 0);
      this.tlpPaintRating.Controls.Add((Control) this.optPaintRating_6, 1, 2);
      this.tlpPaintRating.Controls.Add((Control) this.optPaintRating_8, 2, 1);
      this.tlpPaintRating.Controls.Add((Control) this.optPaintRating_3, 0, 2);
      this.tlpPaintRating.Controls.Add((Control) this.optPaintRating_4, 1, 0);
      this.tlpPaintRating.Controls.Add((Control) this.optPaintRating_7, 2, 0);
      this.tlpPaintRating.Controls.Add((Control) this.optPaintRating_5, 1, 1);
      this.tlpPaintRating.Controls.Add((Control) this.optPaintRating_2, 0, 1);
      this.tlpPaintRating.Dock = DockStyle.Fill;
      this.tlpPaintRating.Location = new Point(3, 16);
      this.tlpPaintRating.Name = "tlpPaintRating";
      this.tlpPaintRating.RowCount = 3;
      this.tlpPaintRating.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
      this.tlpPaintRating.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
      this.tlpPaintRating.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
      this.tlpPaintRating.Size = new Size(246, 84);
      this.tlpPaintRating.TabIndex = 124;
      this.cboInspectionDates.Anchor = AnchorStyles.None;
      this.cboInspectionDates.AutoCompleteSource = AutoCompleteSource.CustomSource;
      this.cboInspectionDates.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboInspectionDates.FormattingEnabled = true;
      this.HelpProvider.SetHelpNavigator((Control) this.cboInspectionDates, HelpNavigator.KeywordIndex);
      this.cboInspectionDates.Location = new Point(3, 3);
      this.cboInspectionDates.Name = "cboInspectionDates";
      this.HelpProvider.SetShowHelp((Control) this.cboInspectionDates, true);
      this.cboInspectionDates.Size = new Size(96, 21);
      this.cboInspectionDates.TabIndex = 149;
      this.lblEnergyAuditRequired.AutoSize = true;
      this.lblEnergyAuditRequired.Dock = DockStyle.Fill;
      this.lblEnergyAuditRequired.Location = new Point(3, 158);
      this.lblEnergyAuditRequired.Name = "lblEnergyAuditRequired";
      this.lblEnergyAuditRequired.Size = new Size(110, 30);
      this.lblEnergyAuditRequired.TabIndex = 146;
      this.lblEnergyAuditRequired.Text = "NOT Energy Efficient:";
      this.lblEnergyAuditRequired.TextAlign = ContentAlignment.MiddleRight;
      this.chkEnergyAuditRequired.BackColor = Color.Transparent;
      this.chkEnergyAuditRequired.Cursor = Cursors.Default;
      this.chkEnergyAuditRequired.Dock = DockStyle.Fill;
      this.chkEnergyAuditRequired.ForeColor = SystemColors.ControlText;
      this.chkEnergyAuditRequired.Location = new Point(119, 161);
      this.chkEnergyAuditRequired.Name = "chkEnergyAuditRequired";
      this.chkEnergyAuditRequired.RightToLeft = RightToLeft.No;
      this.chkEnergyAuditRequired.Size = new Size(86, 24);
      this.chkEnergyAuditRequired.TabIndex = 145;
      this.chkEnergyAuditRequired.UseVisualStyleBackColor = false;
      this.tlpSectionInfo.SetColumnSpan((Control) this.cboSectionName, 3);
      this.cboSectionName.Dock = DockStyle.Fill;
      this.cboSectionName.Location = new Point(119, 3);
      this.cboSectionName.MaxLength = 50;
      this.cboSectionName.Name = "cboSectionName";
      this.cboSectionName.Size = new Size(582, 21);
      this.cboSectionName.TabIndex = 144;
      this.tlpSectionInfo.SetColumnSpan((Control) this.cboSectionPaintType, 2);
      this.cboSectionPaintType.Dock = DockStyle.Fill;
      this.cboSectionPaintType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboSectionPaintType.Location = new Point(119, 246);
      this.cboSectionPaintType.Name = "cboSectionPaintType";
      this.cboSectionPaintType.Size = new Size(216, 21);
      this.cboSectionPaintType.TabIndex = 143;
      this.tlpSectionInfo.SetColumnSpan((Control) this.cboSectionComponentType, 3);
      this.cboSectionComponentType.Dock = DockStyle.Fill;
      this.cboSectionComponentType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboSectionComponentType.Location = new Point(119, 57);
      this.cboSectionComponentType.Name = "cboSectionComponentType";
      this.cboSectionComponentType.Size = new Size(582, 21);
      this.cboSectionComponentType.TabIndex = 142;
      this.tlpSectionInfo.SetColumnSpan((Control) this.cboSectionMaterial, 3);
      this.cboSectionMaterial.Dock = DockStyle.Fill;
      this.cboSectionMaterial.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboSectionMaterial.Location = new Point(119, 30);
      this.cboSectionMaterial.Name = "cboSectionMaterial";
      this.cboSectionMaterial.Size = new Size(582, 21);
      this.cboSectionMaterial.TabIndex = 141;
      this.chkYearEstimated.AutoSize = true;
      this.chkYearEstimated.BackColor = Color.Transparent;
      this.chkYearEstimated.Cursor = Cursors.Default;
      this.chkYearEstimated.Dock = DockStyle.Fill;
      this.chkYearEstimated.ForeColor = SystemColors.ControlText;
      this.chkYearEstimated.Location = new Point(211, 109);
      this.chkYearEstimated.Name = "chkYearEstimated";
      this.chkYearEstimated.RightToLeft = RightToLeft.No;
      this.chkYearEstimated.Size = new Size(124, 19);
      this.chkYearEstimated.TabIndex = 135;
      this.chkYearEstimated.Text = "Estimated";
      this.chkYearEstimated.UseVisualStyleBackColor = false;
      this.dtPainted.AcceptsReturn = true;
      this.dtPainted.BackColor = SystemColors.Window;
      this.dtPainted.Cursor = Cursors.IBeam;
      this.dtPainted.Dock = DockStyle.Fill;
      this.dtPainted.ForeColor = SystemColors.WindowText;
      this.dtPainted.Location = new Point(119, 221);
      this.dtPainted.MaxLength = 0;
      this.dtPainted.Name = "dtPainted";
      this.dtPainted.RightToLeft = RightToLeft.No;
      this.dtPainted.Size = new Size(86, 19);
      this.dtPainted.TabIndex = 53;
      this.chkPainted.BackColor = Color.Transparent;
      this.chkPainted.Cursor = Cursors.Default;
      this.chkPainted.Dock = DockStyle.Fill;
      this.chkPainted.ForeColor = SystemColors.ControlText;
      this.chkPainted.Location = new Point(119, 191);
      this.chkPainted.Name = "chkPainted";
      this.chkPainted.RightToLeft = RightToLeft.No;
      this.chkPainted.Size = new Size(86, 24);
      this.chkPainted.TabIndex = 52;
      this.chkPainted.UseVisualStyleBackColor = false;
      this.lblPaintType.AutoSize = true;
      this.lblPaintType.BackColor = Color.Transparent;
      this.lblPaintType.Cursor = Cursors.Default;
      this.lblPaintType.Dock = DockStyle.Fill;
      this.lblPaintType.ForeColor = SystemColors.ControlText;
      this.lblPaintType.Location = new Point(3, 243);
      this.lblPaintType.Name = "lblPaintType";
      this.lblPaintType.RightToLeft = RightToLeft.No;
      this.lblPaintType.Size = new Size(110, 27);
      this.lblPaintType.TabIndex = 137;
      this.lblPaintType.Text = "Paint/Coating Type:";
      this.lblPaintType.TextAlign = ContentAlignment.MiddleRight;
      this.lblDatePainted.AutoSize = true;
      this.lblDatePainted.BackColor = Color.Transparent;
      this.lblDatePainted.Cursor = Cursors.Default;
      this.lblDatePainted.Dock = DockStyle.Fill;
      this.lblDatePainted.ForeColor = SystemColors.ControlText;
      this.lblDatePainted.Location = new Point(3, 218);
      this.lblDatePainted.Name = "lblDatePainted";
      this.lblDatePainted.RightToLeft = RightToLeft.No;
      this.lblDatePainted.Size = new Size(110, 25);
      this.lblDatePainted.TabIndex = 56;
      this.lblDatePainted.Text = "Year Painted/Coated:";
      this.lblDatePainted.TextAlign = ContentAlignment.MiddleRight;
      this.lblPainted.AutoSize = true;
      this.lblPainted.BackColor = Color.Transparent;
      this.lblPainted.Cursor = Cursors.Default;
      this.lblPainted.Dock = DockStyle.Fill;
      this.lblPainted.ForeColor = SystemColors.ControlText;
      this.lblPainted.Location = new Point(3, 188);
      this.lblPainted.Name = "lblPainted";
      this.lblPainted.RightToLeft = RightToLeft.No;
      this.lblPainted.Size = new Size(110, 30);
      this.lblPainted.TabIndex = 55;
      this.lblPainted.Text = "Painted/Coated:";
      this.lblPainted.TextAlign = ContentAlignment.MiddleRight;
      this.lblSectionAmount.AutoSize = true;
      this.lblSectionAmount.BackColor = Color.Transparent;
      this.lblSectionAmount.Cursor = Cursors.Default;
      this.lblSectionAmount.Dock = DockStyle.Fill;
      this.lblSectionAmount.ForeColor = SystemColors.ControlText;
      this.lblSectionAmount.Location = new Point(3, 81);
      this.lblSectionAmount.Name = "lblSectionAmount";
      this.lblSectionAmount.RightToLeft = RightToLeft.No;
      this.lblSectionAmount.Size = new Size(110, 25);
      this.lblSectionAmount.TabIndex = 32;
      this.lblSectionAmount.Text = "Quantity:";
      this.lblSectionAmount.TextAlign = ContentAlignment.MiddleRight;
      this.lblSectionYearBuilt.AutoSize = true;
      this.lblSectionYearBuilt.BackColor = Color.Transparent;
      this.lblSectionYearBuilt.Cursor = Cursors.Default;
      this.lblSectionYearBuilt.Dock = DockStyle.Fill;
      this.lblSectionYearBuilt.ForeColor = SystemColors.ControlText;
      this.lblSectionYearBuilt.Location = new Point(3, 106);
      this.lblSectionYearBuilt.Name = "lblSectionYearBuilt";
      this.lblSectionYearBuilt.RightToLeft = RightToLeft.No;
      this.lblSectionYearBuilt.Size = new Size(110, 25);
      this.lblSectionYearBuilt.TabIndex = 31;
      this.lblSectionYearBuilt.Text = "Year Built:";
      this.lblSectionYearBuilt.TextAlign = ContentAlignment.MiddleRight;
      this.lblComponentType.AutoSize = true;
      this.lblComponentType.BackColor = Color.Transparent;
      this.lblComponentType.Cursor = Cursors.Default;
      this.lblComponentType.Dock = DockStyle.Fill;
      this.lblComponentType.ForeColor = SystemColors.ControlText;
      this.lblComponentType.Location = new Point(3, 54);
      this.lblComponentType.Name = "lblComponentType";
      this.lblComponentType.RightToLeft = RightToLeft.No;
      this.lblComponentType.Size = new Size(110, 27);
      this.lblComponentType.TabIndex = 30;
      this.lblComponentType.Text = "Component Type:";
      this.lblComponentType.TextAlign = ContentAlignment.MiddleRight;
      this.lblMaterial.AutoSize = true;
      this.lblMaterial.BackColor = Color.Transparent;
      this.lblMaterial.Cursor = Cursors.Default;
      this.lblMaterial.Dock = DockStyle.Fill;
      this.lblMaterial.ForeColor = SystemColors.ControlText;
      this.lblMaterial.Location = new Point(3, 27);
      this.lblMaterial.Name = "lblMaterial";
      this.lblMaterial.RightToLeft = RightToLeft.No;
      this.lblMaterial.Size = new Size(110, 27);
      this.lblMaterial.TabIndex = 29;
      this.lblMaterial.Text = "Material:";
      this.lblMaterial.TextAlign = ContentAlignment.MiddleRight;
      this.lblSectionName.AutoSize = true;
      this.lblSectionName.BackColor = Color.Transparent;
      this.lblSectionName.Cursor = Cursors.Default;
      this.lblSectionName.Dock = DockStyle.Fill;
      this.lblSectionName.ForeColor = SystemColors.ControlText;
      this.lblSectionName.Location = new Point(3, 0);
      this.lblSectionName.Name = "lblSectionName";
      this.lblSectionName.RightToLeft = RightToLeft.No;
      this.lblSectionName.Size = new Size(110, 27);
      this.lblSectionName.TabIndex = 28;
      this.lblSectionName.Text = "Section Name:";
      this.lblSectionName.TextAlign = ContentAlignment.MiddleRight;
      this.lblLocationInfo.BackColor = SystemColors.Control;
      this.lblLocationInfo.Cursor = Cursors.Default;
      this.lblLocationInfo.ForeColor = SystemColors.ControlText;
      this.lblLocationInfo.Location = new Point(6, 6);
      this.lblLocationInfo.Name = "lblLocationInfo";
      this.lblLocationInfo.RightToLeft = RightToLeft.No;
      this.lblLocationInfo.Size = new Size(257, 25);
      this.lblLocationInfo.TabIndex = 138;
      this.lblLocationInfo.Text = "There are no editable properties for locations.";
      this.tlpBuildingInfo.SetColumnSpan((Control) this.cboCatCode, 4);
      this.cboCatCode.Dock = DockStyle.Fill;
      this.cboCatCode.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboCatCode.Location = new Point(95, 29);
      this.cboCatCode.Name = "cboCatCode";
      this.cboCatCode.Size = new Size(627, 21);
      this.cboCatCode.TabIndex = 131;
      this.tlpBuildingInfo.SetColumnSpan((Control) this.cboConstructionType, 2);
      this.cboConstructionType.Dock = DockStyle.Fill;
      this.cboConstructionType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboConstructionType.Location = new Point(95, 56);
      this.cboConstructionType.Name = "cboConstructionType";
      this.cboConstructionType.Size = new Size(311, 21);
      this.cboConstructionType.TabIndex = 130;
      this.frmBuildingPOC.AutoSize = true;
      this.frmBuildingPOC.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.frmBuildingPOC.BackColor = SystemColors.Control;
      this.tlpBuildingInfo.SetColumnSpan((Control) this.frmBuildingPOC, 5);
      this.frmBuildingPOC.Controls.Add((Control) this.tlpBuildingPOC);
      this.frmBuildingPOC.Dock = DockStyle.Fill;
      this.frmBuildingPOC.ForeColor = SystemColors.ControlText;
      this.frmBuildingPOC.Location = new Point(3, 290);
      this.frmBuildingPOC.Name = "frmBuildingPOC";
      this.frmBuildingPOC.RightToLeft = RightToLeft.No;
      this.frmBuildingPOC.Size = new Size(719, 97);
      this.frmBuildingPOC.TabIndex = 40;
      this.frmBuildingPOC.TabStop = false;
      this.frmBuildingPOC.Text = "Point of Contact";
      this.tlpBuildingPOC.AutoSize = true;
      this.tlpBuildingPOC.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.tlpBuildingPOC.ColumnCount = 3;
      this.tlpBuildingPOC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpBuildingPOC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpBuildingPOC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 100f));
      this.tlpBuildingPOC.Controls.Add((Control) this.lblPOC, 0, 0);
      this.tlpBuildingPOC.Controls.Add((Control) this.lblPOCEmail, 0, 2);
      this.tlpBuildingPOC.Controls.Add((Control) this.lblPOCPhone, 0, 1);
      this.tlpBuildingPOC.Controls.Add((Control) this.txtPOC, 1, 0);
      this.tlpBuildingPOC.Controls.Add((Control) this.txtPOCEmail, 1, 2);
      this.tlpBuildingPOC.Controls.Add((Control) this.txtPOCPhone, 1, 1);
      this.tlpBuildingPOC.Dock = DockStyle.Fill;
      this.tlpBuildingPOC.Location = new Point(3, 16);
      this.tlpBuildingPOC.Name = "tlpBuildingPOC";
      this.tlpBuildingPOC.RowCount = 3;
      this.tlpBuildingPOC.RowStyles.Add(new RowStyle());
      this.tlpBuildingPOC.RowStyles.Add(new RowStyle());
      this.tlpBuildingPOC.RowStyles.Add(new RowStyle());
      this.tlpBuildingPOC.Size = new Size(713, 78);
      this.tlpBuildingPOC.TabIndex = 44;
      this.lblPOC.AutoSize = true;
      this.lblPOC.BackColor = SystemColors.Control;
      this.lblPOC.Cursor = Cursors.Default;
      this.lblPOC.Dock = DockStyle.Fill;
      this.lblPOC.ForeColor = SystemColors.ControlText;
      this.lblPOC.Location = new Point(3, 0);
      this.lblPOC.Name = "lblPOC";
      this.lblPOC.RightToLeft = RightToLeft.No;
      this.lblPOC.Size = new Size(41, 26);
      this.lblPOC.TabIndex = 41;
      this.lblPOC.Text = "Name:";
      this.lblPOC.TextAlign = ContentAlignment.MiddleRight;
      this.lblPOCEmail.AutoSize = true;
      this.lblPOCEmail.BackColor = SystemColors.Control;
      this.lblPOCEmail.Cursor = Cursors.Default;
      this.lblPOCEmail.Dock = DockStyle.Fill;
      this.lblPOCEmail.ForeColor = SystemColors.ControlText;
      this.lblPOCEmail.Location = new Point(3, 52);
      this.lblPOCEmail.Name = "lblPOCEmail";
      this.lblPOCEmail.RightToLeft = RightToLeft.No;
      this.lblPOCEmail.Size = new Size(41, 26);
      this.lblPOCEmail.TabIndex = 43;
      this.lblPOCEmail.Text = "E-Mail:";
      this.lblPOCEmail.TextAlign = ContentAlignment.MiddleRight;
      this.lblPOCPhone.AutoSize = true;
      this.lblPOCPhone.BackColor = SystemColors.Control;
      this.lblPOCPhone.Cursor = Cursors.Default;
      this.lblPOCPhone.Dock = DockStyle.Fill;
      this.lblPOCPhone.ForeColor = SystemColors.ControlText;
      this.lblPOCPhone.Location = new Point(3, 26);
      this.lblPOCPhone.Name = "lblPOCPhone";
      this.lblPOCPhone.RightToLeft = RightToLeft.No;
      this.lblPOCPhone.Size = new Size(41, 26);
      this.lblPOCPhone.TabIndex = 42;
      this.lblPOCPhone.Text = "Phone:";
      this.lblPOCPhone.TextAlign = ContentAlignment.MiddleRight;
      this.frmBuildingAddress.AutoSize = true;
      this.frmBuildingAddress.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.frmBuildingAddress.BackColor = SystemColors.Control;
      this.tlpBuildingInfo.SetColumnSpan((Control) this.frmBuildingAddress, 5);
      this.frmBuildingAddress.Controls.Add((Control) this.tlpBuildingAddress);
      this.frmBuildingAddress.Dock = DockStyle.Fill;
      this.frmBuildingAddress.ForeColor = SystemColors.ControlText;
      this.frmBuildingAddress.Location = new Point(3, 187);
      this.frmBuildingAddress.Name = "frmBuildingAddress";
      this.frmBuildingAddress.RightToLeft = RightToLeft.No;
      this.frmBuildingAddress.Size = new Size(719, 97);
      this.frmBuildingAddress.TabIndex = 35;
      this.frmBuildingAddress.TabStop = false;
      this.frmBuildingAddress.Text = "Address";
      this.tlpBuildingAddress.AutoSize = true;
      this.tlpBuildingAddress.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.tlpBuildingAddress.ColumnCount = 5;
      this.tlpBuildingAddress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpBuildingAddress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpBuildingAddress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpBuildingAddress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpBuildingAddress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 100f));
      this.tlpBuildingAddress.Controls.Add((Control) this.lblZipCode, 2, 2);
      this.tlpBuildingAddress.Controls.Add((Control) this.lblState, 0, 2);
      this.tlpBuildingAddress.Controls.Add((Control) this.lblCity, 0, 1);
      this.tlpBuildingAddress.Controls.Add((Control) this.lblAddress, 0, 0);
      this.tlpBuildingAddress.Controls.Add((Control) this.txtState, 1, 2);
      this.tlpBuildingAddress.Controls.Add((Control) this.txtZipCode, 3, 2);
      this.tlpBuildingAddress.Controls.Add((Control) this.txtAddress, 1, 0);
      this.tlpBuildingAddress.Controls.Add((Control) this.txtCity, 1, 1);
      this.tlpBuildingAddress.Dock = DockStyle.Fill;
      this.tlpBuildingAddress.Location = new Point(3, 16);
      this.tlpBuildingAddress.Name = "tlpBuildingAddress";
      this.tlpBuildingAddress.RowCount = 3;
      this.tlpBuildingAddress.RowStyles.Add(new RowStyle());
      this.tlpBuildingAddress.RowStyles.Add(new RowStyle());
      this.tlpBuildingAddress.RowStyles.Add(new RowStyle());
      this.tlpBuildingAddress.Size = new Size(713, 78);
      this.tlpBuildingAddress.TabIndex = 40;
      this.lblZipCode.AutoSize = true;
      this.lblZipCode.BackColor = SystemColors.Control;
      this.lblZipCode.Cursor = Cursors.Default;
      this.lblZipCode.Dock = DockStyle.Fill;
      this.lblZipCode.ForeColor = SystemColors.ControlText;
      this.lblZipCode.Location = new Point(128, 52);
      this.lblZipCode.Name = "lblZipCode";
      this.lblZipCode.RightToLeft = RightToLeft.No;
      this.lblZipCode.Size = new Size(53, 26);
      this.lblZipCode.TabIndex = 39;
      this.lblZipCode.Text = "Zip Code:";
      this.lblZipCode.TextAlign = ContentAlignment.MiddleRight;
      this.lblState.AutoSize = true;
      this.lblState.BackColor = SystemColors.Control;
      this.lblState.Cursor = Cursors.Default;
      this.lblState.Dock = DockStyle.Fill;
      this.lblState.ForeColor = SystemColors.ControlText;
      this.lblState.Location = new Point(3, 52);
      this.lblState.Name = "lblState";
      this.lblState.RightToLeft = RightToLeft.No;
      this.lblState.Size = new Size(64, 26);
      this.lblState.TabIndex = 38;
      this.lblState.Text = "State:";
      this.lblState.TextAlign = ContentAlignment.MiddleRight;
      this.lblCity.AutoSize = true;
      this.lblCity.BackColor = SystemColors.Control;
      this.lblCity.Cursor = Cursors.Default;
      this.lblCity.Dock = DockStyle.Fill;
      this.lblCity.ForeColor = SystemColors.ControlText;
      this.lblCity.Location = new Point(3, 26);
      this.lblCity.Name = "lblCity";
      this.lblCity.RightToLeft = RightToLeft.No;
      this.lblCity.Size = new Size(64, 26);
      this.lblCity.TabIndex = 37;
      this.lblCity.Text = "City:";
      this.lblCity.TextAlign = ContentAlignment.MiddleRight;
      this.lblAddress.AutoSize = true;
      this.lblAddress.BackColor = SystemColors.Control;
      this.lblAddress.Cursor = Cursors.Default;
      this.lblAddress.Dock = DockStyle.Fill;
      this.lblAddress.ForeColor = SystemColors.ControlText;
      this.lblAddress.Location = new Point(3, 0);
      this.lblAddress.Name = "lblAddress";
      this.lblAddress.RightToLeft = RightToLeft.No;
      this.lblAddress.Size = new Size(64, 26);
      this.lblAddress.TabIndex = 36;
      this.lblAddress.Text = "St. Address:";
      this.lblAddress.TextAlign = ContentAlignment.MiddleRight;
      this.txtBuildingNumber.AcceptsReturn = true;
      this.txtBuildingNumber.BackColor = SystemColors.Window;
      this.txtBuildingNumber.Cursor = Cursors.IBeam;
      this.txtBuildingNumber.Dock = DockStyle.Fill;
      this.txtBuildingNumber.ForeColor = SystemColors.WindowText;
      this.txtBuildingNumber.Location = new Point(95, 3);
      this.txtBuildingNumber.MaxLength = 0;
      this.txtBuildingNumber.Name = "txtBuildingNumber";
      this.txtBuildingNumber.RightToLeft = RightToLeft.No;
      this.txtBuildingNumber.Size = new Size(100, 20);
      this.txtBuildingNumber.TabIndex = 9;
      this.lblBldgSF.AutoSize = true;
      this.lblBldgSF.BackColor = SystemColors.Control;
      this.lblBldgSF.Cursor = Cursors.Default;
      this.lblBldgSF.Dock = DockStyle.Fill;
      this.lblBldgSF.ForeColor = SystemColors.ControlText;
      this.lblBldgSF.Location = new Point(201, 80);
      this.lblBldgSF.Name = "lblBldgSF";
      this.lblBldgSF.RightToLeft = RightToLeft.No;
      this.lblBldgSF.Size = new Size(205, 26);
      this.lblBldgSF.TabIndex = 129;
      this.lblBldgSF.Text = "SF";
      this.lblBldgSF.TextAlign = ContentAlignment.MiddleLeft;
      this.lblBldgArea.AutoSize = true;
      this.lblBldgArea.BackColor = SystemColors.Control;
      this.lblBldgArea.Cursor = Cursors.Default;
      this.lblBldgArea.Dock = DockStyle.Fill;
      this.lblBldgArea.ForeColor = SystemColors.ControlText;
      this.lblBldgArea.Location = new Point(3, 80);
      this.lblBldgArea.Name = "lblBldgArea";
      this.lblBldgArea.RightToLeft = RightToLeft.No;
      this.lblBldgArea.Size = new Size(86, 26);
      this.lblBldgArea.TabIndex = 128;
      this.lblBldgArea.Text = "Quantity:";
      this.lblBldgArea.TextAlign = ContentAlignment.MiddleRight;
      this.lblNoFloors.AutoSize = true;
      this.lblNoFloors.BackColor = SystemColors.Control;
      this.lblNoFloors.Cursor = Cursors.Default;
      this.lblNoFloors.Dock = DockStyle.Fill;
      this.lblNoFloors.ForeColor = SystemColors.ControlText;
      this.lblNoFloors.Location = new Point(95, 132);
      this.lblNoFloors.Name = "lblNoFloors";
      this.lblNoFloors.RightToLeft = RightToLeft.No;
      this.lblNoFloors.Size = new Size(100, 26);
      this.lblNoFloors.TabIndex = 34;
      this.lblNoFloors.Text = "No. Floors:";
      this.lblNoFloors.TextAlign = ContentAlignment.MiddleRight;
      this.lblYearBuilt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.lblYearBuilt.AutoSize = true;
      this.lblYearBuilt.BackColor = SystemColors.Control;
      this.lblYearBuilt.Cursor = Cursors.Default;
      this.lblYearBuilt.ForeColor = SystemColors.ControlText;
      this.lblYearBuilt.Location = new Point(34, 106);
      this.lblYearBuilt.Name = "lblYearBuilt";
      this.lblYearBuilt.RightToLeft = RightToLeft.No;
      this.lblYearBuilt.Size = new Size(55, 26);
      this.lblYearBuilt.TabIndex = 33;
      this.lblYearBuilt.Text = "Year Built:";
      this.lblYearBuilt.TextAlign = ContentAlignment.MiddleRight;
      this.lblConstructionType.AutoSize = true;
      this.lblConstructionType.BackColor = SystemColors.Control;
      this.lblConstructionType.Cursor = Cursors.Default;
      this.lblConstructionType.Dock = DockStyle.Fill;
      this.lblConstructionType.ForeColor = SystemColors.ControlText;
      this.lblConstructionType.Location = new Point(3, 53);
      this.lblConstructionType.Name = "lblConstructionType";
      this.lblConstructionType.RightToLeft = RightToLeft.No;
      this.lblConstructionType.Size = new Size(86, 27);
      this.lblConstructionType.TabIndex = 25;
      this.lblConstructionType.Text = "Const. Type:";
      this.lblConstructionType.TextAlign = ContentAlignment.MiddleRight;
      this.lblCategoryCode.AutoSize = true;
      this.lblCategoryCode.BackColor = SystemColors.Control;
      this.lblCategoryCode.Cursor = Cursors.Default;
      this.lblCategoryCode.Dock = DockStyle.Fill;
      this.lblCategoryCode.ForeColor = SystemColors.ControlText;
      this.lblCategoryCode.Location = new Point(3, 26);
      this.lblCategoryCode.Name = "lblCategoryCode";
      this.lblCategoryCode.RightToLeft = RightToLeft.No;
      this.lblCategoryCode.Size = new Size(86, 27);
      this.lblCategoryCode.TabIndex = 10;
      this.lblCategoryCode.Text = "Building Use:";
      this.lblCategoryCode.TextAlign = ContentAlignment.MiddleRight;
      this.lblBuildingID.AutoSize = true;
      this.lblBuildingID.BackColor = SystemColors.Control;
      this.lblBuildingID.Cursor = Cursors.Default;
      this.lblBuildingID.Dock = DockStyle.Fill;
      this.lblBuildingID.ForeColor = SystemColors.ControlText;
      this.lblBuildingID.Location = new Point(3, 0);
      this.lblBuildingID.Name = "lblBuildingID";
      this.lblBuildingID.RightToLeft = RightToLeft.No;
      this.lblBuildingID.Size = new Size(86, 26);
      this.lblBuildingID.TabIndex = 8;
      this.lblBuildingID.Text = "Building ID:";
      this.lblBuildingID.TextAlign = ContentAlignment.MiddleRight;
      this.lblSecQtyValue.AutoSize = true;
      this.lblSecQtyValue.Dock = DockStyle.Fill;
      this.lblSecQtyValue.Location = new Point(3, 0);
      this.lblSecQtyValue.MinimumSize = new Size(28, 17);
      this.lblSecQtyValue.Name = "lblSecQtyValue";
      this.lblSecQtyValue.Size = new Size(28, 17);
      this.lblSecQtyValue.TabIndex = 8;
      this.lblSecQtyValue.TextAlign = ContentAlignment.MiddleRight;
      this.frmMethod.AutoSize = true;
      this.frmMethod.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.frmMethod.BackColor = SystemColors.Control;
      this.frmMethod.Controls.Add((Control) this.flpInspMethod);
      this.frmMethod.Dock = DockStyle.Fill;
      this.frmMethod.ForeColor = SystemColors.ControlText;
      this.frmMethod.Location = new Point(500, 59);
      this.frmMethod.Name = "frmMethod";
      this.frmMethod.RightToLeft = RightToLeft.No;
      this.frmMethod.Size = new Size(222, 88);
      this.frmMethod.TabIndex = 106;
      this.frmMethod.TabStop = false;
      this.frmMethod.Text = "Method";
      this.flpInspMethod.AutoSize = true;
      this.flpInspMethod.Controls.Add((Control) this.optMethod_0);
      this.flpInspMethod.Controls.Add((Control) this.optMethod_1);
      this.flpInspMethod.Dock = DockStyle.Fill;
      this.flpInspMethod.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
      this.flpInspMethod.Location = new Point(3, 16);
      this.flpInspMethod.Margin = new Padding(2, 2, 2, 2);
      this.flpInspMethod.Name = "flpInspMethod";
      this.flpInspMethod.Size = new Size(216, 69);
      this.flpInspMethod.TabIndex = 109;
      this.optMethod_0.AutoSize = true;
      this.optMethod_0.BackColor = SystemColors.Control;
      this.optMethod_0.Cursor = Cursors.Default;
      this.optMethod_0.Dock = DockStyle.Fill;
      this.optMethod_0.ForeColor = SystemColors.ControlText;
      this.optMethod_0.Location = new Point(3, 3);
      this.optMethod_0.Name = "optMethod_0";
      this.optMethod_0.RightToLeft = RightToLeft.No;
      this.optMethod_0.Size = new Size(88, 17);
      this.optMethod_0.TabIndex = 108;
      this.optMethod_0.TabStop = true;
      this.optMethod_0.Text = "Not Sampling";
      this.optMethod_0.UseVisualStyleBackColor = false;
      this.optMethod_1.AutoSize = true;
      this.optMethod_1.BackColor = SystemColors.Control;
      this.optMethod_1.Cursor = Cursors.Default;
      this.optMethod_1.Dock = DockStyle.Fill;
      this.optMethod_1.ForeColor = SystemColors.ControlText;
      this.optMethod_1.Location = new Point(3, 26);
      this.optMethod_1.Name = "optMethod_1";
      this.optMethod_1.RightToLeft = RightToLeft.No;
      this.optMethod_1.Size = new Size(88, 17);
      this.optMethod_1.TabIndex = 107;
      this.optMethod_1.TabStop = true;
      this.optMethod_1.Text = "Sampling";
      this.optMethod_1.UseVisualStyleBackColor = false;
      this.frmRatingType.AutoSize = true;
      this.frmRatingType.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.frmRatingType.Controls.Add((Control) this.flpInspType);
      this.frmRatingType.Dock = DockStyle.Fill;
      this.frmRatingType.Location = new Point(273, 59);
      this.frmRatingType.Name = "frmRatingType";
      this.frmRatingType.RightToLeft = RightToLeft.No;
      this.frmRatingType.Size = new Size(221, 88);
      this.frmRatingType.TabIndex = 105;
      this.frmRatingType.TabStop = false;
      this.frmRatingType.Text = "Type";
      this.flpInspType.AutoSize = true;
      this.flpInspType.Controls.Add((Control) this.optRatingType_1);
      this.flpInspType.Controls.Add((Control) this.optRatingType_2);
      this.flpInspType.Controls.Add((Control) this.optRatingType_3);
      this.flpInspType.Dock = DockStyle.Fill;
      this.flpInspType.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
      this.flpInspType.Location = new Point(3, 16);
      this.flpInspType.Margin = new Padding(2, 2, 2, 2);
      this.flpInspType.Name = "flpInspType";
      this.flpInspType.Size = new Size(215, 69);
      this.flpInspType.TabIndex = 74;
      this.flpInspType.WrapContents = false;
      this.optRatingType_1.AutoSize = true;
      this.optRatingType_1.BackColor = SystemColors.Control;
      this.optRatingType_1.Cursor = Cursors.Default;
      this.optRatingType_1.Dock = DockStyle.Fill;
      this.optRatingType_1.ForeColor = SystemColors.ControlText;
      this.optRatingType_1.Location = new Point(3, 3);
      this.optRatingType_1.Name = "optRatingType_1";
      this.optRatingType_1.RightToLeft = RightToLeft.No;
      this.optRatingType_1.Size = new Size(98, 17);
      this.optRatingType_1.TabIndex = 72;
      this.optRatingType_1.TabStop = true;
      this.optRatingType_1.Text = "Distress Survey";
      this.optRatingType_1.UseVisualStyleBackColor = false;
      this.optRatingType_2.AutoSize = true;
      this.optRatingType_2.BackColor = SystemColors.Control;
      this.optRatingType_2.Cursor = Cursors.Default;
      this.optRatingType_2.Dock = DockStyle.Fill;
      this.optRatingType_2.ForeColor = SystemColors.ControlText;
      this.optRatingType_2.Location = new Point(3, 26);
      this.optRatingType_2.Name = "optRatingType_2";
      this.optRatingType_2.RightToLeft = RightToLeft.No;
      this.optRatingType_2.Size = new Size(98, 17);
      this.optRatingType_2.TabIndex = 73;
      this.optRatingType_2.TabStop = true;
      this.optRatingType_2.Text = "Direct Rating";
      this.optRatingType_2.UseVisualStyleBackColor = false;
      this.optRatingType_3.AutoSize = true;
      this.optRatingType_3.BackColor = SystemColors.Control;
      this.optRatingType_3.Cursor = Cursors.Default;
      this.optRatingType_3.Dock = DockStyle.Fill;
      this.optRatingType_3.ForeColor = SystemColors.ControlText;
      this.optRatingType_3.Location = new Point(3, 49);
      this.optRatingType_3.Name = "optRatingType_3";
      this.optRatingType_3.RightToLeft = RightToLeft.No;
      this.optRatingType_3.Size = new Size(98, 17);
      this.optRatingType_3.TabIndex = 74;
      this.optRatingType_3.TabStop = true;
      this.optRatingType_3.Text = "PM Inspection";
      this.optRatingType_3.UseVisualStyleBackColor = true;
      this.lblSecQtyUM.AutoSize = true;
      this.lblSecQtyUM.BackColor = SystemColors.Control;
      this.lblSecQtyUM.Cursor = Cursors.Default;
      this.lblSecQtyUM.ForeColor = SystemColors.ControlText;
      this.lblSecQtyUM.Location = new Point(37, 0);
      this.lblSecQtyUM.Name = "lblSecQtyUM";
      this.lblSecQtyUM.RightToLeft = RightToLeft.No;
      this.lblSecQtyUM.Size = new Size(21, 13);
      this.lblSecQtyUM.TabIndex = 9;
      this.lblSecQtyUM.Text = "EA";
      this.lblSecQtyUM.TextAlign = ContentAlignment.MiddleLeft;
      this.lblSecQty.AutoSize = true;
      this.lblSecQty.BackColor = SystemColors.Control;
      this.lblSecQty.Cursor = Cursors.Default;
      this.lblSecQty.Dock = DockStyle.Fill;
      this.lblSecQty.ForeColor = SystemColors.ControlText;
      this.lblSecQty.Location = new Point(0, 39);
      this.lblSecQty.Margin = new Padding(0);
      this.lblSecQty.Name = "lblSecQty";
      this.lblSecQty.RightToLeft = RightToLeft.No;
      this.lblSecQty.Size = new Size(92, 17);
      this.lblSecQty.TabIndex = 7;
      this.lblSecQty.Text = "Sec Qty:";
      this.lblSecQty.TextAlign = ContentAlignment.MiddleRight;
      this.txtComponentType.AutoSize = true;
      this.txtComponentType.BackColor = SystemColors.Control;
      this.tlpInspectionInfo.SetColumnSpan((Control) this.txtComponentType, 6);
      this.txtComponentType.Cursor = Cursors.Default;
      this.txtComponentType.Dock = DockStyle.Fill;
      this.txtComponentType.ForeColor = SystemColors.ControlText;
      this.txtComponentType.Location = new Point(95, 26);
      this.txtComponentType.Name = "txtComponentType";
      this.txtComponentType.RightToLeft = RightToLeft.No;
      this.txtComponentType.Size = new Size(627, 13);
      this.txtComponentType.TabIndex = 6;
      this.txtComponentType.Text = "txtComponentType";
      this.txtComponentType.TextAlign = ContentAlignment.MiddleLeft;
      this.lblCmponentType.AutoSize = true;
      this.lblCmponentType.BackColor = SystemColors.Control;
      this.lblCmponentType.Cursor = Cursors.Default;
      this.lblCmponentType.Dock = DockStyle.Fill;
      this.lblCmponentType.ForeColor = SystemColors.ControlText;
      this.lblCmponentType.Location = new Point(0, 26);
      this.lblCmponentType.Margin = new Padding(0);
      this.lblCmponentType.Name = "lblCmponentType";
      this.lblCmponentType.RightToLeft = RightToLeft.No;
      this.lblCmponentType.Size = new Size(92, 13);
      this.lblCmponentType.TabIndex = 5;
      this.lblCmponentType.Text = "Component Type:";
      this.lblCmponentType.TextAlign = ContentAlignment.MiddleRight;
      this.txtMaterialCategory.AutoSize = true;
      this.txtMaterialCategory.BackColor = SystemColors.Control;
      this.tlpInspectionInfo.SetColumnSpan((Control) this.txtMaterialCategory, 6);
      this.txtMaterialCategory.Cursor = Cursors.Default;
      this.txtMaterialCategory.Dock = DockStyle.Fill;
      this.txtMaterialCategory.ForeColor = SystemColors.ControlText;
      this.txtMaterialCategory.Location = new Point(95, 13);
      this.txtMaterialCategory.Name = "txtMaterialCategory";
      this.txtMaterialCategory.RightToLeft = RightToLeft.No;
      this.txtMaterialCategory.Size = new Size(627, 13);
      this.txtMaterialCategory.TabIndex = 4;
      this.txtMaterialCategory.Text = "txtMaterialCategory";
      this.txtMaterialCategory.TextAlign = ContentAlignment.MiddleLeft;
      this.lblMaterialCategory.AutoSize = true;
      this.lblMaterialCategory.BackColor = SystemColors.Control;
      this.lblMaterialCategory.Cursor = Cursors.Default;
      this.lblMaterialCategory.Dock = DockStyle.Fill;
      this.lblMaterialCategory.ForeColor = SystemColors.ControlText;
      this.lblMaterialCategory.Location = new Point(0, 13);
      this.lblMaterialCategory.Margin = new Padding(0);
      this.lblMaterialCategory.Name = "lblMaterialCategory";
      this.lblMaterialCategory.RightToLeft = RightToLeft.No;
      this.lblMaterialCategory.Size = new Size(92, 13);
      this.lblMaterialCategory.TabIndex = 3;
      this.lblMaterialCategory.Text = "Material Category:";
      this.lblMaterialCategory.TextAlign = ContentAlignment.MiddleRight;
      this.lblComponent.AutoSize = true;
      this.lblComponent.BackColor = SystemColors.Control;
      this.lblComponent.Cursor = Cursors.Default;
      this.lblComponent.Dock = DockStyle.Fill;
      this.lblComponent.ForeColor = SystemColors.ControlText;
      this.lblComponent.Location = new Point(0, 0);
      this.lblComponent.Margin = new Padding(0);
      this.lblComponent.Name = "lblComponent";
      this.lblComponent.RightToLeft = RightToLeft.No;
      this.lblComponent.Size = new Size(92, 13);
      this.lblComponent.TabIndex = 1;
      this.lblComponent.Text = "Component:";
      this.lblComponent.TextAlign = ContentAlignment.MiddleRight;
      this.lblNoInspection.AutoSize = true;
      this.lblNoInspection.BackColor = SystemColors.Control;
      this.lblNoInspection.Cursor = Cursors.Default;
      this.lblNoInspection.Dock = DockStyle.Fill;
      this.lblNoInspection.Font = new Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, GraphicsUnit.Point, (byte) 0);
      this.lblNoInspection.ForeColor = SystemColors.ControlText;
      this.lblNoInspection.Location = new Point(0, 0);
      this.lblNoInspection.Name = "lblNoInspection";
      this.lblNoInspection.RightToLeft = RightToLeft.No;
      this.lblNoInspection.Size = new Size(0, 20);
      this.lblNoInspection.TabIndex = 135;
      this.lblNoInspection.TextAlign = ContentAlignment.TopCenter;
      this.lblOpeningMessage.AutoSize = true;
      this.lblOpeningMessage.BackColor = SystemColors.Control;
      this.lblOpeningMessage.Cursor = Cursors.Default;
      this.lblOpeningMessage.ForeColor = SystemColors.ControlText;
      this.lblOpeningMessage.Location = new Point(6, 10);
      this.lblOpeningMessage.Name = "lblOpeningMessage";
      this.lblOpeningMessage.RightToLeft = RightToLeft.No;
      this.lblOpeningMessage.Size = new Size(395, 13);
      this.lblOpeningMessage.TabIndex = 54;
      this.lblOpeningMessage.Text = "Please select a database using File -> Open Inspection Database from main menu.";
      this.lblComponentText.AutoSize = true;
      this.lblComponentText.BackColor = SystemColors.Control;
      this.lblComponentText.Cursor = Cursors.Default;
      this.lblComponentText.ForeColor = SystemColors.ControlText;
      this.lblComponentText.Location = new Point(6, 6);
      this.lblComponentText.Name = "lblComponentText";
      this.lblComponentText.RightToLeft = RightToLeft.No;
      this.lblComponentText.Size = new Size(236, 13);
      this.lblComponentText.TabIndex = 45;
      this.lblComponentText.Text = "There are no editable properties for components.";
      this.lblSystemText.AutoSize = true;
      this.lblSystemText.BackColor = SystemColors.Control;
      this.lblSystemText.Cursor = Cursors.Default;
      this.lblSystemText.ForeColor = SystemColors.ControlText;
      this.lblSystemText.Location = new Point(6, 6);
      this.lblSystemText.Name = "lblSystemText";
      this.lblSystemText.RightToLeft = RightToLeft.No;
      this.lblSystemText.Size = new Size(254, 13);
      this.lblSystemText.TabIndex = 44;
      this.lblSystemText.Text = "There are no editable properties for building systems.";
      this.Timer1.Enabled = true;
      this.pnlLocationInfo.AutoSize = true;
      this.pnlLocationInfo.Controls.Add((Control) this.lblLocationInfo);
      this.pnlLocationInfo.Dock = DockStyle.Fill;
      this.pnlLocationInfo.Location = new Point(0, 0);
      this.pnlLocationInfo.Name = "pnlLocationInfo";
      this.pnlLocationInfo.Size = new Size(725, 498);
      this.pnlLocationInfo.TabIndex = 132;
      this.pnlComponentInfo.AutoSize = true;
      this.pnlComponentInfo.Controls.Add((Control) this.lblComponentText);
      this.pnlComponentInfo.Dock = DockStyle.Fill;
      this.pnlComponentInfo.Location = new Point(0, 0);
      this.pnlComponentInfo.Name = "pnlComponentInfo";
      this.pnlComponentInfo.Size = new Size(725, 498);
      this.pnlComponentInfo.TabIndex = 46;
      this.pnlSystemInfo.AutoSize = true;
      this.pnlSystemInfo.Controls.Add((Control) this.lblSystemText);
      this.pnlSystemInfo.Dock = DockStyle.Fill;
      this.pnlSystemInfo.Location = new Point(0, 0);
      this.pnlSystemInfo.Name = "pnlSystemInfo";
      this.pnlSystemInfo.Size = new Size(725, 498);
      this.pnlSystemInfo.TabIndex = 45;
      this.pnlStartup.AutoSize = true;
      this.pnlStartup.Controls.Add((Control) this.lblOpeningMessage);
      this.pnlStartup.Dock = DockStyle.Fill;
      this.pnlStartup.Location = new Point(0, 0);
      this.pnlStartup.Name = "pnlStartup";
      this.pnlStartup.Size = new Size(725, 498);
      this.pnlStartup.TabIndex = 55;
      this.pnlBuildingInsp.AutoSize = true;
      this.pnlBuildingInsp.Controls.Add((Control) this.lnkFunctionality);
      this.pnlBuildingInsp.Dock = DockStyle.Fill;
      this.pnlBuildingInsp.Location = new Point(0, 0);
      this.pnlBuildingInsp.Name = "pnlBuildingInsp";
      this.pnlBuildingInsp.Size = new Size(725, 498);
      this.pnlBuildingInsp.TabIndex = 126;
      this.lnkFunctionality.AutoSize = true;
      this.lnkFunctionality.Location = new Point(12, 41);
      this.lnkFunctionality.Name = "lnkFunctionality";
      this.lnkFunctionality.Size = new Size(126, 13);
      this.lnkFunctionality.TabIndex = 126;
      this.lnkFunctionality.TabStop = true;
      this.lnkFunctionality.Text = "Building Inspection Panel";
      this.pnlBuildingInfo.AutoScroll = true;
      this.pnlBuildingInfo.AutoSize = true;
      this.pnlBuildingInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.pnlBuildingInfo.Controls.Add((Control) this.tlpBuildingInfo);
      this.pnlBuildingInfo.Dock = DockStyle.Fill;
      this.pnlBuildingInfo.Location = new Point(0, 0);
      this.pnlBuildingInfo.Name = "pnlBuildingInfo";
      this.pnlBuildingInfo.Size = new Size(725, 498);
      this.pnlBuildingInfo.TabIndex = 132;
      this.tlpBuildingInfo.AutoSize = true;
      this.tlpBuildingInfo.ColumnCount = 5;
      this.tlpBuildingInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpBuildingInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpBuildingInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpBuildingInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpBuildingInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 100f));
      this.tlpBuildingInfo.Controls.Add((Control) this.lblBuildingID, 0, 0);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblCategoryCode, 0, 1);
      this.tlpBuildingInfo.Controls.Add((Control) this.cboCatCode, 1, 1);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblConstructionType, 0, 2);
      this.tlpBuildingInfo.Controls.Add((Control) this.cboConstructionType, 1, 2);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblBldgArea, 0, 3);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblBldgSF, 2, 3);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblYearBuilt, 0, 4);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblNoFloors, 0, 5);
      this.tlpBuildingInfo.Controls.Add((Control) this.frmBuildingAddress, 0, 7);
      this.tlpBuildingInfo.Controls.Add((Control) this.frmBuildingPOC, 0, 8);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblAlternateID, 0, 6);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblAlternateIDSource, 2, 6);
      this.tlpBuildingInfo.Controls.Add((Control) this.txtYearBuilt, 1, 4);
      this.tlpBuildingInfo.Controls.Add((Control) this.txtAlternateID, 1, 6);
      this.tlpBuildingInfo.Controls.Add((Control) this.txtAlternateIDSource, 3, 6);
      this.tlpBuildingInfo.Controls.Add((Control) this.txtBldgArea, 1, 3);
      this.tlpBuildingInfo.Controls.Add((Control) this.txtBuildingName, 2, 0);
      this.tlpBuildingInfo.Controls.Add((Control) this.txtBuildingNumber, 1, 0);
      this.tlpBuildingInfo.Controls.Add((Control) this.txtNoFloors, 1, 5);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblYearRenovated, 0, 5);
      this.tlpBuildingInfo.Controls.Add((Control) this.txtYearRenovated, 1, 5);
      this.tlpBuildingInfo.Controls.Add((Control) this.txtDoesNotContain, 3, 3);
      this.tlpBuildingInfo.Controls.Add((Control) this.lblUnableToInspect, 3, 4);
      this.tlpBuildingInfo.Controls.Add((Control) this.rcbInspIssue, 4, 4);
      this.tlpBuildingInfo.Controls.Add((Control) this.btnDoesNotContain, 4, 3);
      this.tlpBuildingInfo.Dock = DockStyle.Fill;
      this.tlpBuildingInfo.Location = new Point(0, 0);
      this.tlpBuildingInfo.Name = "tlpBuildingInfo";
      this.tlpBuildingInfo.RowCount = 10;
      this.tlpBuildingInfo.RowStyles.Add(new RowStyle());
      this.tlpBuildingInfo.RowStyles.Add(new RowStyle());
      this.tlpBuildingInfo.RowStyles.Add(new RowStyle());
      this.tlpBuildingInfo.RowStyles.Add(new RowStyle());
      this.tlpBuildingInfo.RowStyles.Add(new RowStyle());
      this.tlpBuildingInfo.RowStyles.Add(new RowStyle());
      this.tlpBuildingInfo.RowStyles.Add(new RowStyle());
      this.tlpBuildingInfo.RowStyles.Add(new RowStyle());
      this.tlpBuildingInfo.RowStyles.Add(new RowStyle());
      this.tlpBuildingInfo.RowStyles.Add(new RowStyle());
      this.tlpBuildingInfo.Size = new Size(725, 498);
      this.tlpBuildingInfo.TabIndex = 0;
      this.lblAlternateID.AutoSize = true;
      this.lblAlternateID.Dock = DockStyle.Fill;
      this.lblAlternateID.Location = new Point(3, 158);
      this.lblAlternateID.Name = "lblAlternateID";
      this.lblAlternateID.Size = new Size(86, 26);
      this.lblAlternateID.TabIndex = 132;
      this.lblAlternateID.Text = "Alternate ID:";
      this.lblAlternateID.TextAlign = ContentAlignment.MiddleRight;
      this.lblAlternateIDSource.AutoSize = true;
      this.lblAlternateIDSource.Dock = DockStyle.Fill;
      this.lblAlternateIDSource.Location = new Point(201, 158);
      this.lblAlternateIDSource.Name = "lblAlternateIDSource";
      this.lblAlternateIDSource.Size = new Size(205, 26);
      this.lblAlternateIDSource.TabIndex = 134;
      this.lblAlternateIDSource.Text = "Alternate ID Source:";
      this.lblAlternateIDSource.TextAlign = ContentAlignment.MiddleRight;
      this.txtAlternateID.BackColor = SystemColors.Window;
      this.txtAlternateID.Dock = DockStyle.Fill;
      this.txtAlternateID.Location = new Point(95, 161);
      this.txtAlternateID.Name = "txtAlternateID";
      this.txtAlternateID.Size = new Size(100, 20);
      this.txtAlternateID.TabIndex = 133;
      this.txtAlternateIDSource.BackColor = SystemColors.Window;
      this.txtAlternateIDSource.Dock = DockStyle.Fill;
      this.txtAlternateIDSource.Location = new Point(412, 161);
      this.txtAlternateIDSource.Name = "txtAlternateIDSource";
      this.txtAlternateIDSource.Size = new Size(100, 20);
      this.txtAlternateIDSource.TabIndex = 135;
      this.lblYearRenovated.AutoSize = true;
      this.lblYearRenovated.Dock = DockStyle.Fill;
      this.lblYearRenovated.Location = new Point(2, 132);
      this.lblYearRenovated.Margin = new Padding(2, 0, 2, 0);
      this.lblYearRenovated.Name = "lblYearRenovated";
      this.lblYearRenovated.Size = new Size(88, 26);
      this.lblYearRenovated.TabIndex = 136;
      this.lblYearRenovated.Text = "Year Renovated:";
      this.lblYearRenovated.TextAlign = ContentAlignment.MiddleRight;
      this.txtYearRenovated.Dock = DockStyle.Fill;
      this.txtYearRenovated.Location = new Point(411, 134);
      this.txtYearRenovated.Margin = new Padding(2, 2, 2, 2);
      this.txtYearRenovated.Name = "txtYearRenovated";
      this.txtYearRenovated.Size = new Size(102, 20);
      this.txtYearRenovated.TabIndex = 137;
      this.txtDoesNotContain.AutoSize = true;
      this.txtDoesNotContain.Dock = DockStyle.Fill;
      this.txtDoesNotContain.Location = new Point(411, 80);
      this.txtDoesNotContain.Margin = new Padding(2, 0, 2, 0);
      this.txtDoesNotContain.Name = "txtDoesNotContain";
      this.txtDoesNotContain.Size = new Size(102, 26);
      this.txtDoesNotContain.TabIndex = 138;
      this.txtDoesNotContain.Text = "Does Not Contain:";
      this.txtDoesNotContain.TextAlign = ContentAlignment.MiddleRight;
      this.lblUnableToInspect.AutoSize = true;
      this.lblUnableToInspect.Dock = DockStyle.Fill;
      this.lblUnableToInspect.Location = new Point(411, 106);
      this.lblUnableToInspect.Margin = new Padding(2, 0, 2, 0);
      this.lblUnableToInspect.Name = "lblUnableToInspect";
      this.lblUnableToInspect.Size = new Size(102, 26);
      this.lblUnableToInspect.TabIndex = 140;
      this.lblUnableToInspect.Text = "Unable To Inspect:";
      this.lblUnableToInspect.TextAlign = ContentAlignment.MiddleRight;
      this.rcbInspIssue.Dock = DockStyle.Fill;
      this.rcbInspIssue.FormattingEnabled = true;
      this.rcbInspIssue.Location = new Point(517, 108);
      this.rcbInspIssue.Margin = new Padding(2, 2, 2, 2);
      this.rcbInspIssue.Name = "rcbInspIssue";
      this.rcbInspIssue.Size = new Size(206, 21);
      this.rcbInspIssue.TabIndex = 141;
      this.btnDoesNotContain.Dock = DockStyle.Fill;
      this.btnDoesNotContain.Location = new Point(517, 82);
      this.btnDoesNotContain.Margin = new Padding(2, 2, 2, 2);
      this.btnDoesNotContain.Name = "btnDoesNotContain";
      this.btnDoesNotContain.Size = new Size(206, 22);
      this.btnDoesNotContain.TabIndex = 142;
      this.btnDoesNotContain.Text = "Select Systems";
      this.btnDoesNotContain.UseVisualStyleBackColor = true;
      this.pnlSectionInfo.AutoSize = true;
      this.pnlSectionInfo.Controls.Add((Control) this.tbSection);
      this.pnlSectionInfo.Dock = DockStyle.Fill;
      this.pnlSectionInfo.Location = new Point(0, 0);
      this.pnlSectionInfo.Name = "pnlSectionInfo";
      this.pnlSectionInfo.Size = new Size(725, 498);
      this.pnlSectionInfo.TabIndex = 147;
      this.tbSection.BackColor = SystemColors.ControlLightLight;
      this.tbSection.Controls.Add((Control) this.tpSection);
      this.tbSection.Controls.Add((Control) this.tpDetails);
      this.tbSection.DefaultPage = this.tpSection;
      this.tbSection.Dock = DockStyle.Fill;
      this.tbSection.EnableTheming = false;
      this.tbSection.Font = new Font("Microsoft Sans Serif", 7.8f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.tbSection.Location = new Point(0, 0);
      this.tbSection.Name = "tbSection";
      this.tbSection.RootElement.AccessibleDescription = (string) null;
      this.tbSection.RootElement.AccessibleName = (string) null;
      this.tbSection.RootElement.ControlBounds = new Rectangle(0, 0, 725, 519);
      this.tbSection.SelectedPage = this.tpSection;
      this.tbSection.Size = new Size(725, 498);
      this.tbSection.TabIndex = 148;
      this.tpSection.Controls.Add((Control) this.tlpSectionInfo);
      this.tpSection.ItemSize = new SizeF(48f, 21f);
      this.tpSection.Location = new Point(10, 30);
      this.tpSection.Name = "tpSection";
      this.tpSection.Size = new Size(704, 457);
      this.tpSection.Text = "Section";
      this.tlpSectionInfo.AutoSize = true;
      this.tlpSectionInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.tlpSectionInfo.ColumnCount = 4;
      this.tlpSectionInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpSectionInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpSectionInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpSectionInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 100f));
      this.tlpSectionInfo.Controls.Add((Control) this.lblEnergyAuditRequired, 0, 6);
      this.tlpSectionInfo.Controls.Add((Control) this.lblSectionName, 0, 0);
      this.tlpSectionInfo.Controls.Add((Control) this.lblMaterial, 0, 1);
      this.tlpSectionInfo.Controls.Add((Control) this.lblComponentType, 0, 2);
      this.tlpSectionInfo.Controls.Add((Control) this.lblSectionAmount, 0, 3);
      this.tlpSectionInfo.Controls.Add((Control) this.lblSectionYearBuilt, 0, 4);
      this.tlpSectionInfo.Controls.Add((Control) this.lblPainted, 0, 7);
      this.tlpSectionInfo.Controls.Add((Control) this.lblDatePainted, 0, 8);
      this.tlpSectionInfo.Controls.Add((Control) this.lblPaintType, 0, 9);
      this.tlpSectionInfo.Controls.Add((Control) this.FlowLayoutPanel1, 2, 3);
      this.tlpSectionInfo.Controls.Add((Control) this.lblFunctionalArea, 0, 5);
      this.tlpSectionInfo.Controls.Add((Control) this.cboSectionMaterial, 1, 1);
      this.tlpSectionInfo.Controls.Add((Control) this.cboSectionComponentType, 1, 2);
      this.tlpSectionInfo.Controls.Add((Control) this.chkEnergyAuditRequired, 1, 6);
      this.tlpSectionInfo.Controls.Add((Control) this.chkYearEstimated, 2, 4);
      this.tlpSectionInfo.Controls.Add((Control) this.cboSectionPaintType, 1, 9);
      this.tlpSectionInfo.Controls.Add((Control) this.cboSectionName, 1, 0);
      this.tlpSectionInfo.Controls.Add((Control) this.chkPainted, 1, 7);
      this.tlpSectionInfo.Controls.Add((Control) this.dtPainted, 1, 8);
      this.tlpSectionInfo.Controls.Add((Control) this.cboFunctionalArea, 1, 5);
      this.tlpSectionInfo.Controls.Add((Control) this.txtSectionAmount, 1, 3);
      this.tlpSectionInfo.Controls.Add((Control) this.txtSectionYearBuilt, 1, 4);
      this.tlpSectionInfo.Dock = DockStyle.Fill;
      this.tlpSectionInfo.Location = new Point(0, 0);
      this.tlpSectionInfo.Name = "tlpSectionInfo";
      this.tlpSectionInfo.RowCount = 11;
      this.tlpSectionInfo.RowStyles.Add(new RowStyle());
      this.tlpSectionInfo.RowStyles.Add(new RowStyle());
      this.tlpSectionInfo.RowStyles.Add(new RowStyle());
      this.tlpSectionInfo.RowStyles.Add(new RowStyle());
      this.tlpSectionInfo.RowStyles.Add(new RowStyle());
      this.tlpSectionInfo.RowStyles.Add(new RowStyle());
      this.tlpSectionInfo.RowStyles.Add(new RowStyle());
      this.tlpSectionInfo.RowStyles.Add(new RowStyle());
      this.tlpSectionInfo.RowStyles.Add(new RowStyle());
      this.tlpSectionInfo.RowStyles.Add(new RowStyle());
      this.tlpSectionInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
      this.tlpSectionInfo.Size = new Size(704, 457);
      this.tlpSectionInfo.TabIndex = 0;
      this.FlowLayoutPanel1.AutoSize = true;
      this.FlowLayoutPanel1.BackColor = Color.Transparent;
      this.tlpSectionInfo.SetColumnSpan((Control) this.FlowLayoutPanel1, 2);
      this.FlowLayoutPanel1.Controls.Add((Control) this.lblUOM);
      this.FlowLayoutPanel1.Controls.Add((Control) this.cmdIncrease);
      this.FlowLayoutPanel1.Controls.Add((Control) this.cmdDecrease);
      this.FlowLayoutPanel1.Controls.Add((Control) this.cmdCalc);
      this.FlowLayoutPanel1.Dock = DockStyle.Fill;
      this.FlowLayoutPanel1.Location = new Point(208, 81);
      this.FlowLayoutPanel1.Margin = new Padding(0);
      this.FlowLayoutPanel1.Name = "FlowLayoutPanel1";
      this.FlowLayoutPanel1.Size = new Size(496, 25);
      this.FlowLayoutPanel1.TabIndex = 147;
      this.lblUOM.AutoSize = true;
      this.lblUOM.BackColor = Color.Transparent;
      this.lblUOM.Cursor = Cursors.Default;
      this.lblUOM.ForeColor = Color.Black;
      this.lblUOM.Location = new Point(3, 3);
      this.lblUOM.Margin = new Padding(3, 3, 10, 3);
      this.lblUOM.Name = "lblUOM";
      this.lblUOM.RightToLeft = RightToLeft.No;
      this.lblUOM.Size = new Size(0, 13);
      this.lblUOM.TabIndex = 13;
      this.lblUOM.TextAlign = ContentAlignment.MiddleLeft;
      this.lblFunctionalArea.AutoSize = true;
      this.lblFunctionalArea.Dock = DockStyle.Fill;
      this.lblFunctionalArea.Location = new Point(3, 131);
      this.lblFunctionalArea.Name = "lblFunctionalArea";
      this.lblFunctionalArea.Size = new Size(110, 27);
      this.lblFunctionalArea.TabIndex = 148;
      this.lblFunctionalArea.Text = "Functional Area:";
      this.lblFunctionalArea.TextAlign = ContentAlignment.MiddleRight;
      this.tlpSectionInfo.SetColumnSpan((Control) this.cboFunctionalArea, 3);
      this.cboFunctionalArea.Dock = DockStyle.Fill;
      this.cboFunctionalArea.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboFunctionalArea.FormattingEnabled = true;
      this.cboFunctionalArea.Location = new Point(119, 134);
      this.cboFunctionalArea.Name = "cboFunctionalArea";
      this.cboFunctionalArea.Size = new Size(582, 21);
      this.cboFunctionalArea.TabIndex = 149;
      this.cboSectionStatus.Dock = DockStyle.Fill;
      this.cboSectionStatus.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cboSectionStatus.FormattingEnabled = true;
      this.cboSectionStatus.Location = new Point(124, 193);
      this.cboSectionStatus.Name = "cboSectionStatus";
      this.cboSectionStatus.Size = new Size(530, 21);
      this.cboSectionStatus.TabIndex = 151;
      this.tpDetails.BackColor = SystemColors.Control;
      this.tpDetails.Controls.Add((Control) this.rgDetails);
      this.tpDetails.Controls.Add((Control) this.RadCommandBar1);
      this.tpDetails.ItemSize = new SizeF(45f, 21f);
      this.tpDetails.Location = new Point(10, 37);
      this.tpDetails.Name = "tpDetails";
      this.tpDetails.Size = new Size(657, 427);
      this.tpDetails.Text = "Details";
      this.rgDetails.BackColor = SystemColors.ControlLightLight;
      this.rgDetails.Dock = DockStyle.Fill;
      this.rgDetails.Location = new Point(0, 30);
      this.rgDetails.MasterTemplate.EnableAlternatingRowColor = true;
      this.rgDetails.MasterTemplate.EnableGrouping = false;
      this.rgDetails.MasterTemplate.ShowRowHeaderColumn = false;
      this.rgDetails.Name = "rgDetails";
      this.rgDetails.RootElement.AccessibleDescription = (string) null;
      this.rgDetails.RootElement.AccessibleName = (string) null;
      this.rgDetails.RootElement.ControlBounds = new Rectangle(0, 30, 657, 397);
      this.rgDetails.Size = new Size(657, 397);
      this.rgDetails.TabIndex = 0;
      this.rgDetails.Text = "RadGridView1";
      this.RadCommandBar1.BackColor = SystemColors.ControlLightLight;
      this.RadCommandBar1.Dock = DockStyle.Top;
      this.RadCommandBar1.Location = new Point(0, 0);
      this.RadCommandBar1.Name = "RadCommandBar1";
      this.RadCommandBar1.RootElement.AccessibleDescription = (string) null;
      this.RadCommandBar1.RootElement.AccessibleName = (string) null;
      this.RadCommandBar1.RootElement.ControlBounds = new Rectangle(0, 0, 657, 30);
      this.RadCommandBar1.Rows.AddRange(this.CommandBarRowElement1);
      this.RadCommandBar1.Size = new Size(657, 30);
      this.RadCommandBar1.TabIndex = 0;
      this.RadCommandBar1.Text = "Add Section Details";
      this.CommandBarRowElement1.MinSize = new Size(25, 25);
      this.CommandBarRowElement1.Strips.AddRange(this.CommandBarStripElement1);
      this.CommandBarStripElement1.DisplayName = "CommandBarStripElement1";
      this.CommandBarStripElement1.Grip.AccessibleDescription = (string) null;
      this.CommandBarStripElement1.Grip.AccessibleName = (string) null;
      this.CommandBarStripElement1.Grip.DisabledTextRenderingHint = TextRenderingHint.SystemDefault;
      this.CommandBarStripElement1.Grip.TextRenderingHint = TextRenderingHint.SystemDefault;
      this.CommandBarStripElement1.Grip.Visibility = ElementVisibility.Hidden;
      this.CommandBarStripElement1.Items.AddRange((RadCommandBarBaseItem) this.cmdNewDetail);
      this.CommandBarStripElement1.Name = "CommandBarStripElement1";
      this.CommandBarStripElement1.OverflowButton.AccessibleDescription = (string) null;
      this.CommandBarStripElement1.OverflowButton.AccessibleName = (string) null;
      this.CommandBarStripElement1.OverflowButton.DisabledTextRenderingHint = TextRenderingHint.SystemDefault;
      this.CommandBarStripElement1.OverflowButton.TextRenderingHint = TextRenderingHint.SystemDefault;
      this.CommandBarStripElement1.Text = "";
      this.cmdNewDetail.AccessibleDescription = "cmdNewDetail";
      this.cmdNewDetail.AccessibleName = "cmdNewDetail";
      this.cmdNewDetail.DisplayName = "cmdNewDetail";
      this.cmdNewDetail.DrawText = true;
      this.cmdNewDetail.Image = (Image) BuilderRED.My.Resources.Resources.Symbol_Add_2;
      this.cmdNewDetail.ImageAlignment = ContentAlignment.MiddleLeft;
      this.cmdNewDetail.Name = "cmdNewDetail";
      this.cmdNewDetail.Text = "Add Detail";
      this.cmdNewDetail.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.pnlInspectionInfo.AutoSize = true;
      this.pnlInspectionInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.pnlInspectionInfo.Controls.Add((Control) this.tlpInspectionInfo);
      this.pnlInspectionInfo.Dock = DockStyle.Fill;
      this.pnlInspectionInfo.Location = new Point(0, 0);
      this.pnlInspectionInfo.Name = "pnlInspectionInfo";
      this.pnlInspectionInfo.Size = new Size(725, 498);
      this.pnlInspectionInfo.TabIndex = 138;
      this.tlpInspectionInfo.AutoSize = true;
      this.tlpInspectionInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.tlpInspectionInfo.ColumnCount = 7;
      this.tlpInspectionInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpInspectionInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpInspectionInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpInspectionInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpInspectionInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpInspectionInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 50f));
      this.tlpInspectionInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 50f));
      this.tlpInspectionInfo.Controls.Add((Control) this.lblComponent, 0, 0);
      this.tlpInspectionInfo.Controls.Add((Control) this.txtComponent, 1, 0);
      this.tlpInspectionInfo.Controls.Add((Control) this.lblMaterialCategory, 0, 1);
      this.tlpInspectionInfo.Controls.Add((Control) this.txtMaterialCategory, 1, 1);
      this.tlpInspectionInfo.Controls.Add((Control) this.lblCmponentType, 0, 2);
      this.tlpInspectionInfo.Controls.Add((Control) this.txtComponentType, 1, 2);
      this.tlpInspectionInfo.Controls.Add((Control) this.lblInspectionDate, 0, 4);
      this.tlpInspectionInfo.Controls.Add((Control) this.lblSecQty, 0, 3);
      this.tlpInspectionInfo.Controls.Add((Control) this.tlpSecQty, 1, 3);
      this.tlpInspectionInfo.Controls.Add((Control) this.flpInspectionDate, 1, 4);
      this.tlpInspectionInfo.Controls.Add((Control) this.frmRatingType, 5, 4);
      this.tlpInspectionInfo.Controls.Add((Control) this.frmMethod, 6, 4);
      this.tlpInspectionInfo.Controls.Add((Control) this.frmLocation, 0, 5);
      this.tlpInspectionInfo.Controls.Add((Control) this.pnlAssessments, 0, 6);
      this.tlpInspectionInfo.Dock = DockStyle.Fill;
      this.tlpInspectionInfo.Location = new Point(0, 0);
      this.tlpInspectionInfo.Name = "tlpInspectionInfo";
      this.tlpInspectionInfo.RowCount = 7;
      this.tlpInspectionInfo.RowStyles.Add(new RowStyle());
      this.tlpInspectionInfo.RowStyles.Add(new RowStyle());
      this.tlpInspectionInfo.RowStyles.Add(new RowStyle());
      this.tlpInspectionInfo.RowStyles.Add(new RowStyle());
      this.tlpInspectionInfo.RowStyles.Add(new RowStyle());
      this.tlpInspectionInfo.RowStyles.Add(new RowStyle());
      this.tlpInspectionInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
      this.tlpInspectionInfo.Size = new Size(725, 498);
      this.tlpInspectionInfo.TabIndex = 0;
      this.lblInspectionDate.AutoSize = true;
      this.lblInspectionDate.BackColor = SystemColors.Control;
      this.lblInspectionDate.Cursor = Cursors.Default;
      this.lblInspectionDate.Dock = DockStyle.Fill;
      this.lblInspectionDate.ForeColor = SystemColors.ControlText;
      this.lblInspectionDate.Location = new Point(0, 56);
      this.lblInspectionDate.Margin = new Padding(0);
      this.lblInspectionDate.Name = "lblInspectionDate";
      this.lblInspectionDate.RightToLeft = RightToLeft.No;
      this.lblInspectionDate.Size = new Size(92, 94);
      this.lblInspectionDate.TabIndex = 10;
      this.lblInspectionDate.Text = "Insp. Date:";
      this.lblInspectionDate.TextAlign = ContentAlignment.TopRight;
      this.tlpSecQty.AutoSize = true;
      this.tlpSecQty.ColumnCount = 2;
      this.tlpInspectionInfo.SetColumnSpan((Control) this.tlpSecQty, 3);
      this.tlpSecQty.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpSecQty.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpSecQty.Controls.Add((Control) this.lblSecQtyValue, 0, 0);
      this.tlpSecQty.Controls.Add((Control) this.lblSecQtyUM, 1, 0);
      this.tlpSecQty.Dock = DockStyle.Fill;
      this.tlpSecQty.Location = new Point(92, 39);
      this.tlpSecQty.Margin = new Padding(0);
      this.tlpSecQty.Name = "tlpSecQty";
      this.tlpSecQty.RowCount = 1;
      this.tlpSecQty.RowStyles.Add(new RowStyle());
      this.tlpSecQty.RowStyles.Add(new RowStyle(SizeType.Absolute, 17f));
      this.tlpSecQty.Size = new Size(61, 17);
      this.tlpSecQty.TabIndex = 148;
      this.flpInspectionDate.AutoSize = true;
      this.tlpInspectionInfo.SetColumnSpan((Control) this.flpInspectionDate, 4);
      this.flpInspectionDate.Controls.Add((Control) this.cboInspectionDates);
      this.flpInspectionDate.Controls.Add((Control) this.cmdNewInspection);
      this.flpInspectionDate.Controls.Add((Control) this.cmdCopyInspection);
      this.flpInspectionDate.Controls.Add((Control) this.cmdDeleteInspection);
      this.flpInspectionDate.Dock = DockStyle.Fill;
      this.flpInspectionDate.Location = new Point(94, 58);
      this.flpInspectionDate.Margin = new Padding(2, 2, 2, 2);
      this.flpInspectionDate.Name = "flpInspectionDate";
      this.flpInspectionDate.Size = new Size(174, 90);
      this.flpInspectionDate.TabIndex = 150;
      this.flpInspectionDate.WrapContents = false;
      this.pnlAssessments.AutoSize = true;
      this.tlpInspectionInfo.SetColumnSpan((Control) this.pnlAssessments, 7);
      this.pnlAssessments.Controls.Add((Control) this.pnlTaskList);
      this.pnlAssessments.Controls.Add((Control) this.frmDistressSurvey);
      this.pnlAssessments.Controls.Add((Control) this.frmDirectRating);
      this.pnlAssessments.Controls.Add((Control) this.lblNoInspection);
      this.pnlAssessments.Dock = DockStyle.Fill;
      this.pnlAssessments.Location = new Point(3, 227);
      this.pnlAssessments.Name = "pnlAssessments";
      this.pnlAssessments.Size = new Size(719, 268);
      this.pnlAssessments.TabIndex = 137;
      this.pnlTaskList.Controls.Add((Control) this.ShowTasksButton);
      this.pnlTaskList.Location = new Point(-3, 0);
      this.pnlTaskList.Margin = new Padding(2, 2, 2, 2);
      this.pnlTaskList.Name = "pnlTaskList";
      this.pnlTaskList.Size = new Size(682, 35);
      this.pnlTaskList.TabIndex = 136;
      this.ShowTasksButton.Location = new Point(5, 4);
      this.ShowTasksButton.Margin = new Padding(2, 2, 2, 2);
      this.ShowTasksButton.Name = "ShowTasksButton";
      this.ShowTasksButton.Size = new Size(86, 19);
      this.ShowTasksButton.TabIndex = 137;
      this.ShowTasksButton.Text = "Show Tasks";
      this.ShowTasksButton.UseVisualStyleBackColor = true;
      this.alertFormsUpdate.Opacity = 1f;
      this.alertFormsUpdate.OptionItems.AddRange((RadItem) this.alertFormsHide);
      this.alertFormsUpdate.ThemeName = "CustomTelerikAll";
      this.alertFormsHide.AccessibleDescription = "Disable Alert for this databse";
      this.alertFormsHide.AccessibleName = "Disable Alert for this databse";
      this.alertFormsHide.CheckOnClick = true;
      this.alertFormsHide.Name = "alertFormsHide";
      this.alertFormsHide.PopupDirection = RadDirection.Up;
      this.alertFormsHide.Text = "Disable Alert for this databse";
      this.alertFormsHide.TextSeparatorVisibility = ElementVisibility.Visible;
      this.pnlDetails.AutoSize = true;
      this.pnlDetails.Controls.Add((Control) this.pnlInspectionInfo);
      this.pnlDetails.Controls.Add((Control) this.pnlSectionInfo);
      this.pnlDetails.Controls.Add((Control) this.pnlBuildingInfo);
      this.pnlDetails.Controls.Add((Control) this.pnlFuncAssessment);
      this.pnlDetails.Controls.Add((Control) this.pnlFuncArea);
      this.pnlDetails.Controls.Add((Control) this.pnlFunctionality);
      this.pnlDetails.Controls.Add((Control) this.pnlLocationInfo);
      this.pnlDetails.Controls.Add((Control) this.pnlBuildingInsp);
      this.pnlDetails.Controls.Add((Control) this.pnlSystemInfo);
      this.pnlDetails.Controls.Add((Control) this.pnlStartup);
      this.pnlDetails.Controls.Add((Control) this.pnlComponentInfo);
      this.pnlDetails.Dock = DockStyle.Fill;
      this.pnlDetails.Location = new Point(0, 0);
      this.pnlDetails.Margin = new Padding(0);
      this.pnlDetails.MinimumSize = new Size(600, 400);
      this.pnlDetails.Name = "pnlDetails";
      this.pnlDetails.Size = new Size(725, 498);
      this.pnlDetails.TabIndex = 148;
      this.pnlFuncAssessment.AutoSize = true;
      this.pnlFuncAssessment.Controls.Add((Control) this.ElementHost1);
      this.pnlFuncAssessment.Dock = DockStyle.Fill;
      this.pnlFuncAssessment.Location = new Point(0, 0);
      this.pnlFuncAssessment.Name = "pnlFuncAssessment";
      this.pnlFuncAssessment.Size = new Size(725, 498);
      this.pnlFuncAssessment.TabIndex = 1;
      this.pnlFuncArea.AutoSize = true;
      this.pnlFuncArea.Controls.Add((Control) this.lblFuncArea);
      this.pnlFuncArea.Dock = DockStyle.Fill;
      this.pnlFuncArea.Location = new Point(0, 0);
      this.pnlFuncArea.Name = "pnlFuncArea";
      this.pnlFuncArea.Size = new Size(725, 498);
      this.pnlFuncArea.TabIndex = 133;
      this.lblFuncArea.AutoSize = true;
      this.lblFuncArea.Location = new Point(88, 151);
      this.lblFuncArea.Name = "lblFuncArea";
      this.lblFuncArea.Size = new Size(111, 13);
      this.lblFuncArea.TabIndex = 1;
      this.lblFuncArea.Text = "Functional Area Panel";
      this.pnlFunctionality.AutoSize = true;
      this.pnlFunctionality.Dock = DockStyle.Fill;
      this.pnlFunctionality.Location = new Point(0, 0);
      this.pnlFunctionality.Name = "pnlFunctionality";
      this.pnlFunctionality.Size = new Size(725, 498);
      this.pnlFunctionality.TabIndex = 152;
      this.pnlTrees.AutoSize = true;
      this.pnlTrees.Controls.Add((Control) this.tvInspection);
      this.pnlTrees.Controls.Add((Control) this.tvFunctionality);
      this.pnlTrees.Controls.Add((Control) this.tvInventory);
      this.pnlTrees.Dock = DockStyle.Fill;
      this.pnlTrees.Location = new Point(0, 0);
      this.pnlTrees.Margin = new Padding(0);
      this.pnlTrees.Name = "pnlTrees";
      this.pnlTrees.Size = new Size(298, 498);
      this.pnlTrees.TabIndex = 149;
      this.tvFunctionality.Dock = DockStyle.Fill;
      this.tvFunctionality.HideSelection = false;
      this.tvFunctionality.Location = new Point(0, 0);
      this.tvFunctionality.Name = "tvFunctionality";
      this.tvFunctionality.Size = new Size(298, 498);
      this.tvFunctionality.TabIndex = 132;
      this.SplitContainerMain.Dock = DockStyle.Fill;
      this.SplitContainerMain.Location = new Point(0, 42);
      this.SplitContainerMain.Margin = new Padding(0);
      this.SplitContainerMain.Name = "SplitContainerMain";
      this.SplitContainerMain.Panel1.Controls.Add((Control) this.pnlTrees);
      this.SplitContainerMain.Panel1MinSize = 100;
      this.SplitContainerMain.Panel2.Controls.Add((Control) this.pnlDetails);
      this.SplitContainerMain.Panel2MinSize = 600;
      this.SplitContainerMain.Size = new Size(1033, 498);
      this.SplitContainerMain.SplitterDistance = 298;
      this.SplitContainerMain.SplitterWidth = 10;
      this.SplitContainerMain.TabIndex = 151;
      this.ssStatusStrip.ImageScalingSize = new Size(20, 20);
      this.ssStatusStrip.Items.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.tsslStatus,
        (ToolStripItem) this.tsslInspector,
        (ToolStripItem) this.tsslCurrentDate,
        (ToolStripItem) this.tsslCurrentTime
      });
      this.ssStatusStrip.Location = new Point(0, 540);
      this.ssStatusStrip.Name = "ssStatusStrip";
      this.ssStatusStrip.Size = new Size(1033, 22);
      this.ssStatusStrip.TabIndex = 152;
      this.tsslStatus.Name = "tsslStatus";
      this.tsslStatus.Padding = new Padding(0, 0, 50, 0);
      this.tsslStatus.Size = new Size(89, 17);
      this.tsslStatus.Text = "Status";
      this.tsslStatus.TextAlign = ContentAlignment.MiddleLeft;
      this.tsslInspector.Name = "tsslInspector";
      this.tsslInspector.Padding = new Padding(0, 0, 50, 0);
      this.tsslInspector.Size = new Size(152, 17);
      this.tsslInspector.Text = "Current Inspector:";
      this.tsslInspector.TextAlign = ContentAlignment.MiddleLeft;
      this.tsslCurrentDate.Name = "tsslCurrentDate";
      this.tsslCurrentDate.Size = new Size(0, 17);
      this.tsslCurrentTime.Name = "tsslCurrentTime";
      this.tsslCurrentTime.Size = new Size(0, 17);
      this.tsToolbar.GripStyle = ToolStripGripStyle.Hidden;
      this.tsToolbar.ImageScalingSize = new Size(20, 20);
      this.tsToolbar.Items.AddRange(new ToolStripItem[14]
      {
        (ToolStripItem) this.tsddbToggle,
        (ToolStripItem) this.tss1,
        (ToolStripItem) this.tsbTally,
        (ToolStripItem) this.tsbNew1,
        (ToolStripItem) this.tsbNew2,
        (ToolStripItem) this.tsbDelete,
        (ToolStripItem) this.tss2,
        (ToolStripItem) this.tsbEdit,
        (ToolStripItem) this.tsbSave,
        (ToolStripItem) this.tsbCancel,
        (ToolStripItem) this.tss3,
        (ToolStripItem) this.tsbComment,
        (ToolStripItem) this.tsbImages,
        (ToolStripItem) this.tsbWorkItems
      });
      this.tsToolbar.Location = new Point(0, 0);
      this.tsToolbar.Name = "tsToolbar";
      this.tsToolbar.Size = new Size(1033, 42);
      this.tsToolbar.TabIndex = 153;
      this.tsddbToggle.DropDownItems.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.InventoryToolStripMenuItem,
        (ToolStripItem) this.InspectionsToolStripMenuItem,
        (ToolStripItem) this.FunctionalityToolStripMenuItem
      });
      this.tsddbToggle.Image = (Image) BuilderRED.My.Resources.Resources.BRED_Inventory;
      this.tsddbToggle.ImageTransparentColor = Color.Magenta;
      this.tsddbToggle.Name = "tsddbToggle";
      this.tsddbToggle.Size = new Size(70, 39);
      this.tsddbToggle.Text = "Inventory";
      this.tsddbToggle.TextImageRelation = TextImageRelation.ImageAboveText;
      this.InventoryToolStripMenuItem.Image = (Image) BuilderRED.My.Resources.Resources.BRED_Inventory;
      this.InventoryToolStripMenuItem.Name = "InventoryToolStripMenuItem";
      this.InventoryToolStripMenuItem.Size = new Size(143, 22);
      this.InventoryToolStripMenuItem.Text = "Inventory";
      this.InspectionsToolStripMenuItem.Image = (Image) BuilderRED.My.Resources.Resources.BRED_Inspect;
      this.InspectionsToolStripMenuItem.Name = "InspectionsToolStripMenuItem";
      this.InspectionsToolStripMenuItem.Size = new Size(143, 22);
      this.InspectionsToolStripMenuItem.Text = "Inspections";
      this.FunctionalityToolStripMenuItem.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard;
      this.FunctionalityToolStripMenuItem.Name = "FunctionalityToolStripMenuItem";
      this.FunctionalityToolStripMenuItem.Size = new Size(143, 22);
      this.FunctionalityToolStripMenuItem.Text = "Functionality";
      this.tss1.Name = "tss1";
      this.tss1.Size = new Size(6, 42);
      this.tsbTally.Image = (Image) BuilderRED.My.Resources.Resources.BRED_Tally;
      this.tsbTally.ImageTransparentColor = Color.Magenta;
      this.tsbTally.Name = "tsbTally";
      this.tsbTally.Size = new Size(120, 39);
      this.tsbTally.Text = "Inspection Summary";
      this.tsbTally.TextImageRelation = TextImageRelation.ImageAboveText;
      this.tsbTally.Visible = false;
      this.tsbNew1.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard;
      this.tsbNew1.ImageTransparentColor = Color.Magenta;
      this.tsbNew1.Name = "tsbNew1";
      this.tsbNew1.Size = new Size(82, 39);
      this.tsbNew1.Text = "New Building";
      this.tsbNew1.TextImageRelation = TextImageRelation.ImageAboveText;
      this.tsbNew2.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard;
      this.tsbNew2.ImageTransparentColor = Color.Magenta;
      this.tsbNew2.Name = "tsbNew2";
      this.tsbNew2.Size = new Size(76, 39);
      this.tsbNew2.Text = "New System";
      this.tsbNew2.TextImageRelation = TextImageRelation.ImageAboveText;
      this.tsbDelete.Image = (Image) BuilderRED.My.Resources.Resources.Delete;
      this.tsbDelete.ImageTransparentColor = Color.Magenta;
      this.tsbDelete.Name = "tsbDelete";
      this.tsbDelete.Size = new Size(44, 39);
      this.tsbDelete.Text = "Delete";
      this.tsbDelete.TextImageRelation = TextImageRelation.ImageAboveText;
      this.tss2.Name = "tss2";
      this.tss2.Size = new Size(6, 42);
      this.tsbEdit.Image = (Image) BuilderRED.My.Resources.Resources.edit;
      this.tsbEdit.ImageTransparentColor = Color.Magenta;
      this.tsbEdit.Name = "tsbEdit";
      this.tsbEdit.Size = new Size(31, 39);
      this.tsbEdit.Text = "Edit";
      this.tsbEdit.TextImageRelation = TextImageRelation.ImageAboveText;
      this.tsbSave.Image = (Image) BuilderRED.My.Resources.Resources.save;
      this.tsbSave.ImageTransparentColor = Color.Magenta;
      this.tsbSave.Name = "tsbSave";
      this.tsbSave.Size = new Size(35, 39);
      this.tsbSave.Text = "Save";
      this.tsbSave.TextImageRelation = TextImageRelation.ImageAboveText;
      this.tsbCancel.Image = (Image) BuilderRED.My.Resources.Resources.Cancel;
      this.tsbCancel.ImageTransparentColor = Color.Magenta;
      this.tsbCancel.Name = "tsbCancel";
      this.tsbCancel.Size = new Size(47, 39);
      this.tsbCancel.Text = "Cancel";
      this.tsbCancel.TextImageRelation = TextImageRelation.ImageAboveText;
      this.tss3.Name = "tss3";
      this.tss3.Size = new Size(6, 42);
      this.tsbComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard;
      this.tsbComment.ImageTransparentColor = Color.Magenta;
      this.tsbComment.Name = "tsbComment";
      this.tsbComment.Size = new Size(70, 39);
      this.tsbComment.Text = "Comments";
      this.tsbComment.TextImageRelation = TextImageRelation.ImageAboveText;
      this.tsbComment.Visible = false;
      this.tsbImages.Image = (Image) BuilderRED.My.Resources.Resources._03_Link;
      this.tsbImages.ImageTransparentColor = Color.Magenta;
      this.tsbImages.Name = "tsbImages";
      this.tsbImages.Size = new Size(116, 39);
      this.tsbImages.Text = "Images Linked - 000";
      this.tsbImages.TextImageRelation = TextImageRelation.ImageAboveText;
      this.tsbWorkItems.Image = (Image) BuilderRED.My.Resources.Resources.Notepad;
      this.tsbWorkItems.ImageTransparentColor = Color.Magenta;
      this.tsbWorkItems.Name = "tsbWorkItems";
      this.tsbWorkItems.Size = new Size(71, 39);
      this.tsbWorkItems.Text = "Work Items";
      this.tsbWorkItems.TextImageRelation = TextImageRelation.ImageAboveText;
      this.lblSectionStatus.AutoSize = true;
      this.lblSectionStatus.Dock = DockStyle.Fill;
      this.lblSectionStatus.Location = new Point(3, 190);
      this.lblSectionStatus.Name = "lblSectionStatus";
      this.lblSectionStatus.Size = new Size(115, 27);
      this.lblSectionStatus.TabIndex = 150;
      this.lblSectionStatus.Text = "Section Status:";
      this.lblSectionStatus.TextAlign = ContentAlignment.MiddleRight;
      this.ElementHost1.AutoSize = true;
      this.ElementHost1.Dock = DockStyle.Fill;
      this.ElementHost1.Location = new Point(0, 0);
      this.ElementHost1.Name = "ElementHost1";
      this.ElementHost1.Size = new Size(725, 498);
      this.ElementHost1.TabIndex = 0;
      this.ElementHost1.Text = "ElementHost1";
      this.ElementHost1.Child = (System.Windows.UIElement) this.MainPageView1;
      this.AllowDrop = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoSize = true;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(1033, 562);
      this.Controls.Add((Control) this.SplitContainerMain);
      this.Controls.Add((Control) this.tsToolbar);
      this.Controls.Add((Control) this.ssStatusStrip);
      this.Cursor = Cursors.Default;
      this.HelpProvider.SetHelpNavigator((Control) this, HelpNavigator.Topic);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Location = new Point(3, 83);
      this.Menu = this.MainMenu1;
      this.MinimumSize = new Size(956, 498);
      this.Name = nameof (frmMain);
      this.RightToLeft = RightToLeft.No;
      this.HelpProvider.SetShowHelp((Control) this, true);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Builder RED";
      ((ISupportInitialize) this.tvInventory).EndInit();
      ((ISupportInitialize) this.tvInspection).EndInit();
      this.frmLocation.ResumeLayout(false);
      this.frmLocation.PerformLayout();
      this.tlpLocation.ResumeLayout(false);
      this.tlpLocation.PerformLayout();
      this.flpLocation.ResumeLayout(false);
      this.flpLocation.PerformLayout();
      this.frmDistressSurvey.ResumeLayout(false);
      this.frmDistressSurvey.PerformLayout();
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      ((ISupportInitialize) this.ugSubcomponents).EndInit();
      this.flpSubCompData.ResumeLayout(false);
      this.flpSubCompData.PerformLayout();
      this.frmDirectRating.ResumeLayout(false);
      this.frmDirectRating.PerformLayout();
      this.tlpRatings.ResumeLayout(false);
      this.tlpRatings.PerformLayout();
      this.frmDirectComponent.ResumeLayout(false);
      this.frmDirectComponent.PerformLayout();
      this.tlpDirectRating.ResumeLayout(false);
      this.frmDirectPaint.ResumeLayout(false);
      this.frmDirectPaint.PerformLayout();
      this.tlpPaintRating.ResumeLayout(false);
      this.frmBuildingPOC.ResumeLayout(false);
      this.frmBuildingPOC.PerformLayout();
      this.tlpBuildingPOC.ResumeLayout(false);
      this.tlpBuildingPOC.PerformLayout();
      this.frmBuildingAddress.ResumeLayout(false);
      this.frmBuildingAddress.PerformLayout();
      this.tlpBuildingAddress.ResumeLayout(false);
      this.tlpBuildingAddress.PerformLayout();
      this.frmMethod.ResumeLayout(false);
      this.frmMethod.PerformLayout();
      this.flpInspMethod.ResumeLayout(false);
      this.flpInspMethod.PerformLayout();
      this.frmRatingType.ResumeLayout(false);
      this.frmRatingType.PerformLayout();
      this.flpInspType.ResumeLayout(false);
      this.flpInspType.PerformLayout();
      this.pnlLocationInfo.ResumeLayout(false);
      this.pnlComponentInfo.ResumeLayout(false);
      this.pnlComponentInfo.PerformLayout();
      this.pnlSystemInfo.ResumeLayout(false);
      this.pnlSystemInfo.PerformLayout();
      this.pnlStartup.ResumeLayout(false);
      this.pnlStartup.PerformLayout();
      this.pnlBuildingInsp.ResumeLayout(false);
      this.pnlBuildingInsp.PerformLayout();
      this.pnlBuildingInfo.ResumeLayout(false);
      this.pnlBuildingInfo.PerformLayout();
      this.tlpBuildingInfo.ResumeLayout(false);
      this.tlpBuildingInfo.PerformLayout();
      this.pnlSectionInfo.ResumeLayout(false);
      this.tbSection.EndInit();
      this.tpSection.ResumeLayout(false);
      this.tpSection.PerformLayout();
      this.tlpSectionInfo.ResumeLayout(false);
      this.tlpSectionInfo.PerformLayout();
      this.FlowLayoutPanel1.ResumeLayout(false);
      this.FlowLayoutPanel1.PerformLayout();
      this.tpDetails.ResumeLayout(false);
      this.tpDetails.PerformLayout();
      this.rgDetails.MasterTemplate.EndInit();
      this.rgDetails.EndInit();
      this.RadCommandBar1.EndInit();
      this.pnlInspectionInfo.ResumeLayout(false);
      this.pnlInspectionInfo.PerformLayout();
      this.tlpInspectionInfo.ResumeLayout(false);
      this.tlpInspectionInfo.PerformLayout();
      this.tlpSecQty.ResumeLayout(false);
      this.tlpSecQty.PerformLayout();
      this.flpInspectionDate.ResumeLayout(false);
      this.pnlAssessments.ResumeLayout(false);
      this.pnlAssessments.PerformLayout();
      this.pnlTaskList.ResumeLayout(false);
      this.pnlDetails.ResumeLayout(false);
      this.pnlDetails.PerformLayout();
      this.pnlFuncAssessment.ResumeLayout(false);
      this.pnlFuncAssessment.PerformLayout();
      this.pnlFuncArea.ResumeLayout(false);
      this.pnlFuncArea.PerformLayout();
      this.pnlTrees.ResumeLayout(false);
      ((ISupportInitialize) this.tvFunctionality).EndInit();
      this.SplitContainerMain.Panel1.ResumeLayout(false);
      this.SplitContainerMain.Panel1.PerformLayout();
      this.SplitContainerMain.Panel2.ResumeLayout(false);
      this.SplitContainerMain.Panel2.PerformLayout();
      this.SplitContainerMain.EndInit();
      this.SplitContainerMain.ResumeLayout(false);
      this.ssStatusStrip.ResumeLayout(false);
      this.ssStatusStrip.PerformLayout();
      this.tsToolbar.ResumeLayout(false);
      this.tsToolbar.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    internal virtual Panel pnlLocationInfo { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Panel pnlComponentInfo { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Panel pnlSystemInfo { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Panel pnlStartup { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Panel pnlBuildingInsp { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Panel pnlBuildingInfo { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tlpBuildingInfo { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Panel pnlSectionInfo { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tlpSectionInfo { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tlpBuildingAddress { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tlpBuildingPOC { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Panel pnlInspectionInfo { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tlpInspectionInfo { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Panel pnlAssessments { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual System.Windows.Forms.Label lblAlternateID { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual System.Windows.Forms.TextBox txtAlternateID
    {
      get
      {
        return this._txtAlternateID;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.Building_TextChanged);
        EventHandler eventHandler2 = new EventHandler(this.txtAlternateID_Enter);
        System.Windows.Forms.TextBox txtAlternateId1 = this._txtAlternateID;
        if (txtAlternateId1 != null)
        {
          txtAlternateId1.TextChanged -= eventHandler1;
          txtAlternateId1.Enter -= eventHandler2;
        }
        this._txtAlternateID = value;
        System.Windows.Forms.TextBox txtAlternateId2 = this._txtAlternateID;
        if (txtAlternateId2 == null)
          return;
        txtAlternateId2.TextChanged += eventHandler1;
        txtAlternateId2.Enter += eventHandler2;
      }
    }

    internal virtual System.Windows.Forms.Label lblAlternateIDSource { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual System.Windows.Forms.TextBox txtAlternateIDSource
    {
      get
      {
        return this._txtAlternateIDSource;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Building_TextChanged);
        System.Windows.Forms.TextBox alternateIdSource1 = this._txtAlternateIDSource;
        if (alternateIdSource1 != null)
          alternateIdSource1.TextChanged -= eventHandler;
        this._txtAlternateIDSource = value;
        System.Windows.Forms.TextBox alternateIdSource2 = this._txtAlternateIDSource;
        if (alternateIdSource2 == null)
          return;
        alternateIdSource2.TextChanged += eventHandler;
      }
    }

    internal virtual FlowLayoutPanel FlowLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual System.Windows.Forms.Label lblUOM { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button cmdIncrease
    {
      get
      {
        return this._cmdIncrease;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.cmdIncrease_Click);
        EventHandler eventHandler2 = new EventHandler(this.cmdIncrease_GotFocus);
        Button cmdIncrease1 = this._cmdIncrease;
        if (cmdIncrease1 != null)
        {
          cmdIncrease1.Click -= eventHandler1;
          cmdIncrease1.GotFocus -= eventHandler2;
        }
        this._cmdIncrease = value;
        Button cmdIncrease2 = this._cmdIncrease;
        if (cmdIncrease2 == null)
          return;
        cmdIncrease2.Click += eventHandler1;
        cmdIncrease2.GotFocus += eventHandler2;
      }
    }

    internal virtual Button cmdDecrease
    {
      get
      {
        return this._cmdDecrease;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.cmdDecrease_Click);
        EventHandler eventHandler2 = new EventHandler(this.cmdIncrease_GotFocus);
        Button cmdDecrease1 = this._cmdDecrease;
        if (cmdDecrease1 != null)
        {
          cmdDecrease1.Click -= eventHandler1;
          cmdDecrease1.GotFocus -= eventHandler2;
        }
        this._cmdDecrease = value;
        Button cmdDecrease2 = this._cmdDecrease;
        if (cmdDecrease2 == null)
          return;
        cmdDecrease2.Click += eventHandler1;
        cmdDecrease2.GotFocus += eventHandler2;
      }
    }

    internal virtual Button cmdCalc
    {
      get
      {
        return this._cmdCalc;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.cmdCalc_Click);
        EventHandler eventHandler2 = new EventHandler(this.cmdIncrease_GotFocus);
        Button cmdCalc1 = this._cmdCalc;
        if (cmdCalc1 != null)
        {
          cmdCalc1.Click -= eventHandler1;
          cmdCalc1.GotFocus -= eventHandler2;
        }
        this._cmdCalc = value;
        Button cmdCalc2 = this._cmdCalc;
        if (cmdCalc2 == null)
          return;
        cmdCalc2.Click += eventHandler1;
        cmdCalc2.GotFocus += eventHandler2;
      }
    }

    internal virtual LinkLabel lnkFunctionality { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual MenuItem miToolsReports
    {
      get
      {
        return this._miToolsReports;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.miToolsReports_Click);
        MenuItem miToolsReports1 = this._miToolsReports;
        if (miToolsReports1 != null)
          miToolsReports1.Click -= eventHandler;
        this._miToolsReports = value;
        MenuItem miToolsReports2 = this._miToolsReports;
        if (miToolsReports2 == null)
          return;
        miToolsReports2.Click += eventHandler;
      }
    }

    public virtual MenuItem miFileClose
    {
      get
      {
        return this._miFileClose;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.miFileClose_Click);
        MenuItem miFileClose1 = this._miFileClose;
        if (miFileClose1 != null)
          miFileClose1.Click -= eventHandler;
        this._miFileClose = value;
        MenuItem miFileClose2 = this._miFileClose;
        if (miFileClose2 == null)
          return;
        miFileClose2.Click += eventHandler;
      }
    }

    internal virtual TableLayoutPanel tlpLocation { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual System.Windows.Forms.Label lblFunctionalArea { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual System.Windows.Forms.ComboBox cboFunctionalArea
    {
      get
      {
        return this._cboFunctionalArea;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cboFunctionalArea_SelectedIndexChanged);
        System.Windows.Forms.ComboBox cboFunctionalArea1 = this._cboFunctionalArea;
        if (cboFunctionalArea1 != null)
          cboFunctionalArea1.SelectedIndexChanged -= eventHandler;
        this._cboFunctionalArea = value;
        System.Windows.Forms.ComboBox cboFunctionalArea2 = this._cboFunctionalArea;
        if (cboFunctionalArea2 == null)
          return;
        cboFunctionalArea2.SelectedIndexChanged += eventHandler;
      }
    }

    public virtual System.Windows.Forms.Label lblInspectionDate { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual Button cmdDeleteInspection
    {
      get
      {
        return this._cmdDeleteInspection;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdDeleteInspection_Click);
        Button deleteInspection1 = this._cmdDeleteInspection;
        if (deleteInspection1 != null)
          deleteInspection1.Click -= eventHandler;
        this._cmdDeleteInspection = value;
        Button deleteInspection2 = this._cmdDeleteInspection;
        if (deleteInspection2 == null)
          return;
        deleteInspection2.Click += eventHandler;
      }
    }

    public virtual Button cmdCopyInspection
    {
      get
      {
        return this._cmdCopyInspection;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdCopyInspection_Click);
        Button cmdCopyInspection1 = this._cmdCopyInspection;
        if (cmdCopyInspection1 != null)
          cmdCopyInspection1.Click -= eventHandler;
        this._cmdCopyInspection = value;
        Button cmdCopyInspection2 = this._cmdCopyInspection;
        if (cmdCopyInspection2 == null)
          return;
        cmdCopyInspection2.Click += eventHandler;
      }
    }

    public virtual Button cmdNewInspection
    {
      get
      {
        return this._cmdNewInspection;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cmdNewInspection_Click);
        Button cmdNewInspection1 = this._cmdNewInspection;
        if (cmdNewInspection1 != null)
          cmdNewInspection1.Click -= eventHandler;
        this._cmdNewInspection = value;
        Button cmdNewInspection2 = this._cmdNewInspection;
        if (cmdNewInspection2 == null)
          return;
        cmdNewInspection2.Click += eventHandler;
      }
    }

    internal virtual TableLayoutPanel tlpSecQty { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Panel pnlDetails { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Panel pnlTrees { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual SplitContainer SplitContainerMain
    {
      get
      {
        return this._SplitContainerMain;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        PaintEventHandler paintEventHandler = new PaintEventHandler(this.SplitContainer_Paint);
        SplitContainer splitContainerMain1 = this._SplitContainerMain;
        if (splitContainerMain1 != null)
          splitContainerMain1.Paint -= paintEventHandler;
        this._SplitContainerMain = value;
        SplitContainer splitContainerMain2 = this._SplitContainerMain;
        if (splitContainerMain2 == null)
          return;
        splitContainerMain2.Paint += paintEventHandler;
      }
    }

    private virtual RadDesktopAlert alertFormsUpdate { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private virtual RadMenuItem alertFormsHide
    {
      get
      {
        return this._alertFormsHide;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.alertFormsHide_Click);
        RadMenuItem alertFormsHide1 = this._alertFormsHide;
        if (alertFormsHide1 != null)
          alertFormsHide1.Click -= eventHandler;
        this._alertFormsHide = value;
        RadMenuItem alertFormsHide2 = this._alertFormsHide;
        if (alertFormsHide2 == null)
          return;
        alertFormsHide2.Click += eventHandler;
      }
    }

    private virtual RadCommandBar RadCommandBar1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private virtual CommandBarRowElement CommandBarRowElement1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual GroupBox frmDirectRating { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual GroupBox frmDirectPaint { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tlpPaintRating { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual RadioButton optPaintRating_9
    {
      get
      {
        return this._optPaintRating_9;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optPaintRating_CheckedChanged);
        RadioButton optPaintRating9_1 = this._optPaintRating_9;
        if (optPaintRating9_1 != null)
          optPaintRating9_1.CheckedChanged -= eventHandler;
        this._optPaintRating_9 = value;
        RadioButton optPaintRating9_2 = this._optPaintRating_9;
        if (optPaintRating9_2 == null)
          return;
        optPaintRating9_2.CheckedChanged += eventHandler;
      }
    }

    public virtual RadioButton optPaintRating_1
    {
      get
      {
        return this._optPaintRating_1;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optPaintRating_CheckedChanged);
        RadioButton optPaintRating1_1 = this._optPaintRating_1;
        if (optPaintRating1_1 != null)
          optPaintRating1_1.CheckedChanged -= eventHandler;
        this._optPaintRating_1 = value;
        RadioButton optPaintRating1_2 = this._optPaintRating_1;
        if (optPaintRating1_2 == null)
          return;
        optPaintRating1_2.CheckedChanged += eventHandler;
      }
    }

    public virtual RadioButton optPaintRating_6
    {
      get
      {
        return this._optPaintRating_6;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optPaintRating_CheckedChanged);
        RadioButton optPaintRating6_1 = this._optPaintRating_6;
        if (optPaintRating6_1 != null)
          optPaintRating6_1.CheckedChanged -= eventHandler;
        this._optPaintRating_6 = value;
        RadioButton optPaintRating6_2 = this._optPaintRating_6;
        if (optPaintRating6_2 == null)
          return;
        optPaintRating6_2.CheckedChanged += eventHandler;
      }
    }

    public virtual RadioButton optPaintRating_8
    {
      get
      {
        return this._optPaintRating_8;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optPaintRating_CheckedChanged);
        RadioButton optPaintRating8_1 = this._optPaintRating_8;
        if (optPaintRating8_1 != null)
          optPaintRating8_1.CheckedChanged -= eventHandler;
        this._optPaintRating_8 = value;
        RadioButton optPaintRating8_2 = this._optPaintRating_8;
        if (optPaintRating8_2 == null)
          return;
        optPaintRating8_2.CheckedChanged += eventHandler;
      }
    }

    public virtual RadioButton optPaintRating_3
    {
      get
      {
        return this._optPaintRating_3;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optPaintRating_CheckedChanged);
        RadioButton optPaintRating3_1 = this._optPaintRating_3;
        if (optPaintRating3_1 != null)
          optPaintRating3_1.CheckedChanged -= eventHandler;
        this._optPaintRating_3 = value;
        RadioButton optPaintRating3_2 = this._optPaintRating_3;
        if (optPaintRating3_2 == null)
          return;
        optPaintRating3_2.CheckedChanged += eventHandler;
      }
    }

    public virtual RadioButton optPaintRating_4
    {
      get
      {
        return this._optPaintRating_4;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optPaintRating_CheckedChanged);
        RadioButton optPaintRating4_1 = this._optPaintRating_4;
        if (optPaintRating4_1 != null)
          optPaintRating4_1.CheckedChanged -= eventHandler;
        this._optPaintRating_4 = value;
        RadioButton optPaintRating4_2 = this._optPaintRating_4;
        if (optPaintRating4_2 == null)
          return;
        optPaintRating4_2.CheckedChanged += eventHandler;
      }
    }

    public virtual RadioButton optPaintRating_7
    {
      get
      {
        return this._optPaintRating_7;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optPaintRating_CheckedChanged);
        RadioButton optPaintRating7_1 = this._optPaintRating_7;
        if (optPaintRating7_1 != null)
          optPaintRating7_1.CheckedChanged -= eventHandler;
        this._optPaintRating_7 = value;
        RadioButton optPaintRating7_2 = this._optPaintRating_7;
        if (optPaintRating7_2 == null)
          return;
        optPaintRating7_2.CheckedChanged += eventHandler;
      }
    }

    public virtual RadioButton optPaintRating_5
    {
      get
      {
        return this._optPaintRating_5;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optPaintRating_CheckedChanged);
        RadioButton optPaintRating5_1 = this._optPaintRating_5;
        if (optPaintRating5_1 != null)
          optPaintRating5_1.CheckedChanged -= eventHandler;
        this._optPaintRating_5 = value;
        RadioButton optPaintRating5_2 = this._optPaintRating_5;
        if (optPaintRating5_2 == null)
          return;
        optPaintRating5_2.CheckedChanged += eventHandler;
      }
    }

    public virtual RadioButton optPaintRating_2
    {
      get
      {
        return this._optPaintRating_2;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optPaintRating_CheckedChanged);
        RadioButton optPaintRating2_1 = this._optPaintRating_2;
        if (optPaintRating2_1 != null)
          optPaintRating2_1.CheckedChanged -= eventHandler;
        this._optPaintRating_2 = value;
        RadioButton optPaintRating2_2 = this._optPaintRating_2;
        if (optPaintRating2_2 == null)
          return;
        optPaintRating2_2.CheckedChanged += eventHandler;
      }
    }

    public virtual GroupBox frmDirectComponent { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tlpDirectRating { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual RadioButton optCompRating_9
    {
      get
      {
        return this._optCompRating_9;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optCompRating_CheckedChanged);
        RadioButton optCompRating9_1 = this._optCompRating_9;
        if (optCompRating9_1 != null)
          optCompRating9_1.CheckedChanged -= eventHandler;
        this._optCompRating_9 = value;
        RadioButton optCompRating9_2 = this._optCompRating_9;
        if (optCompRating9_2 == null)
          return;
        optCompRating9_2.CheckedChanged += eventHandler;
      }
    }

    public virtual RadioButton optCompRating_5
    {
      get
      {
        return this._optCompRating_5;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optCompRating_CheckedChanged);
        RadioButton optCompRating5_1 = this._optCompRating_5;
        if (optCompRating5_1 != null)
          optCompRating5_1.CheckedChanged -= eventHandler;
        this._optCompRating_5 = value;
        RadioButton optCompRating5_2 = this._optCompRating_5;
        if (optCompRating5_2 == null)
          return;
        optCompRating5_2.CheckedChanged += eventHandler;
      }
    }

    public virtual RadioButton optCompRating_6
    {
      get
      {
        return this._optCompRating_6;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optCompRating_CheckedChanged);
        RadioButton optCompRating6_1 = this._optCompRating_6;
        if (optCompRating6_1 != null)
          optCompRating6_1.CheckedChanged -= eventHandler;
        this._optCompRating_6 = value;
        RadioButton optCompRating6_2 = this._optCompRating_6;
        if (optCompRating6_2 == null)
          return;
        optCompRating6_2.CheckedChanged += eventHandler;
      }
    }

    public virtual RadioButton optCompRating_8
    {
      get
      {
        return this._optCompRating_8;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optCompRating_CheckedChanged);
        RadioButton optCompRating8_1 = this._optCompRating_8;
        if (optCompRating8_1 != null)
          optCompRating8_1.CheckedChanged -= eventHandler;
        this._optCompRating_8 = value;
        RadioButton optCompRating8_2 = this._optCompRating_8;
        if (optCompRating8_2 == null)
          return;
        optCompRating8_2.CheckedChanged += eventHandler;
      }
    }

    public virtual RadioButton optCompRating_7
    {
      get
      {
        return this._optCompRating_7;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optCompRating_CheckedChanged);
        RadioButton optCompRating7_1 = this._optCompRating_7;
        if (optCompRating7_1 != null)
          optCompRating7_1.CheckedChanged -= eventHandler;
        this._optCompRating_7 = value;
        RadioButton optCompRating7_2 = this._optCompRating_7;
        if (optCompRating7_2 == null)
          return;
        optCompRating7_2.CheckedChanged += eventHandler;
      }
    }

    public virtual RadioButton optCompRating_4
    {
      get
      {
        return this._optCompRating_4;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optCompRating_CheckedChanged);
        RadioButton optCompRating4_1 = this._optCompRating_4;
        if (optCompRating4_1 != null)
          optCompRating4_1.CheckedChanged -= eventHandler;
        this._optCompRating_4 = value;
        RadioButton optCompRating4_2 = this._optCompRating_4;
        if (optCompRating4_2 == null)
          return;
        optCompRating4_2.CheckedChanged += eventHandler;
      }
    }

    public virtual RadioButton optCompRating_2
    {
      get
      {
        return this._optCompRating_2;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optCompRating_CheckedChanged);
        RadioButton optCompRating2_1 = this._optCompRating_2;
        if (optCompRating2_1 != null)
          optCompRating2_1.CheckedChanged -= eventHandler;
        this._optCompRating_2 = value;
        RadioButton optCompRating2_2 = this._optCompRating_2;
        if (optCompRating2_2 == null)
          return;
        optCompRating2_2.CheckedChanged += eventHandler;
      }
    }

    public virtual RadioButton optCompRating_1
    {
      get
      {
        return this._optCompRating_1;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optCompRating_CheckedChanged);
        RadioButton optCompRating1_1 = this._optCompRating_1;
        if (optCompRating1_1 != null)
          optCompRating1_1.CheckedChanged -= eventHandler;
        this._optCompRating_1 = value;
        RadioButton optCompRating1_2 = this._optCompRating_1;
        if (optCompRating1_2 == null)
          return;
        optCompRating1_2.CheckedChanged += eventHandler;
      }
    }

    public virtual RadioButton optCompRating_3
    {
      get
      {
        return this._optCompRating_3;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optCompRating_CheckedChanged);
        RadioButton optCompRating3_1 = this._optCompRating_3;
        if (optCompRating3_1 != null)
          optCompRating3_1.CheckedChanged -= eventHandler;
        this._optCompRating_3 = value;
        RadioButton optCompRating3_2 = this._optCompRating_3;
        if (optCompRating3_2 == null)
          return;
        optCompRating3_2.CheckedChanged += eventHandler;
      }
    }

    public virtual UltraTree tvInventory
    {
      get
      {
        return this._tvInventory;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        AfterNodeChangedEventHandler changedEventHandler1 = new AfterNodeChangedEventHandler(this.tvInventory_AfterActivate);
        AfterNodeChangedEventHandler changedEventHandler2 = new AfterNodeChangedEventHandler(this.tvInventory_AfterCollapse);
        AfterNodeChangedEventHandler changedEventHandler3 = new AfterNodeChangedEventHandler(this.tvInventory_AfterExpand);
        BeforeNodeChangedEventHandler changedEventHandler4 = new BeforeNodeChangedEventHandler(this.tvInventory_BeforeActivate);
        BeforeNodeChangedEventHandler changedEventHandler5 = new BeforeNodeChangedEventHandler(this.tvInventory_BeforeExpand);
        UltraTree tvInventory1 = this._tvInventory;
        if (tvInventory1 != null)
        {
          tvInventory1.AfterActivate -= changedEventHandler1;
          tvInventory1.AfterCollapse -= changedEventHandler2;
          tvInventory1.AfterExpand -= changedEventHandler3;
          tvInventory1.BeforeActivate -= changedEventHandler4;
          tvInventory1.BeforeExpand -= changedEventHandler5;
        }
        this._tvInventory = value;
        UltraTree tvInventory2 = this._tvInventory;
        if (tvInventory2 == null)
          return;
        tvInventory2.AfterActivate += changedEventHandler1;
        tvInventory2.AfterCollapse += changedEventHandler2;
        tvInventory2.AfterExpand += changedEventHandler3;
        tvInventory2.BeforeActivate += changedEventHandler4;
        tvInventory2.BeforeExpand += changedEventHandler5;
      }
    }

    public virtual UltraTree tvInspection
    {
      get
      {
        return this._tvInspection;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        AfterNodeChangedEventHandler changedEventHandler1 = new AfterNodeChangedEventHandler(this.tvInspection_AfterActivate);
        AfterNodeChangedEventHandler changedEventHandler2 = new AfterNodeChangedEventHandler(this.tvInspection_AfterCollapse);
        AfterNodeChangedEventHandler changedEventHandler3 = new AfterNodeChangedEventHandler(this.tvInspection_AfterExpand);
        BeforeNodeChangedEventHandler changedEventHandler4 = new BeforeNodeChangedEventHandler(this.tvInspection_BeforeActivate);
        UltraTree tvInspection1 = this._tvInspection;
        if (tvInspection1 != null)
        {
          tvInspection1.AfterActivate -= changedEventHandler1;
          tvInspection1.AfterCollapse -= changedEventHandler2;
          tvInspection1.AfterExpand -= changedEventHandler3;
          tvInspection1.BeforeActivate -= changedEventHandler4;
        }
        this._tvInspection = value;
        UltraTree tvInspection2 = this._tvInspection;
        if (tvInspection2 == null)
          return;
        tvInspection2.AfterActivate += changedEventHandler1;
        tvInspection2.AfterCollapse += changedEventHandler2;
        tvInspection2.AfterExpand += changedEventHandler3;
        tvInspection2.BeforeActivate += changedEventHandler4;
      }
    }

    public virtual UltraGrid ugSubcomponents
    {
      get
      {
        return this._ugSubcomponents;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        InitializeRowEventHandler initializeRowEventHandler = new InitializeRowEventHandler(this.ugSubcomponents_InitializeRow);
        CellEventHandler cellEventHandler1 = new CellEventHandler(this.ugSubcomponents_CellChange);
        CellEventHandler cellEventHandler2 = new CellEventHandler(this.ugSubcomponents_AfterCellUpdate);
        CellEventHandler cellEventHandler3 = new CellEventHandler(this.ugSubcomponents_ClickCellButton);
        InitializeLayoutEventHandler layoutEventHandler = new InitializeLayoutEventHandler(this.ugSubcomponents_InitializeLayout);
        UltraGrid ugSubcomponents1 = this._ugSubcomponents;
        if (ugSubcomponents1 != null)
        {
          ugSubcomponents1.InitializeRow -= initializeRowEventHandler;
          ugSubcomponents1.CellChange -= cellEventHandler1;
          ugSubcomponents1.AfterCellUpdate -= cellEventHandler2;
          ugSubcomponents1.ClickCellButton -= cellEventHandler3;
          ugSubcomponents1.InitializeLayout -= layoutEventHandler;
        }
        this._ugSubcomponents = value;
        UltraGrid ugSubcomponents2 = this._ugSubcomponents;
        if (ugSubcomponents2 == null)
          return;
        ugSubcomponents2.InitializeRow += initializeRowEventHandler;
        ugSubcomponents2.CellChange += cellEventHandler1;
        ugSubcomponents2.AfterCellUpdate += cellEventHandler2;
        ugSubcomponents2.ClickCellButton += cellEventHandler3;
        ugSubcomponents2.InitializeLayout += layoutEventHandler;
      }
    }

    internal virtual UltraTree tvFunctionality
    {
      get
      {
        return this._tvFunctionality;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        AfterNodeChangedEventHandler changedEventHandler1 = new AfterNodeChangedEventHandler(this.tvFunctionality_AfterActivate);
        AfterNodeChangedEventHandler changedEventHandler2 = new AfterNodeChangedEventHandler(this.tvFunctionality_AfterCollapse);
        AfterNodeChangedEventHandler changedEventHandler3 = new AfterNodeChangedEventHandler(this.tvFunctionality_AfterExpand);
        BeforeNodeChangedEventHandler changedEventHandler4 = new BeforeNodeChangedEventHandler(this.tvFunctionality_BeforeActivate);
        UltraTree tvFunctionality1 = this._tvFunctionality;
        if (tvFunctionality1 != null)
        {
          tvFunctionality1.AfterActivate -= changedEventHandler1;
          tvFunctionality1.AfterCollapse -= changedEventHandler2;
          tvFunctionality1.AfterExpand -= changedEventHandler3;
          tvFunctionality1.BeforeActivate -= changedEventHandler4;
        }
        this._tvFunctionality = value;
        UltraTree tvFunctionality2 = this._tvFunctionality;
        if (tvFunctionality2 == null)
          return;
        tvFunctionality2.AfterActivate += changedEventHandler1;
        tvFunctionality2.AfterCollapse += changedEventHandler2;
        tvFunctionality2.AfterExpand += changedEventHandler3;
        tvFunctionality2.BeforeActivate += changedEventHandler4;
      }
    }

    internal virtual Panel pnlFunctionality { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Panel pnlFuncAssessment { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Panel pnlFuncArea { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual System.Windows.Forms.Label lblFuncArea { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private virtual RadGridView rgDetails { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private virtual CommandBarStripElement CommandBarStripElement1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private virtual CommandBarButton cmdNewDetail { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private virtual RadPageView tbSection { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private virtual RadPageViewPage tpSection { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private virtual RadPageViewPage tpDetails { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ElementHost ElementHost1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual MenuItem miInspectionMode
    {
      get
      {
        return this._miInspectionMode;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.miInspectionMode_Click);
        MenuItem miInspectionMode1 = this._miInspectionMode;
        if (miInspectionMode1 != null)
          miInspectionMode1.Click -= eventHandler;
        this._miInspectionMode = value;
        MenuItem miInspectionMode2 = this._miInspectionMode;
        if (miInspectionMode2 == null)
          return;
        miInspectionMode2.Click += eventHandler;
      }
    }

    internal virtual MenuItem miFunctionalityMode
    {
      get
      {
        return this._miFunctionalityMode;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.miFunctionalityMode_Click);
        MenuItem functionalityMode1 = this._miFunctionalityMode;
        if (functionalityMode1 != null)
          functionalityMode1.Click -= eventHandler;
        this._miFunctionalityMode = value;
        MenuItem functionalityMode2 = this._miFunctionalityMode;
        if (functionalityMode2 == null)
          return;
        functionalityMode2.Click += eventHandler;
      }
    }

    internal virtual FlowLayoutPanel flpInspectionDate { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual System.Windows.Forms.ComboBox cboInspectionDates
    {
      get
      {
        return this._cboInspectionDates;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cboInspectionDates_SelectedIndexChanged);
        System.Windows.Forms.ComboBox cboInspectionDates1 = this._cboInspectionDates;
        if (cboInspectionDates1 != null)
          cboInspectionDates1.SelectedIndexChanged -= eventHandler;
        this._cboInspectionDates = value;
        System.Windows.Forms.ComboBox cboInspectionDates2 = this._cboInspectionDates;
        if (cboInspectionDates2 == null)
          return;
        cboInspectionDates2.SelectedIndexChanged += eventHandler;
      }
    }

    internal virtual FlowLayoutPanel flpInspType { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual FlowLayoutPanel flpInspMethod { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual FlowLayoutPanel flpLocation { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tlpRatings { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual StatusStrip ssStatusStrip { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ToolStripStatusLabel tsslStatus { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ToolStripStatusLabel tsslInspector { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ToolStripStatusLabel tsslCurrentDate { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ToolStripStatusLabel tsslCurrentTime { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ToolStrip tsToolbar { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ToolStripDropDownButton tsddbToggle { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ToolStripSeparator tss1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ToolStripButton tsbTally
    {
      get
      {
        return this._tsbTally;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsToolbar_Click);
        ToolStripButton tsbTally1 = this._tsbTally;
        if (tsbTally1 != null)
          tsbTally1.Click -= eventHandler;
        this._tsbTally = value;
        ToolStripButton tsbTally2 = this._tsbTally;
        if (tsbTally2 == null)
          return;
        tsbTally2.Click += eventHandler;
      }
    }

    internal virtual ToolStripButton tsbNew1
    {
      get
      {
        return this._tsbNew1;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsToolbar_Click);
        ToolStripButton tsbNew1_1 = this._tsbNew1;
        if (tsbNew1_1 != null)
          tsbNew1_1.Click -= eventHandler;
        this._tsbNew1 = value;
        ToolStripButton tsbNew1_2 = this._tsbNew1;
        if (tsbNew1_2 == null)
          return;
        tsbNew1_2.Click += eventHandler;
      }
    }

    internal virtual ToolStripButton tsbNew2
    {
      get
      {
        return this._tsbNew2;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsToolbar_Click);
        ToolStripButton tsbNew2_1 = this._tsbNew2;
        if (tsbNew2_1 != null)
          tsbNew2_1.Click -= eventHandler;
        this._tsbNew2 = value;
        ToolStripButton tsbNew2_2 = this._tsbNew2;
        if (tsbNew2_2 == null)
          return;
        tsbNew2_2.Click += eventHandler;
      }
    }

    internal virtual ToolStripButton tsbDelete
    {
      get
      {
        return this._tsbDelete;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsToolbar_Click);
        ToolStripButton tsbDelete1 = this._tsbDelete;
        if (tsbDelete1 != null)
          tsbDelete1.Click -= eventHandler;
        this._tsbDelete = value;
        ToolStripButton tsbDelete2 = this._tsbDelete;
        if (tsbDelete2 == null)
          return;
        tsbDelete2.Click += eventHandler;
      }
    }

    internal virtual ToolStripSeparator tss2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ToolStripButton tsbEdit
    {
      get
      {
        return this._tsbEdit;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsToolbar_Click);
        ToolStripButton tsbEdit1 = this._tsbEdit;
        if (tsbEdit1 != null)
          tsbEdit1.Click -= eventHandler;
        this._tsbEdit = value;
        ToolStripButton tsbEdit2 = this._tsbEdit;
        if (tsbEdit2 == null)
          return;
        tsbEdit2.Click += eventHandler;
      }
    }

    internal virtual ToolStripButton tsbSave
    {
      get
      {
        return this._tsbSave;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsToolbar_Click);
        ToolStripButton tsbSave1 = this._tsbSave;
        if (tsbSave1 != null)
          tsbSave1.Click -= eventHandler;
        this._tsbSave = value;
        ToolStripButton tsbSave2 = this._tsbSave;
        if (tsbSave2 == null)
          return;
        tsbSave2.Click += eventHandler;
      }
    }

    internal virtual ToolStripButton tsbCancel
    {
      get
      {
        return this._tsbCancel;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsToolbar_Click);
        ToolStripButton tsbCancel1 = this._tsbCancel;
        if (tsbCancel1 != null)
          tsbCancel1.Click -= eventHandler;
        this._tsbCancel = value;
        ToolStripButton tsbCancel2 = this._tsbCancel;
        if (tsbCancel2 == null)
          return;
        tsbCancel2.Click += eventHandler;
      }
    }

    internal virtual ToolStripSeparator tss3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ToolStripButton tsbComment
    {
      get
      {
        return this._tsbComment;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsToolbar_Click);
        ToolStripButton tsbComment1 = this._tsbComment;
        if (tsbComment1 != null)
          tsbComment1.Click -= eventHandler;
        this._tsbComment = value;
        ToolStripButton tsbComment2 = this._tsbComment;
        if (tsbComment2 == null)
          return;
        tsbComment2.Click += eventHandler;
      }
    }

    internal virtual ToolStripButton tsbImages
    {
      get
      {
        return this._tsbImages;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsToolbar_Click);
        ToolStripButton tsbImages1 = this._tsbImages;
        if (tsbImages1 != null)
          tsbImages1.Click -= eventHandler;
        this._tsbImages = value;
        ToolStripButton tsbImages2 = this._tsbImages;
        if (tsbImages2 == null)
          return;
        tsbImages2.Click += eventHandler;
      }
    }

    internal virtual ToolStripButton tsbWorkItems
    {
      get
      {
        return this._tsbWorkItems;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsToolbar_Click);
        ToolStripButton tsbWorkItems1 = this._tsbWorkItems;
        if (tsbWorkItems1 != null)
          tsbWorkItems1.Click -= eventHandler;
        this._tsbWorkItems = value;
        ToolStripButton tsbWorkItems2 = this._tsbWorkItems;
        if (tsbWorkItems2 == null)
          return;
        tsbWorkItems2.Click += eventHandler;
      }
    }

    internal virtual ToolStripMenuItem InventoryToolStripMenuItem
    {
      get
      {
        return this._InventoryToolStripMenuItem;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsToolbar_Click);
        ToolStripMenuItem toolStripMenuItem1 = this._InventoryToolStripMenuItem;
        if (toolStripMenuItem1 != null)
          toolStripMenuItem1.Click -= eventHandler;
        this._InventoryToolStripMenuItem = value;
        ToolStripMenuItem toolStripMenuItem2 = this._InventoryToolStripMenuItem;
        if (toolStripMenuItem2 == null)
          return;
        toolStripMenuItem2.Click += eventHandler;
      }
    }

    internal virtual ToolStripMenuItem InspectionsToolStripMenuItem
    {
      get
      {
        return this._InspectionsToolStripMenuItem;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsToolbar_Click);
        ToolStripMenuItem toolStripMenuItem1 = this._InspectionsToolStripMenuItem;
        if (toolStripMenuItem1 != null)
          toolStripMenuItem1.Click -= eventHandler;
        this._InspectionsToolStripMenuItem = value;
        ToolStripMenuItem toolStripMenuItem2 = this._InspectionsToolStripMenuItem;
        if (toolStripMenuItem2 == null)
          return;
        toolStripMenuItem2.Click += eventHandler;
      }
    }

    internal virtual ToolStripMenuItem FunctionalityToolStripMenuItem
    {
      get
      {
        return this._FunctionalityToolStripMenuItem;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.tsToolbar_Click);
        ToolStripMenuItem toolStripMenuItem1 = this._FunctionalityToolStripMenuItem;
        if (toolStripMenuItem1 != null)
          toolStripMenuItem1.Click -= eventHandler;
        this._FunctionalityToolStripMenuItem = value;
        ToolStripMenuItem toolStripMenuItem2 = this._FunctionalityToolStripMenuItem;
        if (toolStripMenuItem2 == null)
          return;
        toolStripMenuItem2.Click += eventHandler;
      }
    }

    internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual FlowLayoutPanel flpSubCompData { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public virtual RadioButton optRatingType_3
    {
      get
      {
        return this._optRatingType_3;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.optRatingType_CheckedChanged);
        RadioButton optRatingType3_1 = this._optRatingType_3;
        if (optRatingType3_1 != null)
          optRatingType3_1.CheckedChanged -= eventHandler;
        this._optRatingType_3 = value;
        RadioButton optRatingType3_2 = this._optRatingType_3;
        if (optRatingType3_2 == null)
          return;
        optRatingType3_2.CheckedChanged += eventHandler;
      }
    }

    internal virtual Panel pnlTaskList { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button ShowTasksButton
    {
      get
      {
        return this._ShowTasksButton;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.ShowTasksButton_Click);
        Button showTasksButton1 = this._ShowTasksButton;
        if (showTasksButton1 != null)
          showTasksButton1.Click -= eventHandler;
        this._ShowTasksButton = value;
        Button showTasksButton2 = this._ShowTasksButton;
        if (showTasksButton2 == null)
          return;
        showTasksButton2.Click += eventHandler;
      }
    }

    internal virtual System.Windows.Forms.Label lblYearRenovated { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual System.Windows.Forms.TextBox txtYearRenovated
    {
      get
      {
        return this._txtYearRenovated;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler1 = new EventHandler(this.Building_TextChanged);
        EventHandler eventHandler2 = new EventHandler(this.txtYearRenovated_TextChanged);
        System.Windows.Forms.TextBox txtYearRenovated1 = this._txtYearRenovated;
        if (txtYearRenovated1 != null)
        {
          txtYearRenovated1.TextChanged -= eventHandler1;
          txtYearRenovated1.TextChanged -= eventHandler2;
        }
        this._txtYearRenovated = value;
        System.Windows.Forms.TextBox txtYearRenovated2 = this._txtYearRenovated;
        if (txtYearRenovated2 == null)
          return;
        txtYearRenovated2.TextChanged += eventHandler1;
        txtYearRenovated2.TextChanged += eventHandler2;
      }
    }

    internal virtual System.Windows.Forms.Label txtDoesNotContain { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual System.Windows.Forms.Label lblUnableToInspect { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual System.Windows.Forms.ComboBox rcbInspIssue
    {
      get
      {
        return this._rcbInspIssue;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.Building_SelectedIndexChanged);
        System.Windows.Forms.ComboBox rcbInspIssue1 = this._rcbInspIssue;
        if (rcbInspIssue1 != null)
          rcbInspIssue1.SelectedIndexChanged -= eventHandler;
        this._rcbInspIssue = value;
        System.Windows.Forms.ComboBox rcbInspIssue2 = this._rcbInspIssue;
        if (rcbInspIssue2 == null)
          return;
        rcbInspIssue2.SelectedIndexChanged += eventHandler;
      }
    }

    internal virtual Button btnDoesNotContain
    {
      get
      {
        return this._btnDoesNotContain;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnDoesNotContain_Click);
        Button btnDoesNotContain1 = this._btnDoesNotContain;
        if (btnDoesNotContain1 != null)
          btnDoesNotContain1.Click -= eventHandler;
        this._btnDoesNotContain = value;
        Button btnDoesNotContain2 = this._btnDoesNotContain;
        if (btnDoesNotContain2 == null)
          return;
        btnDoesNotContain2.Click += eventHandler;
      }
    }

    internal virtual System.Windows.Forms.Label lblSectionStatus { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual System.Windows.Forms.ComboBox cboSectionStatus
    {
      get
      {
        return this._cboSectionStatus;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.cboSectionStatus_SelectedIndexChanged);
        System.Windows.Forms.ComboBox cboSectionStatus1 = this._cboSectionStatus;
        if (cboSectionStatus1 != null)
          cboSectionStatus1.SelectedIndexChanged -= eventHandler;
        this._cboSectionStatus = value;
        System.Windows.Forms.ComboBox cboSectionStatus2 = this._cboSectionStatus;
        if (cboSectionStatus2 == null)
          return;
        cboSectionStatus2.SelectedIndexChanged += eventHandler;
      }
    }

    public bool BldgNeedToSave
    {
      get
      {
        return this._bBldgNeedToSave;
      }
      set
      {
        this._bBldgNeedToSave = value;
      }
    }

    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (!this.DataHasBeenSaved())
        return;
      if (mdUtility.DatabasePath != null)
      {
        MySettingsProperty.Settings.LastOpened = mdUtility.DatabasePath;
        MySettingsProperty.Settings.OpenLastFile = this.miOpenLastFile.Checked;
        MySettingsProperty.Settings.HideAlertForms = this.alertFormsHide.IsChecked;
        MySettingsProperty.Settings.Save();
      }
      else
      {
        MySettingsProperty.Settings.Reset();
        MySettingsProperty.Settings.Save();
      }
    }

    protected override void OnHandleCreated(EventArgs e)
    {
      base.OnHandleCreated(e);
      ParentWindowContainer.SetWpfInteropParentHandle(this.Handle);
    }

    public string CurrentFuncArea
    {
      get
      {
        return this.m_strAreaID;
      }
    }

    public string CurrentBldg
    {
      get
      {
        return this.m_strBldgID;
      }
    }

    public string CurrentDoesNotContain
    {
      get
      {
        return this.m_DoesNotContain;
      }
      set
      {
        this.m_DoesNotContain = value;
      }
    }

    public string CurrentSystem
    {
      get
      {
        return this.m_strSysID;
      }
    }

    public string CurrentLocation
    {
      get
      {
        return this.m_strLocation;
      }
    }

    public string CurrentComp
    {
      get
      {
        return this.m_strCompID;
      }
    }

    public string CurrentSection
    {
      get
      {
        return this.m_strSecID;
      }
    }

    public string CurrentSectionID
    {
      get
      {
        return !Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(this.tvInspection.GetNodeByKey(this.m_strSecID).Tag, (object) "Section", false) ? this.tvInspection.GetNodeByKey(this.m_strSecID).Tag.ToString() : this.m_strSecID;
      }
    }

    public string CurrentInspection
    {
      get
      {
        return this.m_strInspID;
      }
    }

    public string CurrentSample
    {
      get
      {
        return this.m_strSampID;
      }
    }

    public bool CanEditInspection
    {
      get
      {
        return this.m_bInspCanEdit;
      }
      set
      {
        this.m_bInspCanEdit = value;
        this.cmdDeleteInspection.Enabled = value;
        this.frmRatingType.Enabled = value;
        this.frmMethod.Enabled = value;
        this.txtSQuantity.ReadOnly = !value;
        this.cmdNewSample.Enabled = value;
        this.cmdDeleteSample.Enabled = value;
        this.cmdEditSample.Enabled = value;
        if (value)
          this.ugSubcomponents.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
        else
          this.ugSubcomponents.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
      }
    }

    internal Bitmap ExpansionImage
    {
      get
      {
        return this.imgExpand;
      }
    }

    internal Bitmap CollapseImage
    {
      get
      {
        return this.imgCollapse;
      }
    }

    private UltraTreeNode FindNodeInOtherTree(UltraTree FromTree, UltraTree OtherTree)
    {
      if (OtherTree.GetNodeByKey(FromTree.ActiveNode.Key) != null)
      {
        OtherTree.ActiveNode = OtherTree.GetNodeByKey(FromTree.ActiveNode.Key);
      }
      else
      {
        UltraTreeNode ultraTreeNode1 = FromTree.ActiveNode;
        UltraTreeNode ultraTreeNode2 = OtherTree.GetNodeByKey(ultraTreeNode1.Key);
        Stack<KeyValuePair<string, string>> keyValuePairStack = new Stack<KeyValuePair<string, string>>();
        string Left1 = ultraTreeNode1.Key.Substring(0, 2);
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "S-", false) == 0 || Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "C-", false) == 0 || Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "L-", false) == 0)
        {
          UltraTreeNode ultraTreeNode3 = FromTree.GetNodeByKey(ultraTreeNode1.Tag.ToString()) ?? FromTree.GetNodeByKey(ultraTreeNode1.Parent.Tag.ToString());
          if (ultraTreeNode3 != null)
            ultraTreeNode1 = ultraTreeNode3;
        }
        for (; ultraTreeNode2 == null; ultraTreeNode2 = OtherTree.GetNodeByKey(ultraTreeNode1.Key))
        {
          string Left2 = ultraTreeNode1.Key.Substring(0, 2);
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left2, "S-", false) != 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left2, "C-", false) != 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left2, "L-", false) != 0)
            keyValuePairStack.Push(new KeyValuePair<string, string>(ultraTreeNode1.Tag.ToString(), ultraTreeNode1.Key));
          ultraTreeNode1 = ultraTreeNode1.Parent;
        }
        while (keyValuePairStack.Count > 0)
        {
          string key = keyValuePairStack.Peek().Key;
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(key, "System", false) != 0)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(key, "Non-Sampling", false) == 0)
              keyValuePairStack.Pop();
          }
          else
          {
            ultraTreeNode2.Expanded = true;
            if (ultraTreeNode2.Nodes.Count > 0)
              ultraTreeNode2 = ultraTreeNode2.Nodes[0];
          }
          ultraTreeNode2.Expanded = true;
          UltraTreeNode nodeByKey = OtherTree.GetNodeByKey(keyValuePairStack.Peek().Value);
          if (nodeByKey != null)
            ultraTreeNode2 = nodeByKey;
          keyValuePairStack.Pop();
        }
        OtherTree.ActiveNode = ultraTreeNode2;
      }
      return OtherTree.ActiveNode;
    }

    public frmMain.ProgramMode Mode
    {
      get
      {
        return this.m_pmProgramMode;
      }
      set
      {
        this.InventoryToolStripMenuItem.Enabled = true;
        this.InspectionsToolStripMenuItem.Enabled = true;
        this.FunctionalityToolStripMenuItem.Enabled = true;
        try
        {
          switch (value)
          {
            case frmMain.ProgramMode.pmInventory:
              bool MustSave1 = false;
              if (!this.CheckInspForSave(ref MustSave1))
                break;
              this.m_pmProgramMode = frmMain.ProgramMode.pmInventory;
              this.miToolsCopyInventory.Enabled = true;
              this.miToolsInspectSections.Enabled = false;
              this.tvInspection.Visible = false;
              this.tvFunctionality.Visible = false;
              this.tvInventory.Visible = true;
              this.tsddbToggle.Text = "Inventory";
              this.tsddbToggle.Image = (Image) BuilderRED.My.Resources.Resources.BRED_Inventory;
              this.InventoryToolStripMenuItem.Enabled = false;
              this.tsbTally.Visible = false;
              this.tsbNew1.Visible = true;
              this.tsbNew2.Visible = true;
              this.tsbDelete.Visible = true;
              this.tss2.Visible = true;
              this._ToggleView = 1;
              UltraTreeNode nodeInOtherTree1 = this.FindNodeInOtherTree(this.tvInspection, this.tvInventory);
              this.SelectNewActiveInventoryNode(ref nodeInOtherTree1);
              this.miInventoryMode.Checked = true;
              this.miInspectionMode.Checked = false;
              this.miFunctionalityMode.Checked = false;
              break;
            case frmMain.ProgramMode.pmInspection:
              if (!(this.CheckBldgForSave() & this.CheckSecForSave()))
                break;
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.strCurrentInspector, "", false) == 0)
              {
                frmChooseInspector frmChooseInspector = new frmChooseInspector();
                int num = (int) frmChooseInspector.ShowDialog();
                frmChooseInspector.Dispose();
              }
              if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.strCurrentInspector, "", false) > 0U)
              {
                this.m_pmProgramMode = frmMain.ProgramMode.pmInspection;
                this.miToolsCopyInventory.Enabled = false;
                this.tvInventory.Visible = false;
                this.tvFunctionality.Visible = false;
                this.tvInspection.Visible = true;
                this.tsddbToggle.Text = "Inspections";
                this.tsddbToggle.Image = (Image) BuilderRED.My.Resources.Resources.BRED_Inspect;
                this.InspectionsToolStripMenuItem.Enabled = false;
                this.tsbTally.Visible = true;
                this.tsbNew1.Visible = false;
                this.tsbNew2.Visible = false;
                this.tsbDelete.Visible = false;
                this.tss2.Visible = false;
                this.tsbEdit.Visible = false;
                this._ToggleView = 2;
                UltraTreeNode nodeInOtherTree2 = this.FindNodeInOtherTree(this.tvInventory, this.tvInspection);
                this.SelectNewActiveInspectionNode(ref nodeInOtherTree2);
                this.miInventoryMode.Checked = false;
                this.miInspectionMode.Checked = true;
                this.miFunctionalityMode.Checked = false;
              }
              break;
            default:
              bool MustSave2 = false;
              if (this.CheckInspForSave(ref MustSave2) & this.CheckBldgForSave() & this.CheckSecForSave())
              {
                this.m_pmProgramMode = frmMain.ProgramMode.pmFunctionality;
                this.tvInspection.Visible = false;
                this.tvInventory.Visible = false;
                this.tvFunctionality.Visible = true;
                this.miToolsCopyInventory.Enabled = false;
                this.miToolsInspectSections.Enabled = false;
                this.tsddbToggle.Text = "Functionality";
                this.tsddbToggle.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard;
                this.FunctionalityToolStripMenuItem.Enabled = false;
                this._ToggleView = 0;
                UltraTreeNode nodeInOtherTree2 = this.FindNodeInOtherTree(this.tvFunctionality, this.tvFunctionality);
                this.SelectNewActiveFunctionalityNode(ref nodeInOtherTree2);
                this.miInventoryMode.Checked = false;
                this.miInspectionMode.Checked = false;
                this.miFunctionalityMode.Checked = true;
              }
              break;
          }
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          mdUtility.Errorhandler(ex, this.Name, nameof (Mode));
          ProjectData.ClearProjectError();
        }
      }
    }

    public bool SamplePainted
    {
      set
      {
        this.chkSampPainted.CheckState = !value ? CheckState.Unchecked : CheckState.Checked;
        this.chkSampPainted_CheckStateChanged((object) this.chkSampPainted, new EventArgs());
      }
    }

    public void BuildingAdded(ref string BldgID, ref string BldgDesc)
    {
      try
      {
        UltraTreeNode nd = this.tvInventory.Nodes.Add(BldgID, BldgDesc);
        nd.Tag = (object) "Building";
        nd.Nodes.Add(nd.Key + "1", "Temp");
        this.tvInventory.Nodes.Override.Sort = SortType.Ascending;
        this.SelectNewActiveInventoryNode(ref nd);
        UltraTreeNode ultraTreeNode1 = this.tvInspection.Nodes.Add(BldgID, BldgDesc);
        ultraTreeNode1.Tag = (object) "Building";
        ultraTreeNode1.Nodes.Add(ultraTreeNode1.Key + "1", "Temp");
        this.tvInspection.Nodes.Override.Sort = SortType.Ascending;
        UltraTreeNode ultraTreeNode2 = this.tvFunctionality.Nodes.Add(BldgID, BldgDesc);
        ultraTreeNode2.Tag = (object) "Building";
        ultraTreeNode2.Nodes.Add(ultraTreeNode2.Key + "1", "Temp");
        this.tvFunctionality.Nodes.Override.Sort = SortType.Ascending;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (BuildingAdded));
        ProjectData.ClearProjectError();
      }
    }

    public void ComponentAdded(
      ref string SystemID,
      ref string ComponentID,
      ref string ComponentDesc)
    {
      try
      {
        if (this.tvInventory.GetNodeByKey(SystemID) == null)
          return;
        if (this.tvInventory.GetNodeByKey(SystemID).Nodes.Count == 0 || (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.tvInventory.GetNodeByKey(SystemID).Nodes[0].Key, this.tvInventory.GetNodeByKey(SystemID).Key + "1", false) > 0U)
        {
          UltraTreeNode ultraTreeNode = this.tvInventory.GetNodeByKey(SystemID).Nodes.Add(ComponentID, ComponentDesc);
          ultraTreeNode.Tag = (object) "Component";
          ultraTreeNode.Nodes.Add(ultraTreeNode.Key + "1", "Temp");
          this.tvInventory.GetNodeByKey(SystemID).Nodes.Override.Sort = SortType.Ascending;
        }
        if (this.tvInspection.GetNodeByKey(SystemID) != null)
        {
          if (this.tvInspection.GetNodeByKey(SystemID).Nodes.Count > 0)
          {
            if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.tvInspection.GetNodeByKey(SystemID).Nodes[0].Key, this.tvInspection.GetNodeByKey(SystemID).Key + "1", false) > 0U)
            {
              UltraTreeNode ultraTreeNode = this.tvInspection.GetNodeByKey(SystemID).Nodes.Add(ComponentID, ComponentDesc);
              ultraTreeNode.Tag = (object) "Component";
              ultraTreeNode.Nodes.Add(ultraTreeNode.Key + "1", "Temp");
              this.tvInspection.GetNodeByKey(SystemID).Nodes.Override.Sort = SortType.Ascending;
            }
          }
          else
          {
            UltraTreeNode ultraTreeNode = this.tvInspection.GetNodeByKey(SystemID).Nodes.Add(ComponentID, ComponentDesc);
            ultraTreeNode.Tag = (object) "Component";
            ultraTreeNode.Nodes.Add(ultraTreeNode.Key + "1", "Temp");
            this.tvInspection.GetNodeByKey(SystemID).Nodes.Override.Sort = SortType.Ascending;
          }
        }
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        Exception ex2 = ex1;
        if (Information.Err().Number != 35601)
          mdUtility.Errorhandler(ex2, this.Name, nameof (ComponentAdded));
        ProjectData.ClearProjectError();
      }
    }

    public void SystemAdded(ref string BldgID, ref string SystemID, ref string SystemDesc)
    {
      try
      {
        if (this.tvInventory.Nodes[BldgID] == null)
          return;
        if (this.tvInventory.Nodes[BldgID].Nodes.Count == 0 || (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.tvInventory.Nodes[BldgID].Nodes[0].Key, this.tvInventory.Nodes[BldgID].Key + "1", false) > 0U)
        {
          UltraTreeNode ultraTreeNode = this.tvInventory.Nodes[BldgID].Nodes.Add(SystemID, SystemDesc);
          ultraTreeNode.Tag = (object) "System";
          ultraTreeNode.Nodes.Add(ultraTreeNode.Key + "1", "Temp");
          this.tvInventory.Nodes[BldgID].Nodes.Override.Sort = SortType.Ascending;
        }
        if (this.tvInspection.GetNodeByKey(BldgID).Nodes[0] != null)
        {
          if (this.tvInspection.GetNodeByKey(BldgID).Nodes[0].Nodes.Count == 0 || (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.tvInspection.GetNodeByKey(BldgID).Nodes[0].Nodes[0].Key, this.tvInspection.GetNodeByKey(BldgID).Nodes[0].Key + "1", false) > 0U)
          {
            UltraTreeNode ultraTreeNode = this.tvInspection.GetNodeByKey(BldgID).Nodes[0].Nodes.Add(SystemID, SystemDesc);
            ultraTreeNode.Tag = (object) "System";
            ultraTreeNode.Nodes.Add(ultraTreeNode.Key + "1", "Temp");
            this.tvInspection.GetNodeByKey(BldgID).Nodes[0].Nodes.Override.Sort = SortType.Ascending;
          }
        }
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        Exception ex2 = ex1;
        if (Information.Err().Number != 35601)
          mdUtility.Errorhandler(ex2, this.Name, nameof (SystemAdded));
        ProjectData.ClearProjectError();
      }
    }

    public void SectionAdded(ref string ComponentID, ref string SectionID, ref string SectionLabel)
    {
      try
      {
        if (this.tvInventory.GetNodeByKey(ComponentID) == null)
          return;
        if (this.tvInventory.GetNodeByKey(ComponentID) != null && (this.tvInventory.GetNodeByKey(ComponentID).Nodes.Count == 0 || (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.tvInventory.GetNodeByKey(ComponentID).Nodes[0].Key, this.tvInventory.GetNodeByKey(ComponentID).Key + "1", false) > 0U))
        {
          this.tvInventory.GetNodeByKey(ComponentID).Nodes.Add(SectionID, SectionLabel).Tag = (object) "Section";
          this.tvInventory.GetNodeByKey(ComponentID).Nodes.Override.Sort = SortType.Ascending;
        }
        if (this.tvInspection.GetNodeByKey(ComponentID) != null)
        {
          if (this.tvInspection.GetNodeByKey(ComponentID).Nodes.Count > 0)
          {
            if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.tvInspection.GetNodeByKey(ComponentID).Nodes[0].Key, this.tvInspection.GetNodeByKey(ComponentID).Key + "1", false) > 0U)
            {
              UltraTreeNode ultraTreeNode = this.tvInspection.GetNodeByKey(ComponentID).Nodes.Add(SectionID, SectionLabel);
              ultraTreeNode.Tag = (object) "Section";
              ultraTreeNode.Nodes.Add(ultraTreeNode.Key + "1", "Temp");
              this.tvInspection.GetNodeByKey(ComponentID).Nodes.Override.Sort = SortType.Ascending;
            }
          }
          else
          {
            UltraTreeNode ultraTreeNode = this.tvInspection.GetNodeByKey(ComponentID).Nodes.Add(SectionID, SectionLabel);
            ultraTreeNode.Tag = (object) "Section";
            ultraTreeNode.Nodes.Add(ultraTreeNode.Key + "1", "Temp");
            this.tvInspection.GetNodeByKey(ComponentID).Nodes.Override.Sort = SortType.Ascending;
          }
        }
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        Exception ex2 = ex1;
        if (Information.Err().Number != 35601)
          mdUtility.Errorhandler(ex2, this.Name, nameof (SectionAdded));
        ProjectData.ClearProjectError();
      }
    }

    public void FuncAreaAdded(ref string AreaID)
    {
      try
      {
        if (this.tvFunctionality.GetNodeByKey(AreaID) == null)
          return;
        if (this.tvFunctionality.GetNodeByKey(AreaID) != null && (this.tvFunctionality.GetNodeByKey(AreaID).Nodes.Count == 0 || (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.tvFunctionality.GetNodeByKey(AreaID).Nodes[0].Key, this.tvFunctionality.GetNodeByKey(AreaID).Key + "1", false) > 0U))
        {
          this.tvFunctionality.GetNodeByKey(AreaID).Nodes.Add(AreaID, this.Name).Tag = (object) "FuncArea";
          this.tvFunctionality.GetNodeByKey(AreaID).Nodes.Override.Sort = SortType.Ascending;
        }
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        Exception ex2 = ex1;
        if (Information.Err().Number != 35601)
          mdUtility.Errorhandler(ex2, this.Name, nameof (FuncAreaAdded));
        ProjectData.ClearProjectError();
      }
    }

    private bool CheckFuncAreaForSave()
    {
      bool flag;
      try
      {
        if (this.MainPageView1.ViewModel.HasChanges)
        {
          Interaction.MsgBox((object) "There is currently an open assessment, please close or save before changing the view.", MsgBoxStyle.OkOnly, (object) null);
          flag = true;
        }
        else
          flag = false;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (CheckFuncAreaForSave));
        ProjectData.ClearProjectError();
      }
      return flag;
    }

    private bool CheckBldgForSave()
    {
      bool flag1;
      try
      {
        bool flag2;
        if (this.BldgNeedToSave)
        {
          switch (Interaction.MsgBox((object) "Data has changed.  Save before continuing?", MsgBoxStyle.YesNoCancel, (object) null))
          {
            case MsgBoxResult.Yes:
              flag2 = Building.SaveBuilding(this.CurrentBldg);
              break;
            case MsgBoxResult.No:
              flag2 = true;
              break;
            default:
              flag2 = false;
              break;
          }
        }
        else
          flag2 = true;
        if (flag2)
          this.SetBldgChanged(false);
        flag1 = flag2;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (CheckBldgForSave));
        flag1 = false;
        ProjectData.ClearProjectError();
      }
      return flag1;
    }

    public bool CheckInspForSave(ref bool MustSave = false)
    {
      bool flag1;
      try
      {
        bool flag2;
        if (this.m_bInspNeedToSave & this.m_bInspLoaded)
        {
          MsgBoxResult msgBoxResult = !MustSave ? Interaction.MsgBox((object) "Data has changed.  Save before continuing?", MsgBoxStyle.YesNoCancel, (object) null) : Interaction.MsgBox((object) "Data has changed.  You must save before continuing.", MsgBoxStyle.OkCancel, (object) null);
          if (msgBoxResult == MsgBoxResult.Yes | msgBoxResult == MsgBoxResult.Ok)
            flag2 = this.SaveInspection();
          else if (msgBoxResult == MsgBoxResult.No)
          {
            if (this.m_bInspNew)
              Inspection.DeleteInspection(this.CurrentInspection);
            else if (this.m_bSampleNew)
              Sample.DeleteSample(this.CurrentSample);
            flag2 = true;
          }
          else
            flag2 = false;
          if (flag2)
            this.SetInspChanged(false);
        }
        else
          flag2 = true;
        flag1 = flag2;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (CheckInspForSave));
        flag1 = false;
        ProjectData.ClearProjectError();
      }
      return flag1;
    }

    private bool CheckSecForSave()
    {
      bool flag1;
      try
      {
        bool flag2;
        if (this.m_bSectionNeedToSave)
        {
          switch (Interaction.MsgBox((object) "Data has changed. Save before continuing?", MsgBoxStyle.YesNoCancel, (object) null))
          {
            case MsgBoxResult.Yes:
              flag2 = Section.SaveSection(this.CurrentSection);
              break;
            case MsgBoxResult.No:
              flag2 = true;
              break;
            default:
              flag2 = false;
              break;
          }
        }
        else
          flag2 = true;
        if (flag2)
          this.SetSectionChanged(false);
        flag1 = flag2;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (CheckSecForSave));
        flag1 = false;
        ProjectData.ClearProjectError();
      }
      return flag1;
    }

    private void AddNewLocation()
    {
      try
      {
        string str1 = Interaction.InputBox("Please enter the name for the new location", "Add Location", "", -1, -1);
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str1, "", false) <= 0U)
          return;
        bool bNew;
        string str2 = Sample.SampleLocationID(str1, ref bNew);
        if (bNew & (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str2, "", false) > 0U)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(this.tvInspection.ActiveNode.Tag, (object) "Sample Locations", false))
          {
            UltraTreeNode activeNode;
            string key = (activeNode = this.tvInspection.ActiveNode).Key;
            this.AttachLocation(ref key, str2, str1);
            activeNode.Key = key;
          }
          else
          {
            UltraTreeNode parent;
            string key = (parent = this.tvInspection.ActiveNode.Parent).Key;
            this.AttachLocation(ref key, str2, str1);
            parent.Key = key;
          }
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (AddNewLocation));
        ProjectData.ClearProjectError();
      }
    }

    public void AttachLocation(ref string LocationParent, string strID, string LocationName)
    {
      try
      {
        UltraTreeNode nodeByKey = this.tvInspection.GetNodeByKey(LocationParent);
        if (nodeByKey != null && (nodeByKey.Nodes.Count == 0 || (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(nodeByKey.Nodes[0].Key, nodeByKey.Key + "1", false) > 0U))
        {
          UltraTreeNode nd = nodeByKey.Nodes.Add("L-" + mdUtility.NewRandomNumberString() + strID, LocationName);
          nd.Tag = (object) strID;
          nodeByKey.Nodes.Override.Sort = SortType.Ascending;
          this.SelectNewActiveInspectionNode(ref nd);
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (AttachLocation));
        ProjectData.ClearProjectError();
      }
    }

    private void AddInspComponent()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;
        frmAttachComponent frmAttachComponent = new frmAttachComponent();
        string component = frmAttachComponent.GetComponent(this.CurrentBldg, this.CurrentLocation);
        frmAttachComponent.Dispose();
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(component, "-1", false) <= 0U)
          return;
        DataTable dataTable = mdUtility.DB.GetDataTable("SELECT comp_desc FROM [Component Info] WHERE [sys_comp_id]={" + component + "}");
        if (dataTable.Rows.Count > 0)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Microsoft.VisualBasic.Strings.Left(this.tvInspection.ActiveNode.Key, 1), "L", false) == 0)
            this.AttachComponent(this.tvInspection.ActiveNode.Key, component, Conversions.ToString(dataTable.Rows[0]["comp_desc"]));
          else
            this.AttachComponent(this.tvInspection.ActiveNode.Parent.Key, component, Conversions.ToString(dataTable.Rows[0]["comp_desc"]));
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (AddInspComponent));
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    public string AttachComponent(string LocationID, string ComponentID, string ComponentLabel)
    {
      string str;
      try
      {
        UltraTreeNode nodeByKey = this.tvInspection.GetNodeByKey(LocationID);
        string key;
        UltraTreeNode nd;
        if (nodeByKey != null)
        {
          if (nodeByKey.Nodes.Count > 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(nodeByKey.Nodes[0].Key, nodeByKey.Key + "1", false) == 0)
            nodeByKey.Nodes.Remove(nodeByKey.Nodes[0]);
          key = "C-" + mdUtility.NewRandomNumberString() + ComponentID;
          nd = nodeByKey.Nodes.Add(key, ComponentLabel);
          nd.Tag = (object) ComponentID;
          nodeByKey.Nodes.Override.Sort = SortType.Ascending;
          this.SelectNewActiveInspectionNode(ref nd);
        }
        nd = (UltraTreeNode) null;
        str = key;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (AttachComponent));
        ProjectData.ClearProjectError();
      }
      return str;
    }

    private void AddInspSection()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;
        frmAttachSection frmAttachSection = new frmAttachSection();
        string section = frmAttachSection.GetSection(this.CurrentComp, this.CurrentLocation);
        frmAttachSection.Dispose();
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(section, "-1", false) <= 0U)
          return;
        this.AttachSection(this.CurrentComp, section, Section.SectionLabel(section));
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (AddInspSection));
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    public void AttachSection(string ComponentNodeKey, string SectionID, string SectionName)
    {
      try
      {
        if (this.tvInspection.GetNodeByKey(ComponentNodeKey) != null)
        {
          object nodeByKey = (object) mdUtility.fMainForm.tvInspection.GetNodeByKey(ComponentNodeKey);
          if (nodeByKey != null)
          {
            int num;
            if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectGreater(NewLateBinding.LateGet(NewLateBinding.LateGet(nodeByKey, (System.Type) null, "Nodes", new object[0], (string[]) null, (System.Type[]) null, (bool[]) null), (System.Type) null, "Count", new object[0], (string[]) null, (System.Type[]) null, (bool[]) null), (object) 0, false))
              num = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(nodeByKey, (System.Type) null, "Nodes", new object[1]
              {
                (object) 0
              }, (string[]) null, (System.Type[]) null, (bool[]) null), (System.Type) null, "Text", new object[0], (string[]) null, (System.Type[]) null, (bool[]) null), (System.Type) null, "ToLower", new object[0], (string[]) null, (System.Type[]) null, (bool[]) null), (object) "temp", false) ? 1 : 0;
            else
              num = 0;
            if (num != 0)
              NewLateBinding.LateCall(nodeByKey, (System.Type) null, "ExpandAll", new object[0], (string[]) null, (System.Type[]) null, (bool[]) null, true);
          }
          UltraTreeNode nd = this.tvInspection.GetNodeByKey(ComponentNodeKey).Nodes.Add("S-" + mdUtility.NewRandomNumberString() + SectionID, SectionName);
          nd.Tag = (object) SectionID;
          this.tvInspection.GetNodeByKey(ComponentNodeKey).Nodes.Override.Sort = SortType.Ascending;
          this.SelectNewActiveInspectionNode(ref nd);
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (AttachSection));
        ProjectData.ClearProjectError();
      }
    }

    public void DetachSection(string SectionKey)
    {
      try
      {
        if (this.tvInspection.GetNodeByKey(SectionKey) != null)
        {
          UltraTreeNode parent1 = this.tvInspection.GetNodeByKey(SectionKey).Parent;
          this.tvInspection.Nodes.Remove(this.tvInspection.GetNodeByKey(SectionKey));
          if (parent1.Nodes.Count == 0)
          {
            string key = parent1.Key;
            UltraTreeNode parent2 = parent1.Parent;
            this.tvInspection.Nodes.Remove(this.tvInspection.GetNodeByKey(key));
            if (parent2.Nodes.Count == 0)
              this.tvInspection.Nodes.Remove(this.tvInspection.GetNodeByKey(parent2.Key));
          }
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (DetachSection));
        ProjectData.ClearProjectError();
      }
    }

    private void OpenDatabase(string DatabasePath)
    {
      DataTable dataTable1 = (DataTable) null;
      mdUtility.ClearMstrSetTables();
      frmLoadBuildings frmLoadBuildings;
      try
      {
        mdUtility.DatabasePath = DatabasePath;
        mdUtility.BREDTableList = (ArrayList) null;
        if (mdUtility.BREDTableList != null)
        {
          this.Cursor = Cursors.WaitCursor;
          this.Text = "Builder RED - " + DatabasePath;
          this.tvInventory.ActiveNode = (UltraTreeNode) null;
          this.tvInspection.ActiveNode = (UltraTreeNode) null;
          this.tvFunctionality.ActiveNode = (UltraTreeNode) null;
          mdUtility.PackageFileName = DatabasePath.Remove(DatabasePath.LastIndexOf(".")) + ".bredpackage";
          frmLoadBuildings = new frmLoadBuildings("While tables are being loaded......");
          frmLoadBuildings.ShowDialog(new Action(this.SetupDatabases));
          if (frmLoadBuildings.DialogResult == DialogResult.Abort)
            throw new Exception(frmLoadBuildings.ErrorMessage);
          MySettingsProperty.Settings.LastOpened = DatabasePath;
          DataRow row = mdUtility.DB.GetDataTable("Select * from Organization where Org_Type = 1").Rows[0];
          if (row["ORG_ID"].GetType() != typeof (Guid))
            throw new Exception("The file you are attempting to open is incompatible with this version of BUILDER RED.  Please re-export your file or downgrade your BUILDER RED to 3.3.1.3");
          this._siteID = new Guid(row["ORG_ID"].ToString());
          MySettingsProperty.Settings.ImageLinkSite = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) (row["ORG_ID"].ToString() + "|"), Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["ORG_NO"])) ? (object) "" : row["ORG_NO"]), (object) "-"), Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["ORG_NAME"])) ? (object) "" : row["ORG_NAME"]));
          MySettingsProperty.Settings.Save();
          DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM Configuration where [ConfigName] = 'Branch'");
          string sSQL = dataTable2.Rows.Count <= 0 ? "Select * from RO_UseType ORDER BY usetype_and_desc" : (!Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(dataTable2.Rows[0]["ConfigValue"]), (object) ""), (object) "", false) && !Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(dataTable2.Rows[0]["ConfigValue"], (object) "Z", false) ? Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SELECT * from RO_UseType WHERE BRANCH= '", dataTable2.Rows[0]["ConfigValue"]), (object) "' ORDER BY usetype_and_desc")) : "SELECT * FROM RO_UseType ORDER BY usetype_and_desc");
          dataTable1 = (DataTable) null;
          this.m_bDDLoad = true;
          mdUtility.LoadMstrTable("CatCode", sSQL);
          this.cboCatCode.ValueMember = "USETYPE_ID";
          this.cboCatCode.DisplayMember = "USETYPE_AND_DESC";
          this.cboCatCode.DataSource = (object) mdUtility.get_MstrTable("CatCode");
          this.m_bDDLoad = false;
          this.FormatFieldLengths();
          mdHierarchyFunction.LoadInventoryTree();
          this.tvInventory.Refresh();
          mdHierarchyFunction.LoadInspectionTree();
          this.tvInspection.Refresh();
          mdHierarchyFunction.LoadFunctionalityTree();
          this.tvFunctionality.Refresh();
          this.Mode = frmMain.ProgramMode.pmInventory;
          if (this.tvInventory.Nodes.Count > 0)
          {
            this.tvInventory.Enabled = true;
            this.tvInventory.ActiveNode = this.tvInventory.Nodes[0];
            this.tvInspection.Enabled = true;
            this.tvInspection.ActiveNode = this.tvInspection.Nodes[0];
            this.tvFunctionality.Enabled = true;
            this.tvFunctionality.ActiveNode = this.tvFunctionality.Nodes[0];
            this.HelpProvider.SetHelpKeyword((Control) this.tvInventory, "Introduction to BuilderRED");
            this.tsToolbar.Items[1].Enabled = true;
            this.miFileInspector.Enabled = true;
            this.miView.Enabled = true;
            this.miTools.Enabled = true;
            this.miInventoryMode.Enabled = true;
            this.tsToolbar.Items[0].Enabled = true;
          }
          else
          {
            mdRightFormHandler.SetCurrentFrame(this.pnlStartup);
            this.miFileInspector.Enabled = false;
            this.miView.Enabled = false;
            this.miTools.Enabled = false;
            this.miInventoryMode.Enabled = false;
            this.tsToolbar.Items[0].Enabled = false;
            this.tsslInspector.Text = "";
            this.tsToolbar.Enabled = true;
          }
          DataTable dataTable3 = mdUtility.DB.GetDataTable("SELECT * FROM Configuration where [ConfigName] = 'UseBREDEnergyForm'");
          mdUtility.UseEnergyForm = dataTable3.Rows.Count > 0 && mdUtility.BREDTableList.Contains((object) "Efficiency_Assessment") && Conversions.ToBoolean(Interaction.IIf(Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Conversions.ToString(dataTable3.Rows[0]["ConfigValue"]).ToUpper(), "TRUE", false) == 0, (object) true, (object) false));
          dataTable1 = (DataTable) null;
          this.lblEnergyAuditRequired.Visible = mdUtility.UseEnergyForm;
          this.chkEnergyAuditRequired.Visible = mdUtility.UseEnergyForm;
          if (!this.alertFormsHide.IsChecked)
          {
            string Left = "You must update your Builder Website or your Database if you wish to use following forms: ";
            this.alertFormsUpdate.ContentText = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "You must update your Builder Website or your Database if you wish to use following forms: ", false) <= 0U ? (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.LookupDataBaseName, "Lookup", false) == 0 ? "Using Default Lookup" : "Using " + mdUtility.LookupDataBaseName) : Left + "\r\n" + (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.LookupDataBaseName, "Lookup", false) == 0 ? "Using Default Lookup" : "Using " + mdUtility.LookupDataBaseName);
            this.alertFormsUpdate.Show();
          }
          this._packageFileName = DatabasePath.Remove(DatabasePath.LastIndexOf(".")) + ".bredpackage";
          this.SetInventoryClassForImages(this._InventoryClass.Value, this._SelectedID.ToString());
          if (!Information.IsNothing((object) mdUtility.fWorkItems))
          {
            mdUtility.fWorkItems.Close();
            mdUtility.fWorkItems.Dispose();
          }
          if (mdUtility.BREDTableList.Contains((object) "WorkItem"))
          {
            this._isWorkItemTableExist = true;
            this.tsbWorkItems.Enabled = true;
            mdUtility.fWorkItems = new frmWorkItems();
          }
          else
          {
            this._isWorkItemTableExist = false;
            this.tsbWorkItems.Enabled = false;
          }
          try
          {
            foreach (Form openForm in (ReadOnlyCollectionBase) Application.OpenForms)
            {
              if (openForm is frmReportsViewer)
              {
                openForm.Close();
                break;
              }
            }
          }
          finally
          {
            IEnumerator enumerator;
            if (enumerator is IDisposable)
              (enumerator as IDisposable).Dispose();
          }
          this.m_ReverseFuncArea = mdUtility.BREDTableList.Contains((object) "Assessment_Response_Image");
          this.SelectInspector();
        }
        else
        {
          int num = (int) Interaction.MsgBox((object) "The selected file is not a valid current database for Builder RED.\rPlease check that the correct file has been selected.", MsgBoxStyle.Critical, (object) "Problem encountered.");
          this.tvInspection.Nodes.Clear();
          this.tvInventory.Nodes.Clear();
          this.tvFunctionality.Nodes.Clear();
          mdRightFormHandler.SetCurrentFrame(this.pnlStartup);
          this.miFileInspector.Enabled = false;
          this.miInventoryMode.Enabled = false;
          this.miInspectionMode.Enabled = false;
          this.miFunctionalityMode.Enabled = false;
          this.tsToolbar.Items[0].Enabled = false;
        }
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        Exception ex2 = ex1;
        if (Information.Err().Number == 3011)
        {
          int num = (int) Interaction.MsgBox((object) "You have chosen an invalid database.  Please choose another one", MsgBoxStyle.OkOnly, (object) null);
          if (!Information.IsNothing((object) frmLoadBuildings))
            frmLoadBuildings.Close();
        }
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(ex2.Message, "Specified lookup file does not exist", false) == 0 || ex2.Message.Contains("Please re-export your file or downgrade your BUILDER RED to 3.3.1.3"))
        {
          int num1 = (int) Interaction.MsgBox((object) ex2.Message, MsgBoxStyle.OkOnly, (object) null);
        }
        else
        {
          mdUtility.Errorhandler(ex2, this.Name, nameof (OpenDatabase));
          if (!Information.IsNothing((object) frmLoadBuildings))
            frmLoadBuildings.Close();
        }
        this.tvInspection.Nodes.Clear();
        this.tvInventory.Nodes.Clear();
        this.tvFunctionality.Nodes.Clear();
        mdRightFormHandler.SetCurrentFrame(this.pnlStartup);
        this.miFileInspector.Enabled = false;
        this.miInventoryMode.Enabled = false;
        this.miInspectionMode.Enabled = false;
        this.miFunctionalityMode.Enabled = false;
        this.tsToolbar.Items[0].Enabled = false;
        this.ClearStatusBarText();
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
        if (!this.m_ReverseFuncArea)
        {
          this.FunctionalityToolStripMenuItem.Enabled = false;
          this.miFunctionalityMode.Enabled = false;
        }
        else
        {
          this.FunctionalityToolStripMenuItem.Enabled = true;
          this.miFunctionalityMode.Enabled = true;
        }
      }
    }

    private void ClearStatusBarText()
    {
      try
      {
        foreach (ToolStripItem toolStripItem in (ArrangedElementCollection) this.ssStatusStrip.Items)
          toolStripItem.Text = "";
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
    }

    private void SetupDatabases()
    {
      DataTable dataTable1 = (DataTable) null;
      ArrayList oTableNames1 = mdUtility.TableList(mdUtility.DatabasePath, "erdccerl");
      if (oTableNames1 == null)
        return;
      this.Text = "Builder RED - " + mdUtility.DatabasePath;
      string databasePath = mdUtility.DatabasePath;
      mdUtility.DBLinker(ref databasePath, oTableNames1, "erdccerl");
      mdUtility.DatabasePath = databasePath;
      DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT * FROM AppVer_Inventory");
      mdUtility.LookupDataBaseName = dataTable2.Rows.Count <= 0 ? "Lookup" : (dataTable2.Columns.IndexOf("LookupDatabase") <= 0 || Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable2.Rows[0]["LookupDatabase"])) ? "Lookup" : Conversions.ToString(dataTable2.Rows[0]["LookupDatabase"]));
      DataTable dataTable3 = mdUtility.DB.GetDataTable("SELECT * FROM Configuration where [ConfigName] = 'LookupDatabase'");
      if (dataTable3.Rows.Count > 0)
      {
        mdUtility.LookupDataBaseName = Conversions.ToString(dataTable3.Rows[0]["ConfigValue"]);
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.LookupDataBaseName, "", false) == 0)
          mdUtility.LookupDataBaseName = "Lookup";
      }
      dataTable1 = (DataTable) null;
      if (!System.IO.File.Exists(mdUtility.CommonApplicationDataDirectory + "\\" + mdUtility.LookupDataBaseName + ".mdb") && (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.get_ConfigValue("BredLkpUri"), "", false) == 0 || Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.get_ConfigValue("BredLkpUri"), "", false) != 0 && (Interaction.MsgBox((object) "The lookup data for this BRED file is missing.  Would you like to download it? An internet connection is required.", MsgBoxStyle.YesNo, (object) null) != MsgBoxResult.Yes || !this.GetLatestLookup())))
        throw new FileNotFoundException("Specified lookup file does not exist", mdUtility.CommonApplicationDataDirectory + "\\" + mdUtility.LookupDataBaseName + ".mdb");
      ArrayList oTableNames2 = mdUtility.TableList(mdUtility.CommonApplicationDataDirectory + "\\" + mdUtility.LookupDataBaseName + ".mdb", "fidelity");
      if (oTableNames2 != null)
      {
        string SourceDB = mdUtility.CommonApplicationDataDirectory + "\\" + mdUtility.LookupDataBaseName + ".mdb";
        DBAccess dbLookup = new DBAccess(IDBAccess.DBServerType.OleDB, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + SourceDB + ";Jet OLEDB:Database Password=fidelity;");
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.get_ConfigValue("BredLkpUri"), "", false) != 0 && this.LookupIsOutOfDate(dbLookup) && Interaction.MsgBox((object) "Your lookup data for this Builder RED file is out of date.  Would you like to download it now? An internet connection is required.", MsgBoxStyle.YesNo, (object) null) == MsgBoxResult.Yes)
          this.GetLatestLookup();
        mdUtility.DBLinker(ref SourceDB, oTableNames2, "fidelity");
        dbLookup.Dispose();
      }
      mdUtility.ReloadDataAccess();
    }

    private void frmMain_Load(object eventSender, EventArgs eventArgs)
    {
      try
      {
        this.m_bLoading = true;
        this.SetUpHelpReferences();
        this.m_bBldgLoaded = true;
        this.BldgNeedToSave = false;
        this.m_bSectionLoaded = true;
        this.m_bSectionNeedToSave = false;
        this.m_bInspLoaded = true;
        this.m_bInspNeedToSave = false;
        this.m_bDistressWarn = false;
        MySettingsProperty.Settings.Reload();
        this.miOpenLastFile.Checked = MySettingsProperty.Settings.OpenLastFile;
        this.alertFormsHide.IsChecked = MySettingsProperty.Settings.HideAlertForms;
        this._ToggleView = 1;
        this.cboConstructionType.ValueMember = "CONST_TYPE_ID";
        this.cboConstructionType.DisplayMember = "CONST_TYPE_DESC";
        this.cboConstructionType.DataSource = (object) mdUtility.DB.GetDataTable("SELECT * FROM RO_Construction_type");
        this.cboSectionPaintType.SelectedValueChanged -= new EventHandler(this.SectionChanged);
        this.cboSectionPaintType.ValueMember = "Paint_TYPE_ID";
        this.cboSectionPaintType.DisplayMember = "PAINT_TYPE_DESC";
        this.cboSectionPaintType.DataSource = (object) mdUtility.DB.GetDataTable("SELECT * FROM qryPaintTypes");
        this.cboSectionPaintType.SelectedValueChanged += new EventHandler(this.SectionChanged);
        this.tvInspection.Nodes.Clear();
        this.tvInventory.Nodes.Clear();
        this.tvFunctionality.Nodes.Clear();
        mdRightFormHandler.SetCurrentFrame(this.pnlStartup);
        if (MySettingsProperty.Settings.OpenLastFile)
        {
          string lastOpened = MySettingsProperty.Settings.LastOpened;
          if (System.IO.File.Exists(lastOpened))
          {
            this.OpenDatabase(lastOpened);
          }
          else
          {
            int num = (int) Interaction.MsgBox((object) "Previous inspection file not found.", MsgBoxStyle.OkOnly, (object) null);
            mdRightFormHandler.SetCurrentFrame(this.pnlStartup);
            this.miFileInspector.Enabled = false;
            this.miInventoryMode.Enabled = false;
            this.miInspectionMode.Enabled = false;
            this.miFunctionalityMode.Enabled = false;
            this.tsToolbar.Items[0].Enabled = false;
            this.tsslInspector.Text = "";
            this.tsToolbar.Enabled = false;
            MySettingsProperty.Settings.OpenLastFile = false;
            MySettingsProperty.Settings.Save();
            this.miOpenLastFile.Checked = false;
          }
        }
        this.HelpProvider.SetHelpKeyword((Control) this.tvInventory, "Introduction to BuilderRED");
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, "Form_Load");
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.m_bLoading = false;
        this.Cursor = Cursors.Default;
      }
    }

    public void miToolsInspectSections_Click(object eventSender, EventArgs eventArgs)
    {
      try
      {
        if (!this.DataHasBeenSaved())
          return;
        new dlgInspectSections().InspectSections(this.CurrentBldg, this.tvInspection.ActiveNode.Tag.ToString(), this.tvInspection.ActiveNode.Key.ToString(), this.tvInspection.ActiveNode.Text);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (miToolsInspectSections_Click));
        ProjectData.ClearProjectError();
      }
    }

    public void miViewRefresh_Click(object eventSender, EventArgs eventArgs)
    {
      try
      {
        string currentBldg = this.CurrentBldg;
        mdHierarchyFunction.LoadInventoryTree();
        mdHierarchyFunction.LoadInspectionTree();
        mdHierarchyFunction.LoadFunctionalityTree();
        if (currentBldg != null && (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(currentBldg, "", false) > 0U)
        {
          this.tvInventory.ActiveNode = this.tvInventory.GetNodeByKey(currentBldg);
          this.tvInspection.ActiveNode = this.tvInspection.GetNodeByKey(currentBldg);
          this.tvFunctionality.ActiveNode = this.tvFunctionality.GetNodeByKey(currentBldg);
        }
        else
        {
          if (this.tvInventory.Nodes.Count > 0)
            this.tvInventory.ActiveNode = this.tvInventory.Nodes[0];
          if (this.tvInspection.Nodes.Count > 0)
            this.tvInspection.ActiveNode = this.tvInspection.Nodes[0];
          if (this.tvFunctionality.Nodes.Count > 0)
            this.tvFunctionality.ActiveNode = this.tvFunctionality.Nodes[0];
        }
        if (this.Mode == frmMain.ProgramMode.pmInventory)
        {
          UltraTree tvInventory;
          UltraTreeNode activeNode = (tvInventory = this.tvInventory).ActiveNode;
          this.SelectNewActiveInventoryNode(ref activeNode);
          tvInventory.ActiveNode = activeNode;
        }
        else if (this.Mode == frmMain.ProgramMode.pmInspection)
        {
          UltraTree tvInspection;
          UltraTreeNode activeNode = (tvInspection = this.tvInspection).ActiveNode;
          this.SelectNewActiveInspectionNode(ref activeNode);
          tvInspection.ActiveNode = activeNode;
        }
        else
        {
          UltraTree tvFunctionality;
          UltraTreeNode activeNode = (tvFunctionality = this.tvFunctionality).ActiveNode;
          this.SelectNewActiveFunctionalityNode(ref activeNode);
          tvFunctionality.ActiveNode = activeNode;
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (miViewRefresh_Click));
        ProjectData.ClearProjectError();
      }
    }

    private void BldgComments()
    {
      try
      {
        string str1 = Building.LabelByID(this.m_strBldgID);
        string str2 = "SELECT * FROM Facility WHERE [Facility_ID]={" + this.m_strBldgID + "}";
        DataTable dataTable = mdUtility.DB.GetDataTable(str2);
        if (dataTable.Rows.Count <= 0)
          return;
        frmComment frmComment = new frmComment();
        if (frmComment.EditComment("Inventory comments for Building: " + str1, RuntimeHelpers.GetObjectValue(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["bldg_comments"]), (object) "")), !this.txtBuildingName.ReadOnly, (IWin32Window) null) == DialogResult.Yes && !this.txtBuildingName.ReadOnly)
        {
          if (!Building.BuildingIsNew(this.m_strBldgID))
            dataTable.Rows[0]["BRED_Status"] = (object) "U";
          if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(frmComment.Comment, "", false) > 0U)
          {
            dataTable.Rows[0]["bldg_comments"] = (object) frmComment.Comment;
            this.tsbComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard_Check;
          }
          else
          {
            dataTable.Rows[0]["bldg_comments"] = (object) DBNull.Value;
            this.tsbComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard;
          }
          mdUtility.DB.SaveDataTable(ref dataTable, str2);
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (BldgComments));
        ProjectData.ClearProjectError();
      }
    }

    private void InspComments()
    {
      try
      {
        string str = "SELECT * FROM inspection_data WHERE [insp_data_id]={" + Microsoft.VisualBasic.Strings.Replace(this.CurrentInspection, "'", "''", 1, -1, CompareMethod.Binary) + "}";
        DataTable dataTable = mdUtility.DB.GetDataTable(str);
        if (dataTable.Rows.Count <= 0)
          return;
        frmComment frmComment = new frmComment();
        if (frmComment.EditComment("Inspection comments for inspection on " + Conversions.ToString(dataTable.Rows[0]["insp_data_insp_date"]), RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["insp_data_comments"]), this.CanEditInspection, (IWin32Window) null) == DialogResult.Yes && this.CanEditInspection)
        {
          if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["BRED_Status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(dataTable.Rows[0]["BRED_Status"], (object) "N", false))
            dataTable.Rows[0]["BRED_Status"] = (object) "U";
          if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(frmComment.Comment, "", false) > 0U)
          {
            dataTable.Rows[0]["insp_data_comments"] = (object) frmComment.Comment;
            this.tsbComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard_Check;
          }
          else
          {
            dataTable.Rows[0]["insp_data_comments"] = (object) DBNull.Value;
            this.tsbComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard;
          }
          mdUtility.DB.SaveDataTable(ref dataTable, str);
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (InspComments));
        ProjectData.ClearProjectError();
      }
    }

    private void SetUpHelpReferences()
    {
      this.HelpProvider.HelpNamespace = mdUtility.HelpFilePath;
      this.HelpProvider.SetHelpKeyword((Control) this, "Introduction to BuilderRED");
      this.HelpProvider.SetHelpKeyword((Control) this.tvInspection, "Getting Started in Inspection");
      this.HelpProvider.SetHelpKeyword((Control) this.tvInventory, "Introduction to BuilderRED");
      this.HelpProvider.SetHelpKeyword((Control) this.txtAddress, "Street Address");
      this.HelpProvider.SetHelpKeyword((Control) this.txtBldgArea, "Building area");
      this.HelpProvider.SetHelpKeyword((Control) this.txtBuildingNumber, "Building Basics");
      this.HelpProvider.SetHelpKeyword((Control) this.txtBuildingName, "Building name");
      this.HelpProvider.SetHelpKeyword((Control) this.txtCity, "City");
      this.HelpProvider.SetHelpKeyword((Control) this.txtNoFloors, "Number of floors");
      this.HelpProvider.SetHelpKeyword((Control) this.txtPOC, "Name of Point of Contact");
      this.HelpProvider.SetHelpKeyword((Control) this.txtPOCEmail, "Email Point of Contact");
      this.HelpProvider.SetHelpKeyword((Control) this.txtPOCPhone, "Phone point of contact");
      this.HelpProvider.SetHelpKeyword((Control) this.txtSQuantity, "Sectioning Process");
      this.HelpProvider.SetHelpKeyword((Control) this.txtState, "State");
      this.HelpProvider.SetHelpKeyword((Control) this.txtYearBuilt, "Year Built");
      this.HelpProvider.SetHelpKeyword((Control) this.txtZipCode, "Zip code");
      this.HelpProvider.SetHelpKeyword((Control) this.frmDirectRating, "Direct Rating Definition");
      this.HelpProvider.SetHelpKeyword((Control) this.frmDistressSurvey, "Distress Survey Definition");
      this.HelpProvider.SetHelpKeyword((Control) this.pnlBuildingInfo, "Getting Started with Inventory");
      this.HelpProvider.SetHelpKeyword((Control) this.pnlInspectionInfo, "Getting Started with Inspection");
      this.HelpProvider.SetHelpKeyword((Control) this.frmDistressSurvey, "Distress Surveys");
      this.HelpProvider.SetHelpKeyword((Control) this.pnlSectionInfo, "Section Information");
    }

    public void miFileClose_Click(object eventSender, EventArgs eventArgs)
    {
      this.Close();
    }

    public void miFileImport_Click(object eventSender, EventArgs eventArgs)
    {
      try
      {
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(mdUtility.DatabasePath, "", false) != 0 && !this.DataHasBeenSaved())
          return;
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Builder Data (*.mdb)|*.mdb";
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
          string fileName = openFileDialog.FileName;
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(fileName, "", false) == 0)
            return;
          this.Cursor = Cursors.WaitCursor;
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Microsoft.VisualBasic.Strings.UCase(Path.GetFileName(fileName)), "BRED.MDB", false) == 0)
          {
            int num = (int) Interaction.MsgBox((object) "You need to choose an export file you created in Builder to export.\r\nThe bred.mdb file is a system file used by the BuilderRED program.", MsgBoxStyle.Critical, (object) null);
          }
          else
            this.OpenDatabase(fileName);
        }
        this.Refresh();
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        Exception ex2 = ex1;
        if (Information.Err().Number == 3011)
        {
          int num = (int) Interaction.MsgBox((object) "You have chosen an invalid database.  Please choose another one", MsgBoxStyle.OkOnly, (object) null);
        }
        else
          mdUtility.Errorhandler(ex2, this.Name, nameof (miFileImport_Click));
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    public void miFileInspector_Popup(object eventSender, EventArgs eventArgs)
    {
      this.miFileInspector_Click(RuntimeHelpers.GetObjectValue(eventSender), eventArgs);
    }

    public void miFileInspector_Click(object eventSender, EventArgs eventArgs)
    {
      this.SelectInspector();
    }

    private bool LookupIsOutOfDate(DBAccess dbLookup)
    {
      bool flag;
      try
      {
        DataRow row = dbLookup.GetDataTable("SELECT * FROM AppVer_Lookup").Rows[0];
        flag = new Version(mdUtility.get_ConfigValue("BredLkpVersion")) > new Version(Conversions.ToInteger(row[0]), Conversions.ToInteger(row[1]), Conversions.ToInteger(row[2]), Conversions.ToInteger(row[3]));
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        int num = (int) MessageBox.Show("Unable to check lookup version number. Please contact your Builder administrator.");
        flag = false;
        ProjectData.ClearProjectError();
      }
      return flag;
    }

    private bool GetLatestLookup()
    {
      string str1 = mdUtility.get_ConfigValue("BredLkpUri");
      string address1;
      string address2;
      if (!str1.EndsWith(mdUtility.LookupDataBaseName + ".mdb") && !str1.EndsWith(mdUtility.LookupDataBaseName + ".zip"))
      {
        address1 = str1.Trim('/') + "/" + mdUtility.LookupDataBaseName + ".mdb";
        address2 = str1.Trim('/') + "/" + mdUtility.LookupDataBaseName + ".zip";
      }
      else
      {
        address1 = str1;
        address2 = str1;
      }
      string str2 = "Lookup data update completed.";
      try
      {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        using (frmMain.MyClient myClient = new frmMain.MyClient())
        {
          myClient.HeadOnly = true;
          myClient.DownloadString(address2);
        }
        using (WebClient webClient = new WebClient())
          webClient.DownloadFile(address2, mdUtility.CommonApplicationDataDirectory + "\\" + mdUtility.LookupDataBaseName + ".zip");
        try
        {
          this.UnzipLookup(mdUtility.CommonApplicationDataDirectory + "\\" + mdUtility.LookupDataBaseName + ".zip");
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          Exception exception = ex;
          str2 = "Error occured during extraction";
          throw exception;
        }
        finally
        {
          try
          {
            MyProject.Computer.FileSystem.DeleteFile(mdUtility.CommonApplicationDataDirectory + "\\" + mdUtility.LookupDataBaseName + ".zip");
          }
          catch (Exception ex)
          {
            ProjectData.SetProjectError(ex);
            ProjectData.ClearProjectError();
          }
        }
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        try
        {
          ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
          using (frmMain.MyClient myClient = new frmMain.MyClient())
          {
            myClient.HeadOnly = true;
            myClient.DownloadString(address1);
          }
          using (WebClient webClient = new WebClient())
            webClient.DownloadFile(address1, mdUtility.CommonApplicationDataDirectory + "\\" + mdUtility.LookupDataBaseName + ".mdb");
        }
        catch (Exception ex2)
        {
          ProjectData.SetProjectError(ex2);
          str2 = "Error occurred during download. Lookup data not updated.";
          ProjectData.ClearProjectError();
        }
        ProjectData.ClearProjectError();
      }
      int num = (int) MessageBox.Show(str2);
      return Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str2, "Lookup data update completed.", false) == 0;
    }

    private void UnzipLookup(string ZipToUnpack)
    {
      IEnumerator<ZipEntry> enumerator;
      using (ZipFile zipFile = ZipFile.Read(ZipToUnpack))
      {
        try
        {
          enumerator = zipFile.GetEnumerator();
          while (enumerator.MoveNext())
            enumerator.Current.Extract(mdUtility.CommonApplicationDataDirectory, ExtractExistingFileAction.OverwriteSilently);
        }
        finally
        {
          enumerator?.Dispose();
        }
      }
    }

    public void miHelpAbout_Click(object eventSender, EventArgs eventArgs)
    {
      int num = (int) new frmAboutBRED().ShowDialog();
    }

    public void miHelpContents_Click(object eventSender, EventArgs eventArgs)
    {
      try
      {
        Help.ShowHelp((Control) this, mdUtility.HelpFilePath, HelpNavigator.TableOfContents);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (miHelpContents_Click));
        ProjectData.ClearProjectError();
      }
    }

    private void DeleteBuilding()
    {
      try
      {
        if (Building.BuildingIsNew(this.CurrentBldg))
        {
          if (Interaction.MsgBox((object) "Are you sure you want to delete \rthis building and its associated\rsystems, components, and sections?", MsgBoxStyle.OkCancel | MsgBoxStyle.Critical | MsgBoxStyle.MsgBoxHelp, (object) "Are you sure?") != MsgBoxResult.Ok)
            return;
          Building.DeleteBuilding(this.CurrentBldg);
          this.Cursor = Cursors.WaitCursor;
          if (this.tvInventory.Nodes.Count > 0)
          {
            TreeNodesCollection nodes;
            UltraTreeNode nd = (nodes = this.tvInventory.Nodes)[0];
            this.SelectNewActiveInventoryNode(ref nd);
            nodes[0] = nd;
          }
          this.Cursor = Cursors.Default;
        }
        else
        {
          int num = (int) Interaction.MsgBox((object) "Only new buildings added during this inspection process\rmay be deleted here.", MsgBoxStyle.Critical, (object) "Unable to continue");
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (DeleteBuilding));
        ProjectData.ClearProjectError();
      }
    }

    private void DeleteComponent()
    {
      try
      {
        if (this.tvInventory.GetNodeByKey(this.CurrentComp) == null)
          return;
        string key = this.tvInventory.GetNodeByKey(this.CurrentComp).Parent.Key;
        if (Interaction.MsgBox((object) "Are you sure you want to delete \rthis component and its associated\rsections?", MsgBoxStyle.OkCancel | MsgBoxStyle.Critical | MsgBoxStyle.MsgBoxHelp, (object) "Are you sure?") == MsgBoxResult.Ok)
          Component.DeleteComponent(this.CurrentComp);
        UltraTreeNode nodeByKey = this.tvInventory.GetNodeByKey(key);
        this.SelectNewActiveInventoryNode(ref nodeByKey);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (DeleteComponent));
        ProjectData.ClearProjectError();
      }
    }

    private void DeleteSection()
    {
      try
      {
        if (this.tvInventory.GetNodeByKey(this.CurrentSection) == null)
          return;
        string key = this.tvInventory.GetNodeByKey(this.CurrentSection).Parent.Key;
        if (Interaction.MsgBox((object) "Are you sure you want to delete \rthis section?", MsgBoxStyle.OkCancel | MsgBoxStyle.Critical | MsgBoxStyle.MsgBoxHelp, (object) "Are you sure?") == MsgBoxResult.Ok)
          Section.DeleteSection(this.CurrentSection);
        UltraTreeNode nodeByKey = this.tvInventory.GetNodeByKey(key);
        this.SelectNewActiveInventoryNode(ref nodeByKey);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (DeleteSection));
        ProjectData.ClearProjectError();
      }
    }

    private void DeleteSystem()
    {
      try
      {
        if (BuildingSystem.SystemIsNew(this.CurrentSystem))
        {
          if (this.tvInventory.GetNodeByKey(this.CurrentSystem) == null)
            return;
          string key = this.tvInventory.GetNodeByKey(this.CurrentSystem).Parent.Key;
          if (Interaction.MsgBox((object) "Are you sure you want to delete \rthis system and its associated\rcomponents and sections?", MsgBoxStyle.OkCancel | MsgBoxStyle.Critical | MsgBoxStyle.MsgBoxHelp, (object) "Are you sure?") == MsgBoxResult.Ok)
            BuildingSystem.DeleteSystem(this.CurrentSystem);
          UltraTreeNode nodeByKey = this.tvInventory.GetNodeByKey(key);
          this.SelectNewActiveInventoryNode(ref nodeByKey);
        }
        else
        {
          int num = (int) Interaction.MsgBox((object) "Only new systems added during this inspection process\rmay be deleted here.", MsgBoxStyle.Critical, (object) "Unable to continue");
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (DeleteSystem));
        ProjectData.ClearProjectError();
      }
    }

    private void DeleteFuncArea()
    {
      try
      {
        if (FunctionalArea.FuncAreaIsNew(this.CurrentFuncArea))
        {
          if (this.tvFunctionality.GetNodeByKey(this.CurrentFuncArea) == null)
            return;
          string key = this.tvFunctionality.GetNodeByKey(this.CurrentFuncArea).Parent.Key;
          if (Interaction.MsgBox((object) "Are you sure you want to delete the selected functional area?", MsgBoxStyle.OkCancel | MsgBoxStyle.Critical | MsgBoxStyle.MsgBoxHelp, (object) "Are you sure?") == MsgBoxResult.Ok)
            FunctionalArea.DeleteFuncArea(this.CurrentFuncArea);
          UltraTreeNode nodeByKey = this.tvFunctionality.GetNodeByKey(key);
          this.SelectNewActiveFunctionalityNode(ref nodeByKey);
        }
        else
        {
          int num = (int) Interaction.MsgBox((object) "Only new functional areas added within BRED may be deleted here.", MsgBoxStyle.Critical, (object) "Unable to continue");
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (DeleteFuncArea));
        ProjectData.ClearProjectError();
      }
    }

    public void miToolsCopyInventory_Popup(object eventSender, EventArgs eventArgs)
    {
      this.miToolsCopyInventory_Click(RuntimeHelpers.GetObjectValue(eventSender), eventArgs);
    }

    private void miToolsReports_Click(object sender, EventArgs e)
    {
      object Operand = (object) false;
      try
      {
        foreach (Form openForm in (ReadOnlyCollectionBase) Application.OpenForms)
        {
          if (openForm is frmReportsViewer)
          {
            openForm.BringToFront();
            Operand = (object) true;
            break;
          }
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      if (!Conversions.ToBoolean(Microsoft.VisualBasic.CompilerServices.Operators.NotObject(Operand)))
        return;
      frmReportsViewer frmReportsViewer = new frmReportsViewer(new ZipBredPackage(this._packageFileName, false));
      try
      {
        frmReportsViewer.Show();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, "Error opening Reports");
        ProjectData.ClearProjectError();
      }
    }

    public void miToolsCopyInventory_Click(object eventSender, EventArgs eventArgs)
    {
      try
      {
        if (!this.DataHasBeenSaved())
          return;
        int num = (int) new dlgCopySections().CopySections(this.CurrentBldg);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (miToolsCopyInventory_Click));
        ProjectData.ClearProjectError();
      }
    }

    public void miUnits_Popup(object eventSender, EventArgs eventArgs)
    {
      this.miUnits_Click(RuntimeHelpers.GetObjectValue(eventSender), eventArgs);
    }

    public void miUnits_Click(object eventSender, EventArgs eventArgs)
    {
      try
      {
        if (this.miUnits.Checked)
        {
          this.miUnits.Checked = false;
          mdUtility.Units = mdUtility.SystemofMeasure.umMetric;
        }
        else
        {
          this.miUnits.Checked = true;
          mdUtility.Units = mdUtility.SystemofMeasure.umEnglish;
        }
        if (this.Mode == frmMain.ProgramMode.pmInspection)
        {
          UltraTree tvInspection;
          UltraTreeNode activeNode = (tvInspection = this.tvInspection).ActiveNode;
          this.SelectNewActiveInspectionNode(ref activeNode);
          tvInspection.ActiveNode = activeNode;
        }
        else if (this.Mode == frmMain.ProgramMode.pmInventory)
        {
          UltraTree tvInventory;
          UltraTreeNode activeNode = (tvInventory = this.tvInventory).ActiveNode;
          this.SelectNewActiveInventoryNode(ref activeNode);
          tvInventory.ActiveNode = activeNode;
        }
        else
        {
          UltraTree tvFunctionality;
          UltraTreeNode activeNode = (tvFunctionality = this.tvFunctionality).ActiveNode;
          this.SelectNewActiveFunctionalityNode(ref activeNode);
          tvFunctionality.ActiveNode = activeNode;
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (miUnits_Click));
        ProjectData.ClearProjectError();
      }
    }

    public void miViewToggleDisplay_Popup(object eventSender, EventArgs eventArgs)
    {
      this.ToggleMode();
    }

    public void ToggleMode()
    {
      this.Mode = this._ToggleView != 1 ? (!(this._ToggleView == 2 & this.m_ReverseFuncArea) ? frmMain.ProgramMode.pmInventory : frmMain.ProgramMode.pmFunctionality) : frmMain.ProgramMode.pmInspection;
      this.tsslStatus.Text = mdUtility.LookupDataBaseName;
    }

    public void miInventoryMode_Click(object eventSender, EventArgs eventArgs)
    {
      if (!this.DataHasBeenSaved())
        return;
      if ((uint) this.Mode > 0U)
        this.Mode = frmMain.ProgramMode.pmInventory;
      this.tsslStatus.Text = mdUtility.LookupDataBaseName;
    }

    public bool SaveInspection()
    {
      bool flag1;
      try
      {
        if (this.optRatingType_1.Checked | this.optRatingType_3.Checked)
        {
          DataTable dataTable = mdUtility.get_MstrTable("SubComps");
          if (dataTable.Rows.Count > 0)
          {
            int num = checked (dataTable.Rows.Count - 1);
            int index = 0;
            while (index <= num)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(dataTable.Rows[index]["samp_subcomp_notappl"], (object) true, false))
              {
                if (!this.m_bDistressWarn)
                {
                  if (Interaction.MsgBox((object) "In order to calculate the CIs properly, all applicable subcomponents should be declared\r\ndefect-free or have defects recorded.  Have you inspected all applicable subcomponents?", MsgBoxStyle.OkCancel | MsgBoxStyle.Question, (object) "Reminder") == MsgBoxResult.Cancel)
                  {
                    flag1 = false;
                    goto label_19;
                  }
                  else
                  {
                    this.m_bDistressWarn = true;
                    break;
                  }
                }
                else
                  break;
              }
              else
                checked { ++index; }
            }
          }
        }
        bool flag2 = Inspection.SaveInspectionData(this.CurrentInspection);
        if (flag2)
        {
          this.SetInspChanged(false);
          this.m_bSampleNew = false;
          this.m_bInspNew = false;
        }
        flag1 = flag2;
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        Exception ex2 = ex1;
        if (Information.Err().Number != -2147217842)
          mdUtility.Errorhandler(ex2, this.Name, nameof (SaveInspection));
        flag1 = false;
        ProjectData.ClearProjectError();
      }
label_19:
      return flag1;
    }

    private void SecComments()
    {
      try
      {
        string str = "SELECT * FROM Component_Section WHERE [sec_id]={" + Microsoft.VisualBasic.Strings.Replace(this.CurrentSection, "'", "''", 1, -1, CompareMethod.Binary) + "}";
        DataTable dataTable = mdUtility.DB.GetDataTable(str);
        if (dataTable.Rows.Count <= 0)
          return;
        frmComment frmComment = new frmComment();
        string strCaption = "Inventory comments for Section - " + Section.SectionLabel(this.CurrentSection);
        if (frmComment.EditComment(strCaption, RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["sec_comments"]), !this.txtSectionAmount.ReadOnly, (IWin32Window) null) == DialogResult.Yes && !this.txtSectionAmount.ReadOnly)
        {
          if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["BRED_Status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(dataTable.Rows[0]["BRED_Status"], (object) "N", false))
            dataTable.Rows[0]["BRED_Status"] = (object) "U";
          if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Microsoft.VisualBasic.Strings.Trim(frmComment.Comment), "", false) > 0U)
          {
            dataTable.Rows[0]["sec_comments"] = (object) frmComment.Comment;
            this.tsbComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard_Check;
          }
          else
          {
            dataTable.Rows[0]["sec_comments"] = (object) DBNull.Value;
            this.tsbComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard;
          }
          mdUtility.DB.SaveDataTable(ref dataTable, str);
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (SecComments));
        ProjectData.ClearProjectError();
      }
    }

    public void SetBldgChanged(bool aflag)
    {
      if (this.m_bLoading || !this.m_bBldgLoaded)
        return;
      this.tsbSave.Enabled = aflag;
      this.tsbCancel.Enabled = aflag;
      this.BldgNeedToSave = aflag;
    }

    public void SetInspChanged(bool aflag)
    {
      if (!this.m_bInspLoaded)
        return;
      this.tsbSave.Enabled = aflag;
      this.tsbCancel.Enabled = aflag;
      this.m_bInspNeedToSave = aflag;
    }

    public void SetSectionChanged(bool aflag)
    {
      if (!this.m_bSectionLoaded)
        return;
      this.tsbSave.Enabled = aflag;
      this.tsbCancel.Enabled = aflag;
      this.m_bSectionNeedToSave = aflag;
    }

    public void UpdateTreeLabels(string SectionID)
    {
      string str = Section.SectionLabel(SectionID);
      try
      {
        this.tvInventory.GetNodeByKey(SectionID).Text = str;
        UltraTreeNode ultraTreeNode = (UltraTreeNode) null;
        UltraTreeNode nodeByKey = this.tvInspection.GetNodeByKey(SectionID);
        if (nodeByKey != null)
          nodeByKey.Text = str;
        ultraTreeNode = (UltraTreeNode) null;
        UltraTreeNode nodeByTag = this.tvInspection.GetNodeByTag(SectionID);
        if (nodeByTag != null)
          nodeByTag.Text = str;
        ultraTreeNode = (UltraTreeNode) null;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (UpdateTreeLabels));
        ProjectData.ClearProjectError();
      }
    }

    private void FormatFieldLengths()
    {
      try
      {
        DataTable tableSchema = mdUtility.DB.GetTableSchema("BuildingInfo");
        this.txtBuildingNumber.MaxLength = tableSchema.Columns["Number"].MaxLength;
        this.txtBuildingName.MaxLength = tableSchema.Columns["Name"].MaxLength;
        this.txtYearBuilt.MaxLength = 4;
        this.txtYearRenovated.MaxLength = 4;
        this.txtNoFloors.MaxLength = 2;
        this.txtBldgArea.MaxLength = 10;
        this.txtAddress.MaxLength = tableSchema.Columns["BLDG_STRT"].MaxLength;
        this.txtCity.MaxLength = tableSchema.Columns["BLDG_CITY"].MaxLength;
        this.txtState.MaxLength = tableSchema.Columns["BLDG_ST"].MaxLength;
        this.txtZipCode.MaxLength = tableSchema.Columns["BLDG_ZIP"].MaxLength;
        this.txtPOC.MaxLength = tableSchema.Columns["BLDG_POC_NAME"].MaxLength;
        this.txtPOCPhone.MaxLength = tableSchema.Columns["BLDG_POC_PH_NO"].MaxLength;
        this.txtPOCEmail.MaxLength = tableSchema.Columns["BLDG_POC_EMAIL"].MaxLength;
        this.txtSectionAmount.MaxLength = 10;
        this.txtSectionYearBuilt.MaxLength = 4;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (FormatFieldLengths));
        ProjectData.ClearProjectError();
      }
    }

    public string FindLocation(ref UltraTreeNode ParentNode, string strLocation)
    {
      string str;
      try
      {
        UltraTreeNode ultraTreeNode;
        if (ParentNode.Nodes.Count > 0)
          ultraTreeNode = ParentNode.Nodes[0];
        if (ParentNode.Nodes.Count == 0 || Microsoft.VisualBasic.CompilerServices.Operators.CompareString(ParentNode.Key + "1", ultraTreeNode.Key, false) == 0)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Microsoft.VisualBasic.Strings.Left(ParentNode.Key, 1), "L", false) == 0)
          {
            string Left = ParentNode.Tag.ToString().ToString();
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Non-sampling", false) != 0)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Sample Locations", false) == 0)
                mdHierarchyFunction.LoadInspectionSample(ParentNode.Key);
              else
                mdHierarchyFunction.LoadInspectionComponents(ParentNode.Tag.ToString(), ParentNode.Key);
            }
            else
              mdHierarchyFunction.LoadInspectionSystems(ParentNode.Key, ParentNode.Tag.ToString());
          }
          else
            mdHierarchyFunction.LoadInspectionSamples(ParentNode.Tag.ToString(), ParentNode.Key);
          if (ParentNode.Nodes.Count == 0)
          {
            str = "";
            goto label_22;
          }
          else
            ultraTreeNode = ParentNode.Nodes[0];
        }
        for (; Microsoft.VisualBasic.CompilerServices.Operators.CompareString(ultraTreeNode.Tag.ToString(), strLocation, false) != 0; ultraTreeNode = ultraTreeNode.GetSibling(NodePosition.Next))
        {
          if (!ultraTreeNode.HasSibling(NodePosition.Next))
          {
            str = "";
            goto label_21;
          }
        }
        str = ultraTreeNode.Key;
        goto label_22;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (FindLocation));
        ProjectData.ClearProjectError();
      }
label_21:
label_22:
      return str;
    }

    public string FindComponent(ref UltraTreeNode ParentNode, ref string ComponentID)
    {
      string str;
      try
      {
        UltraTreeNode ultraTreeNode;
        if (ParentNode.Nodes.Count > 0)
          ultraTreeNode = ParentNode.Nodes[0];
        if (ParentNode.Nodes.Count == 0 || Microsoft.VisualBasic.CompilerServices.Operators.CompareString(ParentNode.Key + "1", ultraTreeNode.Key, false) == 0)
        {
          mdHierarchyFunction.LoadInspectionComponents(ParentNode.Tag.ToString(), ParentNode.Key.ToString());
          if (ParentNode.Nodes.Count == 0)
          {
            str = "";
            goto label_15;
          }
          else
            ultraTreeNode = ParentNode.Nodes[0];
        }
        for (; Microsoft.VisualBasic.CompilerServices.Operators.CompareString(ultraTreeNode.Tag.ToString(), ComponentID, false) != 0; ultraTreeNode = ultraTreeNode.GetSibling(NodePosition.Next))
        {
          if (!ultraTreeNode.HasSibling(NodePosition.Next))
          {
            str = "";
            goto label_14;
          }
        }
        str = ultraTreeNode.Key;
        goto label_15;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (FindComponent));
        ProjectData.ClearProjectError();
      }
label_14:
label_15:
      return str;
    }

    public object FindSection(ref UltraTreeNode ParentNode, string SectionID)
    {
      object obj;
      try
      {
        UltraTreeNode ultraTreeNode;
        if (ParentNode.Nodes.Count > 0)
          ultraTreeNode = ParentNode.Nodes[0];
        if (ParentNode.Nodes.Count == 0 || Microsoft.VisualBasic.CompilerServices.Operators.CompareString(ParentNode.Key + "1", ultraTreeNode.Key, false) == 0)
        {
          mdHierarchyFunction.LoadInspectionSections(ParentNode.Tag.ToString(), ParentNode.Key.ToString());
          if (ParentNode.Nodes.Count == 0)
          {
            obj = (object) "";
            goto label_14;
          }
          else
            ultraTreeNode = ParentNode.Nodes[0];
        }
        for (; Microsoft.VisualBasic.CompilerServices.Operators.CompareString(ultraTreeNode.Tag.ToString(), SectionID, false) != 0; ultraTreeNode = ultraTreeNode.GetSibling(NodePosition.Next))
        {
          if (!ultraTreeNode.HasSibling(NodePosition.Next))
          {
            obj = (object) "";
            goto label_14;
          }
        }
        obj = (object) ultraTreeNode.Key;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (FindSection));
        ProjectData.ClearProjectError();
      }
label_14:
      return obj;
    }

    public void PurgeInspectionNode(ref UltraTreeNode nd)
    {
      try
      {
        nd.Nodes.Clear();
        nd.Parent.Nodes.Remove(nd);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, "PurgeInspectioNode");
        ProjectData.ClearProjectError();
      }
    }

    public void RefreshSections(ref string SectionID)
    {
      try
      {
        if (this.tvInspection.GetNodeByKey(SectionID) == null)
          return;
        UltraTreeNode nodeByKey = this.tvInspection.GetNodeByKey(SectionID);
        DataTable dataTable = mdUtility.DB.GetDataTable("SELECT DISTINCT samp_data_loc, Location FROM samples_by_sections WHERE [SEC_ID]={" + SectionID + "} AND (BRED_Status <> 'D' OR BRED_Status IS NULL)");
        try
        {
          foreach (DataRow row in dataTable.Rows)
          {
            if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["samp_data_loc"])) && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Conversions.ToString(this.FindSection(ref nodeByKey, Conversions.ToString(row["samp_data_loc"]))), "", false) == 0)
            {
              nodeByKey.Nodes.Add(Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) ("L-" + mdUtility.NewRandomNumberString()), row["samp_data_loc"])), Conversions.ToString(row["Location"])).Tag = RuntimeHelpers.GetObjectValue(row["samp_data_loc"]);
              nodeByKey.Nodes.Override.Sort = SortType.Ascending;
            }
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        TreeNodesCollection nodes = nodeByKey.Nodes;
        if (nodes.Count > 0)
        {
          if (dataTable.Rows.Count == 0)
          {
            nodes.Clear();
          }
          else
          {
            int index = checked (nodes.Count - 1);
            while (index >= 0)
            {
              if (dataTable.Select("samp_data_loc={" + Microsoft.VisualBasic.Strings.Replace(nodes[index].Tag.ToString(), "'", "''", 1, -1, CompareMethod.Binary) + "}").Length == 0)
                nodes.Remove(nodes[index]);
              checked { index += -1; }
            }
          }
        }
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        Exception ex2 = ex1;
        if (Information.Err().Number != 35601)
          mdUtility.Errorhandler(ex2, this.Name, nameof (RefreshSections));
        ProjectData.ClearProjectError();
      }
    }

    private void ResetCurrentKeys()
    {
      this.m_strBldgID = "";
      this.m_strLocation = "";
      this.m_strSysID = "";
      this.m_strSecID = "";
      this.m_strCompID = "";
      this.m_strAreaID = "";
    }

    public void RenameAllLocations(ref string BldgID, ref string OldName, ref string NewName)
    {
      try
      {
        DataTable dataTable = mdUtility.DB.GetDataTable("SELECT SEC_ID FROM qryReverseHierarchy WHERE (BRED_Status <> 'D' OR BRED_Status IS NULL) AND [Facility_ID]={" + BldgID + "}");
        try
        {
          foreach (DataRow row in dataTable.Rows)
          {
            try
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(OldName, "", false) == 0)
              {
                this.tvInspection.GetNodeByKey(Conversions.ToString(row["SEC_ID"])).Nodes.Add("L-" + mdUtility.NewRandomNumberString() + NewName, NewName).Tag = (object) NewName;
                this.tvInspection.GetNodeByKey(Conversions.ToString(row["SEC_ID"])).Nodes.Override.Sort = SortType.Ascending;
              }
              else
              {
                UltraTreeNode nodeByKey1 = this.tvInspection.GetNodeByKey(Conversions.ToString(row["SEC_ID"]));
                string location1 = this.FindLocation(ref nodeByKey1, OldName);
                UltraTreeNode nodeByKey2 = this.tvInspection.GetNodeByKey(Conversions.ToString(row["SEC_ID"]));
                string location2 = this.FindLocation(ref nodeByKey2, NewName);
                if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(location1, "", false) > 0U & Microsoft.VisualBasic.CompilerServices.Operators.CompareString(location2, "", false) == 0)
                {
                  this.tvInspection.GetNodeByKey(location1).Text = NewName;
                  this.tvInspection.GetNodeByKey(location1).Tag = (object) NewName;
                }
                else if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(location1, "", false) > 0U & (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(location1, "", false) > 0U)
                  this.tvInspection.Nodes.Remove(this.tvInspection.GetNodeByKey(location1));
              }
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              Exception exception = ex;
              if (Information.Err().Number != 35601)
                throw exception;
              ProjectData.ClearProjectError();
            }
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (RenameAllLocations));
        ProjectData.ClearProjectError();
      }
    }

    private void tvFunctionality_AfterActivate(object sender, NodeEventArgs e)
    {
      try
      {
        if (e.TreeNode != null && this.DataHasBeenSaved())
        {
          this.Cursor = Cursors.WaitCursor;
          this.ResetCurrentKeys();
          string Left = Conversions.ToString(e.TreeNode.Tag);
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Building", false) != 0)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "FuncArea", false) == 0)
            {
              this.m_strAreaID = e.TreeNode.Key;
              mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "FuncArea", e.TreeNode.Key);
            }
          }
          else
          {
            this.HelpProvider.SetHelpKeyword((Control) this.tvFunctionality, "Building Basics");
            this.m_strBldgID = e.TreeNode.Key;
            mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "Building", e.TreeNode.Key);
          }
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
      }
    }

    private void tvInventory_AfterActivate(object sender, NodeEventArgs e)
    {
      try
      {
        if (!this.DataHasBeenSaved())
          return;
        this.Cursor = Cursors.WaitCursor;
        this.ResetCurrentKeys();
        if (e.TreeNode != null)
        {
          string Left = e.TreeNode.Tag.ToString();
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Building", false) != 0)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "System", false) != 0)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Component", false) != 0)
              {
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Section", false) == 0)
                {
                  this.HelpProvider.SetHelpKeyword((Control) this.tvInventory, "Section Basics");
                  this.m_strBldgID = e.TreeNode.Parent.Parent.Parent.Key;
                  this.LoadSectionNames(this.m_strBldgID);
                  this.m_strSysID = e.TreeNode.Parent.Parent.Key;
                  this.m_strCompID = e.TreeNode.Parent.Key;
                  this.m_strSecID = e.TreeNode.Key;
                  this.SetInventoryClassForImages(InventoryClass.ComponentSection, e.TreeNode.Key);
                }
              }
              else
              {
                this.HelpProvider.SetHelpKeyword((Control) this.tvInventory, "Component Basics");
                this.m_strBldgID = e.TreeNode.Parent.Parent.Key;
                this.m_strSysID = e.TreeNode.Parent.Key;
                this.m_strCompID = e.TreeNode.Key;
                this.SetInventoryClassForImages(InventoryClass.SystemComponent, e.TreeNode.Key);
              }
            }
            else
            {
              this.HelpProvider.SetHelpKeyword((Control) this.tvInventory, "System Basics");
              this.m_strBldgID = e.TreeNode.Parent.Key;
              this.m_strSysID = e.TreeNode.Key;
              this.SetInventoryClassForImages(InventoryClass.BuildingSystem, e.TreeNode.Key);
            }
          }
          else
          {
            this.HelpProvider.SetHelpKeyword((Control) this.tvInventory, "Building Basics");
            this.m_strBldgID = e.TreeNode.Key;
            this.SetInventoryClassForImages(InventoryClass.Facility, e.TreeNode.Key);
          }
          mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, e.TreeNode.Tag.ToString(), e.TreeNode.Key);
        }
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        Exception ex2 = ex1;
        if (Information.Err().Number != 91)
          mdUtility.Errorhandler(ex2, this.Name, nameof (tvInventory_AfterActivate));
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    internal void SetInventoryClassForImages(InventoryClass ic, string id)
    {
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(id, "", false) == 0)
      {
        this.tsbImages.Text = "Images - 0";
        this.tsbImages.Enabled = false;
      }
      else
      {
        this._InventoryClass = new InventoryClass?(ic);
        if (id.Length > 38)
          id = id.Substring(2, 36);
        this._SelectedID = new Guid(id);
        this._ZP = new ZipBredPackage(this._packageFileName, false);
        this._ZP.LoadManifest();
        this.tsbImages.Text = "Images - " + this._ZP.ImageManifest.Where<IAttachmentInfo>((Func<IAttachmentInfo, bool>) (im =>
        {
          int inventoryClass1 = (int) im.InventoryClass;
          InventoryClass? inventoryClass2 = this._InventoryClass;
          int? nullable1 = inventoryClass2.HasValue ? new int?((int) inventoryClass2.GetValueOrDefault()) : new int?();
          bool? nullable2 = nullable1.HasValue ? new bool?(inventoryClass1 == nullable1.GetValueOrDefault()) : new bool?();
          return (im.InventoryId == this._SelectedID ? nullable2 : new bool?(false)).Value;
        })).ToList<IAttachmentInfo>().Count.ToString();
        this.tsbImages.Enabled = true;
      }
    }

    private void tvFunctionality_AfterCollapse(object sender, NodeEventArgs e)
    {
      try
      {
        this.ResetCurrentKeys();
        e.TreeNode.Selected = true;
        string Left = Conversions.ToString(e.TreeNode.Tag);
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Building", false) != 0)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "FuncArea", false) == 0)
            this.m_strAreaID = e.TreeNode.Key;
        }
        else
          this.m_strBldgID = e.TreeNode.Key;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
      }
    }

    private void tvInventory_AfterCollapse(object sender, NodeEventArgs e)
    {
      try
      {
        if (!this.CheckBldgForSave() || !this.CheckSecForSave())
          return;
        this.Cursor = Cursors.WaitCursor;
        this.ResetCurrentKeys();
        e.TreeNode.Selected = true;
        this.HelpProvider.SetHelpKeyword((Control) this.tvInventory, "Getting Started in Inventory");
        string Left = e.TreeNode.Tag.ToString();
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Building", false) != 0)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "System", false) != 0)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Component", false) == 0)
            {
              this.HelpProvider.SetHelpKeyword((Control) this.tvInventory, "Component Basics");
              this.m_strBldgID = e.TreeNode.Parent.Parent.Key;
              this.m_strSysID = e.TreeNode.Parent.Key;
              this.m_strCompID = e.TreeNode.Key;
            }
          }
          else
          {
            this.HelpProvider.SetHelpKeyword((Control) this.tvInventory, "System Basics");
            this.m_strBldgID = e.TreeNode.Parent.Key;
            this.m_strSysID = e.TreeNode.Key;
          }
        }
        else
        {
          this.HelpProvider.SetHelpKeyword((Control) this.tvInventory, "Building Basics");
          this.m_strBldgID = e.TreeNode.Key;
        }
        mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, e.TreeNode.Tag.ToString(), e.TreeNode.Key);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (tvInventory_AfterCollapse));
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private void tvFunctionality_AfterExpand(object sender, NodeEventArgs e)
    {
      try
      {
        this.ResetCurrentKeys();
        e.TreeNode.Selected = true;
        this.Cursor = Cursors.WaitCursor;
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Conversions.ToString(e.TreeNode.Tag), "Building", false) == 0)
        {
          this.m_strBldgID = e.TreeNode.Key;
          if (this.tvFunctionality.GetNodeByKey(e.TreeNode.Key + "1") != null && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.TreeNode.Nodes[0].Key, e.TreeNode.Key + "1", false) == 0)
            mdHierarchyFunction.LoadFunctionalAreas(e.TreeNode.Key);
        }
        this.tvFunctionality.ActiveNode = e.TreeNode;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (tvFunctionality_AfterExpand));
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private void tvInventory_AfterExpand(object sender, NodeEventArgs e)
    {
      try
      {
        this.HelpProvider.SetHelpKeyword((Control) this.tvInventory, "Getting Started in Inventory");
        this.ResetCurrentKeys();
        e.TreeNode.Selected = true;
        this.Cursor = Cursors.WaitCursor;
        string Left = e.TreeNode.Tag.ToString();
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Building", false) != 0)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "System", false) != 0)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Component", false) == 0)
            {
              this.HelpProvider.SetHelpKeyword((Control) this.tvInventory, "Component Basics");
              this.m_strBldgID = e.TreeNode.Parent.Parent.Key;
              this.m_strSysID = e.TreeNode.Parent.Key;
              this.m_strCompID = e.TreeNode.Key;
              if (this.tvInventory.GetNodeByKey(e.TreeNode.Key + "1") != null && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.TreeNode.Nodes[0].Key, e.TreeNode.Key + "1", false) == 0)
                mdHierarchyFunction.LoadInventorySections(e.TreeNode.Key);
            }
          }
          else
          {
            this.HelpProvider.SetHelpKeyword((Control) this.tvInventory, "System Basics");
            this.m_strBldgID = e.TreeNode.Parent.Key;
            this.m_strSysID = e.TreeNode.Key;
            if (this.tvInventory.GetNodeByKey(e.TreeNode.Key + "1") != null && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.TreeNode.Nodes[0].Key, e.TreeNode.Key + "1", false) == 0)
              mdHierarchyFunction.LoadInventoryComponents(e.TreeNode.Key);
          }
        }
        else
        {
          this.HelpProvider.SetHelpKeyword((Control) this.tvInventory, "Building Basics");
          this.m_strBldgID = e.TreeNode.Key;
          if (this.tvInventory.GetNodeByKey(e.TreeNode.Key + "1") != null && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.TreeNode.Nodes[0].Key, e.TreeNode.Key + "1", false) == 0)
            mdHierarchyFunction.LoadInventorySystems(e.TreeNode.Key);
        }
        this.tvInventory.ActiveNode = e.TreeNode;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (tvInventory_AfterExpand));
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private async void tvInspection_AfterActivate(object sender, NodeEventArgs e)
    {
      try
      {
        if (e.TreeNode != null)
        {
          if (this.m_bInspLoaded)
          {
            bool MustSave = false;
            if (!this.CheckInspForSave(ref MustSave))
              return;
          }
          this.Cursor = Cursors.WaitCursor;
          this.ResetCurrentKeys();
          string Left1 = e.TreeNode.Tag.ToString();
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "Building", false) != 0)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "Non-sampling", false) != 0)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "Sample Locations", false) != 0)
              {
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "System", false) != 0)
                {
                  if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "Component", false) != 0)
                  {
                    if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "Section", false) == 0)
                    {
                      this.m_strBldgID = e.TreeNode.Parent.Parent.Parent.Parent.Key;
                      this.m_strSysID = e.TreeNode.Parent.Parent.Key;
                      this.m_strCompID = e.TreeNode.Parent.Key;
                      this.m_strSecID = e.TreeNode.Key;
                      mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "Inspection", e.TreeNode.Key);
                    }
                    else
                    {
                      string Left2 = Microsoft.VisualBasic.Strings.Left(e.TreeNode.Key, 1);
                      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left2, "L", false) != 0)
                      {
                        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left2, "C", false) != 0)
                        {
                          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left2, "S", false) == 0)
                          {
                            this.m_strBldgID = e.TreeNode.Parent.Parent.Parent.Parent.Key;
                            this.m_strLocation = e.TreeNode.Parent.Parent.Key;
                            this.m_strCompID = e.TreeNode.Parent.Key;
                            this.m_strSecID = e.TreeNode.Key;
                            mdRightFormHandler.SetCurrentItem(e.TreeNode.Key.ToString(), "Inspection", e.TreeNode.Tag.ToString());
                          }
                        }
                        else
                        {
                          this.m_strBldgID = e.TreeNode.Parent.Parent.Parent.Key;
                          this.m_strLocation = e.TreeNode.Parent.Key;
                          this.m_strCompID = e.TreeNode.Key;
                          mdRightFormHandler.SetCurrentItem(e.TreeNode.Key.ToString(), "Component", e.TreeNode.Tag.ToString());
                        }
                      }
                      else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.TreeNode.Parent.Tag.ToString(), "Sample Locations", false) == 0)
                      {
                        this.m_strBldgID = e.TreeNode.Parent.Parent.Key;
                        this.m_strLocation = e.TreeNode.Key;
                        mdRightFormHandler.SetCurrentItem(e.TreeNode.Key.ToString(), "Location", e.TreeNode.Tag.ToString());
                      }
                      else
                      {
                        this.m_strBldgID = e.TreeNode.Parent.Parent.Parent.Parent.Parent.Key;
                        this.m_strLocation = e.TreeNode.Key;
                        this.m_strSysID = e.TreeNode.Parent.Parent.Parent.Key;
                        this.m_strCompID = e.TreeNode.Parent.Parent.Key;
                        this.m_strSecID = e.TreeNode.Parent.Key;
                        mdRightFormHandler.SetCurrentItem(e.TreeNode.Key.ToString(), "Inspection", e.TreeNode.Tag.ToString());
                      }
                    }
                  }
                  else
                  {
                    this.m_strBldgID = e.TreeNode.Parent.Parent.Parent.Key;
                    this.m_strSysID = e.TreeNode.Parent.Key;
                    this.m_strCompID = e.TreeNode.Key;
                    mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "Component", e.TreeNode.Key);
                    this.SetInventoryClassForImages(InventoryClass.SystemComponent, e.TreeNode.Key);
                  }
                }
                else
                {
                  this.m_strBldgID = e.TreeNode.Parent.Parent.Key;
                  this.m_strSysID = e.TreeNode.Key;
                  mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "System", e.TreeNode.Tag.ToString());
                  this.SetInventoryClassForImages(InventoryClass.BuildingSystem, e.TreeNode.Key);
                }
              }
              else
              {
                this.m_strBldgID = e.TreeNode.Parent.Key;
                mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "Location", e.TreeNode.Tag.ToString());
                this.SetInventoryClassForImages(InventoryClass.Facility, "");
              }
            }
            else
            {
              this.m_strBldgID = e.TreeNode.Parent.Key;
              mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "Location", e.TreeNode.Tag.ToString());
              this.SetInventoryClassForImages(InventoryClass.InspectionData, e.TreeNode.Key);
            }
          }
          else
          {
            this.m_strBldgID = e.TreeNode.Key;
            mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "Building", e.TreeNode.Key);
            this.pnlBuildingInsp.Controls.Clear();
            this.SetInventoryClassForImages(InventoryClass.Facility, e.TreeNode.Key);
          }
        }
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        Exception ex = ex1;
        if (Information.Err().Number != 91)
          mdUtility.Errorhandler(ex, this.Name, nameof (tvInspection_AfterActivate));
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private async void tvInspection_AfterCollapse(object sender, NodeEventArgs e)
    {
      try
      {
        if (this.m_bInspLoaded)
        {
          bool MustSave = false;
          if (!this.CheckInspForSave(ref MustSave))
            return;
        }
        e.TreeNode.Selected = true;
        this.Cursor = Cursors.WaitCursor;
        this.ResetCurrentKeys();
        this.tvInspection.ActiveNode = e.TreeNode;
        string Left1 = e.TreeNode.Tag.ToString();
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "Building", false) != 0)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "Non-sampling", false) != 0)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "Sample Locations", false) != 0)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "System", false) != 0)
              {
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "Component", false) != 0)
                {
                  if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "Section", false) == 0)
                  {
                    this.m_strBldgID = e.TreeNode.Parent.Parent.Parent.Parent.Key;
                    this.m_strSysID = e.TreeNode.Parent.Parent.Key;
                    this.m_strCompID = e.TreeNode.Parent.Key;
                    this.m_strSecID = e.TreeNode.Key;
                    mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "Inspection", e.TreeNode.Key);
                  }
                  else
                  {
                    string Left2 = Microsoft.VisualBasic.Strings.Left(e.TreeNode.Key, 1);
                    if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left2, "L", false) != 0)
                    {
                      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left2, "C", false) == 0)
                      {
                        this.m_strBldgID = e.TreeNode.Parent.Parent.Parent.Key;
                        this.m_strLocation = e.TreeNode.Parent.Key;
                        this.m_strCompID = e.TreeNode.Tag.ToString();
                        mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "Component", e.TreeNode.Tag.ToString());
                      }
                    }
                    else
                    {
                      this.m_strBldgID = e.TreeNode.Parent.Parent.Key;
                      this.m_strLocation = e.TreeNode.Key;
                      mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "Location", e.TreeNode.Tag.ToString());
                    }
                  }
                }
                else
                {
                  this.m_strBldgID = e.TreeNode.Parent.Parent.Parent.Key;
                  this.m_strSysID = e.TreeNode.Parent.Key;
                  this.m_strCompID = e.TreeNode.Key;
                  mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "Component", e.TreeNode.Key);
                }
              }
              else
              {
                this.m_strBldgID = e.TreeNode.Parent.Parent.Key;
                this.m_strSysID = e.TreeNode.Key;
              }
            }
            else
            {
              this.m_strBldgID = e.TreeNode.Parent.Key;
              mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "Location", e.TreeNode.Tag.ToString());
            }
          }
          else
          {
            this.m_strBldgID = e.TreeNode.Parent.Key;
            mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "Location", e.TreeNode.Tag.ToString());
          }
        }
        else
        {
          this.m_strBldgID = e.TreeNode.Key;
          mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "Building", e.TreeNode.Tag.ToString());
        }
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        Exception ex = ex1;
        mdUtility.Errorhandler(ex, this.Name, nameof (tvInspection_AfterCollapse));
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private async void tvInspection_AfterExpand(object sender, NodeEventArgs e)
    {
      try
      {
        if (this.m_bInspLoaded)
        {
          bool MustSave = false;
          if (!this.CheckInspForSave(ref MustSave))
            return;
        }
        e.TreeNode.Selected = true;
        this.Cursor = Cursors.WaitCursor;
        this.ResetCurrentKeys();
        this.tvInspection.ActiveNode = e.TreeNode;
        string Left1 = e.TreeNode.Tag.ToString();
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "Building", false) != 0)
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "Non-sampling", false) != 0)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "Sample Locations", false) != 0)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "System", false) != 0)
              {
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "Component", false) != 0)
                {
                  if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left1, "Section", false) == 0)
                  {
                    this.m_strBldgID = e.TreeNode.Parent.Parent.Parent.Parent.Key;
                    this.m_strSysID = e.TreeNode.Parent.Parent.Key;
                    this.m_strCompID = e.TreeNode.Parent.Key;
                    this.m_strSecID = e.TreeNode.Key;
                    if (e.TreeNode.Nodes.Count > 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.TreeNode.Nodes[0].Key, e.TreeNode.Key + "1", false) == 0)
                      mdHierarchyFunction.LoadInspectionSamples(e.TreeNode.Key, e.TreeNode.Key);
                    mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "Inspection", e.TreeNode.Key);
                  }
                  else
                  {
                    string Left2 = Microsoft.VisualBasic.Strings.Left(e.TreeNode.Key, 1);
                    if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left2, "L", false) != 0)
                    {
                      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left2, "C", false) == 0)
                      {
                        this.m_strBldgID = e.TreeNode.Parent.Parent.Parent.Key;
                        this.m_strLocation = e.TreeNode.Parent.Key;
                        this.m_strCompID = e.TreeNode.Key;
                        if (e.TreeNode.Nodes.Count > 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.TreeNode.Nodes[0].Key, e.TreeNode.Key + "1", false) == 0)
                          mdHierarchyFunction.LoadInspectionSections(e.TreeNode.Tag.ToString(), e.TreeNode.Key.ToString());
                        mdRightFormHandler.SetCurrentItem(e.TreeNode.Key.ToString(), "Component", e.TreeNode.Tag.ToString());
                      }
                    }
                    else
                    {
                      this.m_strBldgID = e.TreeNode.Parent.Parent.Key;
                      this.m_strLocation = e.TreeNode.Key;
                      if (e.TreeNode.Nodes.Count > 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.TreeNode.Nodes[0].Key, e.TreeNode.Key + "1", false) == 0)
                        mdHierarchyFunction.LoadInspectionComponents(e.TreeNode.Tag.ToString(), e.TreeNode.Key.ToString());
                      mdRightFormHandler.SetCurrentItem(e.TreeNode.Key.ToString(), "Location", e.TreeNode.Tag.ToString());
                    }
                  }
                }
                else
                {
                  this.m_strBldgID = e.TreeNode.Parent.Parent.Parent.Key;
                  this.m_strSysID = e.TreeNode.Parent.Key;
                  this.m_strCompID = e.TreeNode.Key;
                  if (e.TreeNode.Nodes.Count > 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.TreeNode.Nodes[0].Key, e.TreeNode.Key + "1", false) == 0)
                    mdHierarchyFunction.LoadInspectionSections(e.TreeNode.Key, e.TreeNode.Key);
                  mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "Component", e.TreeNode.Key);
                }
              }
              else
              {
                this.m_strBldgID = e.TreeNode.Parent.Parent.Key;
                this.m_strSysID = e.TreeNode.Key;
                if (e.TreeNode.Nodes.Count > 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.TreeNode.Nodes[0].Key, e.TreeNode.Key + "1", false) == 0)
                  mdHierarchyFunction.LoadInspectionComponents(e.TreeNode.Tag.ToString(), e.TreeNode.Key);
                mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "System", e.TreeNode.Tag.ToString());
              }
            }
            else
            {
              this.m_strBldgID = e.TreeNode.Parent.Key;
              if (e.TreeNode.Nodes.Count > 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.TreeNode.Nodes[0].Key, e.TreeNode.Key + "1", false) == 0)
                mdHierarchyFunction.LoadInspectionSample(e.TreeNode.Key);
              mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "Location", e.TreeNode.Tag.ToString());
            }
          }
          else
          {
            this.m_strBldgID = e.TreeNode.Parent.Key;
            if (e.TreeNode.Nodes.Count > 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.TreeNode.Nodes[0].Key, e.TreeNode.Key + "1", false) == 0)
              mdHierarchyFunction.LoadInspectionSystems(e.TreeNode.Key, e.TreeNode.Tag.ToString());
            mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "Location", e.TreeNode.Tag.ToString());
          }
        }
        else
        {
          this.m_strBldgID = e.TreeNode.Key;
          if (e.TreeNode.Nodes.Count > 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.TreeNode.Nodes[0].Key, e.TreeNode.Key + "1", false) == 0)
            mdHierarchyFunction.LoadInspectionLocations(e.TreeNode.Key);
          mdRightFormHandler.SetCurrentItem(e.TreeNode.Key, "Building", e.TreeNode.Key);
        }
        this.tvInspection.ActiveNode = e.TreeNode;
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        Exception ex = ex1;
        mdUtility.Errorhandler(ex, this.Name, nameof (tvInspection_AfterExpand));
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private void cmdNewSample_Click_1(object sender, EventArgs e)
    {
      try
      {
        bool MustSave = true;
        if (!this.CheckInspForSave(ref MustSave))
          return;
        string str1 = Sample.NewSample(this.CurrentInspection);
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str1, "", false) > 0U)
        {
          if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.CurrentSystem, "", false) > 0U)
          {
            UltraTreeNode ultraTreeNode = this.tvInspection.GetNodeByKey(this.CurrentSection);
            string location = this.FindLocation(ref ultraTreeNode, str1);
            this.tvInspection.ActiveNode = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(location, "", false) <= 0U ? this.tvInspection.GetNodeByKey(this.CurrentSection) : this.tvInspection.GetNodeByKey(location);
            UltraTree tvInspection;
            ultraTreeNode = (tvInspection = this.tvInspection).ActiveNode;
            this.SelectNewActiveInspectionNode(ref ultraTreeNode);
            tvInspection.ActiveNode = ultraTreeNode;
          }
          else
          {
            TreeNodesCollection nodes;
            UltraTreeNode nodeByKey = (nodes = this.tvInspection.GetNodeByKey(this.CurrentBldg).Nodes)[1];
            string location = this.FindLocation(ref nodeByKey, str1);
            nodes[1] = nodeByKey;
            string str2 = location;
            if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str2, "", false) > 0U)
            {
              nodeByKey = this.tvInspection.GetNodeByKey(str2);
              ref UltraTreeNode local1 = ref nodeByKey;
              string str3 = this.tvInspection.GetNodeByKey(this.CurrentComp).Tag.ToString();
              ref string local2 = ref str3;
              string component = this.FindComponent(ref local1, ref local2);
              if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(component, "", false) > 0U)
              {
                nodeByKey = this.tvInspection.GetNodeByKey(component);
                string str4 = Conversions.ToString(this.FindSection(ref nodeByKey, this.tvInspection.GetNodeByKey(this.CurrentSection).Tag.ToString()));
                if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str4, "", false) > 0U)
                {
                  nodeByKey = this.tvInspection.GetNodeByKey(str4);
                  this.SelectNewActiveInspectionNode(ref nodeByKey);
                }
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, "cmdNewSample_Click");
        ProjectData.ClearProjectError();
      }
    }

    private void chkDefFree_CheckStateChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.m_bInspLoaded)
        {
          if (!this.CanEditInspection)
          {
            this.m_bInspLoaded = false;
            this.chkDefFree.Checked = !this.chkDefFree.Checked;
            this.m_bInspLoaded = true;
          }
          else if (this.chkDefFree.Checked)
          {
            if (mdUtility.get_MstrTable("Distresses").Rows.Count > 0)
            {
              int num = (int) Interaction.MsgBox((object) "Unable to declare defect free because of existing distresses.", MsgBoxStyle.OkOnly, (object) null);
              this.chkDefFree.CheckState = CheckState.Unchecked;
            }
            else if (mdUtility.get_MstrTable("SelectedCrit") != null & mdUtility.get_MstrTable("SelectedCrit").Rows.Count > 0)
            {
              int num = (int) Interaction.MsgBox((object) "Unable to declare defect free because of existing distresses.", MsgBoxStyle.OkOnly, (object) null);
              this.chkDefFree.CheckState = CheckState.Unchecked;
            }
            else
            {
              DataTable dataTable = mdUtility.get_MstrTable("Subcomps");
              if (dataTable.Rows.Count > 0)
              {
                UltraGridRow activeRow = this.ugSubcomponents.ActiveRow;
                int num = checked (dataTable.Rows.Count - 1);
                int index = 0;
                while (index <= num)
                {
                  dataTable.Rows[index]["BRED_Status"] = Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[index]["ID"])) ? (object) "A" : (object) "C";
                  dataTable.Rows[index]["samp_subcomp_notappl"] = (object) false;
                  dataTable.Rows[index]["samp_subcomp_insp"] = (object) true;
                  checked { ++index; }
                }
                this.ugSubcomponents.ActiveRow = activeRow;
              }
              this.SetInspChanged(true);
            }
          }
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (chkDefFree_CheckStateChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void chkPaintDefFree_CheckStateChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.m_bInspLoaded)
        {
          if (!this.CanEditInspection)
          {
            this.m_bInspLoaded = false;
            this.chkPaintDefFree.Checked = !this.chkPaintDefFree.Checked;
            this.m_bInspLoaded = true;
          }
          else if (this.chkPaintDefFree.Checked)
          {
            DataTable dataTable = mdUtility.get_MstrTable("SubComps");
            if (dataTable.Rows.Count > 0)
            {
              UltraGridRow activeRow = this.ugSubcomponents.ActiveRow;
              int num1 = checked (dataTable.Rows.Count - 1);
              int index1 = 0;
              while (index1 <= num1)
              {
                if (Conversions.ToBoolean(Microsoft.VisualBasic.CompilerServices.Operators.AndObject(Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectNotEqual(dataTable.Rows[index1]["samp_subcomp_paintrate"], (object) 100, false), Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectNotEqual(dataTable.Rows[index1]["samp_subcomp_paintrate"], (object) -1, false))))
                {
                  int num2 = (int) Interaction.MsgBox((object) "Unable to declare defect free because of existing ratings.", MsgBoxStyle.OkOnly, (object) null);
                  this.chkPaintDefFree.CheckState = CheckState.Unchecked;
                  return;
                }
                checked { ++index1; }
              }
              int num3 = checked (dataTable.Rows.Count - 1);
              int index2 = 0;
              while (index2 <= num3)
              {
                if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(dataTable.Rows[index2]["SAMP_SUBCOMP_NOTAPPL"], (object) false, false))
                {
                  dataTable.Rows[index2]["samp_subcomp_paintdf"] = (object) true;
                  dataTable.Rows[index2]["BRED_Status"] = Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[index2]["ID"])) ? (object) "A" : (object) "C";
                  dataTable.Rows[index2]["samp_subcomp_paintna"] = (object) false;
                  dataTable.Rows[index2]["samp_subcomp_paintrate"] = (object) "100";
                  dataTable.Rows[index2].EndEdit();
                }
                checked { ++index2; }
              }
              this.ugSubcomponents.ActiveRow = activeRow;
            }
            this.SetInspChanged(true);
          }
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (chkPaintDefFree_CheckStateChanged));
        this.chkPaintDefFree.CheckState = CheckState.Unchecked;
        ProjectData.ClearProjectError();
      }
    }

    private void chkPainted_CheckStateChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.chkPainted.Checked)
        {
          this.lblDatePainted.Visible = true;
          this.dtPainted.Visible = true;
          this.lblPaintType.Visible = true;
          this.cboSectionPaintType.Visible = true;
        }
        else
        {
          this.lblDatePainted.Visible = false;
          this.dtPainted.Visible = false;
          this.lblPaintType.Visible = false;
          this.cboSectionPaintType.Visible = false;
        }
        if (this.m_bSectionLoaded)
        {
          if (this.txtSectionAmount.ReadOnly)
          {
            this.m_bSectionLoaded = false;
            this.chkPainted.Checked = !this.chkPainted.Checked;
            this.m_bSectionLoaded = true;
            return;
          }
          if (this.chkPainted.CheckState == CheckState.Checked)
          {
            string sSQL;
            if (BuildingSystem.SystemLink(Component.BuildingSystem(this.CurrentComp)) == 6 | BuildingSystem.SystemLink(Component.BuildingSystem(this.CurrentComp)) == 211)
            {
              string InstallationID = Building.Installation(BuildingSystem.Building(Component.BuildingSystem(this.CurrentComp)));
              int num = Installation.HVACZone(ref InstallationID);
              sSQL = !mdUtility.UseUniformat ? Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SELECT PAINT_LIFE FROM qryRSLbyCMC WHERE [CMC_CType_Link]=", this.cboSectionComponentType.SelectedValue), (object) " AND [CMC_MCat_Link]="), this.cboSectionMaterial.SelectedValue), (object) " AND [CMC_COMP_Link]="), (object) Component.ComponentLink(this.CurrentComp)), (object) " AND ((qryRSLbyCMC.HVAC_Zone)="), (object) num), (object) ");")) : Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SELECT PAINT_LIFE FROM qryRSLbyCMC WHERE [CMC_CType_Link]=", this.cboSectionComponentType.SelectedValue), (object) " AND [CMC_MCat_Link]="), this.cboSectionMaterial.SelectedValue), (object) " AND [CMC_COMP_UII_LINK]="), (object) Component.ComponentLink(this.CurrentComp)), (object) " AND ((qryRSLbyCMC.HVAC_Zone)="), (object) num), (object) ");"));
            }
            else
              sSQL = !mdUtility.UseUniformat ? Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SELECT PAINT_LIFE FROM qryRSLbyCMC WHERE [CMC_CType_Link]=", this.cboSectionComponentType.SelectedValue), (object) " AND [CMC_MCat_Link]="), this.cboSectionMaterial.SelectedValue), (object) " AND [CMC_COMP_Link]="), (object) Component.ComponentLink(this.CurrentComp))) : Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SELECT PAINT_LIFE FROM qryRSLbyCMC WHERE [CMC_CType_Link]=", this.cboSectionComponentType.SelectedValue), (object) " AND [CMC_MCat_Link]="), this.cboSectionMaterial.SelectedValue), (object) " AND [CMC_COMP_UII_LINK]="), (object) Component.ComponentLink(this.CurrentComp)));
            DataTable dataTable = mdUtility.DB.GetDataTable(sSQL);
            if (dataTable.Rows.Count > 0)
            {
              if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["PAINT_LIFE"])))
              {
                int integer = Conversions.ToInteger(dataTable.Rows[0]["PAINT_LIFE"]);
                int num = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtSectionYearBuilt.Text, "", false) <= 0U ? checked (DateAndTime.Year(DateAndTime.Now) - Building.BuiltYear(this.CurrentBldg)) : checked (DateAndTime.Year(DateAndTime.Now) - Conversions.ToInteger(this.txtSectionYearBuilt.Text));
                this.dtPainted.Text = (double) num / (double) integer <= 1.5 ? ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtSectionYearBuilt.Text, "", false) <= 0U ? Conversions.ToString(Building.BuiltYear(this.CurrentBldg)) : this.txtSectionYearBuilt.Text) : Conversions.ToString(checked (DateAndTime.Year(DateAndTime.Now) - unchecked (num % integer)));
              }
              else
                this.dtPainted.Text = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtSectionYearBuilt.Text, "", false) <= 0U ? Conversions.ToString(Building.BuiltYear(this.CurrentBldg)) : this.txtSectionYearBuilt.Text;
            }
            else
              this.dtPainted.Text = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtSectionYearBuilt.Text, "", false) <= 0U ? Conversions.ToString(Building.BuiltYear(this.CurrentBldg)) : this.txtSectionYearBuilt.Text;
          }
        }
        this.SetSectionChanged(true);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (chkPainted_CheckStateChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void chkSampNonRep_CheckStateChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.m_bInspLoaded)
        {
          if (!this.CanEditInspection)
          {
            this.m_bInspLoaded = false;
            this.chkSampNonRep.Checked = !this.chkSampNonRep.Checked;
            this.m_bInspLoaded = true;
          }
          else
            this.SetInspChanged(true);
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (chkSampNonRep_CheckStateChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void chkSampPainted_CheckStateChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.optRatingType_1.Checked | this.optRatingType_3.Checked)
        {
          if (this.chkSampPainted.Checked)
            this.chkPaintDefFree.Visible = true;
          else
            this.chkPaintDefFree.Visible = false;
        }
        if (this.m_bInspLoaded)
        {
          if (!this.CanEditInspection)
          {
            this.m_bInspLoaded = false;
            this.chkSampPainted.Checked = !this.chkSampPainted.Checked;
            this.m_bInspLoaded = true;
          }
          else
          {
            if (this.optRatingType_1.Checked | this.optRatingType_3.Checked)
            {
              UltraGrid ugSubcomponents = this.ugSubcomponents;
              ugSubcomponents.DisplayLayout.Bands[0].Override.AllowUpdate = (DefaultableBoolean) -(!this.CanEditInspection ? 1 : 0);
              if (this.chkSampPainted.CheckState == CheckState.Checked)
              {
                ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTNA"].Hidden = false;
                ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTDF"].Hidden = false;
                ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTRATE"].Hidden = false;
              }
              else
              {
                ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTNA"].Hidden = true;
                ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTDF"].Hidden = true;
                ugSubcomponents.DisplayLayout.Bands[0].Columns["SAMP_SUBCOMP_PAINTRATE"].Hidden = true;
              }
            }
            this.SetInspChanged(true);
          }
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, "chkSampPainted_CheckStateChange");
        ProjectData.ClearProjectError();
      }
    }

    private void cboFunctionalArea_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        if (!this.m_bSectionLoaded)
          return;
        this.SetSectionChanged(true);
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, this.Name, nameof (cboFunctionalArea_SelectedIndexChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void chkYearEstimated_CheckStateChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.chkYearEstimated.CheckState == CheckState.Checked)
          this.txtSectionYearBuilt.BackColor = Color.Yellow;
        else
          this.txtSectionYearBuilt.BackColor = SystemColors.Window;
        if (this.m_bSectionLoaded)
        {
          if (this.txtSectionAmount.ReadOnly)
          {
            this.m_bSectionLoaded = false;
            this.chkYearEstimated.Checked = !this.chkYearEstimated.Checked;
            this.m_bSectionLoaded = true;
          }
          else
            this.SetSectionChanged(true);
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, "chkYearEstimated");
        ProjectData.ClearProjectError();
      }
    }

    private void cmdCopyInspection_Click(object sender, EventArgs e)
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;
        Inspection.CopyInspection(this.CurrentInspection);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (cmdCopyInspection_Click));
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private void cmdDeleteInspection_Click(object sender, EventArgs e)
    {
      try
      {
        if ((!this.m_bInspNeedToSave ? Interaction.MsgBox((object) "WARNING: You are about to delete this inspection, including all samples.  Are you sure you want to do this?\r\nIf you want to delete this sample only use the button next to the location list.", MsgBoxStyle.OkCancel | MsgBoxStyle.Critical, (object) "Confirm delete") : Interaction.MsgBox((object) "Data has changed.  Cancel changes and delete inspection?\r\nWARNING: You are about to delete this inspection, including all samples.  Are you sure you want to do this?\r\nIf you want to delete this sample only use the button next to the location list.", MsgBoxStyle.OkCancel | MsgBoxStyle.Critical, (object) "Confirm delete")) != MsgBoxResult.Ok)
          return;
        Inspection.DeleteInspection(this.CurrentInspection);
        this.m_bInspLoaded = false;
        string currentSectionId = this.CurrentSectionID;
        this.RefreshSections(ref currentSectionId);
        Inspection.LoadInspectionDates(this.CurrentSectionID);
        this.m_bInspLoaded = true;
        if (this.tvInspection.GetNodeByKey(this.CurrentSectionID) != null)
        {
          UltraTreeNode nodeByKey = this.tvInspection.GetNodeByKey(this.CurrentSectionID);
          this.SelectNewActiveInspectionNode(ref nodeByKey);
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (cmdDeleteInspection_Click));
        ProjectData.ClearProjectError();
      }
    }

    private void cmdDeleteSample_Click(object sender, EventArgs e)
    {
      try
      {
        UltraTreeNode ultraTreeNode;
        if (mdUtility.get_MstrTable("Samples").Rows.Count > 1)
        {
          if ((!this.m_bInspNeedToSave ? Interaction.MsgBox((object) "Delete this sample?", MsgBoxStyle.OkCancel | MsgBoxStyle.Critical, (object) null) : Interaction.MsgBox((object) "Data has changed. Cancel changes and delete sample?", MsgBoxStyle.OkCancel | MsgBoxStyle.Critical, (object) null)) == MsgBoxResult.Ok)
          {
            DataTable dataTable1 = mdUtility.DB.GetDataTable("SELECT samp_data_loc, SAMP_DATA_INSP_DATA_ID FROM Sample_Data WHERE [samp_data_id]={" + Microsoft.VisualBasic.Strings.Replace(this.CurrentSample, "'", "''", 1, -1, CompareMethod.Binary) + "}");
            string Right = dataTable1.Rows[0]["samp_data_loc"].ToString();
            string str1 = dataTable1.Rows[0]["SAMP_DATA_INSP_DATA_ID"].ToString();
            Sample.DeleteSample(this.CurrentSample);
            this.SetInspChanged(false);
            bool flag = false;
            if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(mdUtility.DB.GetDataTable("SELECT Count(SEC_ID) FROM Samples_by_sections WHERE [SEC_ID]={" + Microsoft.VisualBasic.Strings.Replace(this.CurrentSection, "'", "''", 1, -1, CompareMethod.Binary) + "} AND SAMP_DATA_LOC={" + Right + "}").Rows[0][0], (object) 0, false))
              flag = true;
            UltraTreeNode nodeByKey1 = this.tvInspection.GetNodeByKey(this.CurrentSection);
            foreach (UltraTreeNode node in nodeByKey1.Nodes)
            {
              if (node.Tag != null && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(node.Tag.ToString(), Right, false) == 0)
              {
                nodeByKey1.Nodes.Remove(node);
                break;
              }
            }
            if (flag)
            {
              foreach (UltraTreeNode node in this.tvInspection.Nodes)
              {
                if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(node.Tag, (object) Right, false))
                {
                  if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(node.Parent.Key, this.CurrentSection, false) == 0)
                    node.Parent.Nodes.Remove(node);
                }
                else if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(node.Tag, (object) this.CurrentSection, false) && Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(node.Parent.Parent.Tag, (object) Right, false))
                {
                  UltraTreeNode parent = node.Parent;
                  parent.Nodes.Remove(node);
                  if (parent.Nodes.Count == 0)
                    parent.Parent.Nodes.Remove(parent);
                  ultraTreeNode = (UltraTreeNode) null;
                }
              }
            }
            string str2;
            if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.CurrentSystem, "", false) > 0U)
            {
              str2 = this.CurrentSection;
            }
            else
            {
              DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT Samp_Data_Loc, SEC_ID FROM qryInspectionDetails WHERE [INSP_DATA_ID]={" + str1 + "} and not (SAMP_DATA_LOC is null)");
              if (dataTable2.Rows.Count > 0)
              {
                string str3 = Conversions.ToString(dataTable2.Rows[0]["Samp_Data_Loc"]);
                string SectionID = Conversions.ToString(dataTable2.Rows[0]["SEC_ID"]);
                if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str3, "", false) > 0U)
                {
                  UltraTreeNode nodeByKey2 = this.tvInspection.GetNodeByKey(this.CurrentBldg);
                  if (nodeByKey2 != null)
                  {
                    if (Conversions.ToBoolean((object) (bool) (nodeByKey2.Nodes.Count == 0 ? 1 : (Conversions.ToBoolean(Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectEqual(nodeByKey2.Nodes[0].Tag, (object) (nodeByKey2.Key + "1"), false)) ? 1 : 0))))
                      mdHierarchyFunction.LoadInspectionLocations(this.CurrentBldg);
                    TreeNodesCollection nodes;
                    UltraTreeNode ParentNode = (nodes = nodeByKey2.Nodes)[1];
                    string location = this.FindLocation(ref ParentNode, str3);
                    nodes[1] = ParentNode;
                    string str4 = location;
                    if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str4, "", false) > 0U)
                    {
                      UltraTreeNode nodeByKey3 = this.tvInspection.GetNodeByKey(str4);
                      ref UltraTreeNode local1 = ref nodeByKey3;
                      string str5 = Section.SectionComponentLink(SectionID);
                      ref string local2 = ref str5;
                      string component = this.FindComponent(ref local1, ref local2);
                      if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(component, "", false) > 0U)
                      {
                        UltraTreeNode nodeByKey4 = this.tvInspection.GetNodeByKey(component);
                        string Left = Conversions.ToString(this.FindSection(ref nodeByKey4, SectionID));
                        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "", false) > 0U)
                          str2 = Left;
                      }
                    }
                  }
                }
              }
            }
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str2, "", false) == 0)
              str2 = this.CurrentBldg;
            UltraTreeNode nodeByKey5 = this.tvInspection.GetNodeByKey(str2);
            this.SelectNewActiveInspectionNode(ref nodeByKey5);
          }
        }
        else
        {
          int num = (int) Interaction.MsgBox((object) "Unable to delete remaining sample data. Every inspection must have at least one record of this type.", MsgBoxStyle.OkOnly, (object) null);
        }
        ultraTreeNode = (UltraTreeNode) null;
      }
      catch (Exception ex1)
      {
        ProjectData.SetProjectError(ex1);
        Exception ex2 = ex1;
        if (Information.Err().Number != 35601)
          mdUtility.Errorhandler(ex2, this.Name, nameof (cmdDeleteSample_Click));
        ProjectData.ClearProjectError();
      }
    }

    private void cmdEditSample_Click(object sender, EventArgs e)
    {
      try
      {
        frmNewSample frmNewSample1 = new frmNewSample();
        this.m_bInspLoaded = false;
        frmNewSample frmNewSample2 = frmNewSample1;
        string currentSample = this.CurrentSample;
        ref string local = ref currentSample;
        frmNewSample2.EditSample(ref local);
        Sample.LoadSampleList(this.CurrentInspection);
        this.cboLocation.SelectedValue = (object) this.CurrentSample;
        this.cboLocation_SelectedIndexChanged((object) null, (EventArgs) null);
        this.m_bInspLoaded = true;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (cmdEditSample_Click));
        ProjectData.ClearProjectError();
      }
    }

    private void cmdNewInspection_Click(object sender, EventArgs e)
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;
        Inspection.NewInspection(Guid.Parse(this.CurrentSectionID));
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmMain), nameof (cmdNewInspection_Click));
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private void cmdSampComment_Click(object sender, EventArgs e)
    {
      try
      {
        string str1 = "SELECT * FROM sample_data WHERE [samp_data_id]={" + Microsoft.VisualBasic.Strings.Replace(this.CurrentSample, "'", "''", 1, -1, CompareMethod.Binary) + "}";
        DataTable dataTable = mdUtility.DB.GetDataTable(str1);
        if (dataTable.Rows.Count <= 0)
          return;
        string str2 = !Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["samp_data_loc"])) ? Sample.GetSampleLocationName(dataTable.Rows[0]["samp_data_loc"].ToString()) : "Initial Sample";
        frmComment frmComment = new frmComment();
        string strCaption = !this.optMethod_1.Checked ? "Component Rating Comment" : "Component Rating Comment - " + str2;
        if (frmComment.EditComment(strCaption, RuntimeHelpers.GetObjectValue(UtilityFunctions.FixDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["samp_data_comments"]), (object) "")), this.CanEditInspection, (IWin32Window) null) == DialogResult.Yes && this.CanEditInspection)
        {
          if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["BRED_Status"])) || Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(dataTable.Rows[0]["BRED_Status"], (object) "N", false))
            dataTable.Rows[0]["BRED_Status"] = (object) "U";
          if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Microsoft.VisualBasic.Strings.Trim(frmComment.Comment), "", false) > 0U)
          {
            dataTable.Rows[0]["samp_data_comments"] = (object) frmComment.Comment;
            this.cmdSampComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard_Check;
          }
          else
          {
            dataTable.Rows[0]["samp_data_comments"] = (object) DBNull.Value;
            this.cmdSampComment.Image = (Image) BuilderRED.My.Resources.Resources.Clipboard;
          }
          mdUtility.DB.SaveDataTable(ref dataTable, str1);
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (cmdSampComment_Click));
        ProjectData.ClearProjectError();
      }
    }

    private void Building_TextChanged(object sender, EventArgs e)
    {
      try
      {
        if (!this.m_bBldgLoaded)
          return;
        this.SetBldgChanged(true);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (Building_TextChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void txtSectionAmount_KeyDown(object sender, KeyEventArgs e)
    {
      this.m_bNonNumberEntered = false;
      if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 || e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9 || (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete))
        return;
      if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.lblUOM.Text, "EA", false) > 0U)
      {
        if (e.KeyCode != Keys.Decimal && e.KeyCode != Keys.OemPeriod || Microsoft.VisualBasic.Strings.InStr(this.txtSectionAmount.Text, ".", CompareMethod.Binary) > 1)
          this.m_bNonNumberEntered = true;
      }
      else
        this.m_bNonNumberEntered = true;
    }

    private void txtSectionAmount_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!this.m_bNonNumberEntered)
        return;
      e.Handled = true;
    }

    private void Section_TextChanged(object sender, EventArgs e)
    {
      try
      {
        if (!this.m_bSectionLoaded)
          return;
        this.SetSectionChanged(true);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (Section_TextChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void txtYearRenovated_TextChanged(object sender, EventArgs e)
    {
      if (!this.m_bBldgLoaded)
        return;
      this.SetBldgChanged(true);
    }

    private void txtSectionYearBuilt_TextChanged(object sender, EventArgs e)
    {
      if (!this.m_bSectionLoaded)
        return;
      this.m_bSectionYearChanged = true;
      this.SetSectionChanged(true);
    }

    private void txtSectionYearBuilt_Leave(object sender, EventArgs e)
    {
      if (this.m_bSectionYearChanged)
        this.chkYearEstimated.CheckState = Interaction.MsgBox((object) "Are you sure of this date?", MsgBoxStyle.YesNo | MsgBoxStyle.Question, (object) "Estimated Date?") != MsgBoxResult.Yes ? CheckState.Checked : CheckState.Unchecked;
      this.m_bSectionYearChanged = false;
    }

    private void txtSQuantity_Leave(object sender, EventArgs e)
    {
      try
      {
        if (!this.CanEditInspection || !Versioned.IsNumeric((object) this.txtSQuantity.Text))
          return;
        this.lblPCInspValue.Text = Microsoft.VisualBasic.Strings.Format((object) mdUtility.GetPercent(this.CurrentInspection, this.CurrentSample, Conversions.ToDouble(this.txtSQuantity.Text)), "0.00%");
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (txtSQuantity_Leave));
        ProjectData.ClearProjectError();
      }
    }

    private void txtSQuantity_TextChanged(object sender, EventArgs e)
    {
      if (!this.m_bInspLoaded)
        return;
      this.SetInspChanged(true);
    }

    private void txtSQuantity_Validating(object sender, CancelEventArgs e)
    {
      try
      {
        if (!this.m_bInspLoaded || !this.CanEditInspection)
          return;
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtSQuantity.Text, "", false) == 0 & (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.cboInspectionDates.SelectedText, "", false) > 0U)
        {
          int num = (int) Interaction.MsgBox((object) "Sample Qty: You must enter a value for this field.", MsgBoxStyle.OkOnly, (object) null);
          e.Cancel = true;
        }
        if (Versioned.IsNumeric((object) this.txtSQuantity.Text))
        {
          if (Conversions.ToDouble(this.txtSQuantity.Text) < 0.0)
          {
            int num = (int) Interaction.MsgBox((object) "Inspection quantities are not allowed to be negative.  Please correct quantity before proceeding.", MsgBoxStyle.OkOnly, (object) null);
            e.Cancel = true;
          }
        }
        else
        {
          int num = (int) Interaction.MsgBox((object) "You cannot enter a non-numeric value in this field.  Please re-enter a sample quantity.", MsgBoxStyle.OkOnly, (object) null);
          e.Cancel = true;
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (txtSQuantity_Validating));
        e.Cancel = true;
        ProjectData.ClearProjectError();
      }
    }

    private void ugSubcomponents_InitializeRow(object sender, InitializeRowEventArgs e)
    {
      if (this.bInitializeRow)
        return;
      if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(e.Row.Cells["SAMP_SUBCOMP_NOTAPPL"].Value, (object) true, false))
      {
        e.Row.Cells[6].Activation = Activation.Disabled;
        e.Row.Cells[13].Activation = Activation.Disabled;
      }
      else
      {
        e.Row.Cells[6].Activation = Activation.AllowEdit;
        e.Row.Cells[13].Activation = Activation.AllowEdit;
      }
      if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(e.Row.Cells["SAMP_SUBCOMP_CONDRATE"].Value, (object) -5, false))
        e.Row.Cells["Criteria"].Column.CellActivation = Activation.Disabled;
      UltraGridCell cell = e.Row.Cells["SAMP_SUBCOMP_PAINTRATE"];
      this.SetRatingColor(ref cell);
    }

    private void ugSubcomponents_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
    {
      this.ugSubcomponents.UpdateData();
    }

    private void ugSubcomponents_AfterCellUpdate(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
    {
      if (this.bUpdatingRow)
        return;
      try
      {
        string str = e.Cell.Row.Cells["Subcomplink"].Value.ToString();
        this.bUpdatingRow = true;
        this.bInitializeRow = true;
        int length1 = mdUtility.get_MstrTable("Distresses").Select(Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SubComplink = ", e.Cell.Row.Cells["Subcomplink"].Value), (object) " And BRED_Status <> 'R'"))).Length;
        int length2;
        if (mdUtility.get_MstrTable("SelectedCrit") != null)
          length2 = mdUtility.get_MstrTable("SelectedCrit").Select("subcomplink = '" + str + "' And CritStatus <> 'D'").Length;
        string key = e.Cell.Column.Key;
        // ISSUE: reference to a compiler-generated method
        switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(key))
        {
          case 117647209:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(key, "SAMP_SUBCOMP_NOTAPPL", false) == 0)
            {
              if (Conversions.ToBoolean(Microsoft.VisualBasic.CompilerServices.Operators.AndObject(Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectEqual(e.Cell.Value, (object) true, false), Microsoft.VisualBasic.CompilerServices.Operators.OrObject(Microsoft.VisualBasic.CompilerServices.Operators.OrObject((object) (length1 > 0), Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectGreater(e.Cell.Row.Cells["SAMP_SUBCOMP_CONDRATE"].Value, (object) 0, false)), (object) (length2 > 0)))))
              {
                int num = (int) Interaction.MsgBox((object) "Selecting Not Applicable inconsistent with recorded distresses.", MsgBoxStyle.OkOnly, (object) null);
                e.Cell.Value = (object) false;
                return;
              }
              if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(e.Cell.Value, (object) true, false))
              {
                this.chkDefFree.CheckState = CheckState.Unchecked;
                this.ugSubcomponents.ActiveRow.Cells["Criteria"].Activation = Activation.Disabled;
                this.ugSubcomponents.ActiveRow.Cells["SAMP_SUBCOMP_CONDRATE"].Activation = Activation.Disabled;
                e.Cell.Row.Cells["SAMP_SUBCOMP_INSP"].Value = (object) false;
                e.Cell.Row.Cells["SAMP_SUBCOMP_PAINTNA"].Value = (object) true;
                e.Cell.Row.Cells["SAMP_SUBCOMP_PAINTDF"].Value = (object) false;
                e.Cell.Row.Cells["SAMP_SUBCOMP_PAINTRATE"].Value = (object) -1;
                UltraGridCell cell = e.Cell.Row.Cells["SAMP_SUBCOMP_PAINTRATE"];
                this.SetRatingColor(ref cell);
                break;
              }
              this.ugSubcomponents.ActiveRow.Cells["Criteria"].Activation = Activation.AllowEdit;
              this.ugSubcomponents.ActiveRow.Cells["SAMP_SUBCOMP_CONDRATE"].Activation = Activation.AllowEdit;
              break;
            }
            break;
          case 1383849235:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(key, "SAMP_SUBCOMP_PAINTRATE", false) == 0)
            {
              int integer = Conversions.ToInteger(e.Cell.Value);
              if (Conversions.ToBoolean(Microsoft.VisualBasic.CompilerServices.Operators.AndObject((object) (integer > 0), Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectEqual(e.Cell.Row.Cells["SAMP_SUBCOMP_NOTAPPL"].Value, (object) true, false))))
              {
                int num = (int) Interaction.MsgBox((object) "You must inspect the subcomponent or mark it defect free before inspecting the paint.", MsgBoxStyle.OkOnly, (object) null);
                e.Cell.Row.Cells["SAMP_SUBCOMP_PAINTDF"].Value = (object) false;
                e.Cell.Value = (object) -1;
                return;
              }
              if (integer > 0)
              {
                e.Cell.Row.Cells["SAMP_SUBCOMP_PAINTNA"].Value = (object) false;
                if (integer < 100)
                {
                  this.chkPaintDefFree.CheckState = CheckState.Unchecked;
                  e.Cell.Row.Cells["SAMP_SUBCOMP_PAINTDF"].Value = (object) false;
                }
                else
                  e.Cell.Row.Cells["SAMP_SUBCOMP_PAINTDF"].Value = (object) true;
              }
              else
              {
                e.Cell.Row.Cells["SAMP_SUBCOMP_PAINTNA"].Value = (object) true;
                e.Cell.Row.Cells["SAMP_SUBCOMP_PAINTDF"].Value = (object) false;
              }
              UltraGridCell cell = e.Cell;
              this.SetRatingColor(ref cell);
              break;
            }
            break;
          case 2002174069:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(key, "SAMP_SUBCOMP_CONDRATE", false) == 0)
            {
              int integer = Conversions.ToInteger(e.Cell.Value);
              if (Conversions.ToBoolean(Microsoft.VisualBasic.CompilerServices.Operators.AndObject((object) (integer > 0), Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectEqual(e.Cell.Row.Cells["SAMP_SUBCOMP_NOTAPPL"].Value, (object) true, false))))
              {
                int num = (int) Interaction.MsgBox((object) "You must inspect the subcomponent or mark it defect free before inspecting the Direct Rating.", MsgBoxStyle.OkOnly, (object) null);
                e.Cell.Value = (object) -1;
                return;
              }
              if (integer == -5 & length2 > 0)
              {
                int num = (int) Interaction.MsgBox((object) "Selecting Not Inspected inconsistent with recorded distress-criteria.", MsgBoxStyle.OkOnly, (object) null);
                e.Cell.Value = (object) -1;
                return;
              }
              if (integer > 0)
              {
                e.Cell.Row.Cells["SAMP_SUBCOMP_INSP"].Value = (object) true;
                e.Cell.Row.Cells["SAMP_SUBCOMP_NOTAPPL"].Value = (object) false;
              }
              else if (length1 == 0)
              {
                e.Cell.Row.Cells["SAMP_SUBCOMP_INSP"].Value = (object) false;
                e.Cell.Row.Cells["SAMP_SUBCOMP_NOTAPPL"].Value = (object) true;
              }
              if (integer == -1)
                e.Cell.Row.Cells["Criteria"].Activation = Activation.AllowEdit;
              e.Cell.Row.Cells["Criteria"].Activation = integer != -5 ? Activation.AllowEdit : Activation.Disabled;
              UltraGridCell cell = e.Cell;
              this.SetRatingColor(ref cell);
              break;
            }
            break;
          case 2797793456:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(key, "SAMP_SUBCOMP_DEFFREE", false) == 0)
            {
              if (Conversions.ToBoolean(Microsoft.VisualBasic.CompilerServices.Operators.AndObject(Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectEqual(e.Cell.Value, (object) true, false), Microsoft.VisualBasic.CompilerServices.Operators.OrObject((object) (length1 > 0), Microsoft.VisualBasic.CompilerServices.Operators.CompareObjectGreater(e.Cell.Row.Cells["SAMP_SUBCOMP_CONDRATE"].Value, (object) 0, false)))))
              {
                int num = (int) Interaction.MsgBox((object) "Selecting Defect Free inconsistent with recorded distresses.", MsgBoxStyle.OkOnly, (object) null);
                e.Cell.Value = (object) false;
                return;
              }
              if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(e.Cell.Value, (object) true, false))
              {
                e.Cell.Row.Cells["SAMP_SUBCOMP_NOTAPPL"].Value = (object) false;
                e.Cell.Row.Cells["SAMP_SUBCOMP_INSP"].Value = (object) false;
                e.Cell.Row.Cells["SAMP_SUBCOMP_INSP"].Value = (object) true;
                break;
              }
              break;
            }
            break;
          case 3146649435:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(key, "SAMP_SUBCOMP_PAINTDF", false) == 0)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(e.Cell.Value, (object) true, false) && Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(e.Cell.Row.Cells["SAMP_SUBCOMP_NOTAPPL"].Value, (object) true, false))
              {
                int num = (int) Interaction.MsgBox((object) "You must inspect the subcomponent or mark it defect free before inspecting the paint.", MsgBoxStyle.OkOnly, (object) null);
                e.Cell.Value = (object) false;
                return;
              }
              if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(e.Cell.Value, (object) true, false))
              {
                e.Cell.Row.Cells["SAMP_SUBCOMP_PAINTNA"].Value = (object) false;
                e.Cell.Row.Cells["SAMP_SUBCOMP_PAINTRATE"].Value = (object) 100;
                UltraGridCell cell = e.Cell.Row.Cells["SAMP_SUBCOMP_PAINTRATE"];
                this.SetRatingColor(ref cell);
                break;
              }
              break;
            }
            break;
          case 3165104340:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(key, "SAMP_SUBCOMP_PAINTNA", false) == 0 && Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(e.Cell.Value, (object) true, false))
            {
              e.Cell.Row.Cells["SAMP_SUBCOMP_PAINTDF"].Value = (object) false;
              e.Cell.Row.Cells["SAMP_SUBCOMP_PAINTRATE"].Value = (object) -1;
              UltraGridCell cell = e.Cell.Row.Cells["SAMP_SUBCOMP_PAINTRATE"];
              this.SetRatingColor(ref cell);
              break;
            }
            break;
          case 3865904547:
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(key, "SAMP_SUBCOMP_INSP", false) == 0 && Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(e.Cell.Value, (object) true, false))
            {
              this.chkDefFree.CheckState = CheckState.Unchecked;
              e.Cell.Row.Cells["SAMP_SUBCOMP_NOTAPPL"].Value = (object) false;
              break;
            }
            break;
        }
        if (Conversions.ToBoolean(Microsoft.VisualBasic.CompilerServices.Operators.AndObject(e.Cell.Row.Cells["SAMP_SUBCOMP_PAINTNA"].Value, Microsoft.VisualBasic.CompilerServices.Operators.NotObject(e.Cell.Row.Cells["SAMP_SUBCOMP_INSP"].Value))) && (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.Cell.Column.Key, "SAMP_SUBCOMP_NOTAPPL", false) > 0U)
          e.Cell.Row.Cells["SAMP_SUBCOMP_NOTAPPL"].Value = (object) false;
        if (this.m_bInspLoaded)
        {
          if (this.ugSubcomponents.ActiveRow.Cells["ID"].Value == null)
            this.ugSubcomponents.ActiveRow.Cells["BRED_Status"].Value = (object) "A";
          else
            this.ugSubcomponents.ActiveRow.Cells["BRED_Status"].Value = (object) "C";
          this.SetInspChanged(true);
        }
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, nameof (frmMain), nameof (ugSubcomponents_AfterCellUpdate));
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.bUpdatingRow = false;
      }
    }

    private void SetRatingColor(ref UltraGridCell oCell)
    {
      string Left = oCell.Value.ToString();
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, Conversions.ToString(80), false) > 0)
        oCell.Appearance.BackColor = Color.Green;
      else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, Conversions.ToString(50), false) > 0)
        oCell.Appearance.BackColor = Color.Yellow;
      else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, Conversions.ToString(-5), false) > 0)
        oCell.Appearance.BackColor = Color.Red;
      else
        oCell.Appearance.BackColor = Color.White;
    }

    private void ShowTasksButton_Click(object eventSender, EventArgs eventArgs)
    {
      frmTaskList frmTaskList1 = new frmTaskList();
      frmTaskList frmTaskList2 = frmTaskList1;
      frmTaskList1.DataGridView1.DataSource = (object) mdUtility.get_MstrTable("Tasks");
      frmTaskList2 = (frmTaskList) null;
      int num = (int) frmTaskList1.ShowDialog();
    }

    private void ugSubcomponents_ClickCellButton(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
    {
      try
      {
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.Cell.Column.Header.Caption, "Distresses", false) == 0)
        {
          frmDistresses frmDistresses1 = new frmDistresses();
          frmDistresses frmDistresses2 = frmDistresses1;
          frmDistresses2.CanEdit = this.CanEditInspection;
          frmDistresses2.lblDistCompIDVal.Text = this.tvInspection.ActiveNode.Parent.Text;
          frmDistresses2.lblDistSecIDVal.Text = this.tvInspection.ActiveNode.Text;
          frmDistresses2.lblDistSubCompIDVal.Text = Conversions.ToString(this.ugSubcomponents.ActiveRow.Cells["SubComponent"].Value);
          frmDistresses2.SubCompLink = Conversions.ToString(this.ugSubcomponents.ActiveRow.Cells["SubCompLink"].Value);
          bool AltUM = Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(this.ugSubcomponents.ActiveRow.Cells["SAMP_SUBCOMP_ALTUM"].Value, (object) true, false);
          frmDistresses2.dtDistresses = mdUtility.get_MstrTable("Distresses");
          frmDistresses2.aSample = this.CurrentSample;
          int iDistressCount;
          if (frmDistresses2.EditDistresses(ref AltUM, ref iDistressCount))
          {
            this.SetInspChanged(true);
            this.ugSubcomponents.ActiveRow.Cells["SAMP_SUBCOMP_ALTUM"].Value = (object) AltUM;
          }
          this.ugSubcomponents.ActiveRow.Cells["SAMP_SUBCOMP_NOTAPPL"].Value = (object) false;
          if (iDistressCount > 0)
          {
            this.ugSubcomponents.ActiveRow.Cells["SAMP_SUBCOMP_INSP"].Value = (object) true;
            this.chkDefFree.CheckState = CheckState.Unchecked;
            this.ugSubcomponents.ActiveRow.Cells["SAMP_SUBCOMP_NOTAPPL"].Activation = Activation.NoEdit;
          }
          else if (Conversions.ToBoolean(this.ugSubcomponents.ActiveRow.Cells["SAMP_SUBCOMP_INSP"].Value))
          {
            this.ugSubcomponents.ActiveRow.Cells["SAMP_SUBCOMP_INSP"].Value = (object) false;
            this.ugSubcomponents.ActiveRow.Cells["SAMP_SUBCOMP_NOTAPPL"].Activation = Activation.AllowEdit;
          }
          frmDistresses1.Dispose();
        }
        else
        {
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(e.Cell.Column.Header.Caption, "Criteria", false) != 0)
            return;
          frmCriteria frmCriteria1 = new frmCriteria();
          string str = this.ugSubcomponents.ActiveRow.Cells["SubCompLink"].Value.ToString();
          string Left = this.ugSubcomponents.ActiveRow.Cells["ID"].Value.ToString();
          frmCriteria frmCriteria2 = frmCriteria1;
          frmCriteria2.SubCompLink = this.ugSubcomponents.ActiveRow.Cells["SubCompLink"].Value.ToString();
          frmCriteria2.SampSubCompID = Left;
          if (mdUtility.get_MstrTable("SelectedSSCCrit") == null && Left != null && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "", false) != 0 || mdUtility.get_MstrTable("SelectedSSCCrit") != null && mdUtility.get_MstrTable("SelectedSSCCrit").Rows.Count >= 0)
            mdUtility.LoadMstrTable("SelectedSSCCrit", "SELECT * FROM Sample_Subcomp_Criteria where SSC_SUBCOMP_ID = {" + Left + "}");
          mdUtility.LoadMstrTable("Criteria", "Select CriterionID As ID, CriterionDescription As Criteria, CMC_SCOMP_ID FROM\r\n                    (RO_CMC_SubComp\r\n                    INNER JOIN RO_Criteria_Linker ON\r\n                    RO_CMC_SubComp.CMC_SCOMP_CRIT_LIST_LINK = RO_Criteria_Linker.CriteriaLinkerCritListLink)\r\n                    INNER JOIN RO_Criterion ON RO_Criteria_Linker.CriteriaLinkerCriterionLink = RO_Criterion.CriterionID\r\n                    WHERE RO_CMC_SubComp.CMC_SCOMP_ID = " + str);
          frmCriteria2.dtCriteria = mdUtility.get_MstrTable("Criteria");
          int iCriteriaCount;
          frmCriteria2.EditCriteria(ref iCriteriaCount);
        }
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, nameof (frmMain), nameof (ugSubcomponents_ClickCellButton));
        ProjectData.ClearProjectError();
      }
    }

    private void optCompRating_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.m_bInspLoaded)
        {
          if (!this.CanEditInspection)
          {
            this.m_bInspLoaded = false;
            Sample.GetDirectRating(this.cboLocation.SelectedValue.ToString());
            this.m_bInspLoaded = true;
          }
          else
            this.SetInspChanged(true);
        }
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, this.Name, nameof (optCompRating_CheckedChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void optMethod_CheckedChanged(object sender, EventArgs e)
    {
      if (Conversions.ToBoolean(NewLateBinding.LateGet(sender, (System.Type) null, "Checked", new object[0], (string[]) null, (System.Type[]) null, (bool[]) null)))
      {
        try
        {
          this.frmLocation.Visible = true;
          this.frmLocation.Enabled = this.optMethod_1.Checked;
          this.frmLocation.Refresh();
          if (this.m_bInspLoaded)
          {
            if (!this.CanEditInspection)
            {
              this.m_bInspLoaded = false;
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(((Control) sender).Name, this.optMethod_0.Name, false) == 0)
                this.optMethod_1.Checked = true;
              else
                this.optMethod_0.Checked = true;
              this.m_bInspLoaded = true;
            }
            else
            {
              if (!this.optMethod_1.Checked)
                this.txtSQuantity.Text = this.lblSecQtyValue.Text;
              this.SetInspChanged(true);
            }
          }
        }
        catch (SystemException ex)
        {
          ProjectData.SetProjectError((Exception) ex);
          mdUtility.Errorhandler((Exception) ex, this.Name, nameof (optMethod_CheckedChanged));
          ProjectData.ClearProjectError();
        }
      }
    }

    private void optPaintRating_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.m_bInspLoaded)
        {
          if (!this.CanEditInspection)
          {
            this.m_bInspLoaded = false;
            Sample.GetPaintRating(Conversions.ToString(this.cboLocation.SelectedValue));
            this.m_bInspLoaded = true;
          }
          else
            this.SetInspChanged(true);
        }
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, this.Name, nameof (optPaintRating_CheckedChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void optRatingType_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.optRatingType_1.Checked)
        {
          this.frmDirectRating.Visible = false;
          this.ShowTasksButton.Visible = false;
          this.frmDistressSurvey.Visible = true;
          this.HelpProvider.SetHelpKeyword((Control) this.pnlInspectionInfo, "Direct Rating Definitions");
        }
        else if (this.optRatingType_2.Checked)
        {
          this.frmDirectRating.Visible = true;
          this.frmDistressSurvey.Visible = false;
          this.ShowTasksButton.Visible = false;
          this.HelpProvider.SetHelpKeyword((Control) this.pnlInspectionInfo, "Distress Surveys");
        }
        else if (this.optRatingType_3.Checked)
        {
          this.frmDirectRating.Visible = false;
          this.frmDistressSurvey.Visible = true;
          this.ShowTasksButton.Visible = true;
          this.HelpProvider.SetHelpKeyword((Control) this.pnlInspectionInfo, "PM Inspections");
        }
        if (this.m_bInspLoaded)
        {
          if (!this.m_bInspCanEdit)
          {
            this.m_bInspLoaded = false;
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(((Control) sender).Name, this.optRatingType_1.Name, false) == 0)
              this.optRatingType_2.Checked = true;
            else
              this.optRatingType_1.Checked = true;
            this.m_bInspLoaded = true;
            return;
          }
          if (Section.SectionMaterialLink(this.CurrentSectionID) == -1 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(((Control) sender).Name, this.optRatingType_1.Name, false) == 0 | Microsoft.VisualBasic.CompilerServices.Operators.CompareString(((Control) sender).Name, this.optRatingType_3.Name, false) == 0)
          {
            this.m_bInspLoaded = false;
            this.optRatingType_2.Checked = true;
            this.m_bInspLoaded = true;
            return;
          }
          this.m_bInspLoaded = false;
          Sample.LoadSample(this.CurrentSample);
          this.m_bInspLoaded = true;
          this.SetInspChanged(true);
        }
        MyProject.Forms.frmTaskList.DataGridView1.DataSource = (object) mdUtility.get_MstrTable("Tasks");
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, this.Name, nameof (optRatingType_CheckedChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void Building_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.m_bDDLoad || !this.m_bBldgLoaded)
          return;
        this.SetBldgChanged(true);
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, this.Name, nameof (Building_SelectedIndexChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void cboCatCodeSelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.lblBldgSF.Text = Building.GetUnitsLabel((short?) this.cboCatCode.SelectedValue);
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, this.Name, nameof (cboCatCodeSelectedIndexChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void cboSectionComponentType_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.m_bSectionLoaded)
        {
          this.SetSectionChanged(true);
          this.lblUOM.Text = mdUtility.UOMforCMC(Conversions.ToLong(this.cboSectionComponentType.SelectedValue));
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.lblUOM.Text, "EA", false) == 0)
          {
            this.cmdIncrease.Visible = true;
            this.cmdDecrease.Visible = true;
            this.cmdCalc.Visible = false;
          }
          else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.lblUOM.Text, "SF", false) == 0 || Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.lblUOM.Text, "SM", false) == 0)
          {
            this.cmdIncrease.Visible = false;
            this.cmdDecrease.Visible = false;
            this.cmdCalc.Visible = true;
          }
          else
          {
            this.cmdIncrease.Visible = false;
            this.cmdDecrease.Visible = false;
            this.cmdCalc.Visible = false;
          }
        }
        this.ToolTip1.SetToolTip((Control) this.cboSectionComponentType, Conversions.ToString(NewLateBinding.LateGet(this.cboSectionComponentType.SelectedItem, (System.Type) null, "item", new object[1]
        {
          (object) "comp_type_desc"
        }, (string[]) null, (System.Type[]) null, (bool[]) null)));
        this.m_bDDLoad = false;
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, this.Name, nameof (cboSectionComponentType_SelectedIndexChanged));
        ProjectData.ClearProjectError();
      }
    }

    internal void cboSectionMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.m_bDDLoad)
          return;
        object obj;
        if (this.cboSectionComponentType.SelectedIndex != -1)
          obj = (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.cboSectionComponentType.SelectedText, "Unknown", false) <= 0U ? (object) null : (!Versioned.IsNumeric(RuntimeHelpers.GetObjectValue(this.cboSectionComponentType.SelectedValue)) ? (object) null : RuntimeHelpers.GetObjectValue(Interaction.IIf((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.cboSectionComponentType.SelectedText, "N/A", false) > 0U, RuntimeHelpers.GetObjectValue(this.cboSectionComponentType.SelectedItem), (object) null)));
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.CurrentComp, "", false) > 0U)
        {
          string sSQL;
          if (mdUtility.UseUniformat)
            sSQL = "Select [CMC_ID], comp_type_desc FROM qryComponentTypesUniformat WHERE [comp_id]= " + Conversions.ToString(Component.ComponentLink(this.CurrentComp)) + " And [CMC_MCAT_LINK]= " + Conversions.ToString(this.cboSectionMaterial.SelectedValue) + " ORDER BY COMP_TYPE_DESC";
          else
            sSQL = "Select [CMC_ID], comp_type_desc FROM qryComponentTypes WHERE [comp_id]=  " + Conversions.ToString(Component.ComponentLink(this.CurrentComp)) + " And [CMC_MCAT_LINK]= " + Conversions.ToString(this.cboSectionMaterial.SelectedValue) + " ORDER BY COMP_TYPE_DESC";
          mdUtility.LoadMstrTable("SectionComponentType", sSQL);
          this.cboSectionComponentType.DisplayMember = "comp_type_desc";
          this.cboSectionComponentType.ValueMember = "CMC_ID";
          this.cboSectionComponentType.DataSource = (object) mdUtility.get_MstrTable("SectionComponentType");
          this.cboSectionComponentType.DropDownWidth = mdUtility.GetDropDownWidth((DataTable) this.cboSectionComponentType.DataSource, this.cboSectionComponentType.DisplayMember, this.cboSectionComponentType, (Form) this);
        }
        if (this.m_bSectionLoaded)
        {
          this.cboSectionComponentType.SelectedIndex = obj != null ? (!this.cboSectionComponentType.Items.Contains(RuntimeHelpers.GetObjectValue(obj)) ? 0 : this.cboSectionComponentType.Items.IndexOf(RuntimeHelpers.GetObjectValue(obj))) : (mdUtility.get_MstrTable("Components").Rows.Count != 2 ? 0 : 1);
          this.SetSectionChanged(true);
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (cboSectionMaterial_SelectedIndexChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void cboSectionStatus_SelectedIndexChanged(object send, EventArgs e)
    {
      try
      {
        if (!this.m_bSectionLoaded)
          return;
        this.SetSectionChanged(true);
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, this.Name, nameof (cboSectionStatus_SelectedIndexChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void cboSectionName_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        if (!this.m_bSectionLoaded)
          return;
        this.SetSectionChanged(true);
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, this.Name, nameof (cboSectionName_SelectedIndexChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void SectionChanged(object sender, EventArgs e)
    {
      try
      {
        if (!this.m_bSectionLoaded)
          return;
        this.SetSectionChanged(true);
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, this.Name, nameof (SectionChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void LoadSectionNames(string strBldgID)
    {
      try
      {
        DataTable dataTable = mdUtility.DB.GetDataTable("SELECT DISTINCT SEC_NAME FROM [qrySectionNamesByBldg] where [Facility_ID] = {" + strBldgID + "}");
        System.Windows.Forms.ComboBox cboSectionName = this.cboSectionName;
        cboSectionName.Items.Clear();
        cboSectionName.Text = "";
        cboSectionName.BeginUpdate();
        try
        {
          foreach (DataRow row in dataTable.Rows)
            cboSectionName.Items.Add(RuntimeHelpers.GetObjectValue(row["Sec_Name"]));
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        cboSectionName.EndUpdate();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (LoadSectionNames));
        ProjectData.ClearProjectError();
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public void cboLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        if (!this.m_bDDLoad)
        {
          this.m_bUserChangedLocation = false;
          if (this.m_bInspLoaded)
          {
            if (this.m_bInspNew)
            {
              bool MustSave = true;
              if (!this.CheckInspForSave(ref MustSave))
              {
                UltraTreeNode nodeByKey = this.tvInspection.GetNodeByKey(this.CurrentSection);
                this.SelectNewActiveInspectionNode(ref nodeByKey);
                return;
              }
            }
            else
            {
              bool MustSave = false;
              if (!this.CheckInspForSave(ref MustSave))
              {
                this.m_bInspLoaded = false;
                this.cboLocation.SelectedValue = (object) this.CurrentSample;
                this.m_bInspLoaded = true;
                return;
              }
              this.m_bUserChangedLocation = true;
            }
          }
          if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(this.cboLocation.SelectedValue, (object) null, false))
          {
            this.m_bInspLoaded = false;
            Sample.LoadSample(this.cboLocation.SelectedValue.ToString());
            if (this.m_bUserChangedLocation)
            {
              DataTable dataTable = mdUtility.DB.GetDataTable("SELECT SAMP_DATA_LOC FROM Sample_Data WHERE [SAMP_DATA_ID]={" + this.cboLocation.SelectedValue.ToString() + "}");
              if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.CurrentSystem, "", false) > 0U)
              {
                if (!Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(this.tvInspection.ActiveNode.Tag, (object) "Section", false))
                {
                  if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["SAMP_DATA_LOC"])))
                  {
                    UltraTreeNode nodeByKey1 = this.tvInspection.GetNodeByKey(this.CurrentSection);
                    string location = this.FindLocation(ref nodeByKey1, Conversions.ToString(dataTable.Rows[0]["SAMP_DATA_LOC"]));
                    if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(location, "", false) > 0U)
                    {
                      UltraTreeNode nodeByKey2 = this.tvInspection.GetNodeByKey(location);
                      this.SelectNewActiveInspectionNode(ref nodeByKey2);
                    }
                    else
                      Information.Err().Raise(10100, (object) nameof (cboLocation_SelectedIndexChanged), (object) "Could Not locate sample location under section on the tree.", (object) null, (object) null);
                  }
                  else
                  {
                    UltraTreeNode nodeByKey = this.tvInspection.GetNodeByKey(this.CurrentSection);
                    this.SelectNewActiveInspectionNode(ref nodeByKey);
                  }
                }
              }
              else if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataTable.Rows[0]["SAMP_DATA_LOC"])))
              {
                TreeNodesCollection nodes;
                UltraTreeNode ParentNode = (nodes = this.tvInspection.GetNodeByKey(this.CurrentBldg).Nodes)[1];
                string location = this.FindLocation(ref ParentNode, Conversions.ToString(dataTable.Rows[0]["SAMP_DATA_LOC"]));
                nodes[1] = ParentNode;
                string str1 = location;
                if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str1, "", false) > 0U)
                {
                  UltraTreeNode nodeByKey = this.tvInspection.GetNodeByKey(str1);
                  ref UltraTreeNode local1 = ref nodeByKey;
                  string str2 = this.tvInspection.GetNodeByKey(this.CurrentComp).Tag.ToString();
                  ref string local2 = ref str2;
                  string component = this.FindComponent(ref local1, ref local2);
                  if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(component, "", false) > 0U)
                  {
                    nodeByKey = this.tvInspection.GetNodeByKey(component);
                    string str3 = Conversions.ToString(this.FindSection(ref nodeByKey, this.tvInspection.GetNodeByKey(this.CurrentSection).Tag.ToString()));
                    if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str3, "", false) > 0U)
                    {
                      nodeByKey = this.tvInspection.GetNodeByKey(str3);
                      this.SelectNewActiveInspectionNode(ref nodeByKey);
                    }
                    else
                      Information.Err().Raise(10100, (object) nameof (cboLocation_SelectedIndexChanged), (object) "Could Not locate section under sample location on the tree.", (object) null, (object) null);
                  }
                  else
                    Information.Err().Raise(10100, (object) nameof (cboLocation_SelectedIndexChanged), (object) "Could Not locate component under sample location on the tree.", (object) null, (object) null);
                }
                else
                  Information.Err().Raise(10100, (object) nameof (cboLocation_SelectedIndexChanged), (object) "Could Not locate sample location on the tree.", (object) null, (object) null);
              }
              this.m_bInspLoaded = true;
            }
            this.m_strSampID = this.cboLocation.SelectedValue.ToString();
          }
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (cboLocation_SelectedIndexChanged));
        ProjectData.ClearProjectError();
      }
    }

    private void cboInspectionDates_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        if (!this.m_bDDLoad)
        {
          if (this.m_bInspLoaded)
          {
            bool MustSave = false;
            if (!this.CheckInspForSave(ref MustSave))
            {
              this.cboInspectionDates.SelectedValue = (object) this.CurrentInspection;
              return;
            }
          }
          if (this.cboInspectionDates.SelectedIndex != -1)
          {
            this.m_strInspID = this.cboInspectionDates.SelectedValue.ToString();
            this.SetInventoryClassForImages(InventoryClass.InspectionData, this.m_strInspID);
            Inspection.LoadInspection(this.cboInspectionDates.SelectedValue.ToString());
            this.tsbComment.Enabled = true;
          }
        }
        else
          this.tsbComment.Enabled = false;
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, this.Name, "cboInspectionDates_SelectedValueChanged");
        ProjectData.ClearProjectError();
      }
    }

    private void Timer1_Tick(object sender, EventArgs e)
    {
      this.tsslCurrentDate.Text = DateAndTime.Now.ToShortDateString();
      this.tsslCurrentTime.Text = DateAndTime.Now.ToShortTimeString();
    }

    private bool DataHasBeenSaved()
    {
      bool flag = true;
      if (this.Mode == frmMain.ProgramMode.pmInspection)
      {
        if (this.m_bInspNeedToSave)
        {
          bool MustSave = false;
          flag = this.CheckInspForSave(ref MustSave);
        }
      }
      else if (this.BldgNeedToSave)
        flag = this.CheckBldgForSave();
      else if (this.m_bSectionNeedToSave)
        flag = this.CheckSecForSave();
      return flag;
    }

    private void tvInventory_BeforeActivate(object sender, CancelableNodeEventArgs e)
    {
      if (!this.DataHasBeenSaved())
        e.Cancel = true;
      this.tsslStatus.Text = mdUtility.LookupDataBaseName;
    }

    private void tvInventory_BeforeExpand(object sender, CancelableNodeEventArgs e)
    {
      if (this.DataHasBeenSaved())
        return;
      e.Cancel = true;
    }

    private void tvFunctionality_BeforeActivate(object sender, CancelableNodeEventArgs e)
    {
      if (!this.DataHasBeenSaved())
        e.Cancel = true;
      this.tsslStatus.Text = mdUtility.LookupDataBaseName;
    }

    private void tvInspection_BeforeActivate(object sender, CancelableNodeEventArgs e)
    {
      if (!this.DataHasBeenSaved())
        e.Cancel = true;
      this.tsslStatus.Text = mdUtility.LookupDataBaseName;
    }

    private void EditLocation()
    {
      if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Microsoft.VisualBasic.Strings.Left(this.tvInspection.ActiveNode.Key, 1), "L", false) != 0 || !((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.tvInspection.ActiveNode.Tag.ToString(), "Sample Locations", false) > 0U & (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.tvInspection.ActiveNode.Tag.ToString(), "Non-sampling", false) > 0U))
        return;
      try
      {
        string Right = this.tvInspection.ActiveNode.Tag.ToString();
        string text = this.tvInspection.ActiveNode.Text;
        string Left = new frmEditLocation().NewLocationName(text);
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "", false) > 0U)
        {
          string str = "SELECT * FROM Sample_Location where Name = '" + Microsoft.VisualBasic.Strings.Replace(text, "'", "''", 1, -1, CompareMethod.Binary) + "' and Building_ID = {" + Microsoft.VisualBasic.Strings.Replace(mdUtility.fMainForm.CurrentBldg, "'", "''", 1, -1, CompareMethod.Binary) + "}";
          DataTable dataTable1 = mdUtility.DB.GetDataTable(str);
          if (dataTable1.Rows.Count > 0)
          {
            dataTable1.Rows[0]["Name"] = (object) Left;
            dataTable1.Rows[0]["BRED_Status"] = (object) "U";
            mdUtility.DB.SaveDataTable(ref dataTable1, str);
          }
          this.tvInspection.ActiveNode.Text = Left;
          this.tvInspection.ActiveNode.Parent.Nodes.Override.Sort = SortType.Ascending;
          DataTable dataTable2 = mdUtility.DB.GetDataTable("SELECT Sec_ID FROM Samples_by_Sections where Samp_Data_Loc = {" + Right + "}");
          try
          {
            foreach (DataRow row in dataTable2.Rows)
            {
              UltraTreeNode nodeByKey = this.tvInspection.GetNodeByKey(row["Sec_ID"].ToString());
              if (nodeByKey != null && nodeByKey.Nodes.Count > 0)
              {
                foreach (UltraTreeNode node in nodeByKey.Nodes)
                {
                  if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(node.Tag.ToString(), Right, false) == 0)
                    node.Text = Left;
                }
                nodeByKey.Nodes.Override.Sort = SortType.Ascending;
              }
            }
          }
          finally
          {
            IEnumerator enumerator;
            if (enumerator is IDisposable)
              (enumerator as IDisposable).Dispose();
          }
        }
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, nameof (frmMain), nameof (EditLocation));
        ProjectData.ClearProjectError();
      }
    }

    private void DeleteLocation()
    {
      try
      {
        UltraTreeNode activeNode = this.tvInspection.ActiveNode;
        UltraTreeNode nd;
        if (activeNode != null && (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Microsoft.VisualBasic.Strings.Left(activeNode.Key, 1), "L", false) == 0 && (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(activeNode.Tag.ToString(), "Sample Locations", false) > 0U & (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(activeNode.Tag.ToString(), "Non-sampling", false) > 0U))
        {
          string str1 = activeNode.Tag.ToString();
          bool flag = false;
          if (Conversions.ToInteger(mdUtility.DB.GetDataTable("SELECT Count(Samp_Data_Loc) FROM Samples_by_Sections where Samp_Data_Loc = {" + str1 + "} and BRED_Status = 'N'").Rows[0][0]) == 0)
            flag = true;
          if (flag)
          {
            string str2;
            if (Interaction.MsgBox((object) ("Are you sure you want to delete \rthe location " + str2 + "?"), MsgBoxStyle.OkCancel | MsgBoxStyle.Critical, (object) "Are you sure?") == MsgBoxResult.Ok)
            {
              string str3 = "SELECT * FROM Sample_Location where Location_ID = {" + str1 + "} and BRED_Status = 'N'";
              DataTable dataTable = mdUtility.DB.GetDataTable(str3);
              if (dataTable.Rows.Count > 0)
                dataTable.Rows[0].Delete();
              mdUtility.DB.SaveDataTable(ref dataTable, str3);
              nd = activeNode.Parent;
              nd.Nodes.Remove(activeNode);
              this.SelectNewActiveInspectionNode(ref nd);
            }
          }
          else
          {
            int num = (int) Interaction.MsgBox((object) "Unable to delete the selected location.\rExisting inspection samples refer to this location.", MsgBoxStyle.Critical, (object) "Problem Encountered");
          }
        }
        nd = (UltraTreeNode) null;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (DeleteLocation));
        ProjectData.ClearProjectError();
      }
    }

    public void SelectNewActiveInventoryNode(ref UltraTreeNode nd)
    {
      UltraTreeNode ultraTreeNode = nd;
      try
      {
        if (nd != null)
        {
          this.tvInventory.ActiveNode = (UltraTreeNode) null;
          this.tvInventory.PerformAction(UltraTreeAction.ClearAllSelectedNodes, false, false);
          ultraTreeNode.BringIntoView();
          this.tvInventory.ActiveNode = nd;
          this.tvInventory.PerformAction(UltraTreeAction.SelectActiveNode, false, false);
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        ProjectData.ClearProjectError();
      }
    }

    public void SelectNewActiveFunctionalityNode(ref UltraTreeNode nd)
    {
      UltraTreeNode ultraTreeNode = nd;
      try
      {
        if (nd != null)
        {
          this.tvFunctionality.PerformAction(UltraTreeAction.ClearAllSelectedNodes, false, false);
          this.tvFunctionality.ActiveNode = (UltraTreeNode) null;
          ultraTreeNode.BringIntoView();
          this.tvFunctionality.ActiveNode = ultraTreeNode;
          this.tvFunctionality.PerformAction(UltraTreeAction.SelectActiveNode, false, false);
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmMain), nameof (SelectNewActiveFunctionalityNode));
        ProjectData.ClearProjectError();
      }
    }

    public void SelectNewActiveInspectionNode(ref UltraTreeNode nd)
    {
      UltraTreeNode ultraTreeNode = nd;
      try
      {
        if (ultraTreeNode == null)
          return;
        this.tvInspection.PerformAction(UltraTreeAction.ClearAllSelectedNodes, false, false);
        this.tvInspection.ActiveNode = (UltraTreeNode) null;
        ultraTreeNode.BringIntoView();
        this.tvInspection.ActiveNode = ultraTreeNode;
        this.tvInspection.PerformAction(UltraTreeAction.SelectActiveNode, false, false);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, nameof (frmMain), nameof (SelectNewActiveInspectionNode));
        ProjectData.ClearProjectError();
      }
      finally
      {
      }
    }

    public void AddNewLocationToInspectionTree(
      string strCompID,
      string strSecID,
      string strLocID,
      string strLocName)
    {
      try
      {
        UltraTreeNode nodeByKey1 = this.tvInspection.GetNodeByKey(strSecID);
        if (nodeByKey1 != null)
        {
          if (nodeByKey1.Nodes.Count == 0 || Microsoft.VisualBasic.CompilerServices.Operators.CompareString(nodeByKey1.Nodes[0].Key, nodeByKey1.Key + "1", false) == 0)
          {
            mdHierarchyFunction.LoadInspectionSamples(strSecID, nodeByKey1.Key);
          }
          else
          {
            bool flag = false;
            foreach (SubObjectBase node in nodeByKey1.Nodes)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(node.Tag.ToString(), strLocID, false) == 0)
              {
                flag = true;
                break;
              }
            }
            if (!flag)
            {
              nodeByKey1.Nodes.Add("L-" + mdUtility.NewRandomNumberString() + strLocID, strLocName).Tag = (object) strLocID;
              nodeByKey1.Nodes.Override.Sort = SortType.Ascending;
            }
          }
        }
        UltraTreeNode nodeByKey2 = this.tvInspection.GetNodeByKey("L-" + this.CurrentBldg + "Sample Locations");
        if (nodeByKey2 != null && nodeByKey2.Nodes.Count > 0 && (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(nodeByKey2.Nodes[0].Key, nodeByKey2.Key + "1", false) > 0U)
        {
          bool flag = false;
          foreach (SubObjectBase node in nodeByKey2.Nodes)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(node.Tag.ToString(), strLocID, false) == 0)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            UltraTreeNode ultraTreeNode = nodeByKey2.Nodes.Add("L-" + mdUtility.NewRandomNumberString() + strLocID, strLocName);
            ultraTreeNode.Tag = (object) strLocID;
            ultraTreeNode.Nodes.Add(ultraTreeNode.Key + "1", "Temp");
            nodeByKey2.Nodes.Override.Sort = SortType.Ascending;
          }
        }
      }
      catch (SystemException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        mdUtility.Errorhandler((Exception) ex, this.Name, nameof (AddNewLocationToInspectionTree));
        ProjectData.ClearProjectError();
      }
    }

    public void MoveSampleLocation(
      string strCompID,
      string strSecID,
      string strOrigLocID,
      string strNewLocID,
      string strName)
    {
      try
      {
        bool flag1 = false;
        if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(strOrigLocID, "", false) > 0U)
        {
          if (Conversions.ToInteger(mdUtility.DB.GetDataTable("SELECT Count(Sample_Data.SAMP_DATA_LOC) FROM Inspection_Data INNER JOIN Sample_Data ON Inspection_Data.[INSP_DATA_ID] = Sample_Data.[SAMP_DATA_INSP_DATA_ID] WHERE Inspection_Data.[INSP_DATA_SEC_ID]= {" + strSecID + "} AND Sample_Data.SAMP_DATA_LOC= {" + strOrigLocID + "}").Rows[0][0]) == 0)
            flag1 = true;
        }
        UltraTreeNode nodeByKey1 = this.tvInspection.GetNodeByKey(strSecID);
        UltraTreeNode nd1;
        if (nodeByKey1 != null && nodeByKey1.Nodes.Count > 0)
        {
          if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(strOrigLocID, "", false) > 0U)
          {
            if (flag1)
            {
              if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(nodeByKey1.Nodes[0].Key, nodeByKey1.Key + "1", false) > 0U)
              {
                int num = checked (nodeByKey1.Nodes.Count - 1);
                int index = 0;
                while (index <= num)
                {
                  if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(nodeByKey1.Nodes[index].Tag.ToString(), strOrigLocID, false) == 0)
                  {
                    nodeByKey1.Nodes.Remove(nodeByKey1.Nodes[index]);
                    break;
                  }
                  checked { ++index; }
                }
              }
              else
                goto label_27;
            }
          }
          else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(nodeByKey1.Nodes[0].Key, nodeByKey1.Key + "1", false) == 0)
            nodeByKey1.Nodes.Remove(nodeByKey1.Nodes[0]);
          bool flag2 = false;
          int num1 = checked (nodeByKey1.Nodes.Count - 1);
          int index1 = 0;
          while (index1 <= num1)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(nodeByKey1.Nodes[index1].Tag.ToString(), strNewLocID, false) == 0)
            {
              nd1 = nodeByKey1;
              flag2 = true;
              break;
            }
            checked { ++index1; }
          }
          if (!flag2)
          {
            nd1 = nodeByKey1.Nodes.Add("L-" + mdUtility.NewRandomNumberString() + strNewLocID, strName);
            nd1.Tag = (object) strNewLocID;
            nodeByKey1.Nodes.Override.Sort = SortType.Ascending;
          }
        }
label_27:
        UltraTreeNode ParentNode1 = this.tvInspection.GetNodeByKey("L-" + this.CurrentBldg + "Sample Locations");
        UltraTreeNode ParentNode2;
        UltraTreeNode ParentNode3;
        UltraTreeNode nd2;
        if (ParentNode1 != null)
        {
          string location1 = this.FindLocation(ref ParentNode1, strOrigLocID);
          if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(location1, "", false) > 0U & flag1)
          {
            ParentNode2 = this.tvInspection.GetNodeByKey(location1);
            if (ParentNode2 != null)
            {
              string component = this.FindComponent(ref ParentNode2, ref strCompID);
              if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(component, "", false) > 0U)
              {
                ParentNode3 = this.tvInspection.GetNodeByKey(component);
                if (ParentNode3 != null)
                {
                  string str = Conversions.ToString(this.FindSection(ref ParentNode3, strSecID));
                  if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "", false) > 0U)
                    ParentNode3.Nodes.Remove(this.tvInspection.GetNodeByKey(str));
                  if (ParentNode3.Nodes.Count == 0)
                    ParentNode2.Nodes.Remove(ParentNode3);
                }
              }
            }
          }
          if (ParentNode1.Nodes.Count > 0 && (uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(ParentNode1.Nodes[0].Key, "L-" + this.CurrentBldg + "Sample Locations1", false) > 0U)
          {
            string location2 = this.FindLocation(ref ParentNode1, strNewLocID);
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(location2, "", false) == 0)
            {
              ParentNode2 = ParentNode1.Nodes.Add("L-" + mdUtility.NewRandomNumberString() + strNewLocID, strName);
              ParentNode2.Tag = (object) strNewLocID;
              ParentNode1.Nodes.Override.Sort = SortType.Ascending;
            }
            else
              ParentNode2 = this.tvInspection.GetNodeByKey(location2);
            if (ParentNode2 != null)
            {
              string component = this.FindComponent(ref ParentNode2, ref strCompID);
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(component, "", false) == 0)
              {
                ParentNode3 = ParentNode2.Nodes.Add("C-" + mdUtility.NewRandomNumberString() + strCompID, Component.Description(strCompID));
                ParentNode3.Tag = (object) strCompID;
                ParentNode2.Nodes.Override.Sort = SortType.Ascending;
              }
              else
                ParentNode3 = this.tvInspection.GetNodeByKey(component);
              if (ParentNode3 != null)
              {
                string str = Conversions.ToString(this.FindSection(ref ParentNode3, strSecID));
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "", false) == 0)
                {
                  nd2 = ParentNode3.Nodes.Add("S-" + mdUtility.NewRandomNumberString() + strSecID, Section.SectionLabel(strSecID));
                  nd2.Tag = (object) strSecID;
                  ParentNode3.Nodes.Override.Sort = SortType.Ascending;
                }
                else
                  nd2 = this.tvInspection.GetNodeByKey(str);
              }
            }
          }
        }
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.CurrentSystem, "", false) != 0 && nd1 != null)
          this.SelectNewActiveInspectionNode(ref nd1);
        else if (nd2 != null)
          this.SelectNewActiveInspectionNode(ref nd2);
        else if (this.tvInspection.GetNodeByKey(strSecID) != null)
        {
          UltraTreeNode nodeByKey2 = this.tvInspection.GetNodeByKey(strSecID);
          this.SelectNewActiveInspectionNode(ref nodeByKey2);
        }
        ParentNode1 = (UltraTreeNode) null;
        nd1 = (UltraTreeNode) null;
        ParentNode2 = (UltraTreeNode) null;
        ParentNode3 = (UltraTreeNode) null;
        nd2 = (UltraTreeNode) null;
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (MoveSampleLocation));
        ProjectData.ClearProjectError();
      }
    }

    private void optRatingType_Enter(object sender, EventArgs e)
    {
      try
      {
        if (mdUtility.get_MstrTable("Samples") != null)
        {
          if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.m_strSecID, "", false) <= 0U || mdUtility.get_MstrTable("Samples").Rows.Count != 1 && this.CanEditInspection && Section.SectionMaterialLink(this.CurrentSectionID) != -1)
            return;
          this.frmRatingType.Focus();
        }
        else
          this.frmRatingType.Focus();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, "frMain", nameof (optRatingType_Enter));
        ProjectData.ClearProjectError();
      }
    }

    private void SectionReadonly_Enter(object sender, EventArgs e)
    {
      if (!this.txtSectionAmount.ReadOnly)
        return;
      this.tsToolbar.Focus();
    }

    private void BuildingReadonly_Enter(object sender, EventArgs e)
    {
      if (!this.txtBuildingName.ReadOnly)
        return;
      this.tsToolbar.Focus();
    }

    private void SelectInspector()
    {
      try
      {
        int num = (int) new frmChooseInspector().ShowDialog();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, nameof (SelectInspector));
        ProjectData.ClearProjectError();
      }
    }

    private void miOpenLastFile_Click(object sender, EventArgs e)
    {
      this.miOpenLastFile.Checked = !this.miOpenLastFile.Checked;
      MySettingsProperty.Settings.OpenLastFile = this.miOpenLastFile.Checked;
    }

    private void chkEnergyEneryAuditRequired_Click(object sender, EventArgs e)
    {
      if (!this.m_bSectionLoaded || !this.txtSectionAmount.ReadOnly)
        return;
      this.m_bSectionLoaded = false;
      this.chkEnergyAuditRequired.Checked = !this.chkEnergyAuditRequired.Checked;
      this.m_bSectionLoaded = true;
    }

    private void txtAlternateID_Enter(object sender, EventArgs e)
    {
      if (this.txtAlternateID.ReadOnly || MessageBox.Show("Altering this value should only be done by system integrators.  Are you sure you want to edit this value?", "Confirm Action", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Cancel)
        return;
      this.txtNoFloors.Focus();
    }

    private void cmdIncrease_Click(object sender, EventArgs e)
    {
      if (this.txtSectionAmount.ReadOnly)
        return;
      this.txtSectionAmount.Text = !Versioned.IsNumeric((object) this.txtSectionAmount.Text) ? Conversions.ToString(1) : Conversions.ToString(Conversions.ToDouble(this.txtSectionAmount.Text) + 1.0);
    }

    private void cmdDecrease_Click(object sender, EventArgs e)
    {
      if (this.txtSectionAmount.ReadOnly)
        return;
      this.txtSectionAmount.Text = !Versioned.IsNumeric((object) this.txtSectionAmount.Text) || Conversions.ToDouble(this.txtSectionAmount.Text) <= 1.0 ? Conversions.ToString(1) : Conversions.ToString(Conversions.ToDouble(this.txtSectionAmount.Text) - 1.0);
    }

    private void cmdCalc_Click(object sender, EventArgs e)
    {
      if (this.txtSectionAmount.ReadOnly)
        return;
      double area = new dlgCalculateArea().CalculateArea((Form) this, Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.txtSectionAmount.Text, "", false) != 0 ? Conversions.ToDouble(this.txtSectionAmount.Text) : 0.0);
      if (area != -1.0)
        this.txtSectionAmount.Text = Conversions.ToString(area);
    }

    private void cmdIncrease_GotFocus(object sender, EventArgs e)
    {
      if (!this.txtSectionAmount.ReadOnly)
        return;
      this.tsToolbar.Focus();
    }

    private void alertFormsHide_Click(object sender, EventArgs e)
    {
      this.alertFormsUpdate.Hide();
    }

    private void lblSecQtyValue_TextChanged(object sender, EventArgs e)
    {
      this.ToolTip1.SetToolTip((Control) this.lblSecQtyValue, this.lblSecQtyValue.Text.ToString());
    }

    private void SplitContainer_Paint(object sender, PaintEventArgs e)
    {
      object Instance = (object) (sender as SplitContainer);
      Point[] pointArray1 = new Point[3];
      object objectValue1 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(Instance, (System.Type) null, "Width", new object[0], (string[]) null, (System.Type[]) null, (bool[]) null));
      object objectValue2 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(Instance, (System.Type) null, "Height", new object[0], (string[]) null, (System.Type[]) null, (bool[]) null));
      object objectValue3 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(Instance, (System.Type) null, "SplitterDistance", new object[0], (string[]) null, (System.Type[]) null, (bool[]) null));
      object objectValue4 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(Instance, (System.Type) null, "SplitterWidth", new object[0], (string[]) null, (System.Type[]) null, (bool[]) null));
      if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(Instance, (System.Type) null, "Orientation", new object[0], (string[]) null, (System.Type[]) null, (bool[]) null), (object) Orientation.Horizontal, false))
      {
        pointArray1[0] = new Point(Conversions.ToInteger(Microsoft.VisualBasic.CompilerServices.Operators.DivideObject(objectValue1, (object) 2)), Conversions.ToInteger(Microsoft.VisualBasic.CompilerServices.Operators.AddObject(objectValue3, Microsoft.VisualBasic.CompilerServices.Operators.DivideObject(objectValue4, (object) 2))));
        pointArray1[1] = new Point(checked (pointArray1[0].X - 10), pointArray1[0].Y);
        pointArray1[2] = new Point(checked (pointArray1[0].X + 10), pointArray1[0].Y);
      }
      else
      {
        pointArray1[0] = new Point(Conversions.ToInteger(Microsoft.VisualBasic.CompilerServices.Operators.AddObject(objectValue3, Microsoft.VisualBasic.CompilerServices.Operators.DivideObject(objectValue4, (object) 2))), Conversions.ToInteger(Microsoft.VisualBasic.CompilerServices.Operators.DivideObject(objectValue2, (object) 2)));
        pointArray1[1] = new Point(pointArray1[0].X, checked (pointArray1[0].Y - 10));
        pointArray1[2] = new Point(pointArray1[0].X, checked (pointArray1[0].Y + 10));
      }
      Point[] pointArray2 = pointArray1;
      int index = 0;
      while (index < pointArray2.Length)
      {
        Point location = pointArray2[index];
        location.Offset(-2, -2);
        e.Graphics.FillEllipse(SystemBrushes.ControlDark, new Rectangle(location, new Size(3, 3)));
        location.Offset(1, 1);
        e.Graphics.FillEllipse(SystemBrushes.ControlLight, new Rectangle(location, new Size(3, 3)));
        checked { ++index; }
      }
    }

    public frmMain()
    {
      this.FormClosing += new FormClosingEventHandler(this.frmMain_FormClosing);
      this.Load += new EventHandler(this.frmMain_Load);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.frmMain_DragDrop);
      this.DragEnter += new System.Windows.Forms.DragEventHandler(this.frmMain_DragEnter);
      this._packageFileName = "";
      this._InventoryClass = new InventoryClass?();
      this.m_bNonNumberEntered = false;
      this.bUpdatingRow = false;
      this.bInitializeRow = false;
      this.m_bLoading = false;
      this.InitializeComponent();
      mdUtility.ScaleFactor = this.CreateGraphics().DpiX / 96f;
      this.tsToolbar.ImageScalingSize = new Size(checked ((int) Math.Round(unchecked (16.0 * (double) mdUtility.ScaleFactor))), checked ((int) Math.Round(unchecked (16.0 * (double) mdUtility.ScaleFactor))));
      UltraTree tvInventory = this.tvInventory;
      Size expansionIndicatorSize = this.tvInventory.ExpansionIndicatorSize;
      int width = checked ((int) Math.Round(unchecked ((double) expansionIndicatorSize.Width * (double) mdUtility.ScaleFactor)));
      expansionIndicatorSize = this.tvInventory.ExpansionIndicatorSize;
      int height = checked ((int) Math.Round(unchecked ((double) expansionIndicatorSize.Height * (double) mdUtility.ScaleFactor)));
      Size size = new Size(width, height);
      tvInventory.ExpansionIndicatorSize = size;
      this.tvInspection.ExpansionIndicatorSize = this.tvInventory.ExpansionIndicatorSize;
      this.tvFunctionality.ExpansionIndicatorSize = this.tvInventory.ExpansionIndicatorSize;
      this.cmdNewDetail.Image = (Image) new Bitmap((Image) BuilderRED.My.Resources.Resources.Symbol_Add_2, new Size(checked ((int) Math.Round(unchecked ((double) BuilderRED.My.Resources.Resources.Symbol_Add_2.Width * (double) mdUtility.ScaleFactor))), checked ((int) Math.Round(unchecked ((double) BuilderRED.My.Resources.Resources.Symbol_Add_2.Height * (double) mdUtility.ScaleFactor)))));
      this.rgDetails.TableElement.GroupIndent = checked ((int) Math.Round(unchecked ((double) this.rgDetails.TableElement.GroupIndent * (double) mdUtility.ScaleFactor)));
      this.imgExpand = new Bitmap((Image) BuilderRED.My.Resources.Resources.Collapse_Down, new Size(checked ((int) Math.Round(unchecked ((double) BuilderRED.My.Resources.Resources.Collapse_Down.Width * (double) mdUtility.ScaleFactor))), checked ((int) Math.Round(unchecked ((double) BuilderRED.My.Resources.Resources.Collapse_Down.Height * (double) mdUtility.ScaleFactor)))));
      this.imgCollapse = new Bitmap((Image) BuilderRED.My.Resources.Resources.Collapse_Up, new Size(checked ((int) Math.Round(unchecked ((double) BuilderRED.My.Resources.Resources.Collapse_Up.Width * (double) mdUtility.ScaleFactor))), checked ((int) Math.Round(unchecked ((double) BuilderRED.My.Resources.Resources.Collapse_Up.Height * (double) mdUtility.ScaleFactor)))));
      this.cmdIncrease.Size = new Size(checked ((int) Math.Round(unchecked ((double) this.cmdIncrease.Width * (double) mdUtility.ScaleFactor))), checked ((int) Math.Round(unchecked ((double) this.cmdIncrease.Height * (double) mdUtility.ScaleFactor))));
      this.cmdDecrease.Size = this.cmdIncrease.Size;
      this.cmdCalc.Size = this.cmdIncrease.Size;
      this.cmdNewInspection.Size = this.cmdIncrease.Size;
      this.cmdDeleteInspection.Size = this.cmdIncrease.Size;
      this.cmdCopyInspection.Size = this.cmdIncrease.Size;
      this.cmdNewSample.Size = this.cmdIncrease.Size;
      this.cmdDeleteSample.Size = this.cmdIncrease.Size;
      this.cmdEditSample.Size = this.cmdIncrease.Size;
      this.cmdSampComment.Size = this.cmdIncrease.Size;
    }

    private void frmMain_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
    {
      if (this._siteID == new Guid() || (this._SelectedID == new Guid() || !this._InventoryClass.HasValue || !e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop)))
        return;
      string[] data = (string[]) e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop);
      frmImages frmImages = new frmImages(this._siteID, this._SelectedID, this._InventoryClass.Value, this._packageFileName);
      frmImages.LoadPendingFiles(data);
      int num = (int) frmImages.ShowDialog();
    }

    public void OpenFrmImagesSectionDetail(Guid selectedID)
    {
      int num = (int) new frmImages(this._siteID, selectedID, InventoryClass.SectionDetail, this._packageFileName).ShowDialog();
      this.SetInventoryClassForImages(InventoryClass.SectionDetail, selectedID.ToString());
    }

    private void OpenFrmImages()
    {
      int num = (int) new frmImages(this._siteID, this._SelectedID, this._InventoryClass.Value, this._packageFileName).ShowDialog();
      this.SetInventoryClassForImages(this._InventoryClass.Value, this._SelectedID.ToString());
    }

    private void frmMain_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
    {
      if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
        e.Effect = System.Windows.Forms.DragDropEffects.Copy;
      else
        e.Effect = System.Windows.Forms.DragDropEffects.None;
    }

    public RadGridView DetailsGrid
    {
      get
      {
        return this.rgDetails;
      }
    }

    public CommandBarStripElement CommandBar
    {
      get
      {
        return this.CommandBarStripElement1;
      }
    }

    public RadPageView SectionPageView
    {
      get
      {
        return this.tbSection;
      }
    }

    public RadPageViewPage SectionPageViewPage
    {
      get
      {
        return this.tpSection;
      }
    }

    public RadPageViewPage DetailsPageViewPage
    {
      get
      {
        return this.tpDetails;
      }
    }

    public CommandBarButton NewDetailButton
    {
      get
      {
        return this.cmdNewDetail;
      }
    }

    private void miInspectionMode_Click(object sender, EventArgs e)
    {
      this.Mode = frmMain.ProgramMode.pmInspection;
    }

    private void miFunctionalityMode_Click(object sender, EventArgs e)
    {
      this.Mode = frmMain.ProgramMode.pmFunctionality;
    }

    private void tsToolbar_Click(object sender, EventArgs e)
    {
      ToolStripItem toolStripItem = (ToolStripItem) sender;
      try
      {
        if (!(Microsoft.VisualBasic.Strings.InStr(toolStripItem.Text, "Save", CompareMethod.Text) > 0 | Microsoft.VisualBasic.Strings.InStr(toolStripItem.Text, "Cancel", CompareMethod.Binary) > 0) && !this.DataHasBeenSaved())
          return;
        string name1 = toolStripItem.Name;
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name1, this.InventoryToolStripMenuItem.Name, false) == 0 || Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name1, this.InspectionsToolStripMenuItem.Name, false) == 0 || Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name1, this.FunctionalityToolStripMenuItem.Name, false) == 0)
        {
          if (this.Mode == frmMain.ProgramMode.pmFunctionality)
          {
            this._FuncAreaNeedToSave = this.CheckFuncAreaForSave();
            if (this._FuncAreaNeedToSave)
              return;
          }
          string name2 = toolStripItem.Name;
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name2, this.InventoryToolStripMenuItem.Name, false) == 0)
            this._ToggleView = 0;
          else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name2, this.InspectionsToolStripMenuItem.Name, false) == 0)
            this._ToggleView = 1;
          else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name2, this.FunctionalityToolStripMenuItem.Name, false) == 0)
            this._ToggleView = 2;
          this.ToggleMode();
        }
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name1, this.tsbTally.Name, false) == 0)
        {
          Form form = (Form) new frmInspectionTally();
          int num = (int) form.ShowDialog();
          form.Dispose();
        }
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name1, this.tsbNew1.Name, false) == 0)
        {
          string text = toolStripItem.Text;
          // ISSUE: reference to a compiler-generated method
          switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(text))
          {
            case 626355024:
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Inspect Component", false) == 0)
              {
                this.AddInspComponent();
                break;
              }
              break;
            case 2155832539:
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Add Section", false) == 0)
              {
                Form form = (Form) new frmNewSection();
                frmNewSection frmNewSection = (frmNewSection) form;
                string currentComp = this.CurrentComp;
                ref string local = ref currentComp;
                frmNewSection.AddSection(ref local);
                form.Dispose();
                break;
              }
              break;
            case 2379286709:
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Add System", false) == 0)
              {
                Form form = (Form) new frmNewSystem();
                frmNewSystem frmNewSystem = (frmNewSystem) form;
                string currentBldg = this.CurrentBldg;
                ref string local = ref currentBldg;
                frmNewSystem.AddSystem(ref local);
                form.Dispose();
                break;
              }
              break;
            case 3023603227:
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Add Location", false) == 0)
              {
                this.AddNewLocation();
                break;
              }
              break;
            case 3283007254:
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Add Functional Area", false) == 0)
              {
                Form form = (Form) new frmNewFuncArea();
                form.StartPosition = FormStartPosition.CenterParent;
                ((frmNewFuncArea) form).AddFuncArea(this.miUnits.Checked);
                form.Dispose();
                break;
              }
              break;
            case 3962903387:
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Add Component", false) == 0)
              {
                Form form = (Form) new frmNewComponent();
                frmNewComponent frmNewComponent = (frmNewComponent) form;
                string currentSystem = this.CurrentSystem;
                ref string local = ref currentSystem;
                frmNewComponent.AddComponent(ref local);
                form.Dispose();
                break;
              }
              break;
            case 4002511836:
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Add Building", false) == 0)
              {
                Form form = (Form) new frmNewBuilding();
                ((frmNewBuilding) form).AddBuilding();
                form.Dispose();
                break;
              }
              break;
            case 4247094780:
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Inspect Section", false) == 0)
              {
                this.AddInspSection();
                break;
              }
              break;
          }
        }
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name1, this.tsbNew2.Name, false) == 0)
        {
          string text = toolStripItem.Text;
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Add System", false) != 0)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Add Component", false) != 0)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Add Section", false) != 0)
              {
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Inspect Component", false) != 0)
                {
                  if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Inspect Section", false) != 0)
                  {
                    if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Add New Assessment", false) == 0)
                      mdRightFormHandler.SetCurrentItem("", "FuncAssessment", "");
                  }
                  else
                    this.AddInspSection();
                }
                else
                  this.AddInspComponent();
              }
              else
              {
                Form form = (Form) new frmNewSection();
                frmNewSection frmNewSection = (frmNewSection) form;
                string currentComp = this.CurrentComp;
                ref string local = ref currentComp;
                frmNewSection.AddSection(ref local);
                form.Dispose();
              }
            }
            else
            {
              Form form = (Form) new frmNewComponent();
              frmNewComponent frmNewComponent = (frmNewComponent) form;
              string currentSystem = this.CurrentSystem;
              ref string local = ref currentSystem;
              frmNewComponent.AddComponent(ref local);
              form.Dispose();
            }
          }
          else
          {
            Form form = (Form) new frmNewSystem();
            frmNewSystem frmNewSystem = (frmNewSystem) form;
            string currentBldg = this.CurrentBldg;
            ref string local = ref currentBldg;
            frmNewSystem.AddSystem(ref local);
            form.Dispose();
          }
        }
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name1, this.tsbDelete.Name, false) == 0)
        {
          string text = toolStripItem.Text;
          if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Delete Building", false) != 0)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Delete System", false) != 0)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Delete Component", false) != 0)
              {
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Delete Section", false) != 0)
                {
                  if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Delete Location", false) != 0)
                  {
                    if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Delete Functional Area", false) == 0)
                      this.DeleteFuncArea();
                  }
                  else
                    this.DeleteLocation();
                }
                else
                  this.DeleteSection();
              }
              else
                this.DeleteComponent();
            }
            else
              this.DeleteSystem();
          }
          else
            this.DeleteBuilding();
        }
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name1, this.tsbEdit.Name, false) == 0)
        {
          if (this.tvInventory.Visible)
          {
            string Left = this.tvInventory.ActiveNode.Tag.ToString();
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Building", false) != 0)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Section", false) == 0)
                Section.LockSection(false);
            }
            else
              Building.LockBuilding(false);
            this.tsbEdit.Enabled = false;
            this.tsbNew1.Enabled = false;
            this.tsbNew2.Enabled = false;
            this.tsbDelete.Enabled = false;
            this.tsbSave.Enabled = true;
            this.tsbCancel.Enabled = true;
          }
          else
            this.EditLocation();
        }
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name1, this.tsbSave.Name, false) == 0)
        {
          this.Cursor = Cursors.WaitCursor;
          bool flag;
          if (this.tvInventory.Visible)
          {
            string Left = this.tvInventory.ActiveNode.Tag.ToString();
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Building", false) != 0)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Section", false) == 0)
              {
                if (this.m_bSectionYearChanged)
                  this.txtSectionYearBuilt_Leave((object) this.txtSectionYearBuilt, new EventArgs());
                flag = Section.SaveSection(this.CurrentSection);
              }
            }
            else
              flag = Building.SaveBuilding(this.CurrentBldg);
          }
          else
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.tvInspection.ActiveNode.Tag.ToString(), "Building", false) == 0)
              ;
            flag = this.SaveInspection();
            if (Versioned.IsNumeric((object) this.txtSQuantity.Text))
              this.lblPCInspValue.Text = Microsoft.VisualBasic.Strings.Format((object) mdUtility.GetPercent(this.CurrentInspection, this.CurrentSample, Conversions.ToDouble(this.txtSQuantity.Text)), "0.00%");
          }
          if (flag)
          {
            this.tsbNew1.Enabled = true;
            this.tsbNew2.Enabled = true;
            this.tsbDelete.Enabled = true;
            this.tsbSave.Enabled = false;
            this.tsbCancel.Enabled = false;
            this.tsbEdit.Enabled = false;
          }
          this.Cursor = Cursors.Default;
        }
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name1, this.tsbCancel.Name, false) == 0)
        {
          this.Cursor = Cursors.WaitCursor;
          if (this.Mode == frmMain.ProgramMode.pmInventory)
          {
            this.tvInventory.Focus();
            string Left = this.tvInventory.ActiveNode.Tag.ToString();
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Building", false) != 0)
            {
              if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "Section", false) == 0)
              {
                UltraTreeNode nodeByKey = this.tvInventory.GetNodeByKey(this.CurrentSection);
                this.SelectNewActiveInventoryNode(ref nodeByKey);
              }
            }
            else
            {
              UltraTreeNode node = this.tvInventory.Nodes[this.CurrentBldg];
              this.SelectNewActiveInventoryNode(ref node);
            }
          }
          else if (this.Mode == frmMain.ProgramMode.pmInspection)
          {
            bool MustSave = false;
            if (this.CheckInspForSave(ref MustSave))
            {
              UltraTree tvInspection;
              UltraTreeNode activeNode = (tvInspection = this.tvInspection).ActiveNode;
              this.SelectNewActiveInspectionNode(ref activeNode);
              tvInspection.ActiveNode = activeNode;
            }
          }
          else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Conversions.ToString(this.tvFunctionality.ActiveNode.Tag), "Building", false) == 0 && this.pnlFuncAssessment.Visible)
          {
            this.pnlFunctionality.Visible = true;
            this.pnlFunctionality.BringToFront();
            UltraTreeNode node = this.tvFunctionality.Nodes[this.CurrentBldg];
            this.SelectNewActiveFunctionalityNode(ref node);
          }
          this.Cursor = Cursors.Default;
        }
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name1, this.tsbComment.Name, false) == 0)
        {
          if (this.pnlBuildingInfo.Visible)
            this.BldgComments();
          else if (this.pnlSectionInfo.Visible & this.tvInventory.Visible)
            this.SecComments();
          else if (this.pnlInspectionInfo.Visible & this.tvInspection.Visible)
            this.InspComments();
        }
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name1, this.tsbImages.Name, false) == 0)
          this.OpenFrmImages();
        else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(name1, this.tsbWorkItems.Name, false) == 0)
        {
          mdUtility.fWorkItems.Visible = true;
          mdUtility.fWorkItems.BringToFront();
        }
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, "tsToolBar_ButtonClick");
        ProjectData.ClearProjectError();
      }
    }

    private void ugSubcomponents_InitializeLayout(object sender, InitializeLayoutEventArgs e)
    {
    }

    private void frmDistressSurvey_Enter(object sender, EventArgs e)
    {
    }

    private void btnDoesNotContain_Click(object sender, EventArgs e)
    {
      frmDoesNotContain frmDoesNotContain = new frmDoesNotContain();
      frmDoesNotContain.StartPosition = FormStartPosition.CenterScreen;
      frmDoesNotContain.DesktopLocation = this.btnDoesNotContain.Location;
      frmDoesNotContain.Show();
    }

    public enum ProgramMode
    {
      pmInventory,
      pmInspection,
      pmFunctionality,
    }

    public class MyClient : WebClient
    {
      private bool m_HeadOnly;

      public bool HeadOnly
      {
        get
        {
          return this.m_HeadOnly;
        }
        set
        {
          this.m_HeadOnly = value;
        }
      }

      protected override WebRequest GetWebRequest(Uri address)
      {
        WebRequest webRequest = base.GetWebRequest(address);
        if (this.HeadOnly && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(webRequest.Method, "GET", false) == 0)
          webRequest.Method = "HEAD";
        return webRequest;
      }
    }
  }
}
