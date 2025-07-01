using UnityEngine;
using UnityEngine.UI;

public class CreaturePortraitHandler : MonoBehaviour
{
    [Header("Références")]
    public Camera portraitCamera;
    public RawImage portraitImage;     // L'UI RawImage qui affichera le portrait
    public int textureWidth = 256;
    public int textureHeight = 256;

    private RenderTexture renderTexture;

    void Awake()
    {
        // Crée dynamiquement un RenderTexture unique pour cette instance
        renderTexture = new RenderTexture(textureWidth, textureHeight, 16, RenderTextureFormat.ARGB32);
        renderTexture.Create();

        // Assigne le RenderTexture à la caméra
        portraitCamera.targetTexture = renderTexture;

        // Affiche le RenderTexture dans l'UI
        if (portraitImage != null)
            portraitImage.texture = renderTexture;
    }

    void OnDestroy()
    {
        // Nettoyage pour éviter les fuites mémoire
        if (renderTexture != null)
        {
            renderTexture.Release();
            Destroy(renderTexture);
        }
    }
}
