using System.Collections.Generic;
using Api.Helpers;

namespace Api.Services;

public class ApiResult
{
        // public int Code { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; }
        public object? Data { get; set; }
}