
using System;

public interface ITranslateBehaviour
{
   public void SetNewTextReader();
   public string GetValueFromDictionary(ReadOnlySpan<char> key);
   public void UpdateText();
}
