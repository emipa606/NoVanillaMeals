using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RimWorld;
using Verse;

namespace NoVanillaMeals;

[StaticConstructorOnStartup]
internal static class NoVanillaMeals
{
    static NoVanillaMeals()
    {
        var vanillaMeals = (from ThingDef meal in DefDatabase<ThingDef>.AllDefsListForReading
            where meal is
            {
                IsIngestible: true, modContentPack: { IsOfficialMod: true },
                ingestible.foodType: FoodTypeFlags.Meal
            }
            select meal).ToList();

        foreach (var thingDef in vanillaMeals)
        {
            thingDef.destroyOnDrop = true;
            thingDef.generateCommonality = 0;
            thingDef.generateAllowChance = 0;
            thingDef.recipeMaker = null;
            thingDef.scatterableOnMapGen = false;
            thingDef.tradeability = Tradeability.None;
            thingDef.tradeTags?.Clear();
        }

        for (var i = vanillaMeals.Count - 1; i > 0; i--)
        {
            GenGeneric.InvokeStaticMethodOnGenericType(typeof(DefDatabase<>), typeof(ThingDef), "Remove",
                vanillaMeals[i]);
        }

        DefDatabase<ThingDef>.ResolveAllReferences();

        var mealRecipes = from recipe in DefDatabase<RecipeDef>.AllDefsListForReading
            where vanillaMeals.Contains(recipe.ProducedThingDef) || (from product in recipe.products
                where vanillaMeals.Contains(product.thingDef)
                select product).Any()
            select recipe;

        foreach (var mealRecipe in mealRecipes)
        {
            mealRecipe.factionPrerequisiteTags = new List<string> { "NotForYou" };
        }

        DefDatabase<RecipeDef>.ResolveAllReferences();

        var field = typeof(ScenPart_ThingCount).GetField("thingDef", BindingFlags.NonPublic |
                                                                     BindingFlags.Instance);

        foreach (var scenarioDef in DefDatabase<ScenarioDef>.AllDefsListForReading)
        {
            var allParts = scenarioDef.scenario.AllParts.ToList();
            // ReSharper disable once ForCanBeConvertedToForeach, changed during iteration
            for (var i = 0; i < allParts.Count; i++)
            {
                var part = allParts[i];
                try
                {
                    var thingCount = part as ScenPart_ThingCount;
                    if (vanillaMeals.Contains((ThingDef)field?.GetValue(thingCount)))
                    {
                        scenarioDef.scenario.RemovePart(part);
                    }
                }
                catch (Exception)
                {
                    //ignore
                }
            }
        }

        var instructions = DefDatabase<InstructionDef>.AllDefsListForReading;
        // ReSharper disable once ForCanBeConvertedToForeach, changed during iteration
        for (var index = 0; index < instructions.Count; index++)
        {
            var instructionDef = instructions[index];
            if (mealRecipes.Contains(instructionDef.recipeDef))
            {
                GenGeneric.InvokeStaticMethodOnGenericType(typeof(DefDatabase<>), typeof(InstructionDef), "Remove",
                    instructions[index]);
            }
        }

        Log.Message(
            $"[NoVanillaMeals]: Removed {vanillaMeals.Count} vanilla meals: {string.Join(",", vanillaMeals.Select(def => def.LabelCap))}");
    }
}