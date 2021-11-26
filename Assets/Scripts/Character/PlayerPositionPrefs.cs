using UnityEngine;

public static class PlayerPositionPrefs
{
    public static void SaveCurrentPlayerPosition(string prefName, Vector3 position)
    {
        Debug.Log("Matt saving this = " + $"{prefName}");
        PlayerPrefs.SetFloat($"{prefName}_x", position.x);
        PlayerPrefs.SetFloat($"{prefName}_y", position.y);
        PlayerPrefs.SetFloat($"{prefName}_z", position.z);
        PlayerPrefs.Save();
    }

    public static Vector3? RestoreCurrentPlayerPosition(string prefName)
    {
        if (PlayerPrefs.HasKey($"{prefName}_x"))
        {
            var x = PlayerPrefs.GetFloat($"{prefName}_x");
            var y = PlayerPrefs.GetFloat($"{prefName}_y");
            var z = PlayerPrefs.GetFloat($"{prefName}_z");
            var vector3 = new Vector3(x, y, z);
            PlayerPrefs.DeleteKey($"{prefName}_x");
            PlayerPrefs.DeleteKey($"{prefName}_y");
            PlayerPrefs.DeleteKey($"{prefName}_z");
            PlayerPrefs.Save();
            return vector3;
        }
        return null;
    }
}
