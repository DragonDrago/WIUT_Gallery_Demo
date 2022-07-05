using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
namespace DemoWIUT_Gallery.Models
{
    public class GalleryPhoto
    {
        public int Id { get; set; }
        
        [Display(Name="Name")]
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [NotMapped]
        [Display(Name="Image")]
        public IFormFile ImageUpload { get; set; }

        [Display(Name ="Image")]
        public byte[] Image { get; set; }
    }
}
