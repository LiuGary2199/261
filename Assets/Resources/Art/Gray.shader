Shader "Gray"  //UI去色变灰
{
    Properties
    {
        [HideInInspector] _MainTex ("主纹理", 2D) = "white" {} // 主纹理属性
        [HideInInspector] _Color ("颜色", Color) = (1,1,1,1) // 颜色属性，默认是白色

        // Stencil 设置 加上这些内容让其能正确被Mask组件遮挡
        [HideInInspector] _StencilComp("Stencil 比较", Float) = 8 // Stencil比较方式
        [HideInInspector] _Stencil("Stencil ID", Float) = 0 // Stencil ID，用于标识不同的Stencil对象
        [HideInInspector] _StencilOp("Stencil 操作", Float) = 0 // Stencil操作，0表示保持不变
        [HideInInspector] _StencilWriteMask("Stencil 写入掩码", Float) = 255 // Stencil写入掩码
        [HideInInspector] _StencilReadMask("Stencil 读取掩码", Float) = 255 // Stencil读取掩码
        [HideInInspector] _ColorMask("颜色掩码", Float) = 15 // 颜色掩码，确保可以写入颜色数据
    }

    SubShader
    {
        Tags { "RenderType"="UI" } // 渲染类型为UI
        Blend SrcAlpha OneMinusSrcAlpha // 混合模式，适用于透明效果
        Cull Off // 不进行面剔除
        ZWrite Off // 不写入深度缓冲区
        ZTest LEqual // 深度测试方式
        ColorMask RGB // 写入颜色的掩码

        Pass
        {
            // Stencil 设置
            Stencil
            {
                Ref[_Stencil] // 设置Stencil引用值
                Comp[_StencilComp] // 设置Stencil比较方式
                Pass[_StencilOp] // 设置Stencil操作
                ReadMask[_StencilReadMask] // 设置Stencil读取掩码
                WriteMask[_StencilWriteMask] // 设置Stencil写入掩码
            }

            ColorMask[_ColorMask] // 设置颜色掩码

            Name "Gray" // 当前Pass的名称
            CGPROGRAM
            #pragma vertex vert // 指定顶点着色器函数
            #pragma fragment frag // 指定片元着色器函数
            #include "UnityUI.cginc" // 包含Unity的UI CG包

            struct appdata_t
            {
                float4 vertex : POSITION; // 顶点位置
                float2 uv : TEXCOORD0; // 纹理坐标
                float4 color : COLOR; // 顶点颜色
            };

            struct v2f
            {
                float4 vertex : SV_POSITION; // 裁剪空间下的顶点位置
                float2 uv : TEXCOORD0; // 纹理坐标
                fixed4 color : COLOR; // 传递颜色
            };

            sampler2D _MainTex; // 纹理采样器
            fixed4 _Color; // Shader属性-颜色

            // 顶点着色器
            v2f vert(appdata_t v)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(v.vertex); // 计算顶点在裁剪空间的位置
                OUT.uv = v.uv; // 传递纹理坐标
                OUT.color = v.color * _Color; // 计算顶点的最终颜色
                return OUT;
            }

            // 片元着色器
            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, IN.uv) * IN.color; // 采样纹理并乘以顶点颜色
                float gray = dot(texColor.rgb, float3(0.299, 0.587, 0.114)); // 转换为灰度值
                return fixed4(gray, gray, gray, texColor.a); // 返回灰度值并保留alpha通道
            }
            ENDCG
        }
    }
    FallBack "Diffuse" // 如果Shader渲染失败，则回退到Diffuse Shader
}
