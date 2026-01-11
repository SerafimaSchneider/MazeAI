using UnityEngine;

public class AgentController : MonoBehaviour
{
    public RaycastSensor sensor;
    public NeuralNetwork brain;

    public float speed = 3.0f;
    public float turnSpeed = 200f;
    public bool isDead = false;
    public float timeAlive = 0f;

    public void Init(NeuralNetwork _brain)
    {
        brain = _brain;
        timeAlive = 0f;
        isDead = false;
    }

    void Update()
    {
        if (isDead) return;
        timeAlive += Time.deltaTime;
        float[] readings = sensor.GetReadings();
        float[] outputs = brain.FeedForward(readings);
        float moveSignal = outputs[0];
        float turnSignal = outputs[1];

        transform.Translate(Vector3.up * moveSignal * speed * Time.deltaTime);
        transform.Rotate(0, 0, -turnSignal * turnSpeed * Time.deltaTime);
    }

    void OisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Die();
        }     
    }

    void OEnter2D(Collider2D collision)
    {
        Die();
    }

    void Die()
    {
        isDead = true;
        GetComponent<SpriteRenderer>().color = Color.black;
    }

}
