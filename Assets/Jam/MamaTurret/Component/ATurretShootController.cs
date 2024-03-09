using MoreMountains.Feedbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
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

    public Action OnShoot;

    public abstract void Shoot(Transform from, Vector3 dir);

    public virtual void ChangeAmmo()
    {
        changeAmmoFeedBack.Play(transform.position);
    }



}
