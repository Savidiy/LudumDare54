using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Savidiy.Utils
{
    public class EditorScriptableObjectLoader<T> where T : ScriptableObject
    {
        private T _asset;

        public T GetAsset()
        {
#if UNITY_EDITOR
            if (_asset != null)
                return _asset;

            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
            if (guids.Length == 0)
                throw new Exception($"Can't find asset of type '{typeof(T)}'");

            string guid = guids[0];
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

            if (guids.Length > 1)
                Debug.LogError($"Found '{guids.Length}' assets of type '{typeof(T)}', returned '{assetPath}'", asset);

            _asset = asset;
#endif
            return _asset;
        }
    }
}