﻿using AutoMapper;
using Domain.Models.DBModels;
using Domain.Models.Request.Categories;
using Domain.Models.Request.Feedback;
using Domain.Models.Request.Filters;
using Domain.Models.Request.Products;
using Domain.Models.Request.Seller;
using Domain.Models.Response.Categories;
using Domain.Models.Response.Feedback;
using Domain.Models.Response.Filters;
using Domain.Models.Response.Gpt.Product;
using Domain.Models.Response.Products;
using Domain.Models.Response.Seller;
using PriceComparisonWebAPI.Infrastructure.MapperResolvers;

namespace PriceComparisonWebAPI.Infrastructure
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            // CHARACTERISTICS
            CreateMap<CharacteristicDBModel, CharacteristicResponseModel>()
                .PreserveReferences();
            CreateMap<CharacteristicCreateRequestModel, CharacteristicDBModel>();
            CreateMap<CharacteristicRequestModel, CharacteristicDBModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CharacteristicGroupDBModel, CharacteristicGroupResponseModel>()
                .PreserveReferences();
            CreateMap<CharacteristicGroupRequestModel, CharacteristicGroupDBModel>();
            CreateMap<CharacteristicGroupCreateRequestModel, CharacteristicGroupDBModel>();

            CreateMap<CategoryCharacteristicGroupDBModel, CategoryCharacteristicGroupResponseModel>();
            CreateMap<CategoryCharacteristicGroupRequestModel, CategoryCharacteristicGroupDBModel>();
            CreateMap<CategoryCharacteristicGroupCreateRequestModel, CategoryCharacteristicGroupDBModel>();

            // CATEGORIES
            CreateMap<CategoryDBModel, CategoryResponseModel>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<CategoryImageUrlResolver>())
                .ForMember(dest => dest.IconUrl, opt => opt.MapFrom<CategoryIconUrlResolver>());

            CreateMap<CategoryCreateRequestModel, CategoryDBModel>();
            CreateMap<CategoryUpdateRequestModel, CategoryDBModel>();

            CreateMap<RelatedCategoryDBModel, RelatedCategoryResponseModel>();
            CreateMap<RelatedCategoryRequestModel, RelatedCategoryDBModel>();

            CreateMap<CategoryCharacteristicDBModel, CategoryCharacteristicResponseModel>()
                .ForMember(dest => dest.CharacteristicTitle, opt => opt.MapFrom(src => src.Characteristic.Title))
                .ForMember(dest => dest.CharacteristicDataType, opt => opt.MapFrom(src => src.Characteristic.DataType))
                .ForMember(dest => dest.CharacteristicUnit, opt => opt.MapFrom(src => src.Characteristic.Unit));
            CreateMap<CategoryCharacteristicRequestModel, CategoryCharacteristicDBModel>();


            // PRODUCTS
            CreateMap<BaseProductDBModel, BaseProductResponseModel>();
            CreateMap<BaseProductCreateRequestModel, BaseProductDBModel>();
            CreateMap<BaseProductUpdateRequestModel, BaseProductDBModel>();
            
            CreateMap<ProductDBModel, ProductResponseModel>();
            CreateMap<ProductCreateRequestModel, ProductDBModel>();
               //.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ProductUpdateRequestModel, ProductDBModel>();
               //.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // todo: find desision about nullable type
            CreateMap<ProductImageDBModel, ProductImageResponseModel>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<ProductImageUrlResolver>());

            CreateMap<FeedbackImageDBModel, FeedbackImageResponseModel>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<FeedbackImageUrlResolver>());

            CreateMap<ProductCharacteristicDBModel, ProductCharacteristicResponseModel>()
               .ForMember(dest => dest.CharacteristicTitle, opt => opt.MapFrom(src => src.Characteristic.Title))
               .ForMember(dest => dest.CharacteristicDataType, opt => opt.MapFrom(src => src.Characteristic.DataType))
               .ForMember(dest => dest.CharacteristicUnit, opt => opt.MapFrom(src => src.Characteristic.Unit));
            CreateMap<ProductCharacteristicUpdateRequestModel, ProductCharacteristicDBModel>();
            CreateMap<ProductCharacteristicCreateRequestModel, ProductCharacteristicDBModel>();

            CreateMap<ProductCharacteristicGroupResponseModel, SimplifiedProductCharacteristicGroupResponseModel>()
                .ForMember(dest => dest.CharacteristicGroupTitle, opt => opt.MapFrom(src => src.CharacteristicGroupTitle))
                .ForMember(dest => dest.ProductCharacteristics, opt => opt.MapFrom(src => src.ProductCharacteristics));

            CreateMap<ProductCharacteristicResponseModel, SimplifiedProductCharacteristicResponseModel>()
                .ForMember(dest => dest.CharacteristicTitle, opt => opt.MapFrom(src => src.CharacteristicTitle))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src =>
                    src.ValueText ??
                    (src.ValueNumber.HasValue ? src.ValueNumber.Value.ToString() :
                    (src.ValueBoolean.HasValue ? src.ValueBoolean.Value.ToString() :
                    (src.ValueDate.HasValue ? src.ValueDate.Value.ToString("o") : string.Empty)))));


            CreateMap<ProductVideoDBModel, ProductVideoResponseModel>();
            CreateMap<ProductVideoCreateRequestModel, ProductVideoDBModel>();
            CreateMap<ProductVideoUpdateRequestModel, ProductVideoDBModel>()
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<InstructionDBModel, InstructionResponseModel>();
            CreateMap<InstructionCreateRequestModel, InstructionDBModel>();
            CreateMap<InstructionUpdateRequestModel, InstructionDBModel>()
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<FeedbackDBModel, FeedbackResponseModel>()
                .ForMember(dest => dest.UserName,
                           opt => opt.MapFrom(src => src.User.NormalizedUserName))
                .ForMember(dest => dest.FeedbackImageUrls,
                           opt => opt.MapFrom<FeedbackImageUrlsResolver>());

            CreateMap<FeedbackCreateRequestModel, FeedbackDBModel>();
            CreateMap<FeedbackUpdateRequestModel, FeedbackDBModel>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.BaseProduct, opt => opt.Ignore())
                .ForMember(dest => dest.FeedbackImages, opt => opt.Ignore());

            CreateMap<ReviewDBModel, ReviewResponseModel>();
            CreateMap<ReviewCreateRequestModel, ReviewDBModel>();
            CreateMap<ReviewUpdateRequestModel, ReviewDBModel>();

            CreateMap<ProductSellerReferenceClickCreateRequestModel, ProductReferenceClickDBModel>();
            CreateMap<ProductSellerReferenceClickUpdateRequestModel, ProductReferenceClickDBModel>();
            CreateMap<ProductReferenceClickDBModel, ProductSellerReferenceClickResponseModel>();

            CreateMap<ColorCreateRequestModel, ColorDBModel>();
            CreateMap<ColorUpdateRequestModel, ColorDBModel>();
            CreateMap<ColorDBModel, ColorResponseModel>();

            CreateMap<ProductGroupCreateRequestModel, ProductGroupDBModel>();
            CreateMap<ProductGroupUpdateRequestModel, ProductGroupDBModel>();
            CreateMap<ProductGroupDBModel, ProductGroupModifiedResponseModel>();
            CreateMap<ProductGroupDBModel, ProductGroupResponseModel>();

            CreateMap<ProductGroupTypeCreateRequestModel, ProductGroupTypeDBModel>();
            CreateMap<ProductGroupTypeUpdateRequestModel, ProductGroupTypeDBModel>();
            CreateMap<ProductGroupTypeDBModel, ProductGroupTypeResponseModel>();


            // SELLER
            CreateMap<SellerDBModel, SellerResponseModel>()
                .ForMember(dest => dest.LogoImageUrl, opt => opt.MapFrom<SellerLogoImageUrlResolver>());
            CreateMap<SellerCreateRequestModel, SellerDBModel>();
            CreateMap<SellerUpdateRequestModel, SellerDBModel>();

            CreateMap<AuctionClickRateCreateRequestModel, AuctionClickRateDBModel>();
            CreateMap<AuctionClickRateUpdateRequestModel, AuctionClickRateDBModel>();
            CreateMap<AuctionClickRateDBModel, AuctionClickRateResponseModel>();

            CreateMap<SellerRequestCreateRequestModel, SellerRequestDBModel>();
            CreateMap<SellerRequestUpdateRequestModel, SellerRequestDBModel>();
            CreateMap<SellerRequestDBModel, SellerRequestResponseModel>();

            // FILTER
            CreateMap<FilterDBModel, FilterResponseModel>();
            CreateMap<FilterCreateRequestModel, FilterDBModel>();
            CreateMap<FilterUpdateRequestModel, FilterDBModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
