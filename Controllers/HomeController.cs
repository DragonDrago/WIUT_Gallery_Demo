using DemoWIUT_Gallery.Models;
using DemoWIUT_Gallery.WIUT_Gallery_DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWIUT_Gallery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGalleryRepo galleryRepo;

        public HomeController(ILogger<HomeController> logger, IGalleryRepo galleryRepo)
        {
            _logger = logger;
            this.galleryRepo = galleryRepo;
        }

        public FileResult GalleryImage(int id)
        {
            GalleryPhoto photo = galleryRepo.GetById(id);
            if (photo != null && photo.Image?.Length > 0)
            {
                return File(photo.Image, "image/jpeg", photo.Name + "-" + photo.Id + ".jpg");
            }
            return null;
        }

       
        public IActionResult Index()
        {
            var photos = galleryRepo.GetAll().OrderBy(x => x.Name).ToList();
            return View(photos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePhoto(GalleryPhoto galleryPhoto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if(galleryPhoto.ImageUpload != null)
            {
                using(var stream = new MemoryStream())
                {
                    galleryPhoto.ImageUpload.CopyTo(stream);
                    galleryPhoto.Image = stream.ToArray();
                }
            }
            galleryRepo.AddEntity(galleryPhoto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var photo = galleryRepo.GetById(id);
            return View(photo);
        }

        [HttpPost]
        public IActionResult EditPhoto(GalleryPhoto galleryPhoto)
        {
            if (ModelState.IsValid)
            { 
                GalleryPhoto photoInDb = galleryRepo.GetById(galleryPhoto.Id);
                if (photoInDb != null && galleryPhoto.ImageUpload !=null)
                {
                    using (var stream = new MemoryStream())
                    {
                        galleryPhoto.ImageUpload.CopyToAsync(stream);
                        galleryPhoto.Image = stream.ToArray();
                    }
                }
                galleryRepo.EditEntity(galleryPhoto);
            }
            else
            {
                return View();
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var photo = galleryRepo.GetById(id);
            return View(photo);
        }

        [HttpPost]
        public IActionResult DeletePhoto (int id)
        {
            var photo = galleryRepo.GetById(id);
            if (photo == null)
            {
                return RedirectToAction("NotFound");
            }
            galleryRepo.DeleteEntity(id);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
