using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Vexplorer", menuName = "Vexplorer/Create New Character")]
[System.Serializable]
public class _BaseCharacter : ScriptableObject
{
    [Header("Informations")]
    [field: SerializeField, TextArea] public string Name { get; private set; }
    [field: SerializeField, TextArea] public string Description { get; private set; }

    [Header("Inventory")]
    [SerializeField] private List<_BaseItem> items = new List<_BaseItem>();
    public List<_BaseItem> Items => items;

    [Header("Companions")]
    [SerializeField] private List<_CreatureBase> creatures = new List<_CreatureBase>();
    public List<_CreatureBase> Creatures => creatures;

    [Header("Dialogues")]
    [SerializeField] private List<DialogueTree> dialogueTrees = new List<DialogueTree>();
    public List<DialogueTree> DialogueTrees => dialogueTrees;
}
