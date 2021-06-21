using ICities;

namespace RealCity
{
    public class Unlock : MilestonesExtensionBase
    {
        public override void OnRefreshMilestones()
        {
            if (RealCity.randomEvent)
                base.milestonesManager.UnlockMilestone("Milestone4");
        }
    }
}