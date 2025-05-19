using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Villa.HELPER.Role;
using Villa.Models;
using Villa.ModelViews;

namespace Villa.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVm);
            }
            if(registerVm is null)
            {
                return BadRequest();
            }
            AppUser user = new AppUser
            {
                UserName = registerVm.UserName,
                Email = registerVm.Email,
                Name = registerVm.Name
            };
            var result = await _userManager.CreateAsync(user, registerVm.Password);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password not correct");
            }

            await _userManager.AddToRoleAsync(user, RoleEnum.Member.ToString());

            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }
            if (loginVm is null)
            {
                return BadRequest();
            }

           var user = await _userManager.FindByNameAsync(loginVm.UserNameOrEmail);
            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(loginVm.UserNameOrEmail);
                if(user == null)
                {
                    ModelState.AddModelError("", "UserName or Password not correct");
                    return View(loginVm);
                }
            }

            var result = await _signInManager.PasswordSignInAsync(loginVm.UserNameOrEmail, loginVm.Password, true, false);
           
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(RoleEnum)))
            {
                await _roleManager.CreateAsync(new()
                {
                    Name = item.ToString()
                });
            }
            return Content("Rollar Yaradildi...");
        }
    }
}
