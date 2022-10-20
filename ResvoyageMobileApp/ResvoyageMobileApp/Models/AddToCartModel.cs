using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models
{
    public class AddToCartModel
    {
        public Guid SessionId { get; set; }
        public Guid ItemId { get; set; }
    }
}
