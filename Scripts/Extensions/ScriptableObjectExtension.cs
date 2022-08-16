using UnityEditor;
using UnityEngine;

namespace Sepjani.Helpers.Scripts.Extensions
{
    public static class ScriptableObjectExtension
    {
        public static T Clone<T>(this T scriptableObject) where T : ScriptableObject
        {
            if (scriptableObject == null)
            {
                Debug.LogError($"ScriptableObject was null. Returning default {typeof(T)} object.");
                return (T) ScriptableObject.CreateInstance(typeof(T));
            }

            var instance = Object.Instantiate(scriptableObject);
            instance.name = scriptableObject.name; // remove (Clone) from name
            return instance;
        }

#if UNITY_EDITOR
        public static T[] GetAllInstances<T>() where T : ScriptableObject
        {
            var guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            var a = new T[guids.Length];
            for (var i = 0; i < guids.Length; i++) //probably could get optimized 
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;
        }
#endif
    }
}