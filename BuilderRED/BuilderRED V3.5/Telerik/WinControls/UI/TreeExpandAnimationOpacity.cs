// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeExpandAnimationOpacity
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class TreeExpandAnimationOpacity : TreeExpandAnimation
  {
    public TreeExpandAnimationOpacity(RadTreeViewElement treeView)
      : base(treeView)
    {
    }

    public override void Expand(RadTreeNode node)
    {
      this.UpdateViewOnExpandChanged(node);
      if (node.Nodes.Count == 0)
        return;
      List<TreeNodeElement> associatedNodes = this.GetAssociatedNodes(node);
      for (int index = associatedNodes.Count - 1; index >= 0; --index)
      {
        TreeNodeElement treeNodeElement = associatedNodes[index];
        new AnimatedPropertySetting(VisualElement.OpacityProperty, (object) 1.0, (object) 1.0, 4, 40)
        {
          StartValue = ((object) 0.0),
          EndValue = ((object) 1.0),
          RemoveAfterApply = true,
          ApplyEasingType = RadEasingType.Linear
        }.ApplyValue((RadObject) treeNodeElement);
      }
    }

    public override void Collapse(RadTreeNode node)
    {
      if (node.Nodes.Count == 0)
      {
        this.UpdateViewOnExpandChanged(node);
      }
      else
      {
        List<TreeNodeElement> associatedNodes = this.GetAssociatedNodes(node);
        for (int index = associatedNodes.Count - 1; index >= 0; --index)
        {
          TreeNodeElement treeNodeElement = associatedNodes[index];
          TreeAnimatedPropertySetting animatedPropertySetting = new TreeAnimatedPropertySetting(VisualElement.OpacityProperty, (object) 1.0, (object) 1.0, 4, 40);
          animatedPropertySetting.StartValue = (object) 1.0;
          animatedPropertySetting.EndValue = (object) 0.0;
          animatedPropertySetting.ApplyEasingType = RadEasingType.Linear;
          if (index == 0)
          {
            animatedPropertySetting.AnimationFinished += new AnimationFinishedEventHandler(this.AnimatedCollapse_Finished);
            animatedPropertySetting.Node = node;
          }
          animatedPropertySetting.ApplyValue((RadObject) treeNodeElement);
        }
      }
    }

    private void AnimatedCollapse_Finished(object sender, AnimationStatusEventArgs e)
    {
      TreeAnimatedPropertySetting animatedPropertySetting = sender as TreeAnimatedPropertySetting;
      animatedPropertySetting.AnimationFinished -= new AnimationFinishedEventHandler(this.AnimatedCollapse_Finished);
      this.UpdateViewOnExpandChanged(animatedPropertySetting.Node);
    }
  }
}
