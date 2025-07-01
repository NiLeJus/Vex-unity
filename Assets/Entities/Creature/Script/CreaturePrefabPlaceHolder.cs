using UnityEngine;


/// <summary>
/// Script for the Creature Placeholder to kill it self right on start cause not necessary
/// </summary>
public class CreaturePrefabPlaceHolder : MonoBehaviour
{
    void Awake()
    {
        Destroy(gameObject);
    }


}
