// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ComponentThemableElementTree
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace Telerik.WinControls
{
  public class ComponentThemableElementTree : RadElementTree, IDisposable, IThemeChangeListener
  {
    private bool enableTheming = true;
    private string themeName = "";
    private bool enableApplicationThemeName = true;
    private string themeClassName;
    private int animationSuspendCounter;
    private Theme theme;
    private int styleVersion;
    private bool fallbackToControlDefault;

    public ComponentThemableElementTree(IComponentTreeHandler owner)
      : base(owner)
    {
      ThemeResolutionService.SubscribeForThemeChanged((IThemeChangeListener) this);
    }

    protected override void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      this.RootElement.Dispose();
      ThemeResolutionService.UnsubscribeFromThemeChanged((IThemeChangeListener) this);
    }

    public bool EnableTheming
    {
      get
      {
        return this.enableTheming;
      }
      set
      {
        if (this.enableTheming == value)
          return;
        this.enableTheming = value;
      }
    }

    public Theme Theme
    {
      get
      {
        return this.theme;
      }
    }

    [Category("StyleSheet")]
    [Description("Gets or sets theme name.")]
    [DefaultValue("")]
    [Editor("Telerik.WinControls.UI.Design.ThemeNameEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Browsable(true)]
    public string ThemeName
    {
      get
      {
        return this.themeName;
      }
      set
      {
        if (!(value != this.themeName))
          return;
        this.theme = (Theme) null;
        string themeName = this.themeName;
        this.themeName = value != null ? value : "";
        if (!this.ComponentTreeHandler.Initializing)
          this.ApplyThemeToElementTree();
        else
          this.RootElement.IsThemeApplied = false;
        this.ComponentTreeHandler.CallOnThemeNameChanged(new ThemeNameChangedEventArgs(themeName, this.themeName));
      }
    }

    [Category("StyleSheet")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual string ThemeClassName
    {
      get
      {
        if (this.themeClassName != null)
          return this.themeClassName;
        return this.ComponentTreeHandler.GetType().FullName;
      }
      set
      {
        this.themeClassName = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public int StyleVersion
    {
      get
      {
        return this.styleVersion;
      }
      set
      {
        this.styleVersion = value;
      }
    }

    [Browsable(false)]
    [Category("StyleSheet")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool FallbackToControlDefault
    {
      get
      {
        return this.fallbackToControlDefault;
      }
      set
      {
        this.fallbackToControlDefault = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool EnableApplicationThemeName
    {
      get
      {
        return this.enableApplicationThemeName;
      }
      set
      {
        if (this.enableApplicationThemeName == value)
          return;
        this.enableApplicationThemeName = value;
        this.theme = (Theme) null;
        this.ApplyThemeToElementTree();
      }
    }

    void IThemeChangeListener.OnThemeChanged(ThemeChangedEventArgs e)
    {
      if (!(e.ThemeName == this.ThemeName) && (!(e.ThemeName == ThemeResolutionService.ControlDefaultThemeName) || !(this.ThemeName == string.Empty)) && !(e.ThemeName == ThemeResolutionService.ApplicationThemeName))
        return;
      this.theme = (Theme) null;
      if (this.ComponentTreeHandler.Initializing)
        return;
      if (this.Control.InvokeRequired)
        this.Control.Invoke((Delegate) (() => this.ApplyThemeToElementTree()));
      else
        this.ApplyThemeToElementTree();
    }

    string IThemeChangeListener.ControlThemeClassName
    {
      get
      {
        if (this.ComponentTreeHandler != null)
          return this.ComponentTreeHandler.ThemeClassName;
        return (string) null;
      }
    }

    public void SuspendAnimations()
    {
      ++this.animationSuspendCounter;
    }

    public void ResumeAnimations()
    {
      --this.animationSuspendCounter;
    }

    public bool AnimationsEnabled
    {
      get
      {
        return this.animationSuspendCounter == 0;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void ApplyThemeToElementTree()
    {
      this.EnsureTheme();
      if (this.theme == null)
        return;
      StyleGroup styleGroup = this.theme.FindStyleGroup(this.Control);
      if (styleGroup == null && this.FallbackToControlDefault)
      {
        this.theme = ThemeRepository.ControlDefault;
        styleGroup = this.theme.FindStyleGroup(this.Control);
      }
      this.RootElement.SetThemeApplied(false);
      this.Control.SuspendLayout();
      IComponentTreeHandler control = this.Control as IComponentTreeHandler;
      control?.SuspendUpdate();
      this.SuspendAnimations();
      int num1 = (int) this.RootElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Inherited | ValueResetFlags.Style);
      int num2 = (int) this.RootElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Inherited | ValueResetFlags.Style);
      int num3 = (int) this.RootElement.ResetValue(VisualElement.FontProperty, ValueResetFlags.Inherited | ValueResetFlags.Style);
      this.ApplyStyleCore((RadObject) this.RootElement, styleGroup, (RadObject) this.RootElement, true);
      this.RootElement.IsThemeApplied = true;
      this.ResumeAnimations();
      control?.ResumeUpdate();
      this.Control.ResumeLayout(true);
      this.RootElement.InvalidateArrange(true);
      this.ComponentTreeHandler.ControlThemeChangedCallback();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void ApplyThemeToElement(RadObject element)
    {
      this.ApplyThemeToElement(element, true);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void ApplyThemeToElement(RadObject element, bool recursive)
    {
      if (this.ComponentTreeHandler.Initializing)
        return;
      this.EnsureTheme();
      if (this.theme == null)
        return;
      StyleGroup styleGroup = this.theme.FindStyleGroup(this.Control);
      if (styleGroup == null)
      {
        for (IStylableNode stylableNode = element as IStylableNode; styleGroup == null && stylableNode != null; stylableNode = stylableNode.Parent)
        {
          IStylableElement stylableElement = stylableNode as IStylableElement;
          if (stylableElement != null)
            styleGroup = this.theme.FindStyleGroup((IStylableNode) stylableElement);
        }
      }
      this.SuspendAnimations();
      this.ApplyStyleCore(element, styleGroup, (RadObject) this.RootElement, recursive);
      this.ResumeAnimations();
      if (element != this.RootElement)
        return;
      this.RootElement.IsThemeApplied = true;
    }

    private void ApplyStyleCore(
      RadObject element,
      StyleGroup styleGroup,
      RadObject stopElement,
      bool recursive)
    {
      IStylableElement stylableElement = element as IStylableElement;
      IStylableNode stylableNode1 = element as IStylableNode;
      bool flag1 = stylableElement != null;
      RadItem radItem = element as RadItem;
      RadElement radElement = element as RadElement;
      if (radItem != null)
        flag1 = radItem.CanHaveOwnStyle;
      else if (radElement != null)
        flag1 = !string.IsNullOrEmpty(radElement.Class);
      if (flag1)
      {
        StyleSheet styleSheet = (StyleSheet) null;
        RadControl control = this.Control as RadControl;
        if (control != null)
          styleGroup = control.ResolveStyleGroupForElement(styleGroup, element);
        if (styleGroup != null)
          styleSheet = styleGroup.CreateStyleSheet(element);
        if (styleSheet == null)
        {
          StyleGroup styleGroup1 = this.theme.FindStyleGroup(stylableNode1);
          if (styleGroup1 == null)
          {
            for (IStylableNode parent = stylableNode1.Parent; parent != null && styleGroup1 == null && parent != stopElement; parent = parent.Parent)
            {
              if (parent.Style != null)
              {
                bool flag2 = false;
                foreach (PropertySettingGroup propertySettingGroup in parent.Style.PropertySettingGroups)
                {
                  if (propertySettingGroup.Selector != null && propertySettingGroup.Selector.Type != ElementSelectorTypes.VisualStateSelector && (propertySettingGroup.Selector.ChildSelector != null && propertySettingGroup.Selector.ChildSelector.Type != ElementSelectorTypes.VisualStateSelector) && (propertySettingGroup.Selector.IsCompatible(parent as RadObject) && propertySettingGroup.Selector.ChildSelector.IsCompatible(element)))
                  {
                    stylableNode1.ApplySettings(propertySettingGroup);
                    flag2 = true;
                    break;
                  }
                }
                if (flag2)
                  break;
              }
              styleGroup1 = this.theme.FindStyleGroup(parent);
            }
          }
          if (styleGroup1 != null)
          {
            styleSheet = styleGroup1.CreateStyleSheet(element);
            styleGroup = styleGroup1;
          }
        }
        if (styleSheet == null && stylableElement != null && stylableElement.FallbackToDefaultTheme)
        {
          styleGroup = ThemeRepository.ControlDefault.FindStyleGroup((IStylableNode) stylableElement);
          if (styleGroup != null)
            styleSheet = styleGroup.CreateStyleSheet(element);
        }
        if (radElement != null)
        {
          if (radElement.GetValueSource(RadElement.StyleProperty) != ValueSource.Local || radElement.Style == null)
          {
            int num = (int) radElement.SetDefaultValueOverride(RadElement.StyleProperty, (object) styleSheet);
          }
        }
        else
          stylableNode1.Style = styleSheet;
        if (radElement != null)
          radElement.styleVersion = this.styleVersion;
      }
      if (!recursive)
        return;
      IStylableNode stylableNode2 = element as IStylableNode;
      if (stylableNode2 == null)
        return;
      if (radElement == null || stylableElement != null && stylableElement.FallbackToDefaultTheme)
        stopElement = element;
      foreach (RadObject child in stylableNode2.Children)
        this.ApplyStyleCore(child, styleGroup, stopElement, recursive);
    }

    private void EnsureTheme()
    {
      if (this.theme != null)
        return;
      string themeName = this.ThemeName;
      if (this.EnableApplicationThemeName && !string.IsNullOrEmpty(ThemeResolutionService.ApplicationThemeName))
        themeName = ThemeResolutionService.ApplicationThemeName;
      this.theme = ThemeRepository.FindTheme(themeName);
      if (this.theme == null)
        return;
      ++this.styleVersion;
    }
  }
}
