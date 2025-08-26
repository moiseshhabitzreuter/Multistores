using Multistores.Contracts.DTOs;

namespace Contracts.DTOs
{
    public class StoreDto : BaseDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string IdentificationCode { get; set; }

        public StoreDto FromDto(CreateUpdateStoreDto dto, Guid id)
        {
            return new StoreDto
            {
                Id = id,
                Code = dto.Code,
                Name = dto.Name,
                IdentificationCode = dto.IdentificationCode
            };
        }
    }
}
