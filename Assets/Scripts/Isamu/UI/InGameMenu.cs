using Isamu.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuObject;
    [SerializeField] private bool hideMenuOnStart = true;

    private Controls controls;
    private bool isPaused;

    public void Pause()
    {
        isPaused = true;
        SetIsMenuVisible(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isPaused = false;
        SetIsMenuVisible(false);
        Time.timeScale = 1;
    }

    public void TogglePauseState(InputAction.CallbackContext ctx)
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #endif
    }

    private void Awake()
    {
        if (hideMenuOnStart)
        {
            SetIsMenuVisible(false);
        }

        InitializeInput();
    }

    private void InitializeInput()
    {
        controls = new Controls();

        controls.Computer.InGameMenu.performed += TogglePauseState;

        controls.Enable();
    }

    private void SetIsMenuVisible(bool isVisible)
    {
        menuObject.SetActive(isVisible);
    }

    private void OnDestroy()
    {
        controls.Computer.InGameMenu.performed -= TogglePauseState;

        controls.Disable();
    }
}
