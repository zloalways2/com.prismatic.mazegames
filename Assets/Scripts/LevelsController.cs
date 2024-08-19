using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsController : MonoBehaviour
{
    [SerializeField] private Button[] _levelButtons;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _buttonSound;

    [SerializeField] private int _scene;

    private int _lastLevel;
    private int _currentLevel;

    private void Start()
    {
        LoadGameProgress();
        InitializeButtons();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.DeleteAll();
            LoadGameProgress();
            InitializeButtons();
        }
    }

    public int GetCurrentLevel()
    {
        return _currentLevel;
    }

    public int GetLastLevel()
    {
        return _lastLevel;
    }

    public void CompleteLevel(int starsCollected)
    {
        if (_currentLevel == _lastLevel)
        {
            _lastLevel++;
        }
        SaveGameProgress(starsCollected);
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            int level = i + 1;

            _levelButtons[i].onClick.RemoveAllListeners();
            _levelButtons[i].onClick.AddListener(() => OnLevelButtonClicked(level));

            if (level <= _lastLevel)
            {
                _levelButtons[i].interactable = true;
                UpdateStarsForLevel(_levelButtons[i], level);
            }
            else
            {
                _levelButtons[i].interactable = false;
            }
        }
    }

    private void UpdateStarsForLevel(Button levelButton, int level)
    {
        int starsCollected = PlayerPrefs.GetInt("LevelStars_" + level, 0);
        GameObject starsContainer = levelButton.transform.Find("Stars").gameObject;

        for (int i = 0; i < starsContainer.transform.childCount; i++)
        {
            starsContainer.transform.GetChild(i).gameObject.SetActive(i < starsCollected);
        }
    }

    private void OnLevelButtonClicked(int level)
    {
        if (level <= _lastLevel)
        {
            _currentLevel = level;
            PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
            OpenLevel();
            if (_buttonSound != null)
                _audioSource.PlayOneShot(_buttonSound);
        }
    }

    public void OpenLevel()
    {
        SceneManager.LoadScene(_scene);
        PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
        PlayerPrefs.Save();
    }

    public void NextLevel()
    {
        if (_currentLevel < _lastLevel)
        {
            _currentLevel++;
            OpenLevel();
            _audioSource.PlayOneShot(_buttonSound);

        }
    }

    private void SaveGameProgress(int starsCollected)
    {
        PlayerPrefs.SetInt("LastLevel", _lastLevel);
        PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
        if (starsCollected > PlayerPrefs.GetInt("LevelStars_" + _currentLevel, 0))
            PlayerPrefs.SetInt("LevelStars_" + _currentLevel, starsCollected);
        PlayerPrefs.Save();
    }

    private void LoadGameProgress()
    {
        _lastLevel = PlayerPrefs.GetInt("LastLevel", 1);
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
    }
}
