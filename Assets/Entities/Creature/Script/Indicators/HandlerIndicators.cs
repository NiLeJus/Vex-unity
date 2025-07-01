using System.Collections.Generic;
using System.Linq;

[System.Serializable]
/// <summary>
/// Abstract Indicator Logic
/// Ressource => https://www.notion.so/Indicateur-Indicators-1ac8661f58da8138a11fea8bd4740aeb?pvs=4
/// </summary>
public class HandlerIndicators
{

    private HandlerStats handlerStats;

    #region Constructor //

    public HandlerIndicators(HandlerStats _handlerStats)
    {
        handlerStats = _handlerStats;
    }
    #endregion

    //Implementation should be better
    public List<KeyValuePair<string, int>> GetIndicators(List<int> qualities, int allLevel, int qualityAmount, int rarity, int worth)
    {
        int getPow = worth / 10 + allLevel + rarity * 2 + qualityAmount / 6;

        var list = new List<KeyValuePair<string, int>>
    {
        new KeyValuePair<string, int>("Val", worth),
        new KeyValuePair<string, int>("Rar", rarity),
        new KeyValuePair<string, int>("Pow", getPow),
        new KeyValuePair<string, int>("Lvl", allLevel),
        new KeyValuePair<string, int>("Qua", qualityAmount / 6 )
    };
        return list;

    }


    //TODO 
    public int quaIndicator(List<int> qualities)
    {
        if (qualities.Count == 0) return 0;
        return (int)qualities.Average();
    }
    public int valIndicator()
    {
        return 0;
    }

    public int lvlIndicator()
    {
        return 0;

    }

    public int powIndicator()
    {
        return 0;
    }

    public int rarIndicator()
    {
        return 0;
    }
}
