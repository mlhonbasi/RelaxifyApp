using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Stress
{
    public interface IStressService
    {
        Task<IList<StressQuestionDto>> GetAllQuestionsAsync();
    }
}
