namespace ShareTravelSystem.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class DisplayAnnouncementViewModel : IMapFrom<Data.Models.Announcement>
    {
        public int Id { get; set; }

        [Required] [Display(Name = "Title")] public string Title { get; set; }

        public string ShortTitle
        {
            get
            {
                if (Title?.Length > 50)
                {
                    return Title.Substring(0, 50) + "...";
                }

                return Title;
            }
        }

        [Required] [Display(Name = "Content")] public string Content { get; set; }


        public string ShortContent
        {
            get
            {
                if (Content?.Length > 100)
                {
                    return Content.Substring(0, 100) + "...";
                }

                return Content;
            }
        }

        [Required]
        [Display(Name = "CreateDate")]
        public DateTime CreateDate { get; set; }

        [Required] [Display(Name = "Author")] public string Author { get; set; }
    }
}