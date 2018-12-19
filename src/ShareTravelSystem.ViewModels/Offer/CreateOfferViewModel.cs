namespace ShareTravelSystem.ViewModels.Offer
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateOfferViewModel 
    {
        public string Type { get; set; }

        [Display(Name = "DepartureName")]
        public int DepartureTownId { get; set; }

        [Display(Name = "DestinationName")]
        public int DestinationTownId { get; set; }

        public int Seat { get; set; }

        public decimal Price { get; set; }

        public DateTime DepartureDate { get; set; }

        public string Description { get; set; }

        //public void Configure(Profile mapper)
        //{
        //    mapper.CreateMap<Offer, CreateOfferViewModel>()
        //         .ForMember(om => om.DepartureTownName, cfg => cfg.MapFrom(o => o.DepartureTown.Name));
        //}

        //public void ConfigureMapping(Profile mapper)
        //{
        //    mapper.CreateMap<Order, OrderDetailsModel>()
        //        .ForMember(om => om.Products, cfg => cfg.MapFrom(o => o.ProductOrders.Select(po =>
        //        new ProductInOrderDetailsModel
        //        {
        //            Id = po.Id,
        //            ProductOrderId = po.Id,
        //            Name = po.Product.Name,
        //            Image = po.Product.Images.Select(i => i.Url).FirstOrDefault(),
        //            Price = po.Price,
        //            Quantity = po.Quantity,
        //            Discount = po.Discount
        //        })));
        //}
    }
}
