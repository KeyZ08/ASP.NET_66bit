using ASP_66Bit_Test.Data;
using ASP_66Bit_Test.Models;
using ASP_66Bit_Test.Models.Extenshions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASP_66Bit_Test.Controllers
{
    public class FootballerController : Controller
    {
        AppDBContext context;
        public FootballerController(AppDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Countries = context.Countries;
            ViewBag.Teams = context.Teams;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FootballerViewModel footballer)
        {
            if (ModelState.IsValid & footballer.IsValid(context, ModelState))
            {
                var newF = footballer.FootballerViewModelToFootballer(context);
                if (newF == null)
                    return NotFound();

                await context.AddAsync(newF);
                await context.SaveChangesAsync();

                return RedirectToAction("All");
            }
            ViewBag.Countries = context.Countries;
            ViewBag.Teams = context.Teams;

            return View();
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var footballer = context.Footballers
                .Include(f => f.Country)
                .Include(f => f.Team)
                .FirstOrDefault(f => f.Id == id);
            if (footballer == null)
                return NotFound();
            var result = footballer.FootballerToFootballerViewModel();
            ViewBag.Countries = context.Countries;
            ViewBag.Teams = context.Teams;
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FootballerViewModel footballer)
        {
            if (ModelState.IsValid & footballer.IsValid(context, ModelState))
            {
                var newF = footballer.FootballerViewModelToFootballer(context);
                if (newF == null)
                    return BadRequest();
                context.Update(newF);
                await context.SaveChangesAsync();
                return RedirectToAction("All");
            }
            else
            {
                ViewBag.Countries = context.Countries;
                ViewBag.Teams = context.Teams;
                return View(footballer);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var footballer = await context.Footballers.FindAsync(id);
            if (footballer == null)
                return NotFound();
            context.Footballers.Remove(footballer);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public IActionResult All()
        {
            var footballers = context.Footballers
                .Include(u => u.Country)
                .Include(t => t.Team)
                .ToList();
            return View(footballers);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
