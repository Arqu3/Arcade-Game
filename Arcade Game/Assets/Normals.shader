Shader "Unlit/Normals"
{
	SubShader
	{
		Pass
		{
			CGPROGRAM
			//Compilation directives/instructions for this snippet
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f
			{
				float4 pos : SV_POSITION;
				fixed3 color : COLOR0;
			};

			//Vertex to fragment
			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.normal * 0.5 + 0.5;
				return o;
			}

			//Output colors with alpha 1
			fixed4 frag(v2f i) : SV_Target
			{
				return fixed4(i.color, 1);
			}

			ENDCG
		}
	}
}
