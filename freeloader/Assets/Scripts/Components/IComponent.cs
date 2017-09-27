using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public interface IComponent
{
    Coroutine StartCoroutine(IEnumerator routine);
}
