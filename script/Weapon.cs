using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour // Код для оружия
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float fireRate = 0.5f;//скорострельность оружия
    public int maxAmmo = 10;// максимальное количество патронов в магазине
    public int allAmmo = 30;// всего патронов у персонажа
    public int maxAllAmmo = 50;// максимум патронов у персонажа
    public int currentAmmo;// текущее количество патронов в магазине
    private float nextFire = 0.0f;//время до следующего выстрела
    public float reloadTime = 4f; // время перезарядки
    public bool isReloading = false; // флаг перезарядки
    public AudioClip reloadSound; // звук перезарядки
    public AudioClip amptySound; // звук отсутствия патронов
    public AudioClip fireSound; // звук выстрела
    public float bulletSpeed = 35f;//скорость пули
    public AudioClip noAmmoSound;// звук отсутсвия патронов
    public float misfireTime = 0.2f;// время осечки
    public bool isMisfire = false;// флаг осечки

    void Start()// текущему значению патронов в магазине присваивается максимальное значение
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire && !isReloading)
        {
            if (currentAmmo > 0 && !isMisfire)
            {
                nextFire = Time.time + fireRate;//устанавливаем время до следующего выстрела
                currentAmmo--;
                Fire();
            }
            else if (!isMisfire)
            {
                StartCoroutine(Misfire());// запуск корутины на осечку
            }
        }


        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && !isReloading)
        {
            StartCoroutine(Reload());// запуск корутины на перезарядку
        }
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);// создаем новую пулю
        Rigidbody rb = bullet.GetComponent<Rigidbody>();// получаем компонент Rigidbody пули
        rb.velocity = bulletSpawn.forward * bulletSpeed;// устанавливаем скорость пули
        AudioSource.PlayClipAtPoint(fireSound, transform.position);// воспроизводим звук выстрела

        Destroy(bullet, 7f); // уничтожаем пулю через 7 секунд
    }

    public void AddAmmo(int amount)
    {
        allAmmo += amount; // добавляем патроны в инвентарь
        if (allAmmo > maxAllAmmo) // если количество патронов превышает максимальное значение
        {
            allAmmo = maxAllAmmo; // устанавливаем количество патронов на максимальное значение
        }
    }

    IEnumerator Misfire() // корутина на осечку
    {
        isMisfire = true;
        AudioSource.PlayClipAtPoint(amptySound, transform.position);// воспроизводим звук осечки
        yield return new WaitForSeconds(misfireTime);
        isMisfire = false;
    }

    IEnumerator Reload()//корутина на перезарядку
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