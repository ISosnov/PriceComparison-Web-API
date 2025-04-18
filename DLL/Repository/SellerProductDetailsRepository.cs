﻿using DLL.Context;
using Domain.Models.DBModels;
using Domain.Models.Exceptions;
using Domain.Models.Response;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DLL.Repository.Abstractions
{
    //deprecated
    public class SellerProductDetailsRepository : ISellerProductDetailsRepository
    {
        private readonly AppDbContext _context;

        public SellerProductDetailsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OperationDetailsResponseModel> CreateAsync(SellerProductDetailsDBModel entity)
        {
            try
            {
                _context.SellerProductDetails.Add(entity);
                await _context.SaveChangesAsync();
                return new OperationDetailsResponseModel() { IsError = false, Message = "Create success", Exception = null };
            }
            catch (Exception ex)
            {
                return new OperationDetailsResponseModel() { IsError = true, Message = "Create error", Exception = ex };
            }
        }
        public async Task<OperationDetailsResponseModel> DeleteAsync(int productId, int sellerId)
        {
            try
            {
                var entity = await _context.SellerProductDetails
                                      .FirstOrDefaultAsync(cc => cc.ProductId == productId &&
                                                                 cc.SellerId == sellerId);
                if (entity == null)
                {
                    return new OperationDetailsResponseModel 
                    { 
                        IsError = true, 
                        Message = "Entity not found",
                        Exception = new EntityNotFoundException("Price not found")
                    };
                }

                _context.SellerProductDetails.Remove(entity);
                await _context.SaveChangesAsync();
                return new OperationDetailsResponseModel() { IsError = false, Message = "Delete success", Exception = null };
            }
            catch (Exception ex)
            {
                return new OperationDetailsResponseModel() { IsError = true, Message = "Delete error", Exception = ex };
            }
        }

        public async Task<OperationDetailsResponseModel> UpdateAsync(SellerProductDetailsDBModel entity)
        {
            try
            {
                _context.SellerProductDetails.Update(entity);
                await _context.SaveChangesAsync();
                return new OperationDetailsResponseModel() { IsError = false, Message = "Update success", Exception = null };
            }
            catch (Exception ex)
            {
                return new OperationDetailsResponseModel() { IsError = true, Message = "Update error", Exception = ex };
            }
        }

        public virtual async Task<IEnumerable<SellerProductDetailsDBModel>> GetFromConditionAsync(Expression<Func<SellerProductDetailsDBModel, bool>> condition) =>
            await _context.SellerProductDetails.Where(condition).ToListAsync().ConfigureAwait(false);

        public IQueryable<SellerProductDetailsDBModel> GetQuery() => _context.SellerProductDetails.AsQueryable();

        public async Task<IEnumerable<SellerProductDetailsDBModel>> ProcessQueryAsync(IQueryable<SellerProductDetailsDBModel> query) => 
            await query.ToListAsync();
    }
}
