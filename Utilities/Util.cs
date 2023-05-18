namespace AccessoriesPlus.Utilities;
internal static class Util
{
    /// <summary>
    /// Finds the index of the tooltip name
    /// </summary>
    /// <param name="tooltipName"></param>
    /// <param name="tooltips"></param>
    /// <returns></returns>
    public static int FindIndexOfTooltipName(this List<TooltipLine> tooltips, string tooltipName)
    {
        for (int i = 0; i < tooltips.Count; i++)
        {
            if (tooltips[i].Name == tooltipName)
                return i;
        }

        return -1;
    }

    /// <summary>
    /// Inserts the tooltips before or after the specified tooltip name
    /// </summary>
    /// <param name="tooltips"></param>
    /// <param name="name"></param>
    /// <param name="after"></param>
    /// <param name="tooltipsToInsert"></param>
    /// <returns></returns>
    public static void InsertTooltips(this List<TooltipLine> tooltips, string name, bool after, params TooltipLine[] tooltipsToInsert)
    {
        int index = tooltips.FindIndexOfTooltipName(name);
        if (index != -1)
        {
            tooltips.InsertRange(after ? index + 1 : index, tooltipsToInsert);
        }
        else
        {
            AccessoriesPlus.Instance.Logger.Warn("Unable to find index of tooltip line: " + name);
        }
    }

    /// <summary>
    /// Removes the list of tooltips
    /// </summary>
    /// <param name="tooltips"></param>
    /// <param name="tooltipNames"></param>
    public static void RemoveTooltips(this List<TooltipLine> tooltips, params string[] tooltipNames)
    {
        var temp = new List<TooltipLine>();
        foreach (var tooltip in tooltips)
        {
            if (tooltipNames.Contains(tooltip.Name))
                temp.Add(tooltip);
        }

        foreach (var item in temp)
        {
            tooltips.Remove(item);
        }
    }

    /// <summary>
    /// Returns a localized tooltipline
    /// </summary>
    /// <param name="name"></param>
    /// <param name="stringFormat"></param>
    /// <returns></returns>
    public static TooltipLine GetTooltipLine(string name, params object[] stringFormat)
    {
        return new TooltipLine(AccessoriesPlus.Instance, "AccessoriesPlus:" + name, GetTextValue("Tooltips." + name, stringFormat));
    }

    /// <summary>
    /// Returns a localized text
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static LocalizedText GetText(string key)
    {
        return Language.GetText("Mods.AccessoriesPlus." + key);
    }

    /// <summary>
    /// Returns a localized text string with the formatting
    /// </summary>
    /// <param name="key"></param>
    /// <param name="stringFormat"></param>
    /// <returns></returns>
    public static string GetTextValue(string key, params object[] stringFormat)
    {
        return GetText(key).Format(stringFormat);
    }

    /// <summary>
    /// Removes all of the recipes for an item type that aren't created by this mod
    /// </summary>
    /// <param name="type"></param>
    public static void RemoveRecipesForItem(int type)
    {
        foreach (var recipe in Main.recipe)
        {
            if (recipe.Mod != AccessoriesPlus.Instance && recipe.createItem.type == type)
                recipe.DisableRecipe();
        }
    }

    /// <summary>
    /// Applies the specified items equip effects to the player
    /// </summary>
    /// <param name="player"></param>
    /// <param name="itemType"></param>
    /// <param name="hideVisual"></param>
    public static void CopyVanillaEquipEffects(this Player player, int itemType, bool hideVisual)
    {
        var item = new Item();
        item.SetDefaults(itemType);
        player.ApplyEquipFunctional(item, hideVisual);
    }

    /// <summary>
    /// Registers a recipe group with the specified internal name and items
    /// </summary>
    /// <param name="name"></param>
    /// <param name="itemIds"></param>
    /// <returns></returns>
    public static RecipeGroup RegisterRecipeGroup(string name, params int[] itemIds)
    {
        var group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(itemIds[0]), itemIds);
        RecipeGroup.RegisterGroup("AccessoriesPlus:" + name, group);
        return group;
    }

    // TODO - asset requesting
}