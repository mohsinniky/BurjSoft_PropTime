using Microsoft.AspNetCore.Mvc;

namespace CoreMVCTutorial.Controllers
{
    public class TopProductsViewComponent : ViewComponent
    {
        //The Invoke method for the View component
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            ViewBag.Count = count;
            return View();
        }
    }
}
