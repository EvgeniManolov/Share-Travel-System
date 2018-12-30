namespace ShareTravelSystem.ViewModels
{
    using ShareTravelSystem.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DisplayAnnouncementViewModel : IMapFrom<Data.Models.Announcement>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        public string ShortTitle
        {
            get
            {
                if (this.Title?.Length > 50)
                {
                    return this.Title.Substring(0, 50) + "...";
                }
                else
                {
                    return this.Title;
                }
            }
        }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }


        public string ShortContent
        {
            get
            {
                if (this.Content?.Length > 100)
                {
                    return this.Content.Substring(0, 100) + "...";
                }
                else
                {
                    return this.Content;
                }
            }
        }

        [Required]
        [Display(Name = "CreateDate")]
        public DateTime CreateDate { get; set; }

        [Required]
        [Display(Name = "Author")]
        public string Author { get; set; }
    }
}