using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class YouWinText : MonoBehaviour
{
    [SerializeField] private Confetti _confetti;

    private void Awake()
    {
        if (_confetti == null)
            throw new NullReferenceException(nameof(_confetti));
    }

    private void OnEnable()
    {
        _confetti.LevelEnded += MoveDown;
    }

    private void OnDisable()
    {
        _confetti.LevelEnded -= MoveDown;
    }

    private void MoveDown()
    {
        StartCoroutine(DelayedMove());

        IEnumerator DelayedMove()
        {
            float height = 450f;
            float duration = 0.7f;
            float delay = 2.3f;

            yield return new WaitForSeconds(delay);
            transform.DOLocalMoveY(height, duration);
        }
    }
}
