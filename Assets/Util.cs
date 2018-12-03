using UnityEngine;

public class Util
{

    public static void SetLayerRecursively(GameObject _obj, int layer)
    {
        if (_obj == null)
            return;
        _obj.layer = layer;
        foreach(Transform _child in _obj.transform)
        {
            if (_child == null)
                continue;
            SetLayerRecursively(_child.gameObject, layer);
        }

    }

}
