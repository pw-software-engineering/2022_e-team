using ECaterer.Contracts.Orders;
using ECaterer.Web.DTO;
using ECaterer.Web.DTO.ClientDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.Web.Converters
{
    public static class ComplaintConverter
    {

        public static ComplaintOrderDTO ConvertBack (ComplaintModel dto)
        {
            return new ComplaintOrderDTO
            {
                ClientName = dto.ComplaintId,
                Status =dto.Status,
                ComplaintDate = dto.Date,
                Description = dto.Description
            }
        }
    }
}
