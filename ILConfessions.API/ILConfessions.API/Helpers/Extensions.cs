using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ILConfessions.API.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse res, string msg)
        {
            res.Headers.Add("Application-Error", msg);
            res.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            res.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static void AddPagination(this HttpResponse res, int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var camelCase = new JsonSerializerSettings();
            camelCase.ContractResolver = new CamelCasePropertyNamesContractResolver();
            res.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCase));
            res.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
        }

        public static int CalculateAge(this DateTime theDateTime)
        {
            var age = DateTime.Today.Year - theDateTime.Year;

            if (theDateTime.AddYears(age) > DateTime.Today)
            {
                age--;
            }

            return age;
        }
    }
}