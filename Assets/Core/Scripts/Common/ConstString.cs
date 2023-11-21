using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public static class ConstString
    {
        #region Tag

        public const string PLAYER_TAG = "Player";
        public const string BRICK_TAG = "Brick";
        public const string MIRROR_TAG = "Mirror";

        #endregion

        #region Path

        public static string LitOnBlockResPath = "Environment/litBlock(on)";

        #endregion
    }

    public static class Constant
    {
        #region SceneBuildIndex

        public const int MAIN_MENU_SCENE_INDEX = 0;
        public const int LEVEL_1_SCENE_INDEX = 1;
        public const int LEVEL_2_SCENE_INDEX = 2;

        #endregion
    }
}