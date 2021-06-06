Shader "Custom/DissolveFade"
{
	Properties
	{
		_MainTex("Main Tex (RGB)", 2D) = "white" {}
		_DissolveTex("Dissolve Tex (RGB)", 2D) = "white" {}
		_Threshold("Threshold", Range(0, 1)) = 0.0
	}

	SubShader
	{
		Tags 
		{ 
			"RenderType" = "Transparent"
			"Queue" = "Transparent"
		}

		Blend SrcAlpha OneMinusSrcAlpha

		Pass 
		{
			CGPROGRAM
			#pragma vertex vert         // 頂点シェーダに使う関数名を指定
			#pragma fragment frag       // フラグメントシェーダに使う関数名を指定

			#include "UnityCG.cginc"    // シェーダ標準関数のライブラリをインクルード

			// 頂点シェーダの入力データ構造体
			struct appdata_t
			{
				float4 vertex : POSITION;      // 頂点の位置情報
				float2 texcoord : TEXCOORD0;   // UV情報
			};

			// フラグメントシェーダの入力構造体
			struct v2f
			{
				float4 vertex : SV_POSITION;    // 頂点の位置情報
				float2 texcoord : TEXCOORD0;    // UV情報
			};

			sampler2D _MainTex;
			sampler2D _DissolveTex;
			float _Threshold;

			v2f vert(appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);  // カメラ座標系への変換
				o.texcoord = v.texcoord;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.texcoord);
				fixed4 col2 = tex2D(_DissolveTex, i.texcoord);

				// ディゾルブ用テクスチャのRが閾値より上か下で判断
				if (col2.r < _Threshold)
				{
					col.a = 0.0f;
				}
				else
				{
					col.a = 1.0f;
				}

				/*
				// エッジ付近の色を変える
				if (_Threshold != 0.0f && col2.r < _Threshold + 0.02f)
				{
					col = float4(1.0f, 1.0f, 1.0f, col.a);
				}
				*/

				return col;
			}
			ENDCG
		}
	}
}
