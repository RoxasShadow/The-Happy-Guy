using UnityEngine;

public class Menu : MonoBehaviour
{
    private bool newgame = true;
    private float savedTimeScale;
    public GameObject start;

    public enum Page
    {
        None,
        Main,
        Options
    };

    private Page currentPage;
    private int toolbarInt = 0;
    private string[] toolbarstrings = { "Audio", "Graphics" };

    void Start()
    {
        Time.timeScale = 1;
        PauseGame();
    }

    void OnGUI()
    {
        if (IsGamePaused())
        {
            switch (currentPage)
            {
                case Page.Main:
                    MainPauseMenu();
                    break;
                case Page.Options:
                    ShowToolbar();
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused())
            {
                switch (currentPage)
                {
                    case Page.None:
                        PauseGame();
                        break;

                    case Page.Main:
                        if (!IsBeginning())
                            UnPauseGame();
                        break;

                    default:
                        currentPage = Page.Main;
                        break;
                }
            }
            else
                PauseGame();
        }
    }

    void ShowToolbar()
    {
        BeginPage(300, 300);
        toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarstrings);
        switch (toolbarInt)
        {
            case 0:
                VolumeControl();
                break;
            case 1:
                Qualities();
                QualityControl();
                break;
        }
        EndPage();
    }

    void ShowBackButton()
    {
        if (GUI.Button(new Rect(Screen.width / 2, (Screen.height / 2) - 50, 50, 20), "Back"))
            currentPage = Page.Main;
    }

    void Qualities()
    {
        switch (QualitySettings.currentLevel)
        {
            case QualityLevel.Fastest:
                GUILayout.Label("Fastest");
                break;
            case QualityLevel.Fast:
                GUILayout.Label("Fast");
                break;
            case QualityLevel.Simple:
                GUILayout.Label("Simple");
                break;
            case QualityLevel.Good:
                GUILayout.Label("Good");
                break;
            case QualityLevel.Beautiful:
                GUILayout.Label("Beautiful");
                break;
            case QualityLevel.Fantastic:
                GUILayout.Label("Fantastic");
                break;
        }
    }

    void QualityControl()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Decrease"))
            QualitySettings.DecreaseLevel();
        if (GUILayout.Button("Increase"))
            QualitySettings.IncreaseLevel();

        GUILayout.EndHorizontal();
    }

    void VolumeControl()
    {
        GUILayout.Label("Volume");
        audio.volume = GUILayout.HorizontalSlider(audio.volume, 0, 1);
    }

    void BeginPage(int width, int height)
    {
        GUILayout.BeginArea(new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height));
    }

    void EndPage()
    {
        GUILayout.EndArea();
        if (currentPage != Page.Main)
            ShowBackButton();
    }

    bool IsBeginning()
    {
        return newgame;
    }

    void MainPauseMenu()
    {
        BeginPage(200, 200);
        if (GUILayout.Button(IsBeginning() ? "Play" : "Continue"))
            UnPauseGame();
        if (!IsBeginning())
            if (GUILayout.Button("Restart"))
                Application.LoadLevel("Main");
        if (GUILayout.Button("Options"))
            currentPage = Page.Options;
        if (GUILayout.Button("Quit"))
            Application.Quit();
        EndPage();
    }

    void PauseGame()
    {
        savedTimeScale = Time.timeScale;
        Time.timeScale = 0;
        audio.Pause();
        currentPage = Page.Main;
        Screen.showCursor = true;
    }

    void UnPauseGame()
    {
        Time.timeScale = savedTimeScale;
        audio.Play();
        currentPage = Page.None;
        Screen.showCursor = false;
        newgame = false;

        if (IsBeginning() && start != null)
            start.active = true;
    }

    bool IsGamePaused()
    {
        return (Time.timeScale == 0);
    }

    void OnApplicationPause(bool pause)
    {
        if (IsGamePaused())
            audio.Pause();
    }
}