using System.Collections;

public static class Extensions
{
    public static void PrintLine<T>(this T content)
    {
        Console.WriteLine(content);
    }

    public static void PrintCollection<T>(this T content) where T : IEnumerable
    {
        foreach (var item in content)
        {
            Console.WriteLine(item);
        }
    }
}