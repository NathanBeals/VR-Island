Shader "Custom/TransparentPaintTexture"
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_TintColor("Tint Color", Color) = (1,1,1,1)
		_Transparency("Transparency", Range(0.0, 0.5)) = .25
	}

		SubShader
		{
			Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
			LOD 100

			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

				GrabPass
				{
						"_BackgroundTexture"
				}

			pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float4 _TintColor;
				float _Transparency;
				sampler2D _BackgroundTexture;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					return o;
				}
				
				fixed4 frag(v2f i) : SV_Target
				{
					//bkcol
				//	half4 bkCol = tex2D(_BackgroundTexture, i.uv);

					//sample the texture
					fixed4 col = tex2D(_MainTex, i.uv) + _TintColor;

				//	if (bkCol.r < 50 && bkCol.g < 50 && bkCol.b < 50)
					//	if (bkCol.r < 1 && bkCol.g > .2 && bkCol.b < 1)
				col.a = _Transparency;
						return col;
				}
				ENDCG
			}

		//Second Pass

		//pass
		//	{
		//		CGPROGRAM
		//		#pragma vertex vert
		//		#pragma fragment frag

		//		#include "UnityCG.cginc"

		//		struct appdata
		//		{
		//			float4 vertex : POSITION;
		//			float2 uv : TEXCOORD0;
		//		};

		//		struct v2f
		//		{
		//			float2 uv : TEXCOORD0;
		//			float4 vertex : SV_POSITION;
		//		};

		//		sampler2D _MainTex;
		//		float4 _MainTex_ST;
		//		float4 _TintColor;
		//		float _Transparency;

		//		v2f vert(appdata v)
		//		{
		//			v2f o;
		//			o.vertex = UnityObjectToClipPos(v.vertex);
		//			o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		//			return o;
		//		}

		//		fixed4 frag(v2f i) : SV_Target
		//		{
		//			//sample the texture
		//			fixed4 col = tex2D(_MainTex, i.uv) + _TintColor;
		//			col.a = _Transparency;
		//			return col;
		//		}
		//		ENDCG
		//	}
		}
}
