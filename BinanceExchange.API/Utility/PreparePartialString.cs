namespace BinanceExchange.API.Utility
{
    public class PrepareCombinedSymbols
    {
        public static string CombinedPartialDepth(string allPairs, string depth)
        {
            string[] allPairsArray = allPairs.Split(',');
            for (int i = 0; i < allPairsArray.Length; i++)
            {
                allPairsArray[i] = allPairsArray[i].ToLower() + "@depth" + depth + "/";
            }
            
            return allPairs = string.Join("", allPairsArray);
        }

        public static string CombinedDepth(string allPairs)
        {
            string[] allPairsArray = allPairs.Split(',');
            for (int i = 0; i < allPairsArray.Length; i++)
            {
                allPairsArray[i] = allPairsArray[i].ToLower() + "@depth" + "/";
            }
            
            return allPairs = string.Join("", allPairsArray);
        }
    }
}
