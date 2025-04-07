using UnityEngine;

public class Collection_dirt : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject onCollectEffect;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Touch a subject");
            Instantiate(onCollectEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }  
    }
}
