using UnityEngine;

public enum GameState   
{
    MainMenu, Gameplay, Pause
}

public class GameManager : Singleton<GameManager>
{
    private static GameState gameState;

    private static float normalDeltaTime = Time.deltaTime;
    private static float pauseDeltaTime = 0f;

    private void Awake()
    {
        Input.multiTouchEnabled = true;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Start()
    {
        ChangeState(GameState.MainMenu);

        //CS_UIManager.Ins.OpenUI<UISplash>();
        //CS_UIManager.Ins.GetUI<UISettings>().GetSetting();
    }

    
    public static void ChangeState(GameState state)
    {
        gameState = state;
    }

    public static bool IsState(GameState state) => gameState == state; 

    public static float DeltaTime
    {
        get
        {
            if (IsState(GameState.Pause))
            {
                return pauseDeltaTime;
            }
            else
            {
                return normalDeltaTime;
            }
        }
    }
}
