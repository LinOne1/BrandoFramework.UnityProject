#if UNITY_EDITOR
//-----------------------------------------------------------------------// <copyright file="CustomContextMenuExamples.cs" company="Sirenix IVS"> // Copyright (c) Sirenix IVS. All rights reserved.// </copyright>//-----------------------------------------------------------------------
#pragma warning disable
namespace Sirenix.OdinInspector.Editor.Examples
{
    using UnityEngine;

    [AttributeExample(typeof(CustomContextMenuAttribute))]
    public class CustomContextMenuExamples
    {
        [InfoBox("A custom context menu is added on this property. Right click the property to view the custom context menu.")]
        [CustomContextMenu("Say Hello/Twice", "SayHello")]
        public int MyProperty;

        private void SayHello()
        {
            Debug.Log("Hello Twice");
        }
    }
}
#endif