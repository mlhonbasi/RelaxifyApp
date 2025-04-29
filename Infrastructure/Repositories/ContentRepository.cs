using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class ContentRepository(RelaxifyDbContext context) :GenericRepository<Content>(context), IContentRepository
    {

    }
}
