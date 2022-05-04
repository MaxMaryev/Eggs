using System;
using UnityEngine;

[RequireComponent(typeof(Egg))]
public class EggPresenter : MonoBehaviour
{
    private Egg _egg;
    private MainEgg _mainEgg;
    private Chain _chain;

    public MainEgg MainEgg => _mainEgg;

    private void Awake()
    {
        _egg = GetComponent<Egg>();
        _chain = GetComponentInParent<Chain>();
        _mainEgg = _chain.GetComponentInChildren<MainEgg>();

        Validate();
    }

    public Transform GetPreviousEggPosition()
    {
        const int First = 1;

        if (_chain.GetSequenceNumber(_egg) == First)
            return _mainEgg.transform;
        else
            return _chain.GetNextToLastEgg();
    }

    public int GetDistanceToMain() => _chain.GetSequenceNumber(_egg);

    private void Validate()
    {
        if (_mainEgg == null)
            throw new NullReferenceException(nameof(_mainEgg));

        if (_chain == null)
            throw new NullReferenceException(nameof(_chain));
    }
}
