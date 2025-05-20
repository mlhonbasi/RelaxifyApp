using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Stress
{
    public class StressService(IStressQuestionRepository questionRepository) : IStressService
    {
        public async Task<IList<StressQuestionDto>> GetAllQuestionsAsync()
        {
            var questions =  await questionRepository.GetAllWithAnswersAsync();

            return questions.Select(x=> new StressQuestionDto
            {
                Id = x.Id,
                Text = x.QuestionText,
                IsReversed = x.IsReversed,
                Answers = x.Answers.Select(a => new StressAnswerDto
                {
                    Id = a.Id,
                    Text = a.Text,
                    Value = a.Value,
                    Order = a.Order
                }).ToList()
            }).ToList();
        }
    }
}
