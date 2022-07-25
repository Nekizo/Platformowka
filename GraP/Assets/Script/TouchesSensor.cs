using UnityEngine;

public class TouchesSensor : MonoBehaviour
{
    public event System.Action<Collider> Collision;
    private void OnTriggerEnter(Collider other)
    {
        Collision.Invoke(other);
    }
}
