using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerMaxHealth = 10; // максимальное количество здоровья игрока
    public int playerCurrentHealth; // текущее количество здоровья игрока

    private void Start()
    {
        playerCurrentHealth = playerMaxHealth; // устанавливаем текущее количество здоровья на максимальное значение при создании игрока
    }

    public void PlayerTakeDamage(int damage)
    {
        playerCurrentHealth -= damage; // отнимаем урон от здоровья игрока
        if (playerCurrentHealth <= 0) // если здоровье игрока меньше или равно нулю
        {
            PlayerDie(); // вызываем метод Die()
        }
    }

    public void PlayerDie()
    {
        // действия при смерти игрока
        Application.Quit(); // завершаем приложение
        Debug.Log("Соси жопу"); 
    }
}