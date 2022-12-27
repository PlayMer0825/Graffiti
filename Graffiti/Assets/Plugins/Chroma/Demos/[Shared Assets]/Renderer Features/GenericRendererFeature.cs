using UnityEngine;
using UnityEngine.Rendering.Universal;

// TODO: Remove for URP 13.
// https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@13.1/manual/upgrade-guide-2022-1.html
#pragma warning disable CS0618

namespace Dustyroom {
public class GenericRendererFeature : ScriptableRendererFeature {
    public Material material;
    public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingTransparents;

    private BlitTexturePass _blitTexturePass;

    public override void Create() {
        _blitTexturePass = new BlitTexturePass() { renderPassEvent = renderPassEvent };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) {
#if UNITY_EDITOR
        if (renderingData.cameraData.isPreviewCamera) {
            return;
        }
#endif

        _blitTexturePass.Setup(material, useDepth: true, useNormals: true, useColor: true);
        renderer.EnqueuePass(_blitTexturePass);
    }
}
}