using System;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private MainEgg _mainEgg;
    [SerializeField] private EggPackView _eggPackView;

    private float _yPosition;
    private float _xOffset;
    private float _zOffset;
    private float _speed;

    private void Awake()
    {
        Validate();

        _yPosition = transform.position.y;
        _speed = 3;
        _zOffset = -4f;
    }

    private void Update()
    {
        Vector3 targetPosition = new Vector3(_xOffset, _yPosition, _mainEgg.transform.position.z + _zOffset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _speed);
    }

    private void OnEnable() => _eggPackView.PuttingDone += ChangePosition;

    private void OnDisable() => _eggPackView.PuttingDone -= ChangePosition;
    private void ChangePosition()
    {
        float xOffsetNew = -0.9f;

        _xOffset = xOffsetNew;
    }

    private void Validate()
    {
        if (_mainEgg == null)
            throw new NullReferenceException(nameof(_mainEgg));

        if (_eggPackView == null)
            throw new NullReferenceException(nameof(_eggPackView));
    }
}
