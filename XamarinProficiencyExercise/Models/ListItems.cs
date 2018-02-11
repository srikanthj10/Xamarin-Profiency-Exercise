using System;
using System.Collections.Generic;

namespace XamarinProficiencyExercise.Models
{
    public class Row
    {
        public string title { get; set; }
        public string description { get; set; }
        public string imageHref { get; set; }
    }

    public class RootObject
    {
        public string title { get; set; }
        public List<Row> rows { get; set; }
    }
}
