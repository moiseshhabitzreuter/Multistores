namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Packaging { get; set; }
        public decimal PackagingWeight { get; set; }
        public decimal IndividualWeight { get; set; }
        public int IndividualQuantity { get; set; }
    }
}
