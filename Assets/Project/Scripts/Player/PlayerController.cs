using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    public float speed;

    private bool byBattery;

    private Rigidbody2D rb;
    private Vector2 moveVelocity;

    [SerializeField] private Component anim;
    private IPlayerAnimator _anim;

    [SerializeField] private UnityEvent OnDie;
    [SerializeField] private UnityEvent OnTakeBattery;

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

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        //Вывод анимации в соответствии с направлением движения
        _anim.OnMove(moveInput);

        if(byBattery && Input.GetKey(KeyCode.Space))
        {
            OnTakeBattery?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("enemy"))
        {
            OnDie?.Invoke();
        }
    }
}
