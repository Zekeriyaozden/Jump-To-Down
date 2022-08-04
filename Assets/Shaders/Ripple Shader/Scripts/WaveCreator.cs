using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCreator : MonoBehaviour
{
    [SerializeField] private List<Material> _materials;

    [SerializeField] private float _currentRing = 0.50f;
    [SerializeField] private float _startRing = 0.50f;
    [SerializeField] private float _endRing = -2f;
    [SerializeField] private bool _waveActive = false;
    [SerializeField] private float _speed = 1f;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (_waveActive)
        {
            _currentRing = Mathf.MoveTowards(_currentRing, _endRing, _speed * Time.deltaTime);
            SetMaterialsRing(_currentRing);
            if (_currentRing == _endRing)
            {
                _waveActive = false;
                _currentRing = _startRing;
                SetMaterialsRing(_currentRing);
            }
        }
    }

    public void WaveStart(Vector3 StartPos)
    {
        _waveActive = true;
        _currentRing = _startRing;
        SetMaterialsStartPos(StartPos);
    }

    public void SetMaterialsRing(float ring)
    {
        foreach (var material in _materials)
        {
            material.SetFloat("_Ring", ring);
        }
    }

    public void SetMaterialsStartPos(Vector3 startPos)
    {
        foreach (var material in _materials)
        {
            material.SetVector("_WaveStartPos", startPos);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.collider.CompareTag ("Hit"))
        {
            WaveStart(transform.position);
            Debug.Log("�arp��ma ya�and�" + collision.collider.tag);
        }
    }
}
