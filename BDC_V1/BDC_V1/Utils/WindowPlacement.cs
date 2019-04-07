/////////////////////////////////////////////////////////////////////////
// 
// Project: Win
//          
// File:    WindowPlacement.cs
//
// Brief:   Encapsulates window location functionality
//
// Copyright: Copyright (c) 2014 Haggard And Associates, Inc.
//          All Rights Reserved.
//
// Author:  Richard Lewis Haggard
//
// Date:    10/7/2014
// 	
/////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Windows;
using System.Windows.Interop;

namespace BDC_V1.Utils
{
    /// <summary>
    /// RECT structure required by WINDOWPLACEMENT structure
    /// </summary>
    [Serializable]
    [StructLayout( LayoutKind.Sequential )]
    public struct RECT
    {
        /// <summary>
        /// Left border of rectangle.
        /// </summary>
        public int Left;

        /// <summary>
        /// Top of the rectangle.
        /// </summary>
        public int Top;

        /// <summary>
        /// Right side of the rectangle.
        /// </summary>
        public int Right;

        /// <summary>
        /// Bottom of the rectangle.
        /// </summary>
        public int Bottom;

        /// <summary>
        /// Class constructor with all parts of the rectangle specified.
        /// </summary>
        /// <param name="left">Left of the rectangle.</param>
        /// <param name="top">Top of the rectangle.</param>
        /// <param name="right">Right side of the rectangle.</param>
        /// <param name="bottom">Bottom of the rectangle.</param>
        public RECT( int left, int top, int right, int bottom )
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }
    }


    /// <summary>
    /// POINT structure required by WINDOWPLACEMENT structure
    /// </summary>
    [Serializable]
    [StructLayout( LayoutKind.Sequential )]
    public struct POINT
    {
        /// <summary>
        /// Specifies the horizontal value of the point.
        /// </summary>
        public int X;

        /// <summary>
        /// Specifies the vertical value of the point.
        /// </summary>
        public int Y;

        /// <summary>
        /// Constructor that specifies both the X and Y of the point.
        /// </summary>
        /// <param name="x">Horizontal value.</param>
        /// <param name="y">Vertical value.</param>
        public POINT( int x, int y )
        {
            this.X = x;
            this.Y = y;
        }
    }


    /// <summary>
    /// WINDOWPLACEMENT stores the position, size, and state of a window
    /// </summary>
    [Serializable]
    [StructLayout( LayoutKind.Sequential )]
    public struct WINDOWPLACEMENT
    {
        /// <summary>
        /// Specifies the size of the WINDOWPLACEMENT structure.
        /// </summary>
        public int length;

        /// <summary>
        /// Specifies the window placement flags. These flags control the position of the minimized window
        /// and how the window is restored.
        /// </summary>
        public int flags;

        /// <summary>
        /// This property reflects how the window is to be displayed initially, SW_HIDE, SW_MAXIMIZE and so on.
        /// </summary>
        public int showCmd;

        /// <summary>
        /// This point reflects the position of the the upper left corner of the window when it
        /// is minimized
        /// </summary>
        public POINT minPosition;

        /// <summary>
        /// This point reflects the position of the the upper left corner of the window when it
        /// is maximized.
        /// </summary>
        public POINT maxPosition;

        /// <summary>
        /// This describes the bounding rectangle of the window when it is in its restored position.
        /// </summary>
        public RECT normalPosition;
    }


    /// <summary>
    /// These are the bitwise flags that are used to define how the SetWindowPos command functions.
    /// </summary>
    [Flags()]
    public enum SetWindowPosFlags : uint
    {
        /// <summary>If the calling thread and the thread that owns the window are attached to different input queues,
        /// the system posts the request to the thread that owns the window. This prevents the calling thread from
        /// blocking its execution while other threads process the request.</summary>
        /// <remarks>SWP_ASYNCWINDOWPOS</remarks>
        AsynchronousWindowPosition = 0x4000,
        /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
        /// <remarks>SWP_DEFERERASE</remarks>
        DeferErase = 0x2000,
        /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
        /// <remarks>SWP_DRAWFRAME</remarks>
        DrawFrame = 0x0020,
        /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to
        /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE
        /// is sent only when the window's size is being changed.</summary>
        /// <remarks>SWP_FRAMECHANGED</remarks>
        FrameChanged = 0x0020,
        /// <summary>Hides the window.</summary>
        /// <remarks>SWP_HIDEWINDOW</remarks>
        HideWindow = 0x0080,
        /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the
        /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter
        /// parameter).</summary>
        /// <remarks>SWP_NOACTIVATE</remarks>
        DoNotActivate = 0x0010,
        /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid
        /// contents of the client area are saved and copied back into the client area after the window is sized or
        /// repositioned.</summary>
        /// <remarks>SWP_NOCOPYBITS</remarks>
        DoNotCopyBits = 0x0100,
        /// <summary>Retains the current position (ignores X and Y parameters).</summary>
        /// <remarks>SWP_NOMOVE</remarks>
        IgnoreMove = 0x0002,
        /// <summary>Does not change the owner window's position in the Z order.</summary>
        /// <remarks>SWP_NOOWNERZORDER</remarks>
        DoNotChangeOwnerZOrder = 0x0200,
        /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to
        /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent
        /// window uncovered as a result of the window being moved. When this flag is set, the application must
        /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
        /// <remarks>SWP_NOREDRAW</remarks>
        DoNotRedraw = 0x0008,
        /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
        /// <remarks>SWP_NOREPOSITION</remarks>
        DoNotReposition = 0x0200,
        /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
        /// <remarks>SWP_NOSENDCHANGING</remarks>
        DoNotSendChangingEvent = 0x0400,
        /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
        /// <remarks>SWP_NOSIZE</remarks>
        IgnoreResize = 0x0001,
        /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
        /// <remarks>SWP_NOZORDER</remarks>
        IgnoreZOrder = 0x0004,
        /// <summary>Displays the window.</summary>
        /// <remarks>SWP_SHOWWINDOW</remarks>
        ShowWindow = 0x0040,
    }


    /// <summary>
    /// This class wraps the functionality required in order to allow a window's placement and
    /// state to be saved when it closes and then re-asserted the next time it opens, thus giving
    /// an application the ability to save and re-use its placement across sessions.
    /// </summary>
    /// <remarks>
    /// To use this class there are three things to do:
    /// <para/>
    /// 1) Add a string member to the project's properties to contain the window position information
    /// <para/>
    /// 2) Fetch the position string from the properites and pass it to the WindowPlacement class
    /// when the window is first constructed.
    /// <para/>
    /// 3) When the window is closing ask the WindowPlacement class provide a placement string to
    /// be saved in the application's properties.
    /// </remarks>
    /// <example>
    /// How to use the window placement string on window creation:
    /// <code>
    /// protected override void OnSourceInitialized( EventArgs e )
    /// {
    ///     base.OnSourceInitialized( e );
    ///     
    ///     // Use the extension method in the WindowPlace class to retrieve this 
    ///     // window's previous position and display state, if any.
    ///     this.SetPlacement( Settings.Default.PlacementStatus );
    /// }
    /// </code>
    /// <para/>
    /// How to construct and save the window placement string on window close:
    /// <code>
    /// protected override void OnClosing( CancelEventArgs e )
    /// {
    ///     base.OnClosing( e );
    ///     
    ///    // Use the extension method in the WindowPlace class to save this 
    ///    // window's current position and display state.
    ///     Settings.Default.PlacementStatus = this.GetPlacement();
    ///     Settings.Default.Save();
    /// }
    /// </code>
    /// </example>
    public static class WindowPlacement
    {
        private static Encoding encoding = new UTF8Encoding();
        private static XmlSerializer serializer = new XmlSerializer( typeof( WINDOWPLACEMENT ) );

        // The GetWindowPlacement and SetWindowPlacement functions in the Win32 library know about
        // limits, multiple monitors, normal size when zoomed and other such gotcha's that simply
        // are not easily available from managed functions so it makes sense to use them despite
        // their unmanaged nature.
        [DllImport( "user32.dll" )]
        private static extern bool GetWindowPlacement( IntPtr hWnd, out WINDOWPLACEMENT lpwndpl );

        [DllImport( "user32.dll" )]
        private static extern bool SetWindowPlacement( IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl );

        [DllImport( "user32.dll" )]
        [return: MarshalAs( UnmanagedType.Bool )]
        static extern bool SetWindowPos( IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags );

        private const int SW_HIDE          = 0;
        private const int SW_SHOWNORMAL    = 1;
        private const int SW_SHOWMINIMIZED = 2;

        // ------------------------ Class methods ---------------------------------- //

        /// <summary>
        /// Given a window handle, collect the placement information and return this information
        /// encoded within an XML string.
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <returns>XML string containing the placement values.</returns>
        private static string GetPlacement( IntPtr windowHandle )
        {
            // GetComponent the window's current placement values as a WINDOWPLACEMENT structure.
            WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
            GetWindowPlacement( windowHandle, out placement );

            // ConvertBase2Enum the WINDOWPLACEMENT structure to an XML string.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlTextWriter xmlTextWriter = new XmlTextWriter( memoryStream, Encoding.UTF8 ))
                {
                    serializer.Serialize( xmlTextWriter, placement );
                    byte[] xmlBytes = memoryStream.ToArray();
                    return encoding.GetString( xmlBytes );
                }
            }
        }


        /// <summary>
        /// Given a window handle, collect the placement information and return this information
        /// encoded within an XML string.
        /// </summary>
        /// <param name="window">Window for which WINDOWPLACEMENT values are to be determined.</param>
        /// <returns>XML string containing the placement values.</returns>
        public static string GetPlacement( this Window window )
        {
            return WindowPlacement.GetPlacement( new WindowInteropHelper( window ).Handle );
        }


        /// <summary>
        /// Assert the given a WINDOWPLACEMENT structure encoded as an XML string
        /// to a specified window.
        /// </summary>
        /// <param name="windowHandle">Window to receive the WINDOWPLACEMENT values.</param>
        /// <param name="placementXml">WINDOWPLACEMENT structure expressed as an XML string.</param>
        private static void SetPlacement( IntPtr windowHandle, string placementXml, bool hide )
        {
            do
            {
                // If no WINDOWPLACEMENT then nothing to do.
                // This is normal if the application is displaying for the first time.
                if (string.IsNullOrEmpty( placementXml ))
                {
                    break;
                }

                try
                {
                    // ConvertBase2Enum the XML string into a WINDOWPLACEMENT structure.
                    WINDOWPLACEMENT placement;
                    byte[] xmlBytes = encoding.GetBytes( placementXml );

                    using (MemoryStream memoryStream = new MemoryStream( xmlBytes ))
                    {
                        placement = (WINDOWPLACEMENT)serializer.Deserialize( memoryStream );
                    }

                    // Check the values. If the placement structure says that the window doesn't have a size then
                    // don't try to assert it.
                    if (placement.normalPosition.Left == placement.normalPosition.Right &&
                        placement.normalPosition.Top == placement.normalPosition.Bottom)
                    {
                        break;
                    }

                    placement.length = Marshal.SizeOf( typeof( WINDOWPLACEMENT ) );
                    placement.flags = 0;

                    // Normally, the showCmd is dependent on values saved from the previous session.
                    // This case is special. The Login has to show first. The problwm is that this
                    // code is being called before the Login is shown so we have to disable the
                    // manner in which the window is shown and make sure it is hidden.
                    if (hide)
                        placement.showCmd = SW_SHOWMINIMIZED;
                    else
                        placement.showCmd = (placement.showCmd == SW_SHOWMINIMIZED ? SW_SHOWNORMAL : placement.showCmd);

                    SetWindowPlacement( windowHandle, ref placement );
                }
                catch (InvalidOperationException)
                {
                    // Parsing placement XML failed. Fail silently.
                }
            } while (false);
        }


        /// <summary>
        /// Assert the given a WINDOWPLACEMENT structure encoded as an XML string
        /// to a specified window.
        /// </summary>
        /// <param name="window">Window to be positioned.</param>
        /// <param name="placementXml">WINDOWPLACEMENT structure expressed as an XML string.</param>
        public static void SetPlacement( this Window window, string placementXml, bool hide )
        {
            WindowPlacement.SetPlacement( new WindowInteropHelper( window ).Handle, placementXml, hide );
        }


        static readonly IntPtr HWND_TOP = new IntPtr( 0 );



        /// <summary>
        /// Sets the Window with the handle provieded to the Topmost in the z-order
        /// </summary>
        /// <param name="windowHandle"></param>
        private static void SetTopmost( IntPtr windowHandle )
        {
            if (windowHandle == null)
                return;

            SetWindowPos( windowHandle, HWND_TOP, 0, 0, 800, 600, SetWindowPosFlags.DoNotReposition | SetWindowPosFlags.IgnoreResize | SetWindowPosFlags.IgnoreMove );
        }



        /// <summary>
        /// Sets the calling Window to topmost
        /// </summary>
        /// <param name="window"></param>
        public static void SetTopmost( this Window window )
        {
            WindowPlacement.SetTopmost( new WindowInteropHelper( window ).Handle );
        }

    }
}
