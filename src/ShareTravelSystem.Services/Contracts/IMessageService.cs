using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShareTravelSystem.Services.Contracts
{
    public interface IMessageService
    {
        Task Create(string message, string userId);
    }
}
