using UnityEngine;

public class RagdollOnOff : MonoBehaviour
{
    public Collider mainCollider;
    public GameObject CharacterRig;
    public Animator CharacterAnimator;

    private void Start()
    {
        rb = CharacterRig.GetComponent<Rigidbody>();
        playerMovement = CharacterRig.GetComponent<PlayerMovement>();
        GetRagdollBits();
        RagdollModeOff();
    }

    private void Update()
    {
        
    }

    Collider[] ragdollColliders;
    Rigidbody[] limbsRigidBodies;
    private Rigidbody rb;
    private PlayerMovement playerMovement;
    private GameObject prop;
    private Rigidbody propRigidBody;
    private Collider propCollider;
    
    void GetRagdollBits()
    {
        ragdollColliders = CharacterRig.GetComponentsInChildren<Collider>();
        limbsRigidBodies = CharacterRig.GetComponentsInChildren<Rigidbody>();
        
    }

    public void RagdollModeOn()
    {
        Vector3 playerVelocity = playerMovement.GetVelocity();
        CharacterAnimator.enabled = false;
        
        foreach (var coll in ragdollColliders)
        {
            coll.enabled = true;
        }

        foreach (var limbRb in limbsRigidBodies)
        {
            limbRb.isKinematic = false;
            limbRb.linearVelocity = playerVelocity;
        }

        mainCollider.enabled = false;
        rb.isKinematic = true;
    }

    private void RagdollModeOff()
    {
        foreach (var coll in ragdollColliders)
        {
            coll.enabled = false;
        }

        foreach (var rigid in limbsRigidBodies)
        {
            rigid.isKinematic = true;
        }
        
        CharacterAnimator.enabled = true;
        mainCollider.enabled = true;
        rb.isKinematic = false;
    }
}
