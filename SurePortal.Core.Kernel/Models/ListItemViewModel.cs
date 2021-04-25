﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.Models
{
    public class ListItemViewModel
    {
        public ListItemViewModel()
        {

        }
        public ListItemViewModel(string text, string value)
        {
            Text = text;
            Value = value;
        }
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
