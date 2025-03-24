using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerUp", menuName = "Game/PowerUp")]
public class PowerUp : ScriptableObject
{
    [Header("Power-Up Info")]
    public string powerUpName;
    public Sprite powerUpImage;
    [TextArea]
    public string description;
}
