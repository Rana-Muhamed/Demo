using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(/*IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository,*/ IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task< IActionResult> Index(string SearchValue)
        {
            ViewData["Message"] = "Hello view data";

            ViewBag.Message = "Hello view bag";
            if (string.IsNullOrEmpty(SearchValue))
            {
                var Employees = await _unitOfWork.EmployeeRepository.GetAll();
                var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
                return View(mappedEmp);
            }
            else
            {
                var Employees = _unitOfWork.EmployeeRepository.SearchEmployeesByName(SearchValue);
                var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
                return View(mappedEmp);
            }

            
        }

        public IActionResult Create()
        {
          //ViewBag.Departments=  _departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel EmployeeVM)
        {
            if (ModelState.IsValid)
            {
                //var employee = new Employee()
                //{
                //    Name = EmployeeVM.Name,
                //    Age= EmployeeVM.Age,
                //    Address= EmployeeVM.Address,
                //    Salary= EmployeeVM.Salary,
                //    IsActive    = EmployeeVM.IsActive,
                //    Email= EmployeeVM.Email,
                //    DepartmentId= EmployeeVM.DepartmentId,
                //};
                EmployeeVM.ImageName = DocumentSettings.UploaFile(EmployeeVM.Image, "Images");
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);
          /*     int count=*/ await _unitOfWork.EmployeeRepository.Add(mappedEmp);
               
               // if (count > 0)
               //     TempData["Message"] = "Employee is created successfully";
                await _unitOfWork.Complete();
                return RedirectToAction("Index");
            }


            return View();
        }
        public async Task< IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var Employee = await _unitOfWork.EmployeeRepository.Get(id.Value);
            if (Employee == null)
                return NotFound();
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(Employee);
            return View(viewName, mappedEmp);
        }

        public async Task< IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
            //if (id is null)
            //    return BadRequest();
            //var deparment = _EmployeeRepository.Get(id.Value);
            //if (deparment == null)
            //    return NotFound();

            //return View(deparment);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit([FromRoute] int id, EmployeeViewModel EmployeeVM)

        {
            if (id != EmployeeVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);
                    _unitOfWork.EmployeeRepository.Update(mappedEmp);
                   await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }

            return View(EmployeeVM);
        }
        public async Task< IActionResult> Delete(int? id)
        {

            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel EmployeeVM)
        {
            if (id != EmployeeVM.Id)
                return BadRequest();
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);
                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
               int count = await _unitOfWork.Complete();
                if(count > 0)
                {
                    DocumentSettings.DeleteFile(EmployeeVM.ImageName, "Images");
                }
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(EmployeeVM);
            }
        }
    }
}
