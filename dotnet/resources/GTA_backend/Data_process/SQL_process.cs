namespace mygaymode
{
    internal class SQL_process
    {
        internal static string Connection_String { get; private set; } = "";

        private static SQL_process instance = null;
        private static object syncRoot = new object();
        internal static SQL_process CreateDB()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new SQL_process();
                        Connection_String = instance.AddConnectionString();
                    }
                }
            }
            return instance;
        }
        private string AddConnectionString()
        {
            string[] str_arr = File.ReadAllLines("..\\..\\..\\Data_process\\cnstr.bin");
            var cstr = str_arr[0].ToCharArray();
            for (int i = 0; i < cstr.Length; i++)
            {
                cstr[i] = (char)(cstr[i] + 5);
            }
            string data = new string(cstr);
            return data;
        }
    }
}
