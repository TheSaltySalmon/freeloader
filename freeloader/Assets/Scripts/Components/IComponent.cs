using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FreeLoader.Components
{
    public interface IComponent
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}
