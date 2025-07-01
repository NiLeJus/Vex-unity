using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

public class AnimatorStateTester : MonoBehaviour
{
    [Header("Configuration")]
    public Animator animator;
    public KeyCode nextStateKey = KeyCode.N;
    public KeyCode previousStateKey = KeyCode.P;
    public KeyCode toggleAutoTestKey = KeyCode.T;
    public float autoTestDuration = 3f;

    [Header("État actuel")]
    public int currentStateIndex = 0;
    public string currentStateName = "";
    public bool autoTestMode = false;

    private List<string> stateNames = new List<string>();
    private float autoTestTimer = 0f;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Aucun Animator trouvé sur cet objet !");
            return;
        }

        CollectAllStates();

        if (stateNames.Count > 0)
        {
            PlayCurrentState();
        }
    }

    void Update()
    {
        HandleInput();
        HandleAutoTest();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(nextStateKey))
        {
            NextState();
        }

        if (Input.GetKeyDown(previousStateKey))
        {
            PreviousState();
        }

        if (Input.GetKeyDown(toggleAutoTestKey))
        {
            ToggleAutoTest();
        }
    }

    void HandleAutoTest()
    {
        if (autoTestMode && stateNames.Count > 1)
        {
            autoTestTimer += Time.deltaTime;
            if (autoTestTimer >= autoTestDuration)
            {
                NextState();
                autoTestTimer = 0f;
            }
        }
    }

    void CollectAllStates()
    {
        stateNames.Clear();

#if UNITY_EDITOR
        // En mode éditeur, on peut accéder à tous les états[4][9]
        AnimatorController ac = animator.runtimeAnimatorController as AnimatorController;
        if (ac != null)
        {
            foreach (AnimatorControllerLayer layer in ac.layers)
            {
                foreach (ChildAnimatorState state in layer.stateMachine.states)
                {
                    stateNames.Add(state.state.name);
                }
            }
        }
#else
        // En runtime, on utilise les clips d'animation disponibles[5][8]
        RuntimeAnimatorController rac = animator.runtimeAnimatorController;
        if (rac != null)
        {
            foreach (AnimationClip clip in rac.animationClips)
            {
                if (!stateNames.Contains(clip.name))
                {
                    stateNames.Add(clip.name);
                }
            }
        }
#endif

        Debug.Log($"États trouvés : {stateNames.Count}");
        foreach (string stateName in stateNames)
        {
            Debug.Log($"- {stateName}");
        }
    }

    public void NextState()
    {
        if (stateNames.Count == 0) return;

        currentStateIndex = (currentStateIndex + 1) % stateNames.Count;
        PlayCurrentState();
    }

    public void PreviousState()
    {
        if (stateNames.Count == 0) return;

        currentStateIndex = currentStateIndex - 1;
        if (currentStateIndex < 0)
            currentStateIndex = stateNames.Count - 1;

        PlayCurrentState();
    }

    void PlayCurrentState()
    {
        if (stateNames.Count == 0) return;

        currentStateName = stateNames[currentStateIndex];

        // Jouer l'état spécifique[6][11]
        animator.Play(currentStateName);

        Debug.Log($"État actuel : {currentStateName} ({currentStateIndex + 1}/{stateNames.Count})");
    }

    public void ToggleAutoTest()
    {
        autoTestMode = !autoTestMode;
        autoTestTimer = 0f;

        Debug.Log($"Mode test automatique : {(autoTestMode ? "Activé" : "Désactivé")}");
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 200));
        GUILayout.BeginVertical("box");

        GUILayout.Label("Testeur d'États Animator", EditorStyles.boldLabel);
        GUILayout.Space(5);

        if (stateNames.Count > 0)
        {
            GUILayout.Label($"État : {currentStateName}");
            GUILayout.Label($"Index : {currentStateIndex + 1}/{stateNames.Count}");

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("← Précédent"))
            {
                PreviousState();
            }
            if (GUILayout.Button("Suivant →"))
            {
                NextState();
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            string autoTestText = autoTestMode ? "Arrêter Auto-Test" : "Démarrer Auto-Test";
            if (GUILayout.Button(autoTestText))
            {
                ToggleAutoTest();
            }

            if (autoTestMode)
            {
                float progress = autoTestTimer / autoTestDuration;
                GUILayout.Label($"Prochain état dans : {(autoTestDuration - autoTestTimer):F1}s");
                GUILayout.HorizontalSlider(progress, 0f, 1f);
            }
        }
        else
        {
            GUILayout.Label("Aucun état trouvé !");
        }

        GUILayout.Space(10);
        GUILayout.Label("Contrôles :");
        GUILayout.Label($"N - État suivant");
        GUILayout.Label($"P - État précédent");
        GUILayout.Label($"T - Toggle auto-test");

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
