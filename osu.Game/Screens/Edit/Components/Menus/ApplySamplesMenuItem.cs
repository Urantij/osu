// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Game.Beatmaps;

namespace osu.Game.Screens.Edit.Components.Menus
{
    public class ApplySamplesMenuItem : EditorMenuItem
    {
        public BeatmapInfo BeatmapInfo { get; }

        public ApplySamplesMenuItem(BeatmapInfo beatmapInfo, Action<BeatmapInfo> samplesApplyFunc)
            : base(string.IsNullOrEmpty(beatmapInfo.DifficultyName) ? "(unnamed)" : beatmapInfo.DifficultyName)
        {
            BeatmapInfo = beatmapInfo;

            Action.Value = () => samplesApplyFunc.Invoke(beatmapInfo);
        }
    }
}
