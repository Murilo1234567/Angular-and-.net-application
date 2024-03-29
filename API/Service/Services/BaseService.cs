using AutoMapper;
using FluentValidation;
using Domain.Entities;
using Domain.Interfaces;

namespace Service.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
        private readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public TInputModel Add<TInputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            Validate(entity, Activator.CreateInstance<TValidator>());
            _baseRepository.Insert(entity);

            return inputModel;
        }

        public void Delete(int id) => _baseRepository.Delete(id);

        public IEnumerable<TOutputModel> Get<TOutputModel>() where TOutputModel : class
        {
            var entities = _baseRepository.Select();

            var outputModels = entities.Select(s => _mapper.Map<TOutputModel>(s));

            return outputModels;
        }

        public TOutputModel GetById<TOutputModel>(int id) where TOutputModel : class
        {
            var entity = _baseRepository.Select(id);

            var outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public TInputModel Update<TInputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            Validate(entity, Activator.CreateInstance<TValidator>());
            _baseRepository.Update(entity);

            return inputModel;
        }

        private static void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }
    }
}