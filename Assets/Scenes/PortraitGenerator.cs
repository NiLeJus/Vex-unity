public class PortraitGenerator
{
    //public static Texture2D GeneratePortrait(GameObject prefab, int width = 256, int height = 256)
    //{
    //    GameObject instance = InstantiatePrefab(prefab);
    //    Camera portraitCamera = FindPortraitCamera(instance);

    //    if (portraitCamera == null)
    //    {
    //        Debug.LogError("Aucune caméra trouvée dans le prefab !");
    //        Cleanup(instance, null);
    //        return null;
    //    }

    //    RenderTexture rt = CreateRenderTexture(width, height);
    //    Texture2D tex = CaptureCameraToTexture(portraitCamera, rt, width, height);

    //    Cleanup(instance, rt);
    //    return tex;
    //}

    //private static GameObject InstantiatePrefab(GameObject prefab)
    //{
    //    return GameObject.Instantiate(prefab);
    //}

    //private static Camera FindPortraitCamera(GameObject instance)
    //{
    //    return instance.GetComponentInChildren<Camera>();
    //}

    //private static RenderTexture CreateRenderTexture(int width, int height)
    //{
    //    RenderTexture rt = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
    //    rt.Create();
    //    return rt;
    //}

    //private static Texture2D CaptureCameraToTexture(Camera camera, RenderTexture rt, int width, int height)
    //{
    //    camera.targetTexture = rt;
    //    RenderTexture prev = RenderTexture.active;
    //    RenderTexture.active = rt;

    //    camera.Render();

    //    Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
    //    tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
    //    tex.Apply();

    //    camera.targetTexture = null;
    //    RenderTexture.active = prev;

    //    return tex;
    //}

    //private static void Cleanup(GameObject instance, RenderTexture rt)
    //{
    //    if (rt != null)
    //    {
    //        rt.Release();
    //        GameObject.Destroy(rt);
    //    }
    //    if (instance != null)
    //    {
    //        GameObject.Destroy(instance);
    //    }
    //}
}
