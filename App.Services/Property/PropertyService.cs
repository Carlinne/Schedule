using App.Common;
using App.Common.Enums;
using App.Common.Extensions;
using App.Data.Repositories.Property;
using App.DTO.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Services.Property
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository)
        {
            this._propertyRepository = propertyRepository;
        }
        public async Task<TransactionResult<bool>> Add(DTO.Schedule.Property dto)
        {
            var result = new TransactionResult<bool>
            {
                Result = false,
                ResultMessages = new List<string>()
            };
            try
            {
                dto.StatusId = (int)AppStatus.Active;
                dto.Created_At = DateTime.UtcNow.CentralStandardTime();
                _propertyRepository.Add(dto);
                await _propertyRepository.SaveChanges();
                result.Result = true;
                result.ResultCode = ResultCode.Success;
                result.ResultMessages.Add(Constants.PropertyInsertSucess);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ResultCode = ResultCode.Fatal;
                result.ResultMessages = new List<string>() { ex.Message };
            }
            return result;
        }
        public async Task<TransactionResult<bool>> Edit(DTO.Schedule.Property dto)
        {
            var result = new TransactionResult<bool>
            {
                Result = false,
                ResultMessages = new List<string>()
            };
            try
            {
                dto.StatusId = (int)AppStatus.Active;
                dto.Updated_At = DateTime.UtcNow.CentralStandardTime();
                _propertyRepository.Edit(dto, dto.Id);
                await _propertyRepository.SaveChanges();
                result.Result = true;
                result.ResultCode = ResultCode.Success;
                result.ResultMessages.Add(Constants.PropertyUpdateSucess);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ResultCode = ResultCode.Fatal;
                result.ResultMessages = new List<string>() { ex.Message };
            }
            return result;
        }
    }
}
