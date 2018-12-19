
namespace ShareTravelSystem.ViewModels.Offer
{
    using ShareTravelSystem.Common;
    using System;
    using System.ComponentModel.DataAnnotations;
    using ShareTravelSystem.Data.Models;
    using AutoMapper;

    public class DisplayOfferViewModel : IMapFrom<Offer>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Author")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "DepartureDate")]
        public DateTime DepartureDate { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }


        [Required]
        [Display(Name = "Seat")]
        public int Seat { get; set; }

        [Required]
        [Display(Name = "DepartureTownName")]
        public string DepartureTownName { get; set; }

        [Required]
        [Display(Name = "DestinationTownName")]
        public string DestinationTownName { get; set; }
        
        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        //public void Configure(Profile mapper)
        //{
        //    mapper.CreateMap<Offer, DisplayOfferViewModel>()
        //         .ForMember(om => om.DepartureTownName, cfg => cfg.MapFrom(o => o.DepartureTown.Name));
        //}
    }
}
