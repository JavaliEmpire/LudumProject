using UnityEngine;
using System.Collections;

public class Jump : ObstacleUtility
{

    void Awake()
    {

    }

    public override void AUpdate()
    {
        base.AUpdate();
    }

    protected override void OnTriggerEnter2D(Collider2D p_other)
    {
        base.OnTriggerEnter2D(p_other);
    }
}
