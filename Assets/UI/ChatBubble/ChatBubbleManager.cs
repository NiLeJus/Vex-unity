using UnityEngine;

public class ChatBubbleManager : MonoBehaviour
{
    // Singleton
    public static ChatBubbleManager i { get; private set; }
    public void Say(Transform parent)
    {
        ChatBubble.Create(parent, parent.transform.position, "Bernard");
    }
    private void Awake()
    {
        if (i != null && i != this)
        {
            Destroy(gameObject);
            return;
        }
        i = this;
    }
}
