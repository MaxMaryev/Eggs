using UnityEngine;

[RequireComponent(typeof(MainEggPresenter))]
public class MainEgg : MonoBehaviour
{
    [SerializeField] private float _speed;

    private MainEggPresenter _mainEggPresenter;

    public float DefaultSpeed { get; private set; }

    private void Awake()
    {
        _speed = Mathf.Clamp(_speed, uint.MinValue, int.MaxValue);
        DefaultSpeed = _speed;
        _mainEggPresenter = GetComponent<MainEggPresenter>();
    }

    private void Update()
    {
        MoveForward();
    }

    private void OnEnable() => _mainEggPresenter.MouseDragged += MoveSideToSide;

    private void OnDisable() => _mainEggPresenter.MouseDragged -= MoveSideToSide;

    private void MoveSideToSide(float delta) => transform.Translate(transform.right * delta / 100);

    private void MoveForward() => transform.Translate(transform.forward * Time.deltaTime * _speed);

    public void SetSpeed(float speed) => _speed = speed;
}
