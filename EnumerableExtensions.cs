namespace Helpers;

public static class EnumerableExtensions
{
	/// <summary>Check if a collection is not null and is not empty</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns>bool</returns>
	public static bool HasData<T>(this IEnumerable<T>? list)
	{
		return list != null && list.Any();
	}

	public static int IndexOf<T>(this IEnumerable<T> obj, T value)
		{
			return obj.IndexOf(value, null);
		}

    private static int IndexOf<T>(this IEnumerable<T> obj, T value, IEqualityComparer<T> comparer)
	{
		comparer ??= EqualityComparer<T>.Default;
		var found = obj
			.Select((a, i) => new { a, i })
			.FirstOrDefault(x => comparer.Equals(x.a, value));
		return found == null ? -1 : found.i;
	}

	public static IEnumerable<T> FindSandwichedItem<T>(this IEnumerable<T> items, Predicate<T> matchFilling)
	{
		if (items == null)
			throw new ArgumentNullException("items");
		if (matchFilling == null)
			throw new ArgumentNullException("matchFilling");

		return FindSandwichedItemImpl(items, matchFilling);
	}

	private static IEnumerable<T> FindSandwichedItemImpl<T>(IEnumerable<T> items, Predicate<T> matchFilling)
	{
		using (var iter = items.GetEnumerator())
		{
			T previous = default;
			while (iter.MoveNext())
			{
				if (matchFilling(iter.Current))
				{
					yield return previous;
					yield return iter.Current;
					if (iter.MoveNext())
						yield return iter.Current;
					else
						yield return default(T);
					yield break;
				}
				previous = iter.Current;
			}
		}
		// If we get here nothing has been found so return three default values
		yield return default(T); // Previous
		yield return default(T); // Current
		yield return default(T); // Next
	}
}