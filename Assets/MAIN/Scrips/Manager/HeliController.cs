using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class HeliController : MonoBehaviour
{
    public static HeliController Instance
    {
        get;
        private set;
    }
    protected enum StateHeli
    {
        startMoving,
        waitting,
        endMoving

    }
    public Action startActioneHeli;
    public Action endActioneHeli;
    public Transform target; //target cage
    private Transform _target;
    private Vector3 _default;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _default = this.transform.position;
      

    }

    public void init(Action startE,Action endE,Transform Target)
    {
        startActioneHeli=startE;
        endActioneHeli=endE;
        this.target=Target;
        _target = target;
        StartHeli();
    }

    [ProButton]
    public void StartHeli()
    {
        startActioneHeli?.Invoke();
        Vector3 adjTranform = new Vector3(_target.position.x, this.transform.position.y, _target.position.z);
        transform.LookAt(adjTranform);
        transform.DOMove(adjTranform, 5f).OnComplete(() =>
        {
            target.transform.parent = this.transform;
           
            target.DOMoveY(transform.position.y-30f, 2f).OnComplete(() =>
            {
                Vector3 targetPosition = transform.position + transform.forward * 200;
                transform.DOMove(targetPosition, 5f).OnComplete(() =>
                {
                    endActioneHeli?.Invoke();
                    StartCoroutine( clear());
                    
                });
            });
        });  
    }

    private IEnumerator clear()
    {
        //heli comback to position default
        //this.transform.DOMove(_default.position, 0f);
        yield return new WaitForSeconds(0.5f);
        transform.position = _default;
        Debug.Log(transform.position);
        startActioneHeli = null;
        endActioneHeli = null;
        Destroy(target.gameObject);
        
    }

}
