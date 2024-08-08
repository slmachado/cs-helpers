using System;
using System.Collections.Generic;
using System.Linq;

namespace Helpers
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Checks if a collection is not null and is not empty.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="list">The collection to check.</param>
        /// <returns>True if the collection is not null and not empty; otherwise, false.</returns>
        public static bool HasData<T>(this IEnumerable<T>? list)
        {
            return list != null && list.Any();
        }

        /// <summary>
        /// Finds the index of a value in a collection.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="obj">The collection to search.</param>
        /// <param name="value">The value to find.</param>
        /// <returns>The index of the value if found; otherwise, -1.</returns>
        public static int IndexOf<T>(this IEnumerable<T> obj, T value)
        {
            return obj.IndexOf(value, null);
        }

        /// <summary>
        /// Finds the index of a value in a collection using a specified comparer.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="obj">The collection to search.</param>
        /// <param name="value">The value to find.</param>
        /// <param name="comparer">The comparer to use.</param>
        /// <returns>The index of the value if found; otherwise, -1.</returns>
        private static int IndexOf<T>(this IEnumerable<T> obj, T value, IEqualityComparer<T> comparer)
        {
            comparer ??= EqualityComparer<T>.Default;
            var found = obj
                .Select((a, i) => new { a, i })
                .FirstOrDefault(x => comparer.Equals(x.a, value));
            return found == null ? -1 : found.i;
        }

        /// <summary>
        /// Finds the item in the collection that is sandwiched between two other items.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="items">The collection to search.</param>
        /// <param name="matchFilling">The predicate to match the filling item.</param>
        /// <returns>The previous, current, and next items if found; otherwise, three default values.</returns>
        public static IEnumerable<T> FindSandwichedItem<T>(this IEnumerable<T> items, Predicate<T> matchFilling)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (matchFilling == null)
                throw new ArgumentNullException(nameof(matchFilling));

            return FindSandwichedItemImpl(items, matchFilling);
        }

        private static IEnumerable<T> FindSandwichedItemImpl<T>(IEnumerable<T> items, Predicate<T> matchFilling)
        {
            using var iter = items.GetEnumerator();
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
            // If we get here nothing has been found so return three default values
            yield return default(T); // Previous
            yield return default(T); // Current
            yield return default(T); // Next
        }

        /// <summary>
        /// Checks if the collection is empty.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The collection to check.</param>
        /// <returns>True if the collection is empty; otherwise, false.</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }

        /// <summary>
        /// Performs the specified action on each element of the collection.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The collection to iterate over.</param>
        /// <param name="action">The action to perform on each element.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// Converts the collection to a comma-separated string.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The collection to convert.</param>
        /// <returns>A comma-separated string of the collection elements.</returns>
        public static string ToCommaSeparatedString<T>(this IEnumerable<T> source)
        {
            return string.Join(", ", source);
        }

        /// <summary>
        /// Splits the collection into chunks of the specified size.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The collection to split.</param>
        /// <param name="size">The size of each chunk.</param>
        /// <returns>An IEnumerable of chunks, each containing up to the specified number of elements.</returns>
        public static IEnumerable<IEnumerable<T>> ChunkBy<T>(this IEnumerable<T> source, int size)
        {
            if (size <= 0)
                throw new ArgumentException("Chunk size must be greater than zero.", nameof(size));

            return ChunkByImpl(source, size);
        }

        private static IEnumerable<IEnumerable<T>> ChunkByImpl<T>(IEnumerable<T> source, int size)
        {
            using var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                yield return YieldChunkElements(enumerator, size - 1).ToList();
            }
        }

        private static IEnumerable<T> YieldChunkElements<T>(IEnumerator<T> source, int size)
        {
            yield return source.Current;
            for (var i = 0; i < size && source.MoveNext(); i++)
            {
                yield return source.Current;
            }
        }

        /// <summary>
        /// Returns distinct elements from a sequence by using a specified selector.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by the key selector.</typeparam>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <returns>An IEnumerable that contains distinct elements from the source sequence.</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Shuffles the elements of the collection randomly.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The collection to shuffle.</param>
        /// <returns>A new collection with the elements shuffled randomly.</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            var rng = new Random();
            return source.OrderBy(_ => rng.Next());
        }

        /// <summary>
        /// Partitions a collection into two collections based on a predicate.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The collection to partition.</param>
        /// <param name="predicate">The predicate to determine the partition.</param>
        /// <returns>A tuple containing two collections: the first containing elements that match the predicate, and the second containing elements that do not match.</returns>
        public static (IEnumerable<T> Matches, IEnumerable<T> NonMatches) Partition<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            var matches = new List<T>();
            var nonMatches = new List<T>();

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    matches.Add(item);
                }
                else
                {
                    nonMatches.Add(item);
                }
            }

            return (matches, nonMatches);
        }
    }
}
