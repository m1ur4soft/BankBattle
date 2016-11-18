using UnityEngine;

public class GameInit{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Screen.SetResolution(640, 360, false, 60);
    }
}
