using App.Common;
using App.Common.Enums;
using App.DTO.Response;
using App.Services.Activity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.RestfulAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {
        #region Attributes 
        private readonly IActivityService _activityService;
        #endregion

        #region Constructor
        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }
        #endregion

        #region Methods
        [HttpPost]
        [Route("CreateActivity")]
        public async Task<ActionResult<TransactionResult<bool>>> Create([FromBody] DTO.Schedule.Activity activity)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new TransactionResult<bool>()
                {
                    ResultCode = ResultCode.Alert,
                    ResultMessages = new List<string> { "NOT FOUND" }
                });
            }
            if (!(activity.Property_Id > 0))
            {
                return Ok(new TransactionResult<bool>()
                {
                    ResultCode = ResultCode.Alert,
                    ResultMessages = new List<string> { Constants.PropertyNotFound }
                });
            }
            return Ok(await _activityService.Add(activity));
        }

        [HttpPut]
        [Route("UpdateActivity")]
        public async Task<ActionResult<TransactionResult<bool>>> Update([FromBody] DTO.Schedule.ModifyActivity activity)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new TransactionResult<bool>()
                {
                    ResultCode = ResultCode.Alert,
                    ResultMessages = new List<string> { "NOT FOUND" }
                });
            }
            if (!(activity.ActivityId > 0))
            {
                return Ok(new TransactionResult<bool>()
                {
                    ResultCode = ResultCode.Alert,
                    ResultMessages = new List<string> { Constants.ScheduleNotFound }
                });
            }
            return Ok(await _activityService.Edit(activity));
        }


        [HttpPut]
        [Route("CancelActivity")]
        public async Task<ActionResult<TransactionResult<bool>>> Cancel(int ActivityId)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new TransactionResult<bool>()
                {
                    ResultCode = ResultCode.Alert,
                    ResultMessages = new List<string> { "NOT FOUND" }
                });
            }
            if (!(ActivityId > 0))
            {
                return Ok(new TransactionResult<bool>()
                {
                    ResultCode = ResultCode.Alert,
                    ResultMessages = new List<string> { Constants.ScheduleNotFound }
                });
            }
            return Ok(await _activityService.Cancel(ActivityId));
        }

        [HttpPost]
        [Route("GetActivitiesDetail")]
        public async Task<ActionResult<TransactionResult<List<DTO.Schedule.Activity>>>> GetActivitiesDetail([FromBody] DTO.SearchParams searchParams)
        {
            return Ok(await _activityService.GetActivitiesDetail(searchParams));
        }
        #endregion
    }
}
