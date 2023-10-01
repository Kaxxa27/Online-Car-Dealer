using WEB_153503_Kakhnouski.Domain.Entities;
using WEB_153503_Kakhnouski.Domain.Models;
using WEB_153503_Kakhnouski.Services.CategoryServicep;

namespace WEB_153503_Kakhnouski.Services.CarService;

public class MemoryCarService : ICarService
{
    private List<Car> _cars;
    private List<Category> _categories;

    public MemoryCarService(ICarCategoryService carCategoryService)
    {
        _categories = carCategoryService.GetCategoryListAsync().Result.Data;
        SetupData();
    }

    /// <summary>
    /// Initializing lists
    /// </summary>
    private void SetupData()
    {
        _cars = new List<Car>
        {
            new Car
            {
                Id = 1,
                Name = "BMW E38",
                Description = "Bumer",
                Price = 5000,
                CategoryId = _categories.Find(c => c.NormalizedName.Equals("car"))
            },
            new Car
            {
                Id = 2,
                Name = "Mersedes W400",
                Description = "Kaban",
                Price = 10000,
                CategoryId = _categories.Find(c => c.NormalizedName.Equals("car"))
            },
        };
    }

    public Task<ResponseData<Car>> CreateCarAsync(Car car, IFormFile? formFile)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCarAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseData<Car>> GetCarByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseData<ListModel<Car>>> GetCarListAsync(string? categoryNormalizedName, int pageNo = 1)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCarAsync(int id, Car car, IFormFile? formFile)
    {
        throw new NotImplementedException();
    }
}
