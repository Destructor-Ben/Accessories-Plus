using Terraria.ModLoader;
using System.Collections.Generic;

namespace AccessoriesPlus
{
	public class AccessoriesPlus : Mod
	{
        public static AccessoriesPlus instance;

        public static List<int> hookItemIDS;
        public static List<int> wingItemIDS;

        public static List<string[]> hookItemStats;
        public static List<string[]> wingItemStats;

        public override void Load()
        {
            instance = this;

            hookItemIDS = new List<int>()
            {
                84,
                1236,
                4759,
                1237,
                1238,
                1239,
                1240,
                4257,
                1241,
                939,
                1273,
                2585,
                2360,
                185,
                1800,
                1915,
                437,
                4980,
                3021,
                3022,
                3023,
                3020,
                2800,
                1829,
                1916,
                3572,
                3623
            };
            wingItemIDS = new List<int>()
            {
                4978,
                493,
                492,
                1162,
                761,
                2494,
                822,
                785,
                748,
                665,
                1583,
                1584,
                1585,
                1586,
                3228,
                3580,
                3582,
                3588,
                3592,
                3924,
                3928,
                4730,
                4746,
                4750,
                4754,
                1165,
                1515,
                749,
                821,
                1866,
                786,
                2770,
                823,
                2280,
                1871,
                1830,
                1797,
                948,
                3883,
                4823,
                2609,
                3470,
                3469,
                3468,
                3471,
                4954
            };

            hookItemStats = new List<string[]>()
            {
                new string[]{ "18.75", "11.5", "1", "Single" },
                new string[]{ "18.75", "10", "1", "Single" },
                new string[]{ "19", "11.5", "1", "Single" },
                new string[]{ "20.625", "10.5", "1", "Single" },
                new string[]{ "22.5", "11", "1", "Single" },
                new string[]{ "24.375", "11.5", "1", "Single" },
                new string[]{ "26.25", "12", "1", "Single" },
                new string[]{ "27.5", "12.5", "1", "Single" },
                new string[]{ "29.125", "12.5", "1", "Single" },
                new string[]{ "different", "different", "1", "different" },
                new string[]{ "21.875", "15", "2", "Simultaneous" },
                new string[]{ "18.75", "13", "3", "Simultaneous" },
                new string[]{ "25", "13", "2", "Simultaneous" },
                new string[]{ "28", "13", "3", "Simultaneous" },
                new string[]{ "31.25", "13.5", "1", "Single" },
                new string[]{ "25", "11.5", "1", "Single" },
                new string[]{ "27.5", "14", "2", "Individual" },
                new string[]{ "30", "16", "1", "Single" },
                new string[]{ "30", "16", "3", "Simultaneous" },
                new string[]{ "30", "15", "3", "Simultaneous" },
                new string[]{ "30", "15", "3", "Simultaneous" },
                new string[]{ "30", "15", "3", "Simultaneous" },
                new string[]{ "31.25", "14", "3", "Simultaneous" },
                new string[]{ "34.375", "15.5", "3", "Simultaneous" },
                new string[]{ "34.375", "15.5", "3", "Simultaneous" },
                new string[]{ "34.375", "18", "4", "Simultaneous" },
                new string[]{ "37.5", "16", "2", "Individual" },
            };
            wingItemStats = new List<string[]>()
            {
                new string[]{ "0.42", "18", "15"},
                new string[]{ "1.67", "53", "32"},
                new string[]{ "1.67", "53", "32"},
                new string[]{ "1.67", "52", "32"},
                new string[]{ "2.17", "67", "34"},
                new string[]{ "2.17", "67", "34"},
                new string[]{ "2.17", "67", "34"},
                new string[]{ "2.17", "67", "34"},
                new string[]{ "2.5", "77", "33"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.5", "77", "36"},
                new string[]{ "2.67", "81", "38"},
                new string[]{ "2.67", "81", "38"},
                new string[]{ "2.67", "81", "38"},
                new string[]{ "2.67", "81", "38"},
                new string[]{ "2.83", "94", "38"},
                new string[]{ "2.83", "94", "38"},
                new string[]{ "2.83", "94", "38"},
                new string[]{ "2.83", "94", "38"},
                new string[]{ "2.83", "94", "38"},
                new string[]{ "3", "107", "38"},
                new string[]{ "3", "107", "38"},
                new string[]{ "3", "107", "38"},
                new string[]{ "3", "107", "38"},
                new string[]{ "2.5", "119", "250"},
                new string[]{ "2.5", "128", "41"},
                new string[]{ "3", "143", "41"},
                new string[]{ "3", "143", "33"},
                new string[]{ "3", "143", "33"},
                new string[]{ "3", "167", "46"},
                new string[]{ "3", "167", "46"},
                new string[]{ "3", "201", "41"}
            };

            base.Load();
        }

        public override void Unload()
        {
            instance = null;
            base.Unload();
        }

        // Returns true if the item type is found in the hook IDs list
        public static bool IsItemHook(int itemType)
        {
            foreach (int i in hookItemIDS)
            {
                if (i == itemType)
                {
                    return true;
                }
            }
            return false;
        }

        // Returns true if the item type is found in the wing IDs list
        public static bool IsItemWings(int itemType)
        {
            foreach (int i in wingItemIDS)
            {
                if (i == itemType)
                {
                    return true;
                }
            }
            return false;
        }

        // Returns an array of 4 strings that represent the hooks stats
        public static string[] GetHookStats(int itemID)
        {
            return hookItemStats[IDToIndex(itemID)];
        }

        // Returns an array of 4 strings that represent the hooks stats
        public static string[] GetWingStats(int itemID)
        {
            return wingItemStats[IDToIndex(itemID)];
        }

        // Converts an item ID to an index for the stats arrays
        public static int IDToIndex(int id)
        {
            int index = -1;

            if (IsItemHook(id))
            {
                for (int i = 0; i < hookItemIDS.Count; i++)
                {
                    if (hookItemIDS[i] == id)
                    {
                        index = i;
                        break;
                    }
                }
            }
            if (IsItemWings(id))
            {
                for (int i = 0; i < wingItemIDS.Count; i++)
                {
                    if (wingItemIDS[i] == id)
                    {
                        index = i;
                        break;
                    }
                }
            }

            return index;
        }
    }
}