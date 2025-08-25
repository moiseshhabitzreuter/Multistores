namespace Contracts.DTOs
{
    public class ProductDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Packaging { get; set; }
        public decimal PackagingWeight { get; set; }
        public decimal IndividualWeight { get; set; }
        public int IndividualQuantity { get; set; }
    }
}
