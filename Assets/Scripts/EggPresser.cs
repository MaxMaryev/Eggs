using UnityEngine;

public class EggPresser : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Egg egg))
            egg.Break();
    }
}
