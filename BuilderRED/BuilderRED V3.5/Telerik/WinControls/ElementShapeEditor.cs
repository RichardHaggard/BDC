// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ElementShapeEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Telerik.WinControls
{
  public class ElementShapeEditor : UITypeEditor
  {
    private const int MaxLoaderExceptionsInMessageBox = 15;
    private IWindowsFormsEditorService editorService;
    private ArrayList shapes;
    private bool indexChanged;

    public override UITypeEditorEditStyle GetEditStyle(
      ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }

    public override object EditValue(
      ITypeDescriptorContext context,
      System.IServiceProvider provider,
      object value)
    {
      this.shapes = new ArrayList();
      IDesignerHost designerHost = context.Container as IDesignerHost ?? provider.GetService(typeof (IDesignerHost)) as IDesignerHost;
      this.editorService = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
      System.Windows.Forms.ListBox listBox = this.CreateListBox(designerHost, value);
      this.indexChanged = false;
      this.editorService.DropDownControl((Control) listBox);
      if (!this.indexChanged)
        return value;
      if (listBox.SelectedIndex == 0)
        return (object) null;
      if (listBox.SelectedIndex - 1 > this.shapes.Count - 1)
      {
        bool flag = true;
        if (designerHost == null && value is CustomShape)
          flag = false;
        if (listBox.SelectedIndex - 1 != this.shapes.Count || !flag)
          return (object) this.EditPoints((CustomShape) value);
        CustomShape newShape = this.CreateNewShape(designerHost);
        CustomShape customShape = this.EditPoints(newShape);
        newShape.AsString = customShape.AsString;
        return (object) newShape;
      }
      object obj = this.shapes[listBox.SelectedIndex - 1];
      if ((object) (obj as System.Type) != null)
      {
        if (designerHost != null)
        {
          foreach (IComponent component in (ReadOnlyCollectionBase) designerHost.Container.Components)
          {
            if ((object) component.GetType() == (object) (System.Type) obj)
            {
              obj = (object) component;
              break;
            }
          }
          if ((object) (obj as System.Type) != null)
            obj = (object) designerHost.CreateComponent((System.Type) obj);
        }
        else
          obj = Activator.CreateInstance((System.Type) obj);
      }
      this.shapes.Clear();
      return obj;
    }

    private static bool IsTelerikAssembly(Assembly asm)
    {
      if ((object) asm == null)
        return false;
      if (asm.FullName.Contains("Telerik"))
        return true;
      foreach (AssemblyName referencedAssembly in asm.GetReferencedAssemblies())
      {
        if (referencedAssembly.FullName.Contains("Telerik"))
          return true;
      }
      return false;
    }

    private CustomShape CreateNewShape(IDesignerHost designerHost)
    {
      CustomShape customShape = designerHost == null ? new CustomShape() : (CustomShape) designerHost.CreateComponent(typeof (CustomShape));
      customShape.CreateRectangleShape(20f, 20f, 200f, 100f);
      return customShape;
    }

    private CustomShape EditPoints(CustomShape shape)
    {
      return new CustomShapeEditorForm().EditShape(shape);
    }

    private void listBox_SelectedValueChanged(object sender, EventArgs e)
    {
      this.indexChanged = true;
      if (this.editorService == null)
        return;
      this.editorService.CloseDropDown();
    }

    private System.Windows.Forms.ListBox CreateListBox(
      IDesignerHost designerHost,
      object value)
    {
      System.Windows.Forms.ListBox listBox = new System.Windows.Forms.ListBox();
      listBox.SelectedValueChanged += new EventHandler(this.listBox_SelectedValueChanged);
      listBox.Dock = DockStyle.Fill;
      listBox.BorderStyle = BorderStyle.None;
      listBox.ItemHeight = 13;
      listBox.Items.Add((object) "(none)");
      if (designerHost != null)
      {
        foreach (System.Type type in (IEnumerable) ((ITypeDiscoveryService) designerHost.GetService(typeof (ITypeDiscoveryService))).GetTypes(typeof (ElementShape), false))
        {
          if ((object) type != (object) typeof (CustomShape) && !type.IsAbstract && type.IsPublic)
          {
            listBox.Items.Add((object) type.Name);
            this.shapes.Add((object) type);
            if (value != null && (object) value.GetType() == (object) type)
              listBox.SelectedIndex = listBox.Items.Count - 1;
          }
        }
      }
      else
      {
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
          try
          {
            if (ElementShapeEditor.IsTelerikAssembly(assembly))
            {
              foreach (System.Type type in assembly.GetTypes())
              {
                if (type.IsClass && type.IsPublic && (!type.IsAbstract && typeof (ElementShape).IsAssignableFrom(type)) && (object) type != (object) typeof (CustomShape))
                {
                  listBox.Items.Add((object) type.Name);
                  this.shapes.Add((object) type);
                  if (value != null && (object) value.GetType() == (object) type)
                    listBox.SelectedIndex = listBox.Items.Count - 1;
                }
              }
            }
          }
          catch (ReflectionTypeLoadException ex)
          {
            string text = ex.Message + "\n\nLoader Exceptions:\n";
            for (int index = 0; index < Math.Min(15, ex.LoaderExceptions.Length); ++index)
              text = text + ex.LoaderExceptions[index].Message + "\n";
            if (ex.LoaderExceptions.Length > 15)
              text += "More...";
            int num = (int) MessageBox.Show(text, assembly.FullName);
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show(ex.Message, assembly.FullName);
          }
        }
      }
      if (designerHost != null)
      {
        foreach (IComponent component in (ReadOnlyCollectionBase) designerHost.Container.Components)
        {
          if (component is CustomShape)
          {
            listBox.Items.Add((object) component.Site.Name);
            this.shapes.Add((object) component);
            if (component == value)
              listBox.SelectedIndex = listBox.Items.Count - 1;
          }
        }
        listBox.Items.Add((object) "Create new custom shape ...");
        if (value != null && value is CustomShape)
          listBox.Items.Add((object) "Edit points ...");
      }
      else if (value != null && value is CustomShape)
        listBox.Items.Add((object) "Edit points ...");
      else
        listBox.Items.Add((object) "Create new custom shape ...");
      return listBox;
    }
  }
}
