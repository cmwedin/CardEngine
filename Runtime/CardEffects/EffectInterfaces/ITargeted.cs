using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargeted {
    public Type TargetType { get; }
    public GameObject Target{ get; set; }
}
