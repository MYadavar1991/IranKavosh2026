
using IranKavosh.Layered.Model;
using IranKavosh.Layered.Repository.Models;
using Microsoft.EntityFrameworkCore;


namespace IranKavosh.Layered.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        //Start_1
        private dbCore context;
        public UnitOfWork(dbCore context)
        {
            this.context = context;


            //Start_2


            LoginRepository = new LoginRepository(context);
            BFactorHeaderRepository = new BFactorHeaderRepository(context);
            BFactorDetailRepository = new BFactorDetailRepository(context);

            ChanelMemberRepository = new ChanelMemberRepository(context);
            BuySellDetailRepository = new BuySellDetailRepository(context);
            PriceListDiscountRepository = new PriceListDiscountRepository(context);
            ErrorExcepRepository = new ErrorExcepRepository(context);
            PriceListRepository = new PriceListRepository(context);
            AccessListRepository = new AccessListRepository(context);
            FundRepository = new FundRepository(context);

            FundTypeRepository = new FundTypeRepository(context);
            CopiedFilmToMemoryRepository = new CopiedFilmToMemoryRepository(context);
            HddMemoryRepository = new HddMemoryRepository(context);
            HddMemoryPersonRepository = new HddMemoryPersonRepository(context);

            AccessTypeRepository = new AccessTypeRepository(context);
            MultiChapterTypeRepository = new MultiChapterTypeRepository(context);
            MultiActorRepository = new MultiActorRepository(context);
            UnitRepository = new UnitRepository(context);
            ShareHolderHeaderRepository = new ShareHolderHeaderRepository(context);
            ShareHolderDetailRepository = new ShareHolderDetailRepository(context);
            SellDetailRepository = new SellDetailRepository(context);
            //ProductSaleTypeRepository = new ProductSaleTypeRepository(context);
            ProductRepository = new ProductRepository(context);
            PersonalGroupRepository = new PersonalGroupRepository(context);
            PersonalRepository = new PersonalRepository(context);
            MultiSellTypeRepository = new MultiSellTypeRepository(context);
            MultiPriceRepository = new MultiPriceRepository(context);
            MultimediaHeaderChapterRepository = new MultimediaHeaderChapterRepository(context);
            MultimediaHeaderRepository = new MultimediaHeaderRepository(context);
            MultimediaChapterDetailRepository = new MultimediaChapterDetailRepository(context);
            MultiLocationRepository = new MultiLocationRepository(context);
            MultiListPersonalRepository = new MultiListPersonalRepository(context);
            MultiGroupRepository = new MultiGroupRepository(context);
            //MultiGenreListRepository = new MultiGenreListRepository(context);
            MultiGenreRepository = new MultiGenreRepository(context);
            MultiCountryRepository = new MultiCountryRepository(context);
            MultiAgeRepository = new MultiAgeRepository(context);
            DefinitiveAccountRepository = new DefinitiveAccountRepository(context);
            BuySellHeaderRepository = new BuySellHeaderRepository(context);

        }


        //public UnitOfWork()
        //{
        //    this.context.Database.SetConnectionString("Data Source=192.168.4.100\\nlina;Initial Catalog=BaseLina;Integrated Security=false;User ID=sa;Password=Pars@63");
        //    BaseUserRepository = new BaseUserRepository(context);
        //}

        //Start_3
        public ILoginRepository LoginRepository { get; }
        public IBFactorHeaderRepository BFactorHeaderRepository { get; }
        public IBFactorDetailRepository BFactorDetailRepository { get; }
        public IChanelMemberRepository ChanelMemberRepository { get; }

        public IBuySellDetailRepository BuySellDetailRepository { get; }
        public IPriceListDiscountRepository PriceListDiscountRepository { get; }
        public IErrorExcepRepository ErrorExcepRepository { get; }
        public IPriceListRepository PriceListRepository { get; }
        public IFundRepository FundRepository { get; }
        public IFundTypeRepository FundTypeRepository { get; }
        public ICopiedFilmToMemoryRepository CopiedFilmToMemoryRepository { get; }
        public IHddMemoryRepository HddMemoryRepository { get; }
        public IHddMemoryPersonRepository HddMemoryPersonRepository { get; }

        public IAccessListRepository AccessListRepository { get; }
        public IAccessTypeRepository AccessTypeRepository { get; }
        public IMultiChapterTypeRepository MultiChapterTypeRepository { get; private set; }
        public IMultiActorRepository MultiActorRepository { get; private set; }
        public IUnitRepository UnitRepository { get; private set; }
        public IShareHolderHeaderRepository ShareHolderHeaderRepository { get; private set; }
        public IShareHolderDetailRepository ShareHolderDetailRepository { get; private set; }
        public ISellDetailRepository SellDetailRepository { get; private set; }
        //public IProductSaleTypeRepository ProductSaleTypeRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public IPersonalGroupRepository PersonalGroupRepository { get; private set; }
        public IPersonalRepository PersonalRepository { get; private set; }
        public IMultiSellTypeRepository MultiSellTypeRepository { get; private set; }
        public IMultiPriceRepository MultiPriceRepository { get; private set; }
        public IMultimediaHeaderChapterRepository MultimediaHeaderChapterRepository { get; private set; }
        public IMultimediaHeaderRepository MultimediaHeaderRepository { get; private set; }
        public IMultimediaChapterDetailRepository MultimediaChapterDetailRepository { get; private set; }
        public IMultiLocationRepository MultiLocationRepository { get; private set; }
        public IMultiListPersonalRepository MultiListPersonalRepository { get; private set; }
        public IMultiGroupRepository MultiGroupRepository { get; private set; }
        //public IMultiGenreListRepository MultiGenreListRepository { get; private set; }
        public IMultiGenreRepository MultiGenreRepository { get; private set; }
        public IMultiCountryRepository MultiCountryRepository { get; private set; }
        public IMultiAgeRepository MultiAgeRepository { get; private set; }
        public IDefinitiveAccountRepository DefinitiveAccountRepository { get; private set; }
        public IBuySellHeaderRepository BuySellHeaderRepository { get; private set; }



        //Start_4
        public void Dispose()
        {
            context.Dispose();
        }
        public int Save()
        {
            int a = 0;
            try
            {
                //context.Database.sta
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        a = context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException dbEx)
                    {
                        var getErr = dbEx.InnerException.Message;
                        string query = $@"INSERT INTO LogE.ErrorExcep
(
  Id
 ,Type
 ,Message
 ,DateTime
)
VALUES
(
  newid()
 ,N'DbUpdateConcurrencyException'
 ,N'{getErr.Replace("'", "''")}'
 ,GETDATE()
)";
                        context.Database.ExecuteSqlRaw(query);
                       
                    }
                    catch (DbUpdateException dbEx)
                    {
                        var getErr = dbEx.InnerException.Message;
                        string query = $@"INSERT INTO LogE.ErrorExcep
(
  Id
 ,Type
 ,Message
 ,DateTime
)
VALUES
(
  newid()
 ,N'DbUpdateException'
 ,N'{getErr.Replace("'", "''")}'
 ,GETDATE()
)";
                        context.Database.ExecuteSqlRaw(query);
                    }
                    catch (Exception ex)
                    {
                        var getErr = ex.InnerException.Message;
                        string query = $@"INSERT INTO LogE.ErrorExcep
(
  Id
 ,Type
 ,Message
 ,DateTime
)
VALUES
(
  newid()
 ,N'Exception'
 ,N'{getErr.Replace("'", "''")}'
 ,GETDATE()
)";
                    }
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {

            }

            return a;
        }
    }
}





















