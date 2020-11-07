using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurretVisionCone : MonoBehaviour
{
    [SerializeField] private UnityEvent onPlayerContact = null;
    [SerializeField] private Sprite BurningFlamesOfFieryDeath = null;
    private SpriteRenderer currentSprite;
    // Start is called before the first frame update
    void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            currentSprite.sprite = BurningFlamesOfFieryDeath;
            onPlayerContact?.Invoke();
        }
    }
}
