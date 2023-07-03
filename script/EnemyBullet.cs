using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int enemyBulletDamage = 1; // количество урона, наносимого пулей врага
    public AudioClip hitPlayerSound; // звук попадания пули в игрока

    public void OnCollisionEnter(Collision collision)
    {
        // проверяем, столкнулась ли пуля с игроком
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>(); // получаем скрипт игрока
            if (playerHealth != null) // если скрипт найден
            {
                playerHealth.PlayerTakeDamage(enemyBulletDamage); // наносим урон игроку

                Destroy(gameObject); // уничтожаем пулю
                AudioSource.PlayClipAtPoint(hitPlayerSound, transform.position); // запускаем звук попадания пули в игрока
            }
        }
        else if (!collision.gameObject.CompareTag("Enemy")) // если пуля попала в другой объект
        {
            Destroy(gameObject); // уничтожаем пулю
        }
    }

}