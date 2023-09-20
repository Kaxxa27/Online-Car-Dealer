using Microsoft.AspNetCore.Mvc;

namespace WEB_153503_Kakhnouski.Components;

public class Cart : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync() => View("Cart");
}
