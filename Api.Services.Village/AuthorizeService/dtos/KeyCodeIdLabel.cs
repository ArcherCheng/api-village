
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Services;

public class KeyCodeIdLabel(string CodeId, string CodeLabel)
{
    public string CodeId { get; set; } = CodeId;
    public string CodeLabel { get; set; } = CodeLabel;
} 