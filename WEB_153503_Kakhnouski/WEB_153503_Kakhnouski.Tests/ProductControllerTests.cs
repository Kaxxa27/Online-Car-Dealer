using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_153503_Kakhnouski.Controllers;
using WEB_153503_Kakhnouski.Domain.Entities;
using WEB_153503_Kakhnouski.Domain.Models;
using WEB_153503_Kakhnouski.Services.CarService;
using WEB_153503_Kakhnouski.Services.CategoryService;

namespace WEB_153503_Kakhnouski.Tests;

public class ProductControllerTests
{

    [Fact]
    public void IndexReturns404WhenCategoriesAreReceiveUnsuccessfully()
    {
        // Arrange.
        var categoryService = new Mock<ICarCategoryService>();
        categoryService.Setup(m => m.GetCategoryListAsync())
            .ReturnsAsync(new ResponseData<List<Category>> { Success = false });

        var carService = new Mock<ICarService>();
        carService.Setup(m => m.GetCarListAsync(It.IsAny<string?>(), It.IsAny<int>()))
            .ReturnsAsync(new ResponseData<ListModel<Car>> { Success = true });

        var controller = new CarController(categoryService.Object, carService.Object);

        // Act.
        var result = controller.Index(null).Result;

        // Assert.
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }

    [Fact]
    public void IndexReturns404WhenCarsAreReceivedUnsuccessfully()
    {
        // Arrange.
        var categoryService = new Mock<ICarCategoryService>();
        categoryService.Setup(m => m.GetCategoryListAsync())
            .ReturnsAsync(new ResponseData<List<Category>>
            { Success = true, Data = new List<Category>() { new Category() { Name = "moto", NormalizedName = "moto" } } });

        var carService = new Mock<ICarService>();
        carService.Setup(m => m.GetCarListAsync(It.IsAny<string?>(), It.IsAny<int>()))
            .ReturnsAsync(new ResponseData<ListModel<Car>> { Success = false });

        var controller = new CarController(categoryService.Object, carService.Object);

        // Act.
        var result = controller.Index(null).Result;

        // Assert.
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }
}
