// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/Lava" {
	Properties {
		_StoneColor ("Stone Color", Color) = (1,1,1,1)
		_MainTex ("Rock Texture (RGB)", 2D) = "white" {}
		[HDR]_BurnedStoneTint("Burned Stone Tint", Color) = (1,1,1,1)
		_LavaTex ("Lava Texture", 2D) = "grey" {}
		_NormalMap ("Normal map", 2D) = "grey" {}
		_NormalPower ("NormalPower", Range(1,5)) = 1.0
		_Occlusion("Occlusion", 2D) = "black" {}
		_Noise("Noise texture", 2D) = "white" {}
		[NoScaleOffset]_MetallicMap("Metallic map", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		[Gamma]_Metallic ("Metallic", Range(0,1)) = 0.0
		_LavaStartHeight ("Lava Start Height", float) = 0.0
		_BurnedStoneHeight ("Burned stone height", Range(0,50)) = 1.0
		_NoisePower ("Noise Power", Range(0,20)) = 1.0
		_Direction("Lava Glow Direction", Vector) = (0,0,0,1)
		_GlowingLavaSize("Glowing Lava Size", Range(0,2)) = 0.5
		[HDR]_DirectionalLavaTint("Directional Lava Tint", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows 


		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex, _LavaTex, _Noise, _NormalMap, _MetallicMap, _Occlusion;
		float _LavaStartHeight, _BurnedStoneHeight, _GlowingLavaSize, _NormalPower, _NoisePower;
		fixed4 _BurnedStoneTint, _DirectionalLavaTint, _Direction, _StoneColor;

		struct Input {
			float3 worldNormal;
			INTERNAL_DATA
			float2 uv_MainTex;
			float2 uv_LavaTex;
			float2 uv_NormalMap;
			float2 uv_Occlusion;
			float2 uv_Noise;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			o.Normal = UnpackNormal (tex2D (_NormalMap, IN.uv_NormalMap)) * _NormalPower;
			fixed4 rawOcclusion = tex2D(_Occlusion, IN.uv_Occlusion);
			fixed4 colorRock = tex2D (_MainTex, IN.uv_MainTex) * rawOcclusion * _StoneColor;
			fixed4 rawLava = tex2D(_LavaTex, IN.uv_LavaTex);
			fixed4 noise = pow(tex2D(_Noise, IN.uv_Noise), _NoisePower);

			fixed4 burnedStonePosition = (1 - saturate((IN.worldPos.y - _LavaStartHeight) / _BurnedStoneHeight)) ;

			fixed4 verticalLava = rawLava * _BurnedStoneTint;

			fixed4 result = (2 * burnedStonePosition * noise.r);

			float3 rockTexture = colorRock.rgb * (1 - result.rgb) + (result.rgb * verticalLava);
			float3 poweredUpNormals = normalize(WorldNormalVector (IN, o.Normal) * 2);
			float mossDirection = dot(poweredUpNormals, - normalize(_Direction).xyz);

			mossDirection = saturate(smoothstep(0, 1, _GlowingLavaSize * mossDirection));
			o.Albedo = rockTexture;
			o.Emission = (rawLava * _DirectionalLavaTint).rgb * mossDirection;

			float4 metalTex = tex2D (_MetallicMap, IN.uv_MainTex);
			o.Metallic = metalTex.r * _Metallic;
			o.Smoothness = metalTex.a * _Glossiness;
			o.Alpha = colorRock.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
