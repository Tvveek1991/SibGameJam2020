using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cable : MonoBehaviour
{
    private float cableLenght;
    private int sectionNumber;

    private UnityAction onBreakCable;

    private AudioSource shortCircuit;

    private void Start()
    {
        shortCircuit = GetComponent<AudioSource>();
    }

    public void SetData(int lenght, int section)
    {
        cableLenght = lenght;
        sectionNumber = section;

        StartCoroutine(MoveWithCable());
    }

    IEnumerator MoveWithCable()
    {
        while (cableLenght > 0)
        {
            yield return new WaitForSecondsRealtime(0.5f);

            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (moveInput.magnitude == 0)
                continue;

            cableLenght -= 0.5f;
        }

        onBreakCable?.Invoke();
        shortCircuit.Play();
        GetComponent<TrailRenderer>().enabled = false;
        Destroy(this.gameObject, 2);
    }

    public int GetSection()
    {
        return sectionNumber;
    }

    public void AddListener(UnityAction action)
    {
        onBreakCable += action;
    }
}
