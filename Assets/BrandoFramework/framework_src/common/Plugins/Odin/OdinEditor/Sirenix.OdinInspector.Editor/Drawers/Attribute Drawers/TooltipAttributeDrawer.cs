#if UNITY_EDITOR
//-----------------------------------------------------------------------// <copyright file="TooltipAttributeDrawer.cs" company="Sirenix IVS"> // Copyright (c) Sirenix IVS. All rights reserved.// </copyright>//-----------------------------------------------------------------------
//-----------------------------------------------------------------------
// <copyright file="TooltipAttributeDrawer.cs" company="Sirenix IVS">
// Copyright (c) Sirenix IVS. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Sirenix.OdinInspector.Editor.Drawers
{
    using Sirenix.Utilities.Editor;
    using UnityEngine;

    /// <summary>
    /// Draws properties marked with <see cref="TooltipAttribute"/>.
    /// </summary>
    /// <seealso cref="TooltipAttribute"/>

    [DrawerPriority(DrawerPriorityLevel.SuperPriority)]
    public sealed class TooltipAttributeDrawer : OdinAttributeDrawer<TooltipAttribute>
    {
        /// <summary>
        /// Draws the property.
        /// </summary>
        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (label != null)
            {
                var property = this.Property;
                var attribute = this.Attribute;

                var context = property.Context.Get<StringMemberHelper>(this, "StringContext", (StringMemberHelper)null);
                if (context.Value == null)
                {
                    context.Value = new StringMemberHelper(property.ParentType, attribute.tooltip);
                }

                if (context.Value.ErrorMessage != null)
                {
                    SirenixEditorGUI.ErrorMessageBox(context.Value.ErrorMessage);
                }

                label.tooltip = context.Value.GetString(property);
            }

            this.CallNextDrawer(label);
        }
    }

    /// <summary>
    /// Draws properties marked with <see cref="PropertyTooltipAttribute"/>.
    /// </summary>
    /// <seealso cref="PropertyTooltipAttribute"/>

    [DrawerPriority(DrawerPriorityLevel.SuperPriority)]
    public sealed class PropertyTooltipAttributeDrawer : OdinAttributeDrawer<PropertyTooltipAttribute>
    {
        /// <summary>
        /// Draws the property.
        /// </summary>
        protected override void DrawPropertyLayout(GUIContent label)
        {
            var property = this.Property;
            var attribute = this.Attribute;

            if (label != null)
            {
                var context = property.Context.Get<StringMemberHelper>(this, "Tooltip", (StringMemberHelper)null);
                if (context.Value == null)
                {
                    context.Value = new StringMemberHelper(property.ParentType, attribute.Tooltip);
                }

                if (context.Value.ErrorMessage != null)
                {
                    SirenixEditorGUI.ErrorMessageBox(context.Value.ErrorMessage);
                }

                label.tooltip = context.Value.GetString(property);
            }

            this.CallNextDrawer(label);
        }
    }
}
#endif