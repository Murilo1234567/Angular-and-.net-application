using FluentValidation;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
            TInputModel Add<TInputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class;

        void Delete(int id);

        IEnumerable<TOutputModel> Get<TOutputModel>() where TOutputModel : class;

        TOutputModel GetById<TOutputModel>(int id) where TOutputModel : class;

        TInputModel Update<TInputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class;
    }
}