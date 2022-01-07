using App.Common.Enums;
using App.DTO.Response;
using App.Services.Property;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.RestfulAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropertyController : ControllerBase
    {
        #region Attributes 
        private readonly IPropertyService _propertyService;
        #endregion

        #region Constructor
        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }
        #endregion

        #region Methods
        [HttpPost]
        [Route("CreateProperty")]
        public async Task<ActionResult<TransactionResult<bool>>> CreateProperty([FromBody] DTO.Schedule.Property property)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new TransactionResult<bool>()
                {
                    ResultCode = ResultCode.Alert,
                    ResultMessages = new List<string> { "NOT FOUND" }
                });
            }
            return Ok(await _propertyService.Add(property));
        }
        [HttpPut("EditProperty")]
        public async Task<ActionResult<TransactionResult<bool>>> EditProperty([FromBody] DTO.Schedule.Property property)
        {
            if (!(property.Id > 0))
            {
                return Ok(new TransactionResult<bool>()
                {
                    ResultCode = ResultCode.Alert,
                    ResultMessages = new List<string> { "NOT FOUND" }
                });

            }
            var response = await _propertyService.Edit(property);
            return Ok(response);
        }
        #endregion
    }
}
