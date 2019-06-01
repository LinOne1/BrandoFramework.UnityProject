#if UNITY_EDITOR
//-----------------------------------------------------------------------// <copyright file="StaticRootPropertyResolver.cs" company="Sirenix IVS"> // Copyright (c) Sirenix IVS. All rights reserved.// </copyright>//-----------------------------------------------------------------------
//-----------------------------------------------------------------------
// <copyright file="StaticRootPropertyResolver.cs" company="Sirenix IVS">
// Copyright (c) Sirenix IVS. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Sirenix.OdinInspector.Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Sirenix.Serialization;
    using Sirenix.Utilities;

    [OdinDontRegister] // DefaultOdinPropertyResolverLocator handles putting this on static tree root properties
    public class StaticRootPropertyResolver<T> : BaseMemberPropertyResolver<T>
    {
        private Type targetType;
        private PropertyContext<bool> allowObsoleteMembers;

        protected override InspectorPropertyInfo[] GetPropertyInfos()
        {
            this.targetType = this.ValueEntry.TypeOfValue;
            var members = targetType.GetAllMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            var infos = new List<InspectorPropertyInfo>();

            this.allowObsoleteMembers = this.Property.Context.GetGlobal("ALLOW_OBSOLETE_STATIC_MEMBERS", false);

            foreach (var member in members.Where(Filter).OrderBy(Order))
            {
                var attributes = new List<Attribute>();
                InspectorPropertyInfoUtility.ProcessAttributes(this.Property, member, attributes);

                if (member is MethodInfo && !attributes.HasAttribute<ButtonAttribute>() && !attributes.HasAttribute<OnInspectorGUIAttribute>())
                {
                    attributes.Add(new ButtonAttribute(ButtonSizes.Medium));
                }

                var info = InspectorPropertyInfo.CreateForMember(member, true, SerializationBackend.None, attributes);

                InspectorPropertyInfo previousPropertyWithName = null;
                int previousPropertyIndex = -1;

                for (int j = 0; j < infos.Count; j++)
                {
                    if (infos[j].PropertyName == info.PropertyName)
                    {
                        previousPropertyIndex = j;
                        previousPropertyWithName = infos[j];
                        break;
                    }
                }

                if (previousPropertyWithName != null)
                {
                    bool createAlias = true;

                    if (member.SignaturesAreEqual(previousPropertyWithName.GetMemberInfo()))
                    {
                        createAlias = false;
                        infos.RemoveAt(previousPropertyIndex);
                    }

                    if (createAlias)
                    {
                        var alias = InspectorPropertyInfoUtility.GetPrivateMemberAlias(previousPropertyWithName.GetMemberInfo(), previousPropertyWithName.TypeOfOwner.GetNiceName(), " -> ");
                        infos[previousPropertyIndex] = InspectorPropertyInfo.CreateForMember(alias, true, SerializationBackend.None, attributes);
                    }
                }

                infos.Add(info);
            }

            return InspectorPropertyInfoUtility.BuildPropertyGroupsAndFinalize(this.Property, targetType, infos, false);
        }

        private int Order(MemberInfo arg1)
        {
            if (arg1 is FieldInfo) return 1;
            if (arg1 is PropertyInfo) return 2;
            if (arg1 is MethodInfo) return 3;
            return 4;
        }

        private bool Filter(MemberInfo member)
        {
            if (member.DeclaringType == typeof(object) && targetType != typeof(object)) return false;
            if (!(member is FieldInfo || member is PropertyInfo || member is MethodInfo)) return false;
            if (member is FieldInfo && (member as FieldInfo).IsSpecialName) return false;
            if (member is MethodInfo && (member as MethodInfo).IsSpecialName) return false;
            if (member is PropertyInfo && (member as PropertyInfo).IsSpecialName) return false;
            if (member.IsDefined<CompilerGeneratedAttribute>()) return false;
            if (!allowObsoleteMembers.Value && member.IsDefined<ObsoleteAttribute>()) return false;

            return true;
        }
    }
}
#endif