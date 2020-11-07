using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UIElements;
using System.Collections;
using UnityEditorInternal;

public class EnemyTrooper : MonoBehaviour
{
    public float endX;
    public float endY;
    public float moveTime = 10;
    public float chaseTime = 10;
    public float chaseSpeed = 0.8F;

    private float startX;
    private float startY;
     private EnemyState enemyState;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
        player = GameObject.FindGameObjectWithTag("Player");
        transform.DOMove(new Vector2(endX, endY), moveTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            PlayerSpotted();

        if (enemyState == EnemyState.triggered)
        {
            enemyState = EnemyState.chasePlayer;
            transform.DOKill();
            StartCoroutine("ChasePlayer");
        }

        if (enemyState == EnemyState.chasePlayer)
        {
            if (player == null)
            {
                return;
            }
            Vector2 playerVector = (player.transform.position - transform.position);
            playerVector = transform.InverseTransformVector(playerVector);
            playerVector.x = playerVector.x * Time.deltaTime * chaseSpeed;
            playerVector.y = playerVector.y * Time.deltaTime * chaseSpeed;
            Debug.Log(playerVector.x);
            transform.Translate(playerVector);
        }
    }

    public void PlayerSpotted()
    {
        enemyState = EnemyState.triggered;
    }

    IEnumerator ChasePlayer()
    {
        Debug.Log("Start chase");
        yield return new WaitForSeconds(chaseTime);
        Debug.Log("End chase");
        enemyState = EnemyState.onPatrol;
        transform.DOMove(new Vector2(startX, startY), chaseTime).OnComplete(startPatrol);
    }

    void startPatrol()
    {
        transform.DOMove(new Vector2(endX, endY), moveTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

}

enum EnemyState { 
    onPatrol,
    triggered,
    chasePlayer,
}
