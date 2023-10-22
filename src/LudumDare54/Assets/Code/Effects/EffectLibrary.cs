using System;
using System.Collections.Generic;
using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(EffectLibrary), menuName = "Static Data/" + nameof(EffectLibrary), order = 0)]
    public sealed class EffectLibrary : AutoSaveScriptableObject
    {
        [SerializeField] private List<EffectData> EffectData = new();
        
        public AnimationData GetAnimationData(EffectType effectType)
        {
            foreach (EffectData effectData in EffectData)
                if (effectData.EffectType == effectType)
                    return effectData.AnimationData;

            return AnimationData.Empty;
        }
    }

    [Serializable]
    internal class EffectData
    {
        [InlineButton(nameof(TestEffect))]public EffectType EffectType;
        public AnimationData AnimationData;
        
        private void TestEffect()
        {
            TestEffectBridge.TestEffect(EffectType);
        }
    }
    
    
}