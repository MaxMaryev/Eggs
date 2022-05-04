using System;
using UnityEngine;

[RequireComponent(typeof(EggPresenter))]
public class Egg : MonoBehaviour
{
    private EggPresenter _eggPresenter;
    private Transform _previousEgg;
    private int _distanceToMainEgg;
    private bool _isPacking;

    public event Action<Egg> Broke;
    public event Action<Vector3> Jumped;

    private void Awake()
    {
        _eggPresenter = GetComponent<EggPresenter>();
    }

    private void Start()
    {
        _previousEgg = _eggPresenter.GetPreviousEggPosition();
        _distanceToMainEgg = _eggPresenter.GetDistanceToMain();
    }

    private void Update()
    {
        if (_isPacking)
            return;

        Move();
    }

    public void Disable() => gameObject.SetActive(false);

    public void JumpToPack(Vector3 targetPosition)
    {
        _isPacking = true;
        Jumped?.Invoke(targetPosition);
    }

    public void Break()
    {
        Broke?.Invoke(this);
    }

    private void Move()
    {
        Vector3 mainEggPosition = _eggPresenter.MainEgg.transform.position;
        Vector3 targetPosition = new Vector3(_previousEgg.position.x, transform.position.y, mainEggPosition.z + _distanceToMainEgg);

        if (IsInLine())
            MoveInLine();
        else
            MoveToEndOfChain();

        void MoveInLine()
        {
            float lerpStep = 40;
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpStep * Time.deltaTime);
        }

        void MoveToEndOfChain()
        {
            float moveTowardsStep = 20;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveTowardsStep * Time.deltaTime);
        }
    }

    private bool IsInLine()
    {
        if (transform.position.z > _previousEgg.position.z)
            return true;
        else
            return false;
    }
}

