#if UNITY_EDITOR
//-----------------------------------------------------------------------// <copyright file="ShowDrawerChainExamples.cs" company="Sirenix IVS"> // Copyright (c) Sirenix IVS. All rights reserved.// </copyright>//-----------------------------------------------------------------------
#pragma warning disable
namespace Sirenix.OdinInspector.Editor.Examples
{
    using System;
    using UnityEngine;

    [AttributeExample(typeof(ShowDrawerChainAttribute))]
    public class ShowDrawerChainExamples
    {
#if UNITY_EDITOR
        [HorizontalGroup(PaddingRight = -1)]
        [ShowInInspector, PropertyOrder(-1)]
        public bool ToggleHideIf { get { Utilities.Editor.GUIHelper.RequestRepaint(); return UnityEditor.EditorApplication.timeSinceStartup % 3 < 1.5f; } }

        [HorizontalGroup]
        [ShowInInspector, HideLabel, ProgressBar(0, 1.5f)]
        private double Animate { get { return Math.Abs(UnityEditor.EditorApplication.timeSinceStartup % 3 - 1.5f); } }
#endif
        [InfoBox(
            "Any drawer not used in the draw chain will be greyed out in the drawer chain so that you can more easily debug the draw chain. You can see this by toggling the above toggle field.\n\n" +
            "If you have any custom drawers they will show up with green names in the drawer chain.")]
        [ShowDrawerChain]
        [HideIf("ToggleHideIf")]
        public GameObject SomeObject;

        [Range(0, 10)]
        [ShowDrawerChain]
        public float SomeRange;
    } 
}
#endif