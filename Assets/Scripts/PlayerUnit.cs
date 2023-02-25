using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    public int Health { get; private set; }
    public bool IsAlive { get => Health == 0; }

    public PlayerUnit(int health)
    {
        Health = health;
    }

    public void TakeDamage()
    {
        Health -= 1;
    }
}
