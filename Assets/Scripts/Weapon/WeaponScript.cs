using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private Transform _barrelExit;
    [SerializeField] private AudioClip[] _fireSoundEffects;
    [SerializeField] private AudioClip _reloadSoundEffect;

    private AudioSource _audioSource;
    private float _nextFire;
    private WeaponProperties _weaponProps;
    
    private GameObject _bulletPrefab;
    private float _fireRate;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    public void SetWeaponProperties(WeaponProperties weaponProperties)
    {
        _weaponProps = weaponProperties;
    }
    
    public void Fire()
    {
        if (Time.time > _nextFire)
        {
            if (_weaponProps.RayCaster)
            {
                RaycastHit hit;
                if (Physics.Raycast(_barrelExit.position, transform.parent.forward,
                        out hit, Mathf.Infinity))
                {
                    DamageTaker dmgTaker = hit.collider.GetComponentInParent<DamageTaker>();
                    if (dmgTaker)
                    {
                        dmgTaker.TakeDamage(100);
                    }
                }
            }
            else
            {
                Instantiate(_weaponProps.bulletPrefab, _barrelExit.position, transform.parent.rotation);
            }

            
            PlayFireSound();
            _nextFire = Time.time + _fireRate;
        }
    }

    public void PlayFireSound()
    {
        if (_fireSoundEffects.Length > 0)
        {
            PlaySound(_fireSoundEffects[Random.Range(0, _fireSoundEffects.Length)]);
        }
    }
    
    public void PlayReloadSound()
    {
        if (_reloadSoundEffect)
        {
            PlaySound(_reloadSoundEffect); 
        }
        
    }
    
    void PlaySound(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
}
