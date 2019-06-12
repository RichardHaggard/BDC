// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BackstageButtonItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class BackstageButtonItem : BackstageVisualElement
  {
    public static readonly RadProperty IsCurrentProperty = RadProperty.Register("IsCurrent", typeof (bool), typeof (BackstageVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsArrange));

    public BackstageButtonItem()
    {
    }

    public BackstageButtonItem(string text)
    {
      this.Text = text;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = nameof (BackstageButtonItem);
      this.ThemeRole = nameof (BackstageButtonItem);
      this.DrawFill = true;
      this.MinSize = new Size(0, 26);
      this.TextAlignment = ContentAlignment.MiddleLeft;
      this.ImageAlignment = ContentAlignment.MiddleLeft;
      this.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.Padding = new Padding(13, 0, 13, 0);
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      (this.Parent as BackstageItemsPanelElement)?.Owner.OnItemClicked((BackstageVisualElement) this);
    }
  }
}
