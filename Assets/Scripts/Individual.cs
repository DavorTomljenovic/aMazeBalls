using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GeneticAlgorithm
{
    public class Individual<T>
    {
        public T[] Genes { get; private set; }
        public float Fitness { get; private set; }

        private System.Random Rng;
        private Func<T> createGene;
        private Func<int, float> fitnessFunc;

        public Individual(int numGenes, System.Random random, Func<T> createGene, Func<int, float> fitnessFunc, bool shouldInitGenes = true)
        {
            Genes = new T[numGenes];
            //Create random generator with the seed value of 5
            Rng = random;
            this.createGene = createGene;
            this.fitnessFunc = fitnessFunc;

            //Create new Genes for Inidividuals not created by CrossOver
            if (shouldInitGenes)
            {
                for (int i = 0; i < Genes.Length; i++)
                {
                    this.Genes[i] = createGene();
                }
            }
        }

        public float CalculateFitness(int individualIdx)
        {
            this.Fitness = fitnessFunc(individualIdx);
            return this.Fitness;
        }

        public Individual<T> Crossover(Individual<T> otherParent)
        {
            // Child individual will have same number of genes as parent
            Individual<T> child = new Individual<T>(Genes.Length, Rng, createGene, fitnessFunc, shouldInitGenes: false);

            //Copy genes from both parents to child
            for (int i = 0; i < Genes.Length; i++)
            {
                // Use random generator to determine from which parent will this gene be copied.
                child.Genes[i] = Rng.NextDouble() < 0.5f ? this.Genes[i] : otherParent.Genes[i];
            }

            return child;
        }

        public void Mutate(float mutationRate)
        {
            for (int i = 0; i < Genes.Length; i++)
            {
                if (Rng.NextDouble() < mutationRate)
                    Genes[i] = createGene();
            }
        }
    }
}
