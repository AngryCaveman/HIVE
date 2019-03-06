using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Hive
{
    class TreeViewNF : System.Windows.Forms.TreeView
    {
        public TreeViewNF()
        {
            // 开启双缓冲
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            // Enable the OnNotifyMessage event so we get a chance to filter out 
            // Windows messages before they get to the form's WndProc
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }
        protected override void WndProc(ref Message m)
        {

            if (m.Msg == 0x0014) // 禁掉清除背景消息

                return;

            base.WndProc(ref m);

        }
    }
}
