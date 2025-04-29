using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class BreathingContentRepository(RelaxifyDbContext context) :GenericRepository<BreathingContent>(context), IBreathingContentRepository
    {
    }
}
