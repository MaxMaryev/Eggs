using System;
using UnityEngine;


public class MoneyCreater : MonoBehaviour
{
    [SerializeField] Money _money;
    [SerializeField] Transform _container;
    [SerializeField] EggPackPresenter _eggPackPresenter;

    private float _xAxisSpread;
    private AudioSource _audioSource;

    private void Awake()
    {
        Validate();

        _audioSource = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        _eggPackPresenter.AllDone -= Create;
    }

    public void Init(EggPackPresenter eggPackPresenter)
    {
        _eggPackPresenter = eggPackPresenter;
        _eggPackPresenter.AllDone += Create;
    }

    public int GetCount(int count)
    {
        if(count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        int countMultipier = 2;
        return count * countMultipier;
    }

    private void Create(int count)
    {
        int xMax = -80;
        int yAxis = 40;

        for (int i = 0; i < GetCount(count); i++)
        {
            _xAxisSpread = UnityEngine.Random.Range(0, xMax);
            Instantiate(_money, transform.position + new Vector3(_xAxisSpread, yAxis, 0), Quaternion.identity, _container);
        }

        _audioSource.PlayOneShot(_audioSource.clip);
    }

    private void Validate()
    {
        if (_money == null)
            throw new NullReferenceException(nameof(_money));

        if (_container == null)
            throw new NullReferenceException(nameof(_container));

        if (_eggPackPresenter == null)
            throw new NullReferenceException(nameof(_eggPackPresenter));
    }
}
