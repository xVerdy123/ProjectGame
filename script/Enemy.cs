using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxEnemyHealth = 100; // максимальное количество здоровья врагаfsaasf
    public int currentEnemyHealth; // текущее количество здоровья врага

    public GameObject AmmoPrefab; // Префаб патронов
    public GameObject enemyBulletPrefab; // префаб пули

    public Transform player; // ссылка на игрока
    public Transform enemyBulletSpawn;

    public float enemyMoveSpeed = 3f; // скорость движения врага
    public float rotationSpeed = 1f;
    public float enemyBulletSpeed = 10f; // скорость пули
    public float enemyFireRate = 0.5f; // частота стрельбы
    private float nextFireTime = 0f; // время следующего выстрела

    public AudioClip enemyFireSound; //звук выстрела врага
    public AudioClip dieEnemySound; // звук смерти врага

    public void Start()
    {
        currentEnemyHealth = maxEnemyHealth; // устанавливаем текущее количество здоровья на максимальное значение при создании врага
    }

    private void Update()
    {
        // вычисляем направление на игрока
        Vector3 direction = player.position - transform.position;
        direction.y = 0f; // обнуляем направление по оси Y

        // вычисляем угол между направлением и осью X
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // поворачиваем врага к игроку
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.position += transform.forward * enemyMoveSpeed * Time.deltaTime;// двигаем врага в направлении игрока

        // проверяем, можно ли стрелять
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + enemyFireRate;//устанавливаем время до следующего выстрела
            EnemyFire();
        }
    }

    public void EnemyFire()
    {
        GameObject enemyBullet = Instantiate(enemyBulletPrefab, enemyBulletSpawn.position, enemyBulletSpawn.rotation);// создаем новую пулю
        Rigidbody rb = enemyBullet.GetComponent<Rigidbody>();// получаем компонент Rigidbody пули
        rb.velocity = enemyBulletSpawn.forward * enemyBulletSpeed;// устанавливаем скорость пули
        AudioSource.PlayClipAtPoint(enemyFireSound, transform.position);// воспроизводим звук выстрела

        Destroy(enemyBullet, 5f); // уничтожаем пулю через 7 секунд
    }

    public void EnemyTakeDamage(int damage)
    {
        currentEnemyHealth -= damage; // отнимаем урон от здоровья врага
        if (currentEnemyHealth <= 0) // если здоровье врага меньше или равно нулю
        {
            EnemyDie(); // вызываем метод Die()
        }
    }

    public void EnemyDie()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, 1f, transform.position.z);
        Instantiate(AmmoPrefab, spawnPosition, transform.rotation);
        Destroy(gameObject); // уничтожаем объект врага
        AudioSource.PlayClipAtPoint(dieEnemySound, transform.position); // запускаем звук смерти врага
    }
}