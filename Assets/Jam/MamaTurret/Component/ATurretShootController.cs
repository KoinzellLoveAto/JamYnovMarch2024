using MoreMountains.Feedbacks;
using System;
using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;

public abstract class ATurretShootController : MonoBehaviour
{

    protected bool _canShoot = true;

    [field: SerializeField]
    public DataTurretShooter dataShootController { get; protected set; }

    [field: SerializeField]
    public AAmmo currentAmmoPrefab { get; protected set; }

    [field: SerializeField]
    public MMFeedback changeAmmoFeedBack { get; protected set; }

    [field: SerializeField]
    public MMFeedback ShootFeeback { get; protected set; }

    public Action OnShoot;
    protected Coroutine _shootRoutine;




    public abstract void Shoot(Transform from, Vector3 dir);

    public virtual void ChangeAmmo()
    {
        changeAmmoFeedBack.Play(transform.position);
    }

    protected IEnumerator ShootRoutine()
    {
        yield return new WaitForSeconds(dataShootController.DelayBetweenShoot);
        _canShoot = true ;

    }

    protected virtual Vector3 GetImprecisionOnVector(Vector3 initialDir, float magnetudeError)
    {
        Vector3 rndVector = UnityEngine.Random.insideUnitSphere * magnetudeError;

        Vector3 dir = initialDir + rndVector;

        return dir.normalized;
    }
}
