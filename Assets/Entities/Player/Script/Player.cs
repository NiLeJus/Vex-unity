using System.Collections.Generic;
[ES3Serializable]

public class Player : IHaveCreatures
{
    public _BasePlayer _basePlayer { get; set; }
    public string Name => _basePlayer.Name;
    //public int Level => Base.tamerLevel;
    public List<_CreatureBase> CreaturesBases => _basePlayer.Creatures;

    public int Money { get; set; }
    public int Level { get; set; }

    private List<Creature> creatures { get; set; }
    public List<Creature> Creatures => creatures;


    public List<_BaseItem> Items => _basePlayer.Items;


    public Player(_BasePlayer basePlayer)
    {
        if (basePlayer == null)
            throw new System.ArgumentNullException(nameof(basePlayer));

        this._basePlayer = basePlayer;

        this.Money = 0;
        this.Level = 0;

        creatures = ConstructCreatures(basePlayer.Creatures);
    }

    #region Builders Methods //
    public List<Creature> ConstructCreatures(List<_CreatureBase> creaturesBases)
    {
        List<Creature> toReturn = new List<Creature>();
        foreach (_CreatureBase creatureBase in creaturesBases)
        {
            toReturn.Add(new Creature(creatureBase));
        }
        return toReturn;
    }

    #endregion

    public void Fight()
    { }



    //public Player PlayerFromData(_BasePlayer _Base)
    //{
    //    Base = _Base;
    //}


    //public void Save(ref PlayerSaveData data)
    //{
    //    data.money = Player.Money;
    //    //data.Creatures = Player;
    //    data.PlayerSaveData = Player.Save();

    //}
    //public void Load(PlayerSaveData data)
    //{
    //}

}

//[System.Serializable]
//public struct PlayerSaveData
//{
//    public int money;
//    //public List<Creature> Creatures;
//}

