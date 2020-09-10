namespace AmStInter.DataSource.Models.Requests
{
    internal class PutBody<T>
    {
        public T Value { get; set; }
        public string Patch { get; set; }
        public string Op { get; set; }

        public PutBody(T value, string patch, string op)
        {
            Value = value;
            Patch = patch;
            Op = op;
        }

        public string ToHttpBody()
        {
            return "[{\"value\":" + Value + ",\"path\":\"" + Patch + "\",\"op\":\"" + Op + "\"}]";
        }
    }
}
