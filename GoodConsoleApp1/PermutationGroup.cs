using System;
using System.Collections.Generic;
using System.Text;

namespace UntitledProjectA
{
    internal class PermutationGroup
    {
        public int[] Commands;
        public int Count;

        public PermutationGroup(int[] commands)
        {
            Commands = commands;
            Count = Commands.Length - 1;
        }

        public static PermutationGroup operator *(PermutationGroup pga, PermutationGroup pgb)
        {
            PermutationGroup newPermutationGroup = new PermutationGroup(new int[pga.Count + 1]);
            for (int i = 1; i <= pga.Count; ++i)
            {
                if (pga.Commands[i] < 0)
                {
                    newPermutationGroup.Commands[i] = -pgb.Commands[-pga.Commands[i]];
                }
                else
                {
                    newPermutationGroup.Commands[i] = pgb.Commands[pga.Commands[i]];
                }
            }
            return newPermutationGroup;
        }

        public static bool operator <(PermutationGroup pga, PermutationGroup pgb)
        {
            for (int i = 1; i <= pga.Count; ++i)
            {
                if (pga.Commands[i] < pgb.Commands[i])
                {
                    return true;
                }
                else if (pga.Commands[i] > pgb.Commands[i])
                {
                    return false;
                }
            }
            return false;
        }

        public static bool operator >(PermutationGroup pga, PermutationGroup pgb)
        {
            for (int i = 1; i <= pga.Count; ++i)
            {
                if (pga.Commands[i] > pgb.Commands[i])
                {
                    return true;
                }
                else if (pga.Commands[i] < pgb.Commands[i])
                {
                    return false;
                }
            }
            return false;
        }

        public override bool Equals(Object pg)
        {
            if (pg.GetType() != GetType())
            {
                return false;
            }
            if ((pg as PermutationGroup).Count != Count)
            {
                return false;
            }
            for (int i = 1; i <= Count; ++i)
            {
                if (Commands[i] != (pg as PermutationGroup).Commands[i])
                {
                    return false;
                }
            }
            return true;
        }

        public List<PermutationGroup> GetAllConjugatedGroups(List<PermutationGroup> otherGroups)
        {
            List<PermutationGroup> allConjugatedGroups = new List<PermutationGroup>();
            foreach (PermutationGroup pg in otherGroups)
            {
                PermutationGroup currentGroup = GetConjugatedGroup(pg);
                if (!allConjugatedGroups.Contains(currentGroup))
                {
                    allConjugatedGroups.Add(currentGroup);
                }
            }
            return allConjugatedGroups;
        }

        public PermutationGroup GetConjugatedGroup(PermutationGroup anotherGroup)
        {
            return anotherGroup * this * anotherGroup.Reverse();
        }

        public PermutationGroup Reverse()
        {
            int[] inverseCommands = new int[Count + 1];
            for (int i = 1; i <= Count; ++i)
            {
                if (Commands[i] < 0)
                {
                    inverseCommands[-Commands[i]] = -i;
                }
                else
                {
                    inverseCommands[Commands[i]] = i;
                }
            }
            return new PermutationGroup(inverseCommands);
        }

        public void Show()
        {
            for (int i = Count; i >= 1; --i)
            {
                Console.Write((-Commands[i]).ToString() + " ");
            }
            for (int i = 1; i <= Count; ++i)
            {
                Console.Write((Commands[i]).ToString() + " ");
            }
        }

        public void ShowInCyclicNotation()
        {
            PermutationGroup mirrorPermutationGroup = new PermutationGroup(new int[Count + 1]);
            for (int i = 1; i <= Count; ++i)
            {
                mirrorPermutationGroup.Commands[i] = -Commands[i];
            }
            bool[] isUsedOriginal = new bool[Count + 1];
            bool[] isUsedMirror = new bool[Count + 1];
            List<List<int>> cyclesList = new List<List<int>>();
            for (int i = Count; i >= 1; --i)
            {
                if ((!isUsedMirror[i]) && (mirrorPermutationGroup.Commands[i] != -i))
                {
                    List<int> currentList = new List<int>();
                    int next = mirrorPermutationGroup.Commands[i];
                    currentList.Add(-i);
                    isUsedMirror[i] = true;
                    while (next != -i)
                    {
                        currentList.Add(next);
                        if (next > 0)
                        {
                            isUsedOriginal[next] = true;
                            next = Commands[next];
                        }
                        else
                        {
                            isUsedMirror[-next] = true;
                            next = mirrorPermutationGroup.Commands[-next];
                        }
                    }
                    cyclesList.Add(currentList);
                }
            }
            for (int i = 1; i <= Count; ++i)
            {
                if ((!isUsedOriginal[i]) && (Commands[i] != i))
                {
                    List<int> currentList = new List<int>();
                    int next = Commands[i];
                    currentList.Add(i);
                    isUsedOriginal[i] = true;
                    while (next != i)
                    {
                        currentList.Add(next);
                        if (next > 0)
                        {
                            isUsedOriginal[next] = true;
                            next = Commands[next];
                        }
                        else
                        {
                            isUsedMirror[-next] = true;
                            next = mirrorPermutationGroup.Commands[-next];
                        }
                    }
                    cyclesList.Add(currentList);
                }
            }
            foreach (List<int> vs in cyclesList)
            {
                Console.Write("( ");
                foreach (int i in vs)
                {
                    Console.Write(i.ToString() + " ");
                }
                Console.Write(") ");
            }
        }
    }
}