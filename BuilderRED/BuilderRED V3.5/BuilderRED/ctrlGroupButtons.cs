// Decompiled with JetBrains decompiler
// Type: BuilderRED.ctrlGroupButtons
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
  public class ctrlGroupButtons : UserControl
  {
    private IContainer components;
    private DataTable m_DataSource;
    private DataColumn[] Keys;
    private string m_ValueMember;
    private string m_DisplayMember;
    private bool m_isRadio;
    private int m_NumColumns;
    private string m_sHeader;
    private ArrayList m_ButtonArray;
    private string newPropertyValue;

    public ctrlGroupButtons()
    {
      this.Keys = new DataColumn[2];
      this.m_ButtonArray = new ArrayList();
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
      this.tblControls = new TableLayoutPanel();
      this.gbButtons = new GroupBox();
      this.gbButtons.SuspendLayout();
      this.SuspendLayout();
      this.tblControls.AutoSize = true;
      this.tblControls.ColumnCount = 6;
      this.tblControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667f));
      this.tblControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667f));
      this.tblControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667f));
      this.tblControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667f));
      this.tblControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667f));
      this.tblControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667f));
      this.tblControls.Location = new Point(3, 21);
      this.tblControls.Margin = new Padding(3, 3, 3, 0);
      this.tblControls.Name = "tblControls";
      this.tblControls.Padding = new Padding(5, 0, 5, 0);
      this.tblControls.RowCount = 1;
      this.tblControls.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tblControls.Size = new Size(10, 29);
      this.tblControls.TabIndex = 0;
      this.gbButtons.AutoSize = true;
      this.gbButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.gbButtons.Controls.Add((Control) this.tblControls);
      this.gbButtons.Location = new Point(3, 7);
      this.gbButtons.Margin = new Padding(3, 3, 3, 0);
      this.gbButtons.Name = "gbButtons";
      this.gbButtons.Padding = new Padding(0);
      this.gbButtons.Size = new Size(16, 63);
      this.gbButtons.TabIndex = 2;
      this.gbButtons.TabStop = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoSize = true;
      this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.Controls.Add((Control) this.gbButtons);
      this.Name = nameof (ctrlGroupButtons);
      this.Size = new Size(22, 70);
      this.gbButtons.ResumeLayout(false);
      this.gbButtons.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    internal virtual TableLayoutPanel tblControls { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    internal virtual GroupBox gbButtons { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

    public ArrayList ButtonArray
    {
      get
      {
        return this.m_ButtonArray;
      }
    }

    public string Header
    {
      get
      {
        return this.m_sHeader;
      }
      set
      {
        this.m_sHeader = value;
      }
    }

    public string NewProperty
    {
      get
      {
        return this.newPropertyValue;
      }
      set
      {
        this.newPropertyValue = value;
      }
    }

    public bool HasSelection
    {
      get
      {
        try
        {
          foreach (CheckBox control in (Control.ControlCollection) this.ButtonsTable.Controls)
          {
            if (control.Checked)
              return true;
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        return false;
      }
    }

    public TableLayoutPanel ButtonsTable
    {
      get
      {
        return this.tblControls;
      }
      set
      {
        this.tblControls = value;
      }
    }

    public DataTable GetSelected
    {
      get
      {
        DataTable dataTable = this.m_DataSource.Copy();
        try
        {
          foreach (DataRow row in dataTable.Rows)
          {
            if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(row["Selected"], (object) false, false))
              row.Delete();
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        dataTable.AcceptChanges();
        return dataTable;
      }
    }

    public DataTable SetSelected
    {
      set
      {
        try
        {
          foreach (DataRow row in value.Rows)
          {
            try
            {
              foreach (AssessmentHelpers.CheckButtonWithValue button in this.m_ButtonArray)
              {
                if (!Information.IsDBNull(RuntimeHelpers.GetObjectValue(row[this.m_ValueMember])) && Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual((object) button.Value, row[this.m_ValueMember], false))
                {
                  button.PerformClick();
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

    public bool IsSelected
    {
      get
      {
        if (this.m_isRadio)
          return true;
        try
        {
          foreach (CheckBox button in this.m_ButtonArray)
          {
            if (button.Checked)
              return true;
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        return false;
      }
    }

    public DataTable DataSource
    {
      get
      {
        return this.m_DataSource;
      }
      set
      {
        value.Columns.Add("Selected", System.Type.GetType("System.Boolean"));
        try
        {
          foreach (DataRow row in value.Rows)
            row["Selected"] = (object) false;
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        value.AcceptChanges();
        this.m_DataSource = value;
      }
    }

    public string ValueMember
    {
      get
      {
        return this.m_ValueMember;
      }
      set
      {
        this.Keys[0] = this.m_DataSource.Columns[value];
        this.m_DataSource.PrimaryKey = this.Keys;
        this.m_ValueMember = value;
      }
    }

    public string DisplayMember
    {
      get
      {
        return this.m_DisplayMember;
      }
      set
      {
        this.m_DisplayMember = value;
      }
    }

    public bool IsRadio
    {
      get
      {
        return this.m_isRadio;
      }
      set
      {
        this.m_isRadio = value;
      }
    }

    public int NumColumns
    {
      get
      {
        return this.m_NumColumns;
      }
      set
      {
        this.m_NumColumns = value;
        this.tblControls.ColumnCount = value;
      }
    }

    public string RowSpan
    {
      get
      {
        return Conversions.ToString(Math.Ceiling((double) this.DataSource.Rows.Count / (double) this.m_NumColumns));
      }
    }

    public void AddControlToControlsTable(Control ctrl)
    {
      this.tblControls.Controls.Add(ctrl);
    }

    public virtual void Button_Clicked(object sender, EventArgs e)
    {
      this.SetSelectedButton((AssessmentHelpers.CheckButtonWithValue) sender);
    }

    private void SetSelectedButton(AssessmentHelpers.CheckButtonWithValue btnClicked)
    {
      if (this.m_isRadio)
      {
        try
        {
          foreach (AssessmentHelpers.CheckButtonWithValue button in this.m_ButtonArray)
          {
            if (button != btnClicked)
              button.Checked = false;
            else
              button.Checked = true;
            this.m_DataSource.Rows.Find((object) button.Value)["Selected"] = (object) button.Checked;
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
      }
      else
        this.m_DataSource.Rows.Find((object) btnClicked.Value)["Selected"] = (object) btnClicked.Checked;
      // ISSUE: reference to a compiler-generated field
      ctrlGroupButtons.GroupControl_ClickEventHandler controlClickEvent = this.GroupControl_ClickEvent;
      if (controlClickEvent == null)
        return;
      controlClickEvent((object) this, new EventArgs());
    }

    public event ctrlGroupButtons.GroupControl_ClickEventHandler GroupControl_Click;

    public virtual void Draw()
    {
      this.gbButtons.Text = this.m_sHeader;
      if (this.m_NumColumns == 0)
        this.tblControls.ColumnCount = this.DataSource.Rows.Count;
      try
      {
        foreach (DataRow row in this.m_DataSource.Rows)
        {
          AssessmentHelpers.CheckButtonWithValue checkButtonWithValue = new AssessmentHelpers.CheckButtonWithValue();
          checkButtonWithValue.Appearance = Appearance.Button;
          checkButtonWithValue.BackColor = Control.DefaultBackColor;
          checkButtonWithValue.TextAlign = ContentAlignment.MiddleCenter;
          checkButtonWithValue.Text = Conversions.ToString(row[this.m_DisplayMember]).Replace(" ", "\r\n");
          checkButtonWithValue.Size = AssessmentHelpers.GetButtonSize;
          checkButtonWithValue.Value = Conversions.ToString(row[this.m_ValueMember]);
          checkButtonWithValue.Name = checkButtonWithValue.Value;
          checkButtonWithValue.Click += new EventHandler(this.Button_Clicked);
          this.tblControls.Controls.Add((Control) checkButtonWithValue);
          this.m_ButtonArray.Add((object) checkButtonWithValue);
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
    }

    public void RedrawButtons()
    {
      try
      {
        foreach (AssessmentHelpers.CheckButtonWithValue button in this.m_ButtonArray)
        {
          int index = this.m_ButtonArray.IndexOf((object) button);
          button.Text = Conversions.ToString(this.m_DataSource.Rows[index][this.m_DisplayMember]).Replace(" ", "\r\n");
          button.Value = Conversions.ToString(this.m_DataSource.Rows[index][this.m_ValueMember]);
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
    }

    public delegate void GroupControl_ClickEventHandler(object sender, EventArgs e);
  }
}
