using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamechiTamoom.DataLayer.Entities.Course
{
    public class CourseGroup
    {
        [Key]
        public int GroupId { get; set; }

        [DisplayName("عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(250, ErrorMessage = "{0} نمیتواند بیش از {1} کاراکتر داشته باشد")]
        public string GroupTitle { get; set; }

        [DisplayName("حذف شده ؟")]
        public bool IsDelete { get; set; }

        [DisplayName("گروه اصلی")]
        public int? ParrentId { get; set; }


        [ForeignKey("ParrentId")]
        public List<CourseGroup> CourseGroups { get; set; }

    }
}
