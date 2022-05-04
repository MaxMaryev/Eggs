using System;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    [SerializeField] private ParticleSystem _confetti;
    [SerializeField] private ParticleSystem _trails;

    public event Action LevelEnded;

    private void Awake()
    {
        Validate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MainEgg mainEgg))
        {
            Play();
            LevelEnded?.Invoke();
        }
    }

    private void Play()
    {
        _confetti.Play();
        _trails.Play();
    }

    private void Validate()
    {
        if (_confetti == null)
            throw new NullReferenceException(nameof(_confetti));


        if (_trails == null)
            throw new NullReferenceException(nameof(_trails));
    }
}
