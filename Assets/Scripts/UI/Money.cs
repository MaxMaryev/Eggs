using UnityEngine;
using DG.Tweening;

public class Money : MonoBehaviour
{
    private Vector3 _firstTarget;
    private Vector3 _firstShift;
    private Vector3 _secondTarget;
    private Vector3 _secondShift;

    private void Awake()
    {
        _firstShift = new Vector3(30, -30, 0);
        _firstTarget = new Vector3(transform.position.x + _firstShift.x, transform.position.y + _firstShift.y, transform.position.z);

        _secondShift = new Vector3(-300, 600, 0);
        _secondTarget = new Vector3(transform.position.x + _secondShift.x, transform.position.y + _secondShift.y, transform.position.z);
    }

    private void Start()
    {
        var random = (min: 0.1f, max: 0.5f);
        float delay = Random.Range(random.min, random.max);
        var speed = (firstTarget: 0.2f, secondTarget: 0.8f);

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(delay);
        sequence.Append(transform.DOMove(_firstTarget, speed.firstTarget));      
        sequence.AppendInterval(delay);
        sequence.Append(transform.DOMove(_secondTarget, speed.secondTarget));
    }
}
