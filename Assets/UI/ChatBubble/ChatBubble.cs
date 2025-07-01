using TMPro;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{


    [SerializeField] private Sprite happyIconSprite;
    [SerializeField] private Sprite neutralIconSprite;
    [SerializeField] private Sprite angryIconSprite;

    private SpriteRenderer backgroundSpriteRenderer;
    private SpriteRenderer iconSpriteRenderer;
    private TextMeshPro textMeshPro;


    public static void Create(Transform parent, Vector3 localPosition, string text)
    {
        Transform chatBubbleTransform = Instantiate(GameAssets.i.pfChatBubble, parent);
        chatBubbleTransform.localPosition = localPosition;
        chatBubbleTransform.GetComponent<ChatBubble>().Setup(text);
        Destroy(chatBubbleTransform.gameObject, 6f);
    }

    public enum IconType
    {
        Happy,
        Neutral,
        Angry,
    }

    private void Awake()
    {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        iconSpriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();

        Setup("Bernard");
    }

    private void Setup(string text)
    {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);

        Vector2 padding = new Vector2(7f, 3f);
        backgroundSpriteRenderer.size = textSize + padding;

        //Vector3 offset = new Vector3(-3f, 0f);
        //backgroundSpriteRenderer.transform.localPosition =
        //    new Vector3(backgroundSpriteRenderer.size.x / 2f, 0f) + offset;

        //iconSpriteRenderer.sprite = GetIconSprite(IconType.Happy);
    }

    //private Sprite GetIconSprite(IconType iconType)
    //{
    //    switch (iconType)
    //    {
    //        default:
    //        case IconType.Happy: return happyIconSprite;
    //        case IconType.Neutral: return neutralIconSprite;
    //        case IconType.Angry: return angryIconSprite;
    //    }
    //}

}
