using Microsoft.EntityFrameworkCore;
using VaraticPrim.Domain.Entities;
using VaraticPrim.Repository.Persistence;
using VaraticPrim.Repository.Repository.Interfaces;

namespace VaraticPrim.Repository.Repository.Implementations;

public class ServiceRepository : GenericRepository<ServiceEntity>, IServiceRepository
{
    public ServiceRepository(ApplicationDbContext context) : base(context)
    {
    }
}