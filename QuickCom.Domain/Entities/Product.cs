namespace QuickCom.Domain.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

    }
}
