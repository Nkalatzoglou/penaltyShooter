using UnityEngine;

public class PenaltyManager : MonoBehaviour
{
    public static PenaltyManager instance;

    public int consecutiveGoals = 0;
    public int consecutiveMisses = 0;
    public float difficultyLevel = 0.5f; // 0 = easy, 1 = hard

    public bool isSpecialShotActive;

    private float originalPropablity;
    private bool wasSpecialActiveLastShot = false;

    void Awake()
    {
        instance = this;
    }


    public void OnPlayerShoots(bool goalScored)
    {
        int currentShot = ScoreController.instance.ShootCounter;
        int totalShots = 20;
        int goalCounter = GameManager.gameManager.goal_Counter;

        // First shot: neutral random (no difficulty applied yet)
        if (currentShot == 0)
        {
            float firstRandom = Random.Range(0f, 1f);
            GameManager.gameManager.saveShoot = firstRandom < 0.5f;
            GameManager.gameManager.LooseShoot = !GameManager.gameManager.saveShoot;

            // Only track streaks (local to this script)
            if (!GameManager.gameManager.saveShoot)
            {
                consecutiveGoals = 1;
                consecutiveMisses = 0;
            }
            else
            {
                consecutiveGoals = 0;
                consecutiveMisses = 1;
            }

            Debug.Log("Shot 1 - Neutral Random. Save: " + GameManager.gameManager.saveShoot);
            return;
        }

        // Update local streaks based on passed goal result
        if (goalScored)
        {
            consecutiveGoals++;
            consecutiveMisses = 0;
        }
        else
        {
            consecutiveMisses++;
            consecutiveGoals = 0;
        }

        AdjustDifficulty(currentShot, totalShots);
        DecideGoalkeeperBehavior(currentShot, totalShots, goalCounter);
    }

    private void AdjustDifficulty(int currentShot, int totalShots)
    {
        // Hot streak: 3+ goals
        if (consecutiveGoals >= 3)
        {
            difficultyLevel = Mathf.Clamp(difficultyLevel + 0.1f, 0f, 1f);
            consecutiveGoals = 0;
        }

        // Cold streak: 2+ misses
        if (consecutiveMisses >= 2)
        {
            difficultyLevel = Mathf.Clamp(difficultyLevel - 0.1f, 0f, 1f);
            consecutiveMisses = 0;
        }

        // Gradual progression scaling
        float progressionFactor = (float)currentShot / totalShots;
        difficultyLevel = Mathf.Clamp(difficultyLevel + progressionFactor * 0.1f, 0f, 1f);

       // Only store the original value once when entering special mode
        if (isSpecialShotActive && !wasSpecialActiveLastShot)
        {
            originalPropablity = difficultyLevel;
            wasSpecialActiveLastShot = true;

            if (difficultyLevel > 0.7f)
                difficultyLevel -= 0.7f;
            else if (difficultyLevel < 0.5f)
                difficultyLevel -= 0.3f;
            else
                difficultyLevel -= 0.1f;

            difficultyLevel = Mathf.Clamp(difficultyLevel, 0f, 1f);
        }

        // Reset to original difficulty when special shot is deactivated
        if (!isSpecialShotActive && wasSpecialActiveLastShot)
        {
            difficultyLevel = originalPropablity;
            wasSpecialActiveLastShot = false;
        }
        
    }

    public void DecideGoalkeeperBehavior(int currentShot, int totalShots, int goalCounter)
    {
        float saveChance = difficultyLevel;

        int remainingShots = totalShots - currentShot;
        int maxPossibleGoals = goalCounter + remainingShots;

        // Drama mode logic for final 5 shots
        if (currentShot >= totalShots - 5)
        {
            if (maxPossibleGoals <= 12)
            {
                saveChance = Mathf.Clamp(0.4f + Random.Range(-0.1f, 0.1f), 0f, 1f);
            }
            else
            {
                saveChance = Mathf.Clamp(0.6f + Random.Range(-0.1f, 0.1f), 0f, 1f);
            }
        }

        float randomValue = Random.Range(0f, 1f);
        GameManager.gameManager.saveShoot = randomValue < saveChance;
        GameManager.gameManager.LooseShoot = !GameManager.gameManager.saveShoot;

        Debug.Log($"[DRAMA] Shot {currentShot}/{totalShots} | Goals: {goalCounter} | SaveChance: {saveChance:F2} | Save: {GameManager.gameManager.saveShoot}");
    }
}
