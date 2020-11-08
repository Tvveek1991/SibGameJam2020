using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    internal static bool isGameOver = false;

    [SerializeField] private UnityEvent OnGameOver;

    public void GameOver()
    {
        print("GAME OVER");
        isGameOver = true;
        OnGameOver?.Invoke();

        Invoke("TryAgain", 2);
    }

    public void TryAgain()
    {
        print("RESTART");
        isGameOver = false;
        SceneManager.LoadScene(0);
    }

    public void FullRestart()
    {
        PlayerPrefs.DeleteAll();
        TryAgain();
    }
}
