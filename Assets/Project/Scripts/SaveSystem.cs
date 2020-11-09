using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveSystem : MonoBehaviour
{
    private const string KEY_STADE = "index_save"; 
    
    IPlayerLogic _playerLogic;

    [SerializeField] private List<LineBuilder> electroLine;


    private int index_save
    {
        get => PlayerPrefs.GetInt(KEY_STADE, 0);
        set
        {
            PlayerPrefs.SetInt(KEY_STADE, value);
        }
    }

    [SerializeField] private List<Transform> savePoints;

    private void Awake()
    {
        _playerLogic = FindObjectsOfType<MonoBehaviour>().OfType<IPlayerLogic>().FirstOrDefault();
        _playerLogic.GetPlayerController().SetPosition(savePoints[index_save]);
    }

    public void SavePoint(int point)
    {
        index_save = point;
    }

    public int GetCountFinishedLines()
    {
        return electroLine.FindAll(p => p.GetIsOver()).Count;
    }
}
