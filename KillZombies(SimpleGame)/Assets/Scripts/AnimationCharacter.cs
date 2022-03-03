using UnityEngine;

public class AnimationCharacter : MonoBehaviour
{
    //CONSTs
    const string ANIMATOR_ATTACKING = "Attacking";
    const string ANIMATOR_RUNNING = "Running";

    //Components
    Animator myAnimator;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void Attack(bool stateAttacking)
    {
        myAnimator.SetBool(ANIMATOR_ATTACKING, stateAttacking);
    }

    public void Walk(float valueWalk)
    {
        myAnimator.SetFloat(ANIMATOR_RUNNING, valueWalk);
    }
}
