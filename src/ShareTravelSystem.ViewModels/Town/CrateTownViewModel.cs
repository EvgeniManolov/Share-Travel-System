using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShareTravelSystem.ViewModels.Town
{
    public class CrateTownViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
