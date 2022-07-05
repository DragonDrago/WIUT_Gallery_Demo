using DemoWIUT_Gallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DemoWIUT_Gallery.WIUT_Gallery_DAL.Repository
{
    public class GalleryRepo : IGalleryRepo
    {
        private readonly ApplicationDbContext applicationDbContext;

        public GalleryRepo(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void AddEntity(GalleryPhoto galleryPhoto)
        {
            applicationDbContext.GalleryPhotos.Add(galleryPhoto);
            applicationDbContext.SaveChanges();
        }

        public void DeleteEntity(int id)
        {
            var photo = applicationDbContext.GalleryPhotos.FirstOrDefault(x => x.Id == id);
            applicationDbContext.GalleryPhotos.Remove(photo);
            applicationDbContext.SaveChanges();
        }

        public void EditEntity(GalleryPhoto updateGalleryPhoto)
        {
            var photoInDb = applicationDbContext.GalleryPhotos.Find(updateGalleryPhoto.Id);
            photoInDb.Name = updateGalleryPhoto.Name;
            if(updateGalleryPhoto.Image != null)
            {
                photoInDb.Image = updateGalleryPhoto.Image;
            }
            
            applicationDbContext.SaveChanges();
        }

        public IEnumerable<GalleryPhoto> GetAll()
        {
            var galleryPhotos = applicationDbContext.GalleryPhotos.ToList();
            return galleryPhotos;
        }

        public GalleryPhoto GetById(int id)
        {
            return applicationDbContext.GalleryPhotos.Find(id);
        }
    }
}
