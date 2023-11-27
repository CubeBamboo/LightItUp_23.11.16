using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public static class Constant
    {
        #region Tag

        public const string PLAYER_TAG = "Player";
        public const string BRICK_TAG = "Brick";
        public const string MIRROR_TAG = "Mirror";
        public const string MOVING_BOUNDS_TAG = "MovingBounds";

        #endregion

        #region FilePath

        public static string LIT_ON_BLOCK_RES_PATH = "Environment/litBlock(on)";

        #endregion

        #region GameObjectPath

        public static string StageClearPanelPath = "/GamingCanvas/StageClearPanel";
        public static string GamingFailPanelPath = "/GamingCanvas/GameFailPanel";
        public static string PLAYER_PATH = "/Player";

        #endregion

    }

    public enum SceneIndex
    {
        MAIN_MENU = 0,
        LEVEL_1 = 1,
        LEVEL_2 = 2,
        LEVEL_OUT_OF_RANGE = 3,
        DEMO_END = 3
    }
}