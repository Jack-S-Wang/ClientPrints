using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.TreeNode
{
    [Serializable()]
    public class GroupTreeNode:System.Windows.Forms.TreeNode
    {
        public GroupTreeNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public GroupTreeNode(string text,int imageNum)
        {
            this.Name = text;
            this.Text = text;
            this.ImageIndex = imageNum;
            this.SelectedImageIndex = imageNum;
            ForeColor = System.Drawing.Color.Green;
        }
        [NonSerialized()]
        public new string ToolTipText;
    }
}
