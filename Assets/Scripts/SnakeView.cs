public interface SnakeView {

    void SetModel(SnakeModel model);
    void ChangeModel(SnakeModel model);
    void UpdateView();
    void InitializeView();
    void GameEnds();
    void RestartGame();
}
