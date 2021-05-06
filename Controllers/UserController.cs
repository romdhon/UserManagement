using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.ViewModel;

namespace UserManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IWebHostEnvironment hostEnv;

        public UserController(IUserRepository userRepository,
                                IWebHostEnvironment hostEnv)
        {
            this.userRepository = userRepository;
            this.hostEnv = hostEnv;
        }
        [HttpGet]
        public ViewResult Index()
        {
            var model = userRepository.GetAllUser();
            return View(model);
        }

        [HttpGet]
        public ViewResult Detail(int? id)
        {
            var model = userRepository.GetUser(id ?? 1);
            return View(model);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        public IActionResult Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uFileName = null;

                if (model.Photo != null)
                {
                    uFileName = GetImagePath(model);
                }

                User user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uFileName
                };

                var newUser = userRepository.AddUser(user);
                return RedirectToAction("Detail", new { id = newUser.UserID });
            }
            return View();
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var user = userRepository.GetUser(id);
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult conFirmedDelete(int id)
        {

            var user = userRepository.GetUser(id);

            if (user.PhotoPath != null)
            {
                string folderPath = Path.Combine(hostEnv.WebRootPath, "images");
                string filePath = Path.Combine(folderPath, user.PhotoPath);
                System.IO.File.Delete(filePath);
            }
            userRepository.RemoveUser(user.UserID);

            return RedirectToAction("index", "User");
        }

        [HttpGet]
        public ViewResult Update(int id)
        {
            User user = userRepository.GetUser(id);
            UserUpdateViewModel model = new UserUpdateViewModel
            {
                ID = user.UserID,
                Name = user.Name,
                Email = user.Email,
                Department = user.Department,
                ExistingPhotoPath = user.PhotoPath
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(UserUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {

                User updateUser = userRepository.GetUser(model.ID);
                updateUser.UserID = model.ID;
                updateUser.Name = model.Name;
                updateUser.Email = model.Email;
                updateUser.Department = model.Department;
                
                if (model.Photo != null)
                {

                    if (model.ExistingPhotoPath != null)
                    {
                        string imageName = model.ExistingPhotoPath;
                        string imagePath = Path.Combine(hostEnv.WebRootPath, "images", imageName);
                        System.IO.File.Delete(imagePath);
                    }

                    string newImageName = GetImagePath(model);
                    updateUser.PhotoPath = newImageName;
                }

                userRepository.UpdateUser(updateUser);
                return RedirectToAction("Detail", new { id = updateUser.UserID });
            }
            return View();
        }

        public string GetImagePath(UserCreateViewModel modelName)
        {
            string uFileName = null;
            string folderName = Path.Combine(hostEnv.WebRootPath, "images");
            uFileName = Guid.NewGuid().ToString() + "_" + modelName.Photo.FileName;
            string filePath = Path.Combine(folderName, uFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                modelName.Photo.CopyTo(fileStream);
            }
            return uFileName;
        }
    }
}