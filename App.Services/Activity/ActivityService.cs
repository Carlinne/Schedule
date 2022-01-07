using App.Common;
using App.Common.Enums;
using App.Common.Extensions;
using App.Data.Repositories.ActivityRepository;
using App.Data.Repositories.Property;
using App.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.Activity
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IPropertyRepository _propertyRepository;


        public ActivityService(IActivityRepository activityRepository,
                                IPropertyRepository propertyRepository)
        {
            this._activityRepository = activityRepository;
            this._propertyRepository = propertyRepository;
        }
        //Agregar actividades
        public async Task<TransactionResult<bool>> Add(DTO.Schedule.Activity dto)
        {
            var result = new TransactionResult<bool>
            {
                Result = false,
                ResultMessages = new List<string>()
            };
            try
            {
                var property = await _propertyRepository.GetByIdAsync(dto.Property_Id);

                //RDN1 -- No se pueden crear actividades si una Propiedad está desactivada
                if (property.StatusId == (int)AppStatus.Active)
                {
                    if (property.Disabled_at != null)
                    {
                        var compareTime = TimeSpan.Compare(DateTime.UtcNow.CentralStandardTime().TimeOfDay, property.Disabled_at.Value.TimeOfDay);
                        if (compareTime == 1)
                        {
                            result.Result = false;
                            result.ResultCode = ResultCode.Alert;
                            result.ResultMessages.Add(Constants.PropertyDisabled);
                            return result;
                        }
                    }
                }
                else
                {
                    result.Result = false;
                    result.ResultCode = ResultCode.Alert;
                    result.ResultMessages.Add(Constants.PropertyDisabled);
                    return result;
                }

                //RDN2 -- No se pueden crear actividades en la misma fecha y hora (para la misma propiedad), tomando en cuenta que cada actividad debe durar máximo una hora
                var activities = await _activityRepository.GetActivitiesByPropertyId(dto);
                if (activities != null && activities.Count() > 0)
                {
                    result.Result = false;
                    result.ResultCode = ResultCode.Alert;
                    result.ResultMessages.Add(Constants.BusySchedule);
                    return result;
                }

                dto.StatusId = (int)AppStatus.Active;
                dto.Created_At = DateTime.UtcNow.CentralStandardTime();
                _activityRepository.Add(dto);
                await _activityRepository.SaveChanges();

                result.Result = true;
                result.ResultCode = ResultCode.Success;
                result.ResultMessages.Add(Constants.ScheduleInsertSuccess);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ResultCode = ResultCode.Fatal;
                result.ResultMessages = new List<string>() { ex.Message };
            }
            return result;
        }

        //Reagendar actividades
        public async Task<TransactionResult<bool>> Edit(DTO.Schedule.ModifyActivity dto)
        {
            var result = new TransactionResult<bool>
            {
                Result = false,
                ResultMessages = new List<string>()
            };
            try
            {
                //RDN2 -- No se pueden crear/actualizar actividades en la misma fecha y hora (para la misma propiedad), tomando en cuenta que cada actividad debe durar máximo una hora

                var activitydto = new DTO.Schedule.Activity
                {
                    Id = dto.ActivityId,
                    Schedule = dto.Schedule
                };

                var activities = await _activityRepository.GetActivitiesById(activitydto);
                if (activities != null && activities.Count() > 0)
                {
                    result.Result = false;
                    result.ResultCode = ResultCode.Alert;
                    result.ResultMessages.Add(Constants.BusySchedule);
                    return result;
                }

                //RDN 1 Solo se puede modificar la fecha en la que se va a realizar la actividad.

                //RDN 2 No se pueden re-agendar actividades canceladas.
                var activity = await _activityRepository.GetByIdAsync(dto.ActivityId);
                if (activity.StatusId == (int)AppStatus.Cancel)
                {
                    result.Result = false;
                    result.ResultCode = ResultCode.Alert;
                    result.ResultMessages.Add(Constants.PropertyCancelled);
                    return result;
                }

                var res = await _activityRepository.UpdateActivity(dto);
                if (res)
                {
                    await _activityRepository.SaveChanges();
                    result.Result = true;
                    result.ResultCode = ResultCode.Success;
                    result.ResultMessages.Add(Constants.UpdateSchedule);
                }
                else
                {
                    result.Result = false;
                    result.ResultCode = ResultCode.Alert;
                    result.ResultMessages.Add(Constants.ScheduleNotFound);
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ResultCode = ResultCode.Fatal;
                result.ResultMessages = new List<string>() { ex.Message };
            }
            return result;
        }

        //Cancelar actividades
        public async Task<TransactionResult<bool>> Cancel(int ActivityId)
        {
            var result = new TransactionResult<bool>
            {
                Result = false,
                ResultMessages = new List<string>()
            };
            try
            {
                //RDN1 -- Solo se puede modificar el estatus de la actividad.

                var res = await _activityRepository.CancelActivity(ActivityId);
                if (res)
                {
                    await _activityRepository.SaveChanges();
                    result.Result = true;
                    result.ResultCode = ResultCode.Success;
                    result.ResultMessages.Add(Constants.CancelledSchedule);
                }
                else
                {
                    result.Result = false;
                    result.ResultCode = ResultCode.Alert;
                    result.ResultMessages.Add(Constants.ScheduleNotFound);
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ResultCode = ResultCode.Fatal;
                result.ResultMessages = new List<string>() { ex.Message };
            }
            return result;
        }

        //Lista de actividades
        public async Task<TransactionResult<List<DTO.Schedule.ActivityDetail>>> GetActivitiesDetail(DTO.SearchParams searchParams)
        {
            var result = new TransactionResult<List<DTO.Schedule.ActivityDetail>>()
            {
                Result = new List<DTO.Schedule.ActivityDetail>()
            };
            try
            {
                var activities = await _activityRepository.GetActivities(searchParams);
                if (activities.ToList().Count() <= 0)
                {
                    result.ResultCode = ResultCode.Alert;
                    result.ResultMessages = new List<string>() { Constants.ShedulesNotFound };
                    return result;
                }
                else
                {
                    result.Result = activities.ToList();
                    result.ResultCode = ResultCode.Success;
                    result.TotalCount = activities.Count();
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = ResultCode.Fatal;
                result.ResultMessages = new List<string>() { ex.Message };
            }
            return result;
        }
    }
}
