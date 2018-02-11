using System;
using System.Collections.Generic;

namespace XamarinProficiencyExercise
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.title;
            Item = item;
        }
    }
}
