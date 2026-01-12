using System.Collections.Generic;
using UnityEngine;

public class EvolutionManager : MonoBehaviour
{
    [Header("Настройки Эволюции")]
    public GameObject agentPrefab;      
    public int populationSize = 50;     
    public float generationDuration = 25f; 
    
    [Header("Мутация")]
    public float mutationChance = 0.05f; 
    public float mutationStrength = 0.2f; 

    // Внутренние переменные
    private List<AgentController> population = new List<AgentController>();
    private float timer = 0f;
    private int generationCount = 1;
    
    private NeuralNetwork bestBrain;

    void Start()
    {
        StartGeneration();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= generationDuration)
        {
            Evolve();
        }
    }

    void StartGeneration()
    {
        timer = 0f;
        
        int[] layers = new int[] { 5, 12, 2 };

        for (int i = 0; i < populationSize; i++)
        {
            GameObject newBird = Instantiate(agentPrefab, Vector3.zero, Quaternion.identity);
            AgentController agent = newBird.GetComponent<AgentController>();

            if (generationCount == 1)
            {
                agent.Init(new NeuralNetwork(layers));
            }
            else
            {
                NeuralNetwork childBrain = new NeuralNetwork(bestBrain);
                
                if (i > 0) 
                {
                    childBrain.Mutate(mutationChance, mutationStrength);
                }
                
                agent.Init(childBrain);
            }

            population.Add(agent);
        }
    }

    void Evolve()
    {
        AgentController bestAgent = null;
        float maxFitness = -1f;

        foreach (var agent in population)
        {
            if (agent.timeAlive > maxFitness)
            {
                maxFitness = agent.timeAlive;
                bestAgent = agent;
            }
            Destroy(agent.gameObject);
        }

        population.Clear();

        if (bestAgent != null)
        {
            bestBrain = bestAgent.brain;
            Debug.Log("Поколение " + generationCount + " завершено! Рекорд: " + maxFitness + " сек.");
        }
        else
        {
            Debug.Log("Все умерли! Используем старого чемпиона.");
        }

        generationCount++;
        StartGeneration();
    }
}