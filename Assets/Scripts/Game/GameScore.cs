public class GameScore {
    public int EnergyCount { get; private set; } = 0;
    public int ShellCount { get; private set; } = 0;
    public int EnemyCount { get; private set; } = 0;
    public bool MiniGamePassed { get; set; } = false;

    public GameScore() {
        Reset();
    }

    public void Reset() {
        EnergyCount = 0;
        ShellCount = 0;
        EnemyCount = 0;
        MiniGamePassed = false;
    }

    public void AddEnergy(int count) => EnergyCount += count;
    public void AddShell(int count) => ShellCount += count;
    public void AddEnemyKill(int count) => EnemyCount += count;

    public int TotalScore() {
        return EnergyCount + (ShellCount * 2) + (EnemyCount * 5);
    }
}
