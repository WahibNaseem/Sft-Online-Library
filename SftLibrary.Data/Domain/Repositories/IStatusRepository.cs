using SftLib.Data.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SftLibrary.Data.Domain.Repositories
{
    public interface IStatusRepository
    {
        Task<IEnumerable<Status>> ListAsync();
    }
}
