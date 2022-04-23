using System;
using System.Collections.Generic;
using ECaterer.Core;
using FluentAssertions;
using Xunit;
using Microsoft.EntityFrameworkCore;
using ECaterer.Core.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ECaterer.Web.DTO.MealsDTO;
using Moq;
using ECaterer.WebApi.Controllers;
using Moq.EntityFrameworkCore;
using ECaterer.WebApi.Services;

namespace ECareter.Web.Test.ApiUnitTests
{
    public class MealsTests
    {
        [Fact]
        public async void GetMealById_ShouldReturnMealAndOk()
        {
            var meals = new List<Meal>()
            {
                new Meal()
                {
                    MealId = 1,
                    Name = "Apple",
                    Calories = 100,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>()
                }
            };

            var options = new DbContextOptions<DataContext>();
            var contextMock = new Mock<DataContext>(options);
            contextMock.Setup(c => c.Meals).ReturnsDbSet(meals);

            var controller = new MealsController(new MealRepository(contextMock.Object));
            var result = await controller.GetMealById(1);
            var okResult = result.Result as OkObjectResult;
            var returnedMeal = okResult.Value as Meal;

            okResult.Should().NotBeNull();
            returnedMeal.Should().NotBeNull();
            returnedMeal.Name.Should().Be("Apple");
        }

        [Fact]
        public async void DeleteMeal_ShouldDeleteMealAndReturnOk()
        {
            var meals = new List<Meal>()
            {
                new Meal()
                {
                    MealId = 1,
                    Name = "Apple",
                    Calories = 100,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>()
                }
            };

            var options = new DbContextOptions<DataContext>();
            var contextMock = new Mock<DataContext>(options);
            contextMock.Setup(c => c.Meals).ReturnsDbSet(meals);
            contextMock.Setup(c => c.Meals.Remove(It.IsAny<Meal>())).Callback<Meal>((meal) => meals.Remove(meal)); ;

            var controller = new MealsController(new MealRepository(contextMock.Object));
            var result = await controller.DeleteMeal(1);
            contextMock.Setup(c => c.Meals).ReturnsDbSet(meals);

            var okResult = result as OkObjectResult;
            var mealsCountAfterDelete = contextMock.Object.Meals.Count();

            okResult.Should().NotBeNull();
            mealsCountAfterDelete.Should().Be(0);
        }
    }
}
