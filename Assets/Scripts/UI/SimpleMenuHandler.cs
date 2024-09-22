using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleMenuHandler : MonoBehaviour
{
    private string MainScene = "SampleScene";
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private VitalitySystem _vitalitySystem;

    private void Awake()
    {
        Time.timeScale = 1;
    }
    private void Start()
    {
        _vitalitySystem.OnDie += LoseState;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_pausePanel.activeInHierarchy)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _pausePanel.SetActive(true);
            }
            else
            {
                ResumeButtonAction();
            }
        }
    }
    private async void LoseState()
    {
        await Task.Delay(2500);
        Time.timeScale = 0;
        _losePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ResumeButtonAction()
    {
        Time.timeScale = 1;
        _pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void RestartButtonAction()
    {
        SceneManager.LoadScene(MainScene);
    }
    public void ExitButtonAction()
    {
        Application.Quit();
    }
}
