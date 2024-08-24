using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshBake : Singleton<NavMeshBake>
{
    public NavMeshPlus.Components.NavMeshSurface nav;

    public void BakingPlace()
    {
        nav.BuildNavMesh();
    }
}
