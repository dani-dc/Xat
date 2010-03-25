using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace XatClient
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Client client = new Client("192.168.130.44", 9898);
			
            string nomUsuari = "dani-dc@hotmail-com";

			if (client.ConnectToServer())
			{
                string missatge = "";

                do
                {
                    Console.WriteLine("Introdueix un missatge ( escriu 'sortir' per sortir):");
                    missatge = Console.ReadLine();

                    if (missatge.ToLower() != "disconnect")
                    {
                        // Enviar el nom d'usuari:
                        client.WriteLine(nomUsuari);
                        
                        // Enviar el missatge:
                        client.WriteLine(missatge);
                    }

                } while (missatge.ToLower() != "disconnect");
			}
		}
	}
	
	public class Client
	{
		private NetworkStream netStream;
		private StreamReader readerStream;
		private StreamWriter writerStream;
		private IPEndPoint server_endpoint;
		private TcpClient tcpClient;
		
		public Client(string ip, int port)
		{
			IPAddress address = IPAddress.Parse(ip);
			server_endpoint = new IPEndPoint(address, port);
		}

		public string ReadLine()
		{
			return readerStream.ReadLine();
		}
		
		public void WriteLine(string str)
		{
			writerStream.WriteLine(str);
			writerStream.Flush();
		}
		
		public bool ConnectToServer()
		{
			try
			{
				// tcpClient = new TcpClient(server_endpoint);
                tcpClient = new TcpClient("192.168.130.44", 9898);
				
				netStream = tcpClient.GetStream();
				readerStream = new StreamReader(netStream);
				writerStream = new StreamWriter(netStream);
			}
			catch(Exception e)
			{
				Console.WriteLine(e.StackTrace);
				Console.WriteLine(e.Message);
				return false;
			}

			Console.WriteLine("M'he connectat amb el servidor");

			return true;		
		}
	}
}
