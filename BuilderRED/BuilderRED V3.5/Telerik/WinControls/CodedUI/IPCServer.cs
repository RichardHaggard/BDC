// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.CodedUI.IPCServer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Telerik.WinControls.CodedUI
{
  public class IPCServer
  {
    public static int IPCMessage = Telerik.WinControls.NativeMethods.RegisterWindowMessage("RadControl.IPC.Message");
    private const uint FileMapAllAccess = 31;
    private const uint PageReadWrite = 4;
    private const uint FileMapRead = 4;
    private const uint FileMapWrite = 2;
    public const int IPCMSG_BeginRequest = 1;
    public const int IPCMSG_EndRequest = 3;
    public const int IPCMSG_Supports = 2;
    public const int IPCResponseOK = 10;
    public const int IPCServerSize = 100000;
    public const string IPCServerName = "RadControl.IPC.Server.Request";
    public const string IPCResponseServerName = "RadControl.IPC.Server.Response";

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CreateFileMapping(
      IntPtr hFile,
      IntPtr lpAttr,
      uint fl,
      uint dwHigh,
      uint dwLow,
      string lpName);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenFileMapping(
      uint dwAccess,
      bool bInheritHandle,
      string lpName);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr MapViewOfFile(
      IntPtr hFile,
      uint dwAccess,
      uint dwOffsetHigh,
      uint dwOffsetLow,
      UIntPtr dwBytesToMap);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr hFile);

    [DllImport("kernel32.dll")]
    private static extern uint GetLastError();

    public static bool IsSupported(IntPtr hWnd)
    {
      if (hWnd != IntPtr.Zero)
        return (int) Telerik.WinControls.NativeMethods.SendMessage(hWnd, IPCServer.IPCMessage, (IntPtr) 2, IntPtr.Zero) == 10;
      return false;
    }

    public static Telerik.WinControls.CodedUI.IPCMessage SendRequest(
      IntPtr hWnd,
      string request,
      string elementName)
    {
      return IPCServer.SendRequest(hWnd, new Telerik.WinControls.CodedUI.IPCMessage(Telerik.WinControls.CodedUI.IPCMessage.MessageTypes.GetPropertyValue, request, (object) elementName));
    }

    public static Telerik.WinControls.CodedUI.IPCMessage SendRequest(
      IntPtr hWnd,
      Telerik.WinControls.CodedUI.IPCMessage request)
    {
      Telerik.WinControls.CodedUI.IPCMessage ipcMessage = (Telerik.WinControls.CodedUI.IPCMessage) null;
      IPCServer ipcServer = new IPCServer();
      IntPtr fileMapping = IPCServer.CreateFileMapping(new IntPtr(-1), IntPtr.Zero, 4U, 0U, 100000U, "RadControl.IPC.Server.Request");
      if (fileMapping != IntPtr.Zero)
      {
        if (IPCServer.SetServerObject(fileMapping, request))
        {
          if ((int) Telerik.WinControls.NativeMethods.SendMessage(hWnd, IPCServer.IPCMessage, (IntPtr) 1, IntPtr.Zero) == 10)
          {
            IntPtr hFile = IPCServer.OpenFileMapping(4U, false, "RadControl.IPC.Server.Response");
            ipcMessage = IPCServer.GetServerObject(hFile);
            IPCServer.CloseHandle(hFile);
          }
          Telerik.WinControls.NativeMethods.SendMessage(hWnd, IPCServer.IPCMessage, (IntPtr) 3, IntPtr.Zero);
        }
        IPCServer.CloseHandle(fileMapping);
      }
      return ipcMessage;
    }

    public static bool ProcessRequest(ref Message msg, IPCHost host)
    {
      bool flag = false;
      if (msg.Msg == IPCServer.IPCMessage)
      {
        if ((int) msg.WParam == 2)
        {
          msg.Result = (IntPtr) 10;
          flag = true;
        }
        else if ((int) msg.WParam == 1)
        {
          IntPtr hFile = IPCServer.OpenFileMapping(6U, false, "RadControl.IPC.Server.Request");
          Telerik.WinControls.CodedUI.IPCMessage serverObject = IPCServer.GetServerObject(hFile);
          IPCServer.CloseHandle(hFile);
          host.ProcessMessage(ref serverObject);
          host.Context = IPCServer.CreateFileMapping(new IntPtr(-1), IntPtr.Zero, 4U, 0U, 100000U, "RadControl.IPC.Server.Response");
          IPCServer.SetServerObject(host.Context, serverObject);
          msg.Result = (IntPtr) 10;
          flag = true;
        }
        else if ((int) msg.WParam == 3)
          IPCServer.CloseHandle(host.Context);
      }
      return flag;
    }

    public static Telerik.WinControls.CodedUI.IPCMessage GetServerObject(IntPtr hFile)
    {
      Telerik.WinControls.CodedUI.IPCMessage ipcMessage = (Telerik.WinControls.CodedUI.IPCMessage) null;
      if (hFile != IntPtr.Zero)
      {
        IntPtr num = IPCServer.MapViewOfFile(hFile, 4U, 0U, 0U, (UIntPtr) 100000U);
        byte[] numArray = new byte[Marshal.ReadInt32(num)];
        Marshal.Copy(new IntPtr(num.ToInt64() + 4L), numArray, 0, numArray.Length);
        using (MemoryStream memoryStream = new MemoryStream(numArray))
        {
          try
          {
            memoryStream.Seek(0L, SeekOrigin.Begin);
            ipcMessage = new BinaryFormatter().Deserialize((Stream) memoryStream) as Telerik.WinControls.CodedUI.IPCMessage;
          }
          catch (Exception ex)
          {
            ipcMessage = (Telerik.WinControls.CodedUI.IPCMessage) null;
          }
        }
        IPCServer.UnmapViewOfFile(num);
      }
      return ipcMessage;
    }

    public static bool SetServerObject(IntPtr hFile, Telerik.WinControls.CodedUI.IPCMessage data)
    {
      bool flag = true;
      if (data == null)
        return false;
      if (hFile != IntPtr.Zero)
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          try
          {
            memoryStream.Seek(0L, SeekOrigin.Begin);
            new BinaryFormatter().Serialize((Stream) memoryStream, (object) data);
          }
          catch (Exception ex)
          {
            flag = false;
          }
          byte[] array = memoryStream.ToArray();
          IntPtr num = IPCServer.MapViewOfFile(hFile, 2U, 0U, 0U, (UIntPtr) 100000U);
          Marshal.WriteInt32(num, array.Length);
          Marshal.Copy(array, 0, new IntPtr(num.ToInt64() + 4L), array.Length);
          IPCServer.UnmapViewOfFile(num);
        }
      }
      return flag;
    }
  }
}
