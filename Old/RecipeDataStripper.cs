using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Terraria;

namespace AccessoriesPlus.Old;
public static class RecipeDataStripper
{
    public static void StripData()
    {
        // Stripping the recipes
        Dictionary<string, List<string>> nodes = new();

        foreach (var recipe in Main.recipe)
        {
            // Checking if the created item is an accessory or an ingredient is an accessory
            bool shouldContinue = recipe.createItem.accessory;

            foreach (var item in recipe.requiredItem)
            {
                shouldContinue |= item.accessory;
            }

            if (!shouldContinue)
                continue;

            // Getting the name and children of the recipe
            string name = recipe.createItem.Name;
            var children = new List<string>();
            foreach (var item in recipe.requiredItem)
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

        string json = JsonSerializer.Serialize(nodes);
        string filePath = @"C:\Users\benho\Documents\My Games\Terraria\tModLoader\ModSources\AccessoriesPlus\recipes.json";
        File.WriteAllText(filePath, json);
    }
}
