﻿using Microsoft.AspNetCore.Mvc;
using WEB_153503_Kakhnouski.Domain.Entities;
using WEB_153503_Kakhnouski.Services.CarService;
using WEB_153503_Kakhnouski.Services.CategoryServicep;

namespace WEB_153503_Kakhnouski.Controllers;

public class CarController : Controller
{
    private readonly ICarCategoryService _carCategoryService;
    private readonly ICarService _carService;

    public CarController(ICarCategoryService carCategoryService, ICarService carService)
    {
        _carCategoryService = carCategoryService;
        _carService = carService;
    }
    public async Task<IActionResult> Index(string? category = "cars", int pageNo = 1)
    {
        var productResponse = await _carService.GetCarListAsync(category);
        if (!productResponse.Success)
            return NotFound(productResponse.ErrorMessage);
        return View(productResponse.Data);
    }
}
