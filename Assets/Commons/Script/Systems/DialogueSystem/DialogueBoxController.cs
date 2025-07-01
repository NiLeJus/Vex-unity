using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Transform choicesContainer;
    [SerializeField] private GameObject pfDialogueActionButton;

    private DialogueTree currentTree;
    private int currentSection;
    private NPCController currentNPCController; // Référence au NPC actuel

    public void StartDialogue(DialogueTree tree, NPCController npcController)
    {
        currentTree = tree;
        currentNPCController = npcController;
        currentSection = 0;
        ShowSection();
    }

    void ShowSection()
    {
        var section = currentTree.sections[currentSection];
        dialogueText.text = (section.dialogueLines != null && section.dialogueLines.Length > 0) ? section.dialogueLines[0] : "";
        ShowChoices(section.branchPoint);
    }

    void ShowChoices(BranchPoint branch)
    {
        foreach (Transform child in choicesContainer)
            Destroy(child.gameObject);

        dialogueText.text = branch.question;

        for (int i = 0; i < branch.answers.Length; i++)
        {
            int index = i;
            Answer answer = branch.answers[index];
            SpawnDialogueButton(
                answer.answerLabel,
                () => OnChoiceSelected(answer)
            );
        }

        choicesContainer.gameObject.SetActive(true);
    }

    void SpawnDialogueButton(string label, UnityEngine.Events.UnityAction onClick)
    {
        GameObject btnGO = Instantiate(pfDialogueActionButton, choicesContainer);
        DialogueActionButton dialogueAction = btnGO.GetComponent<DialogueActionButton>();

        if (dialogueAction != null)
        {
            dialogueAction.Setup(label, onClick);
        }
        else
        {
            Button btn = btnGO.GetComponent<Button>();
            TextMeshProUGUI btnText = btnGO.GetComponentInChildren<TextMeshProUGUI>();
            if (btnText != null) btnText.text = label;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(onClick);
        }
    }

    void OnChoiceSelected(Answer answer)
    {
        switch (answer.eventType)
        {
            case DialogueEventType.TriggerCombat:
                if (currentNPCController != null)
                    GameManager.I.SetBattleBetween(currentNPCController.NPC);
                break;

            case DialogueEventType.OpenShop:
                //if (currentNPCController != null)
                //ShopManager.Instance.OpenShop(currentNPCController.Inventory);
                break;

            case DialogueEventType.GiveItem:
                //InventoryManager.I.AddItem(answer.eventParameter);
                break;
        }

        if (answer.nextSectionIndex >= 0)
        {
            currentSection = answer.nextSectionIndex;
            ShowSection();
        }
        else
        {
            DialogueHandler.I.EndDialogue();
            currentNPCController = null; // Reset la référence
        }
    }
}
