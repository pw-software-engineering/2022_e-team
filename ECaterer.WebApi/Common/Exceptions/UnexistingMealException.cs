using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Exceptions
{
    public class UnexistingMealException : Exception
    {
        public UnexistingMealException(int mealId) : base($"Meal with specified ID doesn't exist: {mealId}")
        {
        }
    }
}
