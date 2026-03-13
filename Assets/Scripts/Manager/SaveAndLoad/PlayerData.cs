using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int level;
    public List<PlantType> unlockedPlant;

    // 构造函数 → 自动new，永远不空
    public PlayerData()
    {
        level = 1;
        unlockedPlant= new List<PlantType>();
    }
}

public static class GameDataHub
{
    // 静态构造 → 只要用到就自动实例化！绝对不空！
    static GameDataHub()
    {
        playerData = new PlayerData();
    }

    public static PlayerData playerData;
}