namespace HamkareBlazor;

public class ApiResponse<TResponse>
{
    public TResponse? Value { get; set; }

    public string? Message { get; set; }

    public Severity Type { get; set; }
}
