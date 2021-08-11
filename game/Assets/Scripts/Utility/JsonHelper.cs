using System;
using System.Collections.Generic;
using UnityEngine;

public static class JsonHelper
{
    public static List<T> FromJson<T>(string json)
    {
        var wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(List<T> list)
    {
        var wrapper = new Wrapper<T> {Items = list};
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(List<T> list, bool prettyPrint)
    {
        var wrapper = new Wrapper<T> {Items = list};
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public List<T> Items;
    }
}
