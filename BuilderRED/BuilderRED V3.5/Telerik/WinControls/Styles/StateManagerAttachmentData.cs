// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Styles.StateManagerAttachmentData
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.Styles
{
  public class StateManagerAttachmentData : IDisposable
  {
    private List<RadProperty> affectedProperties;
    private RadObject elementToAttachTo;
    private RadItemStateChangedEventHandler eventHandler;

    public StateManagerAttachmentData(
      RadObject elementToAttachTo,
      RadItemStateChangedEventHandler eventHandler)
    {
      this.eventHandler = eventHandler;
      this.elementToAttachTo = elementToAttachTo;
      elementToAttachTo.RadPropertyChanged += new RadPropertyChangedEventHandler(this.attachedElement_RadPropertyChanged);
    }

    public void AddEventHandlers(List<RadProperty> properties)
    {
      if (this.affectedProperties == null)
        this.affectedProperties = new List<RadProperty>();
      this.affectedProperties.AddRange((IEnumerable<RadProperty>) properties);
    }

    private void attachedElement_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (this.affectedProperties == null)
        return;
      IStylableElement stylableElement = sender as IStylableElement;
      RadObject sender1 = sender as RadObject;
      if (stylableElement == null)
        return;
      for (int index = 0; index < this.affectedProperties.Count; ++index)
      {
        if (e.Property == this.affectedProperties[index])
        {
          this.eventHandler(sender1, e);
          break;
        }
      }
    }

    public void Dispose()
    {
      if (this.elementToAttachTo == null)
        return;
      this.elementToAttachTo.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.attachedElement_RadPropertyChanged);
    }
  }
}
