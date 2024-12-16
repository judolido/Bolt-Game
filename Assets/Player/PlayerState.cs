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
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            onDeath.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            onFinish.Invoke();
        }
    }
}
