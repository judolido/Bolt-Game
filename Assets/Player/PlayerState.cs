using UnityEngine;
using UnityEngine.Events;

public class PlayerState : MonoBehaviour
{
    public UnityEvent onDeath;
    public UnityEvent onFinish;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            onDeath.Invoke();
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            onFinish.Invoke();
        }
    }
}
