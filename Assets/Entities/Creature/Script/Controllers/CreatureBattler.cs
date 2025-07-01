using System;
using UnityEngine;

/// <summary>
/// Use for creating new creature Data set in the Unity Editor > inspector
/// Right click in project tab => right click => create => creature => new creature  
/// </summary>
public class CreatureBattler : MonoBehaviour
{
    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private Action currentSlideCompleteCallback;
    public float speed = 10;
    public DamageFeedbackManager damageFeedback;

    public CreatureController creatureController;

    //Internal State
    private State state;
    private enum State { Idle, Moving, Busy, Back }

    [SerializeField] private float slideSpeed = 3f;

    public void SpawnCreature(Vector3 position)
    {

    }

    void Awake()
    {
        creatureController = GetComponent<CreatureController>();
        initialPosition = transform.position;
        state = State.Idle;
        InitFeedbackManager();
    }

    public void InitFeedbackManager()
    {
        GameObject go = GameObject.Find("--SYSTEM");
        damageFeedback = go != null ? go.GetComponent<DamageFeedbackManager>() : null;
    }


    void Update()
    {
        switch (state)
        {
            case State.Moving:

                creatureController.SetSpeed(1);

                transform.position = Vector3.MoveTowards(
                     GetPosition(),
                     targetPosition,
                     speed * Time.deltaTime
                     );
                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    creatureController.SetSpeed(0);
                    state = State.Busy;
                    currentSlideCompleteCallback();
                }
                break;

            case State.Back:
                creatureController.SetSpeed(1);
                transform.position = Vector3.MoveTowards(
                 GetPosition(),
            initialPosition,
             speed * Time.deltaTime
    );
                if (Vector3.Distance(transform.position, initialPosition) < 0.1f)
                {
                    state = State.Idle;
                    currentSlideCompleteCallback();
                }
                break;
        }
    }

    private void MoveTowards(Vector3 position)
    {
        transform.position += (position - transform.position).normalized
                            * slideSpeed * Time.deltaTime;
    }

    public void Attack(CreatureBattler target, Move move, Action onAttackComplete)
    {
        // Récupérer CreatureController à chaque appel
        CreatureController controller = GetComponent<CreatureController>();
        if (controller == null || controller.Creature == null)
        {
            Debug.LogError("CreatureController ou Creature manquant !", this);
            return;
        }

        creatureController.TriggerAttack();
        float attackResult = controller.Creature.Attack(move);

        // Déplacement vers la cible
        SlideToPosition(target.GetPosition(), () =>
        {


            creatureController.TriggerAttack();
            // Appliquer les dégâts
            target.Damage(attackResult);

            // Retour à la position initiale
            SlideToPosition(initialPosition, () =>
            {
                onAttackComplete?.Invoke();
            });

            damageFeedback.TriggerDamageFeedback(target.transform, attackResult);

        });
        creatureController.SetSpeed(0);

    }

    public void Damage(float damages)
    {

        CreatureController controller = GetComponent<CreatureController>();
        if (controller != null && controller.Creature != null)
        {
            controller.Creature.Damage(damages);
        }
    }

    private void SlideToPosition(Vector3 position, Action onSlideComplete)
    {
        targetPosition = position;
        currentSlideCompleteCallback = onSlideComplete;
        state = State.Moving;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void OnAttackComplete()
    {
        state = State.Back;
    }
}
