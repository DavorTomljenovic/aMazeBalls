using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GeneticAlgorithm
{
    class GeneticAlgorithm<T>
    {
        public List<Individual<T>> Population { get; private set; }
        public int Generation { get; private set; }
        public float MutationRate;
        public float PopulationFitness { get; private set; }

        private System.Random Rng;

        public GeneticAlgorithm(int populationSize, int numGenes, System.Random random, Func<T> createGene, Func<int, float> fitnessFunc,
            float mutationRate = 0.01f)
        {
            //Start from first Generation
            Generation = 1;
            PopulationFitness = 0;
            this.MutationRate = mutationRate;
            this.Rng = random;

            //Create a population of Individuals
            Population = new List<Individual<T>>();
            
            //Create an Individual and add it to the population
            for (int i = 0; i < populationSize; i++)
            {
                Population.Add(new Individual<T>(numGenes, random, createGene, fitnessFunc, shouldInitGenes: true));
            }
        }

        public void NewGeneration()
        {
            if(Population.Count <= 0)
                return;

            CalculateFitness();
            List<Individual<T>> newPopulation = new List<Individual<T>>();

            for (int i = 0; i < Population.Count; i++)
            {
                Individual<T> firstParent = ChooseParent();
                Individual<T> secondParent = ChooseParent();

                Individual<T> child = firstParent.Crossover(secondParent);
                child.Mutate(MutationRate);

                newPopulation.Add(child);
            }


            this.Population = newPopulation;
            this.Generation++;
        }

        public void CalculateFitness()
        {
            // Reset previous fitness
            PopulationFitness = 0;

            for (int i = 0; i < Population.Count; i++)
            {
                PopulationFitness += Population[i].CalculateFitness(i);
            }
        }

        private Individual<T> ChooseParent()
        {
            double fitnessBorder = Rng.NextDouble() * PopulationFitness;

            for (int i = 0; i < Population.Count; i++)
            {
                if (fitnessBorder < Population[i].Fitness)
                {
                    return Population[i];
                }

                fitnessBorder -= Population[i].Fitness;
            }
            return null;
        }
    }
}
