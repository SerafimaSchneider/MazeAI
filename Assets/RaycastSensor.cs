using UnityEngine;

public class RaycastSensor : MonoBehaviour
{
    
    public float viewDistance = 5.0f;
    public int rayCount = 5;
    public float angle = 90f;

    void Update()
    {

        CastRays();

    }
void CastRays()
{
    float startAngle = -angle / 2;
    float angleStep = angle / (rayCount - 1);

    for (int i = 0; i < rayCount; i++)
    {
        float currentAngle = startAngle + (angleStep * i);

        Vector3 direction = Quaternion.Euler(0, 0, currentAngle) * transform.up;

        Debug.DrawRay(transform.position, direction * viewDistance, Color.green);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, viewDistance);

        if (hit.collider != null)
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
    }
}

}
