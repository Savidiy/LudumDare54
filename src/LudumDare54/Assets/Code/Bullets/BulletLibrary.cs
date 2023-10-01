using System;
using System.Collections.Generic;
using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(BulletLibrary), menuName = "Static Data/" + nameof(BulletLibrary), order = 0)]
    public sealed class BulletLibrary : AutoSaveScriptableObject
    {
        public List<BulletStaticData> Datas = new();

        public ValueDropdownList<string> BulletIds { get; } = new();

        protected override void OnValidate()
        {
            base.OnValidate();
            BulletIds.Clear();
            foreach (BulletStaticData data in Datas)
                BulletIds.Add(data.BulletId);
        }

        public BulletStaticData Get(string bulletId)
        {
            foreach (BulletStaticData data in Datas)
                if (data.BulletId.Equals(bulletId))
                    return data;

            Debug.LogError($"Bullet with id '{bulletId}' not found");
            return Datas[0];
        }
    }

    [Serializable]
    public class BulletStaticData
    {
        public string BulletId = "";
        public BulletBehaviour BulletPrefab;
        [Min(0)] public float BulletSpeed;
        [Min(0)] public float BulletLifeTime;
    }
}