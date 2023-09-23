using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TakeDamageEffect : MonoBehaviour
{
    [SerializeField] private CharacterHealth _myHealth;
    [SerializeField] private SkinnedMeshRenderer _myRenderer;
    [SerializeField] private Material _damageMaterial;
    [SerializeField] private float _time;


    private Coroutine _coroutine;
    private Material[] _startMaterials;

    private void Awake()
    {
        _startMaterials = _myRenderer.materials;
    }

    private void Start()
    {
        _myHealth.GetDamage_notifier += StartEffect;
    }

    private void StartEffect(object sender, EventArgs e)
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(EffectTimer());
    }

    private Material[] ChangeDamageMaterials(SkinnedMeshRenderer meshRenderer)
    {
        var count = _myRenderer.materials.Length;

        Material[] materials = new Material[count];

        for (int i = 0; i < count; i++)
        {
            materials[i] = _damageMaterial;
        }

        return materials;
    }

    private IEnumerator EffectTimer()
    {
        _myRenderer.materials = ChangeDamageMaterials(_myRenderer);
        yield return new WaitForSeconds(_time);
        _myRenderer.materials = _startMaterials;
    }
}
