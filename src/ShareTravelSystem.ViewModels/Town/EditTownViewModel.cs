using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShareTravelSystem.ViewModels.Town
{
    public class EditTownViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
