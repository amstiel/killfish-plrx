using System;
public class WorldInfo {
    public enum GameState {
        Start,
        Moving,
        Fight
    }

    private static WorldInfo instance;

    private WorldInfo() { }

    public static WorldInfo Instance() {
        if (instance == null) {
            instance = new WorldInfo();
        }
        return instance;
    }

    private int _globalFrame = 0; 
    public void IncGlobalFrame() {
        _globalFrame++;
    }
    public int globalFrame {
        get { return _globalFrame; }
    }

    private GameState _gameState;
    public void SetState(GameState state) {
        _gameState = state;
    }
    public GameState gameState {
        get { return _gameState; }
    }
}