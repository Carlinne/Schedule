using App.DTO.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Services.Activity
{
    public interface IActivityService
    {
        Task<TransactionResult<bool>> Add(DTO.Schedule.Activity dto);
        Task<TransactionResult<bool>> Edit(DTO.Schedule.ModifyActivity dto);
        Task<TransactionResult<bool>> Cancel(int ActivityId);
        Task<TransactionResult<List<DTO.Schedule.ActivityDetail>>> GetActivitiesDetail(DTO.SearchParams searchParams);
    }
}
