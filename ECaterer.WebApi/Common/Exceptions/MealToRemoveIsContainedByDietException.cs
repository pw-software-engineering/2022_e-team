using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Exceptions
{
    public class MealToRemoveIsContainedByDietException : Exception
    {
        public MealToRemoveIsContainedByDietException(int mealId) : base($"Can't delete meal with specified ID: {mealId}, because it's contained by diet")
        {
        }
    }
}
