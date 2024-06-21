namespace MovieTicketAPI.Models.State
{
    public class CancelledState : TicketState
    {
        public override void Handle(TicketContext context)
        {
            context.SetState(new AvailableState());
            // Logic for transitioning from cancelled to available state
            System.Console.WriteLine("Ticket is now available again.");
        }

        public override string ToString() => "Cancelled";
    }
}
