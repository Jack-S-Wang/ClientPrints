using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClinetPrints.CreatContorl
{
    public class PanControlWheel:Panel
    {
       //不执行该父类的方法
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            
        }
    }
}
