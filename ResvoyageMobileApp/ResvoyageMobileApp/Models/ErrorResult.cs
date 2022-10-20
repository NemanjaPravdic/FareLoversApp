using System;
using System.Collections.Generic;
using System.Text;

namespace ResvoyageMobileApp.Models
{
    public class ErrorResult
    {
        public int StatusCode { get; set; }
        public string ErrorId { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
