// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NotASquidgie"
{
	Properties
	{
		_CompressionAmount("CompressionAmount", Range( 0 , 1)) = 0.4452901
		[Header(Alters how much of the impulse should move the object vs compressing it)]_ImpulseMultiplier("ImpulseMultiplier", Float) = 0.02
		_Texture("Texture", 2D) = "white" {}
		[HideInInspector][PerRendererData]_CollisionNormalImpulse("CollisionNormalImpulse", Float) = 0
		[HideInInspector][PerRendererData]_CollisionNormal("CollisionNormal", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float4 _CollisionNormal;
		uniform float _CollisionNormalImpulse;
		uniform float _ImpulseMultiplier;
		uniform float _CompressionAmount;
		uniform sampler2D _Texture;
		uniform float4 _Texture_ST;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float4 _CollisionNormalNegated19 = -_CollisionNormal;
			float4 break25 = _CollisionNormalNegated19;
			float _NormalImpulse20 = _CollisionNormalImpulse;
			float temp_output_38_0 = ( _NormalImpulse20 * _ImpulseMultiplier );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float3 _WorldPosition18 = ase_worldPos;
			float3 break36 = _WorldPosition18;
			float3 appendResult42 = (float3(( ( break25.x * temp_output_38_0 ) + break36.x ) , ( ( break25.y * temp_output_38_0 ) + break36.y ) , break36.z));
			float3 worldToObj37 = mul( unity_WorldToObject, float4( appendResult42, 1 ) ).xyz;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 _VertexPos21 = ase_vertex3Pos;
			float3 objToWorldDir9 = mul( unity_ObjectToWorld, float4( _VertexPos21, 0 ) ).xyz;
			float dotResult12 = dot( float4( objToWorldDir9 , 0.0 ) , _CollisionNormalNegated19 );
			float temp_output_14_0 = saturate( ( saturate( dotResult12 ) * 1.5 ) );
			v.vertex.xyz = ( ( worldToObj37 * temp_output_14_0 * ( 1.0 - (0.0 + (_NormalImpulse20 - 0.0) * (_CompressionAmount - 0.0) / (15.0 - 0.0)) ) ) + ( worldToObj37 * ( 1.0 - temp_output_14_0 ) ) );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Texture = i.uv_texcoord * _Texture_ST.xy + _Texture_ST.zw;
			float4 tex2DNode44 = tex2D( _Texture, uv_Texture );
			o.Emission = tex2DNode44.rgb;
			o.Alpha = tex2DNode44.a;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17000
1536;73;883;770;1760.082;282.2198;1.782338;False;False
Node;AmplifyShaderEditor.PosVertexDataNode;46;-2202.228,-154.764;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;4;-1953.114,-100.3278;Float;False;499.0045;259.7255;Vertex and world positions;2;21;18;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;6;-1425.114,-132.3278;Float;False;736.4633;287.8888;Values via script from Rigidbody;4;23;20;19;45;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;47;-1400.411,30.4428;Float;False;Property;_CollisionNormal;CollisionNormal;4;2;[HideInInspector];[PerRendererData];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;7;-1617.114,779.6722;Float;False;960.8025;434.4297;Use dot product of vert position converted to world space with normal from collision surface to determine impact direction;5;16;13;12;10;9;;1,1,1,1;0;0
Node;AmplifyShaderEditor.NegateNode;23;-1153.114,59.67221;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;45;-1361.498,-64.58916;Float;False;Property;_CollisionNormalImpulse;CollisionNormalImpulse;3;2;[HideInInspector];[PerRendererData];Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;21;-1681.114,-52.32779;Float;False;_VertexPos;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;10;-1553.114,843.6722;Float;False;21;_VertexPos;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;20;-1057.114,-84.32779;Float;False;_NormalImpulse;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;48;-2258.13,64.12718;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RegisterLocalVarNode;19;-977.1139,43.67221;Float;False;_CollisionNormalNegated;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.CommentaryNode;1;-1393.114,203.6722;Float;False;1488.345;531.0997;Gets the impact direction (normal), and moves the overall object in that direction by a far reduced amount of the impulse (force of impact). This new position is used BEFORE compression!;13;43;42;41;40;39;38;37;36;35;34;25;22;17;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TransformDirectionNode;9;-1329.114,827.6722;Float;True;Object;World;False;Fast;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;22;-1326.414,362.2722;Float;False;20;_NormalImpulse;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;16;-1345.114,1115.672;Float;False;19;_CollisionNormalNegated;1;0;OBJECT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;35;-1345.114,251.6722;Float;False;19;_CollisionNormalNegated;1;0;OBJECT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;18;-1697.114,59.67221;Float;False;_WorldPosition;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-1363.714,515.4722;Float;False;Property;_ImpulseMultiplier;ImpulseMultiplier;1;0;Create;True;0;0;False;1;Header(Alters how much of the impulse should move the object vs compressing it);0.02;0.02;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;34;-1313.114,603.6722;Float;False;18;_WorldPosition;1;0;OBJECT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DotProductOpNode;12;-1089.114,843.6722;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-991.1139,411.1723;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;25;-1041.114,267.6722;Float;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.CommentaryNode;8;-497.1139,779.6722;Float;False;590.2426;300.9095;Increase effect amount and use as mask;3;15;14;11;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;2;-641.1139,-180.3278;Float;False;733.7901;332.5344;Take impulse value and remap as a multiplier to 'compress' affected verts;5;33;32;31;30;29;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-481.1139,907.6722;Float;False;Constant;_IncreaseAmount;IncreaseAmount;3;0;Create;True;0;0;False;0;1.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;36;-1073.114,603.6722;Float;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SaturateNode;13;-865.1139,827.6722;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-689.1139,379.6722;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-689.1139,523.6722;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-609.1139,59.67221;Float;False;Property;_CompressionAmount;CompressionAmount;0;0;Create;True;0;0;False;0;0.4452901;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;40;-497.1139,379.6722;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;33;-561.1139,-116.3278;Float;False;20;_NormalImpulse;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;43;-513.1139,523.6722;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-513.1139,-36.32779;Float;False;Constant;_ImpulseMax;ImpulseMax;5;0;Create;True;0;0;False;0;15;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-257.1139,827.6722;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;42;-337.1139,523.6722;Float;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;3;190.8861,427.6722;Float;False;453.8733;303;Leave unaffected verts where they are;2;28;27;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SaturateNode;14;-49.11389,827.6722;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;29;-305.1139,-100.3278;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;15;False;3;FLOAT;0;False;4;FLOAT;0.8;False;1;FLOAT;0
Node;AmplifyShaderEditor.TransformPositionNode;37;-193.1139,523.6722;Float;False;World;Object;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.OneMinusNode;31;-113.1139,-100.3278;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;27;238.8861,555.6722;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;5;190.8861,59.67221;Float;False;286;303;Offset affected verts;1;26;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;254.8861,107.6722;Float;True;3;3;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;414.8861,491.6722;Float;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;24;702.8861,379.6722;Float;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;44;647.9203,96.0416;Float;True;Property;_Texture;Texture;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1083.239,300.8999;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;NotASquidgie;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Absolute;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;23;0;47;0
WireConnection;21;0;46;0
WireConnection;20;0;45;0
WireConnection;19;0;23;0
WireConnection;9;0;10;0
WireConnection;18;0;48;0
WireConnection;12;0;9;0
WireConnection;12;1;16;0
WireConnection;38;0;22;0
WireConnection;38;1;39;0
WireConnection;25;0;35;0
WireConnection;36;0;34;0
WireConnection;13;0;12;0
WireConnection;17;0;25;0
WireConnection;17;1;38;0
WireConnection;41;0;25;1
WireConnection;41;1;38;0
WireConnection;40;0;17;0
WireConnection;40;1;36;0
WireConnection;43;0;41;0
WireConnection;43;1;36;1
WireConnection;11;0;13;0
WireConnection;11;1;15;0
WireConnection;42;0;40;0
WireConnection;42;1;43;0
WireConnection;42;2;36;2
WireConnection;14;0;11;0
WireConnection;29;0;33;0
WireConnection;29;2;30;0
WireConnection;29;4;32;0
WireConnection;37;0;42;0
WireConnection;31;0;29;0
WireConnection;27;0;14;0
WireConnection;26;0;37;0
WireConnection;26;1;14;0
WireConnection;26;2;31;0
WireConnection;28;0;37;0
WireConnection;28;1;27;0
WireConnection;24;0;26;0
WireConnection;24;1;28;0
WireConnection;0;2;44;0
WireConnection;0;9;44;4
WireConnection;0;11;24;0
ASEEND*/
//CHKSM=07188856070D652CF3880D396E87ADB3C868DEF2