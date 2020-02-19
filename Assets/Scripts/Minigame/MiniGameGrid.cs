using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameGrid : MonoBehaviour {

    #region editor
    public MiniGameGridHelp gridHelpHint;
    [SerializeField] private float rotateSpeed = 1f;
    #endregion

    [HideInInspector]
    public bool CanClick { get; private set; } = true;
    public bool CanExit { get { return gridHelpHint.IsNotActive; } }

    private MiniGame miniGame;
    private Sprite[] validSprites;
    private List<MiniGameGridCell> cells = new List<MiniGameGridCell>();

    private bool shuffling = false;

    #region private
    private void Awake() {
        cells = transform.GetComponentsInChildren<MiniGameGridCell>().ToList();
    }

    private bool IsValid() {
        return validSprites.Select((e, i) => cells[i].Compare(e)).All(e => e);
    }

    private IEnumerator MoveToPosition(MiniGameGridCell cell, Vector3 position, float timeToMove) {
        Transform currentPos = cell.transform;
        var elapsed = 0f;
        while (elapsed < timeToMove) {
            currentPos.position = Vector3.Lerp(currentPos.position, position, (elapsed / timeToMove));
            elapsed += Time.deltaTime;
            CanClick = false;
            yield return new WaitForEndOfFrame();
        }
        currentPos.position = position;
        CanClick = true;
    }
    #endregion

    #region public
    public void SetupMiniGameInstance(MiniGame mg) {
        miniGame = mg;
    }

    public void ResetGrid() {
        if (gridHelpHint.IsNotActive) {
            shuffling = true;
            for (int i = 0; i < miniGame.Difficult; i++) {
                SwapBlock(Random.Range(0, 4));
            }
            shuffling = false;
        }
    }

    public void SwapBlock(int index) {
        if (!CanClick || gridHelpHint.IsActive) return;

        if (!shuffling) miniGame.SwapAnimation(index);

        index = index >= 2 ? index + 1 : index;
        int[] cellIndexes = new int[] { index, index + 1, index + 4, index + 3, index };
        MiniGameGridCell prev = cells[cellIndexes.First()];
        for (int i = 1; i < cellIndexes.Length; i++) {
            int idx = cellIndexes[i];
            MiniGameGridCell curr = cells.ElementAt(idx);

            if (!shuffling) {
                StartCoroutine(MoveToPosition(prev, curr.transform.position, rotateSpeed));
                cells[idx] = prev;
                prev = curr;
            } else {
                prev.Swap(curr);
            }
        }

        if (IsValid()) miniGame.StopGame();
    }

    public void SetPictures(MiniGamePictures pictures) {
        validSprites = pictures.sprites;
        gridHelpHint.SetupSprite(pictures.hint);
        for (int i = 0; i < pictures.sprites.Length; i++) {
            MiniGameGridCell cell = cells[i];
            cell.SetupSprite(pictures.sprites[i]);
        }

        ResetGrid();
    }

    public void Help() {
        if (!gridHelpHint.IsActive && !gridHelpHint.IsNotActive) return;
        if (gridHelpHint.IsNotActive) gridHelpHint.Show();
        if(gridHelpHint.IsActive) gridHelpHint.Hide();
    }
    #endregion
}
