// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadThemeComponentBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Telerik.WinControls
{
  [Designer("Telerik.WinControls.UI.Design.RadThemesDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public abstract class RadThemeComponentBase : Component
  {
    private bool isDesignMode;

    public RadThemeComponentBase()
    {
      if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
        return;
      LicenseManager.Validate(this.GetType());
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public virtual void DeserializeTheme()
    {
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual string ThemeName
    {
      get
      {
        return string.Empty;
      }
    }

    public abstract void Load();

    protected virtual bool LoadResource(Assembly resourceAssembly, string resourcePath)
    {
      try
      {
        using (Stream manifestResourceStream = resourceAssembly.GetManifestResourceStream(resourcePath))
        {
          if (manifestResourceStream != null)
          {
            Theme theme = new TSSPThemeReader().Read(manifestResourceStream);
            if (theme != null)
            {
              ThemeRepository.Add(theme);
              return true;
            }
          }
        }
      }
      catch
      {
        int num = (int) MessageBox.Show(string.Format("Failed to load {0} theme.", (object) resourcePath));
      }
      return false;
    }

    public bool IsDesignMode
    {
      get
      {
        return this.isDesignMode;
      }
    }

    public override ISite Site
    {
      get
      {
        return base.Site;
      }
      set
      {
        if (!this.isDesignMode && value != null)
        {
          this.isDesignMode = true;
          if (ThemeRepository.FindTheme(this.ThemeName) == null)
            this.Load();
        }
        base.Site = value;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (this.Site == null && this.IsDesignMode)
        ThemeRepository.Remove(this.ThemeName);
      base.Dispose(disposing);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static RadThemeComponentBase.ThemeContext CreateContext(
      Control control)
    {
      return new RadThemeComponentBase.ThemeContext(control);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public class ThemeContext
    {
      private Dictionary<int, RadThemeComponentBase.ThemeContext.ControlInfo> infos = new Dictionary<int, RadThemeComponentBase.ThemeContext.ControlInfo>();
      private List<Control> blackList = new List<Control>();
      private Control control;

      public ThemeContext(Control control)
      {
        this.control = control;
        this.Save();
      }

      public Control Control
      {
        get
        {
          return this.control;
        }
      }

      public List<Control> ExcludeFromNearest
      {
        get
        {
          return this.blackList;
        }
      }

      public void Save()
      {
        this.blackList.Clear();
        this.infos.Clear();
        this.SavePositions(this.control.Controls);
      }

      public void Restore()
      {
        this.RestorePositions(this.control.Controls);
      }

      public void CorrectPositions()
      {
        this.CorrectPositionsByY(this.control.Controls);
        this.CorrectPositionsByX(this.control.Controls);
      }

      private void SavePositions(Control.ControlCollection controls)
      {
        foreach (Control control in (ArrangedElementCollection) controls)
        {
          this.infos.Add(control.GetHashCode(), new RadThemeComponentBase.ThemeContext.ControlInfo()
          {
            Control = control,
            Bounds = control.Bounds,
            ParentSpaceAfter = new Point(controls.Owner.ClientSize.Width - control.Right, controls.Owner.ClientSize.Height - control.Bottom)
          });
          this.SavePositions(control.Controls);
        }
      }

      private void RestorePositions(Control.ControlCollection controls)
      {
        foreach (Control control in (ArrangedElementCollection) controls)
        {
          control.Bounds = this.infos[control.GetHashCode()].Bounds;
          this.RestorePositions(control.Controls);
        }
      }

      private List<List<Control>> OrderInRows(Control.ControlCollection controls)
      {
        List<List<Control>> controlListList = new List<List<Control>>();
        foreach (Control control in (ArrangedElementCollection) controls)
        {
          bool flag = false;
          for (int index1 = 0; index1 < controlListList.Count; ++index1)
          {
            if (controlListList[index1][0].Top == control.Top)
            {
              for (int index2 = 0; index2 < controlListList[index1].Count; ++index2)
              {
                if (controlListList[index1][index2].Left > control.Left)
                {
                  controlListList[index1].Insert(index2, control);
                  flag = true;
                  break;
                }
              }
              if (!flag)
              {
                controlListList[index1].Add(control);
                flag = true;
                break;
              }
              break;
            }
            if (controlListList[index1][0].Top > control.Top)
            {
              controlListList.Insert(index1, new List<Control>());
              controlListList[index1].Add(control);
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            controlListList.Add(new List<Control>());
            controlListList[controlListList.Count - 1].Add(control);
          }
        }
        return controlListList;
      }

      private List<List<Control>> OrderInColumns(Control.ControlCollection controls)
      {
        List<List<Control>> controlListList = new List<List<Control>>();
        foreach (Control control in (ArrangedElementCollection) controls)
        {
          bool flag1 = false;
          for (int index1 = 0; index1 < controlListList.Count; ++index1)
          {
            bool flag2 = controlListList[index1][0].Left == control.Left;
            if (this.control.RightToLeft == RightToLeft.Yes)
              flag2 = controlListList[index1][0].Right == control.Right;
            if (flag2)
            {
              for (int index2 = 0; index2 < controlListList[index1].Count; ++index2)
              {
                if (controlListList[index1][index2].Top > control.Top)
                {
                  controlListList[index1].Insert(index2, control);
                  flag1 = true;
                  break;
                }
              }
              if (!flag1)
              {
                controlListList[index1].Add(control);
                flag1 = true;
                break;
              }
              break;
            }
            bool flag3 = controlListList[index1][0].Left > control.Left;
            if (this.control.RightToLeft == RightToLeft.Yes)
              flag3 = controlListList[index1][0].Right < control.Right;
            if (flag3)
            {
              controlListList.Insert(index1, new List<Control>());
              controlListList[index1].Add(control);
              flag1 = true;
              break;
            }
          }
          if (!flag1)
          {
            controlListList.Add(new List<Control>());
            controlListList[controlListList.Count - 1].Add(control);
          }
        }
        return controlListList;
      }

      private List<Control> SortByY(Control.ControlCollection controls)
      {
        List<Control> controlList = new List<Control>();
        foreach (Control control in (ArrangedElementCollection) controls)
          controlList.Add(control);
        controlList.Sort((IComparer<Control>) new RadThemeComponentBase.ThemeContext.ControlComparer());
        return controlList;
      }

      private Control FindNearestTopControl(
        List<Control> row,
        Control.ControlCollection controls)
      {
        int num1 = 10000000;
        Control control1 = (Control) null;
        foreach (object obj in row)
        {
          RadThemeComponentBase.ThemeContext.ControlInfo info1 = this.GetInfo(obj.GetHashCode());
          if (info1 != null)
          {
            foreach (Control control2 in (ArrangedElementCollection) controls)
            {
              if (!row.Contains(control2) && !this.blackList.Contains(control2))
              {
                RadThemeComponentBase.ThemeContext.ControlInfo info2 = this.GetInfo(control2.GetHashCode());
                if (info2 != null && info2.Bounds.Bottom <= info1.Bounds.Top)
                {
                  int num2 = info1.Bounds.Top - info2.Bounds.Bottom;
                  if (num2 < num1 && num1 > 0 || info1.Bounds.Left > info2.Bounds.Right && info2.Bounds.Right > control1.Bounds.Right)
                  {
                    num1 = num2;
                    control1 = control2;
                  }
                }
              }
            }
          }
        }
        return control1;
      }

      private Control FindNearestLeftControl(
        List<Control> row,
        Control.ControlCollection controls)
      {
        int num1 = 10000000;
        Control control1 = (Control) null;
        foreach (object obj in row)
        {
          RadThemeComponentBase.ThemeContext.ControlInfo info1 = this.GetInfo(obj.GetHashCode());
          if (info1 != null)
          {
            foreach (Control control2 in (ArrangedElementCollection) controls)
            {
              if (!row.Contains(control2))
              {
                RadThemeComponentBase.ThemeContext.ControlInfo info2 = this.GetInfo(control2.GetHashCode());
                if (info2 != null && info2.Bounds.Right < info1.Bounds.Left)
                {
                  int num2 = info1.Bounds.Left - info2.Bounds.Right;
                  if (num2 < num1 || info1.Bounds.Top > info2.Bounds.Bottom && info2.Bounds.Bottom > control1.Bounds.Bottom)
                  {
                    num1 = num2;
                    control1 = control2;
                  }
                }
              }
            }
          }
        }
        return control1;
      }

      private void CorrectPositionsByY(Control.ControlCollection controls)
      {
        List<List<Control>> controlListList = this.OrderInRows(controls);
        AnchorStyles[] styles = this.PersistAnchorStyles(controls);
        for (int index = 0; index < controlListList.Count; ++index)
        {
          List<Control> row = controlListList[index];
          foreach (Control control in row)
            this.CorrectPositionsByY(control.Controls);
          if (index != 0)
          {
            Control nearestTopControl = this.FindNearestTopControl(row, controls);
            if (nearestTopControl != null)
            {
              int num1 = this.infos[row[0].GetHashCode()].Bounds.Top - this.infos[nearestTopControl.GetHashCode()].Bounds.Bottom;
              int num2 = nearestTopControl.Bounds.Bottom + num1;
              foreach (Control control in row)
              {
                if (control.Dock == DockStyle.None)
                  control.Top = num2;
              }
            }
          }
        }
        if (controls.Count == 1 && controls[0].GetType().Name == "HostedTextBoxBase")
          return;
        RadThemeComponentBase.ThemeContext.ControlInfo controlInfo = (RadThemeComponentBase.ThemeContext.ControlInfo) null;
        foreach (Control control in (ArrangedElementCollection) controls)
        {
          if (controlInfo == null || controlInfo.Control.Bounds.Bottom < control.Bounds.Bottom)
            controlInfo = this.GetInfo(control.GetHashCode());
        }
        if (controlInfo != null && controlInfo.ParentSpaceAfter.Y != controls.Owner.ClientSize.Height - controlInfo.Control.Bottom)
        {
          controls.Owner.ClientSize = new Size(controls.Owner.ClientSize.Width, controlInfo.Control.Bottom + controlInfo.ParentSpaceAfter.Y);
          for (int index = 0; index < controls.Count; ++index)
          {
            if ((styles[index] & AnchorStyles.Bottom) != AnchorStyles.None)
            {
              Control control1 = controls[index];
              Control control2 = (Control) null;
              int num1 = 10000000;
              foreach (Control control3 in (ArrangedElementCollection) controls)
              {
                if (control3.Top > control1.Bottom)
                {
                  int num2 = control3.Top - control1.Bottom;
                  if (num2 < num1)
                  {
                    num1 = num2;
                    control2 = control3;
                  }
                }
              }
              if (control2 == null)
              {
                int y = this.infos[control1.GetHashCode()].ParentSpaceAfter.Y;
                int num2 = controls.Owner.ClientRectangle.Height - control1.Bounds.Bottom;
                if (num2 != y)
                  control1.Height += num2 - y;
              }
            }
          }
        }
        this.RestoreAnchorStyles(controls, styles);
      }

      private void CorrectPositionsByX(Control.ControlCollection controls)
      {
        List<List<Control>> controlListList = this.OrderInColumns(controls);
        AnchorStyles[] styles = this.PersistAnchorStyles(controls);
        for (int index = 0; index < controlListList.Count; ++index)
        {
          List<Control> row = controlListList[index];
          foreach (Control control in row)
            this.CorrectPositionsByX(control.Controls);
          if (index != 0)
          {
            Control nearestLeftControl = this.FindNearestLeftControl(row, controls);
            if (nearestLeftControl != null)
            {
              int num1 = this.infos[row[0].GetHashCode()].Bounds.Left - this.infos[nearestLeftControl.GetHashCode()].Bounds.Right;
              if (this.control.RightToLeft == RightToLeft.Yes)
              {
                int num2 = nearestLeftControl.Bounds.Left - num1;
                foreach (Control control in row)
                {
                  if (control.Dock == DockStyle.None)
                    control.Left = num2 - control.Width;
                }
              }
              else
              {
                int num2 = nearestLeftControl.Bounds.Right + num1;
                foreach (Control control in row)
                {
                  if (control.Dock == DockStyle.None)
                    control.Left = num2;
                }
              }
            }
          }
        }
        if (controls.Count == 1 && controls[0].GetType().Name == "HostedTextBoxBase")
          return;
        if (this.control.RightToLeft == RightToLeft.Yes)
          this.CorrectOwnerWidthRTL(controls, styles);
        else
          this.CorrectOwnerWidth(controls, styles);
        this.RestoreAnchorStyles(controls, styles);
      }

      private void CorrectOwnerWidth(Control.ControlCollection controls, AnchorStyles[] styles)
      {
        RadThemeComponentBase.ThemeContext.ControlInfo controlInfo = (RadThemeComponentBase.ThemeContext.ControlInfo) null;
        foreach (Control control in (ArrangedElementCollection) controls)
        {
          if (controlInfo == null || controlInfo.Control.Bounds.Right < control.Bounds.Right)
            controlInfo = this.GetInfo(control.GetHashCode());
        }
        if (controlInfo == null || controlInfo.ParentSpaceAfter.X == controls.Owner.ClientSize.Width - controlInfo.Control.Right)
          return;
        controls.Owner.ClientSize = new Size(controlInfo.Control.Right + controlInfo.ParentSpaceAfter.X, controls.Owner.ClientSize.Height);
        for (int index = 0; index < controls.Count; ++index)
        {
          if ((styles[index] & AnchorStyles.Right) != AnchorStyles.None)
          {
            Control control1 = controls[index];
            Control control2 = (Control) null;
            int num1 = 10000000;
            foreach (Control control3 in (ArrangedElementCollection) controls)
            {
              if (control3.Left > control1.Right)
              {
                int num2 = control3.Left - control1.Right;
                if (num2 < num1)
                {
                  num1 = num2;
                  control2 = control3;
                }
              }
            }
            if (control2 == null)
            {
              int x = this.infos[control1.GetHashCode()].ParentSpaceAfter.X;
              int num2 = controls.Owner.ClientRectangle.Width - control1.Bounds.Right;
              if (num2 != x)
                control1.Width += num2 - x;
            }
          }
        }
      }

      private void CorrectOwnerWidthRTL(Control.ControlCollection controls, AnchorStyles[] styles)
      {
        RadThemeComponentBase.ThemeContext.ControlInfo controlInfo = (RadThemeComponentBase.ThemeContext.ControlInfo) null;
        foreach (Control control in (ArrangedElementCollection) controls)
        {
          if (controlInfo == null || control.Bounds.Left < controlInfo.Control.Bounds.Left)
            controlInfo = this.GetInfo(control.GetHashCode());
        }
        if (controlInfo == null || controlInfo.ParentSpaceAfter.X == controlInfo.Control.Left)
          return;
        int num = controlInfo.ParentSpaceAfter.X - controlInfo.Control.Left;
        controls.Owner.ClientSize = new Size(controls.Owner.ClientSize.Width + num, controls.Owner.ClientSize.Height);
        foreach (Control control in (ArrangedElementCollection) controls)
          control.Left += num;
      }

      private AnchorStyles[] PersistAnchorStyles(Control.ControlCollection controls)
      {
        AnchorStyles[] anchorStylesArray = new AnchorStyles[controls.Count];
        for (int index = 0; index < anchorStylesArray.Length; ++index)
        {
          anchorStylesArray[index] = controls[index].Anchor;
          controls[index].Anchor = AnchorStyles.Top | AnchorStyles.Left;
        }
        return anchorStylesArray;
      }

      private void RestoreAnchorStyles(Control.ControlCollection controls, AnchorStyles[] styles)
      {
        for (int index = 0; index < styles.Length; ++index)
          controls[index].Anchor = styles[index];
      }

      public RadThemeComponentBase.ThemeContext.ControlInfo GetInfo(
        int hashCode)
      {
        if (this.infos.ContainsKey(hashCode))
          return this.infos[hashCode];
        return (RadThemeComponentBase.ThemeContext.ControlInfo) null;
      }

      public class ControlInfo
      {
        public Control Control;
        public Rectangle Bounds;
        public Point ParentSpaceAfter;
      }

      private class ControlComparer : IComparer<Control>
      {
        public int Compare(Control x, Control y)
        {
          if (x.Top < y.Top)
            return -1;
          return x.Top > y.Top ? 1 : 0;
        }
      }
    }
  }
}
