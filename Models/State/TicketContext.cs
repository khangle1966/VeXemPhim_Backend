namespace MovieTicketAPI.Models.State
{
    public class TicketContext
    {
        private TicketState _state;

        public TicketContext(TicketState state)
        {
            _state = state;
        }

        public void SetState(TicketState state)
        {
            _state = state;
        }

        public TicketState GetState()
        {
            return _state;
        }

        public void Request()
        {
            _state.Handle(this);
        }
    }
}
