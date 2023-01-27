using Persistence.Entities.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.v1
{
    public interface IInsuaranceService
    {
        InsurancePolicy Create(Vehicle vehicle);
    }
}
