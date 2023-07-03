using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text ammoText;
    public GameObject gameOverPanel;

    private Weapon weapon;

    void Start()
    {
        weapon = FindObjectOfType<Weapon>();
        UpdateAmmoText();
    }

    void Update()
    {
        UpdateAmmoText();
    }

    void UpdateAmmoText()
    {
        ammoText.text = "Ammo: " + weapon.currentAmmo + "/" + weapon.allAmmo;
    }
}