﻿using AutoMapper;
using DLL.Repository;
using Domain.Models.DBModels;
using Domain.Models.Primitives;
using Domain.Models.Request.Categories;
using Domain.Models.Response;
using Domain.Models.Response.Categories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BLL.Services.CategoryCharacteristicService
{
    public class CategoryCharacteristicService : ICategoryCharacteristicService
    {
        private readonly ICompositeKeyRepository<CategoryCharacteristicDBModel, int, int> _categoryCharacteristicRepository;
        private readonly IRepository<CharacteristicDBModel, int> _characteristicRepository;
        private readonly IMapper _mapper;

        public CategoryCharacteristicService(
            ICompositeKeyRepository<CategoryCharacteristicDBModel, int, int> categoryCharacteristicRepository,
            IRepository<CharacteristicDBModel, int> characteristicRepository,
            IMapper mapper)
        {
            _categoryCharacteristicRepository = categoryCharacteristicRepository;
            _characteristicRepository = characteristicRepository;
            _mapper = mapper;
        }

        public async Task<OperationResultModel<bool>> CreateAsync(CategoryCharacteristicDBModel model)
        {

            var characteristicList = await _characteristicRepository.GetFromConditionAsync(x => x.Id == model.CharacteristicId);
            if (!characteristicList.Any())
            {
                return OperationResultModel<bool>.Failure($"Characteristic with ID {model.CharacteristicId} does not exist.");
            }

            var existingRecords = await GetFromConditionAsync(x => x.CategoryId == model.CategoryId && x.CharacteristicId == model.CharacteristicId);
            if (existingRecords.Any())
            {
                return OperationResultModel<bool>.Failure($"Record with CategoryId = {model.CategoryId} and CharacteristicId = {model.CharacteristicId} already exists.");
            }

            var repoResult = await _categoryCharacteristicRepository.CreateAsync(model);
            return repoResult.IsSuccess
                ? OperationResultModel<bool>.Success(true)
                : OperationResultModel<bool>.Failure(repoResult.ErrorMessage!, repoResult.Exception);
        }

        public async Task<OperationResultModel<bool>> CreateMultipleAsync(CategoryCharacteristicRequestModel request)
        {
            int successCount = 0;
            var errors = new List<string>();

            foreach (var id in request.CharacteristicIds)
            {
                var model = new CategoryCharacteristicDBModel
                {
                    CategoryId = request.CategoryId,
                    CharacteristicId = id
                };
                var result = await CreateAsync(model);
                if (result.IsSuccess)
                {
                    successCount++;
                }
                else
                {
                    errors.Add(result.ErrorMessage!);
                }
            }

            if (successCount == 0)
            {
                return OperationResultModel<bool>.Failure("No characteristic was successfully added. " + string.Join("; ", errors));
            }
            return OperationResultModel<bool>.Success(true);
        }

        public async Task<OperationResultModel<bool>> UpdateAsync(CategoryCharacteristicUpdateRequestModel request)
        {
            var existingRecords = await _categoryCharacteristicRepository
                .GetFromConditionAsync(x => x.CategoryId == request.OldCategoryId && x.CharacteristicId == request.OldCharacteristicId);

            var existing = existingRecords.FirstOrDefault();
            if (existing == null)
            {
                return OperationResultModel<bool>.Failure("Entity not found.");
            }

            var duplicateCheck = await _categoryCharacteristicRepository
               .GetFromConditionAsync(x => x.CategoryId == request.NewCategoryId && x.CharacteristicId == request.NewCharacteristicId);
            var dublicate = duplicateCheck.FirstOrDefault();

            if (dublicate != null)
            {
                return OperationResultModel<bool>.Failure($"CategoryCharacteristic with CategoryId {request.NewCategoryId} and CharacteristicId {request.NewCharacteristicId} already exists.");
            }

            var compositeKey = new CompositeKey<int, int> { Key1 = request.OldCategoryId, Key2 = request.OldCharacteristicId };
            var deleteResult = await _categoryCharacteristicRepository.DeleteAsync(compositeKey);
            if (!deleteResult.IsSuccess)
            {
                return OperationResultModel<bool>.Failure(deleteResult.ErrorMessage!, deleteResult.Exception);
            }

            var newRecord = new CategoryCharacteristicDBModel
            {
                CategoryId = request.NewCategoryId,
                CharacteristicId = request.NewCharacteristicId
            };

            var createResult = await _categoryCharacteristicRepository.CreateAsync(newRecord);
            return createResult.IsSuccess
                ? OperationResultModel<bool>.Success(true)
                : OperationResultModel<bool>.Failure(createResult.ErrorMessage!, createResult.Exception);

        }

        public async Task<OperationResultModel<bool>> DeleteAsync(int categoryId, int characteristicId)
        {
            var compositeKey = new CompositeKey<int, int> { Key1 = categoryId, Key2 = characteristicId };
            var repoResult = await _categoryCharacteristicRepository.DeleteAsync(compositeKey);
            return repoResult.IsSuccess
                ? OperationResultModel<bool>.Success(true)
                : OperationResultModel<bool>.Failure(repoResult.ErrorMessage!, repoResult.Exception);
        }

        public async Task<OperationResultModel<bool>> DeleteMultipleAsync(CategoryCharacteristicRequestModel request)
        {
            int successCount = 0;
            var errors = new List<string>();

            foreach (var id in request.CharacteristicIds)
            {
                var compositeKey = new CompositeKey<int, int> { Key1 = request.CategoryId, Key2 = id };
                var repoResult = await _categoryCharacteristicRepository.DeleteAsync(compositeKey);
                if (repoResult.IsSuccess)
                {
                    successCount++;
                }
                else
                {
                    errors.Add(repoResult.ErrorMessage!);
                }
            }

            if (successCount == 0)
            {
                return OperationResultModel<bool>.Failure("No characteristic was successfully deleted. " + string.Join("; ", errors));
            }
            return OperationResultModel<bool>.Success(true);
        }

        public IQueryable<CategoryCharacteristicDBModel> GetQuery()
        {
            return _categoryCharacteristicRepository.GetQuery();
        }

        public async Task<IEnumerable<CategoryCharacteristicDBModel>> GetFromConditionAsync(Expression<Func<CategoryCharacteristicDBModel, bool>> condition)
        {
            return await _categoryCharacteristicRepository.GetFromConditionAsync(condition);
        }

        public async Task<IEnumerable<CategoryCharacteristicDBModel>> ProcessQueryAsync(IQueryable<CategoryCharacteristicDBModel> query)
        {
            return await _categoryCharacteristicRepository.ProcessQueryAsync(query);
        }

        public async Task<OperationResultModel<IEnumerable<CategoryCharacteristicResponseModel>>> GetMappedCharacteristicsAsync(int categoryId)
        {
            var dbModels = await _categoryCharacteristicRepository.GetQuery()
                                .Where(x => x.CategoryId == categoryId)
                                .Include(x => x.Characteristic)
                                .ToListAsync();

            if (dbModels == null || !dbModels.Any())
            {
                return OperationResultModel<IEnumerable<CategoryCharacteristicResponseModel>>.Failure("No records found.");
            }

            var mapped = _mapper.Map<IEnumerable<CategoryCharacteristicResponseModel>>(dbModels);
            return OperationResultModel<IEnumerable<CategoryCharacteristicResponseModel>>.Success(mapped);
        }
    }
}
