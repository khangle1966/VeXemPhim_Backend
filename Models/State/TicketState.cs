namespace MovieTicketAPI.Models.State
{
    public abstract class TicketState
    {
        public abstract void Handle(TicketContext context);
    }
}
