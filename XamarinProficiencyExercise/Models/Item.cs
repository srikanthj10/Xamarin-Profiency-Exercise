using System;
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
        public string Text
        {
            get => this.title;
            set => this.Text = this.title;
        }
        public string description { get; set; }
        public string imageHref { get; set; }

        public static implicit operator ObservableCollection<object>(Item v)
        {
            throw new NotImplementedException();
        }
    }
}
