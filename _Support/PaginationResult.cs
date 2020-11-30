using System.Collections.Generic;

namespace _Support
{
    public struct PaginationResult<T>
    {
        public int TotalItens { get; set; }
        public List<T> Result { get; set; }

        public PaginationResult(List<T> result, int totalItens)
        {
            Result = result;
            TotalItens = totalItens;
        }
    }
}