using ECaterer.Core;
using ECaterer.Core.Models;
using ECaterer.Core.Models.Enums;
using ECaterer.WebApi.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly DataContext _context;

        public ComplaintService(DataContext context)
        {
            _context = context;
        }

        public async Task<(bool exists, bool answered)> AnswerComplaint(string orderId, string answer)
        {
            var order = _context.Orders.Include(o => o.Complaint).FirstOrDefault(o => o.OrderId == orderId);
            var complaint = order.Complaint;

            if (order == default || complaint is null)
                return (exists: false, answered: false);
            if (complaint.Status != (int)ComplaintStatus.Opened)
                return (exists: true, answered: false);

            complaint.Answer = answer;
            complaint.Status = (int)ComplaintStatus.Closed;
            await _context.SaveChangesAsync();

            return (exists: true, answered: true);
        }

        public async Task<IEnumerable<Complaint>> GetOrdersComplaints()
        {
            return await _context.Complaints.ToArrayAsync();
        }
    }
}
