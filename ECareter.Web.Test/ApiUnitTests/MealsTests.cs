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
using ECaterer.WebApi.Common.Queries;

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
                    IngredientList = new List<Ingredient>(),
                    Vegan = true
                }
            };

            var options = new DbContextOptions<DataContext>();
            var contextMock = new Mock<DataContext>(options);
            contextMock
                .Setup(c => c.Meals)
                .ReturnsDbSet(meals);

            var controller = new MealsController(new MealRepository(contextMock.Object));
            var result = await controller.GetMealById(1);
            var okResult = result.Result as OkObjectResult;
            var returnedMeal = okResult.Value as MealDTO;

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
                    IngredientList = new List<Ingredient>(),
                    Vegan = true
                }
            };

            var options = new DbContextOptions<DataContext>();
            var contextMock = new Mock<DataContext>(options);
            contextMock
                .Setup(c => c.Ingredients)
                .ReturnsDbSet(new List<Ingredient>());
            contextMock
                .Setup(c => c.Allergents)
                .ReturnsDbSet(new List<Allergent>());
            contextMock
                .Setup(c => c.Meals)
                .ReturnsDbSet(meals);
            contextMock
                .Setup(c => c.Diets)
                .ReturnsDbSet(new List<Diet>());
            contextMock
                .Setup(c => c.Meals.Remove(It.IsAny<Meal>()))
                .Callback<Meal>((meal) => meals.Remove(meal));

            var controller = new MealsController(new MealRepository(contextMock.Object));
            var result = await controller.DeleteMeal(1);

            var okResult = result as OkObjectResult;
            var mealsCountAfterDelete = contextMock.Object.Meals.Count();

            okResult.Should().NotBeNull();
            mealsCountAfterDelete.Should().Be(0);
        }

        [Fact]
        public async void DeleteMealContainedByDiet_ShouldReturnBadRequest()
        {
            var meals = new List<Meal>()
            {
                new Meal()
                {
                    MealId = 1,
                    Name = "Apple",
                    Calories = 100,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = true
                }
            };

            var options = new DbContextOptions<DataContext>();
            var contextMock = new Mock<DataContext>(options);
            contextMock
                .Setup(c => c.Meals)
                .ReturnsDbSet(meals);
            contextMock
                .Setup(c => c.Diets)
                .ReturnsDbSet(new List<Diet>() { new Diet() { Meals = meals } }) ;
            contextMock
                .Setup(c => c.Meals.Remove(It.IsAny<Meal>()))
                .Callback<Meal>((meal) => meals.Remove(meal));

            var controller = new MealsController(new MealRepository(contextMock.Object));
            var result = await controller.DeleteMeal(1);

            var badRequestResult = result as BadRequestObjectResult;

            badRequestResult.Should().NotBeNull();
        }

        [Fact]
        public async void EditMeal_ShouldEditMealAndAddNewAllergentsAndIngredientsToDatabaseAndReturnOk()
        {
            var allergents = new List<Allergent>()
            {
                new Allergent() { Name = "Milk"}
            };
            var ingredients = new List<Ingredient>()
            {
                new Ingredient() { Name = "Milk"},
                new Ingredient() { Name = "Oatmeals"}
            };
            var meals = new List<Meal>()
            {
                new Meal()
                {
                    MealId = 1,
                    Name = "Cereals",
                    Calories = 100,
                    AllergentList = allergents,
                    IngredientList = ingredients,
                    Vegan = true
                }
            };

            var editMealDto = new MealDTO()
            {
                Name = "Pancake",
                Calories = 130,
                AllergentList = new string[] { "Milk", "Egg" },
                IngredientList = new string[] { "Milk", "Egg", "Flour" },
                Vegan = false
            };

            var options = new DbContextOptions<DataContext>();
            var contextMock = new Mock<DataContext>(options);
            contextMock
                .Setup(c => c.Meals)
                .ReturnsDbSet(meals);
            contextMock
                .Setup(c => c.Ingredients)
                .ReturnsDbSet(ingredients);
            contextMock
                .Setup(c => c.Allergents)
                .ReturnsDbSet(allergents);
            contextMock
                .Setup(c => c.Meals.Update(It.Is<Meal>(m => m.Name == "Pancake")))
                .Callback<Meal>((meal) => meals[0] = meal);
            contextMock
                .Setup(c => c.Allergents.AddRange(It.Is<IEnumerable<Allergent>>(al => al.Count() == 2)))
                .Callback(() => allergents.AddRange(new List<Allergent>()
                {
                    new Allergent() { Name = "Milk" },
                    new Allergent() { Name = "Egg" }
                }));
            contextMock
                .Setup(c => c.Ingredients.AddRange(It.Is<IEnumerable<Ingredient>>(ing => ing.Count() == 3)))
                .Callback(() => ingredients.AddRange(new List<Ingredient>()
                {
                    new Ingredient() { Name = "Milk" },
                    new Ingredient() { Name = "Egg" },
                    new Ingredient() { Name = "Flour" }
                }));

            var controller = new MealsController(new MealRepository(contextMock.Object));
            var result = await controller.EditMeal(1, editMealDto);
            var okResult = result as OkObjectResult;
            var editedMeal = meals.FirstOrDefault();

            okResult.Should().NotBeNull();
            editedMeal.Name.Should().Be("Pancake");
            ingredients.Count().Should().Be(5);
            allergents.Count().Should().Be(3);
        }

        [Fact]
        public async void EditMeal_ShouldReturnNotFound()
        {
            var meals = new List<Meal>()
            {
                new Meal()
                {
                    MealId = 1,
                    Name = "Cereals",
                    Calories = 100,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = true
                }
            };

            var editMealDto = new MealDTO()
            {
                Name = "Pancake",
                Calories = 130,
                AllergentList = new string[] { "Milk", "Egg" },
                IngredientList = new string[] { "Milk", "Egg", "Flour" },
                Vegan = false
            };

            var options = new DbContextOptions<DataContext>();
            var contextMock = new Mock<DataContext>(options);
            contextMock
                .Setup(c => c.Meals)
                .ReturnsDbSet(meals);

            var controller = new MealsController(new MealRepository(contextMock.Object));
            var result = await controller.EditMeal(2, editMealDto);
            var notFoundResult = result as NotFoundObjectResult;

            notFoundResult.Should().NotBeNull();
        }

        [Fact]
        public async void AddMeal_ShouldAddMealAndNewAllergentsAndIngredientsToDatabaseAndReturnOk()
        {
            var allergents = new List<Allergent>()
            {
                new Allergent() { Name = "Milk"}
            };
            var ingredients = new List<Ingredient>()
            {
                new Ingredient() { Name = "Milk"},
                new Ingredient() { Name = "Oatmeals"}
            };
            var meals = new List<Meal>()
            {
                new Meal()
                {
                    MealId = 1,
                    Name = "Cereals",
                    Calories = 100,
                    AllergentList = allergents,
                    IngredientList = ingredients,
                    Vegan = true
                }
            };
            var addMealDto = new MealDTO()
            {
                Name = "Pancake",
                Calories = 130,
                AllergentList = new string[] { "Milk", "Egg" },
                IngredientList = new string[] { "Milk", "Egg", "Flour" },
                Vegan = false
            };

            var options = new DbContextOptions<DataContext>();
            var contextMock = new Mock<DataContext>(options);
            contextMock
                .Setup(c => c.Meals)
                .ReturnsDbSet(meals);
            contextMock
                .Setup(c => c.Ingredients)
                .ReturnsDbSet(ingredients);
            contextMock
                .Setup(c => c.Allergents)
                .ReturnsDbSet(allergents);
            contextMock
                .Setup(c => c.Meals.Add(It.Is<Meal>(m => m.Name == "Pancake")))
                .Callback<Meal>((meal) => meals.Add(meal));
            contextMock
                .Setup(c => c.Allergents.AddRange(It.Is<IEnumerable<Allergent>>(al => al.Count() == 2)))
                .Callback(() => allergents.AddRange(new List<Allergent>()
                {
                    new Allergent() { Name = "Milk" },
                    new Allergent() { Name = "Egg" }
                }));
            contextMock
                .Setup(c => c.Ingredients.AddRange(It.Is<IEnumerable<Ingredient>>(ing => ing.Count() == 3)))
                .Callback(() => ingredients.AddRange(new List<Ingredient>()
                {
                    new Ingredient() { Name = "Milk" },
                    new Ingredient() { Name = "Egg" },
                    new Ingredient() { Name = "Flour" }
                }));

            var controller = new MealsController(new MealRepository(contextMock.Object));
            var result = await controller.AddMeal(addMealDto);
            var okResult = result as OkObjectResult;
            var addedMeal = meals.FirstOrDefault(meal => meal.Name == "Pancake");

            okResult.Should().NotBeNull();
            addedMeal.Should().NotBeNull();
            addedMeal.Name.Should().Be("Pancake");
            ingredients.Count().Should().Be(5);
            allergents.Count().Should().Be(3);
        }
        
        [Fact]
        public async void GetMeals_ShouldReturnAllMealsAndOk()
        {
            var meals = new List<Meal>()
            {
                new Meal()
                {
                    MealId = 1,
                    Name = "Apple",
                    Calories = 100,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = true
                },
                new Meal()
                {
                    MealId = 2,
                    Name = "Banana",
                    Calories = 200,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = true
                },
                new Meal()
                {
                    MealId = 3,
                    Name = "Burger",
                    Calories = 500,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = false
                }
            };
            var query = new GetMealsQuery();

            var options = new DbContextOptions<DataContext>();
            var contextMock = new Mock<DataContext>(options);
            contextMock
                .Setup(c => c.Meals)
                .ReturnsDbSet(meals);

            var controller = new MealsController(new MealRepository(contextMock.Object));
            var result = await controller.GetMeals(query);
            var okResult = result.Result as OkObjectResult;
            var returnedMeals = okResult.Value as IEnumerable<GetMealDTO>;

            okResult.Should().NotBeNull();
            returnedMeals.Should().NotBeNull();
            returnedMeals.Count().Should().Be(3);
        }

        [Fact]
        public async void GetMealsWithOffset_ShouldReturnMealsAndOk()
        {
            var meals = new List<Meal>()
            {
                new Meal()
                {
                    MealId = 1,
                    Name = "Apple",
                    Calories = 100,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = true
                },
                new Meal()
                {
                    MealId = 2,
                    Name = "Banana",
                    Calories = 200,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = true
                },
                new Meal()
                {
                    MealId = 3,
                    Name = "Burger",
                    Calories = 500,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = false
                }
            };
            var query = new GetMealsQuery()
            {
                Offset = 1
            };

            var options = new DbContextOptions<DataContext>();
            var contextMock = new Mock<DataContext>(options);
            contextMock
                .Setup(c => c.Meals)
                .ReturnsDbSet(meals);

            var controller = new MealsController(new MealRepository(contextMock.Object));
            var result = await controller.GetMeals(query);
            var okResult = result.Result as OkObjectResult;
            var returnedMeals = okResult.Value as IEnumerable<GetMealDTO>;

            okResult.Should().NotBeNull();
            returnedMeals.Should().NotBeNull();
            returnedMeals.Count().Should().Be(2);
            returnedMeals.FirstOrDefault().Name.Should().Be("Banana");
        }

        [Fact]
        public async void GetMealsWithLimit_ShouldReturnMealsAndOk()
        {
            var meals = new List<Meal>()
            {
                new Meal()
                {
                    MealId = 1,
                    Name = "Apple",
                    Calories = 100,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = true
                },
                new Meal()
                {
                    MealId = 2,
                    Name = "Banana",
                    Calories = 200,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = true
                },
                new Meal()
                {
                    MealId = 3,
                    Name = "Burger",
                    Calories = 500,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = false
                }
            };
            var query = new GetMealsQuery()
            {
                Limit = 1
            };

            var options = new DbContextOptions<DataContext>();
            var contextMock = new Mock<DataContext>(options);
            contextMock
                .Setup(c => c.Meals)
                .ReturnsDbSet(meals);

            var controller = new MealsController(new MealRepository(contextMock.Object));
            var result = await controller.GetMeals(query);
            var okResult = result.Result as OkObjectResult;
            var returnedMeals = okResult.Value as IEnumerable<GetMealDTO>;

            okResult.Should().NotBeNull();
            returnedMeals.Should().NotBeNull();
            returnedMeals.Count().Should().Be(1);
            returnedMeals.FirstOrDefault().Name.Should().Be("Apple");
        }

        [Fact]
        public async void GetMealssWithSorting_ShouldReturnMealAndOk()
        {
            var meals = new List<Meal>()
            {
                new Meal()
                {
                    MealId = 1,
                    Name = "Apple",
                    Calories = 100,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = true
                },
                new Meal()
                {
                    MealId = 2,
                    Name = "Banana",
                    Calories = 200,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = true
                },
                new Meal()
                {
                    MealId = 3,
                    Name = "Burger",
                    Calories = 500,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = false
                }
            };
            var query = new GetMealsQuery()
            {
                Sort = "calories(desc)"
            };

            var options = new DbContextOptions<DataContext>();
            var contextMock = new Mock<DataContext>(options);
            contextMock
                .Setup(c => c.Meals)
                .ReturnsDbSet(meals);

            var controller = new MealsController(new MealRepository(contextMock.Object));
            var result = await controller.GetMeals(query);
            var okResult = result.Result as OkObjectResult;
            var returnedMeals = okResult.Value as IEnumerable<GetMealDTO>;

            okResult.Should().NotBeNull();
            returnedMeals.Should().NotBeNull();
            returnedMeals.FirstOrDefault().Name.Should().Be("Burger");
        }

        [Fact]
        public async void GetMealsWithSpecifiedName_ShouldReturnMealsAndOk()
        {
            var meals = new List<Meal>()
            {
                new Meal()
                {
                    MealId = 1,
                    Name = "Apple",
                    Calories = 100,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = true
                },
                new Meal()
                {
                    MealId = 2,
                    Name = "Banana",
                    Calories = 200,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = true
                },
                new Meal()
                {
                    MealId = 3,
                    Name = "Burger",
                    Calories = 500,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = false
                }
            };
            var query = new GetMealsQuery()
            {
                Name = "Banana"
            };

            var options = new DbContextOptions<DataContext>();
            var contextMock = new Mock<DataContext>(options);
            contextMock
                .Setup(c => c.Meals)
                .ReturnsDbSet(meals);

            var controller = new MealsController(new MealRepository(contextMock.Object));
            var result = await controller.GetMeals(query);
            var okResult = result.Result as OkObjectResult;
            var returnedMeals = okResult.Value as IEnumerable<GetMealDTO>;

            okResult.Should().NotBeNull();
            returnedMeals.Should().NotBeNull();
            returnedMeals.FirstOrDefault().Name.Should().Be("Banana");
        }

        [Fact]
        public async void GetMealsWithSpecifiedTextInName_ShouldReturnMealsAndOk()
        {
            var meals = new List<Meal>()
            {
                new Meal()
                {
                    MealId = 1,
                    Name = "Apple",
                    Calories = 100,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = true
                },
                new Meal()
                {
                    MealId = 2,
                    Name = "Banana",
                    Calories = 200,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = true
                },
                new Meal()
                {
                    MealId = 3,
                    Name = "Burger",
                    Calories = 500,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = false
                }
            };
            var query = new GetMealsQuery()
            {
                Name_with = "ur"
            };

            var options = new DbContextOptions<DataContext>();
            var contextMock = new Mock<DataContext>(options);
            contextMock
                .Setup(c => c.Meals)
                .ReturnsDbSet(meals);

            var controller = new MealsController(new MealRepository(contextMock.Object));
            var result = await controller.GetMeals(query);
            var okResult = result.Result as OkObjectResult;
            var returnedMeals = okResult.Value as IEnumerable<GetMealDTO>;

            okResult.Should().NotBeNull();
            returnedMeals.Should().NotBeNull();
            returnedMeals.Count().Should().Be(1);
            returnedMeals.FirstOrDefault().Name.Should().Be("Burger");
        }

        [Fact]
        public async void GetMealsWithCaloriesLimit_ShouldReturnMealsAndOk()
        {
            var meals = new List<Meal>()
            {
                new Meal()
                {
                    MealId = 1,
                    Name = "Apple",
                    Calories = 100,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = true
                },
                new Meal()
                {
                    MealId = 2,
                    Name = "Banana",
                    Calories = 200,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = true
                },
                new Meal()
                {
                    MealId = 3,
                    Name = "Burger",
                    Calories = 500,
                    AllergentList = new List<Allergent>(),
                    IngredientList = new List<Ingredient>(),
                    Vegan = false
                }
            };
            var query = new GetMealsQuery()
            {
                Calories_ht = 300
            };

            var options = new DbContextOptions<DataContext>();
            var contextMock = new Mock<DataContext>(options);
            contextMock
                .Setup(c => c.Meals)
                .ReturnsDbSet(meals);

            var controller = new MealsController(new MealRepository(contextMock.Object));
            var result = await controller.GetMeals(query);
            var okResult = result.Result as OkObjectResult;
            var returnedMeals = okResult.Value as IEnumerable<GetMealDTO>;

            okResult.Should().NotBeNull();
            returnedMeals.Should().NotBeNull();
            returnedMeals.Count().Should().Be(1);
            returnedMeals.FirstOrDefault().Name.Should().Be("Burger");
        }
    }
}