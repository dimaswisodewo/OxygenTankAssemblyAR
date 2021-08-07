using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Config
{
    public static string APP_VERSION = "v.1.0.1.I.14";

    public const string RESUME_TEXT = "Resume";
    public const string PAUSE_TEXT = "Pause";
    public const string SHOW_TEXT = "Show";
    public const string HIDE_TEXT = "Hide";
    public const string ASSEMBLY_TEXT = "Installation";
    public const string DIASSEMBLY_TEXT = "Removal";
    public const string SAFETY_TEXT = "Safety Precaution";
    public const string OXYGEN_TEXT = "Oxygen";

    public const string AR_SCENE = "ARScene";

    public const int ANIMATION_ASSEMBLY_FRAME_COUNT = 1169;
    public const int ANIMATION_DIASSEMBLY_FRAME_COUNT = 1097;

    public const float DESCRIPTION_PANEL_TOP_POS = 0f;
    public const float DESCRIPTION_PANEL_BOT_POS = -785f;
    public const float DESCRIPTION_PANEL_TOP_POS_2 = -300f;

    public const float SIDE_BUTTONS_SHOWN_POS = 0f;
    public const float SIDE_BUTTONS_HIDDEN_POS = -160f;

    public const float MIN_SCALE = 0.3f;
    public const float MAX_SCALE = 1.8f;

    public const float DIASSEMBLY_FRAME_COUNT = 1220f;
    public const float ASSEMBLY_FRAME_COUNT = 1750f;

    public static float[][] DIASSEMBLY_ANIMATION_STEP = new float[][]
    {
        new float[] { 0f, 90f },
        new float[] { 90f, 165f },
        new float[] { 222f, 354f },
        new float[] { 354f, 560f },
        new float[] { 560f, 800f },
        new float[] { 800f, 982f },
        new float[] { 983f, 1220f }
    };

    public static float[][] ASSEMBLY_ANIMATION_STEP = new float[][]
    {
        new float[] { 0f, 20f },
        new float[] { 22f, 130f },
        new float[] { 130f, 289f },
        new float[] { 290f, 750f },
        new float[] { 750f, 975f },
        new float[] { 975f, 1150f },
        new float[] { 1150f, 1470f },
        new float[] { 1470f, 1750f }
    };
}
