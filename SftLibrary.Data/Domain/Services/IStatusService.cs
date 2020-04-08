using SftLib.Data.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SftLibrary.Data.Domain.Services
{
   public interface IStatusService
    {
        Task<IEnumerable<Status>> ListAsync();
    }
}
