Shader "Unlit/BasicVertexLit"
{
	//Basic shader that implements different types of lights on object
	Properties
	{
		//Public properties shown in inspector
		_Color("Main Color", Color) = (1, 0.5, 0.5, 1)
		_SpecColor("Spec Color", Color) = (1, 1, 1, 1)
		_Emission("Emmisive Color", Color) = (0, 0, 0, 0)
		_Shininess("Shininess", Range(0.01, 1)) = 0.7
		_MainTex("Base (RGB)", 2D) = "white" { }
	}
	SubShader
	{
		Pass
		{
			Name "Pass1"
			Material
			{
				//Assign colors to lighting functions in material
				Diffuse[_Color]
				Ambient[_Color]
				Shininess[_Shininess]
				Specular[_SpecColor]
				Emission[_Emission]
			}
				//Turn lighting on, tell shader to use separate color for specular lighting
				Lighting On
				SeparateSpecular On
				//Set texture on shader
				SetTexture[_MainTex]
			{
				//Sets base color
				constantColor[_Color]
				//Combine color and alpha
				Combine texture * primary DOUBLE, texture * constant
			}
		}
	}
		//What shader to use if none of the above works on current hardware
		FallBack "VertexLit"
}
