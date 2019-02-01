using ChupeLupe.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChupeLupe.Services
{
    public interface IWebServicesApi
    {
        Task<List<Promotion>> GetPromotions();
    }
}
