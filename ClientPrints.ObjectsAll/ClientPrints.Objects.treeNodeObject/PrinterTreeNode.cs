using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Drawing;
using System.Text;
using ClientPrintsObjectsAll.ClientPrints.Objects.Printers;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.treeNodeObject
{

    [Serializable]
    public class PrinterTreeNode:System.Windows.Forms.TreeNode, ISerializable
    {
        protected PrinterTreeNode(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private PrinterObjects _PrinterObject;
        /// <summary>
        /// 打印机的对象属性
        /// </summary>
        public PrinterObjects PrinterObject {
            get { return _PrinterObject; }
            set {
                Name = value.onlyAlias;
                if (Text == "")
                {
                Text = value.alias + "(" + value.model + ")";
                }
                switch (value.stateCode)
                {
                    case 1://空闲
                        ImageIndex = 1;
                        SelectedImageIndex = 1;
                        ForeColor = Color.Green;
                        break;
                    case 2://就绪
                        ImageIndex = 1;
                        SelectedImageIndex = 1;
                        ForeColor = Color.Green;
                        break;
                    case 3://工作中
                        ImageIndex = 2;
                        SelectedImageIndex = 2;
                        ForeColor = Color.Blue;
                        break;
                    case 4://繁忙
                        ImageIndex = 3;
                        SelectedImageIndex = 3;
                        ForeColor = Color.Yellow;
                        break;
                    case 5://暂停
                        ImageIndex = 4;
                        SelectedImageIndex = 4;
                        ForeColor = Color.Orange;
                        break;
                    case 6:
                        ImageIndex = 5;
                        SelectedImageIndex = 5;
                        ForeColor = Color.Red;
                        break;
                }
                ToolTipText = value.stateMessage;
                StateCode = value.stateCode.ToString();
                _PrinterObject = value;
            }
        }

        /// <summary>
        /// 借用Tag值来设置状态码的先后顺序
        /// </summary>
        public string StateCode {
            get { return Tag as string; }
            set { Tag = value; }
        }

        /// <summary>
        /// 有打印机对象属性创建的树形节点
        /// </summary>
        /// <param name="po"></param>
        public PrinterTreeNode(PrinterObjects po)
        {
            PrinterObject = po;
        }

        /// <summary>
        /// 无打印机对象属性创建的树形节点
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        public PrinterTreeNode(string name,string text)
        {
            Name = name;
            Text = text;
            ImageIndex = 6;
            SelectedImageIndex = 6;
            ForeColor = Color.Gray;
            ToolTipText = "离线";
            StateCode = "0";
        }

        /// <summary>
        /// 设置离线
        /// </summary>
        public void SetOffline()
        {
            ImageIndex = 6;
            SelectedImageIndex = 6;
            ForeColor = Color.Gray;
            ToolTipText = "离线";
            StateCode = "0";
        }

        /// <summary>
        /// 在线状态更新内容
        /// </summary>
        /// <param name="stateCode">状态码</param>
        public void SetState(int stateCode)
        {
            switch (stateCode)
            {
                case 1://空闲
                    ImageIndex = 1;
                    SelectedImageIndex = 1;
                    ForeColor = Color.Green;
                    break;
                case 2://就绪
                    ImageIndex = 1;
                    SelectedImageIndex = 1;
                    ForeColor = Color.Green;
                    break;
                case 3://工作中
                    ImageIndex = 2;
                    SelectedImageIndex = 2;
                    ForeColor = Color.Blue;
                    break;
                case 4://繁忙
                    ImageIndex = 3;
                    SelectedImageIndex = 3;
                    ForeColor = Color.Yellow;
                    break;
                case 5://暂停
                    ImageIndex = 4;
                    SelectedImageIndex = 4;
                    ForeColor = Color.Orange;
                    break;
                case 6:
                    ImageIndex = 5;
                    SelectedImageIndex = 5;
                    ForeColor = Color.Red;
                    break;
            }
            StateCode = stateCode.ToString();
        }
        /// <summary>
        /// 重命名
        /// </summary>
        public string rename(string name)
        {
            string oldtext = Text;
            int index = 0;
            for(int i=0;i<oldtext.Length;i++)
            {
                if (oldtext[i] == '(')
                {
                    index = i;
                    break;
                }
            }
            Text = name + oldtext.Substring(index);
            return Text;
        }
    }
}
