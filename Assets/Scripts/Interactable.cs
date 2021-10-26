using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    //for visualizing and debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
