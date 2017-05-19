using System;
using System.Collections.Generic;
using System.Text;

namespace JD.NetCore.SDK
{
    public class CategoryResult
    {
        public int catId { get; set; }
        public int parentId { get; set; }
        public string name { get; set; }
        public int catClass { get; set; }
        public int state { get; set; }
    }
}
