using Cook.Level;
using Cook.Recipe;
using System;
using System.Collections.Generic;

[Serializable]
public class GameLibraryData
{
    public GameLibraryData()
    {
        levels = new List<LevelData>();
        levels.Add(LevelData.CreateTestLevelData());

        CurrentLevel = 0;
        Recipes = new List<Recipe>();
    }

    public List<LevelData> levels;
    public int CurrentLevel;
    public List<Recipe> Recipes;

    public LevelData GetCurrentLevelData() => levels[CurrentLevel];
}