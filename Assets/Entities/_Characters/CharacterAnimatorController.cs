using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour
{

    public Animator characterAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void AnimateJump()
    {
        characterAnimator.SetTrigger("JumbTrigger");
    }

    public void UpdateSpeed(float speed)
    {
        characterAnimator.SetFloat("Speed", speed);
    }


}
