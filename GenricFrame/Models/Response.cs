using GenricFrame.AppCode.Interfaces;
using System;

namespace GenricFrame.Models
{
    public class Response<T> : IResponse<T>
    {
        public int StatusCode { get; set; }
        public string ResponseText { get; set; }
        public Exception Exception { get; set; }
        public T Result { get; set; }
    }
}
