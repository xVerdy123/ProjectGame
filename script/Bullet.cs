using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20; // урон пули
    public float knockbackForce = 10f; // сила отталкивани

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // если пуля попала во врага
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>(); // получаем скрипт врага
            if (enemy != null) // если скрипт найден
            {
                enemy.EnemyTakeDamage(damage); // наносим урон врагу

                Vector3 direction = collision.transform.position - transform.position;// отталкиваем врага
                direction = direction.normalized;
                Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                enemyRigidbody.AddForce(direction * knockbackForce, ForceMode.Impulse);
                Destroy(gameObject); // уничтожаем пулю
            }
        }
    }
}