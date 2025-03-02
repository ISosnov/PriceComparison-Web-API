﻿using BLL.Services.ProductServices;
using BLL.Services.SellerServices;
using Domain.Models.Exceptions;
using Domain.Models.Request.Products;
using Domain.Models.Response;
using Domain.Models.SuccessCodes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparisonWebAPI.Controllers.Seller;

namespace PriceComparisonWebAPI.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GeneralApiResponseModel))]
    public class ProductSellerReferenceClickController : ControllerBase
    {
        private readonly ILogger<AuctionClickRateController> _logger;
        private readonly IProductSellerReferenceClickService _productSellerReferenceClickService;

        public ProductSellerReferenceClickController(ILogger<AuctionClickRateController> logger,
            IProductSellerReferenceClickService productSellerReferenceClickService)
        {
            _logger = logger;
            _productSellerReferenceClickService = productSellerReferenceClickService;
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralApiResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GeneralApiResponseModel))]
        public async Task<JsonResult> ProcessClickAsync([FromBody] ProductSellerReferenceClickCreateRequestModel model)
        {
            var result = await _productSellerReferenceClickService.ProcessReferenceClick(model);

            if (!result.IsSuccess)
            {
                _logger.LogError(result.Exception, AppErrors.General.CreateError);
                return GeneralApiResponseModel.GetJsonResult(AppErrors.General.CreateError, StatusCodes.Status400BadRequest, result.ErrorMessage);
            }
            return GeneralApiResponseModel.GetJsonResult(AppSuccessCodes.CreateSuccess, StatusCodes.Status200OK);
        }
    }
}
