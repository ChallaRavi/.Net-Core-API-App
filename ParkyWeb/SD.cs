using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb
{
    public static class SD
    {
        public static string APIBaseURL = "https://localhost:44305/";
        public static string NationalParkAPIPath = APIBaseURL+ "api/v1/nationalparks";
        public static string TrailAPIPath = APIBaseURL+ "api/v1/trails";
    }
}
