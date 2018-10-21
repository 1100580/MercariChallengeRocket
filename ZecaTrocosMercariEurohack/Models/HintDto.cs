using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZecaTrocosMercariEurohack.Models
{
    public class HintDto
    {
        public int Id { get; set; }
        public string CategoryIdentifier { get; set; }
        public string HintText { get; set; }
        public string CsFields { get; set; }
    }
}