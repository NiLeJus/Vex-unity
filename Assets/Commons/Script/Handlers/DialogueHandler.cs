using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    #region Singleton
    public static DialogueHandler I { get; private set; }
    private void SingletonImplementation()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
    }

    #endregion

    [SerializeField]
    private DialogueBoxController dialogueBoxController;
    private GameObject dialogueBoxGO;



    private void Awake()
    {
        SingletonImplementation();
    }
    private
    void Start()
    {
        dialogueBoxGO = dialogueBoxController.transform.gameObject;
        dialogueBoxGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Ping()
    {
        Debug.Log("Ping");
    }

    public void StartDialogue(DialogueTree dialogueTree, NPCController npcTalkingTo)
    {
        dialogueBoxGO.SetActive(true);
        dialogueBoxController.StartDialogue(dialogueTree, npcTalkingTo);
    }

    public void EndDialogue()
    {

    }
}
