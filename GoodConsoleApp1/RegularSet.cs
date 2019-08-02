using System;
using System.Collections.Generic;
using System.Text;

namespace UntitledProjectA
{
    internal class RegularSet
    {
        public int Count;
        public int[] OriginalList;
        public int[] ReversedList;

        public RegularSet(int count, bool needInit = true)
        {
            OriginalList = new int[count + 1];
            ReversedList = new int[count + 1];
            Count = count;
            if (needInit)
            {
                for (int i = 0; i < OriginalList.Length; ++i)
                {
                    OriginalList[i] = i;
                    ReversedList[i] = -i;
                }
            }
        }

        public static RegularSet operator *(RegularSet rs, PermutationGroup pg)
        {
            RegularSet newRegularSet = new RegularSet(rs.Count, false);
            for (int i = 1; i <= pg.Count; ++i)
            {
                if (pg.Commands[i] < 0)
                {
                    newRegularSet.ReversedList[-pg.Commands[i]] = rs.OriginalList[i];
                    newRegularSet.OriginalList[-pg.Commands[i]] = rs.ReversedList[i];
                }
                else
                {
                    newRegularSet.OriginalList[pg.Commands[i]] = rs.OriginalList[i];
                    newRegularSet.ReversedList[pg.Commands[i]] = rs.ReversedList[i];
                }
            }
            return newRegularSet;
        }

        public List<PermutationGroup> GetAllPermutationGroups()
        {
            List<PermutationGroup> allPermutationGroups = new List<PermutationGroup>();
            bool[] isUsedOriginal = new bool[Count + 1];
            bool[] isUsedReversed = new bool[Count + 1];
            int[] rawPermutatoinGroupList = new int[Count + 1];
            Fetch(1, ref rawPermutatoinGroupList, ref isUsedOriginal, ref isUsedReversed, ref allPermutationGroups);
            return allPermutationGroups;
        }

        private void Fetch(int depth, ref int[] rawList, ref bool[] isUsedOriginal, ref bool[] isUsedReversed, ref List<PermutationGroup> finalList)
        {
            if (depth <= Count)
            {
                for (int i = Count; i >= 1; --i)
                {
                    if (!isUsedReversed[i])
                    {
                        rawList[depth] = -i;
                        isUsedOriginal[i] =
                            isUsedReversed[i] = true;
                        Fetch(depth + 1, ref rawList, ref isUsedOriginal, ref isUsedReversed, ref finalList);
                        isUsedOriginal[i] =
                            isUsedReversed[i] = false;
                    }
                }
                for (int i = 1; i <= Count; ++i)
                {
                    if (!isUsedOriginal[i])
                    {
                        rawList[depth] = i;
                        isUsedOriginal[i] =
                            isUsedReversed[i] = true;
                        Fetch(depth + 1, ref rawList, ref isUsedOriginal, ref isUsedReversed, ref finalList);
                        isUsedOriginal[i] =
                            isUsedReversed[i] = false;
                    }
                }
            }
            else
            {
                int[] copiedList = new int[Count + 1];
                rawList.CopyTo(copiedList, 0);
                finalList.Add(new PermutationGroup(copiedList));
            }
        }
    }
}