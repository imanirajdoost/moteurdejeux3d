using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks if player has collected enough coins to unlock new levels
/// By Iman IRAJ DOOST and Ahmad JREDA
/// </summary>
public class LevelsManager : MonoBehaviour
{
    //[SerializeField] private int[] levelPrice;      //No. of coins required for each level to be unlocked starting at index 0

    /// <summary>
    /// Checks if level is unlocked based on player's current coin count
    /// In debug mode it always returns true
    /// </summary>
    /// <param name="levelIndex">Level index</param>
    /// <returns>returns true if player has enough coins for the given level</returns>
    public bool IsLevelUnlocked(int levelIndex)
    {
        if (GameManager.instance.isDebug)
            return true;

        if (GameManager.instance.nbcoin >= Generator.selectedEnvIndex * 10)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Returns number of coins needed to unlock level
    /// </summary>
    /// <param name="levelIndex">Level index</param>
    /// <returns>number of needed coins</returns>
    public int GetCoinsNeededForLevel(int levelIndex)
    {
        return Generator.selectedEnvIndex * 10;
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
