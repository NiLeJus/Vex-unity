using System.Collections.Generic;
using UnityEngine;

public interface ICanInteract
{

    void OnInteract();
}

public interface ICanTalk
{
    System.Collections.Generic.List<DialogueTree> DialogueTree { get; }
    void OnInteract();
    void Talk();
}

public interface ICanFight
{
    System.Collections.Generic.List<_CreatureBase> CreaturesDatas { get; }
    void Fight();
}

public interface IHaveCreatures
{
    List<Creature> Creatures { get; }
    List<Creature> ConstructCreatures(List<_CreatureBase> creatureBases);
}


public interface IHaveCreaturesData
{
    [SerializeField] public List<_CreatureBase> CreaturesDatas { get; }
}
