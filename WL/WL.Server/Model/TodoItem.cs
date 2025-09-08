using WL.Server.RestfulPractice.P01;

namespace WL.Server.Model
{

    public class TodoItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
