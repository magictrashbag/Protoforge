// Copyright (c) 2018 Maxim Tiourin
// Please direct any bug reports/feedback to maximtiourin@gmail.com

Shader "Fizzik/HealthBarShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ColorHealth ("Health Color", Color) = (1, 1, 1, 1)
		_ColorDamage ("Damage Color", Color) = (1, 1, 1, 1)
		_ColorTick ("Tick Color", Color) = (1, 1, 1, 1)
		_ColorBigTick ("Big Tick Color", Color) = (1, 1, 1, 1)
		_ColorBorder ("Border Color", Color) = (1, 1, 1, 1)
		_HealthPerTick ("Health per Tick", Float) = 25
		_HealthPerBigTick ("Health per Big Tick", Float) = 50
		_HealthCurrent ("Current Health", Float) = 100
		_HealthTotal ("Total Health", Float) = 100
		_WidthTick ("Tick Width", Float) = 1
		_WidthBigTick ("Big Tick Width", Float) = 1
		_WidthBorder ("Border Width", Float) = 1
		_HeightPercentTick ("Tick Height Percent", Float) = 1
		_HeightPercentBigTick ("Big Tick Height Percent", Float) = 1
		_HealthbarSize ("Healthbar Size", Vector) = (100, 100, 0, 0)
		_EnabledBorder ("Border Enabled", Float) = 1
		_EnabledTick ("Tick Enabled", Float) = 1
		_EnabledBigTick ("Big Tick Enabled", Float) = 0

	}
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
		}

		Cull Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Blend One OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			fixed4 _ColorHealth;
			fixed4 _ColorDamage;
			fixed4 _ColorTick;
			fixed4 _ColorBigTick;
			fixed4 _ColorBorder;
			float _HealthPerTick;
			float _HealthPerBigTick;
			float _HealthCurrent;
			float _HealthTotal;
			float _WidthTick;
			float _WidthBigTick;
			float _WidthBorder;
			fixed _HeightPercentTick;
			fixed _HeightPercentBigTick;
			float4 _HealthbarSize;
			fixed _EnabledBorder;
			fixed _EnabledTick;
			fixed _EnabledBigTick;


			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.color = v.color;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, i.uv);

				float healthPercent = _HealthCurrent / _HealthTotal;

				float tickPercent = _HealthPerTick / _HealthTotal;
				float tickwidth = _WidthTick;
				float tickwh = tickwidth / 2.0;

				float btickPercent = _HealthPerBigTick / _HealthTotal;
				float btickwidth = _WidthBigTick;
				float btickwh = btickwidth / 2.0;

				float borderwidth = _WidthBorder;

				float tickright = i.uv.x % tickPercent;
				float tickleft = tickPercent - tickright;

				float btickright = i.uv.x % btickPercent;
				float btickleft = btickPercent - btickright;

				float xscale = _HealthbarSize.x;
				float yscale = _HealthbarSize.y;

				if ((_EnabledBorder >= 1) && ((i.uv.x * xscale < borderwidth || i.uv.x * xscale > xscale - borderwidth) || (i.uv.y * yscale < borderwidth || i.uv.y * yscale > yscale - borderwidth))) {
					//Border Pixel
					c *= _ColorBorder;
				}
				else {
					if ((_EnabledBigTick >= 1) && ((btickleft * xscale < btickwh || btickright * xscale < btickwh) && (i.uv.x * xscale >= btickwh && i.uv.x * xscale <= xscale - btickwh) && (i.uv.y >(1.0 - _HeightPercentBigTick) && i.uv.y <= 1.0))) {
						//Big Tick Pixel
						c *= _ColorBigTick;
					}
					else if ((_EnabledTick) && ((tickleft * xscale < tickwh || tickright * xscale < tickwh) && (i.uv.x * xscale >= tickwh && i.uv.x * xscale <= xscale - tickwh) && (i.uv.y > (1.0 - _HeightPercentTick) && i.uv.y <= 1.0))) {
						//Tick Pixel
						c *= _ColorTick;
					}
					else {
						if (i.uv.x <= healthPercent) {
							//Health Pixel
							c *= _ColorHealth;
						}
						else {
							//Damage Pixel
							c *= _ColorDamage;
						}
					}
				}

				c.rgb *= c.a;
				return c;
			}
			ENDCG
		}
	}
}
