namespace DTO
{
    public class ProductDTO
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public double UnitPrice { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public string? CategoryName { get; set; }
        public int? CategoryID { get; set; }
    }
}
