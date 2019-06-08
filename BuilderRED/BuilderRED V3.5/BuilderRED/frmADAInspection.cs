// Decompiled with JetBrains decompiler
// Type: BuilderRED.frmADAInspection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace BuilderRED
{
  [DesignerGenerated]
  public class frmADAInspection : Form
  {
    private IContainer components;
    private int m_NumQuestions;
    private string m_bldg_ID;
    private string m_sAssessmentSQL;
    private DataTable m_dtAssessment;
    private DataRow m_drAssessment;
    private string m_sAssessment_ID;
    private string m_sAttributesSQL;
    private DataTable m_dtAttributes;
    private string[] m_aQuestions;
    private string m_sChecklist1;
    private string m_sChecklist2;
    private string m_sChecklist3;
    private string m_sChecklist4;
    private string m_sChecklist5;
    private string m_sChecklist6;
    private string m_sChecklist7;
    private string m_sChecklist8;
    private string m_sChecklist9;
    private string m_sChecklist10;
    private string m_sChecklist11;
    private string m_sChecklist12;
    private string m_sChecklist13;
    private string m_sChecklist14;
    private string m_sChecklist15;
    private string m_sChecklist16;
    private string m_sChecklist17;
    private string m_sChecklist18;
    private string m_sChecklist19;
    private string m_sChecklist20;
    private string m_sChecklist21;
    private string m_sChecklist22;
    private string m_sChecklist23;
    private string[] m_aChecklists;

    public frmADAInspection()
    {
      this.Load += new EventHandler(this.frmADAInspection_Load);
      this.m_NumQuestions = 23;
      this.m_bldg_ID = mdUtility.fMainForm.CurrentBldg;
      this.m_sAssessmentSQL = "SELECT * FROM ADA_Assessment WHERE [Building_ID]='" + this.m_bldg_ID + "'";
      this.m_dtAssessment = mdUtility.DB.GetDataTable(this.m_sAssessmentSQL);
      this.m_drAssessment = this.getAssessmentDataRow(ref this.m_dtAssessment);
      this.m_sAssessment_ID = Conversions.ToString(this.m_drAssessment["ADA_Assessment_ID"]);
      this.m_sAttributesSQL = "SELECT * FROM ADA_Attributes WHERE [ADA_Assessment_ID]='" + this.m_sAssessment_ID + "'";
      this.m_dtAttributes = this.getAttributesDataTable(ref this.m_drAssessment);
      this.m_aQuestions = new string[23]
      {
        "Question 1 - Is the route of travel from the site to the building (or functional area) ADA compliant?",
        "Question 2 - Is the parking and/or drop-off area ADA compliant?",
        "Question 3 - Is there an ADA compliant entrance?",
        "Question 4 - Are all doors leading into necessary spaces ADA compliant?",
        "Question 5 - Is horizontal circulation in the building (or functional area) ADA compliant?",
        "Question 6 - Are all necessary rooms and spaces ADA compliant?",
        "Question 7 - Are assembly areas ADA compliant?",
        "Question 8 - Are areas of rescue assistance ADA compliant?",
        "Question 9 - Is vertical circulation ADA compliant?",
        "Question 10 - Are ramps ADA compliant?",
        "Question 11 - Are stairs ADA compliant?",
        "Question 12 - Are elevators ADA compliant?",
        "Question 13 - Are lifts ADA compliant?",
        "Question 14 - Are drinking fountains ADA compliant?",
        "Question 15 - Are restrooms ADA compliant?",
        "Question 16 - Are bathing facilities and shower rooms ADA compliant?",
        "Question 17 - Is signage ADA compliant?",
        "Question 18 - Are fixed cabinets, shelves, and drawers ADA compliant?",
        "Question 19 - Are fixed seats, tables, and counters ADA compliant?",
        "Question 20 - Are the necessary controls ADA compliant?",
        "Question 21 - Are emergency egress alarm systems ADA compliant?",
        "Question 22 - Are washing machines and clothes dryers ADA compliant?",
        "Question 23 - Are wheelchair accessible telephones ADA compliant?"
      };
      this.m_sChecklist1 = "<html>*Is at least one accessible route of travel provided from the site to an accessible entrance into the building (or functional area)?\r\n\r\n*Is the route of travel free of stairs?\r\n\r\n*Is the route of travel stable, firm, and slip-resistant?\r\n\r\n*Is the route of travel at least 36-in wide?\r\n\r\n*Do all obstacles and protrusions between 27-in and 80-in above the ground protrude less than 4-in into the route of travel (obstacles and protrusions less than 27-in above the ground can protrude any amount into the route of travel because they are cane-detectable)?\r\n\r\n*Do curbs on the route of travel have curb cuts at least 36-in wide at drives, parking, and sidewalks?\r\n\r\n*Are landings provided at the tops of curb ramps?</html>";
      this.m_sChecklist2 = "<html>*Are the accessible parking spaces and drop-off areas closest to the accessible entrance and along an accessible route of travel?\r\n\r\n*Are there an adequate number of accessible parking spaces?\r\n\r\n*Are accessible parking spaces a minimum of 8-ft wide and have a 5-ft wide aisle?\r\n\r\n*Are there an adequate number of van accessible spaces?\r\n\r\n*Are the parking space access aisles part of the accessible route of travel?\r\n\r\n*Is there signage, which meet the requirements of Question 17, marking each accessible parking space?\r\n\r\n*Are parking spaces level?</html>";
      this.m_sChecklist3 = "<html>*Is at least one accessible entrance provided into the building (or functional area)?\r\n\r\n*Is there signage, which meet the requirements of Question 17, at inaccessible entrances that give directions to accessible entrances?</html>";
      this.m_sChecklist4 = "<html>*Do doors have at least a 32-in clear opening?\r\n\r\n*Is at least 48-in of clear space, outside of door swing, provided between two doors in a series?\r\n\r\n*Are operable parts (handles, key card readers, etc.) at least 34-in but no more than 48-in from the finish of the floor or ground?\r\n\r\n*Are automatic door controls mounted outside the swing of the door?\r\n\r\n*Is there an ADA compliant door next to a revolving door, gate, or turnstile?</html>";
      this.m_sChecklist5 = "<html>*Are all necessary spaces in the building (or functional area) on an accessible route that is at least 36-in wide?\r\n\r\n*Is the slope of the circulation route no greater than 1:20?\r\n\r\n*Is at least 80-in of clear headroom provided along the circulation route?</html>";
      this.m_sChecklist6 = "<html>*Are aisles and pathways to materials and services within the room/space at least 36-in wide?</html>";
      this.m_sChecklist7 = "<html>*Are there an adequate number of wheelchair spaces given the number of seats (see table)?\r\n\r\nNumber of Seats (Min Number of Required Wheelchair Spaces)\r\n\r\n4 to 25 (1)\r\n\r\n26 to 50 (2)\r\n\r\n51 to 150 (4)\r\n\r\n151  to 300 (5)\r\n\r\n301  to 500 (6)\r\n\r\n501  to 5000 (6)\r\n\r\n5001 and over (6, plus 1 for each 150 or fraction thereof)\r\n\r\n*Is at least one companion fixed seat provided next to each wheelchair seating area?\r\n\r\n*Is the floor at the wheelchair location level, stable, and firm?\r\n\r\n*Do wheelchair spaces offer spectators with choices of seating locations and viewing angles?</html>";
      this.m_sChecklist8 = "<html>*Is there audible and visible two-way communication between the area of rescue assistance and the primary entry or another location approved by the fire department?\r\n\r\n*Is there signage, which meets the requirements of Question 17 and is illuminated when necessary, identifying areas of rescue?\r\n\r\n*Is there signage, which meets the requirements of Question 17, at inaccessible exits that give directions to areas of rescue assistance?</html>";
      this.m_sChecklist9 = "<html>*Are there ramps, lifts, or elevators to all necessary levels?\r\n\r\n*If there are stairs between the entrance and/or elevator and required areas on any level, is there an alternate accessible route?</html>";
      this.m_sChecklist10 = "<html>*Is the slope of the ramp surface no greater than 1:12?\r\n\r\n*Is the ramp surface stable, firm, and slip-resistant?\r\n\r\n*Do ramp runs with a rise greater than 6-in have handrails?\r\n\r\n*Is the width between the handrails and/or curbs of the ramp at least 36-in?\r\n\r\n*Is there a 5-ft landing for every 30-ft horizontal length of ramp, at the top of the ramp, at the bottom of the ramp, and at all switchbacks (for changes in direction the landing is 5-ft square)?\r\n\r\n*Do outdoor ramps prevent water from accumulating on the walking surface?";
      this.m_sChecklist11 = "<html>*Are stair tread and riser dimensions constant?\r\n\r\n*Risers shall be 4-in high min, 7-in high max\r\n\r\n*Are stair treads sturdy and non-slip?\r\n\r\n*Do outdoor stairs prevent water from accumulating on the treads?</html>";
      this.m_sChecklist12 = "<html>*Are call buttons in the elevator no higher than 48-in above the floor for side approach or 48-in above the floor for forward approach?\r\n\r\n*Is the call button that designates the up direction located above the button that designates the down direction?\r\n\r\n*Do call buttons should have visible signals to indicate when each call is registered and when each call is answered?\r\n\r\n*Is the emergency intercom, if provided, no higher than 48-in above the floor, usable without voice communication, and identified by signage complying with Question 17?\r\n\r\n*Is there signage on both door jams at every floor, which meets the requirements of Question 17, identifying the floor?\r\n\r\n*Are visible and audible door opening, closing, and floor indicators present in the elevator and in the hallway outside the elevator?\r\n\r\n*Do elevators have a self-leveling feature that automatically brings the car to floor landings within a tolerance of \x00BD-in?\r\n\r\n*Is elevator illumination in controls, platform, car threshold, and car landing sill 5 foot candles or greater?\r\n\r\n*Are elevator doors horizontal sliding type?\r\n\r\n*Do elevator hoistway and car doors open and close automatically?\r\n\r\n*Do elevators have a protective and reopening device that will stop and reopen a car door if it becomes obstructed (this device senses obstruction passing through the opening at 5 inches and 29 inches, does not require physical contact to be activated, remains effective for 20 seconds minimum)?</html>";
      this.m_sChecklist13 = "<html>*Can the lift be used without assistance or is a call button provided?\r\n\r\n*Is at least 30-in by 48-in clear space provided to approach the lift, to reach the controls, and use the lift?\r\n\r\n*Do doors remain open for 20 seconds minimum? Do doors have appropriate clear space (32 inches for end gates/doors, 42 inches side gates/doors)?</html>";
      this.m_sChecklist14 = "<html>*Is there at least one accessible drinking fountain per floor?\r\n\r\n*Is at least 30-in by 48-in clear floor space provided in front of accessible drinking fountains?</html>";
      this.m_sChecklist15 = "<html>*Is there signage, which meets the requirements of Question 17, identifying accessible restrooms?\r\n\r\n*Is there signage, which meets the requirements of Question 17, at inaccessible restrooms that give directions to accessible ones?\r\n\r\n*Is there a 36-in wide path to all accessible fixtures and stalls?\r\n\r\n*Does the door(s) into the restroom meet the requirements of Question 4?\r\n\r\n*Is there a 5-ft circle or T-shaped space along the circulation route for a person using a wheelchair to reverse direction?\r\n\r\n*Is there an ADA compliant stall?\r\n<ul><li>Does the stall door meet the requirements of Question 4?</li>\r\n<li>Are grab bars with a diameter of 1-\x00BC to 2-in  located behind and on the sidewall nearest the fixture?</li></ul>\r\n\r\n*Is there an ADA compliant lavatory?\r\n<ul><li>Is there at least 29-in from the floor to the bottom of the lavatory apron, excluding pipes?</li>\r\n<li>Are hot water and drain pipes under lavatories insulated or configured to protect against contact?</li></ul>\r\n\r\n*If urinals are provided, is at least one ADA compliant?\r\n<ul><li>Is the urinal wall-hung with an elongated rim no less than 13 \x00BD inches from the wall and no greater than 17-in above the floor?</li></ul></html>";
      this.m_sChecklist16 = "<html>*Is there at least one accessible bathing facility or shower room for each sex or one accessible unisex bathing facility or shower room?\r\n\r\n*Is there signage, which meets the requirements of Question 17, identifying accessible bathing facilities and shower rooms?\r\n\r\n*Is there signage, which meets the requirements of Question 17, at inaccessible bathing facilities and shower rooms that give directions to accessible ones?\r\n\r\n*If bathtubs are provided, is at least one ADA accessible?\r\n<ul><li>Is at least 30-in by 60-in clear floor space, which can include one accessible lavatory, provided in front of the bathtub?</li>\r\n<li>Is a structurally sound, securely mounted, and slip-resistant seat provided at the head end of the bathtub?</li>\r\n<li>Is the bathtub enclosure installed so it avoids obstructing the controls or transfer from wheelchairs onto bathtub seats or into bathtubs?</li>\r\n<li>Are bathtub enclosure tracks not mounted on bathtub rims?</li></ul>\r\n\r\n*If shower stalls are provided, is at least one ADA accessible?\r\n<ul><li>Are shower stalls a minimum of 36-in by 36-in (transfer type) or 30-in by 60-in(roll-in type)?</li>\r\n<li>Is at least 36-in by 48-in clear floor space provided for 36-in by 36-in shower stalls(transfer type)?</li>\r\n<li>Is the shower stall enclosure installed so it avoids obstructing the controls or transfer from wheelchairs onto shower seats?</li></ul></html>";
      this.m_sChecklist17 = "<html>*Is the symbol of accessibility shown on accessible elements?\r\n\r\n*Braille Dimensions\r\n<ul><li>Is dot base diameter between .059-in and .063-in?</li>\r\n<li>Is the distance between two dots in the same cell between .090-in and .100-in?</li>\r\n<li>Is the distance between corresponding dots in adjacent cells between .241-in and .300-in?</li>\r\n<li>Is the dot height between .025-in and .037-in?</li>\r\n<li>Is the distance between corresponding dots from one cell directly below between .395-in and .400-in?</li></ul>\r\n\r\n*Does signage have adequately sized characters according to the viewing distance from which they are to be read?\r\n\r\n*Does signage have a non-glare background with high-contrast characters?\r\n\r\n*Does signage with pictograms have raised braille characters accompanying it?\r\n\r\n*Is signage mounted with centerline between 48-in and 60-in from the floor?\r\n\r\n*Is permanent room identification signage mounted on the wall adjacent to the latch side of the door or as close as possible?</html>";
      this.m_sChecklist18 = "<html>*Is at least 30-in by 48-in clear space provided in front of cabinets, shelves, and drawers?\r\n\r\n*Are cabinets, shelves, and drawers located 15-in to 48-in above the floor for side approach or 15-in to 48-in above the floor for front approach?\r\n\r\n*Is hardware on cabinets, shelves, and drawers operable with a closed fist with a force no greater than 5 lbf?</html>";
      this.m_sChecklist19 = "<html>*Is at least 27-in high, 30-in wide, and 19-in deep knee space provided at each seating space?\r\n\r\n*Is at least 30-in by 48-in clear floor space, overlapping the knee space by no greater than 19 in, provided at all accessible seating spaces?\r\n\r\n*Are aisles between fixed seating, tables, and counters at least 36-in wide?\r\n\r\n*Are the tops of tables and counters 28-in to 34-in above the floor?</html>";
      this.m_sChecklist20 = "<html>*Is at least 30-in by 48-in clear floor space provided in front of the controls?</html>";
      this.m_sChecklist21 = "<html>*Does the emergency egress alarm system have visual signals in restrooms and general usage areas?\r\n\r\n*Does the emergency egress alarm system have audible signals in restrooms and general usage areas</html>?";
      this.m_sChecklist22 = "<html>*Is clear space provided? Section 305.\r\n\r\n*Are all operable parts operable with a closed fist with a force no greater than 5-lbs?\r\n\r\n*Is the door of top loading machines no greater than 36-in maximum above the floor finish?\r\n\r\n*Is the door of front loading machines no less than 15-in from the floor finish and no greater than 36-in from floor finish?</html>";
      this.m_sChecklist23 = "<html>*Is at least 30-in by 48-in clear floor space provided in front of the controls?</html>";
      this.m_aChecklists = new string[23]
      {
        this.m_sChecklist1,
        this.m_sChecklist2,
        this.m_sChecklist3,
        this.m_sChecklist4,
        this.m_sChecklist5,
        this.m_sChecklist6,
        this.m_sChecklist7,
        this.m_sChecklist8,
        this.m_sChecklist9,
        this.m_sChecklist10,
        this.m_sChecklist11,
        this.m_sChecklist12,
        this.m_sChecklist13,
        this.m_sChecklist14,
        this.m_sChecklist15,
        this.m_sChecklist16,
        this.m_sChecklist17,
        this.m_sChecklist18,
        this.m_sChecklist19,
        this.m_sChecklist20,
        this.m_sChecklist21,
        this.m_sChecklist22,
        this.m_sChecklist23
      };
      this.InitializeComponent();
    }

    [DebuggerNonUserCode]
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!disposing || this.components == null)
          return;
        this.components.Dispose();
      }
      finally
      {
        base.Dispose(disposing);
      }
    }

    [DebuggerStepThrough]
    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmADAInspection));
      this.lblAccessibility = new RadLabel();
      this.btnSave = new Button();
      this.btnCancel = new Button();
      this.tblChecklist = new TableLayoutPanel();
      this.RadLabel12 = new RadLabel();
      this.RadLabel11 = new RadLabel();
      this.RadLabel2 = new RadLabel();
      this.RadLabel3 = new RadLabel();
      this.tblApplicability = new TableLayoutPanel();
      this.lblApplicability = new RadLabel();
      this.lblApp2 = new RadLabel();
      this.RadLabel6 = new RadLabel();
      this.RadLabel4 = new RadLabel();
      this.RadLabel8 = new RadLabel();
      this.RadLabel9 = new RadLabel();
      this.CtrlPreQuestion1 = new ctrlADARadioButton();
      this.CtrlPreQuestion2 = new ctrlADARadioButton();
      this.pnlScroll = new Panel();
      this.tblQuestions = new TableLayoutPanel();
      this.tblHeader = new TableLayoutPanel();
      this.lblHeader = new RadLabel();
      this.tblOuter = new TableLayoutPanel();
      this.lblAccessibility.BeginInit();
      this.tblChecklist.SuspendLayout();
      this.RadLabel12.BeginInit();
      this.RadLabel11.BeginInit();
      this.RadLabel2.BeginInit();
      this.RadLabel3.BeginInit();
      this.tblApplicability.SuspendLayout();
      this.lblApplicability.BeginInit();
      this.lblApp2.BeginInit();
      this.RadLabel6.BeginInit();
      this.RadLabel4.BeginInit();
      this.RadLabel8.BeginInit();
      this.RadLabel9.BeginInit();
      this.pnlScroll.SuspendLayout();
      this.tblQuestions.SuspendLayout();
      this.tblHeader.SuspendLayout();
      this.lblHeader.BeginInit();
      this.tblOuter.SuspendLayout();
      this.SuspendLayout();
      this.lblAccessibility.Dock = DockStyle.Bottom;
      this.lblAccessibility.Location = new Point(3, 20);
      this.lblAccessibility.Name = "lblAccessibility";
      this.lblAccessibility.Size = new Size(120, 16);
      this.lblAccessibility.TabIndex = 0;
      this.lblAccessibility.Text = "Accessibility Checklist ";
      this.lblAccessibility.TextAlignment = ContentAlignment.BottomLeft;
      this.lblAccessibility.ThemeName = "Windows7";
      this.btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btnSave.Location = new Point(695, 3);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new Size(80, 23);
      this.btnSave.TabIndex = 5;
      this.btnSave.Text = "Save";
      this.btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btnCancel.Location = new Point(695, 32);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(80, 23);
      this.btnCancel.TabIndex = 6;
      this.btnCancel.Text = "Cancel";
      this.tblChecklist.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.tblChecklist.AutoSize = true;
      this.tblChecklist.ColumnCount = 6;
      this.tblChecklist.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.tblChecklist.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75f));
      this.tblChecklist.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75f));
      this.tblChecklist.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75f));
      this.tblChecklist.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50f));
      this.tblChecklist.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50f));
      this.tblChecklist.Controls.Add((Control) this.lblAccessibility, 0, 0);
      this.tblChecklist.Controls.Add((Control) this.RadLabel12, 1, 0);
      this.tblChecklist.Controls.Add((Control) this.RadLabel11, 2, 0);
      this.tblChecklist.Controls.Add((Control) this.RadLabel2, 3, 0);
      this.tblChecklist.Location = new Point(0, 104);
      this.tblChecklist.Margin = new Padding(0);
      this.tblChecklist.Name = "tblChecklist";
      this.tblChecklist.RowCount = 2;
      this.tblChecklist.RowStyles.Add(new RowStyle());
      this.tblChecklist.RowStyles.Add(new RowStyle());
      this.tblChecklist.Size = new Size(775, 39);
      this.tblChecklist.TabIndex = 9;
      this.RadLabel12.AutoSize = false;
      this.RadLabel12.Dock = DockStyle.Bottom;
      this.RadLabel12.Location = new Point(453, 3);
      this.RadLabel12.Name = "RadLabel12";
      this.RadLabel12.Size = new Size(69, 33);
      this.RadLabel12.TabIndex = 4;
      this.RadLabel12.Text = "Compliant";
      this.RadLabel12.TextAlignment = ContentAlignment.BottomCenter;
      this.RadLabel12.ThemeName = "Windows7";
      this.RadLabel11.AutoSize = false;
      this.RadLabel11.Dock = DockStyle.Bottom;
      this.RadLabel11.Location = new Point(528, 3);
      this.RadLabel11.Name = "RadLabel11";
      this.RadLabel11.Size = new Size(69, 33);
      this.RadLabel11.TabIndex = 3;
      this.RadLabel11.Text = "Non-Compliant";
      this.RadLabel11.TextAlignment = ContentAlignment.BottomCenter;
      this.RadLabel11.ThemeName = "Windows7";
      this.RadLabel2.AutoSize = false;
      this.RadLabel2.Dock = DockStyle.Bottom;
      this.RadLabel2.Location = new Point(603, 3);
      this.RadLabel2.Name = "RadLabel2";
      this.RadLabel2.Size = new Size(69, 33);
      this.RadLabel2.TabIndex = 2;
      this.RadLabel2.Text = "N/A";
      this.RadLabel2.TextAlignment = ContentAlignment.BottomCenter;
      this.RadLabel2.ThemeName = "Windows7";
      this.RadLabel3.AutoSize = false;
      this.RadLabel3.Location = new Point(32, 36);
      this.RadLabel3.Name = "RadLabel3";
      this.RadLabel3.Size = new Size(520, 18);
      this.RadLabel3.TabIndex = 1;
      this.RadLabel3.Text = "ADA Checklist Questions";
      this.RadLabel3.ThemeName = "Windows7";
      this.tblApplicability.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.tblApplicability.AutoSize = true;
      this.tblApplicability.ColumnCount = 6;
      this.tblApplicability.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.tblApplicability.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75f));
      this.tblApplicability.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75f));
      this.tblApplicability.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75f));
      this.tblApplicability.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50f));
      this.tblApplicability.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50f));
      this.tblApplicability.Controls.Add((Control) this.lblApplicability, 0, 0);
      this.tblApplicability.Controls.Add((Control) this.lblApp2, 0, 2);
      this.tblApplicability.Controls.Add((Control) this.RadLabel6, 0, 1);
      this.tblApplicability.Controls.Add((Control) this.RadLabel4, 1, 0);
      this.tblApplicability.Controls.Add((Control) this.RadLabel8, 2, 0);
      this.tblApplicability.Controls.Add((Control) this.RadLabel9, 3, 0);
      this.tblApplicability.Controls.Add((Control) this.CtrlPreQuestion1, 1, 1);
      this.tblApplicability.Controls.Add((Control) this.CtrlPreQuestion2, 1, 2);
      this.tblApplicability.Location = new Point(0, 0);
      this.tblApplicability.Margin = new Padding(0);
      this.tblApplicability.Name = "tblApplicability";
      this.tblApplicability.RowCount = 3;
      this.tblApplicability.RowStyles.Add(new RowStyle());
      this.tblApplicability.RowStyles.Add(new RowStyle());
      this.tblApplicability.RowStyles.Add(new RowStyle());
      this.tblApplicability.Size = new Size(775, 104);
      this.tblApplicability.TabIndex = 10;
      this.lblApplicability.AutoSize = false;
      this.lblApplicability.Dock = DockStyle.Bottom;
      this.lblApplicability.Location = new Point(3, 16);
      this.lblApplicability.Name = "lblApplicability";
      this.lblApplicability.Size = new Size(444, 23);
      this.lblApplicability.TabIndex = 8;
      this.lblApplicability.Text = "Applicability";
      this.lblApplicability.TextAlignment = ContentAlignment.BottomLeft;
      this.lblApplicability.ThemeName = "Windows7";
      this.lblApp2.AutoSize = false;
      this.lblApp2.BackColor = Color.White;
      this.lblApp2.Dock = DockStyle.Fill;
      this.lblApp2.Location = new Point(3, 73);
      this.lblApp2.Margin = new Padding(3, 0, 0, 0);
      this.lblApp2.Name = "lblApp2";
      this.lblApp2.Size = new Size(447, 31);
      this.lblApp2.TabIndex = 3;
      this.lblApp2.Text = "Do occupants of this building require ADA compliance?";
      this.lblApp2.ThemeName = "Windows7";
      this.RadLabel6.AutoSize = false;
      this.RadLabel6.Dock = DockStyle.Fill;
      this.RadLabel6.Location = new Point(3, 42);
      this.RadLabel6.Margin = new Padding(3, 0, 0, 0);
      this.RadLabel6.Name = "RadLabel6";
      this.RadLabel6.Size = new Size(447, 31);
      this.RadLabel6.TabIndex = 2;
      this.RadLabel6.Text = "Was this building built after September 15, 2010?";
      this.RadLabel6.ThemeName = "Windows7";
      this.RadLabel4.AutoSize = false;
      this.RadLabel4.Location = new Point(453, 3);
      this.RadLabel4.Name = "RadLabel4";
      this.RadLabel4.Size = new Size(69, 36);
      this.RadLabel4.TabIndex = 1;
      this.RadLabel4.Text = "Yes";
      this.RadLabel4.TextAlignment = ContentAlignment.BottomCenter;
      this.RadLabel4.ThemeName = "Windows7";
      this.RadLabel8.AutoSize = false;
      this.RadLabel8.Location = new Point(528, 3);
      this.RadLabel8.Name = "RadLabel8";
      this.RadLabel8.Size = new Size(69, 36);
      this.RadLabel8.TabIndex = 4;
      this.RadLabel8.Text = "No";
      this.RadLabel8.TextAlignment = ContentAlignment.BottomCenter;
      this.RadLabel8.ThemeName = "Windows7";
      this.RadLabel9.AutoSize = false;
      this.RadLabel9.Location = new Point(603, 3);
      this.RadLabel9.Name = "RadLabel9";
      this.RadLabel9.Size = new Size(69, 36);
      this.RadLabel9.TabIndex = 5;
      this.RadLabel9.Text = "N/A";
      this.RadLabel9.TextAlignment = ContentAlignment.BottomCenter;
      this.RadLabel9.ThemeName = "Windows7";
      this.CtrlPreQuestion1.AutoSize = true;
      this.CtrlPreQuestion1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.CtrlPreQuestion1.BackColor = SystemColors.Control;
      this.CtrlPreQuestion1.Budget = new double?();
      this.tblApplicability.SetColumnSpan((Control) this.CtrlPreQuestion1, 5);
      this.CtrlPreQuestion1.Comment = (string) null;
      this.CtrlPreQuestion1.ControlColor = SystemColors.Control;
      this.CtrlPreQuestion1.Location = new Point(450, 42);
      this.CtrlPreQuestion1.Margin = new Padding(0);
      this.CtrlPreQuestion1.MinimumSize = new Size(243, 29);
      this.CtrlPreQuestion1.Name = "CtrlPreQuestion1";
      this.CtrlPreQuestion1.QuestionNumber = 0;
      this.CtrlPreQuestion1.Size = new Size(324, 31);
      this.CtrlPreQuestion1.Status = new ctrlADARadioButton.StatusRB?();
      this.CtrlPreQuestion1.TabIndex = 9;
      this.CtrlPreQuestion2.AutoSize = true;
      this.CtrlPreQuestion2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.CtrlPreQuestion2.BackColor = SystemColors.Control;
      this.CtrlPreQuestion2.Budget = new double?();
      this.tblApplicability.SetColumnSpan((Control) this.CtrlPreQuestion2, 5);
      this.CtrlPreQuestion2.Comment = (string) null;
      this.CtrlPreQuestion2.ControlColor = SystemColors.Control;
      this.CtrlPreQuestion2.Location = new Point(450, 73);
      this.CtrlPreQuestion2.Margin = new Padding(0);
      this.CtrlPreQuestion2.MinimumSize = new Size(243, 29);
      this.CtrlPreQuestion2.Name = "CtrlPreQuestion2";
      this.CtrlPreQuestion2.QuestionNumber = 0;
      this.CtrlPreQuestion2.Size = new Size(324, 31);
      this.CtrlPreQuestion2.Status = new ctrlADARadioButton.StatusRB?();
      this.CtrlPreQuestion2.TabIndex = 10;
      this.pnlScroll.AutoScroll = true;
      this.pnlScroll.Controls.Add((Control) this.tblQuestions);
      this.pnlScroll.Dock = DockStyle.Fill;
      this.pnlScroll.Location = new Point(3, 92);
      this.pnlScroll.Margin = new Padding(3, 3, 0, 3);
      this.pnlScroll.Name = "pnlScroll";
      this.pnlScroll.Size = new Size(781, 424);
      this.pnlScroll.TabIndex = 11;
      this.tblQuestions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.tblQuestions.AutoSize = true;
      this.tblQuestions.ColumnCount = 1;
      this.tblQuestions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.tblQuestions.Controls.Add((Control) this.tblApplicability, 0, 0);
      this.tblQuestions.Controls.Add((Control) this.tblChecklist, 0, 1);
      this.tblQuestions.Location = new Point(2, 3);
      this.tblQuestions.Name = "tblQuestions";
      this.tblQuestions.RowCount = 2;
      this.tblQuestions.RowStyles.Add(new RowStyle());
      this.tblQuestions.RowStyles.Add(new RowStyle());
      this.tblQuestions.Size = new Size(775, 173);
      this.tblQuestions.TabIndex = 11;
      this.tblHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.tblHeader.AutoSize = true;
      this.tblHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.tblHeader.ColumnCount = 2;
      this.tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.tblHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 86f));
      this.tblHeader.Controls.Add((Control) this.btnSave, 1, 0);
      this.tblHeader.Controls.Add((Control) this.lblHeader, 0, 0);
      this.tblHeader.Controls.Add((Control) this.btnCancel, 1, 1);
      this.tblHeader.Location = new Point(3, 3);
      this.tblHeader.Name = "tblHeader";
      this.tblHeader.RowCount = 2;
      this.tblHeader.RowStyles.Add(new RowStyle());
      this.tblHeader.RowStyles.Add(new RowStyle());
      this.tblHeader.Size = new Size(778, 83);
      this.tblHeader.TabIndex = 13;
      this.lblHeader.Location = new Point(3, 3);
      this.lblHeader.MaximumSize = new Size(400, 0);
      this.lblHeader.Name = "lblHeader";
      this.lblHeader.RootElement.MaxSize = new Size(400, 0);
      this.tblHeader.SetRowSpan((Control) this.lblHeader, 2);
      this.lblHeader.Size = new Size(395, 77);
      this.lblHeader.TabIndex = 7;
      this.lblHeader.Text = componentResourceManager.GetString("lblHeader.Text");
      this.tblOuter.ColumnCount = 1;
      this.tblOuter.ColumnStyles.Add(new ColumnStyle());
      this.tblOuter.Controls.Add((Control) this.tblHeader, 0, 0);
      this.tblOuter.Controls.Add((Control) this.pnlScroll, 0, 1);
      this.tblOuter.Dock = DockStyle.Fill;
      this.tblOuter.Location = new Point(0, 0);
      this.tblOuter.Name = "tblOuter";
      this.tblOuter.RowCount = 2;
      this.tblOuter.RowStyles.Add(new RowStyle());
      this.tblOuter.RowStyles.Add(new RowStyle());
      this.tblOuter.Size = new Size(784, 519);
      this.tblOuter.TabIndex = 14;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(784, 519);
      this.Controls.Add((Control) this.tblOuter);
      this.MinimumSize = new Size(800, 550);
      this.Name = nameof (frmADAInspection);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Accessibility";
      this.TopMost = true;
      this.lblAccessibility.EndInit();
      this.tblChecklist.ResumeLayout(false);
      this.tblChecklist.PerformLayout();
      this.RadLabel12.EndInit();
      this.RadLabel11.EndInit();
      this.RadLabel2.EndInit();
      this.RadLabel3.EndInit();
      this.tblApplicability.ResumeLayout(false);
      this.tblApplicability.PerformLayout();
      this.lblApplicability.EndInit();
      this.lblApp2.EndInit();
      this.RadLabel6.EndInit();
      this.RadLabel4.EndInit();
      this.RadLabel8.EndInit();
      this.RadLabel9.EndInit();
      this.pnlScroll.ResumeLayout(false);
      this.pnlScroll.PerformLayout();
      this.tblQuestions.ResumeLayout(false);
      this.tblQuestions.PerformLayout();
      this.tblHeader.ResumeLayout(false);
      this.tblHeader.PerformLayout();
      this.lblHeader.EndInit();
      this.tblOuter.ResumeLayout(false);
      this.tblOuter.PerformLayout();
      this.ResumeLayout(false);
    }

    internal virtual RadLabel lblAccessibility { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Button btnSave
    {
      get
      {
        return this._btnSave;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnSave_Click);
        Button btnSave1 = this._btnSave;
        if (btnSave1 != null)
          btnSave1.Click -= eventHandler;
        this._btnSave = value;
        Button btnSave2 = this._btnSave;
        if (btnSave2 == null)
          return;
        btnSave2.Click += eventHandler;
      }
    }

    internal virtual Button btnCancel
    {
      get
      {
        return this._btnCancel;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnCancel_Click);
        Button btnCancel1 = this._btnCancel;
        if (btnCancel1 != null)
          btnCancel1.Click -= eventHandler;
        this._btnCancel = value;
        Button btnCancel2 = this._btnCancel;
        if (btnCancel2 == null)
          return;
        btnCancel2.Click += eventHandler;
      }
    }

    internal virtual TableLayoutPanel tblChecklist { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel RadLabel3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tblApplicability { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel lblApp2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel RadLabel6 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel RadLabel4 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel RadLabel8 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel RadLabel9 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel RadLabel12 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel RadLabel11 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel RadLabel2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual Panel pnlScroll { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel lblApplicability { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ctrlADARadioButton CtrlPreQuestion1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual ctrlADARadioButton CtrlPreQuestion2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tblHeader { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual RadLabel lblHeader { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tblQuestions { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual TableLayoutPanel tblOuter { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private void btnSave_Click(object sender, EventArgs e)
    {
      int[] numArray = new int[checked (this.m_NumQuestions - 1 + 1)];
      try
      {
        if (this.m_drAssessment.Table.Columns.Contains("InspectorLink"))
          AssessmentHelpers.SaveInspector(this.m_drAssessment);
        string str1 = ((frmADAInspection.DBQuestionField) this.CtrlPreQuestion1.QuestionNumber).ToString();
        ctrlADARadioButton.StatusRB? status = this.CtrlPreQuestion1.Status;
        if (status.HasValue)
        {
          DataRow drAssessment = this.m_drAssessment;
          string index = str1;
          status = this.CtrlPreQuestion1.Status;
          int num;
          if (status.Value != ctrlADARadioButton.StatusRB.COMPLIANT)
          {
            status = this.CtrlPreQuestion1.Status;
            num = status.Value == ctrlADARadioButton.StatusRB.N_A ? -1 : 0;
          }
          else
            num = 1;
          // ISSUE: variable of a boxed type
          __Boxed<int> local = (System.ValueType) num;
          drAssessment[index] = (object) local;
        }
        string str2 = ((frmADAInspection.DBQuestionField) this.CtrlPreQuestion2.QuestionNumber).ToString();
        status = this.CtrlPreQuestion2.Status;
        if (status.HasValue)
        {
          DataRow drAssessment = this.m_drAssessment;
          string index = str2;
          status = this.CtrlPreQuestion2.Status;
          int num;
          if (status.Value != ctrlADARadioButton.StatusRB.COMPLIANT)
          {
            status = this.CtrlPreQuestion2.Status;
            num = status.Value == ctrlADARadioButton.StatusRB.N_A ? -1 : 0;
          }
          else
            num = 1;
          // ISSUE: variable of a boxed type
          __Boxed<int> local = (System.ValueType) num;
          drAssessment[index] = (object) local;
        }
        int numQuestions = this.m_NumQuestions;
        int row1 = 1;
        while (row1 <= numQuestions)
        {
          ctrlADARadioButton controlFromPosition = (ctrlADARadioButton) this.tblChecklist.GetControlFromPosition(1, row1);
          string str3 = ((frmADAInspection.DBQuestionField) row1).ToString();
          status = controlFromPosition.Status;
          if (status.HasValue)
          {
            DataRow drAssessment = this.m_drAssessment;
            string index = str3;
            status = controlFromPosition.Status;
            // ISSUE: variable of a boxed type
            __Boxed<ctrlADARadioButton.StatusRB> local = (Enum) status.Value;
            drAssessment[index] = (object) local;
          }
          DataRow row2;
          if (((IEnumerable<DataRow>) this.m_dtAttributes.Select("ADA_Assessment_Question = '" + str3 + "'")).Count<DataRow>() > 0)
          {
            row2 = this.m_dtAttributes.Select("ADA_Assessment_Question = '" + str3 + "'")[0];
          }
          else
          {
            row2 = this.m_dtAttributes.NewRow();
            DataRow dataRow = row2;
            dataRow["ADA_Attributes_ID"] = (object) mdUtility.GetUniqueID();
            dataRow["ADA_Assessment_ID"] = RuntimeHelpers.GetObjectValue(this.m_drAssessment["ADA_Assessment_ID"]);
            dataRow["ADA_Assessment_Question"] = (object) str3;
            this.m_dtAttributes.Rows.Add(row2);
          }
          DataRow dataRow1 = row2;
          double? budget = controlFromPosition.Budget;
          bool? nullable1;
          if (budget.HasValue)
          {
            budget = controlFromPosition.Budget;
            nullable1 = budget.HasValue ? new bool?(budget.GetValueOrDefault() == 0.0) : new bool?();
          }
          else
            nullable1 = new bool?(true);
          bool? nullable2 = nullable1;
          nullable2 = !nullable1.HasValue || nullable2.GetValueOrDefault() ? (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(controlFromPosition.Comment, "", false) == 0 ? nullable2 : new bool?(false)) : new bool?(false);
          if (nullable2.GetValueOrDefault())
          {
            row2.Delete();
          }
          else
          {
            budget = controlFromPosition.Budget;
            if (budget.HasValue)
            {
              DataRow dataRow2 = dataRow1;
              budget = controlFromPosition.Budget;
              // ISSUE: variable of a boxed type
              __Boxed<double> local = (System.ValueType) budget.Value;
              dataRow2["ADA_Budget"] = (object) local;
            }
            dataRow1["ADA_Comment"] = (object) controlFromPosition.Comment;
          }
          checked { ++row1; }
        }
        mdUtility.DB.SaveDataTable(ref this.m_dtAssessment, this.m_sAssessmentSQL);
        mdUtility.DB.SaveDataTable(ref this.m_dtAttributes, this.m_sAttributesSQL);
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, "Save Accessibililty");
        ProjectData.ClearProjectError();
      }
      finally
      {
        this.Close();
      }
    }

    private DataRow getAssessmentDataRow(ref DataTable dt)
    {
      if (dt.Rows.Count < 1)
      {
        DataRow row = dt.NewRow();
        DataRow dataRow = row;
        dataRow["Building_ID"] = (object) this.m_bldg_ID;
        dataRow["ADA_Assessment_ID"] = (object) mdUtility.GetUniqueID();
        dt.Rows.Add(row);
        mdUtility.DB.SaveDataTable(ref dt, this.m_sAssessmentSQL);
      }
      return dt.Rows[0];
    }

    private DataTable getAttributesDataTable(ref DataRow assessmentRow)
    {
      return mdUtility.DB.GetDataTable(this.m_sAttributesSQL);
    }

    private void frmADAInspection_Load(object sender, EventArgs e)
    {
      Font font = this.Font;
      this.CtrlPreQuestion1.Status = new ctrlADARadioButton.StatusRB?();
      this.CtrlPreQuestion2.Status = new ctrlADARadioButton.StatusRB?();
      this.CtrlPreQuestion1.HideExtraButtons();
      this.CtrlPreQuestion2.HideExtraButtons();
      this.CtrlPreQuestion1.QuestionNumber = 101;
      this.CtrlPreQuestion2.QuestionNumber = 102;
      try
      {
        this.SuspendLayout();
        string index1 = frmADAInspection.DBQuestionField.ADA_Grandfathered.ToString();
        if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.m_drAssessment[index1])))
          this.CtrlPreQuestion1.Status = new ctrlADARadioButton.StatusRB?(Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(this.m_drAssessment[index1], (object) 1, false) ? ctrlADARadioButton.StatusRB.COMPLIANT : (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(this.m_drAssessment[index1], (object) 0, false) ? ctrlADARadioButton.StatusRB.NON_COMPLIANT : ctrlADARadioButton.StatusRB.N_A));
        frmADAInspection.DBQuestionField dbQuestionField = frmADAInspection.DBQuestionField.ADA_Occupant_Reqmts;
        string index2 = dbQuestionField.ToString();
        if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.m_drAssessment[index2])))
          this.CtrlPreQuestion2.Status = new ctrlADARadioButton.StatusRB?(Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(this.m_drAssessment[index2], (object) 1, false) ? ctrlADARadioButton.StatusRB.COMPLIANT : (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(this.m_drAssessment[index2], (object) 0, false) ? ctrlADARadioButton.StatusRB.NON_COMPLIANT : ctrlADARadioButton.StatusRB.N_A));
        this.CtrlPreQuestion2.ControlColor = Color.White;
        int numQuestions = this.m_NumQuestions;
        int row = 1;
        while (row <= numQuestions)
        {
          ctrlADAQuestions ctrlAdaQuestions = new ctrlADAQuestions();
          ctrlADARadioButton ctrlAdaRadioButton = new ctrlADARadioButton();
          ctrlAdaRadioButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
          ctrlAdaRadioButton.QuestionNumber = row;
          if (row % 2 == 0)
          {
            ctrlAdaQuestions.ControlColor = Color.White;
            ctrlAdaRadioButton.ControlColor = Color.White;
          }
          ctrlAdaQuestions.lblQuestion.Text = this.m_aQuestions[checked (row - 1)];
          ctrlAdaQuestions.lblChecklist.Text = this.m_aChecklists[checked (row - 1)];
          ctrlAdaQuestions.lblChecklist.MaximumSize = new Size(checked (ctrlAdaQuestions.Size.Width - 10), 0);
          ctrlAdaQuestions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
          this.tblChecklist.Controls.Add((Control) ctrlAdaQuestions, 0, row);
          ctrlAdaQuestions.Dock = DockStyle.Fill;
          if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(this.m_drAssessment[index2])))
            ctrlAdaRadioButton.Status = new ctrlADARadioButton.StatusRB?((ctrlADARadioButton.StatusRB) Conversions.ToInteger(this.m_drAssessment[index2]));
          dbQuestionField = (frmADAInspection.DBQuestionField) row;
          index2 = dbQuestionField.ToString();
          if (((IEnumerable<DataRow>) this.m_dtAttributes.Select("ADA_Assessment_Question = '" + index2 + "'")).Count<DataRow>() > 0)
          {
            DataRow dataRow = this.m_dtAttributes.Select("ADA_Assessment_Question = '" + index2 + "'")[0];
            if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataRow["ADA_Budget"])))
              ctrlAdaRadioButton.Budget = new double?(Conversions.ToDouble(dataRow["ADA_Budget"]));
            if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(dataRow["ADA_Comment"])))
              ctrlAdaRadioButton.Comment = Conversions.ToString(dataRow["ADA_Comment"]);
          }
          this.tblChecklist.Controls.Add((Control) ctrlAdaRadioButton, 1, row);
          ctrlAdaRadioButton.Dock = DockStyle.Fill;
          this.tblChecklist.SetColumnSpan((Control) ctrlAdaRadioButton, 5);
          checked { ++row; }
        }
        this.Resize += new EventHandler(this.FormResize);
        this.ResizeHeader();
        this.ResumeLayout();
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        mdUtility.Errorhandler(ex, this.Name, "Load Accessibility Form");
        ProjectData.ClearProjectError();
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void ResizeHeader()
    {
      this.lblHeader.MaximumSize = new Size(checked (this.Width - 300), 0);
    }

    private void FormResize(object sender, EventArgs e)
    {
      this.ResizeHeader();
    }

    public enum DBQuestionField
    {
      ADA_Route = 1,
      ADA_Parking = 2,
      ADA_Entrance = 3,
      ADA_Doors = 4,
      ADA_Horiz_Circulation = 5,
      ADA_Rooms = 6,
      ADA_Assembly = 7,
      ADA_Rescue = 8,
      ADA_Vert_Circulation = 9,
      ADA_Ramps = 10, // 0x0000000A
      ADA_Stairs = 11, // 0x0000000B
      ADA_Elevators = 12, // 0x0000000C
      ADA_Lifts = 13, // 0x0000000D
      ADA_Fountains = 14, // 0x0000000E
      ADA_Restrooms = 15, // 0x0000000F
      ADA_Bathing = 16, // 0x00000010
      ADA_Signage = 17, // 0x00000011
      ADA_Cabinets = 18, // 0x00000012
      ADA_Seats = 19, // 0x00000013
      ADA_Controls = 20, // 0x00000014
      ADA_Egress = 21, // 0x00000015
      ADA_Laundry = 22, // 0x00000016
      ADA_Telephones = 23, // 0x00000017
      ADA_Grandfathered = 101, // 0x00000065
      ADA_Occupant_Reqmts = 102, // 0x00000066
    }
  }
}
