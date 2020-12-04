using Anderson.Produtos.Domain.Util;

namespace Anderson.Produtos.Domain.Model
{
    public interface IBaseModel
    {
        public long Id { get; init; }

        //SelfValidate
        public abstract bool IsValid(IValidationResult validationResult);
    }
}
