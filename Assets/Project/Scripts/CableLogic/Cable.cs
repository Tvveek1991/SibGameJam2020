using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    private float cableLenght;
    private int sectionNumber;



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

        Destroy(this.gameObject);
    }

    public int GetSection()
    {
        return sectionNumber;
    }
}
