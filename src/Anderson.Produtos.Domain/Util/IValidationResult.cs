namespace Anderson.Produtos.Domain.Util
{
    using System.Collections.Generic;

    /// <summary>
    /// is Scoped
    /// </summary>
    public interface IValidationResult
    {
        ICollection<string> Errors { get; }
        void AddError(string erro);
    }
}
