Shader "Custom/Outline/Stencil"
{
    Properties
    {
        _Color ("Color",COLOR) = (1,1,1,0)
    }

    SubShader
    {
        Pass
        {   
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest

            #include "UnityCG.cginc"

            float4 _Color;

            struct a2v
            {
                float4 vertex: POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };
            
            v2f vert(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            
            fixed4 frag(v2f i) : SV_Target
            {
                return _Color;
            }

            ENDCG
        }
    }

    FallBack off
}