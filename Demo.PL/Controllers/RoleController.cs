using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Demo.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {


                var roles = await _roleManager.Roles.Select(R => new RoleViewModel()
                {

                    Id = R.Id,
                    RoleName= R.Name

                }).ToListAsync();
                return View(roles);
            }
            else
            {


                var role = await _roleManager.FindByNameAsync(name);
                if(role is not null) {
                    var mappedRole = new RoleViewModel()
                    {

                        Id = role?.Id,
                        RoleName = role?.Name
                    };
                    return View(new List<RoleViewModel>() { mappedRole });
                }
                return View(Enumerable.Empty<RoleViewModel>());
                
            }
        }

        public IActionResult Create() { return View(); }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleVM) {
            if (ModelState.IsValid)
            {
                var mappedRole =  _mapper.Map<RoleViewModel, IdentityRole>(roleVM);
                await _roleManager.CreateAsync(mappedRole);
                return RedirectToAction(nameof(Index));
            }
            return View(roleVM); }
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();
            var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(role);
            return View(viewName, mappedRole);
        }

        public async Task<IActionResult> Edit(string id)
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
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel updatedRole)

        {
            if (id != updatedRole.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {

                    //var mappedUser = _mapper.Map<UsersViewModel, ApplicationUser>(UpdatedUser);
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = updatedRole.RoleName;
                    //user.Email = UpdatedUser.Email;
                    //user.SecurityStamp = Guid.NewGuid().ToString();
                    await _roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }

            return View(updatedRole);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {

            return await Details(id, "Delete");
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {

            try
            {
                var user = await _roleManager.FindByIdAsync(id);

                //user.Email = UpdatedUser.Email;
                //user.SecurityStamp = Guid.NewGuid().ToString();
                await _roleManager.UpdateAsync(user);
                await _roleManager .DeleteAsync(user);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
