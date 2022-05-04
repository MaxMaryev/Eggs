using System;
using UnityEngine;

public class EggPackAdder : MonoBehaviour
{
    [SerializeField] EggPack _newPack;
    [SerializeField] EggPack _previousPack;

    private void Awake()
    {
        Validate();    
    }

    private void OnEnable()
    {
        _previousPack.GetComponent<EggPackView>().Removed += Create;
    }

    private void OnDisable()
    {
        _previousPack.GetComponent<EggPackView>().Removed -= Create;
    }

    private void Create()
    {
        _newPack.gameObject.SetActive(true);
    }

    private void Validate()
    {
        if (_newPack == null)
            throw new NullReferenceException(nameof(_newPack));

        if(_previousPack == null)
            throw new NullReferenceException(nameof(_previousPack));
    }
}
