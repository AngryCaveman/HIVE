using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive
{
    public class StepRule
    {
        string Step;
        string Paging;
        string Dynamic;
        string Linkre;
        string Item;
        string Cls;
        string Follow;
        public string step
        {
            get { return Step; }
            set { Step = value; }
        }
        public string paging
        {
            get { return Paging; }
            set { Paging = value; }
        }
        public string dynamic
        {
            get { return Dynamic; }
            set { Dynamic = value; }
        }
        public string linkre
        {
            get { return Linkre; }
            set { Linkre = value; }
        }
        public string item
        {
            get { return Item; }
            set { Item = value; }
        }
        public string cls
        {
            get { return Cls; }
            set { Cls = value; }
        }
        public string follow
        {
            get { return Follow; }
            set { Follow = value; }
        }
    }
}
