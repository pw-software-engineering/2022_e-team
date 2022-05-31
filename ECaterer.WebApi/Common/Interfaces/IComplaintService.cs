using ECaterer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Interfaces
{
    public interface IComplaintService
    {
        Task<(bool exists, bool answered)> AnswerComplaint(string complaintId, string answer);
        Task<IEnumerable<Complaint>> GetOrdersComplaints();
    }
}
