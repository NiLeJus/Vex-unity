using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPC : IHaveCreatures
{
    [SerializeField] private _BaseCharacter _baseNPC;
    [SerializeField] private List<Creature> _creatures;

    public List<Creature> Creatures => _creatures;

    public NPC(_BaseCharacter baseCharacter)
    {
        if (baseCharacter == null)
            throw new System.ArgumentNullException(nameof(baseCharacter));

        this._baseNPC = baseCharacter;
        _creatures = ConstructCreatures(_baseNPC.Creatures);
    }

    public List<Creature> ConstructCreatures(List<_CreatureBase> creaturesBases)
    {
        List<Creature> toReturn = new List<Creature>();
        foreach (var creatureBase in creaturesBases)
        {
            if (creatureBase != null)
                toReturn.Add(new Creature(creatureBase));
        }
        return toReturn;
    }
}
