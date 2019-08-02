using System;
using System.Collections.Generic;

// 约定俗成：数组一律从 1 开始计数，其他类型从 0 开始计数。

namespace UntitledProjectA
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            /*Console.WriteLine("Hello World!");
            RegularSet regularSet = new RegularSet(3);
            List<PermutationGroup> permutationgroups = regularSet.GetAllPermutationGroups();
            regularSet = regularSet * permutationgroups[3] * permutationgroups[3].Reverse();
            for (int i = regularSet.Count; i >= 1; --i)
            {
                Console.Write(regularSet.ReversedList[i].ToString() + " ");
            }
            for (int i = 1; i <= regularSet.Count; ++i)
            {
                Console.Write(regularSet.OriginalList[i].ToString() + " ");
            }
            PermutationGroup pga = new PermutationGroup(new int[] { 0, -3, 1, 2 });
            PermutationGroup pgb = new PermutationGroup(new int[] { 0, 3, 2, 1 });
            PermutationGroup pgc = pga * pgb;
            pgb.Show();
            pgb.ShowInCyclicNotation();*/

            /*RegularSet regularSet = new RegularSet(3);
            PermutationGroup customizedPermutation = new PermutationGroup(new int[] { 0, 3, 2, 1 });
            Console.Write("进行操作的变换：");
            customizedPermutation.ShowInCyclicNotation();
            List<PermutationGroup> allPermutationGroups = regularSet.GetAllPermutationGroups();
            List<PermutationGroup> allConjugatedGroups = customizedPermutation.GetAllConjugatedGroups(allPermutationGroups);
            foreach (PermutationGroup pg in allConjugatedGroups)
            {
                pg.ShowInCyclicNotation();
            }*/

            RegularSet regularSet = new RegularSet(3);
            List<PermutationGroup> allPermutationGroups = regularSet.GetAllPermutationGroups();
            List<PermutationGroup> registedPermutationGroups = new List<PermutationGroup>();
            foreach (PermutationGroup pg in allPermutationGroups)
            {
                if (!registedPermutationGroups.Contains(pg))
                {
                    registedPermutationGroups.Add(pg);
                    Console.Write("进行操作的变换：");
                    pg.ShowInCyclicNotation();
                    Console.WriteLine();
                    List<PermutationGroup> allConjugatedGroups = pg.GetAllConjugatedGroups(allPermutationGroups);
                    foreach (PermutationGroup pgg in allConjugatedGroups)
                    {
                        registedPermutationGroups.Add(pgg);
                        pgg.ShowInCyclicNotation();
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}