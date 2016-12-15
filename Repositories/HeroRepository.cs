using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class HeroRepository : BaseRepository<Models.Hero, ViewModels.Hero>
    {
        public HeroRepository(Models.HeroContext context, IMapper mapper): base(context, mapper)
        {
        }
    }

    public abstract class BaseRepository<TModel, TViewModel> where TModel: Models.Entity
    {
        protected readonly DbSet<TModel> _set;
        protected readonly IMapper _mapper;
        protected readonly Models.HeroContext _context;

        protected BaseRepository(Models.HeroContext context, IMapper mapper)
        {
            _context = context;
            _set = _context.Set<TModel>();
            _mapper = mapper;
        }

        public List<TViewModel> Get()
        {
            return _set
                .ProjectTo<TViewModel>()
                .ToList();
        }

        public TViewModel Get(int id)
        {
            return _set
                .Where(h => h.Id == id)
                .ProjectTo<TViewModel>()
                .FirstOrDefault();
        }
        
        public void Delete(int id)
        {
            var model = _set.First(h => h.Id == id);
            _set.Remove(model);
            _context.SaveChanges();
           
        }

        public TViewModel Add(TViewModel viewModel)
        {
            TModel model = _mapper.Map<TModel>(viewModel);

            _set.Add(model);
            _context.SaveChanges();

            return _mapper.Map<TViewModel>(model);
        }

        public TViewModel Update(int id, TViewModel viewModel)
        {
            TModel model = _set.First(h => h.Id == id);

            _mapper.Map<TViewModel, TModel>(viewModel, model);

            _set.Update(model);
            _context.SaveChanges();

            return viewModel;
        } 
    }
}