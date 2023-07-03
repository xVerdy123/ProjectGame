using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int enemyBulletDamage = 1; // ���������� �����, ���������� ����� �����
    public AudioClip hitPlayerSound; // ���� ��������� ���� � ������

    public void OnCollisionEnter(Collision collision)
    {
        // ���������, ����������� �� ���� � �������
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>(); // �������� ������ ������
            if (playerHealth != null) // ���� ������ ������
            {
                playerHealth.PlayerTakeDamage(enemyBulletDamage); // ������� ���� ������

                Destroy(gameObject); // ���������� ����
                AudioSource.PlayClipAtPoint(hitPlayerSound, transform.position); // ��������� ���� ��������� ���� � ������
            }
        }
        else if (!collision.gameObject.CompareTag("Enemy")) // ���� ���� ������ � ������ ������
        {
            Destroy(gameObject); // ���������� ����
        }
    }

}