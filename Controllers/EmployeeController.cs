using System;
using Microsoft.AspNetCore.Mvc;
using CrudDemo.Models;
using Microsoft.EntityFrameworkCore;
using CrudDemo.Data;

namespace CrudDemo.Controllers
{
	public class EmployeeController : Controller
	{
        private readonly CrudDbContext crudDbContext;

        public EmployeeController(CrudDbContext crudDbContext)
		{
            this.crudDbContext = crudDbContext;
        }

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var employess = await crudDbContext.Employess.ToListAsync();
			return View(employess);
		}
		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> View(Guid Id)
		{
            var employee = await crudDbContext.Employess.FirstOrDefaultAsync(x => x.Id == Id);
			if(employee!= null)
			{
				var viewModel = new UpdateViewModel()
				{
					Id = employee.Id,
					FirstName = employee.FirstName,
					LastName = employee.LastName,
					Role = employee.Role,
					PhoneNum = employee.PhoneNum,
					Salary = employee.Salary
				};
				return await Task.Run( ()=>  View("View", viewModel));
			}
			return RedirectToAction("Index");
		}
		[HttpPost]
        public async  Task<IActionResult> Add(AddEmployee addEmployee)
		{
			var Employees = new Employee()
			{
				FirstName = addEmployee.FirstName,
				LastName = addEmployee.LastName,
				Role = addEmployee.Role,
				PhoneNum = addEmployee.PhoneNum,
				Salary = addEmployee.Salary
			}; 
			await crudDbContext.AddAsync(Employees);
			await crudDbContext.SaveChangesAsync();

			return RedirectToAction("Index");

		}
		[HttpPost]
		public async Task<IActionResult> View(UpdateViewModel updateViewModel)
		{
			var updateDb = await crudDbContext.Employess.FindAsync(updateViewModel.Id);
			if(updateDb!= null)
			{
				updateDb.FirstName = updateViewModel.FirstName; 
				updateDb.LastName = updateViewModel.LastName;
				updateDb.Role = updateViewModel.Role;
				updateDb.PhoneNum = updateViewModel.PhoneNum;
				updateDb.Salary = updateViewModel.Salary;

				await crudDbContext.SaveChangesAsync();
				return RedirectToAction("Index");

			}
			return RedirectToAction("Index");

		}
		[HttpPost]
		public async Task<IActionResult> Delete(UpdateViewModel updateViewModel)
		{
            var deleteDb = await crudDbContext.Employess.FindAsync(updateViewModel.Id);
			if(deleteDb!= null)
			{
				crudDbContext.Employess.Remove(deleteDb);
				await crudDbContext.SaveChangesAsync();
			}
            return RedirectToAction("Index");
        }
	}
}
