﻿using System;

namespace Webox.DAL.Entities
{
    public class Review
    {
        public string ReviewId { get; set; }
        public string ReviewText { get; set; }
        public DateTime PublishDateTime { get; set; }
        public float Rating { get; set; }

        public string AccountId { get; set; }
        public UserAccount Account { get; set; }

        public string LaptopId { get; set; }
        public Laptop Laptop { get; set; }
    }
}
