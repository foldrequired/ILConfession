using System;
using Microsoft.AspNetCore.Http;

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