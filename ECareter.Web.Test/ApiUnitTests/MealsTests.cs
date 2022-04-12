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

namespace ECareter.Web.Test.ApiUnitTests
{
    public class MealsTests
    {
        [Fact]
        public void DeleteMeal_ShouldDeleteMealAndReturnOk()
        {
            var contextMock = new Mock<DataContext>();
        }
    }
}
