namespace VacationRental.Entities.Result;

public class Result
{
    private protected Result() { }

    public string Error { get; private protected set; }

    public bool IsError => !string.IsNullOrEmpty(Error);

    public static Result Success() => new Result();

    public static Result Fail(string error) => new Result { Error = error };
}