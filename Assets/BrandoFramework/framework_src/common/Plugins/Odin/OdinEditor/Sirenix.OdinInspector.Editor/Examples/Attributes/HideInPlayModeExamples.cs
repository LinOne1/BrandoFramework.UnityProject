#if UNITY_EDITOR
//-----------------------------------------------------------------------// <copyright file="HideInPlayModeExamples.cs" company="Sirenix IVS"> // Copyright (c) Sirenix IVS. All rights reserved.// </copyright>//-----------------------------------------------------------------------
#pragma warning disable
namespace Sirenix.OdinInspector.Editor.Examples
{
    [AttributeExample(typeof(HideInPlayModeAttribute))]
    public class HideInPlayModeExamples
    {
        [Title("Hidden in play mode")]
        [HideInPlayMode]
        public int A;

        [HideInPlayMode]
        public int B;
    }
}
#endif