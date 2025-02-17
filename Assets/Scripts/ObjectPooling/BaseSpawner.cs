using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public abstract class BaseSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected int maxObjetctsAtOnce = 1;
    protected int objectCount = 0;
    protected float timer = 0f;

    [SerializeField] protected T prefab;
    public IObjectPool<T> objectPool;

    protected virtual void Awake()
    {
        objectCount = 0;
        InitializePool();
    }

    private void InitializePool()
    {
        objectPool = new ObjectPool<T>(CreateObject, OnGet, OnRelease);
    }

    public abstract T CreateObject();

    public abstract void OnGet(T obj);

    public abstract void OnRelease(T obj);

    protected virtual void Update()
    {
        
    }
}
