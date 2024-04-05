using WhaleSpotting.Models.Data;

namespace WhaleSpotting.Helpers;

public static class AchievementHelper
{
    public static Achievement GetAchievementForExperience(List<Achievement> achievements, int experience)
    {
        return achievements
            .Where(achievement => achievement.MinExperience <= experience)
            .OrderBy(achievement => achievement.MinExperience)
            .Last();
    }
}
