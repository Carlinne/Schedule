using App.DTO.Response;
using System.Threading.Tasks;

namespace App.Services.Property
{
    public interface IPropertyService
    {
        Task<TransactionResult<bool>> Add(DTO.Schedule.Property dto);
        Task<TransactionResult<bool>> Edit(DTO.Schedule.Property dto);
    }
}
