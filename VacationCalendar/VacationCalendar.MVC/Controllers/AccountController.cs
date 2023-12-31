﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VacationCalendar.BusinessLogic.Dtos;
using VacationCalendar.BusinessLogic.Models;
using VacationCalendar.BusinessLogic.Services;

namespace VacationCalendar.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IAdminService _adminService;
        private readonly IPasswordHasher<Employee> _passwordHasher;
        //private readonly IToastNotification _toastNotification;
        public AccountController(IAccountService accountService, IPasswordHasher<Employee> password, IAdminService adminService)
        {
            _accountService = accountService;
            _passwordHasher = password;
            _adminService = adminService;
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Register()
        {
            var settings = await _adminService.GetAdminSettings();
            ViewBag.ManagerId = new SelectList(await _adminService.GetManagersAsync(), "Id", "LastName");
            ViewBag.RoleId = new SelectList(await _accountService.GetRolesAsync(), "Id", "Name", settings.RoleId.ToString());
            ViewData["VacationDays"] = settings.DefaultVacationDays;

            return View("Register");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterEmployeeDto dto)
        {
            var settings = await _adminService.GetAdminSettings();
            if (!ModelState.IsValid)
            {
                ViewBag.RoleId = new SelectList(await _accountService.GetRolesAsync(), "Id", "Name");
                ViewBag.ManagerId = new SelectList(await _adminService.GetManagersAsync(), "Id", "LastName");
                ViewData["VacationDays"] = settings.DefaultVacationDays;
                return View(dto);
            }
            ViewBag.RoleId = new SelectList(await _accountService.GetRolesAsync(), "Id", "Name");
            ViewBag.ManagerId = new SelectList(await _adminService.GetManagersAsync(), "Id", "LastName");
            ViewData["VacationDays"] = settings.DefaultVacationDays;

            await _accountService.RegisterEmployee(dto);
            return RedirectToAction("GetEmployees", "Admin");
        }
        [HttpGet]
        public IActionResult Login()
        {
            ViewData["email"] = "";
            return View("Login");
        }

        [HttpPost]
        public async Task<ActionResult> LoginAsync(LoginDto dto)
        {
            var employee = await _accountService.GetEmployeeByEmail(dto.Email);
            var isLogged = await _accountService.LoginAsync(dto, employee);

            if (isLogged)
            {
                if (!employee.FirstPasswordChange)
                {
                    return RedirectToAction("ChangePassword");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        public ActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [Authorize(Roles = "admin,manager,employee")]
        public IActionResult ChangePassword()
        {
            return View("ChangePassword");
        }

        [HttpPost]
        [Authorize(Roles = "admin,manager,employee")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changedPassword)
        {
            if(!ModelState.IsValid)
            {
                return View("ChangePassword");
            }
            var email = User.Identity.Name;
            var emp = await _accountService.GetEmployeeByEmail(email);
            await _accountService.ChangePassword(changedPassword, emp);
            return RedirectToAction("ChangePassword");

            // TODO: Dodac instrukcje warunkową
        }
    }
}
