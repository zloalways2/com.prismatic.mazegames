using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] _levels;
    [SerializeField] private GameObject[] _textsLevel;
    [SerializeField] private GameObject[] _stars;

    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _loseUI;
    [SerializeField] private GameObject _optionsUI;
    [SerializeField] private GameObject _buttonNext;
    [SerializeField] private GameObject _player;

    [SerializeField] private LevelsController _levelsController;

    [SerializeField] private Text _textTime;
    [SerializeField] private Text _textTimeWin;

    [SerializeField] private AudioClip _buttonSound;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _loseSound;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;

    public bool IsPlaying;

    private int _currentLevel;
    private int _countStars;

    private float _time = 180;

    private bool _isPaused;
    
    private void Start()
    {
        LoadGameProgress();
        InitializeLevel();
        Time.timeScale = 1.0f;
        _textsLevel[_currentLevel - 1].SetActive(true);
    }
    private void Update()
    {
        _time -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(_time / 60);
        int seconds = Mathf.FloorToInt(_time % 60);
        _textTime.text = $"{minutes:D2}:{seconds:D2}";
        if (_time <= 0)
        {
            Lose();
            Time.timeScale = 0f;
        }
    }
    public void NextLevel()
    {
        _levelsController.NextLevel();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Options()
    {
        _isPaused = !_isPaused;
        _optionsUI.SetActive(_isPaused);
        Time.timeScale = !_isPaused ? 1.0f : 0.0f;
        _player.SetActive(!_isPaused);
        _soundSource.PlayOneShot(_buttonSound);
    }

    public void Win()
    {
        if (_time < 120) _countStars = 1;
        if (_time >= 120 && _time < 150) _countStars = 2;
        if (_time >= 150) _countStars = 3;
        _levelsController.CompleteLevel(_countStars);
        _winUI.SetActive(true);
        if (_levelsController.GetCurrentLevel() == 3)
        {
            _buttonNext.SetActive(false);
        }
        _player.SetActive(false);
        int minutes = Mathf.FloorToInt(_time / 60);
        int seconds = Mathf.FloorToInt(_time % 60);
        _textTimeWin.text = $"{minutes:D2}:{seconds:D2}";
        for (int i = 0; i < _countStars; i++)
        {
            _stars[i].SetActive(true);
        }
        _musicSource.Stop();
        IsPlaying = false;
        PlaySound(_winSound);
    }

    private void Lose()
    {
        _loseUI.SetActive(true);
        _player.SetActive(false);
        _textsLevel[_currentLevel + 2].SetActive(true);
        _musicSource.Stop();
        IsPlaying = false;
        PlaySound(_loseSound);
    }

    private void InitializeLevel()
    {
        _levels[_currentLevel - 1].SetActive(true);
    }

    private void LoadGameProgress()
    {
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
    }

    private void PlaySound(AudioClip audioClip)
    {
        _soundSource.clip = audioClip;
        _soundSource.loop = false;
        _soundSource.Play();
    }
}
