using System;
using UnityEngine;
using UnityEngine.Rendering;
public class OutlineEffect : MonoBehaviour {
    public static Action<CommandBuffer> renderEvent; 
    public float offsetScale = 2;
    public int iterate = 3;
    public float outlineStrength = 3;

    Material blurMaterial;
    Material compositeMaterial;
    CommandBuffer commandBuffer;
    RenderTexture stencilTex;
    RenderTexture blurTex;
    
    
    private void Awake()
    {
        blurMaterial = new Material(Shader.Find("Custom/Outline/Blur"));
        compositeMaterial = new Material(Shader.Find("Custom/Outline/Composite"));
        commandBuffer = new CommandBuffer();
    }
    
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (renderEvent != null)
        {
            RenderStencil();
            RenderBlur(source.width, source.height);
            RenderComposite(source, destination);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
    
    private void RenderStencil()
    {
        stencilTex = RenderTexture.GetTemporary(Screen.width, Screen.height, 0);
        commandBuffer.SetRenderTarget(stencilTex);
        commandBuffer.ClearRenderTarget(true, true, Color.clear);
        renderEvent.Invoke(commandBuffer);
        Graphics.ExecuteCommandBuffer(commandBuffer);
    }

    private void RenderBlur(int width, int height)
    {
        blurTex = RenderTexture.GetTemporary(width, height, 0);
        RenderTexture temp = RenderTexture.GetTemporary(width, height, 0);
        blurMaterial.SetFloat("_OffsetScale", offsetScale);
        Graphics.Blit(stencilTex, blurTex, blurMaterial);
        for (int i = 0; i < iterate; i++)
        {
            Graphics.Blit(blurTex, temp, blurMaterial);
            Graphics.Blit(temp, blurTex, blurMaterial);
        }
        RenderTexture.ReleaseTemporary(temp);
    }
    
    private void RenderComposite(RenderTexture source, RenderTexture destination)
    {
        compositeMaterial.SetTexture("_MainTex", source);
        compositeMaterial.SetTexture("_StencilTex", stencilTex);
        compositeMaterial.SetTexture("_BlurTex", blurTex);
        compositeMaterial.SetFloat("_OutlineStrength", outlineStrength);
        Graphics.Blit(source, destination, compositeMaterial);
        RenderTexture.ReleaseTemporary(stencilTex);
        RenderTexture.ReleaseTemporary(blurTex);
        stencilTex = null;
        blurTex = null;
    }
}
