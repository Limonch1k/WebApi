using AutoMapper;
using DatabaseLayer.IDbContext;
using DatabaseLayer.ModelInterface;
using DatabaseLayer.ParamModel;
using DB.DBModels;
using DB.IRepository;
using DB.TableModels;
using GeneralObject.DynamicGenerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Static.Repository_Old;
using System.Linq.Dynamic.Core;

namespace DB.Repository
{
    public class UniversalMeteoRepository<ModelDB,TableModel> : IResorceRepositoryAsync<ModelDB> where ModelDB : class where TableModel : class, ISynopProperty
    {
        private DbSet<TableModel> _query_entityframework_pieceOFshit {get;set;}

        private DbContext _context { get; set; }
        
        private IMapper _mapper;
        public UniversalMeteoRepository(IDbContext<DbSet<TableModel>> context, IMapper mapper)
        {

            _mapper = mapper;
            _query_entityframework_pieceOFshit = context.SetTable();
            _context = GetContext(_query_entityframework_pieceOFshit);
        }

        public Task<ModelDB> GetByIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ModelDB>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        

        public async Task<List<ModelDB>> ResourceIdStartDateEndDateOrderbyFilter(MeteoParamModel_DL paramModel)
        {
            var list = await Task<IQueryable<TableModel>>.Run( () => 
            {
                if (paramModel.orderbyList is null)
                {
                    return _query_entityframework_pieceOFshit
                    .ByDateFilter((DateTime)paramModel.start_dt, (DateTime)paramModel.end_dt)
                    .ToList().AsQueryable()
                    .FieldListInFilter(paramModel.stationList);
                }
                else
                {
                    return _query_entityframework_pieceOFshit

                    .ByDateFilter((DateTime)paramModel.start_dt, (DateTime)paramModel.end_dt)
                    .OrderBy(paramModel.orderbyList)
                    .ToList().AsQueryable()
                    .FieldListInFilter(paramModel.stationList);
                }
                
            });

            return _mapper.Map<List<ModelDB>>(list);
        }

        public async Task<List<ModelDB>> ResourceIdParamStartDateEndDateOrderbyFilter(MeteoParamModel_DL paramModel)
        {

            var b = SelectGenerator.DynamicSelectGenerator<GroundDatum>();
            var list = await Task<IQueryable<TableModel>>.Run( () => 
            {
                if (paramModel.orderbyList is not null)
                {
                    var test =
                    (_query_entityframework_pieceOFshit as IQueryable<TableModel>)
                    .ByDateFilter((DateTime)paramModel.start_dt, (DateTime)paramModel.end_dt)
                    .ModelIdFilter(paramModel.model_id)
                    .FieldListInFilter(paramModel.stationList)
                    .SelectParamField(paramModel.paramList)
                    .OrderBy(paramModel.orderbyList)
                    .Take(10000)
                    .ToDynamicList();
                    return test;
                }
                else
                {
                    var test =
                    (_query_entityframework_pieceOFshit as IQueryable<TableModel>)
                    .ByDateFilter((DateTime)paramModel.start_dt, (DateTime)paramModel.end_dt)
                    .ModelIdFilter(paramModel.model_id)
                    .FieldListInFilter(paramModel.stationList)
                    .SelectParamField(paramModel.paramList)
                    .Take(10000)
                    .ToDynamicList();
                    return test;
                }             
            });

            return _mapper.Map<List<ModelDB>>(list);
        }

        public async Task<List<ModelDB>> GetLastOrderbyFilter(MeteoParamModel_DL paramModel)
        {
            var list = await Task<IQueryable<TableModel>>.Run( () => 
            {
                if (paramModel.orderbyList is null)
                {
                    return _query_entityframework_pieceOFshit
                    .GetLastFilter()
                    .ToList();
                }
                else
                {
                    return _query_entityframework_pieceOFshit
                    .GetLastFilter()
                    .OrderBy(paramModel.orderbyList)
                    .ToList();
                }
                
            });

            return _mapper.Map<List<ModelDB>>(list);
        }

        public async Task<List<ModelDB>> GetLastResourceIdOrderbyFilter(MeteoParamModel_DL paramModel)
        {
            var list = await Task<IQueryable<TableModel>>.Run( () => 
            {
                if (paramModel.orderbyList is null)
                {
                    return _query_entityframework_pieceOFshit
                    .GetLastFilter()
                    .ToList().AsQueryable()
                    .FieldListInFilter(paramModel.stationList);
                }
                else
                {
                    return _query_entityframework_pieceOFshit
                    .GetLastFilter()
                    .OrderBy(paramModel.orderbyList)
                    .ToList().AsQueryable()
                    .FieldListInFilter(paramModel.stationList);
                    
                }
                
            });

            return _mapper.Map<List<ModelDB>>(list);
        }

        public async Task<List<ModelDB>> GetLastParamOrderbyFilter(MeteoParamModel_DL paramModel)
        {
            var list = await Task<IQueryable<TableModel>>.Run( () => 
            {
                if (paramModel.orderbyList is null)
                {
                    return _query_entityframework_pieceOFshit
                    .SelectParamField(paramModel.stationList)
                    .GetLastFilter()
                    .ToList();
                }
                else
                {
                    return _query_entityframework_pieceOFshit
                    .SelectParamField(paramModel.stationList)
                    .GetLastFilter()
                    .OrderBy(paramModel.orderbyList)
                    .ToList();
                }
                
            });

            return _mapper.Map<List<ModelDB>>(list);
        }

        public async Task<List<ModelDB>> GetLastResourceIdParamOrderbyFilter(MeteoParamModel_DL paramModel)
        {
            var list = await Task<IQueryable<TableModel>>.Run( () => 
            {
                if (paramModel.orderbyList is null)
                {
                    return _query_entityframework_pieceOFshit
                    
                    .SelectParamField(paramModel.paramList)
                    .GetLastFilter()
                    .ToList().AsQueryable()
                    .FieldListInFilter(paramModel.stationList);
                }
                else
                {
                    return _query_entityframework_pieceOFshit
                    
                    .SelectParamField(paramModel.paramList)
                    .GetLastFilter()
                    .OrderBy(paramModel.orderbyList)
                    .ToList().AsQueryable()
                    .FieldListInFilter(paramModel.stationList);
                }
                
            });

            return _mapper.Map<List<ModelDB>>(list);
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async void DisposeAsync()
        {
            _context.DisposeAsync();
            return;
        }

        public Task<List<ModelDB>> ResourceIdParamDateOrderbyFilter(MeteoParamModel_DL paramModel)
        {
            throw new NotImplementedException();
        }

        private DbContext GetContext<TEntity>(DbSet<TEntity> dbSet) where TEntity : class
        {
            var dbcontext = dbSet.GetService<ICurrentDbContext>().Context;
            return dbcontext;
        }
    }
}