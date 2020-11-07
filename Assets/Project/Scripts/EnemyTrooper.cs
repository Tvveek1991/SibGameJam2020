using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class EnemyTrooper : MonoBehaviour
{
    public float endX;
    public float endY;
    public float moveTime = 15;
    private EnemyState enemyState;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.DOMove(new Vector2(endX, endY), moveTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlayerSpotted(Vector2 coordinates)
    {
        enemyState = EnemyState.chasePlayer;

    }

}

enum EnemyState { 
    onPatrol,
    chasePlayer
}
