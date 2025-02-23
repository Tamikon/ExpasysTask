namespace ASP_Homework_Product.Models
{
    public class Product
    {
        private static int idCounter = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }

        public string ImagePath { get; }
        public Product(string name, string description, int cost, string imagePath)
        {
            Id = idCounter;
            Name = name;
            Description = description;
            Cost = cost;
            idCounter++;
            ImagePath = imagePath;
        }
        public override string ToString()
        {
            return $"{Id}\n{Name}\n{Cost}";
        }
    }
}