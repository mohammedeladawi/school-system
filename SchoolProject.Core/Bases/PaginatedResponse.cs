namespace SchoolProject.Core.Bases;

public class PaginatedResponse<T>
{
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    public int TotalRecords { get; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
    public IEnumerable<T> Data { get; }
    public IEnumerable<string> Messages { get; } = new List<string>();
    public bool Success { get; } = true;
    public object Meta { get; }

    public PaginatedResponse(IEnumerable<T> data, int pageNumber, int pageSize, int totalRecords)
    {
        Data = data;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
    }

    public PaginatedResponse(IEnumerable<T> data, int pageNumber, int pageSize, int totalRecords, IEnumerable<string> messages, bool success = true, object meta = null)
        : this(data, pageNumber, pageSize, totalRecords)
    {
        Messages = messages;
        Success = success;
        Meta = meta;
    }

    public static PaginatedResponse<T> SuccessResult(IEnumerable<T> data, int pageNumber, int pageSize, int totalRecords, IEnumerable<string> messages = null, object meta = null)
    {
        return new PaginatedResponse<T>(data, pageNumber, pageSize, totalRecords, messages ?? new List<string>(), true, meta);
    }
}