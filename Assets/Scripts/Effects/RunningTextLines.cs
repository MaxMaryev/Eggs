using System;
using UnityEngine;

public class RunningTextLines : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Material _material;
    private float _yAxisOffset;
    private const string Albedo = "_MainTex";

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;

        if (_material == null)
            throw new NullReferenceException("Материал не назначен");

        _speed = Mathf.Clamp(_speed, 0, int.MaxValue);
    }

    private void Update()
    {
        _yAxisOffset -= Time.deltaTime * _speed;
        _material.SetTextureOffset(Albedo, new Vector2(uint.MinValue, _yAxisOffset));
    }
}
