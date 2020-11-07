using System;
using System.Collections;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public int visionChangeTime = 10;
    public float newConeDelayTime = 1;
    public GameObject visionConePrefab;
    private GameObject currentVisionCone;
    private TurretRotation currentRotation = TurretRotation.UP;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("VisionDirectionChange");
    }


    IEnumerator VisionDirectionChange()
    {
        turnClockwise();
        spawnNewCone();
        yield return new WaitForSeconds(visionChangeTime);
        Destroy(currentVisionCone);
        StartCoroutine("NewConeDelay");
    }

    IEnumerator NewConeDelay()
    {
        yield return new WaitForSeconds(newConeDelayTime);
        StartCoroutine("VisionDirectionChange");
    }

    void spawnNewCone()
    {
        switch (currentRotation) {
            case TurretRotation.UP:
                currentVisionCone = Instantiate(visionConePrefab, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.Euler(0F, 0F, 180F));
                break;
            case TurretRotation.RIGHT:
                currentVisionCone = Instantiate(visionConePrefab, new Vector2(transform.position.x + 1, transform.position.y), Quaternion.Euler(0F, 0F, 90F));
                break;
            case TurretRotation.DOWN:
                currentVisionCone = Instantiate(visionConePrefab, new Vector2(transform.position.x, transform.position.y - 1), Quaternion.Euler(0F, 0F, 0F));
                break;
            case TurretRotation.LEFT:
                currentVisionCone = Instantiate(visionConePrefab, new Vector2(transform.position.x-1, transform.position.y), Quaternion.Euler(0F, 0F, -90F));
                break;
        }       
    }

    public void TurnOffRotation()
    {
        StopAllCoroutines();
    }

    private void turnClockwise()
    {
        if (currentRotation != TurretRotation.LEFT)
        {
            currentRotation += 1;
        } else
        {
            currentRotation = TurretRotation.UP;
        }
    }

}

enum TurretRotation
{
    UP,
    RIGHT,
    DOWN,
    LEFT
}

