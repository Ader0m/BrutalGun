using static BrutalGun.BetterCardControl.DynamicCardStatsManager;

namespace BrutalGun.BetterCardControl
{
    public interface IDynamicCardStats
    {
        public void ChangeCardStats(DynamicType type);
    }
}
