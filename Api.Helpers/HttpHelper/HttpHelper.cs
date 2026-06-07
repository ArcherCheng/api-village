using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Api.Helpers;
 
public static class HttpHelper
{
    public static void AddApplicationError(this HttpResponse response, string message)
    {
        //response.Headers.Add("Application-Error", System.Net.WebUtility.UrlEncode(message));
        response.Headers.Append("Application-Error", message);
        response.Headers.Append("Access-Control-Expose-Headers", "Application-Error");
        response.Headers.Append("Access-Control-Allow-Origin", "*");
    }

    public static void AddPagination(this HttpResponse response,
        int currentPage, int itemsPerPage, int totalItems, int totalPages)
    {
        var paginationHeader = new Pagination();
        paginationHeader.PageIndex = currentPage;
        paginationHeader.PageSize = itemsPerPage;
        paginationHeader.TotalItems = totalItems;
        // paginationHeader.TotalPages = totalPages;

        var camelCaseFormatter = new JsonSerializerSettings();
        camelCaseFormatter.ContractResolver =
            new CamelCasePropertyNamesContractResolver();
        response.Headers.Append("Pagination",
            JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
        response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
    }
}