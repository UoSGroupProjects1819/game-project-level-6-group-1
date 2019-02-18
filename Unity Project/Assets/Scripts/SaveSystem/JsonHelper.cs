using System;
using UnityEngine;

namespace SaveSystem
{
    /// <summary>
    /// JsonHelper script, supplied by boxhead productions: http://www.boxheadproductions.com.au/deserializing-top-level-arrays-in-json-with-unity/
    ///     This script is to be used together with Unity's JSON serializer.
    ///     These functions will allow serializing/deserializing of arrays.
    ///     
    ///     A separate function with ability to use 'pretty print' added by Daniel Pokladek.
    /// </summary>

    public static class JsonHelper {

        public static T[] FromJson<T>(string json)
        {
            Wrappers<T> wrapper = JsonUtility.FromJson<Wrappers<T>>(json);
            return wrapper.planetObjects;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrappers<T> wrapper = new Wrappers<T> {planetObjects = array};
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrappers<T> wrapper = new Wrappers<T> {planetObjects = array};
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        public static string ToJsonPlanetItems<T>(T[] array, bool prettyPrint)
        {
            Wrappers<T> wrapper = new Wrappers<T> {planetObjects = array};
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        public static string ToJsonInventory<T>(T[] array, bool prettyPrint)
        {
            Wrappers<T> wrapper = new Wrappers<T> {inventoryItems = array};
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrappers<T>
        {
            public T[] planetObjects;
            public T[] inventoryItems;
        }
    }
}
