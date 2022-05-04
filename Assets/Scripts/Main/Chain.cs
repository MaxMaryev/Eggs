using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    [SerializeField] private List<Egg> _eggs = new List<Egg>();
    [SerializeField] private List<DeactivatedEgg> _deactivatedEggs = new List<DeactivatedEgg>();

    private void Awake()
    {
        _eggs.AddRange(GetComponentsInChildren<Egg>());
    }

    private void OnEnable()
    {
        foreach (DeactivatedEgg deactivatedEgg in _deactivatedEggs)
        {
            deactivatedEgg.Activated += Add;
        }

        foreach (Egg egg in _eggs)
        {
            egg.Broke += Remove;
        }
    }

    private void OnDisable()
    {
        foreach (DeactivatedEgg deactivatedEgg in _deactivatedEggs)
        {
            deactivatedEgg.Activated -= Add;
        }
    }

    public int GetSequenceNumber(Egg egg)
    {
        const int One = 1;
        return _eggs.FindIndex(thisEgg => thisEgg == egg) + One;
    }

    public Transform GetNextToLastEgg()
    {
        const int Two = 2;
        return _eggs[_eggs.Count - Two].transform;
    }

    public void Remove(Egg egg)
    {
        _eggs.Remove(egg);
        egg.Broke -= Remove;
    }
    private void Add(Egg egg)
    {
        _eggs.Add(egg);
        egg.Broke += Remove;
    }
}
