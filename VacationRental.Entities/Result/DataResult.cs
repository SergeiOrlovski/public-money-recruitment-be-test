namespace VacationRental.Entities.Result;

public class DataResult<T> : Result
{
    private DataResult() { }

    private DataResult(string error) => Error = error;

    public T Data { get; private set; }

    public static DataResult<T> Success(T data) => new DataResult<T> { Data = data };

    public new static DataResult<T> Fail(string error) => new DataResult<T>(error);
}