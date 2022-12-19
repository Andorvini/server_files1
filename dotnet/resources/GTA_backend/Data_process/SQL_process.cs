using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security;
using System.Text;
using Npgsql;
using System.Runtime.CompilerServices;
using GTANetworkAPI;
using System.Security.Cryptography;

//  <PackageReference Include="Microsoft.Data.SqlClient" Version="5.0.1" />
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
                        Connection_String = instance.AddConnectionString(Path.GetFullPath("Data_files\\ConnectionString.txt"));
                    }
                }
            }
            return instance;
        }
        private string AddConnectionString(string path_encrypted_file)
        {
            byte[] scanned = File.ReadAllBytes(path_encrypted_file);
            Aes aes = Aes.Create();
            byte[] decoded = aes.DecryptEcb(scanned, PaddingMode.ISO10126);
            string res = Encoding.ASCII.GetString(decoded, 0, decoded.Length);
            NAPI.Util.ConsoleOutput(res);
            return res;
        }
    }
}
