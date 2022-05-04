using System;
using UnityEngine;

public class DeactivatedEgg : MonoBehaviour
{
    [SerializeField] private Egg _eggPrefab;
    [SerializeField] private Chain _chain;

    private Transform _container;
    private bool isAlreadyTriggered;

    public event Action<Egg> Activated;

    private void Awake()
    {
        Validate();

        _container = _chain.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAlreadyTriggered)
            return;

        if (other.TryGetComponent(out Egg egg) || other.TryGetComponent(out MainEgg mainEgg))
        {
            isAlreadyTriggered = true;
            Activate();
        }
    }

    private void Validate()
    {
        if (_eggPrefab == null)
            throw new NullReferenceException(nameof(_eggPrefab));

        if (_chain == null)
            throw new NullReferenceException(nameof(_chain));
    }

    private void Activate()
    {
        Egg newEgg = Instantiate(_eggPrefab, transform.position, Quaternion.identity, _container);
        Activated?.Invoke(newEgg);
        gameObject.SetActive(false);
    }
}
