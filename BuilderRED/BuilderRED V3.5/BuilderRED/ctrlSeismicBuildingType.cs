// Decompiled with JetBrains decompiler
// Type: BuilderRED.ctrlSeismicBuildingType
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BuilderRED
{
  [DesignerGenerated]
  public class ctrlSeismicBuildingType : UserControl
  {
    private IContainer components;
    private ArrayList m_aryModifiers;
    private ComboBox cboBuildingType;
    private string m_SelectedBldgType;
    private AssessmentHelpers.LabelWithMargins lblFinalScore;
    private AssessmentHelpers.LabelWithMargins lblBasicScore;
    private AssessmentHelpers.LabelWithMargins lblSoilType;
    private AssessmentHelpers.LabelWithMargins lblDivider;
    private const int NUM_CONTROLS = 13;
    private int m_Seismicity;
    private int m_SoilType;
    private Panel m_pnlCBOHolder;
    private string m_BuildingTypeID;
    private ctrlGroupButtons m_oGroupControl;
    private double m_FinalScore;

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
      this.tblColumn = new TableLayoutPanel();
      this.SuspendLayout();
      this.tblColumn.AutoSize = true;
      this.tblColumn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.tblColumn.ColumnCount = 1;
      this.tblColumn.ColumnStyles.Add(new ColumnStyle());
      this.tblColumn.Location = new Point(0, 0);
      this.tblColumn.Name = "tblColumn";
      this.tblColumn.RowCount = 2;
      this.tblColumn.RowStyles.Add(new RowStyle());
      this.tblColumn.RowStyles.Add(new RowStyle());
      this.tblColumn.Size = new Size(0, 0);
      this.tblColumn.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoSize = true;
      this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.BackColor = SystemColors.Control;
      this.Controls.Add((Control) this.tblColumn);
      this.Name = nameof (ctrlSeismicBuildingType);
      this.Size = new Size(3, 3);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    internal virtual TableLayoutPanel tblColumn { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private virtual Button btnDeleteColumn { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    private virtual AssessmentHelpers.CheckButtonWithValue btnEstimated
    {
      get
      {
        return this._btnEstimated;
      }
      [MethodImpl(MethodImplOptions.Synchronized)] set
      {
        EventHandler eventHandler = new EventHandler(this.btnEstimated_Click);
        AssessmentHelpers.CheckButtonWithValue btnEstimated1 = this._btnEstimated;
        if (btnEstimated1 != null)
          btnEstimated1.Click -= eventHandler;
        this._btnEstimated = value;
        AssessmentHelpers.CheckButtonWithValue btnEstimated2 = this._btnEstimated;
        if (btnEstimated2 == null)
          return;
        btnEstimated2.Click += eventHandler;
      }
    }

    public double FinalScore
    {
      get
      {
        return this.m_FinalScore;
      }
      set
      {
        this.m_FinalScore = value;
        ctrlSeismicBuildingType.FinalScoreChangedEventHandler scoreChangedEvent = this.FinalScoreChangedEvent;
        if (scoreChangedEvent == null)
          return;
        scoreChangedEvent((object) this, this.m_FinalScore);
      }
    }

    public event ctrlSeismicBuildingType.FinalScoreChangedEventHandler FinalScoreChanged;

    public string BuildingTypeID
    {
      get
      {
        return this.m_BuildingTypeID;
      }
      set
      {
        this.m_BuildingTypeID = value;
      }
    }

    public int Seismicity
    {
      set
      {
        try
        {
          this.SuspendLayout();
          this.m_Seismicity = value;
          int selectedIndex = this.cboBuildingType.SelectedIndex;
          this.cboBuildingType.DataSource = (object) mdUtility.DB.GetDataTable("SELECT BuildingTypeID, BuildingTypeName as Display FROM RO_Seismic_BuildingType WHERE SeismicityID = " + Conversions.ToString(this.m_Seismicity));
          this.cboBuildingType.SelectedIndex = selectedIndex;
          this.RefreshAll((object) null, false);
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          ProjectData.ClearProjectError();
        }
        finally
        {
          this.ResumeLayout();
        }
      }
    }

    public int SoilType
    {
      set
      {
        this.m_SoilType = value;
        this.UpdateSoilTypeLabel();
        this.UpdateFinalScore();
      }
    }

    public string SelectedBldgType
    {
      get
      {
        return this.m_SelectedBldgType;
      }
      set
      {
        this.m_SelectedBldgType = value;
      }
    }

    public ArrayList ControlsArray
    {
      get
      {
        return this.m_aryModifiers;
      }
      set
      {
        this.m_aryModifiers = value;
      }
    }

    private ArrayList GetSelectedButtonPositions()
    {
      ArrayList arrayList = new ArrayList();
      try
      {
        foreach (AssessmentHelpers.CheckButtonWithValue aryModifier in this.m_aryModifiers)
        {
          if (aryModifier.Checked)
            arrayList.Add((object) this.m_aryModifiers.IndexOf((object) aryModifier));
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      return arrayList;
    }

    private void SetSelectedButtonPositions(ArrayList Positions)
    {
      try
      {
        foreach (object position in Positions)
          ((CheckBox) this.m_aryModifiers[Conversions.ToInteger(position)]).Checked = true;
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
    }

    private void UpdateSoilTypeLabel()
    {
      int num = checked (this.m_SoilType + 5);
      if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.m_SelectedBldgType, "", false) <= 0U)
        return;
      if (num == 8 || num == 9 || num == 10)
        this.lblSoilType.Text = Conversions.ToString(mdUtility.DB.GetDataTable("SELECT * FROM RO_Seismic_Modifiers WHERE BuildingTypeID = " + this.m_SelectedBldgType + " AND ModifierTypeID = " + Conversions.ToString(num) + " ORDER BY ModifierTypeID").Rows[0]["Modifier"]);
      else
        this.lblSoilType.Text = Conversions.ToString(0);
    }

    public ctrlSeismicBuildingType(string Type, int iSoil = 0, int iSeismicity = 0)
    {
      this.m_aryModifiers = new ArrayList();
      this.cboBuildingType = new ComboBox();
      this.btnDeleteColumn = new Button();
      this.btnEstimated = new AssessmentHelpers.CheckButtonWithValue();
      this.lblFinalScore = new AssessmentHelpers.LabelWithMargins();
      this.lblBasicScore = new AssessmentHelpers.LabelWithMargins();
      this.lblSoilType = new AssessmentHelpers.LabelWithMargins();
      this.lblDivider = new AssessmentHelpers.LabelWithMargins();
      this.m_pnlCBOHolder = new Panel();
      this.m_oGroupControl = new ctrlGroupButtons();
      this.InitializeComponent();
      this.Dock = DockStyle.Left;
      if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Type, "BuildingType", false) > 0U)
      {
        int num = 1;
        do
        {
          AssessmentHelpers.LabelWithMargins labelWithMargins = new AssessmentHelpers.LabelWithMargins();
          labelWithMargins.Size = AssessmentHelpers.GetButtonSize;
          labelWithMargins.TextAlign = ContentAlignment.MiddleRight;
          if (num != 11)
            this.tblColumn.Controls.Add((Control) labelWithMargins);
          else if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Type, "Add", false) > 0U)
            this.tblColumn.Controls.Add((Control) this.lblDivider);
          checked { ++num; }
        }
        while (num <= 13);
        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Type, "Label", false) == 0)
          this.MakeLabelColumn();
      }
      else
      {
        this.m_Seismicity = iSeismicity;
        this.m_SoilType = iSoil;
        this.FormatControls();
      }
      this.lblDivider.Height = 1;
      this.lblDivider.BorderStyle = BorderStyle.Fixed3D;
      this.lblDivider.Font = new Font("", 1f);
      this.m_BuildingTypeID = mdUtility.GetUniqueID();
    }

    private void MakeLabelColumn()
    {
      this.tblColumn.Controls[0].Text = "";
      this.tblColumn.Controls[1].Text = "Building Type";
      this.tblColumn.Controls[2].Text = "Basic Score";
      this.tblColumn.Controls[3].Text = "Mid Rise (4-7 stories)";
      this.tblColumn.Controls[4].Text = "High Rise (>7 Stories)";
      this.tblColumn.Controls[5].Text = "Vertical Irregularity";
      this.tblColumn.Controls[6].Text = "Plan Irregularity";
      this.tblColumn.Controls[7].Text = "Pre-Code";
      this.tblColumn.Controls[8].Text = "Post-Benchmark";
      this.tblColumn.Controls[9].Text = "Soil Type";
      this.tblColumn.Controls[11].Text = "FINAL SCORE";
      this.tblColumn.Controls[12].Text = "Estimated, Subjective,\r\nor unreliable data";
      try
      {
        foreach (Label control in (Control.ControlCollection) this.tblColumn.Controls)
          control.Width = checked ((int) Math.Round(unchecked ((double) control.Width * 1.5)));
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
    }

    private void FormatControls()
    {
      Control[] controlArray1 = new Control[8]{ (Control) this.m_pnlCBOHolder, (Control) this.btnDeleteColumn, (Control) this.btnEstimated, (Control) this.lblBasicScore, (Control) this.lblFinalScore, (Control) this.lblSoilType, (Control) this.lblDivider, (Control) this.cboBuildingType };
      int index1 = 0;
      while (index1 < controlArray1.Length)
      {
        controlArray1[index1].Size = AssessmentHelpers.GetButtonSize;
        checked { ++index1; }
      }
      Control[] controlArray2 = new Control[3]{ (Control) this.lblBasicScore, (Control) this.lblFinalScore, (Control) this.lblSoilType };
      int index2 = 0;
      while (index2 < controlArray2.Length)
      {
        AssessmentHelpers.LabelWithMargins labelWithMargins = (AssessmentHelpers.LabelWithMargins) controlArray2[index2];
        labelWithMargins.Dock = DockStyle.Fill;
        labelWithMargins.TextAlign = ContentAlignment.MiddleCenter;
        checked { ++index2; }
      }
      this.btnDeleteColumn.Image = (Image) BuilderRED.My.Resources.Resources.Symbol_Delete;
      this.btnDeleteColumn.Click += new EventHandler(this.btnDeleteColumn_click);
      this.btnEstimated.Appearance = Appearance.Button;
      this.lblFinalScore.Font = new Font(this.lblFinalScore.Font.FontFamily, this.lblFinalScore.Font.Size, FontStyle.Bold);
      this.tblColumn.RowStyles.Add(new RowStyle(SizeType.AutoSize));
      this.tblColumn.Controls.Add((Control) this.btnDeleteColumn);
      this.m_pnlCBOHolder.Controls.Add((Control) this.NewBuildingType());
      this.tblColumn.Controls.Add((Control) this.m_pnlCBOHolder);
    }

    public ComboBox NewBuildingType()
    {
      this.cboBuildingType.ValueMember = "BuildingTypeID";
      this.cboBuildingType.DisplayMember = "Display";
      this.cboBuildingType.DataSource = (object) mdUtility.DB.GetDataTable("SELECT BuildingTypeID, BuildingTypeName as Display  FROM RO_Seismic_BuildingType WHERE SeismicityID = " + Conversions.ToString(this.m_Seismicity));
      this.cboBuildingType.Refresh();
      this.cboBuildingType.DropDown += (EventHandler) ((a0, a1) => this.cboBuildingType_droppedDown());
      this.cboBuildingType.SelectionChangeCommitted += new EventHandler(this.cboBuildingType_ChangeCommitted);
      return this.cboBuildingType;
    }

    public void LoadColumnFromDB(DataRow dr)
    {
      this.m_BuildingTypeID = Conversions.ToString(dr["SeismicBuildingTypeID"]);
      this.m_SelectedBldgType = Conversions.ToString(dr["BuildingTypeID"]);
      this.cboBuildingType.SelectedValue = (object) this.m_SelectedBldgType;
      string sSQL = "SELECT * FROM Seismic_BuildingTypeModifiers WHERE SeismicBuildingTypeID = '" + this.m_BuildingTypeID + "'";
      this.RefreshAll((object) null, true);
      DataTable dataTable = mdUtility.DB.GetDataTable(sSQL);
      if (dataTable.Rows.Count > 0)
      {
        try
        {
          foreach (DataRow row in dataTable.Rows)
          {
            try
            {
              foreach (AssessmentHelpers.CheckButtonWithValue aryModifier in this.m_aryModifiers)
              {
                if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(row["ModifierID"])) && Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual((object) aryModifier.Value, row["ModifierID"], false))
                  aryModifier.PerformClick();
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
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
      }
      if (Conversions.ToBoolean(dr["EstimatedData"]))
        this.btnEstimated.PerformClick();
      this.UpdateFinalScore();
    }

    public static void DeleteModifiersFromDB(DataRow row)
    {
      string str = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject((object) "SELECT * FROM Seismic_BuildingTypeModifiers WHERE SeismicBuildingTypeID = '", row["SeismicBuildingTypeID"]), (object) "'"));
      DataTable dataTable = mdUtility.DB.GetDataTable(str);
      if (dataTable.Rows.Count > 0)
      {
        try
        {
          foreach (DataRow row1 in dataTable.Rows)
          {
            row = row1;
            row.Delete();
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
      }
      mdUtility.DB.SaveDataTable(ref dataTable, str);
    }

    public DataRow Save(ref DataRow dr)
    {
      string str = "SELECT * FROM Seismic_BuildingTypeModifiers WHERE SeismicBuildingTypeID = '" + this.m_BuildingTypeID + "'";
      DataTable dataTable = mdUtility.DB.GetDataTable(str);
      try
      {
        foreach (DataRow row in dataTable.Rows)
          row.Delete();
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      try
      {
        foreach (DataRow row1 in this.m_oGroupControl.GetSelected.Rows)
        {
          DataRow row2 = dataTable.NewRow();
          row2["SeismicBuildingTypeID"] = (object) this.m_BuildingTypeID;
          row2["BuildingTypeModifierID"] = (object) mdUtility.GetUniqueID();
          row2["ModifierID"] = RuntimeHelpers.GetObjectValue(row1["ModifierID"]);
          dataTable.Rows.Add(row2);
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      mdUtility.DB.SaveDataTable(ref dataTable, str);
      dr["SeismicBuildingTypeID"] = (object) this.m_BuildingTypeID;
      dr["BuildingTypeID"] = (object) this.m_SelectedBldgType;
      dr["EstimatedData"] = (object) this.btnEstimated.Checked;
      return dr;
    }

    private void cbBuildingTypeSelection_Changed(object sender, EventArgs e)
    {
      this.RefreshAll(RuntimeHelpers.GetObjectValue(sender), false);
    }

    public void RefreshAll(object sender = null, bool NewType = false)
    {
      this.m_SelectedBldgType = Conversions.ToString(this.cboBuildingType.SelectedValue);
      this.SuspendLayout();
      this.lblBasicScore.Text = Conversions.ToString(mdUtility.DB.GetDataTable("SELECT * FROM RO_Seismic_BuildingType WHERE BuildingTypeID = " + this.m_SelectedBldgType).Rows[0]["BasicScore"]);
      this.UpdateSoilTypeLabel();
      if (NewType)
      {
        this.DrawControls();
        this.m_aryModifiers = this.m_oGroupControl.ButtonArray;
      }
      else
      {
        ArrayList selectedButtonPositions = this.GetSelectedButtonPositions();
        this.m_oGroupControl.DataSource = mdUtility.DB.GetDataTable("SELECT * FROM RO_Seismic_Modifiers WHERE BuildingTypeID = " + this.m_SelectedBldgType + " AND ModifierTypeID < 8  ORDER BY ModifierTypeID");
        this.m_oGroupControl.ValueMember = "ModifierID";
        this.m_oGroupControl.RedrawButtons();
        this.SetSelectedButtonPositions(selectedButtonPositions);
      }
      try
      {
        foreach (Control aryModifier in this.m_aryModifiers)
          aryModifier.Click += new EventHandler(this.btnModifier_Clicked);
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      this.UpdateFinalScore();
      if (sender != null)
        this.Parent.Controls[0].Focus();
      this.ResumeLayout();
    }

    public void DrawControls()
    {
      this.SuspendLayout();
      this.m_oGroupControl.DataSource = mdUtility.DB.GetDataTable("SELECT * FROM RO_Seismic_Modifiers WHERE BuildingTypeID = " + this.m_SelectedBldgType + " AND ModifierTypeID < 8  ORDER BY ModifierTypeID");
      this.m_oGroupControl.DisplayMember = "Modifier";
      this.m_oGroupControl.ValueMember = "ModifierID";
      this.m_oGroupControl.Draw();
      this.m_aryModifiers = this.m_oGroupControl.ButtonArray;
      this.tblColumn.Controls.Add((Control) this.lblBasicScore);
      if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.m_SelectedBldgType, "", false) > 0U)
      {
        try
        {
          foreach (object aryModifier in this.m_aryModifiers)
          {
            object objectValue = RuntimeHelpers.GetObjectValue(aryModifier);
            if (objectValue is Control)
              this.tblColumn.Controls.Add((Control) objectValue);
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        this.tblColumn.Controls.Add((Control) this.lblSoilType);
        this.tblColumn.Controls.Add((Control) this.lblDivider);
        this.tblColumn.Controls.Add((Control) this.lblFinalScore);
        this.tblColumn.Controls.Add((Control) this.btnEstimated);
        this.cboBuildingType.SelectedValue = (object) this.m_SelectedBldgType;
      }
      this.ResumeLayout();
    }

    private void btnModifier_Clicked(object sender, EventArgs e)
    {
      this.UpdateFinalScore();
    }

    private void UpdateFinalScore()
    {
      if ((uint) Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.m_SelectedBldgType, "", false) > 0U)
      {
        this.lblFinalScore.Text = this.lblBasicScore.Text;
        try
        {
          foreach (AssessmentHelpers.CheckButtonWithValue aryModifier in this.m_aryModifiers)
          {
            if (aryModifier.Checked)
              this.lblFinalScore.Text = Conversions.ToString(Conversions.ToDouble(this.lblFinalScore.Text) + Conversions.ToDouble(aryModifier.Text));
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        this.lblFinalScore.Text = Conversions.ToString(Conversions.ToDouble(this.lblFinalScore.Text) + Conversions.ToDouble(this.lblSoilType.Text));
      }
      this.FinalScore = Conversions.ToDouble(Interaction.IIf(Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.lblFinalScore.Text, "", false) == 0, (object) 0, (object) this.lblFinalScore.Text));
    }

    private void btnEstimated_Click(object sender, EventArgs e)
    {
      if (this.btnEstimated.Checked)
        this.btnEstimated.Image = (Image) BuilderRED.My.Resources.Resources.asteriskClean16x16;
      else
        this.btnEstimated.Image = (Image) null;
    }

    private void btnDeleteColumn_click(object sender, EventArgs e)
    {
      this.Parent.Controls[0].Focus();
      this.Parent.Controls.Remove((Control) this);
    }

    private void cboBuildingType_droppedDown()
    {
      this.cboBuildingType.DataSource = (object) mdUtility.DB.GetDataTable("SELECT BuildingTypeID, BuildingTypeName & \" - \" & BuildingTypeDesc as Display  FROM RO_Seismic_BuildingType WHERE SeismicityID = " + Conversions.ToString(this.m_Seismicity));
      this.cboBuildingType.DropDownWidth = 500;
    }

    private void cboBuildingType_closed(object sender, EventArgs e)
    {
      int selectedIndex = this.cboBuildingType.SelectedIndex;
      this.cboBuildingType.DataSource = (object) mdUtility.DB.GetDataTable("SELECT BuildingTypeID, BuildingTypeName as Display  FROM RO_Seismic_BuildingType WHERE SeismicityID = " + Conversions.ToString(this.m_Seismicity));
      this.cboBuildingType.SelectedIndex = selectedIndex;
      if (Conversions.ToDouble(this.m_SelectedBldgType) > 0.0)
        this.RefreshAll((object) this.cboBuildingType, false);
      else
        this.DrawControls();
    }

    private void cboBuildingType_ChangeCommitted(object sender, EventArgs e)
    {
      int selectedIndex = this.cboBuildingType.SelectedIndex;
      this.cboBuildingType.DataSource = (object) mdUtility.DB.GetDataTable("SELECT BuildingTypeID, BuildingTypeName as Display  FROM RO_Seismic_BuildingType WHERE SeismicityID = " + Conversions.ToString(this.m_Seismicity));
      this.cboBuildingType.SelectedIndex = selectedIndex;
      if (Conversions.ToDouble(this.m_SelectedBldgType) > 0.0)
        this.RefreshAll((object) this.cboBuildingType, false);
      else
        this.RefreshAll((object) this.cboBuildingType, true);
    }

    public delegate void FinalScoreChangedEventHandler(object sender, double finalScore);
  }
}
