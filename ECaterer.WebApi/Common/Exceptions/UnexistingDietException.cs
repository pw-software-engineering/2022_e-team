using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECaterer.WebApi.Common.Exceptions
{
    public class UnexistingDietException: Exception
    {
        public UnexistingDietException(string dietId): base($"Podana dieta nie istenie, id: {dietId}")
        {
        }
    }
}
