Shader "Unlit/TileMasking"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_MaskTex("Mask Texture", 2D)="white"{}
		_UpLeft("Top Left",float) = 0
		_UpRight("Top Right",float) = 0
		_DownLeft("Down Left",float) = 0
		_DownRight("Down Right",float) = 0

	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex,_MaskTex;
			float4 _MainTex_ST;
			float _UpLeft,_UpRight,_DownLeft,_DownRight;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);

				//maskUVBottomLeft = float2((maskTileWidth / maskTextureWidth) * maskTileIndex, 0)
				//maskUVSize = float2((maskTileWidth / maskTextureWidth) * tileVertexUV.x, (maskTileHeight / maskTextureHeight) * tileVertexUV.y)


				// top right		-0.03448276/2, -0.5
				// top Left			0.03448276/2,   -0.5
				// down right		-0.03448276/2,  0.5
				// down Left		0.03448276/2,   0.5

				float multiplier=0.03448276;

				// top right corner position
				float2 upRightMaskUVPosition = float2(_UpRight*multiplier*2 -multiplier/2 ,-0.5);

				float2 upRightMaskUVSize = float2(i.uv.x *multiplier , i.uv.y );

			
				// top left corner positon
				float2 upLeftMaskUVPosition = float2(_UpLeft*multiplier*2 + multiplier/2 ,-0.5);

				float2 upLeftMaskUVSize = float2(i.uv.x *multiplier , i.uv.y );
	

				// down right
				float2 downRightMaskUVPosition = float2(_DownRight*multiplier*2 - multiplier/2 ,0.5);

				float2 downRightMaskUVSize = float2(i.uv.x *multiplier , i.uv.y );


				//down left
				float2 downLeftMaskUVPosition = float2(_DownLeft*multiplier*2 + multiplier/2 ,0.5);

				float2 downLeftMaskUVSize = float2(i.uv.x *multiplier , i.uv.y);



				fixed4 m=tex2D(_MaskTex,upLeftMaskUVPosition+upLeftMaskUVSize)*tex2D(_MaskTex,upRightMaskUVPosition+upRightMaskUVSize)*tex2D(_MaskTex,downRightMaskUVPosition+downRightMaskUVSize)*tex2D(_MaskTex,downLeftMaskUVPosition+downLeftMaskUVSize);




				col=m*col;

			
				col.rgb*=m.g;
				return col;
			}
			ENDCG
		}
	}
}
