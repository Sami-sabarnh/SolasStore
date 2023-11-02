using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Solas.BL.IRepositories;
using Solas.BL.Models;
using Solas.EF;
using Solas.EF.Repositories;
using Solas.UI.Models.ViewModels;
using System.Security.Claims;

namespace Solas.UI.Controllers
{
    public class AccountController : Controller
    {
        #region Configrition
        private IUnitOfWork unitofwork;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        public AccountController(IUnitOfWork _unitofwork ,IWebHostEnvironment _webHost, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext db)
        {
            unitofwork = _unitofwork;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion
        #region Users

      

     

        [AllowAnonymous]
        public IActionResult Register()
        {

            return View();
        }
        [AllowAnonymous]

        [HttpPost]
        
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                ApplicationUser user = new ApplicationUser
                {
                    FirstName=model.FirstName,
                    LastName=model.LastName,

                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.Mobile
                };
                bool userIsAdmin = false;

                //if (model.Email == "sabarnh123@gmail.com") {
                //    userIsAdmin = true;
                //        };
               
                var result = await _userManager.CreateAsync(user, model.Password!);
                if (result.Succeeded)
                {
                   
                    return RedirectToAction("Login", "Account");
                }
                //foreach (var err in result.Errors)
                //{
                //    ModelState.AddModelError(err.Code, err.Description);
                //}
                return View(model);

            }
            return View(model);
        }
        [AllowAnonymous]


        public IActionResult Login()
        {

            return View();
        }
        [AllowAnonymous]

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email!, model.Password!, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                //ModelState.AddModelError("", "Invalid UserName or Password");
                return View(model);

            }

            return View(model);
        }


        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");


        }
        [AllowAnonymous]
        public async Task<IActionResult> AccessDenied()
        {

            return View();
        }
        #endregion
        #region UserInformition
        private List<string> GetCompanyNamesFromDataSource()
        {
            // This is a simple example - replace this with your actual data retrieval logic
            List<string> companyNames = new List<string>
    {
        "Jordan",
        "Palestine",
        "Syria",
        "Egypt"
        // Add more company names as needed
    };

            return companyNames;
        }
        public async Task<IActionResult> EditAddress(int id)
        {
           // List<string> companies = GetCompanyNamesFromDataSource();
          //  ViewBag.Companies = companies;

            var data = await unitofwork.address.Find(x => x.AddressId == id);
            return View("EditAddress",data);
        }
        [HttpPost]
        public IActionResult EditAddress(Address address)
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            address.UserId = userid;
            unitofwork.address.Update(address.AddressId,address);
            unitofwork.Complete();
            return RedirectToAction("CheckOut","Order");
        }

        public IActionResult AddAddress()
        {
            return View();

        }
            [HttpPost]
        public  IActionResult AddAddress(Address address)
        {
            if (_signInManager.IsSignedIn(User))
            {

                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (address == null || userid == null)
                {
                    return BadRequest("address orr user id null");
                }
                address.UserId = userid;
                ModelState.Clear();
                if (ModelState.IsValid)
                {
                    unitofwork.address.Add(address);
                    unitofwork.Complete();
                    return RedirectToAction("Profile");
                }
                return View(address);
            }
            return RedirectToAction("Login", "Account");
        }
        public IActionResult DeleteAddress(int id)
        {
            if (id == 0)
            {
                return View("Error", new { Error = "Id null!" });
            }
            unitofwork.address.Delete(x=>x.AddressId==id);
            unitofwork.Complete();
            return RedirectToAction("Profile");
        }
        public async Task<IActionResult> Profile()
        {
            if (_signInManager.IsSignedIn(User))
            {

                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                //   var data = 
                ViewBag.Addresses = await unitofwork.address.Findbyid(x => x.UserId == userid);
                return View();
            }
            return RedirectToAction("Login", "Account");

        }
        #endregion
    }
}
