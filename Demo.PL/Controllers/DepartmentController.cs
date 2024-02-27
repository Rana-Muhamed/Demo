using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	[Authorize]
	public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task< IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAll();
           
            return View(departments);
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if(ModelState.IsValid)
            {
              /*  int count =*/ await _unitOfWork.DepartmentRepository.Add(department);
                // if (count > 0)
                //     TempData["Message"] = "department is created successfully";
               await _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View();
        }
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var deparment = await _unitOfWork.DepartmentRepository.Get(id.Value);
            if(deparment == null)
                return NotFound();

            return View(viewName, deparment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id,"Edit");
            //if (id is null)
            //    return BadRequest();
            //var deparment = _departmentRepository.Get(id.Value);
            //if (deparment == null)
            //    return NotFound();

            //return View(deparment);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute ] int id ,Department department)

        {
            if (id != department.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Update(department);
                   await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch ( Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                
            }

            return View(department);
        }
        public async Task<IActionResult> Delete(int? id) {

            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, Department department)
        {
            if (id != department.Id)
                return BadRequest();
            try
            {
                _unitOfWork.DepartmentRepository.Delete(department);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(department);
            }
        }
    }
}
