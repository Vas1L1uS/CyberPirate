using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SphereZone : MonoBehaviour
{
    public event EventHandler Detected_notifier;

    public List<RaycastHit> Hits = new List<RaycastHit>();

    [SerializeField] private LayerMask _trigger;
    [SerializeField] private float _radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawSphere(this.transform.position, _radius);
    }

    private void Update()
    {
        Hits = Physics.SphereCastAll(this.transform.position, _radius, Vector3.one, 10, _trigger).ToList();

        if (Hits.Count > 0)
        {
            Detected_notifier?.Invoke(this, EventArgs.Empty);
        }
    }
}