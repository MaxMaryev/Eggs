using System;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class EggCounter : MonoBehaviour
{
    private EggPack _eggPack;
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _eggPack = GetComponentInParent<EggPack>();

        if(_eggPack == null)
            throw new NullReferenceException(nameof(_eggPack));
    }

    private void OnEnable()
    {
        _eggPack.EggsCountChanged += Show;
    }

    private void OnDisable()
    {
        _eggPack.EggsCountChanged -= Show;
    }

    private void Show(int eggsCount)
    {
        if(eggsCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(eggsCount));

        _text.text = $"X{eggsCount}";
    }
}
