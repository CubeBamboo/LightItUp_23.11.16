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

        public const string LIT_ON_BLOCK_RES_PATH = "Environment/litBlock(on)";

        #endregion

        #region GameObjectPath

        //public static string StageClearPanelPath = "/GamingCanvas/StageClearPanel";
        //public static string GamingFailPanelPath = "/GamingCanvas/GameFailPanel";
        public const string PLAYER_PATH = "/Player";

        #endregion

        #region GameObjectName

        public const string RB_TEXT_NAME = "RbText";
        public const string MAIN_CAMERA_NAME = "Main Camera";
        public const string RB_CANVAS_BUTTON_FATHER_NAME = "ButtonSet/";

        #endregion

        #region InputControlSchemes

        public const string MOUSE_KEYBOARD_SCHEMES = "Keyboard&Mouse";
        public const string GAMEPAD_SCHEMES = "Gamepad";

        #endregion

    }

    public enum SceneIndex
    {
        MAIN_MENU = 0,
        LEVEL_1 = 1,
        LEVEL_2 = 2,
        LEVEL_3 = 3,
        LEVEL_OUT_OF_RANGE,
        DEMO_END = 4
    }
}