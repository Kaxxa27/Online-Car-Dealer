using Microsoft.EntityFrameworkCore;
using WEB_153503_Kakhnouski.API.Data;
using WEB_153503_Kakhnouski.Domain.Entities;
using WEB_153503_Kakhnouski.Domain.Models;
using WEB_153503_Kakhnouski.Services.CarService;

namespace WEB_153503_Kakhnouski.API.Services.CarService;

public class CarService : ICarService
{
    private const int _maxPageSize = 20;
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public CarService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<ResponseData<Car>> CreateCarAsync(Car car)
    {
        _context.Cars.Add(car);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return new ResponseData<Car>
            {
                Success = false,
                ErrorMessage = ex.Message,
            };
        }

        return new ResponseData<Car>
        {
            Data = car
        };
    }


    public async Task DeleteCarAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car is null)
        {
            throw new Exception("Car was not found");
        }

        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
    }

    public async Task<ResponseData<Car>> GetCarByIdAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car is null)
        {
            return new()
            {
                Success = false,
                ErrorMessage = "Car was not found"
            };
        }

        return new()
        {
            Data = car
        };
    }

    public async Task<ResponseData<ListModel<Car>>> GetCarListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3)
    {
        if (pageSize > _maxPageSize)
            pageSize = _maxPageSize;

        var query = _context.Cars.AsQueryable();
        var dataList = new ListModel<Car>();

        query = query.Where(d => categoryNormalizedName == null ||
                            d.Category!.NormalizedName.Equals(categoryNormalizedName))
                     .Include(t => t.Category);

        var count = query.Count();
        if (count == 0)
        {
            return new ResponseData<ListModel<Car>>
            {
                Data = dataList
            };
        }

        int totalPages = (int)Math.Ceiling(count / (double)pageSize);
        if (pageNo > totalPages)
        {
            return new ResponseData<ListModel<Car>>
            {
                Data = null,
                Success = false,
                ErrorMessage = "No such page"
            };
        }

        dataList.Items = await query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
        dataList.CurrentPage = pageNo;
        dataList.TotalPages = totalPages;
        return new ResponseData<ListModel<Car>>
        {
            Data = dataList
        };
    }

    public async Task<ResponseData<string>> SaveImageAsync(int id, IFormFile? formFile)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car is null)
        {
            return new ResponseData<string>
            {
                Success = false,
                ErrorMessage = "Car was not found"
            };
        }

        string imageRoot = Path.Combine(_configuration["AppUrl"]!, "images");
        string uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;

        string imagePath = Path.Combine(imageRoot, uniqueFileName);

        using (var stream = new FileStream(imagePath, FileMode.Create))
        {
            await formFile.CopyToAsync(stream);
        }

        car.Image = imagePath;
        await _context.SaveChangesAsync();

        return new ResponseData<string>
        {
            Data = car.Image,
            Success = true
        };
    }

    public async Task UpdateCarAsync(int id, Car car)
    {
        var oldCar = await _context.Cars.FindAsync(id);
        if (oldCar is null)
        {
            throw new Exception("Car was not found");
        }

        oldCar.Name = car.Name;
        oldCar.Description = car.Description;
        oldCar.Price = car.Price;
        oldCar.Image = car.Image;
        oldCar.Category = car.Category;

        await _context.SaveChangesAsync();
    }
}
