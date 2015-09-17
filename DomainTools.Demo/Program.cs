using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTools.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a domain name to get the whois information.");
            var domainName = Console.ReadLine();
            try
            {
                var whoisText = Whois.Lookup(domainName, RecordType.domain);
                Console.WriteLine(whoisText);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Press any key to exit..");
            Console.Read();
        }
    }
}
