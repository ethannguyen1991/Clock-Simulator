using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockSimulation
{
    public static class Class1
    {
        //"The program must simulate a memory space of 10 frames numbered 0 through 9"
        public static IList<MemoryFrame> MemorySpace;
        public const int maxFrames = 10;

        //"The page number should be an integer value between 0 and 99"
        public const int maxPageNumber = 99;
        public const int minPageNumber = 0;

        public static int CurrentFrameIndex;
        public static Random PageNumberGenerator;

        public static void Main()
        {
            PageNumberGenerator = new Random();
            InitializeMemorySpace();
            PromptUserInput();

        }

        public static void PromptUserInput()
        {
            var quit = false;
            while(quit == false)
            {
                DisplayMemorySpace();
                Console.WriteLine("Please insert a number between "+minPageNumber+" and "+maxPageNumber+".  Or \"quit\" to rage quit.");
                var input = Console.ReadLine();
                if (input.Equals("quit"))
                {
                    quit = true;
                }else
                {
                    try
                    {
                        var number = int.Parse(input);
                        if(number<minPageNumber || number > maxPageNumber)
                        {
                            Console.WriteLine("You tripping.. enter a number between 0 and 99 dude");
                            continue;
                        }
                        InsertFrame(number);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("You tripping.. enter a valid value dude");
                    }
                    
                }
            }
        }

        public static void InsertFrame(int pageNumber){
            //Used this example for the insert logic: http://cs.stackexchange.com/questions/24011/clock-page-replacement-algorithm-already-existing-pages
            var found = false;
            //first step - check to see if there's already a same pageNumber, if there is then toggle usebit to true
            foreach (var frame in MemorySpace)
            {
                if(frame.PageNumber == pageNumber)
                {
                    frame.UseBit = true;
                    found = true;
                }
            }

            //if not found, then this will be in a new frame
            if (found == false)
            {
                //If marker/pointer or what you might call it - if it's at the last frame, then set all use bits to false 
                if (CurrentFrameIndex==(maxFrames)) {
                    foreach (var frame in MemorySpace)
                    {
                        frame.UseBit = false;
                    }
                    CurrentFrameIndex = 0;
                }
                //Insert new frame
                if(MemorySpace.Count < maxFrames)
                {
                    MemorySpace.Add(new MemoryFrame() { PageNumber = pageNumber, UseBit = true });
                }
                else
                {
                    MemorySpace[CurrentFrameIndex] = new MemoryFrame() { PageNumber = pageNumber, UseBit = true };
                }
                
                //Move the marker/pointer or what you might call it
                CurrentFrameIndex++;
                
            }

        }

        public static void InitializeMemorySpace()
        {
            //"When it starts, your prgram should randomly fill the array with page numbers and use bits."
            MemorySpace = new List<MemoryFrame>();
            while(MemorySpace.Count < maxFrames)
            {
                var rand = PageNumberGenerator.Next(minPageNumber, maxPageNumber);
                InsertFrame(rand);
            }

            //"The next page pointer variable will start with a value of 0"
            CurrentFrameIndex = 0;
        }

        public static void DisplayMemorySpace()
        {
            Console.WriteLine("PageNumber  UseBit");
            foreach (var currentMemoryFrame in MemorySpace)
            {
                Console.WriteLine(currentMemoryFrame.PageNumber + "  " + currentMemoryFrame.UseBit);
            }

            Console.WriteLine("pagePointer index = " + CurrentFrameIndex);
        }

    }
}
