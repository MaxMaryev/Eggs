using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EggPackView))]
public class EggPack : MonoBehaviour
{
    [SerializeField] private int _packNumber;
    [SerializeField] private Transform[] _slots;
    [SerializeField] private MainEggPresenter _mainEggPresenter;
    [SerializeField] private Chain _chain;

    private List<Egg> _eggs = new List<Egg>();
    private EggPackView _eggPackView;
    private int _currentSlotIndex;

    public event Action<Vector3> EggPackingStarted;
    public event Action<int> EggsCountChanged;
    public event Action PackingCompleted;

    public int PackNumber => _packNumber;
    public int EggsCount => _eggs.Count;

    private void Awake()
    {
        Validate();

        _eggPackView = GetComponent<EggPackView>();
    }

    public void DisableEggsInside()
    {
        foreach (var egg in _eggs)
            egg.Disable();
    }

    public void Pack(Egg egg)
    {
        if(egg == null)
            throw new ArgumentNullException(nameof(egg));

        _eggs.Add(egg);
        egg.JumpToPack(_slots[_currentSlotIndex].position);
        EggsCountChanged?.Invoke(EggsCount);
        _currentSlotIndex++;

        if (isAddedLastEgg())
            RemoveFullPackage();
    }

    private void RemoveFullPackage()
    {
        StartCoroutine(_eggPackView.Close());
        StartCoroutine(_eggPackView.RemovePack());
    }

    private bool isAddedLastEgg()
    {
        if (EggsCount == _slots.Length || EggsCount == _chain.GetComponentsInChildren<Egg>().Length)
            return true;
        else
            return false;
    }

    private void Validate()
    {
        if (_slots == null)
            throw new NullReferenceException(nameof(_slots));

        if (_mainEggPresenter == null)
            throw new NullReferenceException(nameof(_mainEggPresenter));

        if (_chain == null)
            throw new NullReferenceException(nameof(_chain));
    }
}
