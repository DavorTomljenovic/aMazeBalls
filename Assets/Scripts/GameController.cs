using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneticAlgorithm
{
    class GameController : MonoBehaviour
    {
        [Header("Genetic Algorithm")]
        [SerializeField]
        int populationSize = 200;
        [SerializeField]
        float mutationRate = 0.01f;
        private System.Random Rng;

        private GeneticAlgorithm<char> genetics;
        private const int maxInputs = 10000;
        private const string validInputs = "wsad ";
        private const string desiredInputs = "";

        void Start()
        {
            Rng = new System.Random();
            genetics = new GeneticAlgorithm<char>(populationSize, maxInputs, Rng, GetRandomCharacter, FitnessFunction, mutationRate);
        }

        void FixedUpdate()
        {
            genetics.NewGeneration();
        }

        private char GetRandomCharacter()
        {
            int i = Rng.Next(validInputs.Length);
            return validInputs[i];
        }

        private float FitnessFunction(int idx)
        {
            float score = 0;
            Individual<char> individual = genetics.Population[idx];

            for (int i = 0; i < individual.Genes.Length; i++)
            {
                // TODO: Calculate fitness
            }

            return score;
        }
    }
}
