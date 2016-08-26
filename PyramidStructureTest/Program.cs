using System;
using System.Collections.Generic;

/*
 * Javascript / C#:  Use either Javascript or C# to write a code snippet to address the following problems:
 * 
 * Number 1:
 * Assume an array (javascript) or list (C#) of objects in pyramid structure with the following properties:
 * Write a recursive function that accepts an ID as the only parameter and will loop through an array or list of an undetermined size and number of levels.
 * The recursive function will calculate the sum of the value of all the blocks that are in the hierarchy under the ID passed to the function.
 * 
 * Number 2:
 * Problem 2 
 * With the same scenario as in problem 1, write a non-recursive routine that also accepts an ID, and returns a list of id's of all 
 * the blocks that are in the hierarchy under the ID passed to the function.
 */
namespace PyramidStructureTest
{
    public struct PyramidBlock
    {
        public int id;
        public string name;
        public double value;
        public int parentId;
    }

    class Program
    {
        private static List<PyramidBlock> bjk_highLevelPyramid;

        static void Main(string[] args)
        {
            // I decided to use C# as I don't use C# every day like I used to, so this does provide a good exercise
            /*
             * Disclaimer: Originally I never heard of "Pyramid" as a data structure, although I was very familiar with a tree structure,
             * which after a brief Google search, seems to hint that they are the same thing. Indeed, the way the problem is worded leant itself to
             * this tree structure which I was more familiar with. If I'm wrong, well, looks like I failed then.
             */

            // Generate the data structure we'll use for these exercises
            bjk_highLevelPyramid = generateStaticPyramid();

            // We'll output Problem 1 as the total sum of the values of each block underneth the passed ID, since ID = 1 is the root, I went ahead and hard-coded that in
            Console.WriteLine(problemNumberOneRecursiveMethod(1));

            // We'll output Problem 2 as a comma delimited list of all IDs under what we passed, since ID = 1 is the root, I went ahead and hard-coded that in
            Console.WriteLine(string.Join(" , ", problemNumberTwoNonRecursiveMethod(1)));
        }

        /// <summary>
        /// The whole purpose of this is the beginning of this exercise, to understand how this Pyramid/Tree Structure is going to work.
        /// Since the C# version of this specified a List, might as well use that as the base structure. Once I can define a static list, I can go ahead and use the random number
        /// generation to generate a far more dynamic list.
        /// </summary>
        /// <returns>A sample data structure that will act as our Pyramid</returns>
        private static List<PyramidBlock> generateStaticPyramid()
        {
            List<PyramidBlock> rtnVal = new List<PyramidBlock>();

            // Root Row, Parent ID = 0
            rtnVal.Add(new PyramidBlock { id = 1, name = "Block 1", value = 100, parentId = 0 });

            // Second Row
            rtnVal.Add(new PyramidBlock { id = 2, name = "Block 2", value = 200, parentId = 1 });
            rtnVal.Add(new PyramidBlock { id = 3, name = "Block 3", value = 300, parentId = 1 });

            // Third Row
            rtnVal.Add(new PyramidBlock { id = 4, name = "Block 4", value = 400, parentId = 2 });
            rtnVal.Add(new PyramidBlock { id = 5, name = "Block 5", value = 500, parentId = 2 });
            rtnVal.Add(new PyramidBlock { id = 6, name = "Block 6", value = 600, parentId = 3 });
            rtnVal.Add(new PyramidBlock { id = 7, name = "Block 7", value = 700, parentId = 3 });

            return rtnVal;
        }

        /// <summary>
        /// The whole purpose of this is to take the static pyramid we had and go ahead and use random numbers to generate a random pyramid.
        /// WARNING: I ran out of time with this, at this point I was going into the 2 hour mark.
        /// </summary>
        /// <returns>A sample data structure that will act as our Pyramid</returns>
        private static List<PyramidBlock> generateRandomPyramid()
        {
            Random rnd = new Random();
            int numberOfPyramidRows = rnd.Next(2, 15); // At least 2 rows, up to 15
            int idIterator = 1;

            List<PyramidBlock> rtnVal = new List<PyramidBlock>();

            // Root Row, Parent ID = 0, we always start here
            rtnVal.Add(new PyramidBlock { id = idIterator, name = "Block 1", value = rnd.Next(1, 500), parentId = 0 });

            // Generate The Pyramid
            for (int rowIterator = 1; rowIterator <= numberOfPyramidRows; rowIterator++)
            {
                // Do Something (every time I say this I imagine that one scene from the movie "Spaceballs" where an order of "Do Something" gets passed down the chain of command)
            }

            return rtnVal;
        }

        /// <summary>
        /// Use this recursive method to sum up all values under the id of the given node
        /// </summary>
        /// <param name="id">The node ID we are looking for</param>
        /// <returns>Sumed Up Double Value</returns>
        private static double problemNumberOneRecursiveMethod(int id)
        {
            double rtnVal = 0.0;

            // Take the class level object, [bjk_highLevelPyramid] and begin iterating through it
            foreach(PyramidBlock block in bjk_highLevelPyramid)
            {
                // If we find a child block, add the return value of that particular node and start looking for child nodes under that given node
                // This allows us to not know or even care about how deep the initial pyramid is
                if(block.parentId == id)
                {
                    rtnVal += block.value + problemNumberOneRecursiveMethod(block.id);
                }
            }

            return rtnVal;
        }

        /// <summary>
        /// With the same scenario as in problem 1, write a non-recursive routine that also accepts an ID, 
        /// and returns a list of id's of all the blocks that are in the hierarchy under the ID passed to the function.
        /// </summary>
        /// <param name="id">ID of the node we are looking under</param>
        /// <returns>A list of all IDs under the given node</returns>
        private static List<int> problemNumberTwoNonRecursiveMethod(int id)
        {
            List<int> rtnVal = new List<int>();

            // Take the class level object, [bjk_highLevelPyramid] and begin iterating through it
            // we want to at least fill the first level so we have data we can keep going through
            foreach (PyramidBlock block in bjk_highLevelPyramid)
            {
                // If we find a child block, add the return value of that particular node and start looking for child nodes under that given node
                if (block.parentId == id)
                {
                    rtnVal.Add(block.id);
                }
            }

            // Now that we have the first level of children, we can go through those ID numbers and see if those IDs have children themselves
            bool hasMoreChildrenToLookup = true;
            while(hasMoreChildrenToLookup)
            {
                hasMoreChildrenToLookup = false; // Set the while condition to false immediately, we'll set it back to true if we find a necessary match

                // Go through our current list, see if another node exists with the current ID we have, this satisfies the condition that the node is underneth the original ID we passed
                for(int iterator = 0; iterator < rtnVal.Count; iterator++)
                {
                    foreach (PyramidBlock childBlock in bjk_highLevelPyramid)
                    {
                        // Condition 1: Does the parent ID of the child match the ID we have in our collection already
                        // Condition 2: Have we already recorded this ID? We want to avoid duplicate entries (and infinite loops)
                        if (childBlock.parentId == rtnVal[iterator] && !rtnVal.Contains(childBlock.id))
                        {
                            rtnVal.Add(childBlock.id);
                            hasMoreChildrenToLookup = true;
                        }
                    }
                }
            }

            return rtnVal;
        }
    }
}