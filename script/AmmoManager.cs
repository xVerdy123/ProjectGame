using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [SerializeField] public int AmmoValue; // значение лута
    [SerializeField] private AudioClip AmmoSound;
    [SerializeField] private Weapon _weaponManager;
        
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ammo>()) 
        {
            Destroy(collision.gameObject);
            _weaponManager.AddAmmo(AmmoValue);
            AudioSource.PlayClipAtPoint(AmmoSound, transform.position);
        }
    }
}
