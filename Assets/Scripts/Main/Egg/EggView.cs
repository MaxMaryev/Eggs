using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Egg), typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class EggView : MonoBehaviour
{
    [SerializeField] private BrokenEgg _brokenEgg;
    [SerializeField] private Material _newMaterial;
    [SerializeField] private AudioClip _jumpSound;

    private Egg _egg;
    private AudioSource _audioSource;

    private void Awake()
    {
        Validate();

        _egg = GetComponent<Egg>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(TemporaryIncreasingScale());
    }

    private void OnEnable()
    {
        _egg.Broke += Break;
        _egg.Jumped += JumpToPack;
    }

    private void OnDisable()
    {
        _egg.Broke -= Break;
        _egg.Jumped -= JumpToPack;
    }

    public void ChangeMaterial() => GetComponent<MeshRenderer>().material = _newMaterial;

    public IEnumerator TemporaryIncreasingScale()
    {
        float speed = 300;
        float maxIncrease = 1.4f;
        bool isScaled = false;
        Vector3 defaultScale = transform.localScale;
        Vector3 targetScale = transform.localScale * maxIncrease;

        StartCoroutine(ChangeScale(targetScale));
        yield return new WaitUntil(() => isScaled == true);
        StartCoroutine(ChangeScale(defaultScale));

        IEnumerator ChangeScale(Vector3 targetScale)
        {
            while (transform.localScale != targetScale)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, Time.deltaTime * speed);
                yield return null;
            }

            isScaled = true;
        }
    }

    public void JumpToPack(Vector3 targetPosition)
    {
        StartCoroutine(JumpToPackCoroutine(targetPosition));

        IEnumerator JumpToPackCoroutine(Vector3 targetPosition)
        {
            const float Half = 0.5f;
            float height = 1.5f;
            float speed = 10;
            float bonusSpeed = 2;
            float startDistance = targetPosition.z - transform.position.z;

            _audioSource.PlayOneShot(_jumpSound);

            while (targetPosition.z - transform.position.z > 0)
            {
                float currentDistance = targetPosition.z - transform.position.z;
                Vector3 direction = (targetPosition - transform.position).normalized;

                if (currentDistance > startDistance * Half)
                    transform.Translate(new Vector3(direction.x, height, direction.z) * Time.deltaTime * speed);
                else
                    transform.Translate(new Vector3(direction.x * bonusSpeed, direction.y, direction.z * bonusSpeed) * Time.deltaTime * speed);

                yield return null;
            }
            FixPosition();
        }

        void FixPosition()
        {
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            transform.position = targetPosition;
        }
    }


    private void Break(Egg egg)
    {
        egg.Disable();
        Instantiate(_brokenEgg, transform.position, Quaternion.identity);
    }

    private void Validate()
    {
        if (_brokenEgg == null)
            throw new NullReferenceException(nameof(_brokenEgg));

        if (_newMaterial == null)
            throw new NullReferenceException(nameof(_newMaterial));

        if (_jumpSound == null)
            throw new NullReferenceException(nameof(_jumpSound));
    }
}
