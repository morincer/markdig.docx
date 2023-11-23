namespace Markdig.Renderers.Docx;

internal static class NetStandard20Polyfills
{
    public static TValue GetValueOrDefault<TKey, TValue>(
        this IReadOnlyDictionary<TKey, TValue> target,
        TKey key,
        TValue defaultValue = default!)
    {
        if (target.TryGetValue(key, out var result))
        {
            return result!;
        }

        return defaultValue;
    }

    public static bool TryPeek<T>(this Stack<T> stack, out T result)
    {
        if (stack.Count == 0)
        {
            result = default!;
            return false;
        }

        result = stack.Peek();
        return true;
    }

    public static string[] Split(this string str, string separator, StringSplitOptions options = StringSplitOptions.None)
    {
        return str.Split(separator.ToCharArray(), options);
    }
}
