using System.Collections.Generic;

namespace SharedLibraries;

public class PagingResult<TIEntity>
{
    public List<TIEntity> Items { get; set; }

    public int TotalItems { get; set; }

    public int TotalPages { get; set; }

    public int Page { get; set; }

    public PagingLoadStrategy LoadStrategy { get; set; }

    public bool IsFinishedLoading { get; set; }

    public PagingResult() {
        Items = new List<TIEntity>();
    }
}
