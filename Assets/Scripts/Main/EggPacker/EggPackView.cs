using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(EggPack))]
[RequireComponent(typeof(BoxCollider))]
public class EggPackView : MonoBehaviour
{
    [SerializeField] private Transform _endPosition;
    [SerializeField] private MainEggPresenter _mainEggPresenter;

    private Animator _animator;
    private EggPack _eggPack;
    private Rigidbody _rigidbody;

    public event Action Removed;
    public event Action PuttingDone;
    public event Action Opened;

    public bool IsClosed { get; private set; }

    private void Awake()
    {
        Validate();

        _animator = GetComponent<Animator>();
        _eggPack = GetComponent<EggPack>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        const int First = 1;

        if(_eggPack.PackNumber != First)
        {
            StartCoroutine(MoveToStartPosition());
            StartCoroutine(Open());
        }

        _mainEggPresenter.Init(this);
    }

    public IEnumerator Close()
    {
        PuttingDone?.Invoke();

        int delay = 1;
        yield return new WaitForSeconds(delay);

        const string PackClose = "PackClose";
        _animator.Play(PackClose);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0).Length);

        IsClosed = true;
    }

    public IEnumerator RemovePack()
    {
        yield return new WaitUntil(() => IsClosed == true);
        _eggPack.DisableEggsInside();

        float speed = 15;

        while (transform.position.z < _endPosition.position.z)
        {
            GetComponent<Rigidbody>().velocity = Vector3.forward * speed;
            yield return null;
        }

        _rigidbody.velocity = Vector3.zero;
        Removed?.Invoke();

        DisableColliders();
    }

    IEnumerator MoveToStartPosition()
    {
        float delay = 1.7f;
        yield return new WaitForSeconds(delay);

        float yAxisTargetPosition = 1;
        float moveduration = 0.4f;
        transform.DOMoveY(yAxisTargetPosition, moveduration);
    }

    IEnumerator Open()
    {
        float delay = 1.5f;
        yield return new WaitForSeconds(delay);

        const string PackOpen = "PackOpen";
        _animator.Play(PackOpen);

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0).Length);
        Opened?.Invoke();
    }

    private void Validate()
    {
        if (_endPosition == null)
            throw new NullReferenceException(nameof(_endPosition));

        if (_mainEggPresenter == null)
            throw new NullReferenceException(nameof(_mainEggPresenter));
    }

    private void DisableColliders()
    {
        MeshCollider meshCollider = GetComponentInChildren<MeshCollider>();

        if (meshCollider == null)
            throw new NullReferenceException(nameof(meshCollider));

        GetComponentInChildren<MeshCollider>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }
}
