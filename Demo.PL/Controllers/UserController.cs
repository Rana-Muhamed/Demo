using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Demo.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
            _mapper = mapper;
        }

		public async Task<IActionResult> Index(string email)
		{
			if (string.IsNullOrEmpty(email))
			{


				var users = await _userManager.Users.Select(U => new UsersViewModel()
				{

					Id = U.Id,
					FName = U.FName,
					LName = U.LName,
					Email = U.Email,
					PhoneNumber = U.PhoneNumber,
					Roles = _userManager.GetRolesAsync(U).Result
				}).ToListAsync();
				return View(users);
			}
			else
			{


				var user = await _userManager.FindByEmailAsync(email);

				var mappedUser = new UsersViewModel()
				{
					Id = user.Id,
					FName = user.FName,
					LName = user.LName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber,
					Roles = _userManager.GetRolesAsync(user).Result
				};
				return View(new List<UsersViewModel>() { mappedUser});
			}
		}

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var mappedUser = _mapper.Map<ApplicationUser , UsersViewModel>(user);
            return View(viewName, mappedUser);
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
        public async Task<IActionResult> Edit([FromRoute] string id, UsersViewModel UpdatedUser)

        {
            if (id != UpdatedUser.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {

                    //var mappedUser = _mapper.Map<UsersViewModel, ApplicationUser>(UpdatedUser);
                    var user = await _userManager.FindByIdAsync(id);
                    user.FName= UpdatedUser.FName;
                    user.LName= UpdatedUser.LName;
                    user.PhoneNumber = UpdatedUser.PhoneNumber;
                    //user.Email = UpdatedUser.Email;
                    //user.SecurityStamp = Guid.NewGuid().ToString();
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }

            return View(UpdatedUser);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {

            return await Details(id, "Delete");
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete( string id)
        {

            try
            {
                var user = await _userManager.FindByIdAsync(id);

                //user.Email = UpdatedUser.Email;
                //user.SecurityStamp = Guid.NewGuid().ToString();
                await _userManager.UpdateAsync(user);
                await _userManager.DeleteAsync(user);

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
