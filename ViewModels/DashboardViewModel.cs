namespace 프로젝트관리.ViewModels;

public class DashboardViewModel
{
    public int TotalClasses { get; set; }
    public int OngoingClasses { get; set; }
    public int PlannedClasses { get; set; }
    public int CompletedClasses { get; set; }
    public int OpenAgendas { get; set; }
    public int OverdueActionItems { get; set; }
    public int DecidedAgendas { get; set; }
    public int OnHoldAgendas { get; set; }

    public int OngoingRate => TotalClasses == 0 ? 0 : (int)Math.Round((double)OngoingClasses / TotalClasses * 100);
    public int AgendaClosedRate
        => (OpenAgendas + DecidedAgendas + OnHoldAgendas) == 0
            ? 0
            : (int)Math.Round((double)DecidedAgendas / (OpenAgendas + DecidedAgendas + OnHoldAgendas) * 100);
}
