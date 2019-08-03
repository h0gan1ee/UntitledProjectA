using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// 约定俗成：数组一律从 1 开始计数，其他类型从 0 开始计数。

namespace UntitledProjectA
{
    internal class Program
    {
        private static List<PermutationGroup> ExpressBySpecificElements(in List<PermutationGroup> specificElements, in PermutationGroup sourceGroup, in PermutationGroup targetGroup)
        {
            if (targetGroup == sourceGroup) return new List<PermutationGroup>();

            const int maxCount = 1000000000;
            int count = 0;

            Queue<List<int[]>> processQueue = new Queue<List<int[]>>();
            Queue<int[]> stateQueue = new Queue<int[]>();
            Queue<int> lastIndexQueue = new Queue<int>();
            List<int[]> finalProcess = new List<int[]>();
            bool notFound = true;

            processQueue.Enqueue(new List<int[]>());
            stateQueue.Enqueue(sourceGroup.Commands);
            lastIndexQueue.Enqueue(specificElements.Count);

            while (count++ < maxCount && notFound)
            {
                List<int[]> currentProcess = processQueue.Dequeue();
                PermutationGroup currentState = new PermutationGroup(stateQueue.Dequeue());
                int lastIndex = lastIndexQueue.Dequeue();

                for (int i = 0; i < specificElements.Count; ++i)
                {
                    if (i == lastIndex) continue;

                    PermutationGroup nextState = currentState * specificElements[i];

                    if (nextState == targetGroup)
                    {
                        currentProcess.Add(specificElements[i].Commands);
                        finalProcess = currentProcess;
                        notFound = false;
                        break;
                    }

                    List<int[]> nextProcess = new List<int[]>();
                    nextProcess.AddRange(currentProcess);
                    nextProcess.Add(specificElements[i].Commands);

                    processQueue.Enqueue(nextProcess);
                    stateQueue.Enqueue(nextState.Commands);
                    lastIndexQueue.Enqueue(i);
                }
            }

            List<PermutationGroup> boxedProcess = new List<PermutationGroup>();
            foreach (int[] i in finalProcess)
            {
                boxedProcess.Add(new PermutationGroup(i));
            }

            return boxedProcess;
        }

        private static List<PermutationGroup> GenerateMinimalElements(int count)
        {
            List<PermutationGroup> MinimalElements = new List<PermutationGroup>();

            int[] rawCommand = new int[count + 1];
            rawCommand[1] = -1;
            for (int i = 2; i <= count; ++i)
            {
                rawCommand[i] = i;
            }
            MinimalElements.Add(new PermutationGroup(rawCommand));

            for (int i = 1; i < count; ++i)
            {
                rawCommand = new int[count + 1];
                for (int j = 1; j <= count; ++j)
                {
                    if (j == i)
                    {
                        rawCommand[j] = i + 1;
                    }
                    else if (j == (i + 1))
                    {
                        rawCommand[i + 1] = i;
                    }
                    else
                    {
                        rawCommand[j] = j;
                    }
                }
                MinimalElements.Add(new PermutationGroup(rawCommand));
            }
            return MinimalElements;
        }

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

            const int n = 2;

            StringBuilder stringBuilder = new StringBuilder();
            RegularSet regularSet = new RegularSet(n);
            List<PermutationGroup> allPermutationGroups = regularSet.GetAllPermutationGroups();
            /*List<PermutationGroup> registedPermutationGroups = new List<PermutationGroup>();
            int Count = 0;
            foreach (PermutationGroup pg in allPermutationGroups)
            {
                if (!registedPermutationGroups.Contains(pg))
                {
                    ++Count;
                    registedPermutationGroups.Add(pg);
                    stringBuilder.Append("进行操作的变换：");
                    stringBuilder.Append(pg.ShowInCyclicNotation());
                    stringBuilder.AppendLine();
                    List<PermutationGroup> allConjugatedGroups = pg.GetAllConjugatedGroups(allPermutationGroups);
                    stringBuilder.AppendLine($"元素个数：{allConjugatedGroups.Count.ToString()}");
                    foreach (PermutationGroup pgg in allConjugatedGroups)
                    {
                        registedPermutationGroups.Add(pgg);*/
            /*pgg.ShowInCyclicNotation();
            Console.WriteLine();*//*
        }
    }
}
/*stringBuilder.AppendLine($"类的个数：{Count.ToString()}");*/

            List<PermutationGroup> basicElements = GenerateMinimalElements(n);
            PermutationGroup elementalGroup = new PermutationGroup(n);
            foreach (PermutationGroup pg in allPermutationGroups)
            {
                stringBuilder.AppendLine($"目标：{pg.ShowInCyclicNotation()}");
                List<PermutationGroup> currentExpression = ExpressBySpecificElements(basicElements, elementalGroup, pg);
                stringBuilder.AppendLine($"过程：");
                foreach (PermutationGroup pgg in currentExpression)
                {
                    stringBuilder.AppendLine(pgg.ShowInCyclicNotation());
                }
                stringBuilder.AppendLine();
            }

            using (StreamWriter sw = new StreamWriter(@"C:\Users\lihao\OneDrive\Documents\cache\output.txt"))
            {
                sw.Write(stringBuilder.ToString());
            }
        }
    }
}