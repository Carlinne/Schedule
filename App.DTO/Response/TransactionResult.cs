using App.Common.Enums;
using System.Collections.Generic;

namespace App.DTO.Response
{
    public class TransactionResult<T>
    {
        public long TotalCount { get; set; }
        public ResultCode ResultCode { get; set; }
        public List<string> ResultMessages { get; set; }
        public T Result { get; set; }
    }
}
