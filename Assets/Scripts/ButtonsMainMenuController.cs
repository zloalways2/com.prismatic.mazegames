using UnityEngine;

public class ButtonsMainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _menuUI;
    [SerializeField] private GameObject _levelsUI;
    [SerializeField] private GameObject _optionsUI;
    [SerializeField] private GameObject _quitUI;

    [SerializeField] private AudioSource _soundSource;

    [SerializeField] private AudioClip _buttonSound;

    public void Play()
    {
        _menuUI.SetActive(false);
        _levelsUI.SetActive(true);
        PlaySound();
    }
    public void Options()
    {
        _menuUI.SetActive(false);
        _optionsUI.SetActive(true);
        PlaySound();
    }
    public void Quit()
    {
        _menuUI.SetActive(false);
        _quitUI.SetActive(true);
        PlaySound();
    }
    public void ConfirmQuit()
    {
        Application.Quit();
        PlaySound();
    }
    public void ConfuseQuit()
    {
        BackToMenu();
        PlaySound();
    }
    public void BackToMenu()
    {
        _menuUI.SetActive(true);
        _levelsUI.SetActive(false);
        _optionsUI.SetActive(false);
        _quitUI.SetActive(false);
        PlaySound();
    }
    private void PlaySound()
    {
        _soundSource.PlayOneShot(_buttonSound);
    }
}
