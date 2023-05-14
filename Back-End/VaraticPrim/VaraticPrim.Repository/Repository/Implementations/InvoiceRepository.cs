using Microsoft.EntityFrameworkCore;
using VaraticPrim.Domain.Entities;
using VaraticPrim.Repository.Persistence;
using VaraticPrim.Repository.Repository.Interfaces;

namespace VaraticPrim.Repository.Repository.Implementations;

public class InvoiceRepository : GenericRepository<InvoiceEntity>, IInvoiceRepository
{
    public InvoiceRepository(ApplicationDbContext context) : base(context)
    {
    }
}