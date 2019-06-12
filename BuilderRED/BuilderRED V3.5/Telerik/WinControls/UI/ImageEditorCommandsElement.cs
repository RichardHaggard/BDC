// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImageEditorCommandsElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class ImageEditorCommandsElement : LightVisualElement
  {
    private RadImageEditorElement imageEditorElement;
    private StackLayoutElement topCommandsStackElement;
    private RadStackViewport commandsStackViewport;
    private RadScrollViewer scrollViewer;
    private RadButtonElement openButton;
    private RadButtonElement saveButton;
    private RadButtonElement undoButton;
    private RadButtonElement redoButton;
    private RadMenuHeaderItem transformItem;
    private RadMenuHeaderItem adjustItem;
    private RadMenuHeaderItem effectsItem;
    private RadMenuItem resizeItem;
    private RadMenuItem canvasResizeItem;
    private RadMenuItem rotate90Item;
    private RadMenuItem rotate180Item;
    private RadMenuItem rotate270Item;
    private RadMenuItem roundCornersItem;
    private RadMenuItem flipHorizontalItem;
    private RadMenuItem flipVerticalItem;
    private RadMenuItem cropItem;
    private RadMenuItem drawTextItem;
    private RadMenuItem drawItem;
    private RadMenuItem drawShapeItem;
    private RadMenuItem panItem;
    private RadMenuItem hueShiftItem;
    private RadMenuItem saturationItem;
    private RadMenuItem contrastItem;
    private RadMenuItem invertColorsItem;
    private RadMenuItem sharpenItem;
    private RadMenuItem blurItem;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.topCommandsStackElement = this.CraeteTopCommandsStackElement();
      this.commandsStackViewport = this.CreateCommandsStackViewport();
      this.scrollViewer = this.CreateScrollViewer();
      this.commandsStackViewport.Children.Add((RadElement) this.topCommandsStackElement);
      this.scrollViewer.Viewport = (RadElement) this.commandsStackViewport;
      this.Children.Add((RadElement) this.scrollViewer);
      this.CreateTopCommands();
      this.CreateCommands();
    }

    protected virtual void CreateTopCommands()
    {
      this.openButton = new RadButtonElement();
      this.openButton.ToolTipText = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("OpenCommandTooltip");
      this.openButton.Click += new EventHandler(this.OpenImageClick);
      this.topCommandsStackElement.Children.Add((RadElement) this.openButton);
      this.saveButton = new RadButtonElement();
      this.saveButton.ToolTipText = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("SaveCommandTooltip");
      this.saveButton.Click += new EventHandler(this.SaveImageClick);
      this.topCommandsStackElement.Children.Add((RadElement) this.saveButton);
      this.undoButton = new RadButtonElement();
      this.undoButton.ToolTipText = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("UndoCommandTooltip");
      this.undoButton.Click += new EventHandler(this.UndoClick);
      this.topCommandsStackElement.Children.Add((RadElement) this.undoButton);
      this.redoButton = new RadButtonElement();
      this.redoButton.ToolTipText = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("RedoCommandTooltip");
      this.redoButton.Click += new EventHandler(this.RedoClick);
      this.topCommandsStackElement.Children.Add((RadElement) this.redoButton);
    }

    protected virtual void CreateCommands()
    {
      this.transformItem = new RadMenuHeaderItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsTransform"));
      this.transformItem.Margin = new Padding(0, 15, 0, 0);
      this.commandsStackViewport.Children.Add((RadElement) this.transformItem);
      this.resizeItem = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsResize"));
      this.resizeItem.Click += (EventHandler) ((param0, param1) => this.ImageEditorElement.ShowResizeDialog());
      this.commandsStackViewport.Children.Add((RadElement) this.resizeItem);
      this.canvasResizeItem = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsCanvasResize"));
      this.canvasResizeItem.Click += (EventHandler) ((param0, param1) => this.ImageEditorElement.ShowCanvasResizeDialog());
      this.commandsStackViewport.Children.Add((RadElement) this.canvasResizeItem);
      this.rotate90Item = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsRotate90"));
      this.rotate90Item.Click += (EventHandler) ((param0, param1) =>
      {
        this.ImageEditorElement.RotateFlip(RotateFlipType.Rotate90FlipNone);
        this.ImageEditorElement.SaveState();
      });
      this.commandsStackViewport.Children.Add((RadElement) this.rotate90Item);
      this.rotate180Item = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsRotate180"));
      this.rotate180Item.Click += (EventHandler) ((param0, param1) =>
      {
        this.ImageEditorElement.RotateFlip(RotateFlipType.Rotate180FlipNone);
        this.ImageEditorElement.SaveState();
      });
      this.commandsStackViewport.Children.Add((RadElement) this.rotate180Item);
      this.rotate270Item = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsRotate270"));
      this.rotate270Item.Click += (EventHandler) ((param0, param1) =>
      {
        this.ImageEditorElement.RotateFlip(RotateFlipType.Rotate270FlipNone);
        this.ImageEditorElement.SaveState();
      });
      this.commandsStackViewport.Children.Add((RadElement) this.rotate270Item);
      this.roundCornersItem = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsRoundCorners"));
      this.roundCornersItem.Click += (EventHandler) ((param0, param1) => this.ImageEditorElement.ShowRoundCornersDialog());
      this.commandsStackViewport.Children.Add((RadElement) this.roundCornersItem);
      this.flipHorizontalItem = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsFlipHorizontal"));
      this.flipHorizontalItem.Click += (EventHandler) ((param0, param1) =>
      {
        this.ImageEditorElement.RotateFlip(RotateFlipType.RotateNoneFlipX);
        this.ImageEditorElement.SaveState();
      });
      this.commandsStackViewport.Children.Add((RadElement) this.flipHorizontalItem);
      this.flipVerticalItem = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsFlipVertical"));
      this.flipVerticalItem.Click += (EventHandler) ((param0, param1) =>
      {
        this.ImageEditorElement.RotateFlip(RotateFlipType.Rotate180FlipX);
        this.ImageEditorElement.SaveState();
      });
      this.commandsStackViewport.Children.Add((RadElement) this.flipVerticalItem);
      this.cropItem = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsCrop"));
      this.cropItem.Click += (EventHandler) ((param0, param1) =>
      {
        if (this.ImageEditorElement.CanvasElement.IsCropping)
          this.ImageEditorElement.StopCrop(false);
        else
          this.ImageEditorElement.StartCrop();
      });
      this.commandsStackViewport.Children.Add((RadElement) this.cropItem);
      this.drawTextItem = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsDrawText"));
      this.drawTextItem.Click += (EventHandler) ((param0, param1) => this.ImageEditorElement.ShowDrawTextDialog());
      this.commandsStackViewport.Children.Add((RadElement) this.drawTextItem);
      this.drawItem = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsDraw"));
      this.drawItem.Click += (EventHandler) ((param0, param1) => this.ImageEditorElement.ShowDrawDialog());
      this.commandsStackViewport.Children.Add((RadElement) this.drawItem);
      this.drawShapeItem = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsDrawShape"));
      this.drawShapeItem.Click += (EventHandler) ((param0, param1) => this.ImageEditorElement.ShowDrawShapeDialog());
      this.commandsStackViewport.Children.Add((RadElement) this.drawShapeItem);
      this.panItem = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsPan"));
      this.panItem.Click += (EventHandler) ((param0, param1) =>
      {
        if (this.ImageEditorElement.CanvasElement.IsPanning)
          this.ImageEditorElement.StopPan();
        else
          this.ImageEditorElement.StartPan();
      });
      this.commandsStackViewport.Children.Add((RadElement) this.panItem);
      this.adjustItem = new RadMenuHeaderItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsAdjust"));
      this.adjustItem.Margin = new Padding(0, 10, 0, 0);
      this.commandsStackViewport.Children.Add((RadElement) this.adjustItem);
      this.hueShiftItem = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsHue"));
      this.hueShiftItem.Click += (EventHandler) ((param0, param1) => this.ImageEditorElement.ShowHueDialog());
      this.commandsStackViewport.Children.Add((RadElement) this.hueShiftItem);
      this.saturationItem = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsSaturation"));
      this.saturationItem.Click += (EventHandler) ((param0, param1) => this.ImageEditorElement.ShowSaturationDialog());
      this.commandsStackViewport.Children.Add((RadElement) this.saturationItem);
      this.contrastItem = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsContrast"));
      this.contrastItem.Click += (EventHandler) ((param0, param1) => this.ImageEditorElement.ShowContrastDialog());
      this.commandsStackViewport.Children.Add((RadElement) this.contrastItem);
      this.invertColorsItem = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsInvertColors"));
      this.invertColorsItem.Click += (EventHandler) ((param0, param1) =>
      {
        this.ImageEditorElement.InvertColors();
        this.ImageEditorElement.SaveState();
      });
      this.commandsStackViewport.Children.Add((RadElement) this.invertColorsItem);
      this.effectsItem = new RadMenuHeaderItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsEffects"));
      this.effectsItem.Margin = new Padding(0, 10, 0, 0);
      this.commandsStackViewport.Children.Add((RadElement) this.effectsItem);
      this.sharpenItem = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsSharpen"));
      this.sharpenItem.Click += (EventHandler) ((param0, param1) => this.ImageEditorElement.ShowSharpenDialog());
      this.commandsStackViewport.Children.Add((RadElement) this.sharpenItem);
      this.blurItem = new RadMenuItem(LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsBlur"));
      this.blurItem.Click += (EventHandler) ((param0, param1) => this.ImageEditorElement.ShowBlurDialog());
      this.commandsStackViewport.Children.Add((RadElement) this.blurItem);
    }

    protected virtual void DisposeCommands()
    {
      this.CommandsStackElement.Children.Clear();
    }

    protected virtual RadScrollViewer CreateScrollViewer()
    {
      return new RadScrollViewer() { UsePhysicalScrolling = true, ShowBorder = false, ShowFill = false };
    }

    protected virtual RadStackViewport CreateCommandsStackViewport()
    {
      RadStackViewport radStackViewport = new RadStackViewport();
      radStackViewport.Orientation = Orientation.Vertical;
      return radStackViewport;
    }

    protected virtual StackLayoutElement CraeteTopCommandsStackElement()
    {
      StackLayoutElement stackLayoutElement = new StackLayoutElement();
      stackLayoutElement.Orientation = Orientation.Horizontal;
      stackLayoutElement.StretchHorizontally = false;
      stackLayoutElement.StretchVertically = false;
      return stackLayoutElement;
    }

    public ImageEditorCommandsElement(RadImageEditorElement owner)
    {
      this.imageEditorElement = owner;
      LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.ImageEditorLocalizationProvider_CurrentProviderChanged);
    }

    public RadButtonElement OpenButton
    {
      get
      {
        return this.openButton;
      }
    }

    public RadButtonElement SaveButton
    {
      get
      {
        return this.saveButton;
      }
    }

    public RadButtonElement UndoButton
    {
      get
      {
        return this.undoButton;
      }
    }

    public RadButtonElement RedoButton
    {
      get
      {
        return this.redoButton;
      }
    }

    public RadImageEditorElement ImageEditorElement
    {
      get
      {
        return this.imageEditorElement;
      }
    }

    public StackLayoutElement TopCommandsStackElement
    {
      get
      {
        return this.topCommandsStackElement;
      }
    }

    public RadStackViewport CommandsStackElement
    {
      get
      {
        return this.commandsStackViewport;
      }
    }

    protected virtual Image ChooseIcon(string name)
    {
      if (this.ElementTree == null)
        return (Image) null;
      string str1 = (this.ElementTree.Control as RadControl).ThemeName;
      if (string.IsNullOrEmpty(str1))
        str1 = ThemeResolutionService.ApplicationThemeName;
      string str2;
      switch (str1)
      {
        case "HighContrastBlack":
        case "VisualStudio2012Dark":
        case "FluentDark":
          str2 = "White";
          break;
        case "Office2013Light":
        case "Office2013Dark":
        case "VisualStudio2012Light":
        case "Windows8":
        case "Material":
        case "MaterialPink":
        case "MaterialTeal":
        case "MaterialBlueGrey":
        case "Fluent":
          str2 = "Gray";
          break;
        case "TelerikMetro":
        case "TelerikMetroBlue":
        case "TelerikMetroTouch":
          str2 = "Green";
          break;
        default:
          str2 = "Default";
          break;
      }
      using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("Telerik.WinControls.UI.RadImageEditor.Icons.{0}.{1}.png", (object) str2, (object) name)))
        return Image.FromStream(manifestResourceStream);
    }

    protected virtual void LocalizeCommands()
    {
      this.openButton.ToolTipText = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("OpenCommandTooltip");
      this.saveButton.ToolTipText = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("SaveCommandTooltip");
      this.undoButton.ToolTipText = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("UndoCommandTooltip");
      this.redoButton.ToolTipText = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("RedoCommandTooltip");
      this.transformItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsTransform");
      this.adjustItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsAdjust");
      this.effectsItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsEffects");
      this.resizeItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsResize");
      this.canvasResizeItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsCanvasResize");
      this.rotate90Item.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsRotate90");
      this.rotate180Item.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsRotate180");
      this.rotate270Item.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsRotate270");
      this.roundCornersItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsRoundCorners");
      this.flipHorizontalItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsFlipHorizontal");
      this.flipVerticalItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsFlipVertical");
      this.cropItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsCrop");
      this.drawTextItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsDrawText");
      this.drawItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsDraw");
      this.drawShapeItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsDrawShape");
      this.panItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsPan");
      this.hueShiftItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsHue");
      this.saturationItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsSaturation");
      this.contrastItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsContrast");
      this.invertColorsItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsInvertColors");
      this.sharpenItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsSharpen");
      this.blurItem.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CommandsBlur");
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.SetCommandIcons();
    }

    public virtual void SetCommandIcons()
    {
      this.OpenButton.Image = this.ChooseIcon("FolderOpen");
      this.SaveButton.Image = this.ChooseIcon("Save");
      this.UndoButton.Image = this.ChooseIcon("Undo");
      this.RedoButton.Image = this.ChooseIcon("Redo");
      this.resizeItem.Image = this.ChooseIcon("Resize");
      this.canvasResizeItem.Image = this.ChooseIcon("Resize");
      this.rotate90Item.Image = this.ChooseIcon("Rotate90CW");
      this.rotate180Item.Image = this.ChooseIcon("Rotate180CW");
      this.rotate270Item.Image = this.ChooseIcon("Rotate90CCW");
      this.roundCornersItem.Image = this.ChooseIcon("RoundCorners");
      this.flipHorizontalItem.Image = this.ChooseIcon("FlipHorizontal");
      this.flipVerticalItem.Image = this.ChooseIcon("FlipVertical");
      this.cropItem.Image = this.ChooseIcon("Crop");
      this.drawTextItem.Image = this.ChooseIcon("DrawText");
      this.drawItem.Image = this.ChooseIcon("Draw");
      this.drawShapeItem.Image = this.ChooseIcon("Shape");
      this.panItem.Image = this.ChooseIcon("Pan");
      this.hueShiftItem.Image = this.ChooseIcon("HueShift");
      this.saturationItem.Image = this.ChooseIcon("Saturation");
      this.contrastItem.Image = this.ChooseIcon("Contrast");
      this.invertColorsItem.Image = this.ChooseIcon("InvertColors");
      this.sharpenItem.Image = this.ChooseIcon("Sharpen");
      this.blurItem.Image = this.ChooseIcon("Blur");
    }

    private void OpenImageClick(object sender, EventArgs e)
    {
      this.ImageEditorElement.OpenImage();
    }

    private void SaveImageClick(object sender, EventArgs e)
    {
      this.ImageEditorElement.SaveImageAs();
    }

    private void UndoClick(object sender, EventArgs e)
    {
      this.ImageEditorElement.Undo();
    }

    private void RedoClick(object sender, EventArgs e)
    {
      this.ImageEditorElement.Redo();
    }

    private void ImageEditorLocalizationProvider_CurrentProviderChanged(object sender, EventArgs e)
    {
      this.LocalizeCommands();
    }
  }
}
