﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XamarinProficiencyExercise
{
    public class Row
    {
        public string title { get; set; }
        public string description { get; set; }
        public string imageHref { get; set; }
    }

    public class Item
    {
        public string title { get; set; }
        public List<Row> rows { get; set; }
    }
}
