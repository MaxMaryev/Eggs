using System;
using UnityEngine;

[RequireComponent(typeof(EggPack), typeof(EggPackView))]
public class EggPackPresenter : MonoBehaviour
{
    [SerializeField] private MoneyCreater _moneyCreater;

    private EggPack _eggPack;
    private EggPackView _eggPackView;

    public event Action<int> AllDone;

    private void Awake()
    {
        _eggPack = GetComponent<EggPack>();
        _eggPackView = GetComponent<EggPackView>();
    }

    private void Start()
    {
        _moneyCreater.Init(this);
    }

    private void OnEnable()
    {
        _eggPackView.Opened += FinalPutting;
    }

    private void OnDisable()
    {
        _eggPackView.Opened += FinalPutting;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Egg egg))
        {
            if (_eggPack.EggsCount < 12)
                _eggPack.Pack(egg);
        }

        if (other.TryGetComponent(out Distributor distributor))
        {
            AllDone?.Invoke(_eggPack.EggsCount);
        }
    }

    private void FinalPutting()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
}
