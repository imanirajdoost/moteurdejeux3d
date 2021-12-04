using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks if player has collected enough coins to unlock new levels
/// By Iman IRAJ DOOST
/// </summary>
public class LevelsManager : MonoBehaviour
{
<<<<<<< HEAD
    //[SerializeField] private int[] levelPrice;      //No. of coins required for each level to be unlocked starting at index 0
=======
    [SerializeField] private int[] levelPrice;      //No. of coins required for each level to be unlocked starting at index 0
>>>>>>> 1048c7c5395fbd75e591693ebe0f6a57585e17aa

    /// <summary>
    /// Checks if level is unlocked based on player's current coin count
    /// In debug mode it always returns true
    /// </summary>
    /// <param name="levelIndex">Level index</param>
    /// <returns>returns true if player has enough coins for the given level</returns>
    public bool IsLevelUnlocked(int levelIndex)
    {
<<<<<<< HEAD
        if (GameManager.instance.nbcoin >= Generator.selectedEnvIndex * 10)
            return true;
        else
            return false;
            
=======
        if (GameManager.instance.isDebug)
        {
            return true;
        }
        else
        {
            if (levelIndex < levelPrice.Length)
            {
                if (GetCurrentPlayerCoinCount() >= levelPrice[levelIndex])
                    return true;
            }

            return false;
        }
>>>>>>> 1048c7c5395fbd75e591693ebe0f6a57585e17aa
    }

    /// <summary>
    /// Returns number of coins needed to unlock level
    /// </summary>
    /// <param name="levelIndex">Level index</param>
    /// <returns>number of needed coins</returns>
    public int GetCoinsNeededForLevel(int levelIndex)
    {
<<<<<<< HEAD
        return Generator.selectedEnvIndex * 10;
=======
        return levelPrice[levelIndex];
>>>>>>> 1048c7c5395fbd75e591693ebe0f6a57585e17aa
    }

    /// <summary>
    /// Returns current coin count of player
    /// </summary>
    /// <returns></returns>
    private int GetCurrentPlayerCoinCount()
    {
        return GameManager.instance.GetCurrentCoinCount();
    }
}
