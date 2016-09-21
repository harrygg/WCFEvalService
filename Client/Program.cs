using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.EvalServiceReference;
using System.Configuration;

namespace Client
{
    class Program
    {
      static void Main(string[] args)
      {
          Console.WriteLine("########################################");
          Console.WriteLine("# WCF Simple Client 1.0 by HarryGG");
          Console.WriteLine("########################################" + Environment.NewLine);

          string selection = "Type: 1 to add comment, 2 to view comments, 3 to exit";
          Console.WriteLine(selection);
          Console.Write(">");
          string action = Console.ReadLine();
          string comment = null;
          string name = null;
          //EvalServiceClient WSHttpBinding_client = new EvalServiceClient("WSHttpBinding_IEvalService");
          EvalServiceClient WSHttpBinding_client = new EvalServiceClient();

          try 
	        {	        
                while (action != "3" && action != "exit")
                {
                    switch (action)
                    {
                        case "1":
                            Console.WriteLine("Write comment and hit <Enter> to submit:");
                            comment = Console.ReadLine();
                            Console.WriteLine("Write your name and hit <Enter> to submit:");
                            name = Console.ReadLine();

                            Eval eval = new Eval();
                            eval.Comments = comment;
                            eval.Submitter = name;
                            eval.TimeSubmitted = DateTime.Now;

                            System.ServiceModel.Configuration.ClientSection clientSection = 
                                (System.ServiceModel.Configuration.ClientSection)ConfigurationManager.GetSection("system.serviceModel/client");  
                            System.ServiceModel.Configuration.ChannelEndpointElement endpoint = clientSection.Endpoints[0];

                            //string endpointStr = endpoint.Address.ToString(); 
             
                            Console.WriteLine("Submitting comment to endpoint ");// + endpointStr);
                            WSHttpBinding_client.SubmitEval(eval);
                            Console.WriteLine("Comment submitted!" + Environment.NewLine);
                            
                            Console.WriteLine(selection);
                            Console.Write(">");
                            action = Console.ReadLine();
                            break;

                        case "2":
                            Console.WriteLine("Getting list of submitted comments..." );
                            Eval[] evals = WSHttpBinding_client.GetEvals();

                            int i = 0;
                            if (evals.Length > 0)
                            {
                                Console.WriteLine(evals.Length + " comments retrieved successfully:");
                                Console.WriteLine(Environment.NewLine);
                                foreach (Eval ev in evals)
                                    Console.WriteLine(++i + ": " + ev.Comments + " from user " + ev.Submitter);
                            }
                            else
                            {
                                Console.WriteLine(Environment.NewLine);
                                Console.WriteLine("No comments found! I suggest you first add some comments!");
                            }
                            Console.WriteLine(Environment.NewLine);
                            
                            Console.WriteLine(selection);
                            Console.Write(">");
                            action = Console.ReadLine();                     
                            break;
                    }
                }
	        }
	        catch (Exception ex)
	        {
		        Console.WriteLine(ex.ToString());
	        }

            Console.WriteLine("*********************************************************");
            //Console.WriteLine(selection);
            //Console.Write(">");
            //action = Console.ReadLine();
            
        }
    }
}
