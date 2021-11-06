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
    public class CourseEpisode
    {
        [Key]
        public int EpisodeId { get; set; }

        public int CourseId { get; set; }

        [DisplayName("عنوان بخش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(400, ErrorMessage = "{0} نمیتواند بیش از {1} کاراکتر داشته باشد")]
        public string EpisodeTitle { get; set; }

        [DisplayName("زمان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public TimeSpan EpisodeTime { get; set; }

        [DisplayName("فایل اپیزود")]
        [MaxLength(300, ErrorMessage = "{0} نمیتواند بیش از {1} کاراکتر داشته باشد")]
        public string EpisodeFileName { get; set; }

        [DisplayName("رایگان است ؟")]
        public bool IsFree { get; set; }



        #region Realtions

        public Course Course { get; set; }

        #endregion
    }
}
