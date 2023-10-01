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
                Image = "images/bmw_e38.jpg",
                Price = 5000,
                Category = _categories.Find(c => c.NormalizedName.Equals("cars"))
            },
            new Car
            {
                Id = 2,
                Name = "Mersedes W140",
                Description = "Kaban",
                Image = "images/mersedes_w140.jpg",
                Price = 10000,
                Category = _categories.Find(c => c.NormalizedName.Equals("cars"))
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
        var listCars = new ListModel<Car>();
        listCars.Items = _cars.Where(c => c.Category.NormalizedName == categoryNormalizedName || categoryNormalizedName is null).ToList();  
        return Task.FromResult(new ResponseData<ListModel<Car>>() { Data=listCars});
    }

    public Task UpdateCarAsync(int id, Car car, IFormFile? formFile)
    {
        throw new NotImplementedException();
    }
}
