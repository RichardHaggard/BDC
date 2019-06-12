// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadMessageBox
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class RadMessageBox
  {
    private static SizeF lastShowDpiScale = new SizeF(1f, 1f);
    private static RadMessageBoxForm radMessageBoxForm;

    public static RadMessageBoxForm Instance
    {
      get
      {
        if (RadMessageBox.radMessageBoxForm == null || RadMessageBox.radMessageBoxForm.IsDisposed)
          RadMessageBox.radMessageBoxForm = new RadMessageBoxForm();
        return RadMessageBox.radMessageBoxForm;
      }
    }

    public static void SetThemeName(string themeName)
    {
      RadMessageBox.Instance.ThemeName = string.Empty;
      RadMessageBox.Instance.ThemeName = themeName;
    }

    public static string ThemeName
    {
      get
      {
        if (RadMessageBox.radMessageBoxForm == null)
          return string.Empty;
        return RadMessageBox.Instance.ThemeName;
      }
      set
      {
        RadMessageBox.SetThemeName(value);
      }
    }

    public static Cursor Cursor
    {
      set
      {
        RadMessageBox.Instance.Cursor = value;
      }
    }

    public static bool ShowInTaskbar
    {
      set
      {
        RadMessageBox.Instance.ShowInTaskbar = value;
      }
    }

    public static bool UseCompatibleTextRendering
    {
      set
      {
        RadMessageBox.Instance.UseCompatibleTextRendering = value;
      }
    }

    public static SizeF LastShowDpiScale
    {
      get
      {
        return RadMessageBox.lastShowDpiScale;
      }
    }

    public static DialogResult Show(string text)
    {
      RadMessageBox.PlaySound(RadMessageIcon.None);
      return RadMessageBox.ShowCore((IWin32Window) null, text, string.Empty, MessageBoxButtons.OK, (Bitmap) null, MessageBoxDefaultButton.Button1, RightToLeft.No, (string) null);
    }

    public static DialogResult Show(string text, string caption)
    {
      RadMessageBox.PlaySound(RadMessageIcon.None);
      return RadMessageBox.ShowCore((IWin32Window) null, text, caption, MessageBoxButtons.OK, (Bitmap) null, MessageBoxDefaultButton.Button1, RightToLeft.No, (string) null);
    }

    public static DialogResult Show(
      string text,
      string caption,
      MessageBoxButtons buttons)
    {
      RadMessageBox.PlaySound(RadMessageIcon.None);
      return RadMessageBox.ShowCore((IWin32Window) null, text, caption, buttons, (Bitmap) null, MessageBoxDefaultButton.Button1, RightToLeft.No, (string) null);
    }

    public static DialogResult Show(
      string text,
      string caption,
      MessageBoxButtons buttons,
      string detailsText)
    {
      RadMessageBox.PlaySound(RadMessageIcon.None);
      return RadMessageBox.ShowCore((IWin32Window) null, text, caption, buttons, (Bitmap) null, MessageBoxDefaultButton.Button1, RightToLeft.No, detailsText);
    }

    public static DialogResult Show(
      string text,
      string caption,
      MessageBoxButtons buttons,
      RadMessageIcon icon)
    {
      RadMessageBox.PlaySound(icon);
      return RadMessageBox.ShowCore((IWin32Window) null, text, caption, buttons, RadMessageBox.GetRadMessageIcon(icon), MessageBoxDefaultButton.Button1, RightToLeft.No, (string) null);
    }

    public static DialogResult Show(
      string text,
      string caption,
      MessageBoxButtons buttons,
      RadMessageIcon icon,
      string detailsText)
    {
      RadMessageBox.PlaySound(icon);
      return RadMessageBox.ShowCore((IWin32Window) null, text, caption, buttons, RadMessageBox.GetRadMessageIcon(icon), MessageBoxDefaultButton.Button1, RightToLeft.No, detailsText);
    }

    public static DialogResult Show(
      string text,
      string caption,
      MessageBoxButtons buttons,
      RadMessageIcon icon,
      MessageBoxDefaultButton defaultButton)
    {
      RadMessageBox.PlaySound(icon);
      return RadMessageBox.ShowCore((IWin32Window) null, text, caption, buttons, RadMessageBox.GetRadMessageIcon(icon), defaultButton, RightToLeft.No, (string) null);
    }

    public static DialogResult Show(
      string text,
      string caption,
      MessageBoxButtons buttons,
      RadMessageIcon icon,
      MessageBoxDefaultButton defaultButton,
      string detailsText)
    {
      RadMessageBox.PlaySound(icon);
      return RadMessageBox.ShowCore((IWin32Window) null, text, caption, buttons, RadMessageBox.GetRadMessageIcon(icon), defaultButton, RightToLeft.No, detailsText);
    }

    public static DialogResult Show(IWin32Window parent, string text)
    {
      RadMessageBox.PlaySound(RadMessageIcon.None);
      return RadMessageBox.ShowCore(parent, text, string.Empty, MessageBoxButtons.OK, (Bitmap) null, MessageBoxDefaultButton.Button1, RightToLeft.No, (string) null);
    }

    public static DialogResult Show(IWin32Window parent, string text, string caption)
    {
      RadMessageBox.PlaySound(RadMessageIcon.None);
      return RadMessageBox.ShowCore(parent, text, caption, MessageBoxButtons.OK, (Bitmap) null, MessageBoxDefaultButton.Button1, RightToLeft.No, (string) null);
    }

    public static DialogResult Show(
      IWin32Window parent,
      string text,
      string caption,
      MessageBoxButtons buttons)
    {
      RadMessageBox.PlaySound(RadMessageIcon.None);
      return RadMessageBox.ShowCore(parent, text, caption, buttons, (Bitmap) null, MessageBoxDefaultButton.Button1, RightToLeft.No, (string) null);
    }

    public static DialogResult Show(
      IWin32Window parent,
      string text,
      string caption,
      MessageBoxButtons buttons,
      string detailsText)
    {
      RadMessageBox.PlaySound(RadMessageIcon.None);
      return RadMessageBox.ShowCore(parent, text, caption, buttons, (Bitmap) null, MessageBoxDefaultButton.Button1, RightToLeft.No, detailsText);
    }

    public static DialogResult Show(
      IWin32Window parent,
      string text,
      string caption,
      MessageBoxButtons buttons,
      RadMessageIcon icon)
    {
      RadMessageBox.PlaySound(icon);
      return RadMessageBox.ShowCore(parent, text, caption, buttons, RadMessageBox.GetRadMessageIcon(icon), MessageBoxDefaultButton.Button1, RightToLeft.No, (string) null);
    }

    public static DialogResult Show(
      IWin32Window parent,
      string text,
      string caption,
      MessageBoxButtons buttons,
      Bitmap icon)
    {
      if (icon.Size.Height > 48 || icon.Size.Width > 48)
        icon = new Bitmap((Image) icon, new Size(48, 48));
      return RadMessageBox.ShowCore(parent, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, RightToLeft.No, (string) null);
    }

    public static DialogResult Show(
      IWin32Window parent,
      string text,
      string caption,
      MessageBoxButtons buttons,
      RadMessageIcon icon,
      MessageBoxDefaultButton defaultBtn)
    {
      RadMessageBox.PlaySound(icon);
      return RadMessageBox.ShowCore(parent, text, caption, buttons, RadMessageBox.GetRadMessageIcon(icon), defaultBtn, RightToLeft.No, (string) null);
    }

    public static DialogResult Show(
      IWin32Window parent,
      string text,
      string caption,
      MessageBoxButtons buttons,
      RadMessageIcon icon,
      MessageBoxDefaultButton defaultBtn,
      RightToLeft rtl)
    {
      RadMessageBox.PlaySound(icon);
      return RadMessageBox.ShowCore(parent, text, caption, buttons, RadMessageBox.GetRadMessageIcon(icon), defaultBtn, rtl, (string) null);
    }

    public static DialogResult Show(
      IWin32Window parent,
      string text,
      string caption,
      MessageBoxButtons buttons,
      RadMessageIcon icon,
      MessageBoxDefaultButton defaultBtn,
      RightToLeft rtl,
      string detailsText)
    {
      RadMessageBox.PlaySound(icon);
      return RadMessageBox.ShowCore(parent, text, caption, buttons, RadMessageBox.GetRadMessageIcon(icon), defaultBtn, rtl, detailsText);
    }

    public static DialogResult Show(
      IWin32Window parent,
      string text,
      string caption,
      MessageBoxButtons buttons,
      Bitmap icon,
      MessageBoxDefaultButton defaultBtn)
    {
      if (icon.Size.Height > 48 || icon.Size.Width > 48)
        icon = new Bitmap((Image) icon, new Size(48, 48));
      return RadMessageBox.ShowCore(parent, text, caption, buttons, icon, defaultBtn, RightToLeft.No, (string) null);
    }

    private static DialogResult ShowCore(
      IWin32Window owner,
      string text,
      string caption,
      MessageBoxButtons buttons,
      Bitmap icon,
      MessageBoxDefaultButton defaultButton,
      RightToLeft rightToLeft,
      string detailsText)
    {
      if (RadMessageBox.Instance.Visible)
        RadMessageBox.Instance.Dispose();
      if (RadMessageBox.Instance == null || RadMessageBox.Instance.IsDisposed)
        RadMessageBox.radMessageBoxForm = new RadMessageBoxForm();
      RadMessageBox.Instance.DialogResult = DialogResult.Cancel;
      RadMessageBox.Instance.RightToLeft = rightToLeft;
      RadMessageBox.Instance.DetailsText = detailsText;
      RadMessageBox.Instance.MessageText = text;
      RadMessageBox.Instance.StartPosition = FormStartPosition.CenterParent;
      RadMessageBox.Instance.Owner = (Form) null;
      Control bottom = (Control) null;
      if (owner != null)
      {
        bottom = Control.FromHandle(owner.Handle);
        if (bottom != null)
          RadMessageBox.Instance.Owner = bottom.FindForm();
      }
      else
        RadMessageBox.Instance.StartPosition = FormStartPosition.CenterScreen;
      if (RadMessageBox.Instance.Owner != null)
        RadMessageBox.Instance.TopMost = RadMessageBox.Instance.Owner.TopMost;
      if (!string.IsNullOrEmpty(caption))
        RadMessageBox.Instance.Text = caption;
      else
        RadMessageBox.Instance.Text = string.Empty;
      RadMessageBox.Instance.MessageIcon = icon;
      RadMessageBox.Instance.ButtonsConfiguration = buttons;
      RadMessageBox.Instance.DefaultButton = defaultButton;
      bool flag = RadMessageBox.CheckParentingCycle(bottom, (Control) RadMessageBox.Instance);
      if (RadMessageBox.Instance.Owner != null)
      {
        if (!flag)
        {
          try
          {
            int num = (int) RadMessageBox.Instance.ShowDialog((IWin32Window) RadMessageBox.Instance.Owner);
            goto label_18;
          }
          catch (ArgumentException ex)
          {
            int num = (int) RadMessageBox.Instance.ShowDialog();
            goto label_18;
          }
        }
      }
      int num1 = (int) RadMessageBox.Instance.ShowDialog();
label_18:
      DialogResult dialogResult = DialogResult.OK;
      if (buttons != MessageBoxButtons.OK)
        dialogResult = RadMessageBox.Instance.DialogResult;
      return dialogResult;
    }

    internal static bool CheckParentingCycle(Control bottom, Control toFind)
    {
      Form form1 = (Form) null;
      Control control1 = (Control) null;
      for (Control control2 = bottom; control2 != null; control2 = control2.Parent)
      {
        control1 = control2;
        if (control2 == toFind)
          return true;
      }
      if (control1 != null && control1 is Form)
      {
        for (Form form2 = (Form) control1; form2 != null; form2 = form2.Owner)
        {
          form1 = form2;
          if (form2 == toFind)
            return true;
        }
      }
      if (form1 != null && form1.Parent != null)
        return RadMessageBox.CheckParentingCycle(form1.Parent, toFind);
      return false;
    }

    private static Bitmap GetRadMessageIcon(RadMessageIcon icon)
    {
      switch (icon)
      {
        case RadMessageIcon.Info:
          Stream manifestResourceStream1 = Assembly.GetExecutingAssembly().GetManifestResourceStream("Telerik.WinControls.UI.Resources.RadMessageBox.MessageInfo.png");
          Bitmap bitmap1 = Image.FromStream(manifestResourceStream1) as Bitmap;
          manifestResourceStream1.Close();
          return bitmap1;
        case RadMessageIcon.Question:
          Stream manifestResourceStream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream("Telerik.WinControls.UI.Resources.RadMessageBox.MessageQuestion.png");
          Bitmap bitmap2 = Image.FromStream(manifestResourceStream2) as Bitmap;
          manifestResourceStream2.Close();
          return bitmap2;
        case RadMessageIcon.Exclamation:
          Stream manifestResourceStream3 = Assembly.GetExecutingAssembly().GetManifestResourceStream("Telerik.WinControls.UI.Resources.RadMessageBox.MessageExclamation.png");
          Bitmap bitmap3 = Image.FromStream(manifestResourceStream3) as Bitmap;
          manifestResourceStream3.Close();
          return bitmap3;
        case RadMessageIcon.Error:
          Stream manifestResourceStream4 = Assembly.GetExecutingAssembly().GetManifestResourceStream("Telerik.WinControls.UI.Resources.RadMessageBox.MessageError.png");
          Bitmap bitmap4 = Image.FromStream(manifestResourceStream4) as Bitmap;
          manifestResourceStream4.Close();
          return bitmap4;
        default:
          return (Bitmap) null;
      }
    }

    private static void PlaySound(RadMessageIcon icon)
    {
      if (!RadMessageBox.Instance.EnableBeep)
        return;
      switch (icon)
      {
        case RadMessageIcon.None:
          SystemSounds.Beep.Play();
          break;
        case RadMessageIcon.Info:
          SystemSounds.Asterisk.Play();
          break;
        case RadMessageIcon.Question:
          SystemSounds.Question.Play();
          break;
        case RadMessageIcon.Exclamation:
          SystemSounds.Exclamation.Play();
          break;
        case RadMessageIcon.Error:
          SystemSounds.Hand.Play();
          break;
      }
    }
  }
}
