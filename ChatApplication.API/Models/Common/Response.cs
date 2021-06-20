using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.API.Models.Common
{
    public sealed class Response<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool isSuccess { get; set; }
    }
}
