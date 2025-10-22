using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpointKey = 0;

    public List<CheckpointBase> checkpoints;

    public bool HasCheckpoint()
    {
        return lastCheckpointKey > 0;
    }
    public void SaveCheckpoint(int i)
    {
        if (i > lastCheckpointKey)
        {
            lastCheckpointKey = i;
        }
    }

    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpointKey);
        
        //SaveManager.instance.Setup.lastPositionX = checkpoint.transform.position.x;
        //SaveManager.instance.Setup.lastPositionY = checkpoint.transform.position.y;
        //SaveManager.instance.Setup.lastPositionZ = checkpoint.transform.position.z;
        SaveManager.instance.Setup.lastPosition = checkpoint.transform.position;

        //Debug.Log("PosX: " + SaveManager.instance.Setup.lastPositionX);
        //Debug.Log(SaveManager.instance.Setup.lastPositionY);
        //Debug.Log(SaveManager.instance.Setup.lastPositionZ);

        SaveManager.instance.Save();
        return checkpoint.transform.position;
    }
}
