// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Carousel.CarouselPathEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Telerik.WinControls.UI.Carousel
{
  public class CarouselPathEditor : UITypeEditor
  {
    private IWindowsFormsEditorService editorService;
    private bool isDirty;

    public override UITypeEditorEditStyle GetEditStyle(
      ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }

    public override bool IsDropDownResizable
    {
      get
      {
        return true;
      }
    }

    public override object EditValue(
      ITypeDescriptorContext context,
      System.IServiceProvider provider,
      object value)
    {
      this.editorService = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
      Control listBox1 = this.CreateListBox(context, value);
      this.isDirty = false;
      this.editorService.DropDownControl(listBox1);
      if (!this.isDirty)
        return value;
      System.Windows.Forms.ListBox listBox2 = (System.Windows.Forms.ListBox) listBox1;
      if (listBox2.SelectedIndex == 0 || listBox2.SelectedItem == null)
        return (object) new CarouselEllipsePath();
      return (object) ((CarouselPathEditor.CarouselPathListItem) listBox2.SelectedItem).PathInstance;
    }

    private Control CreateListBox(ITypeDescriptorContext context, object value)
    {
      System.Windows.Forms.ListBox listBox = new System.Windows.Forms.ListBox();
      listBox.Dock = DockStyle.Fill;
      listBox.SelectedValueChanged += new EventHandler(this.listBox_SelectedValueChanged);
      listBox.BorderStyle = BorderStyle.None;
      listBox.ItemHeight = 13;
      listBox.Items.Add((object) "(none)");
      CarouselBezierPath carouselBezierPath = new CarouselBezierPath();
      carouselBezierPath.CtrlPoint1 = new Point3D(14.0, 76.0, 70.0);
      carouselBezierPath.CtrlPoint2 = new Point3D(86.0, 76.0, 70.0);
      carouselBezierPath.FirstPoint = new Point3D(10.0, 20.0, 0.0);
      carouselBezierPath.LastPoint = new Point3D(90.0, 20.0, 0.0);
      CarouselEllipsePath carouselEllipsePath = new CarouselEllipsePath();
      carouselEllipsePath.Center = new Point3D(50.0, 50.0, 0.0);
      carouselEllipsePath.FinalAngle = -100.0;
      carouselEllipsePath.InitialAngle = -90.0;
      carouselEllipsePath.U = new Point3D(-20.0, -17.0, -50.0);
      carouselEllipsePath.V = new Point3D(30.0, -25.0, -60.0);
      carouselEllipsePath.ZScale = 500.0;
      listBox.Items.Add((object) new CarouselPathEditor.CarouselPathListItem((ICarouselPath) carouselBezierPath));
      listBox.Items.Add((object) new CarouselPathEditor.CarouselPathListItem((ICarouselPath) carouselEllipsePath));
      return (Control) listBox;
    }

    private void listBox_SelectedValueChanged(object sender, EventArgs e)
    {
      this.isDirty = true;
      this.editorService.CloseDropDown();
    }

    private class CarouselPathListItem
    {
      private ICarouselPath pathInstance;

      public CarouselPathListItem(ICarouselPath pathInstance)
      {
        this.pathInstance = pathInstance;
      }

      public override string ToString()
      {
        return "new " + this.pathInstance.GetType().Name;
      }

      public ICarouselPath PathInstance
      {
        get
        {
          return this.pathInstance;
        }
      }
    }
  }
}
