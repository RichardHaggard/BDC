// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ExprEditorDragDropManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Data.Expressions;

namespace Telerik.WinControls.UI
{
  internal class ExprEditorDragDropManager : IDisposable
  {
    private static ExprEditorDragDropManager DragDropManagerInstance;
    private RadTreeView treeControl;
    private RadTextBox textBox;
    private Point downPt;
    private bool dragging;
    private Form outlineForm;
    private bool listItemClicked;

    public static void Attach(RadTreeView listControl, RadTextBox textBox)
    {
      ExprEditorDragDropManager.Detach();
      ExprEditorDragDropManager.DragDropManagerInstance = new ExprEditorDragDropManager(listControl, textBox);
    }

    public static void Detach()
    {
      if (ExprEditorDragDropManager.DragDropManagerInstance == null)
        return;
      ExprEditorDragDropManager.DragDropManagerInstance.Dispose();
      ExprEditorDragDropManager.DragDropManagerInstance = (ExprEditorDragDropManager) null;
    }

    private static bool IsRealDragging(Point mousePosition, Point initialMousePosition)
    {
      if (Math.Abs(mousePosition.X - initialMousePosition.X) < SystemInformation.DragSize.Width)
        return Math.Abs(mousePosition.Y - initialMousePosition.Y) >= SystemInformation.DragSize.Height;
      return true;
    }

    public ExprEditorDragDropManager(RadTreeView treeControl, RadTextBox textBox)
    {
      this.treeControl = treeControl;
      this.textBox = textBox;
      this.treeControl.MouseUp += new MouseEventHandler(this.listControl_MouseUp);
      this.treeControl.MouseDown += new MouseEventHandler(this.listControl_MouseDown);
      this.treeControl.MouseMove += new MouseEventHandler(this.listControl_MouseMove);
    }

    private void listControl_MouseDown(object sender, MouseEventArgs e)
    {
      this.downPt = e.Location;
      this.listItemClicked = false;
      RadElement elementAtPoint = this.treeControl.ElementTree.GetElementAtPoint(e.Location);
      TreeNodeElement treeNodeElement = elementAtPoint as TreeNodeElement;
      if (treeNodeElement == null && elementAtPoint != null)
        treeNodeElement = elementAtPoint.FindDescendant<TreeNodeElement>();
      if (treeNodeElement == null)
        return;
      this.listItemClicked = true;
    }

    private void listControl_MouseMove(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left || !this.listItemClicked)
        return;
      if (!this.dragging && ExprEditorDragDropManager.IsRealDragging(e.Location, this.downPt))
      {
        RadElement elementAtPoint = this.treeControl.ElementTree.GetElementAtPoint(e.Location);
        TreeNodeElement treeNodeElement = elementAtPoint as TreeNodeElement;
        if (treeNodeElement == null && elementAtPoint != null)
          treeNodeElement = elementAtPoint.FindDescendant<TreeNodeElement>();
        if (treeNodeElement == null)
          return;
        this.treeControl.SelectedNode = treeNodeElement.Data;
        this.dragging = true;
        this.treeControl.Capture = true;
        this.treeControl.FindForm().Cursor = Cursors.No;
        this.outlineForm = TelerikHelper.CreateOutlineForm();
        this.outlineForm.ShowInTaskbar = false;
        this.outlineForm.ShowIcon = false;
        this.outlineForm.BackgroundImage = (Image) treeNodeElement.GetAsBitmap(Brushes.Transparent, 0.0f, new SizeF(1f, 1f));
        this.outlineForm.FormBorderStyle = FormBorderStyle.None;
        this.outlineForm.Size = this.outlineForm.BackgroundImage.Size;
        this.outlineForm.MinimumSize = this.outlineForm.BackgroundImage.Size;
        this.outlineForm.Location = new Point(Cursor.Position.X + 2, Cursor.Position.Y + 2);
        this.outlineForm.TopMost = true;
        this.outlineForm.Show();
      }
      else
      {
        if (!this.dragging)
          return;
        Point mousePosition = Control.MousePosition;
        Point client = this.treeControl.FindForm().PointToClient(mousePosition);
        this.outlineForm.Location = new Point(mousePosition.X + 5, mousePosition.Y + 5);
        if (this.textBox.Bounds.Contains(client))
          this.treeControl.FindForm().Cursor = Cursors.Arrow;
        else
          this.treeControl.FindForm().Cursor = Cursors.No;
      }
    }

    private void listControl_MouseUp(object sender, MouseEventArgs e)
    {
      this.listItemClicked = false;
      if (!this.dragging)
        return;
      if (this.outlineForm != null)
      {
        this.outlineForm.Close();
        this.outlineForm.Dispose();
        this.outlineForm = (Form) null;
      }
      if (this.textBox.Bounds.Contains(this.treeControl.FindForm().PointToClient(this.treeControl.PointToScreen(e.Location))))
      {
        ExpressionItem dataBoundItem = this.treeControl.SelectedNode.DataBoundItem as ExpressionItem;
        if (dataBoundItem != null)
        {
          this.textBox.Text = this.textBox.Text + dataBoundItem.Value + " ";
          this.textBox.SelectionStart = this.textBox.Text.Length;
          this.textBox.Focus();
        }
      }
      this.treeControl.FindForm().Cursor = Cursors.Arrow;
      this.treeControl.Capture = false;
      this.dragging = false;
    }

    public void Dispose()
    {
      if (this.treeControl == null)
        return;
      this.treeControl.MouseUp -= new MouseEventHandler(this.listControl_MouseUp);
      this.treeControl.MouseDown -= new MouseEventHandler(this.listControl_MouseDown);
      this.treeControl.MouseMove -= new MouseEventHandler(this.listControl_MouseMove);
    }
  }
}
