using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProEventos.Repository.Models
{
    public class PageList<T>
    {
         public int CurrentPage { get; set; }
         public int TotalPages { get; set; }
         public int PageSize { get; set; }
         public int TotalCount { get; set; }
         public List<T> Items { get; set; }
         
         public PageList(List<T> items, int count, int pageNumber, int pageSize)
         {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
         }

         public static async Task<PageList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
         {
             var count = await source.CountAsync();
             var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
             return new PageList<T>(items, count, pageNumber, pageSize);
         }
    }
}