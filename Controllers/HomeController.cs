using EmployeeTaskManager.Data;
using EmployeeTaskManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTaskManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            bool isAdmin = User.IsInRole("Admin");
            string? currentUserEmail = User.Identity?.Name;

            IQueryable<TaskItem> query = _context.TaskItems
                .Include(t => t.Employee);

            if (!isAdmin)
            {
                query = query.Where(t => t.Employee.Email == currentUserEmail);
            }

            ViewBag.TotalEmployees = isAdmin ? await _context.Employees.CountAsync() : 0;
            ViewBag.TotalTasks = await query.CountAsync();
            ViewBag.PendingTasks = await query.CountAsync(t => t.Status == EmployeeTaskManager.Models.TaskStatus.Pending);
            ViewBag.InProgressTasks = await query.CountAsync(t => t.Status == EmployeeTaskManager.Models.TaskStatus.InProgress);
            ViewBag.CompletedTasks = await query.CountAsync(t => t.Status == EmployeeTaskManager.Models.TaskStatus.Completed);


            var recentTasks = await query
                .OrderByDescending(t => t.DueDate ?? DateTime.MaxValue)
                .Take(5)
                .ToListAsync();

            return View(recentTasks);
        }
    }
}
