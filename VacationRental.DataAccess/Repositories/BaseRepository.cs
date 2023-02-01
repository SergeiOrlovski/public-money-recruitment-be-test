using System;
using System.Collections.Generic;
using System.Linq;

namespace VacationRental.DataAccess.Repositories;

public class BaseRepository<TValue> where TValue : class, new()
{
    private readonly IDictionary<int, TValue> _dataSource;

    protected BaseRepository(IDictionary<int, TValue> dataSource)
    {
        _dataSource = dataSource;
    }

    protected int GetCountValues() => _dataSource.Keys.Count;
    
    protected List<TValue> GetAllValues() => _dataSource.Values.ToList();
    
    protected TValue GetValue(int valueId)
    {
        var isValueExist = _dataSource.TryGetValue(valueId, out var value);

        return isValueExist ? value : null;
    }

    protected bool CreateValue(int valueId, TValue value)
    {
        try
        {
            _dataSource.Add(valueId, value);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    protected bool UpdateValue(int valueId, TValue value)
    {
        try
        {
            _dataSource[valueId] = value;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}