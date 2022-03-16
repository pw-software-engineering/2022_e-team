using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECaterer.Core.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
