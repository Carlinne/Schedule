using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Data.Repositories.ActivityRepository
{
    public interface IActivityRepository
    {
        Task<DTO.Schedule.Activity> GetByIdAsync(object id);
        Task<IEnumerable<DTO.Schedule.Activity>> GetAllAsync();
        int Add(DTO.Schedule.Activity activity);
        void Edit(DTO.Schedule.Activity activity, int id);
        Task<int> SaveChanges();
        Task<IEnumerable<DTO.Schedule.Activity>> GetActivitiesByPropertyId(DTO.Schedule.Activity act);
        Task<IEnumerable<DTO.Schedule.Activity>> GetActivitiesById(DTO.Schedule.Activity act);
        Task<bool> UpdateActivity(DTO.Schedule.ModifyActivity act);
        Task<bool> CancelActivity(int ActivityId);
        Task<IEnumerable<DTO.Schedule.ActivityDetail>> GetActivities(DTO.SearchParams searchParams);
    }
}
