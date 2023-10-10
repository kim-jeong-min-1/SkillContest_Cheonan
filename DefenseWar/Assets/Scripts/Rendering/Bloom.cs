using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class Bloom : MonoBehaviour
{
    public int inter = 3;
    public float intensity = 0.4f;

    public Shader shader;
    private Material material;

    private RenderTexture[] textures = new RenderTexture[16];

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material == null)
        {
            material = new Material(shader);
            material.hideFlags = HideFlags.HideAndDontSave;
        }
        material.SetFloat("_Intensity", Mathf.GammaToLinearSpace(intensity));

        int width = source.width / 2;
        int height = source.height / 2;

        RenderTexture des = textures[0] = RenderTexture.GetTemporary(width, height, 0, source.format);
        Graphics.Blit(source, des, material, 0);
        RenderTexture cur = des;

        int i;

        for (i = 1; i < inter; i++)
        {
            width /= 2;
            height /= 2;
            if (height < 2) break;

            des = textures[i] = RenderTexture.GetTemporary(width, height, 0, source.format);
            Graphics.Blit(cur, des, material, 1);
            cur = des;
        }

        for (i -= 2; i >= 0; i--)
        {
            des = textures[i];
            textures[i] = null;
            Graphics.Blit(cur, des, material, 2);
            RenderTexture.ReleaseTemporary(cur);
            cur = des;
        }

        material.SetTexture("_SourceTex", source);

        Graphics.Blit(cur, destination, material, 3);
        RenderTexture.ReleaseTemporary(cur);
    }

}
