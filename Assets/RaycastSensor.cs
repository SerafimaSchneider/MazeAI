using UnityEngine;

public class RaycastSensor : MonoBehaviour
{
    
    public float viewDistance = 5.0f;
    public int rayCount = 5;
    public float angle = 90f;
    public float speed = 3.0f;
    public float turnSpeed = 200f;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

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



        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, viewDistance);

        if (hit.collider != null)
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);

            if (hit.distance < 1.5f)
            {
                transform.Rotate(0, 0, -turnSpeed * Time.deltaTime);
            }
        }

        else
        {

            Debug.DrawRay(transform.position, direction * viewDistance, Color.green);
        }
    }
}

}
