using System;

namespace GenricFrame.AppCode.Interfaces
{
    public interface IResponse<T>
    {
        int StatusCode { get; set; }
        string ResponseText { get; set; }
        Exception Exception { get; set; }
        T Result { get; set; }
    }
}
