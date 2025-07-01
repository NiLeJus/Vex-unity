using System;
using MoreMountains.Feedbacks;
using UnityEngine;

[RequireComponent(typeof(CreatureBattler))] //Garanti presence du composent 
public class CreatureController : MonoBehaviour
{

    public Animator creatureAnimator;

    public Creature Creature { get; private set; }
    public GameObject creatureVisual;
    public event Action<int> OnHealthChanged;

    [SerializeField] MMFeedbacks DamageFeedback;
    [SerializeField] FloatingHealthBar healthBar;


    #region Setup //

    public void InjectCreature(Creature creature)
    {
        Creature = creature;
        Debug.Log($"Creature injected : {Creature}");
        creatureVisual = Creature.GetVisualPrefab();
        //InitializeHealthBar();
    }
    public void InitializeHealthBar()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        UpdateHealthBar();
    }
    private void InitializeAnimator(GameObject visualInstance)
    {
        // Récupère l'Animator du visuel instancié
        creatureAnimator = visualInstance.GetComponent<Animator>();

        if (creatureAnimator == null)
        {
            Debug.LogWarning($"Aucun Animator trouvé sur {visualInstance.name}");
        }
        else
        {
            Debug.Log($"Animator initialisé pour {Creature.Name}");
        }
    }

    #endregion


    #region Update // 

    #endregion

    public void UpdateHealthBar()
    {
        healthBar.UpdateHealthBar(Creature.Resources.Health.Value, Creature.Resources.Health.MaxValue);
    }
    public static CreatureController InstantiateCreature(Creature creature, Transform spawnPoint, GameObject prefab)
    {
        GameObject instance = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        CreatureController controller = instance.GetComponent<CreatureController>();
        controller.InjectCreature(creature);
        controller.SpawnCreatureVisual();

        return controller;
    }
    public void Update()
    {
        UpdateHealthBar();
        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage(10);
        }
    }
    private void InitializeUI()
    {

    }
    public void SpawnCreatureVisual()
    {
        if (creatureVisual != null)
        {
            GameObject visualInstance = Instantiate(creatureVisual, transform);
            InitializeAnimator(visualInstance);
        }
    }
    public CreatureBattler GetCreatureBattler()
    {
        return GetComponent<CreatureBattler>();
    }
    void Start()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();

        if (Creature == null)
        {
            Debug.LogWarning("No creature injected");
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Take damage here");
        DamageFeedback.PlayFeedbacks(transform.position, damage);

        if (Creature.Resources.Health.Value <= 0)
        {
            TriggerDeath();
        }
    }

    public void SetSpeed(float speed)
    {
        if (creatureAnimator != null)
            creatureAnimator.SetFloat("speed", speed);
    }

    public void TriggerAttack()
    {
        if (creatureAnimator != null)
            creatureAnimator.SetTrigger("onAttack");
    }

    public void TriggerDeath()
    {
        if (creatureAnimator != null)
            creatureAnimator.SetTrigger("onDeath");
    }

}
