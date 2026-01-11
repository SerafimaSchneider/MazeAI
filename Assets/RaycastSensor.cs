using UnityEngine;

public class RaycastSensor : MonoBehaviour
{
    
    public float viewDistance = 5.0f;
    public int rayCount = 5;
    public float angle = 90f;

    public float[] GetReadings()
    {
     float[] readings = new float[rayCount];

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
                readings[i] = hit.distance;
            }

            else
            {
                Debug.DrawRay(transform.position, direction * viewDistance, Color.green);
                readings[i] = viewDistance;
            }
        } 

        return readings;
    }
}