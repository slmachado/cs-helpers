namespace Helpers;

public static class TreeHelper
{
    /// <summary>
    /// Return all nodes in a list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="treeNode"></param>
    /// <param name="getChildren"></param>
    /// <returns></returns>
    public static IEnumerable<T> GetAllNodes<T>(T treeNode, Func<T,IEnumerable<T>> getChildren )
    {
        List<T> result = new List<T>();
        result.Add(treeNode);

        var children = getChildren(treeNode);
        if (children != null)
        {
            foreach (T child in children)
            {
                result.AddRange(GetAllNodes(child, getChildren));
            }
        }
        return result;
    }

    /// <summary>
    /// Do the action on each node if matching to the condition
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="treeNode"></param>
    /// <param name="getChildren"></param>
    /// <param name="condition"></param>
    /// <param name="action"></param>
    /// <param name="elseAction"></param>
    public static void DoActionOnCondition<T>(T treeNode,  Func<T,IEnumerable<T>> getChildren , Func<T, bool> condition, Action<T> action,  Action<T> elseAction = null)
    {
        if (treeNode != null  && condition != null && action != null)
        {
            if (condition(treeNode))
            {
                action(treeNode);
            }
            else if (elseAction != null)
            {
                elseAction(treeNode);
            }

            var children = getChildren(treeNode);
            if (children != null)
            {
                foreach (T child in children)
                {
                    DoActionOnCondition(child, getChildren, condition, action, elseAction);
                }
            }
        }

    }


    /// <summary>
    /// Get site tree
    /// </summary>
    /// <typeparam name="P"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="rootId"></param>
    /// <param name="items"></param>
    /// <param name="id"></param>
    /// <param name="parentId"></param>
    /// <param name="children"></param>
    /// <returns></returns>
    public static T GetTree<P, T>(P rootId, IEnumerable<T> items, Func<T, P> id, Func<T, P?> parentId, Func<T, IList<T>> children) where T : class where P : struct
    {
        if (items != null && items.Any())
        {
            var assets = items.ToDictionary(id);

            var assetsGrouped = assets.Values.GroupBy(parentId);
            foreach (var assetGroup in assetsGrouped)
            {
                if (assetGroup.Key.HasValue)
                {
                    var asset = assets[assetGroup.Key.Value];
                    foreach (var child in assetGroup)
                    {
                        children(asset).Add(child);
                    }
                }
            }

            return assets[rootId];
        }

        return null;
    }
       
}