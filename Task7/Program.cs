using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Task7
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.StreamReader file = new System.IO.StreamReader("../../input.txt");

            List<Step> steps = new List<Step>();


            string line;

            while ((line = file.ReadLine()) != null)
            {
                string[] ss = line.Split(' ');
                char beforeChar = ss[1][0];
                char afterChar = ss[7][0];

                Step before = steps.FirstOrDefault(s => s.Name == beforeChar);
                Step after = steps.FirstOrDefault(s => s.Name == afterChar);

                if (before == null)
                {
                    before = new Step { IsDone = false, Name = beforeChar, BeforeSteps = new List<Step>() };
                    steps.Add(before);
                }
                if (after == null)
                {
                    after = new Step { IsDone = false, Name = afterChar, BeforeSteps = new List<Step>() };
                    steps.Add(after);
                }

                after.BeforeSteps.Add(before);


            }

            file.Close();

            List<Step> order = new List<Step>();

            //while (steps.Count > 0)
            //{
            //    Step nextStep = new Step { Name = (char)('Z' + 1) };

            //    foreach (Step step in steps)
            //    {
            //        if (step.Ready() && step.Name < nextStep.Name)
            //            nextStep = step;
            //    }

            //    nextStep.IsDone = true;
            //    order.Add(nextStep);
            //    steps.Remove(nextStep);
            //}

            List<Worker> workers = new List<Worker>
            {
                new Worker{Step = null,MinutesLeft = -1},
                new Worker{Step = null,MinutesLeft = -1},
                new Worker{Step = null,MinutesLeft = -1},
                new Worker{Step = null,MinutesLeft = -1},
                new Worker{Step = null,MinutesLeft = -1},
            };

            int minutes = 0;
            while (true)
            {
                foreach (Worker worker in workers)
                {
                    if (worker.Step == null)
                    {
                        Step nextStep = new Step {Name = (char) ('Z' + 1)};

                        foreach (Step step in steps)
                        {
                            if (step.Ready() && step.Name < nextStep.Name)
                                nextStep = step;
                        }

                        //nextStep.IsDone = true;
                        //order.Add(nextStep);

                        if (nextStep.Name != (char) ('Z' + 1))
                        {
                            steps.Remove(nextStep);

                            worker.Step = nextStep;
                            worker.MinutesLeft = (61 + (nextStep.Name - 'A'));//
                        }
                    }
                }

                foreach (Worker worker in workers)
                {

                    worker.MinutesLeft--;
                    if (worker.MinutesLeft == 0)
                    {
                        worker.Step.IsDone = true;
                        order.Add(worker.Step);
                        worker.Step = null;
                    }

                }

                if (steps.Count == 0 && workers.Count(s => s.Step == null) == 5) //
                {

                    break;
                }

                minutes++;
            }

            Console.WriteLine(minutes);



            foreach (Step step in order)
            {
                Console.Write(step);
            }

            Console.ReadKey();
        }

        class Worker
        {
            public Step Step { get; set; }  
            public int MinutesLeft { get; set; }
        }
    

    static void Main1(string[] args)
        {
            System.IO.StreamReader file = new System.IO.StreamReader("../../input.txt");

            List<Step> steps = new List<Step>();


            string line;
            
            while ((line = file.ReadLine()) != null)
            {
                string []ss = line.Split(' ');
                char beforeChar = ss[1][0];
                char afterChar = ss[7][0];

                Step before = steps.FirstOrDefault(s => s.Name == beforeChar);
                Step after = steps.FirstOrDefault(s => s.Name == afterChar);

                if (before == null)
                {
                    before = new Step {IsDone = false, Name = beforeChar, BeforeSteps = new List<Step>()};
                    steps.Add(before);
                }
                if (after == null)
                {
                    after = new Step {IsDone = false, Name = afterChar, BeforeSteps = new List<Step>()};
                    steps.Add(after);
                }

                after.BeforeSteps.Add(before);


            }

            file.Close();

            List<Step> order = new List<Step>();

            while (steps.Count > 0)
            {
                Step nextStep = new Step{Name = (char)('Z'+1)};

                foreach (Step step in steps)
                {
                    if (step.Ready() && step.Name < nextStep.Name)
                        nextStep = step;
                }

                nextStep.IsDone = true;
                order.Add(nextStep);
                steps.Remove(nextStep);
            }

            foreach (Step step in order)
            {
                Console.Write(step);
            }

            Console.ReadKey();
        }
    }

    class Step
    {
        public char Name { get; set; }
        public bool IsDone { get; set; }

        public List<Step> BeforeSteps { get; set; }

        public bool Ready()
        {
            return BeforeSteps.Count(s => !s.IsDone) == 0;
        }

        public override string ToString()
        {
            return Name + "";
        }
    }
}
