using System.Diagnostics;
using bdt.Data;
using bdt.Models;
using Microsoft.AspNetCore.Mvc;

namespace bdt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ExpensesDbContext _context;

        public HomeController(ILogger<HomeController> logger, ExpensesDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
            //return Expenses();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CreateEditExpense(int? id)
        {
            if (id != null)
            {
                Expense? expenseInDb = _context.expenses.SingleOrDefault(expense => expense.Id == id);
                return View(expenseInDb);

            }

            return View();
        }

        public IActionResult DeleteExpense(int id)
        {

            Expense? expenseInDb = _context.expenses.SingleOrDefault(expense => expense.Id == id);
            _context.expenses.Remove(expenseInDb);
            if (!_context.expenses.Any(e => e.Category == expenseInDb.Category))
            {
                ExpenseCategory.Categories.Remove(expenseInDb.Category);
            }

            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }

        [HttpGet]
        public JsonResult GetSuggestions(string term)
        {
            // Example data source (could be from a database

            // Filter matches
            var matches = ExpenseCategory.Categories
                .Where(f => f.StartsWith(term, System.StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Json(matches);
        }


        public IActionResult CreateEditExpense_Form(Expense model)
        {
            Debug.WriteLine(model.Id);
            if (model.Id == 0)
            {
                _context.expenses.Add(model);
            }
            else
            {
                _context.expenses.Update(model);
            }

            if (ExpenseCategory.Categories.Contains(model.Category))
                ExpenseCategory.Categories.Add(model.Category);


            _context.SaveChanges();

            return RedirectToAction("Expenses");
        }

        public IActionResult Expenses() // needs the exact same name as view
        {
            List<Expense> allExpenses = _context.expenses.ToList();

            decimal totalExpenses = allExpenses.Sum(expense => expense.Price);

            ViewBag.expenses = totalExpenses;
            return View(allExpenses);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
