using UnityEngine;

public enum DialogueEventType
{
    None,
    TriggerCombat,
    OpenShop,
    GiveItem,
    Custom
}

[System.Serializable]
public struct DialogueSection
{
    [TextArea]
    public string[] dialogueLines;
    public bool endsConversation;
    public BranchPoint branchPoint;
}

[System.Serializable]
public struct BranchPoint
{
    [TextArea]
    public string question;
    public Answer[] answers;
}

[System.Serializable]
public struct Answer
{
    public string answerLabel;
    public int nextSectionIndex; // -1 si fin de dialogue
    public DialogueEventType eventType;
    public string eventParameter; // ID d’item, nom de shop, etc.
}
