using ECaterer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Builder
{
    public class MealsQueryBuilder
    {
        private IQueryable<Meal> meals;

        public MealsQueryBuilder(IQueryable<Meal> meals)
        {
            this.meals = meals;
        }

        public MealsQueryBuilder SetNameFilter(string name)
        {
            if (name is not null)
                meals = meals.Where(m => m.Name == name);
            return new MealsQueryBuilder(meals);
        }

        public MealsQueryBuilder SetNameWithFilter(string name_with)
        {
            if (name_with is not null)
                meals = meals.Where(m => m.Name.Contains(name_with));
            return new MealsQueryBuilder(meals);
        }

        public MealsQueryBuilder SetVeganFilter(bool? vegan)
        {
            if (vegan is not null)
                meals = meals.Where(m => m.Vegan == vegan);
            return new MealsQueryBuilder(meals);
        }

        public MealsQueryBuilder SetCaloriesFilter(int? calories)
        {
            if (calories is not null)
                meals = meals.Where(m => m.Calories == calories);
            return new MealsQueryBuilder(meals);
        }

        public MealsQueryBuilder SetCaloriesLowerThanFilter(int? calories_lt)
        {
            if (calories_lt is not null)
                meals = meals.Where(m => m.Calories <= calories_lt);
            return new MealsQueryBuilder(meals);
        }

        public MealsQueryBuilder SetCaloriesHigherThanFilter(int? calories_ht)
        {
            if (calories_ht is not null)
                meals = meals.Where(m => m.Calories >= calories_ht);
            return new MealsQueryBuilder(meals);
        }

        public MealsQueryBuilder SetSorting(string sort)
        {
            switch (sort)
            {
                case "title(asc)":
                    meals = meals.OrderBy(m => m.Name);
                    break;
                case "title(desc)":
                    meals = meals.OrderByDescending(m => m.Name);
                    break;
                case "calories(asc)":
                    meals = meals.OrderBy(m => m.Calories);
                    break;
                case "calories(desc)":
                    meals = meals.OrderByDescending(m => m.Calories);
                    break;
                case null:
                    break;
                default:
                    throw new Exception("Unexpected sort type");
            }
            return new MealsQueryBuilder(meals);
        }

        public MealsQueryBuilder SetOffset(int? offset)
        {
            if (offset is not null)
                meals = meals.Skip((int)offset);
            return new MealsQueryBuilder(meals);
        }

        public MealsQueryBuilder SetLimit(int? limit)
        {
            if (limit is not null)
                meals = meals.Take((int)limit);
            return new MealsQueryBuilder(meals);
        }
    }
}
