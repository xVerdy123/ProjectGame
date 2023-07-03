using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour // ��� ��� ������
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float fireRate = 0.5f;//���������������� ������
    public int maxAmmo = 10;// ������������ ���������� �������� � ��������
    public int allAmmo = 30;// ����� �������� � ���������
    public int maxAllAmmo = 50;// �������� �������� � ���������
    public int currentAmmo;// ������� ���������� �������� � ��������
    private float nextFire = 0.0f;//����� �� ���������� ��������
    public float reloadTime = 4f; // ����� �����������
    public bool isReloading = false; // ���� �����������
    public AudioClip reloadSound; // ���� �����������
    public AudioClip amptySound; // ���� ���������� ��������
    public AudioClip fireSound; // ���� ��������
    public float bulletSpeed = 35f;//�������� ����
    public AudioClip noAmmoSound;// ���� ��������� ��������
    public float misfireTime = 0.2f;// ����� ������
    public bool isMisfire = false;// ���� ������

    void Start()// �������� �������� �������� � �������� ������������� ������������ ��������
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire && !isReloading)
        {
            if (currentAmmo > 0 && !isMisfire)
            {
                nextFire = Time.time + fireRate;//������������� ����� �� ���������� ��������
                currentAmmo--;
                Fire();
            }
            else if (!isMisfire)
            {
                StartCoroutine(Misfire());// ������ �������� �� ������
            }
        }


        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && !isReloading)
        {
            StartCoroutine(Reload());// ������ �������� �� �����������
        }
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);// ������� ����� ����
        Rigidbody rb = bullet.GetComponent<Rigidbody>();// �������� ��������� Rigidbody ����
        rb.velocity = bulletSpawn.forward * bulletSpeed;// ������������� �������� ����
        AudioSource.PlayClipAtPoint(fireSound, transform.position);// ������������� ���� ��������

        Destroy(bullet, 7f); // ���������� ���� ����� 7 ������
    }

    public void AddAmmo(int amount)
    {
        allAmmo += amount; // ��������� ������� � ���������
        if (allAmmo > maxAllAmmo) // ���� ���������� �������� ��������� ������������ ��������
        {
            allAmmo = maxAllAmmo; // ������������� ���������� �������� �� ������������ ��������
        }
    }

    IEnumerator Misfire() // �������� �� ������
    {
        isMisfire = true;
        AudioSource.PlayClipAtPoint(amptySound, transform.position);// ������������� ���� ������
        yield return new WaitForSeconds(misfireTime);
        isMisfire = false;
    }

    IEnumerator Reload()//�������� �� �����������
    {
        if (allAmmo > 0)
        {
            isReloading = true;
            Debug.Log("Reloading...");
            AudioSource.PlayClipAtPoint(reloadSound, transform.position);

            yield return new WaitForSeconds(reloadTime);

            if (maxAmmo < (allAmmo + currentAmmo))
            {
                allAmmo -= (maxAmmo - currentAmmo);
                currentAmmo = maxAmmo;
            }
            else
            {
                currentAmmo += allAmmo;
                allAmmo = 0;
            }
            isReloading = false;
            Debug.Log("Reloaded!");
        }
        else
        {
            AudioSource.PlayClipAtPoint(noAmmoSound, transform.position);
        }
    }
}