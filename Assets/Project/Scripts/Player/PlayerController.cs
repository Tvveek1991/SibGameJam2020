using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    public float speed;
    private bool moveAccess = true;

    private Rigidbody2D rb = null;
    private Vector2 moveVelocity;

    [SerializeField] private Component playerAnimator = null;
    private IPlayerAnimator _playerAnimator;

    [SerializeField] private Component playerLogic = null;
    private IPlayerLogic _playerLogic;

    [SerializeField] private UnityEvent OnDie = null;
    [SerializeField] private UnityEvent OnTakeBattery = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CheckInterfaces();
    }
    private void CheckInterfaces()
    {
        _playerLogic = InterfaceTools.GetInteface<IPlayerLogic>(playerLogic);
        if (_playerLogic == null)
            playerLogic = null;

        _playerAnimator = InterfaceTools.GetInteface<IPlayerAnimator>(playerAnimator);
        if (_playerAnimator == null)
            playerAnimator = null;
    }

    public void SetMoveAccess(bool value)
    {
        moveAccess = value;
    }

    public void SetPosition(Transform savePoint)
    {
        transform.position = savePoint.position;
    }

    private void Update()
    {
        if (GameStatus.isGameOver || !moveAccess)
            return;

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        //Вывод анимации в соответствии с направлением движения
        _playerAnimator.OnMove(moveInput);

        if(_playerLogic.GetByBattery() && Input.GetKeyDown(KeyCode.Space))
            OnTakeBattery?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("enemy"))
        {
            OnDie?.Invoke();
        }
    }
}
