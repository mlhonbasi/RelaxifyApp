using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class MeditationFocusLossRepository(RelaxifyDbContext context) : GenericRepository<MeditationFocusLossLog>(context), IMeditationFocusLossRepository
    {
    }
}
