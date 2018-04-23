// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Custom/RenderFirst"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_AlphaChannel("Alpha Level", Float) = 1
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Overlay"
		}
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			ZTest Always

			CGPROGRAM
	#pragma vertex vert             
	#pragma fragment frag

			struct vertInput
		{
			float4 pos : POSITION;
			float2 uv : TEXCOORD;
		};

		struct vertOutput
		{
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD;
		};

		vertOutput vert(vertInput input)
		{
			vertOutput o;
			o.pos = UnityObjectToClipPos(input.pos);
			o.uv = input.uv;
			return o;
		}

		sampler2D _MainTex;
		float _AlphaChannel;

		float4 frag(vertOutput output) : COLOR
		{
			float4 color = tex2D(_MainTex, output.uv);
			if (color.a != 0)
			{
				color.a = _AlphaChannel;
			}
			return color;
		}
			ENDCG
		}
	}
}
