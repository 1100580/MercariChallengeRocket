using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZecaTrocosMercariEurohack.Models
{
    public class Prediction
    {
        public string label { get; set; }
        public double probability { get; set; }
    }

    public class Result
    {
        public List<Prediction> prediction { get; set; }
        public string file { get; set; }
    }

    public class CategorizeResponse
    {
        public string message { get; set; }
        public List<Result> result { get; set; }
    }
}