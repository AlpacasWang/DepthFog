using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Rendering;
using System.Collections.Generic;

[ExecuteInEditMode]
public class DepthFog : MonoBehaviour
{
    private Camera m_Camera;
    public Material FogMaterial;

    void Start()
    {
        m_Camera = GetComponent<Camera>();
        m_Camera.depthTextureMode  |= DepthTextureMode.DepthNormals;
    }

    [ImageEffectOpaque]
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, FogMaterial);

    }

}
