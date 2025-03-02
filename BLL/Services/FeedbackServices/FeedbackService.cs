﻿using System.Linq.Expressions;
using AutoMapper;
using DLL.Repository;
using Domain.Models.DBModels;
using Domain.Models.Request.Feedback;
using Domain.Models.Response;
using Domain.Models.Response.Feedback;

namespace BLL.Services.FeedbackAndReviewServices
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IRepository<FeedbackDBModel> _repository;
        private readonly IMapper _mapper;
        public FeedbackService(IRepository<FeedbackDBModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResultModel<bool>> CreateAsync(FeedbackCreateRequestModel request)
        {
            var model = _mapper.Map<FeedbackDBModel>(request);

            model.CreatedAt = DateTime.UtcNow;
            var result = await _repository.CreateAsync(model);
            return result.IsSuccess
                ? OperationResultModel<bool>.Success(true)
                : OperationResultModel<bool>.Failure(result.ErrorMessage!, result.Exception);
        }

        public async Task<OperationResultModel<bool>> UpdateAsync(FeedbackUpdateRequestModel request)
        {
            var existingFeedbacks = await _repository.GetFromConditionAsync(x => x.Id == request.Id);
            var existingFeedback = existingFeedbacks.FirstOrDefault();
            if (existingFeedback == null)
            {
                return OperationResultModel<bool>.Failure("Feedback not found", null);
            }

            existingFeedback.FeedbackText = request.FeedbackText;
            existingFeedback.Rating = request.Rating;

            var result = await _repository.UpdateAsync(existingFeedback);
            return !result.IsError
                ? OperationResultModel<bool>.Success(true)
                : OperationResultModel<bool>.Failure(result.Message, result.Exception);
        }

        public async Task<OperationResultModel<bool>> DeleteAsync(int id)
        {
            var result = await _repository.DeleteAsync(id);
            return !result.IsError
                ? OperationResultModel<bool>.Success(true)
                : OperationResultModel<bool>.Failure(result.Message, result.Exception);
        }

        public async Task<IEnumerable<FeedbackResponseModel>> GetFromConditionAsync(Expression<Func<FeedbackDBModel, bool>> condition)
        {
            var dbModels = await _repository.GetFromConditionAsync(condition);
            return _mapper.Map<IEnumerable<FeedbackResponseModel>>(dbModels);
        }

        public IQueryable<FeedbackDBModel> GetQuery()
        {
            return _repository.GetQuery();
        }

        public async Task<IEnumerable<FeedbackDBModel>> ProcessQueryAsync(IQueryable<FeedbackDBModel> query)
        {
            return await _repository.ProcessQueryAsync(query);
        }
    }
}
