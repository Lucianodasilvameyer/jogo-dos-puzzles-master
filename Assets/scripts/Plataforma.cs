﻿using UnityEngine;

public class Plataforma : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private float timerMax;

    [SerializeField]
    private bool invert;

    // Start is called before the first frame update
    private void Start()
    {
        // Timer meutimer;
        //  meutimer = Timer.Register(1f, () => { int a = 3; a += 2; }, isLooped: true);// o register e o AttachTimer registram um Timer, a diferença é que o AttachTimer destroi o Timer quando o objeto é destruido

        direction = direction.normalized;

        this.AttachTimer(timerMax, InvertDirection, isLooped: true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!invert)
            transform.Translate(direction * speed * Time.deltaTime);
        else
            transform.Translate(-direction * speed * Time.deltaTime);
    }

    public void InvertDirection()
    {
        invert = !invert;
    }
}