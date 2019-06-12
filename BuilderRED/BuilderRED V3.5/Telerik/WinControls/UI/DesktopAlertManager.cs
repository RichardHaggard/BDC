// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DesktopAlertManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class DesktopAlertManager
  {
    [ThreadStatic]
    private static DesktopAlertManager instance;
    private Screen activeScreen;
    private List<RadDesktopAlert> openedAlerts;

    public event DesktopAlertManagerEventHandler AlertAdded;

    public event DesktopAlertManagerEventHandler AlertRemoved;

    private DesktopAlertManager()
    {
      this.openedAlerts = new List<RadDesktopAlert>();
      this.activeScreen = Screen.PrimaryScreen;
    }

    public static DesktopAlertManager Instance
    {
      get
      {
        if (DesktopAlertManager.instance == null)
          DesktopAlertManager.instance = new DesktopAlertManager();
        return DesktopAlertManager.instance;
      }
    }

    public Screen ActiveScreen
    {
      get
      {
        return this.activeScreen;
      }
    }

    public Point GetAlertPopupLocation(RadDesktopAlert alert)
    {
      if (alert.GetLocationModifiedByUser())
        return alert.Popup.Location;
      switch (alert.ScreenPosition)
      {
        case AlertScreenPosition.Manual:
          return alert.Popup.Location;
        case AlertScreenPosition.BottomRight:
        case AlertScreenPosition.BottomCenter:
        case AlertScreenPosition.BottomLeft:
          return new Point() { X = this.GetHorizontalLocation(alert), Y = this.GetBottomTopLocation(alert) };
        case AlertScreenPosition.TopRight:
        case AlertScreenPosition.TopCenter:
        case AlertScreenPosition.TopLeft:
          return new Point() { X = this.GetHorizontalLocation(alert), Y = this.GetTopBottomLocation(alert) };
        default:
          return Point.Empty;
      }
    }

    public void SetActiveScreen(Screen activeScreen)
    {
      this.activeScreen = activeScreen;
      this.UpdateAlertsOrder();
    }

    public IEnumerator<RadDesktopAlert> GetRegisteredAlerts()
    {
      return (IEnumerator<RadDesktopAlert>) this.openedAlerts.GetEnumerator();
    }

    public void UpdateAlertsOrder()
    {
      foreach (RadDesktopAlert openedAlert in this.openedAlerts)
      {
        Point alertPopupLocation = this.GetAlertPopupLocation(openedAlert);
        openedAlert.OnLocationChangeRequested(alertPopupLocation);
      }
    }

    public void AddAlert(RadDesktopAlert alert)
    {
      if (this.openedAlerts.Contains(alert))
        return;
      alert.OnLocationChangeRequested(this.GetAlertPopupLocation(alert));
      this.openedAlerts.Add(alert);
      if (this.AlertAdded == null)
        return;
      this.AlertAdded((object) this, new DesktopAlertManagerEventArgs(alert));
    }

    public void RemoveAlert(RadDesktopAlert alert)
    {
      this.openedAlerts.Remove(alert);
      this.UpdateAlertsOrder();
      if (this.AlertRemoved == null)
        return;
      this.AlertRemoved((object) this, new DesktopAlertManagerEventArgs(alert));
    }

    public bool ContainsAlert(RadDesktopAlert alert)
    {
      return this.openedAlerts.Contains(alert);
    }

    private int GetHorizontalLocation(RadDesktopAlert alert)
    {
      Size size = alert.Popup.NonAnimatedSize;
      if (alert.Popup.LastShowDpiScaleFactor.IsEmpty)
        size = TelerikDpiHelper.ScaleSize(size, alert.DpiScaleElement.DpiScaleFactor);
      switch (alert.ScreenPosition)
      {
        case AlertScreenPosition.BottomRight:
        case AlertScreenPosition.TopRight:
          return this.activeScreen.WorkingArea.X + this.activeScreen.WorkingArea.Width - size.Width;
        case AlertScreenPosition.BottomCenter:
        case AlertScreenPosition.TopCenter:
          return this.activeScreen.WorkingArea.X + (this.activeScreen.WorkingArea.Width - size.Width) / 2;
        case AlertScreenPosition.BottomLeft:
        case AlertScreenPosition.TopLeft:
          return this.activeScreen.WorkingArea.X;
        default:
          return this.activeScreen.WorkingArea.X;
      }
    }

    private int GetBottomTopLocation(RadDesktopAlert alert)
    {
      Size size = alert.Popup.NonAnimatedSize;
      if (alert.Popup.LastShowDpiScaleFactor.IsEmpty)
        size = TelerikDpiHelper.ScaleSize(size, alert.DpiScaleElement.DpiScaleFactor);
      int initialOffset = this.activeScreen.WorkingArea.Height - size.Height + this.activeScreen.WorkingArea.Y;
      this.EvaluateAlertOffset(alert, ref initialOffset, false);
      return initialOffset;
    }

    private int GetTopBottomLocation(RadDesktopAlert alert)
    {
      int y = this.activeScreen.WorkingArea.Y;
      this.EvaluateAlertOffset(alert, ref y, true);
      return y;
    }

    private void EvaluateAlertOffset(
      RadDesktopAlert alert,
      ref int initialOffset,
      bool isTopBottom)
    {
      foreach (RadDesktopAlert openedAlert in this.openedAlerts)
      {
        if (object.ReferenceEquals((object) openedAlert, (object) alert))
          break;
        if (!openedAlert.GetLocationModifiedByUser())
        {
          Size nonAnimatedSize = openedAlert.Popup.NonAnimatedSize;
          if (openedAlert.ScreenPosition == alert.ScreenPosition)
          {
            if (isTopBottom)
              initialOffset += nonAnimatedSize.Height;
            else
              initialOffset -= nonAnimatedSize.Height;
          }
        }
      }
    }
  }
}
