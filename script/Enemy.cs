using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxEnemyHealth = 100; // ������������ ���������� �������� �����fsaasf
    public int currentEnemyHealth; // ������� ���������� �������� �����

    public GameObject AmmoPrefab; // ������ ��������
    public GameObject enemyBulletPrefab; // ������ ����

    public Transform player; // ������ �� ������
    public Transform enemyBulletSpawn;

    public float enemyMoveSpeed = 3f; // �������� �������� �����
    public float rotationSpeed = 1f;
    public float enemyBulletSpeed = 10f; // �������� ����
    public float enemyFireRate = 0.5f; // ������� ��������
    private float nextFireTime = 0f; // ����� ���������� ��������

    public AudioClip enemyFireSound; //���� �������� �����
    public AudioClip dieEnemySound; // ���� ������ �����

    public void Start()
    {
        currentEnemyHealth = maxEnemyHealth; // ������������� ������� ���������� �������� �� ������������ �������� ��� �������� �����
    }

    private void Update()
    {
        // ��������� ����������� �� ������
        Vector3 direction = player.position - transform.position;
        direction.y = 0f; // �������� ����������� �� ��� Y

        // ��������� ���� ����� ������������ � ���� X
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // ������������ ����� � ������
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.position += transform.forward * enemyMoveSpeed * Time.deltaTime;// ������� ����� � ����������� ������

        // ���������, ����� �� ��������
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + enemyFireRate;//������������� ����� �� ���������� ��������
            EnemyFire();
        }
    }

    public void EnemyFire()
    {
        GameObject enemyBullet = Instantiate(enemyBulletPrefab, enemyBulletSpawn.position, enemyBulletSpawn.rotation);// ������� ����� ����
        Rigidbody rb = enemyBullet.GetComponent<Rigidbody>();// �������� ��������� Rigidbody ����
        rb.velocity = enemyBulletSpawn.forward * enemyBulletSpeed;// ������������� �������� ����
        AudioSource.PlayClipAtPoint(enemyFireSound, transform.position);// ������������� ���� ��������

        Destroy(enemyBullet, 5f); // ���������� ���� ����� 7 ������
    }

    public void EnemyTakeDamage(int damage)
    {
        currentEnemyHealth -= damage; // �������� ���� �� �������� �����
        if (currentEnemyHealth <= 0) // ���� �������� ����� ������ ��� ����� ����
        {
            EnemyDie(); // �������� ����� Die()
        }
    }

    public void EnemyDie()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, 1f, transform.position.z);
        Instantiate(AmmoPrefab, spawnPosition, transform.rotation);
        Destroy(gameObject); // ���������� ������ �����
        AudioSource.PlayClipAtPoint(dieEnemySound, transform.position); // ��������� ���� ������ �����
    }
}