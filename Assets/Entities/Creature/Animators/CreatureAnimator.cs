using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
#endif

public class CreatureAnimatorController : MonoBehaviour
{
    public Animator creatureAnimator;


    [Header("État actuel")]
    public int currentStateIndex = 0;
    public string currentStateName = "";
    public bool autoTestMode = false;

    private List<string> stateNames = new List<string>();
    private float autoTestTimer = 0f;

    void Start()
    {
        if (creatureAnimator == null)
            creatureAnimator = GetComponentInChildren<Animator>();

        if (creatureAnimator == null)
        {
            Debug.LogError("No animator found on this GameObject" + gameObject);
            return;
        }
    }

    void Update()
    {

    }
}
