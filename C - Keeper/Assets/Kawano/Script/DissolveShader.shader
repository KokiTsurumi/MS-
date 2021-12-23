Shader "Custom/DissolveShader"
{
	Properties{
		[PerRendererData] _MainTex("Main texture", 2D) = "white" {}
		_DissolveTex("Dissolution texture", 2D) = "gray" {}
		_Threshold("Threshold", Range(0., 1.01)) = 0.
		_CutOff("Cut off", Range(0.0, 1.0)) = 0.0
		_Width("Width", Float) = 0.01
	}

		SubShader{

			Tags { "Queue" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha

			Pass {

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct v2f {
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
				};

				sampler2D _MainTex;

				float _CutOff;
				float _Width;

				v2f vert(appdata_base v) {
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = v.texcoord;
					return o;
				}
				
				sampler2D _DissolveTex;
				float _Threshold;
				
				fixed4 frag(v2f i) : SV_Target {
					float4 c = tex2D(_MainTex, i.uv);
					float val = tex2D(_DissolveTex, i.uv).r;

					//c.a *= step(_Threshold, val);//1.01~0Å@1.01Ç™ìßñæ

					fixed a = Luminance(tex2D(_MainTex,i.uv).xyz);
					fixed w = _Width * 1.5;
					fixed b = smoothstep(_Threshold - w, _Threshold, a) - smoothstep(_Threshold, _Threshold + w, a);

					c.a = smoothstep(_Threshold - w,_Threshold, val);//1.01~0Å@1.01Ç™ìßñæ

					if (c.a <= 0.5)
						c.rgb = (0.5, 0.5, 0.8);

					return c;
				}
				ENDCG
			}
		}
}