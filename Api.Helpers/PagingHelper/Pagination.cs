namespace Api.Helpers;

public class Pagination
{
    public int TotalItems { get; set; } = 0;
    public int PageIndex { get; set; } = 1;
    private int _pageSize = 10;
    public int PageSize
    {
        get { return _pageSize; }
        set
        {
            if (value > 200)
            {
                _pageSize = 200; 
            }
            else if (value < 5)
            {
                _pageSize = 5;
            }
            else
            {
                _pageSize = value;
            }
        }
    }
    public int TotalPages
    {
        get
        {
            return (int)System.Math.Ceiling(this.TotalItems / (double)this.PageSize);
        }
    }

    // public System.Collections.Generic.List<OrderByHelper> OrderByList {get; set;}
    public string? OrderBy { get; set; }
    public bool IsAscending { get; set; }
    public string? ThenBy { get; set; }
    public bool IsThenAscending { get; set; }
    public string? ThreeBy { get; set; }
    public bool IsThreeAscending { get; set; }

}