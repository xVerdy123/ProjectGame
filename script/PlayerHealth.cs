using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerMaxHealth = 10; // ������������ ���������� �������� ������
    public int playerCurrentHealth; // ������� ���������� �������� ������

    private void Start()
    {
        playerCurrentHealth = playerMaxHealth; // ������������� ������� ���������� �������� �� ������������ �������� ��� �������� ������
    }

    public void PlayerTakeDamage(int damage)
    {
        playerCurrentHealth -= damage; // �������� ���� �� �������� ������
        if (playerCurrentHealth <= 0) // ���� �������� ������ ������ ��� ����� ����
        {
            PlayerDie(); // �������� ����� Die()
        }
    }

    public void PlayerDie()
    {
        // �������� ��� ������ ������
        Application.Quit(); // ��������� ����������
        Debug.Log("���� ����"); 
    }
}