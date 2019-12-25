﻿Shader "GraphicsStudio/Scene/Building"
{
	Properties
	{
		_MainTex ("Albedo(2D)", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_GlowScale("GlowScale",Range(0,2)) = 1
		_Metalic("金属度", Range(0, 1)) = 0.5
		[Toggle] _Normal("使用顶点法线", Float) = 0
		_Normap("法线贴图（RG） 光滑度（B）",2D) = "bump"{}
		
		[Space(10)]
		_CutOff("Alpha 裁剪",Range(0,0.9)) = 0.5
		_EmssiveColor("自发光颜色", Color) = (1,1,1,1)
		_EmssiveScale("自发光强度",Range(0,10)) = 1

		[Space(10)]
		[Header(............MaskTexture............)]
		[NoScaleOffset]_MaskTex("Mask贴图", 2D) = "white" {}
		[Toggle] _SpecularMask("高光遮罩 Mask贴图 R 通道", Float) = 0
		[Toggle] _AlphaTest("Alpha遮罩 Mask贴图 G 通道",Float) = 0
		[Toggle] _Emssive("自发光遮罩 Mask贴图 B 通道",Float) = 0
		[Toggle] _HeightMap("高度图 Mask贴图 B 通道",Float) = 0

		[Space(10)]
		[Toggle] _ReflectionProbe("开启环境反射", Float) = 0
		[Toggle] _ForceBake("强行烘焙", Float) = 0
		[Enum(CullMode)] _Cull("剔除方式", Float) = 2

	}
	SubShader
	{
		Tags { "RenderType"="Opaque" "DepthReplaceTag" = "On" }
		Cull[_Cull]
		LOD 200

		Pass
		{
			Tags{ "LightMode" = "ForwardBase" }
			CGPROGRAM
			#include "Scene_BuildingBaseCginc.cginc"
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#pragma multi_compile_fog
			#pragma multi_compile __ _REALTIME_ON
			#pragma skip_variants SHADOWS_CUBE LIGHTMAP_SHADOW_MIXING
			#pragma multi_compile __ ATMOSPHERICS_FOG
			#pragma shader_feature _SPECULARMASK_ON
			#pragma shader_feature _FORCEBAKE_ON
			#pragma shader_feature _EMSSIVE_ON
			#pragma shader_feature _HEIGHTMAP_ON
			#pragma shader_feature _NORMAL_ON
			#pragma shader_feature _ALPHATEST_ON
			#pragma shader_feature _REFLECTIONPROBE_ON

			float4 frag (vertexOutput i) : SV_Target
			{
				float3 albedo = 0;
				float4 resultColor = (0,0,0,1);
				float emisScale = 0;
			#if _REALTIME_ON
				FragmentCommonData fragData = SceneBuildingFragmentSetup(i, emisScale);
				albedo = fragData.diffColor;
				resultColor = SceneCaculateLight(i,fragData);
				resultColor += CalcLightMapColor(i, s.diffColor);
			#else
				FragmentCommonData fragData = SceneBuildingFragmentSetup(i, emisScale);
				albedo = fragData.diffColor;
				half shadowMask;
				half3 lightMapDiffuse = GetLightMapDiffuse(i.uv.zw, shadowMask);
				resultColor.rgb = CalcBlinnSpecular(fragData.normalWorld, 
					-fragData.eyeVec,
					fragData.diffColor,
					1 - fragData.smoothness,
					fragData.specColor,
					normalize(_GlobalLightDir.xyz),
					_GlobalLightColor.rgb * shadowMask, 
					1.0, 
					0.0);
				//对 lightMapDiffuse 保持引用，避免在 编译阶段被优化删除，导致缺少采样器报错
				resultColor.rgb += lightMapDiffuse*0.00001;
			#endif
			
			#if _EMSSIVE_ON
				half3 emissiveColor
				#if _REALTIME_ON
					emissiveColor = albedo * max(_GlobalEmissiveColor.rgb,
						half3(0.85f, 0.33f, 0.17f)*0.24) * 
						emisScale * _EmssiveScale;)
				#else
					emissiveColor = albedo * max(_GlobalEmissiveColor.rgb, 
						half3(0.85f, 0.33f, 0.17f)*0.24) * 
						_EmssiveColor.rgb * emisScale * _PaintScale;
				#endif
				resultColor += emissiveColor;
			#endif
				resultColor.rgb = resultColor.rgb * _LightStrength;
				UNITY_APPLY_FOG(i.fogCoord, resultColor);
				return resultColor;
			}
			ENDCG
		}
	}
}
