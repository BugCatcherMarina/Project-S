using Isamu.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuObject;

    private Controls controls;
    private bool isPaused;

    public void Pause()
    {
        isPaused = true;
        menuObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isPaused = false;
        menuObject.SetActive(false);
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
        controls = new Controls();

        controls.Computer.InGameMenu.performed += TogglePauseState;

        controls.Enable();
    }

    private void OnDestroy()
    {
        controls.Computer.InGameMenu.performed -= TogglePauseState;

        controls.Disable();
    }
}
