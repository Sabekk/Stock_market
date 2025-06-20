using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CollectionExtensions
{
    public static IIdEqualable GetRandomExcludedIds(this IIdEqualable[] collection, List<int> excludedIds)
    {
        if (collection == null || collection.Length == 0)
            return null;

        HashSet<int> excludedSet = new HashSet<int>(excludedIds);

        List<IIdEqualable> availableGroups = collection
            .Where(g => !excludedSet.Contains(g.Id))
            .ToList();

        if (availableGroups.Count == 0)
            return null;

        return availableGroups[Random.Range(0, availableGroups.Count)];
    }

    public static IIdEqualable GetRandomExcludedIds(this List<IIdEqualable> collection, List<int> excludedIds)
    {
        if (collection == null || collection.Count == 0)
            return null;

        HashSet<int> excludedSet = new HashSet<int>(excludedIds);

        List<IIdEqualable> availableGroups = collection
            .Where(g => !excludedSet.Contains(g.Id))
            .ToList();

        if (availableGroups.Count == 0)
            return null;

        return availableGroups[Random.Range(0, availableGroups.Count)];
    }
}
