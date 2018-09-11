using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeLoader.Components
{
    public interface IProjectileComponent
    {
        void Fire(Transform weaponTransform);
        void Awake();
        void OnCollisionEnter2D(Collision2D collision);
        void FixedUpdate();
    }
}