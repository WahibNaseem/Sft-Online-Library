using SftLib.Data.Domain.Models;
using SftLibrary.Data.Domain.Repositories;
using SftLibrary.Data.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SftLibrary.Service.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
        public async Task<IEnumerable<Status>> ListAsync()
        {
            return await _statusRepository.ListAsync();
        }
    }
}
