Shader "Custom/Outline/Composite"
{
    Properties
    {
        _MainTex ("source", 2D) = "" {}
        _StencilTex ("stencil", 2D) = "" {}
        _BlurTex ("blur", 2D) = "" {}
        _OutlineStrength ("OutlineStrength", Range(1, 5)) = 3
    }
    
    SubShader
    {
        Pass
        {
            ZTest Always
            Cull Off
            ZWrite Off
            Lighting Off
            Fog { Mode off }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
            
            #include "UnityCG.cginc"
        
            sampler2D _MainTex;
            sampler2D _StencilTex;
            sampler2D _BlurTex;
            float _OutlineStrength;
            float4 _MainTex_TexelSize;

            struct a2v
            {
                float4 vertex: POSITION;
                half2 texcoord: TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                half2 uv : TEXCOORD0;
            };
            
            v2f vert(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                if (_MainTex_TexelSize.y < 0)
                    o.uv.y = 1 - o.uv.y;
                return o;
            }
            
            fixed4 frag(v2f i) : COLOR
            {
                fixed4 source = tex2D(_MainTex, i.uv);
                fixed4 stencil = tex2D(_StencilTex, i.uv);
                if (any(stencil.rgb))
                {
                    return source;
                }
                else
                {
                    fixed4 blur = tex2D(_BlurTex, i.uv);
                    fixed4 color;
                    color.rgb = lerp(source.rgb, blur.rgb * _OutlineStrength, saturate(blur.a - stencil.a));
                    color.a = source.a;
                    return color;
                }
            }

            ENDCG
        }
    }
    
    Fallback Off
}