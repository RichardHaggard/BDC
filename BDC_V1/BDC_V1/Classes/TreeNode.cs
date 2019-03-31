using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BDC_V1.Enumerations;
using JetBrains.Annotations;
using Prism.Mvvm;

namespace BDC_V1.Classes
{
    public class TreeNode : BindableBase
    {
        public EnumTreeNodeType NodeType
        {
            get => _nodeType;
            set => SetProperty(ref _nodeType, value);
        }
        private EnumTreeNodeType _nodeType;

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        private string _description;

        public IList<TreeNode> Children { get; } = new List<TreeNode>();

        public TreeNode()
        {
        }

        // copy constructor
        public TreeNode(TreeNode srcNode)
        {
            NodeType = srcNode.NodeType;
            Description = srcNode.Description;

            foreach (var item in srcNode.Children)
            {
                Children.Add(new TreeNode(item));
            }
        }

        [CanBeNull]
        public static TreeNode FindNode(TreeNode srcNode, EnumTreeNodeType nodeType, string description)
        {
            if ((srcNode.NodeType == nodeType) && (srcNode.Description == description))
                return srcNode;

            return srcNode.Children
                .Select(item => FindNode(item, nodeType, description))
                .FirstOrDefault(tmp => tmp != null);
        }

        public override string ToString()
        {
            return Description;
        }

        public static TreeViewItem BuildTree(TreeNode srcNode, Predicate<TreeNode> filter)
        {
            var node = new TreeViewItem
            {
                Header = srcNode, 
                Foreground = Brushes.Black
            };

            switch (srcNode.NodeType)
            {
                case EnumTreeNodeType.FacilityNode:
                case EnumTreeNodeType.HeaderNode:
                    node.FontSize *= 1.2;
                    node.FontWeight = FontWeights.Bold;
                    break;
                case EnumTreeNodeType.SystemNode:
                case EnumTreeNodeType.ComponentNode:
                    node.FontSize *= 1;
                    node.FontWeight = FontWeights.Normal;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            foreach (var item in srcNode.Children)
            {
                if (filter(item)) node.Items.Add(BuildTree(item, filter));
            }

            return node;
        }
    }
}
