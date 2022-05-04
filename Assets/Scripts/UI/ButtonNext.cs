using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ButtonNext : MonoBehaviour
{
    [SerializeField] private Confetti _confetti;

    private void Awake()
    {
        if (_confetti == null)
            throw new NullReferenceException(nameof(_confetti));
    }

    private void OnEnable()
    {
        _confetti.LevelEnded += MoveUp;
    }

    private void OnDisable()
    {
        _confetti.LevelEnded -= MoveUp;
    }

    private void MoveUp()
    {
        StartCoroutine(DelayedMove());

        IEnumerator DelayedMove()
        {
            float height = 9;
            float duration = 0.7f;
            float delay = 2.3f;

            yield return new WaitForSeconds(delay);
            transform.DOMoveY(height, duration);
        }
    }
}
