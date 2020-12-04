using Anderson.Produtos.Domain.Util;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Anderson.Produtos.Domain
{
    public sealed class ValidationResult : IValidationResult
    {
        public ICollection<string> Erros { get; } = new List<string>();

        public void AddError(string erro)
        {
            Erros.Add(erro);
        }
    }

    public static class Errors
    {
        private struct Result
        {
            public bool Success { get; set; }
            public string Error { get;  init; }
        }

        public static string ToFormattedString(this ICollection<string> erros)
        {
            return JsonSerializer.Serialize(new Result { Error = erros.FirstOrDefault() } );
        }
    }
}
