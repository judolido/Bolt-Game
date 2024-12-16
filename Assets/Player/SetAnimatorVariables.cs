using UnityEngine;

public class SetAnimatorVariables : MonoBehaviour
{
    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Finished = Animator.StringToHash("Finished");
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    public void SetRunning(bool value)
    {
        animator.SetBool(Running, value);
    }
    
    public void SetGrounded(bool value)
    {
        animator.SetBool(Grounded, value);
    }
}
