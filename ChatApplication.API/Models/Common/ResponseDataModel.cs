using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.API.Models.Common
{
    public class ResponseDataModel<T>:Response
    {
        public T Data { get; set; }
    }
}
