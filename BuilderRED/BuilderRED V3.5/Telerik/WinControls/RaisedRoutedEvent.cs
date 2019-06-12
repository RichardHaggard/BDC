// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RaisedRoutedEvent
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Telerik.WinControls
{
  [XmlInclude(typeof (EventBehaviorSenderType))]
  [XmlInclude(typeof (RoutingDirection))]
  [Serializable]
  public class RaisedRoutedEvent
  {
    private RoutedEvent routedEvent;
    private string sender;
    private EventBehaviorSenderType senderType;
    private RoutingDirection direction;
    private string routedEventFullName;

    public RaisedRoutedEvent()
    {
    }

    public RaisedRoutedEvent(
      RoutedEvent routedEvent,
      string sender,
      EventBehaviorSenderType senderType,
      RoutingDirection direction)
    {
      this.routedEvent = routedEvent;
      this.sender = sender;
      this.senderType = senderType;
      this.direction = direction;
    }

    [XmlIgnore]
    public RoutedEvent RoutedEvent
    {
      get
      {
        if (!string.IsNullOrEmpty(this.RoutedEventFullName))
          throw new NotImplementedException();
        return this.routedEvent;
      }
      set
      {
        this.routedEvent = value;
        this.RoutedEventFullName = this.routedEvent.OwnerType.FullName + "." + this.routedEvent.EventName;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [XmlAttribute]
    public string RoutedEventFullName
    {
      get
      {
        return this.routedEventFullName;
      }
      set
      {
        this.routedEventFullName = value;
      }
    }

    [XmlAttribute]
    public EventBehaviorSenderType SenderType
    {
      get
      {
        return this.senderType;
      }
      set
      {
        this.senderType = value;
      }
    }

    [XmlAttribute]
    public string Sender
    {
      get
      {
        return this.sender;
      }
      set
      {
        this.sender = value;
      }
    }

    [XmlAttribute]
    public RoutingDirection Direction
    {
      get
      {
        return this.direction;
      }
      set
      {
        this.direction = value;
      }
    }

    public bool IsSameEvent(RadElement senderElement, RoutedEventArgs eventArgs)
    {
      bool flag = false;
      if (string.Compare(this.RoutedEvent.EventName, eventArgs.RoutedEvent.EventName, true) == 0)
      {
        switch (this.SenderType)
        {
          case EventBehaviorSenderType.AnySender:
            flag = true;
            break;
          case EventBehaviorSenderType.ElementType:
            if (this.Direction == eventArgs.Direction && senderElement.GetType().Name == this.sender)
            {
              flag = true;
              break;
            }
            break;
          case EventBehaviorSenderType.ElementName:
            if (this.Direction == eventArgs.Direction && string.Compare(senderElement.Name, this.sender, true) == 0)
            {
              flag = true;
              break;
            }
            break;
        }
      }
      return flag;
    }

    public bool IsSameEvent(RaisedRoutedEvent targetEvent)
    {
      bool flag = false;
      if (string.Compare(this.RoutedEvent.EventName, targetEvent.RoutedEvent.EventName, true) == 0 && this.direction == targetEvent.direction)
        flag = this.Sender == targetEvent.Sender;
      return flag;
    }
  }
}
