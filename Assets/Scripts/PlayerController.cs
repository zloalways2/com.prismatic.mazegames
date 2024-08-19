using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private GameController _gameController;

    [SerializeField] private AudioSource _soundSource;

    [SerializeField] private AudioClip _buttonMovementSound;

    private Vector2 _movementDirection;

    private void Start()
    {
        PlacePlayer(Screen.width / 10, Screen.height / 1.45f);
    }

    private void Update()
    {
        if (_movementDirection != Vector2.zero)
        {
            Move(_movementDirection);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ZoneEnd"))
        {
            _gameController.Win();
        }
    }

    public void OnMoveUpButtonPressed()
    {
        _movementDirection = Vector2.up;
        PlaySound();
    }

    public void OnMoveDownButtonPressed()
    {
        _movementDirection = Vector2.down;
        PlaySound();
    }

    public void OnMoveLeftButtonPressed()
    {
        _movementDirection = Vector2.left;
        PlaySound();
    }

    public void OnMoveRightButtonPressed()
    {
        _movementDirection = Vector2.right;
        PlaySound();
    }

    public void OnMoveButtonReleased()
    {
        _movementDirection = Vector2.zero;
        if (_gameController.IsPlaying)
        {
            _soundSource.clip = null;
            _soundSource.Stop();
        }

    }

    private void Move(Vector2 direction)
    {
        Vector3 movement = direction * _speed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }

    private void PlacePlayer(float screenX, float screenY)
    {
        Vector3 position = GetScreenToWorldPoint(screenX, screenY);
        transform.position = position;
    }

    private Vector3 GetScreenToWorldPoint(float screenX, float screenY)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(screenX, screenY, Camera.main.nearClipPlane));
    }

    private void PlaySound()
    {
        _soundSource.clip = _buttonMovementSound;
        _soundSource.Play();
    }
}
