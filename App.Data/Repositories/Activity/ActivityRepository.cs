using App.Common.Enums;
using App.Common.Extensions;
using App.Data.Repositories.BaseRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Data.Repositories.ActivityRepository
{
    public class ActivityRepository : BaseRepository<Models.Schedule.Activity, DTO.Schedule.Activity>, IActivityRepository
    {
        public ActivityRepository(ApplicationDBContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<IEnumerable<DTO.Schedule.Activity>> GetActivitiesByPropertyId(DTO.Schedule.Activity act)
        {
            var result = await _context.Activities
                         .Where(a => a.StatusId == (int)AppStatus.Active &&
                                a.Property_Id == act.Property_Id &&
                                a.Schedule.Date == act.Schedule.Date &&
                                a.Schedule.Hour == act.Schedule.Hour)
                         .ToListAsync();
            return result.Select(a => _mapper.Map<DTO.Schedule.Activity>(a));
        }
        public async Task<IEnumerable<DTO.Schedule.Activity>> GetActivitiesById(DTO.Schedule.Activity act)
        {
            var activity = await _context.Activities.FirstOrDefaultAsync(w => w.Id == act.Id);

            var result = await _context.Activities
                         .Where(a => a.StatusId == (int)AppStatus.Active &&
                                a.Property_Id == activity.Property_Id &&
                                a.Schedule.Date == act.Schedule.Date &&
                                a.Schedule.Hour == act.Schedule.Hour)
                         .ToListAsync();
            return result.Select(a => _mapper.Map<DTO.Schedule.Activity>(a));
        }
        public async Task<bool> UpdateActivity(DTO.Schedule.ModifyActivity act)
        {
            var activity = await _context.Activities.FirstOrDefaultAsync(w => w.Id == act.ActivityId);
            if (activity != null)
            {
                activity.Updated_At = DateTime.UtcNow.CentralStandardTime();

                activity.Schedule = act.Schedule;
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> CancelActivity(int ActivityId)
        {
            var activity = await _context.Activities.FirstOrDefaultAsync(w => w.Id == ActivityId);
            if (activity != null)
            {
                activity.Updated_At = DateTime.UtcNow.CentralStandardTime();

                activity.StatusId = (int)AppStatus.Cancel;
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<IEnumerable<DTO.Schedule.ActivityDetail>> GetActivities(DTO.SearchParams searchParams)
        {
            var currentDay = DateTime.UtcNow.CentralStandardTime();
            var activities = await (from a in _context.Activities
                                    join p in _context.Properties on a.Property_Id equals p.Id
                                    join st in _context.Statuses on a.StatusId equals st.Id
                                    select new DTO.Schedule.ActivityDetail
                                    {
                                        Id = a.Id,
                                        StatusId = a.StatusId,
                                        Schedule = a.Schedule,
                                        Title = a.Title,
                                        Created_At = a.Created_At,
                                        Status = st.Name,
                                        Condicion = a.StatusId == (int)AppStatus.Active && a.Schedule.Date >= currentDay.Date ? "Pendiente a realizar" :
                                                   a.StatusId == (int)AppStatus.Active && a.Schedule.Date < currentDay.Date ? "Atrasada" :
                                                   a.StatusId == (int)AppStatus.Done ? "Finalizada" : "No encontrada",
                                        Property = new DTO.Schedule.Property
                                        {
                                            Id = p.Id,
                                            Title = p.Title,
                                            Address = p.Address
                                        },
                                        Survey = (from s in _context.Surveys
                                                  where a.Id == s.Activity_Id
                                                  select new DTO.Schedule.Survey
                                                  {
                                                      Activity_Id = a.Id,
                                                      Answers = s.Answers
                                                  }).FirstOrDefault()

                                    }).ToListAsync();

            if (!searchParams.Filter)
            {
                var conditionDate1 = DateTime.UtcNow.CentralStandardTime().AddDays(-3); //Fecha actual menos 3 días
                var conditionDate2 = DateTime.UtcNow.CentralStandardTime().AddDays(15); //Fecha actual más 2 semanas

                var result = activities.Where(a => conditionDate1.Date <= a.Schedule.Date && a.Schedule.Date <= conditionDate2.Date);
                return result.Select(a => _mapper.Map<DTO.Schedule.ActivityDetail>(a));
            }
            else
            {
                var result = activities.Where(a => a.StatusId == searchParams.StatusId
                                || ((searchParams.InitialDate == null || a.Schedule.Date >= searchParams.InitialDate.Value.Date)
                                    && (searchParams.FinalDate == null || a.Schedule.Date <= searchParams.FinalDate.Value.Date)));
                return result.Select(a => _mapper.Map<DTO.Schedule.ActivityDetail>(a));
            }
        }
    }
}
