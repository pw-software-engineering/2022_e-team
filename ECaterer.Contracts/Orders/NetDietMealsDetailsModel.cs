
namespace ECaterer.Contracts.Orders
{
    public class CreateDietMealsModel
    {
        public string Name { get; set; }

        public string[] MealsId { get; set; }

        public int Price { get; set; }
    }
}
