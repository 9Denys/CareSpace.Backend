using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareSpace.Backend.Application.Interfaces
{
    public interface IAppointmentCompletionService
    {
        Task CompleteExpiredAppointmentsAsync(CancellationToken cancellationToken);
    }
}
