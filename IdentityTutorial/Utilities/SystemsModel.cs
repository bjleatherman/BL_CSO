namespace IdentityTutorial.Utilities
{
    public class SystemsModel
    {
        public int Id;
        public string HtmlId;
        public Helper.SYSTEMS_PIPES Pipe;
        public string Direction;
        public string Power;
        public int DbIndex;
        public bool? isFixed;

        public SystemsModel(int id, string htmlId, Helper.SYSTEMS_PIPES pipe, 
            string direction, string power, int dbIndex)
        {
            Id = id;
            HtmlId = htmlId;
            Pipe = pipe;
            Direction = direction;
            Power = power;
            DbIndex = dbIndex;
        }
    }
}
