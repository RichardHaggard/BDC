// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.CodedUI.IPCMessage
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.CodedUI
{
  [Serializable]
  public class IPCMessage
  {
    private IPCMessage.MessageTypes messageType;
    private string message;
    private object data;
    private string controlType;

    public IPCMessage(IPCMessage.MessageTypes type, string message, object data)
    {
      this.messageType = type;
      this.message = message;
      this.data = data;
    }

    public IPCMessage(
      IPCMessage.MessageTypes type,
      string message,
      object data,
      string controlType)
      : this(type, message, data)
    {
      this.controlType = controlType;
    }

    public IPCMessage.MessageTypes Type
    {
      get
      {
        return this.messageType;
      }
      set
      {
        this.messageType = value;
      }
    }

    public string Message
    {
      get
      {
        return this.message;
      }
      set
      {
        this.message = value;
      }
    }

    public object Data
    {
      get
      {
        return this.data;
      }
      set
      {
        this.data = value;
      }
    }

    public string ControlType
    {
      get
      {
        return this.controlType;
      }
      set
      {
        this.controlType = value;
      }
    }

    public enum MessageTypes
    {
      GetPropertyValue,
      SetPropertyValue,
      ExecuteMethod,
      ControlTypeName,
      GetChildPropertyValue,
      SetChildPropertyValue,
    }
  }
}
