using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DomainTools
{
    /// <summary>
    /// A class to lookup whois information.
    /// </summary>
    public class Whois
    {
        private const int Whois_Server_Default_PortNumber = 43;

        /// <summary>
        /// Retrieves whois information
        /// </summary>
        /// <param name="domainName">The registrar or domain or name server whose whois information to be retrieved</param>
        /// <param name="recordType">The type of record i.e a domain, nameserver or a registrar</param>
        /// <returns></returns>
        public static string Lookup(string domainName, RecordType recordType)
        {
            string whoisServerName = WhoisServerResolver.GetWhoisServerName(domainName);
            using (TcpClient whoisClient = new TcpClient())
            {
                whoisClient.Connect(whoisServerName, Whois_Server_Default_PortNumber);

                string domainQuery = recordType.ToString() + " " + domainName + "\r\n";
                byte[] domainQueryBytes = Encoding.ASCII.GetBytes(domainQuery.ToCharArray());

                Stream whoisStream = whoisClient.GetStream();
                whoisStream.Write(domainQueryBytes, 0, domainQueryBytes.Length);

                StreamReader whoisStreamReader = new StreamReader(whoisClient.GetStream(), Encoding.ASCII);

                string streamOutputContent = "";
                List<string> whoisData = new List<string>();
                while (null != (streamOutputContent = whoisStreamReader.ReadLine()))
                {
                    whoisData.Add(streamOutputContent);
                }

                whoisClient.Close();

                return String.Join(Environment.NewLine, whoisData);
            }            
        }        
    }
}
