namespace ECaterer.Web.DTO
{
    public class EditDietDTO
    {
        public string Id { get; set; }
        public int Calories { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Vegan { get; set; }
    }
}
