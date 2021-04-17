using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace AspnetCoreSession8.Endpoints.WebUi
{
    public class NationalCodeConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route,string routeKey, RouteValueDictionary values,RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out var routeValue))
            {
                var parameterValueString = Convert.ToString(routeValue,CultureInfo.InvariantCulture);
                return IsNationalCodeValid(parameterValueString);
            }
            return false;
        }

        private bool IsNationalCodeValid(string input)
        {
            if (!Regex.IsMatch(input, @"^\d{10}$"))
                return false;

            var check = Convert.ToInt32(input.Substring(9, 1));
            var sum = Enumerable.Range(0, 9)
                .Select(x => Convert.ToInt32(input.Substring(x, 1)) * (10 - x))
                .Sum() % 11;

            return (sum < 2 && check == sum) || (sum >= 2 && check + sum == 11);
        }
    }
}