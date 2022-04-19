using ECaterer.Contracts;
using ECaterer.Contracts.Client;
using ECaterer.Web.DTO.ClientDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.Web.Converters
{
    public static class RegisterConverter
    {
        public static RegisterUserModel Convert(RegisterDTO input)
        {
            return new RegisterUserModel()
            {
                Password = input.Password,
                Client = new ClientModel()
                {
                    Name = input.Client.FirstName,
                    LastName = input.Client.LastName,
                    Email = input.Client.Email,
                    PhoneNumber = input.Client.Phone,
                    Address = new AddressModel()
                    {
                        ApartmentNumber = input.Address.Apartment,
                        BuildingNumber = input.Address.Building,
                        City = input.Address.City,
                        PostCode = input.Address.Code,
                        Street = input.Address.Street
                    }
                }
            };
        }
    }
}
