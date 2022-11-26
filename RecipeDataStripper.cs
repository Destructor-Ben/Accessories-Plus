using Terraria;

using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace AccessoriesPlus
{
    public static class RecipeDataStripper
    {
        public static void StripData(Recipe[] recipes, string fileName, AccessoriesPlus ap)
        {
            // Stripping the recipes
            ap.Logger.Info("Starting recipe stripping...");

            Dictionary<string, List<string>> nodes = new();
            
            foreach (Recipe recipe in recipes)
            {
                // Checking if the created item is an accessory or an ingredient is an accessory
                bool shouldContinue = recipe.createItem.accessory;

                foreach (Item item in recipe.requiredItem)
                {
                    shouldContinue = shouldContinue || item.accessory;
                    if (shouldContinue)
                        break;
                }

                if (!shouldContinue)
                    continue;

                // Getting the name and children of the recipe
                string name = recipe.createItem.Name;
                var children = new List<string>();
                foreach (Item item in recipe.requiredItem)
                {
                    children.Add(item.Name);
                }
                
                // Adding the children to the list of nodes
                foreach (string child in children)
                {
                    if (nodes.ContainsKey(child))
                    {
                        nodes[child].Add(name);
                    }
                    else
                    {
                        nodes.Add(child, new List<string>() { name });
                    }
                }
            }

            ap.Logger.Info("Finished stripping recipes");


            // Saving the recipes to a json file
            ap.Logger.Info("Saving recipes...");

            string json = JsonSerializer.Serialize(nodes);
            string filePath = fileName;//@"C:\Users\benho\Documents\My Games\Terraria\tModLoader\ModSources\AccessoriesPlus\" + fileName;
            File.WriteAllText(filePath, json);

            ap.Logger.Info("Recipes saved");
        }
    }
}
