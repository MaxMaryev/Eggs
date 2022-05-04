using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class LuggageBelt : MonoBehaviour
{
    [SerializeField] float _speed;

    private Material _material;
    private float _xAxisTextureOffset;
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
        ShowMovement();
    }

    private void ShowMovement()
    {
        _xAxisTextureOffset += Time.deltaTime;
        _material.SetTextureOffset(Albedo, new Vector2(_xAxisTextureOffset, uint.MinValue));
    }
}
