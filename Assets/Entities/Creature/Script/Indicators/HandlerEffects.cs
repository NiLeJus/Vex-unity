using System.Collections.Generic;

[System.Serializable]
/// <summary>
/// Abstract Indicator Logic
/// Ressource => https://www.notion.so/Indicateur-Indicators-1ac8661f58da8138a11fea8bd4740aeb?pvs=4
/// </summary>
public class HandlerEffects
{

    private HandlerStats handlerStats;

    #region Constructor //

    public HandlerEffects()
    {
        //handlerStats = _handlerStats;
    }
    #endregion

    private List<Effect> activeEffects = new List<Effect>();

    public void ProcessEffects()
    {

        //EffectContext(Creature target);

        //foreach (IEffect effect in activeEffects)
        //{
        //    effect.Trigger();
        //}
    }


}
