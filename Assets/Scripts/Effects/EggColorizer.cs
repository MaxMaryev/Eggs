using System;
using UnityEngine;

public class EggColorizer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EggView eggView))
        {
            eggView.ChangeMaterial();
            StartCoroutine(eggView.TemporaryIncreasingScale());
        }
    }
}
