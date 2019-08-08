using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// 约定俗成：数组一律从 1 开始计数，其他类型从 0 开始计数。

namespace UntitledProjectA
{
    internal class Program
    {
        private static int temp_counter = 0;

        private static List<PermutationGroup> ExpressBySpecificElements(in List<PermutationGroup> specificElements, in PermutationGroup sourceGroup, in PermutationGroup targetGroup)
        {
            if (targetGroup == sourceGroup) return new List<PermutationGroup>();

            const int maxCount = 1000000000;
            int count = 0;

            //Queue<List<int[]>> processQueue = new Queue<List<int[]>>();
            Queue<int[]> stateQueue = new Queue<int[]>();
            Queue<int> lastIndexQueue = new Queue<int>();
            HashSet<int[]> visitedStates = new HashSet<int[]>();
            Queue<int> tempQueue = new Queue<int>();
            //List<int[]> finalProcess = new List<int[]>();
            bool notFound = true;

            //processQueue.Enqueue(new List<int[]>());
            stateQueue.Enqueue(sourceGroup.Commands);
            lastIndexQueue.Enqueue(specificElements.Count);
            tempQueue.Enqueue(0);

            while (count++ < maxCount && notFound)
            {
                //List<int[]> currentProcess = processQueue.Dequeue();
                PermutationGroup currentState = new PermutationGroup(stateQueue.Dequeue());
                int lastIndex = lastIndexQueue.Dequeue();
                int tempCounter = tempQueue.Dequeue();

                for (int i = 0; i < specificElements.Count; ++i)
                {
                    if (i == lastIndex) continue;

                    PermutationGroup nextState = currentState * specificElements[i];

                    if (visitedStates.Contains(nextState.Commands)) continue;

                    if (nextState == targetGroup)
                    {
                        //currentProcess.Add(specificElements[i].Commands);
                        //finalProcess = currentProcess;
                        temp_counter = tempCounter + 1;
                        notFound = false;
                        break;
                    }

                    //List<int[]> nextProcess = new List<int[]>();
                    //nextProcess.AddRange(currentProcess);
                    //nextProcess.Add(specificElements[i].Commands);

                    //processQueue.Enqueue(nextProcess);
                    stateQueue.Enqueue(nextState.Commands);
                    lastIndexQueue.Enqueue(i);
                    visitedStates.Add(nextState.Commands);
                    tempQueue.Enqueue(tempCounter + 1);
                }
            }

            List<PermutationGroup> boxedProcess = new List<PermutationGroup>();
            /*foreach (int[] i in finalProcess)
            {
                boxedProcess.Add(new PermutationGroup(i));
            }*/

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

        private static int GetCount(PermutationGroup pg)
        {
            int count = 0;

            LinkedList<int> order = new LinkedList<int>();

            for (int i = 1; i <= pg.Count; ++i)
            {
                if (pg.Commands[i] < 0)
                {
                    count += i;
                    order.AddFirst(-pg.Commands[i]);
                }
                else
                {
                    order.AddLast(pg.Commands[i]);
                }
            }

            List<int> foreNum = new List<int>();

            foreach (int i in order)
            {
                //int cnum = pg.Commands[i] < 0 ? -pg.Commands[i] : pg.Commands[i];
                foreach (int j in foreNum)
                {
                    if (i < j)
                    {
                        ++count;
                    }
                }
                foreNum.Add(i);
            }

            return count;
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

            const int n = 4;

            StringBuilder stringBuilder = new StringBuilder();
            RegularSet regularSet = new RegularSet(n);
            List<PermutationGroup> allPermutationGroups = regularSet.GetAllPermutationGroups();

            /*foreach (var pg in allPermutationGroups)
            {
                stringBuilder.AppendLine($"{pg.Show()} : {NewGetCount(pg)}");
            }*/

            /*PermutationGroup pga = new PermutationGroup(new int[] { 0, -1, -2, 3 });
            PermutationGroup pgb = new PermutationGroup(new int[] { 0, -1, 2, -3 });

            stringBuilder.AppendLine($"{pga.ShowInCyclicNotation()}=> {pgb.ShowInCyclicNotation()}:\n");

            foreach (var pg in allPermutationGroups)
            {
                if (pga.GetConjugatedGroup(pg) == pgb)
                {
                    stringBuilder.AppendLine(pg.ShowInCyclicNotation());
                }
            }*/

            /*List<PermutationGroup> registedPermutationGroups = new List<PermutationGroup>();
            int Count = 0;
            foreach (PermutationGroup pg in allPermutationGroups)
            {
                if (!registedPermutationGroups.Contains(pg))
                {
                    ++Count;
                    registedPermutationGroups.Add(pg);
                    stringBuilder.AppendLine("进行操作的变换：");
                    stringBuilder.AppendLine(pg.Show());
                    List<PermutationGroup> allConjugatedGroups = pg.GetAllConjugatedGroups(allPermutationGroups);
                    //stringBuilder.AppendLine($"元素个数：{allConjugatedGroups.Count.ToString()}");
                    foreach (PermutationGroup pgg in allConjugatedGroups)
                    {
                        registedPermutationGroups.Add(pgg);
                        stringBuilder.AppendLine(pgg.Show());
                    }
                }
            }*/
            /*stringBuilder.AppendLine($"类的个数：{Count.ToString()}");*/

            List<PermutationGroup> basicElements = GenerateMinimalElements(n);
            PermutationGroup elementalGroup = new PermutationGroup(n);

            /*PermutationGroup pg = new PermutationGroup(new int[] { 0, -1, -2, -3, -4, -5 });

            stringBuilder.AppendLine($"目标：{pg.ShowInCyclicNotation()}");
            List<PermutationGroup> currentExpression = ExpressBySpecificElements(basicElements, elementalGroup, pg);
            stringBuilder.AppendLine($"过程：");
            stringBuilder.AppendLine($"（共 {currentExpression.Count} 步）");
            foreach (PermutationGroup pgg in currentExpression)
            {
                stringBuilder.Append(pgg.ShowInCyclicNotation());
            }
            stringBuilder.AppendLine();*/

            List<PermutationGroup>[] tempBuckets = new List<PermutationGroup>[100010];
            int tempMax = 0;

            foreach (PermutationGroup pg in allPermutationGroups)
            {
                ExpressBySpecificElements(basicElements, elementalGroup, pg);
                int tempNum = temp_counter;
                tempMax = tempMax < tempNum ? tempNum : tempMax;
                if (tempBuckets[tempNum] == null)
                {
                    tempBuckets[tempNum] = new List<PermutationGroup>();
                }
                tempBuckets[tempNum].Add(pg);
            }

            for (int i = tempMax; i >= 0; --i)
            {
                if (tempBuckets[i] != null)
                {
                    stringBuilder.AppendLine($"步数：{i}");
                    foreach (PermutationGroup pg in tempBuckets[i])
                    {
                        int yac = NewGetCount(pg);
                        stringBuilder.AppendLine($"{pg.Show()} : { (yac == i ? "TRUE" : yac.ToString()) }");
                    }
                    stringBuilder.AppendLine("　");
                }
            }

            /*foreach (PermutationGroup pg in allPermutationGroups)
            {
                stringBuilder.AppendLine($"目标：{pg.ShowInCyclicNotation()}");
                List<PermutationGroup> currentExpression = ExpressBySpecificElements(basicElements, elementalGroup, pg);
                stringBuilder.AppendLine($"过程：");
                stringBuilder.AppendLine($"（共 {currentExpression.Count} 步）");
                foreach (PermutationGroup pgg in currentExpression)
                {
                    stringBuilder.AppendLine(pgg.ShowInCyclicNotation());
                }
                stringBuilder.AppendLine();
            }*/

            using (StreamWriter sw = new StreamWriter(@"C:\Users\lihao\OneDrive\Documents\cache\output.txt"))
            {
                sw.Write(stringBuilder.ToString());
            }
        }

        private static int NewGetCount(PermutationGroup pg)
        {
            int[] fullCommand = new int[pg.Count * 2 + 1];
            for (int i = 1; i <= pg.Count; ++i)
            {
                fullCommand[i] = -pg.Commands[pg.Count - i + 1];
            }
            for (int i = pg.Count + 1; i <= pg.Count * 2; ++i)
            {
                fullCommand[i] = pg.Commands[i - pg.Count];
            }

            List<int> foreNum = new List<int>();
            int count = 0;

            for (int i = 1; i < fullCommand.Length; ++i)
            {
                int currentNum = fullCommand[i];
                foreach (int j in foreNum)
                {
                    if (currentNum < j)
                    {
                        ++count;
                    }
                }
                foreNum.Add(currentNum);
            }

            for (int i = 1; i <= pg.Count; ++i)
            {
                if (pg.Commands[i] < 0)
                {
                    ++count;
                }
            }

            return count / 2;
        }
    }
}