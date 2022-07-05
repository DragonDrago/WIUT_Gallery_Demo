using DemoWIUT_Gallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DemoWIUT_Gallery.WIUT_Gallery_DAL.Repository
{
    public interface IGalleryRepo
    {
        void AddEntity(GalleryPhoto galleryPhoto);
        IEnumerable<GalleryPhoto> GetAll();
        GalleryPhoto GetById (int id);
        void EditEntity (GalleryPhoto galleryPhoto);
        void DeleteEntity (int id);
    }
}
