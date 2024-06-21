namespace MovieTicketAPI.Models.State
{
    public class BookedState : TicketState
    {
        public override void Handle(TicketContext context)
        {
            context.SetState(new CancelledState());
            // Logic for transitioning from booked to cancelled state
            System.Console.WriteLine("Ticket is now cancelled.");
        }

        public override string ToString() => "Booked";
    }
}
