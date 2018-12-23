using ShareTravelSystem.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShareTravelSystem.ViewModels.Town
{
    public class DisplayTownViewModel : IMapFrom<Data.Models.Town>
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
