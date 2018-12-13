﻿namespace ShareTravelSystem.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DisplayAnnouncementViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "CreateDate")]
        public DateTime CreateDate { get; set; }

        [Required]
        [Display(Name = "Author")]
        public string Author { get; set; }
    }
}