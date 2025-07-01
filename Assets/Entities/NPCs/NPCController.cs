using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, ICanInteract
{
    [SerializeField] private _BaseCharacter _baseCharacter;
    public _BaseCharacter BaseCharacter => _baseCharacter;


    [SerializeField] private NPC npc;
    public NPC NPC => npc;

    [SerializeField] public List<_CreatureBase> CreaturesBases => _baseCharacter.Creatures;


    [SerializeField]
    public List<DialogueTree> DialogueTrees => _baseCharacter.DialogueTrees;

    [SerializeField] private List<Creature> creatures { get; set; }
    public List<Creature> Creatures => creatures;

    public void Awake()
    {
        npc = new NPC(_baseCharacter);
    }
    public void Fight()
    { }

    public void OnInteract()
    {
        if (DialogueTrees == null)
        {
            Debug.LogWarning("NO dialogue tree");
        }
        else
        {
            DialogueHandler.I.StartDialogue(DialogueTrees[0], this);
        }
    }

    public _BaseCharacter ReturnCharacterBase()
    {
        return _baseCharacter;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
