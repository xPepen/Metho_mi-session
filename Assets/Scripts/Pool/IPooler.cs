using System;

public interface IPooler<T>
{
   public Action<T> RePoolItem { get; set; }
}
