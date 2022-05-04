using System;
using System.Collections.Generic;
using System.Text;

namespace ECaterer.Contracts.Diets
{
    public class GetDietModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Calories { get; set; }
        public bool Vegan { get; set; }
    }
}
