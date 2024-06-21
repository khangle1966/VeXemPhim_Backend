namespace MovieTicketAPI.Models.State
{
    public class AvailableState : TicketState
    {
        public override void Handle(TicketContext context)
        {
            context.SetState(new BookedState());
            // Logic for transitioning from available to booked state
            System.Console.WriteLine("Ticket is now booked.");
        }

        public override string ToString() => "Available";
    }
}
