using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ATurretShootController : MonoBehaviour
{

    protected bool _canShoot;

    [field: SerializeField]
    public DataTurretShooter dataShootController { get; protected set; }

    [field: SerializeField]
    public AAmmo currentAmmoPrefab { get; protected set; }

    [field: SerializeField]
    public MMFeedback changeAmmoFeedBack { get; protected set; }

    [field: SerializeField]
    public MMFeedback ShootFeeback { get; protected set; }


    public abstract void Shoot();

    public virtual void ChangeAmmo()
    {
        changeAmmoFeedBack.Play(transform.position);
    }

}
