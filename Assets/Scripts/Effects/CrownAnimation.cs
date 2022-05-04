using UnityEngine;

public class CrownAnimation : MonoBehaviour
{
    [SerializeField] private int _speed;

    private void Awake()
    {
        _speed = Mathf.Clamp(_speed, 0, int.MaxValue);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * _speed);
    }
}
