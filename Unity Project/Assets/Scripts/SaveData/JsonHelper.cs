using System;
using UnityEngine;

/// <summary>
/// JsonHelper script, supplied by boxhead productions: http://www.boxheadproductions.com.au/deserializing-top-level-arrays-in-json-with-unity/
///     This script is to be used together with Unity's JSON serializer.
///     These fucntions will allow serializing/deserializing of arrays.
///     
///     A separate function with ability to use 'pretty print' added by Daniel Pokladek.
/// </summary>

public static class JsonHelper {

    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Objects;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Objects = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Objects = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Objects;
    }
}
