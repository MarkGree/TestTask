using System.Collections.Generic;
using UnityEngine;

public class UnitsFightingDirector : MonoBehaviour
{
    [SerializeField] private UnitsSpawner unitsSpawner;

    private LinkedList<Unit> activeUnits = new LinkedList<Unit>();
    private Unit[] unitsBuffer;

    private void Start()
    {
        unitsBuffer = new Unit[unitsSpawner.AllUnits.Count];
    }

    private void Update()
    {
        CheckBufferResize();

        TargetActiveUnits();

        ReleaseInactiveUnits();
    }

    private void CheckBufferResize()
    {
        int actualUnitsCount = unitsSpawner.AllUnits.Count;

        if (unitsBuffer.Length != actualUnitsCount)
            unitsBuffer = new Unit[actualUnitsCount];
    }

    private void TargetActiveUnits()
    {
        LinkedListNode<Unit> node = activeUnits.First;

        int bufferUnitsCount = 0;
        for (int i = 0; i < activeUnits.Count; i++)
        {
            Unit unit = node.Value;

            if (unit == null)
            {
                var nullNode = node;
                node = node.Next;

                activeUnits.Remove(nullNode);
                i--;

                continue;
            }

            if (unit.Targeting.TargetUnit == null)
            {
                unitsBuffer[bufferUnitsCount] = unit;
                bufferUnitsCount++;
            }

            node = node.Next;
        }


        if (bufferUnitsCount < 2)
            return;


        for (int j = 0; j < bufferUnitsCount; j++)
        {
            Unit u = unitsBuffer[j];
            Unit u2 = u;

            Unit closestNotFighting = u2;
            float distanceMin = float.MaxValue;

            for (int i = 1; i < bufferUnitsCount; i++)
            {
                u2 = unitsBuffer[i];

                if (u != u2 && u.Targeting.TargetUnit == null && u2.Targeting.TargetUnit == null)
                {
                    float dist = PlaneVectors.Distance(u.transform.position, u2.transform.position);
                    if (dist < distanceMin)
                    {
                        closestNotFighting = u2;
                        distanceMin = dist;
                    }
                }
            }

            if (u != closestNotFighting)
            {
                u.Targeting.SetTarget(closestNotFighting);
                closestNotFighting.Targeting.SetTarget(u);
            }
        }
    }

    private void ReleaseInactiveUnits()
    {
        for (int i = 0; i < unitsSpawner.InactiveUnits.Count; i++)
        {
            Unit unit = unitsSpawner.ReleaseLastInactive();
            var node = activeUnits.AddLast(unit);

            unit.NodesHolder.Add(0, node);
            unit.Parameters.OnDie += () => activeUnits.Remove(unit.NodesHolder.Remove(0));
        }
    }
}
