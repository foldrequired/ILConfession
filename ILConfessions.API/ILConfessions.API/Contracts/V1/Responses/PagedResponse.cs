using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ILConfessions.API.Contracts.V1.Responses
{
    public class PagedResponse<T> : List<T>
    {
        public PagedResponse()
        {

        }

        public PagedResponse(IEnumerable<T> data, int count, int pageNumber, int pageSize)
        {
            Data = data;
            TotalCount = count;
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(data);
        }

        public static async Task<PagedResponse<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedResponse<T>(items, count, pageNumber, pageSize);
        }

        public IEnumerable<T> Data { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string NextPage { get; set; }

        public string PreviousPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

    }
}
