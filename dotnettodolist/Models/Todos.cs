namespace dotnettodolist.Models
{
    public class Todos
    {
        public int Id { get; set; }
        public required string Todo {  get; set; }
        public bool IsFinished { get; set; } = false;
    }
}
