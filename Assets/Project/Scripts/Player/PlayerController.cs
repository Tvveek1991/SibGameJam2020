using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    public float speed;

    private bool byBattery;

    private Rigidbody2D rb = null;
    private Vector2 moveVelocity;

    [SerializeField] private Component anim = null;
    private IPlayerAnimator _anim;

    [SerializeField] private UnityEvent OnDie = null;
    [SerializeField] private UnityEvent OnTakeBattery = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CheckInterfaces();
    }
    private void CheckInterfaces()
    {
        _anim = InterfaceTools.GetInteface<IPlayerAnimator>(anim);
        if (_anim == null)
            anim = null;
    }

    public void SetByBattery(bool value)
    {
        byBattery = value;
    }

    private void Update()
    {
        if (GameStatus.isGameOver)
            return;

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        //Вывод анимации в соответствии с направлением движения
        _anim.OnMove(moveInput);

        if(byBattery && Input.GetKeyDown(KeyCode.Space))
        {
            OnTakeBattery?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("enemy"))
        {
            OnDie?.Invoke();
        }
    }
}
