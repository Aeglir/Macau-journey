Shader "MAINMENU/Fillet"
{
    Properties
    {
        _Color ("Color",Color) = (1,1,1,1)
        _RoundRadius("Round Radius", Range(0,0.25)) = 0.1
        _Width("Width",Int) = 0
        _Height("Height",Int) = 0
    }
    SubShader
    {
        Tags { "Queue"="background" }

        Pass
        {
            Cull Back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            fixed4 _Color;
            float _RoundRadius;
            float _Height;
            float _Width;

            struct a2v{
                float4 vertex : POSITION;
            };

            struct v2f{
                float4 pos:SV_POSITION;
                float2 modelPos:TEXCOORD0;
            };

            v2f vert (a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.modelPos=float2(v.vertex.x,v.vertex.y);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float factor = _RoundRadius*_Width/2;
                float x = abs(i.modelPos.x);
                float y = abs(i.modelPos.y);
                float2 arc = float2(_Width/2-factor,_Height/2-factor);
                float dis = length(float2(x-arc.x,y-arc.y));
                if(dis>factor&&x>arc.x&&y>arc.y)
                {
                    discard;
                }   
                return _Color;
            }
            ENDCG
        }
    }
}
