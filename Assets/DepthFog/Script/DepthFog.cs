using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class DepthFog : MonoBehaviour
{
    private Camera m_Camera;
    private RenderTexture m_ColorBuffer;
    private RenderTexture m_DepthBuffer;
    public Material m_material;

    void Start()
    {
        m_Camera = GetComponent<Camera>();

        // カラーバッファを生成
        m_ColorBuffer = new RenderTexture(1334,750, 0);
        m_ColorBuffer.Create();

        // 深度バッファを生成
        m_DepthBuffer = new RenderTexture(1334, 750, 24, RenderTextureFormat.Depth);
        m_DepthBuffer.Create();

        m_Camera.SetTargetBuffers(m_ColorBuffer.colorBuffer, m_DepthBuffer.depthBuffer);
        AddCommand();
    }

    private void AddCommand()
    {
        // 深度バッファをセットするコマンド
        {
            CommandBuffer command = new CommandBuffer();
            command.name = "Set depth texture";

            command.SetGlobalTexture("_DepthTexture", m_DepthBuffer);

            m_Camera.AddCommandBuffer(CameraEvent.BeforeImageEffects, command);
        }
    }

    void OnPostRender()
    {
        Graphics.SetRenderTarget(null);
        // rtを画面に直接描画
        Graphics.Blit(m_ColorBuffer, m_material);
    }
}
