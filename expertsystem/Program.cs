using expertsystem.services;

namespace expertsystem
{
    class Program
    {
        /// <summary>
        /// Entry point of our application
        /// </summary>
        private static void Main()
        {
            // create node service and prepare data
            var nodeService = new SearchService();

            // start searching for node connections
            nodeService.Search();
        }
    }
}