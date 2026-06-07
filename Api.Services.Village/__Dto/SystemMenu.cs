using System.Text.Json.Nodes;

namespace Api.Services;

public class SystemMenu
{
    public required string System { get; set; }
    public required string Group { get; set; }
    public List<TabPgm>? TabPgms { get; set; }
}

public class TabPgm
{
    public required string System { get; set; }
    public required string Component { get; set; }        
    public string? ComponentDesc { get; set; }        

}


// public class SpaComponent
// {
//     IList<SystemMenu> SystemMenus { get; set; }
//     string JsonNodes { get; set; }
// }
 