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

    public static string ToStringCollection<T>(this IEnumerable<T> source)
    {
        // use string builder here
        var g = source?.Aggregate(string.Empty, (result, next) =>
        {
            return result + next;
        });
        return $"[ {g} ]";
    }
}