using System.Linq;
using osu.Framework.Bindables;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Osu.Beatmaps;
using osu.Game.Rulesets.Osu.Objects;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModNoStreams : Mod, IApplicableAfterBeatmapConversion
    {
        public override string Name => "No Streams";
        public override string Acronym => "NS";
        public override ModType Type => ModType.Fun;
        public override string Description => @"Streams are no more.";
        public override double ScoreMultiplier => 1;

        [SettingSource("1x = 1 note per second per 60 bpm.", "More - More")]
        public BindableNumber<double> ForgivenessChange { get; } = new BindableDouble
        {
            MinValue = 0.1,
            MaxValue = 50,
            Default = 20.0,
            Value = 20.0,
            Precision = 0.5,
        };

        public void ApplyToBeatmap(IBeatmap beatmap)
        {
            var osuBeatmap = (OsuBeatmap)beatmap;

            double? lastCircleStartTime = null;
            osuBeatmap.HitObjects = osuBeatmap.HitObjects.Where(item =>
            {
                if (item is HitCircle circle)
                {
                    if (lastCircleStartTime == null)
                    {
                        lastCircleStartTime = item.StartTime;
                        return true;
                    }

                    var passed = item.StartTime - lastCircleStartTime.Value;

                    var info = osuBeatmap.ControlPointInfo.TimingPointAt(item.StartTime);
                    var minDistance = (info.BPM / (60.0 * ForgivenessChange.Value)) * 1000;

                    if (passed < minDistance)
                    {
                        return false;
                    }

                    lastCircleStartTime = item.StartTime;
                }

                return true;
            }).ToList();
        }
    }
}