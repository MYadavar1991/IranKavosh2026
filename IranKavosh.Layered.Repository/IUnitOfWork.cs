using IranKavosh.Layered.Repository.Models;

namespace IranKavosh.Layered.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        //Start_1
        ILoginRepository LoginRepository { get; }
        IBFactorDetailRepository BFactorDetailRepository { get; }
        IBFactorHeaderRepository BFactorHeaderRepository { get; }
        IChanelMemberRepository ChanelMemberRepository { get; }
        IBuySellDetailRepository BuySellDetailRepository { get; }
        IPriceListDiscountRepository PriceListDiscountRepository { get; }
        IErrorExcepRepository ErrorExcepRepository { get; }
        IPriceListRepository PriceListRepository { get; }
        IFundRepository FundRepository { get; }
        IFundTypeRepository FundTypeRepository { get; }
        ICopiedFilmToMemoryRepository CopiedFilmToMemoryRepository { get; }

        IHddMemoryRepository HddMemoryRepository { get; }
        IHddMemoryPersonRepository HddMemoryPersonRepository { get; }



        IAccessListRepository AccessListRepository { get; }
        IAccessTypeRepository AccessTypeRepository { get; }
        IMultiChapterTypeRepository MultiChapterTypeRepository { get; }
        IMultiActorRepository MultiActorRepository { get; }
        IUnitRepository UnitRepository { get; }
        IShareHolderHeaderRepository ShareHolderHeaderRepository { get; }
        IShareHolderDetailRepository ShareHolderDetailRepository { get; }
        ISellDetailRepository SellDetailRepository { get; }
        //IProductSaleTypeRepository ProductSaleTypeRepository { get; }
        IProductRepository ProductRepository { get; }
        IPersonalGroupRepository PersonalGroupRepository { get; }
        IPersonalRepository PersonalRepository { get; }
        IMultiSellTypeRepository MultiSellTypeRepository { get; }
        IMultiPriceRepository MultiPriceRepository { get; }
        IMultimediaHeaderChapterRepository MultimediaHeaderChapterRepository { get; }
        IMultimediaHeaderRepository MultimediaHeaderRepository { get; }
        IMultimediaChapterDetailRepository MultimediaChapterDetailRepository { get; }
        IMultiLocationRepository MultiLocationRepository { get; }
        IMultiListPersonalRepository MultiListPersonalRepository { get; }
        IMultiGroupRepository MultiGroupRepository { get; }
        //IMultiGenreListRepository MultiGenreListRepository { get; }
        IMultiGenreRepository MultiGenreRepository { get; }
        IMultiCountryRepository MultiCountryRepository { get; }
        IMultiAgeRepository MultiAgeRepository { get; }
        IDefinitiveAccountRepository DefinitiveAccountRepository { get; }
        IBuySellHeaderRepository BuySellHeaderRepository { get; }

        int Save();
    }
}





















