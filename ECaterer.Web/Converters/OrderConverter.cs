using ECaterer.Contracts.Orders;
using ECaterer.Web.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.Web.Converters
{
    public static class OrderConverter
    {
        public static AddOrderModel Convert(ClientOrderDTO dto)
        {
            return new AddOrderModel
            {
                DietIDs = dto.DietIds.ToArray(),
                DeliveryDetails = new DeliveryDetailsModel
                {
                    Address = new Contracts.Client.AddressModel
                    {
                        Street = dto.address?.Street,
                        BuildingNumber = dto.address?.Building,
                        ApartmentNumber = dto.address?.Apartment,
                        PostCode = dto.address?.Code,
                        City = dto.address?.City
                    },
                    CommentForDeliverer = dto.Comment,
                    PhoneNumber = null
                },
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };
        }

    }
}
