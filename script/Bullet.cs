using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20; // ���� ����
    public float knockbackForce = 10f; // ���� �����������

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // ���� ���� ������ �� �����
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>(); // �������� ������ �����
            if (enemy != null) // ���� ������ ������
            {
                enemy.EnemyTakeDamage(damage); // ������� ���� �����

                Vector3 direction = collision.transform.position - transform.position;// ����������� �����
                direction = direction.normalized;
                Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                enemyRigidbody.AddForce(direction * knockbackForce, ForceMode.Impulse);
                Destroy(gameObject); // ���������� ����
            }
        }
    }
}