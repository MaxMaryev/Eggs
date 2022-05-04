using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MainEgg))]
public class MainEggPresenter : MonoBehaviour
{   
    private InputActions _inputActions;
    private EggPackView _eggPackView;
    private MainEgg _mainEgg;

    public event Action<float> MouseDragged;

    public float DeltaX { get; private set; }

    private void Awake()
    {
        _mainEgg = GetComponent<MainEgg>();
        _inputActions = new InputActions();
        _inputActions.Egg.Move.performed += ctx => SetDeltaX();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Finish finish))
        {
            _mainEgg.SetSpeed(0);
        }

        if (other.TryGetComponent(out EggPack eggPack))
            StartMoveForward();
    }

    public void Init(EggPackView eggPackView)
    {
        _eggPackView = eggPackView;

        _eggPackView.PuttingDone += _inputActions.Disable;
    }

    private void SetDeltaX()
    {
        DeltaX = Mouse.current.delta.x.ReadValue();
        MouseDragged?.Invoke(DeltaX);
    }

    private void StartMoveForward()
    {
        _mainEgg.SetSpeed(_mainEgg.DefaultSpeed);
    }
}
