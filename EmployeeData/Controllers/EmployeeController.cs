using System.Runtime.ExceptionServices;
using EmployeeData.DAL;
using EmployeeData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeData.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }
        [HttpGet]
        public IActionResult GetEmplyeeDetails()
        {
            //This Employee Model will be use to communicate with DataBase..
            var employees = _employeeDbContext.Employees.ToList();

            //this EmployeeViewModel will be use to pass the Data from Controller-to-View and View-to-Controller
            List<EmployeeViewModel> employeeList = new List<EmployeeViewModel>();

            if (employees != null)
            {
                foreach (var employee in employees)
                {
                    var EmployeeViewModel = new EmployeeViewModel() {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DOB = employee.DOB,
                        Designation = employee.Designation,
                        Experiance = employee.Experiance,
                        Salary = employee.Salary
                    };
                    employeeList.Add(EmployeeViewModel);
                }
                return View(employeeList);
            }
            //if there is no data, will retun 
            return Ok(employeeList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = new Employee()
                    {
                        FirstName = employeeData.FirstName,
                        LastName = employeeData.LastName,
                        DOB = employeeData.DOB,
                        Designation = employeeData.Designation,
                        Experiance = employeeData.Experiance,
                        Salary = employeeData.Salary
                    };
                    _employeeDbContext.Employees.Add(employee);
                    _employeeDbContext.SaveChanges();
                    TempData["SuccessMessage"] = "Employee data created successfully!";
                    return RedirectToAction("GetEmplyeeDetails");
                }
                else
                {
                    TempData["errorMessage"] = "Model data is not valid";
                    return View();
                }
            }
            catch (Exception error)
            {
                TempData["errorMessage"] = error.Message;
                return View();
            }
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var employee = _employeeDbContext.Employees.SingleOrDefault(x => x.Id == id);

                if (employee != null)
                {
                    var employeeView = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DOB = employee.DOB,
                        Designation = employee.Designation,
                        Experiance = employee.Experiance,
                        Salary = employee.Salary
                    };
                    return View(employeeView);
                }
                else
                {
                    TempData["errorMessage"] = $"Employee detals not available with this Id: {id}";
                    return RedirectToAction("GetEmplyeeDetails");
                }
            }
            catch (Exception error)
            {
                TempData["errorMessage"] = error.Message;
                return RedirectToAction("GetEmplyeeDetails");
            }
        }

        [HttpPost]                    
        public IActionResult Edit(EmployeeViewModel model) {
			//Receive the data as EmployeeViewModel, responsible to pass data from the controller-to-View and View-to-Controller
			try
			{
                if (ModelState.IsValid)
                {
                    //Employee class is the responsible to communicate with the Database.
                    var employee = new Employee()
                    {
                        Id = model.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        DOB = model.DOB,
                        Designation = model.Designation,
                        Experiance = model.Experiance,
                        Salary = model.Salary
                    };
                    _employeeDbContext.Employees.Update(employee);
                    _employeeDbContext.SaveChanges();
                    TempData["successMessage"] = "Employee details updated successfully!";
                    return RedirectToAction("GetEmplyeeDetails");
                }
                else
                {
                    TempData["errorMessage"] = "Model data is invalid";
                    return View();

                }
            }
            catch (Exception error)
            {
				TempData["errorMessage"] = error.Message;
				return View();
			}
        }


		[HttpGet]
		public IActionResult Delete(int id)
		{
			try
			{
				var employee = _employeeDbContext.Employees.SingleOrDefault(x => x.Id == id);

				if (employee != null)
				{
					var employeeView = new EmployeeViewModel()
					{
						Id = employee.Id,
						FirstName = employee.FirstName,
						LastName = employee.LastName,
						DOB = employee.DOB,
						Designation = employee.Designation,
						Experiance = employee.Experiance,
						Salary = employee.Salary
					};
					return View(employeeView);
				}
				else
				{
					TempData["errorMessage"] = $"Employee detals not available with this Id: {id}";
					return RedirectToAction("GetEmplyeeDetails");
				}
			}
			catch (Exception error)
			{
				TempData["errorMessage"] = error.Message;
				return RedirectToAction("GetEmplyeeDetails");
			}
		}

        [HttpPost]
        public IActionResult Delete(EmployeeViewModel model)
        {
            try
            {
                var employee = _employeeDbContext.Employees.SingleOrDefault(x => x.Id == model.Id);
                if (employee != null)
                {
                    _employeeDbContext.Employees.Remove(employee);
                    _employeeDbContext.SaveChanges();
                    TempData["successMessage"] = "Employee deleted successfully";
                    return RedirectToAction("GetEmplyeeDetails");
                }
                else
                {
                    TempData["errorMessage"] = $"Employee detals not available with this Id: {model.Id}";
                    return RedirectToAction("GetEmplyeeDetails");
                }
            }
            catch (Exception error)
            {
				TempData["errorMessage"] = error.Message;
				return RedirectToAction("GetEmplyeeDetails");
			}
        }



		protected void Login_Click(object sender, EventArgs e)//login button event
		{
			Response.Redirect("GetEmplyeeDetails");//When u click login button the page redirect to userHome.aspx
		}


	}
}
