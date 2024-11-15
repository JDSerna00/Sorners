using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WeaponDamager : MonoBehaviour, IDamageSender
{
    [SerializeField]
    private float baseDamage = 10f;
    private float multiplier;
    private List<IDamageReceiver> hitReceivers = new List<IDamageReceiver>();

    public int Faction => 0;

    public void SendDamage(IDamageReceiver target)
    {
        DamagePayload damagePayload = new DamagePayload { damage = baseDamage * multiplier, position = transform.position, severity = DamagePayload.DamageSeverity.Light};
        target.ReceiveDamage(this, damagePayload);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out IDamageReceiver target) && target.Faction != Faction) //!hitReceivers.Contains(target))
        {
            hitReceivers.Add(target);
            SendDamage(target);
        }
    }

    public void Toggle(float multiplier)
    {
        hitReceivers.Clear();
        Collider col = GetComponent<Collider>();
        col.enabled = !col.enabled;
        this.multiplier = multiplier;
    }

    public void AntiBug_Collider(){
        Collider col = GetComponent<Collider>();
        col.enabled = false;
    }
    
}
