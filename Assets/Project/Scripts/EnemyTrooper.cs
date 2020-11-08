using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UIElements;
using System.Collections;
using UnityEditorInternal;

/// <WARNING MADAFAKA>
/////////////////////////////////////////////////////
///////////////////////////////////////////////////// 
/////////////////////////////////////////////////////
///////////////////////////////////////////////////// 
///////////////////////////////////////////////////// 
///////////////////////////////////////////////////// 
/////////////////////////////////////////////////////
/// THIS SHIT DOESN'T WORK AND WE DON'T USE IT ANYMORE
///////////////////////////////////////////////////// 
///////////////////////////////////////////////////// 
///////////////////////////////////////////////////// 
///////////////////////////////////////////////////// 
///////////////////////////////////////////////////// 
///////////////////////////////////////////////////// 
///////////////////////////////////////////////////// 
/// <WARNING MADAFAKA>
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
        startPatrol();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
            PlayerSpotted();

        if (enemyState == EnemyState.triggered)
        {
            enemyState = EnemyState.chasePlayer;
            var killed = transform.DOKill();
            Debug.Log(killed);
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
            transform.Translate(playerVector);
        }
    }

    public void PlayerSpotted()
    {
        enemyState = EnemyState.triggered;
    }

    IEnumerator ChasePlayer()
    {
        yield return new WaitForSeconds(chaseTime);
        enemyState = EnemyState.onPatrol;
        transform.DOMove(new Vector2(startX, startY), chaseTime).OnComplete(startPatrol);
    }

    void startPatrol()
    {
        transform.DOMove(new Vector2(endX, endY), moveTime).SetEase(Ease.Linear);
    }

    void returnToStartPoint()
    {
        transform.DOMove(new Vector2(startX, startY), moveTime).OnComplete(startPatrol).SetEase(Ease.Linear);
    }

}

enum EnemyState { 
    onPatrol,
    triggered,
    chasePlayer,
}
