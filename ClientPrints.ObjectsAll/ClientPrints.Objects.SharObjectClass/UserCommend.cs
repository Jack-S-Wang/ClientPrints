using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientPrintsObjectsAll.ClientPrints.Objects.SharObjectClass
{
    /// <summary>
    /// 用户执行命令的记录
    /// </summary>
    public class UserCommend
    {
        private List<string> li = new List<string>();
        public void Add(string str)
        {
            if (li.Count > 5)
            {
                li.RemoveAt(0);
            }
            li.Add(str);
        }
        public StringBuilder getCommendInfo()
        {
            StringBuilder strB = new StringBuilder();
            for (int i = 0; i < li.Count; i++)
            {
                strB.Append(li[i]);
            }
            return strB;
        }
    }
    abstract class Commend
    {
        public UserCommend user;

        protected Commend(UserCommend user)
        {
            this.user = user;
        }
        public abstract void Execute();
    }
    /// <summary>
    /// 用户点击按钮命令
    /// </summary>
    class clickCommend:Commend
    {
        private string name;
        private string text;
        public clickCommend(UserCommend user,string name,string text)
            :base(user)
        {
            this.name = name;
            this.text = text;
        }
        public override void Execute()
        {
            string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":执行了命令！" + "名称=" + name + ";信息=" + text;
            base.user.Add(str);
        }
    }
    class Invoker
    {
        private Commend commend;
        public void setCommend(Commend commend)
        {
            this.commend = commend;
        }
        public void ExecuteCommend()
        {
            commend.Execute();
        }
    }
    public class addCommend
    {
        /// <summary>
        /// 创建并添加用户点击按钮对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        public addCommend(UserCommend user,string name,string text)
        {
            clickCommend cComm = new clickCommend(user, name, text);
            Invoker invoker = new Invoker();
            invoker.setCommend(cComm);
            invoker.ExecuteCommend();
        }
    }
}
