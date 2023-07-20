namespace SharedLibraries;

public class PagingOptions
{

    public int PageSize { get; set; }

    public int Page { get; set; }

    public PagingLoadStrategy LoadStrategy { get; set; }

    public bool CalculateTotals { get; set; }

}
