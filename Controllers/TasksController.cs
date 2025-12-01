using EmployeeTaskManager.Data;
using EmployeeTaskManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskStatus = EmployeeTaskManager.Models.TaskStatus;

namespace EmployeeTaskManager.Controllers
{
    [Authorize]  // Everyone must be logged in
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index(string? status, int? employeeId)
        {
            var userEmail = User.Identity?.Name;
            var isAdmin = User.IsInRole("Admin");

            var query = _context.TaskItems
                .Include(t => t.Employee)
                .AsQueryable();

            // Employees only see their own tasks
            if (!isAdmin)
            {
                query = query.Where(t => t.Employee.Email == userEmail);
            }

            if (!string.IsNullOrEmpty(status) &&
                Enum.TryParse<EmployeeTaskManager.Models.TaskStatus>(status, ignoreCase: true, out var parsedStatus)
)
            {
                query = query.Where(t => t.Status == parsedStatus);
            }

            if (employeeId.HasValue)
            {
                query = query.Where(t => t.EmployeeId == employeeId.Value);
            }

            ViewBag.IsAdmin = isAdmin;
            ViewBag.Employees = await _context.Employees.OrderBy(e => e.Name).ToListAsync();

            return View(await query.OrderByDescending(t => t.DueDate).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var taskItem = await _context.TaskItems.Include(t => t.Employee).FirstOrDefaultAsync(m => m.Id == id);

            if (taskItem == null) return NotFound();

            // Access control: Employees can only open their own task
            if (!User.IsInRole("Admin") && taskItem.Employee.Email != User.Identity?.Name)
                return Forbid();

            ViewBag.IsAdmin = User.IsInRole("Admin");
            return View(taskItem);
        }

        // Only Admin can Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            await PopulateEmployeesDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await PopulateEmployeesDropDownList(taskItem.EmployeeId);
            return View(taskItem);
        }

        // Only Admin can Edit
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem == null) return NotFound();

            await PopulateEmployeesDropDownList(taskItem.EmployeeId);
            return View(taskItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, TaskItem taskItem)
        {
            if (id != taskItem.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskItemExists(taskItem.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            await PopulateEmployeesDropDownList(taskItem.EmployeeId);
            return View(taskItem);
        }

        // Only Admin can Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var taskItem = await _context.TaskItems.Include(t => t.Employee).FirstOrDefaultAsync(m => m.Id == id);

            if (taskItem == null) return NotFound();

            return View(taskItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem != null)
            {
                _context.TaskItems.Remove(taskItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // EMPLOYEE ONLY: Change status (New Feature!)
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateStatus(
    int id,
    EmployeeTaskManager.Models.TaskStatus status)

        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null)
                return NotFound();

            // Employee can only update their own tasks
            if (!User.IsInRole("Admin"))
            {
                var currentEmail = User.Identity?.Name;
                var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == task.EmployeeId);

                if (employee?.Email != currentEmail)
                    return Forbid();
            }

            task.Status = status;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




        private bool TaskItemExists(int id)
        {
            return _context.TaskItems.Any(e => e.Id == id);
        }

        private async Task PopulateEmployeesDropDownList(object? selectedEmployee = null)
        {
            ViewBag.EmployeeId = new SelectList(
                await _context.Employees.OrderBy(e => e.Name).ToListAsync(),
                "Id", "Name", selectedEmployee);
        }
    }
}
