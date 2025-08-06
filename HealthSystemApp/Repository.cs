using System;
using System.Collections.Generic;

public class Repository<T>
{
    private List<T> items = new();

    public void Add(T item) => items.Add(item);

    public List<T> GetAll() => new(items);

    public T? GetById(Func<T, bool> predicate) => items.Find(predicate);

    public bool Remove(Func<T, bool> predicate)
    {
        var item = items.Find(predicate);
        return item != null && items.Remove(item);
    }
}